using AutoMapper;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using PsychologicalSupportPlatform.Authorization.Application.Interfaces;
using PsychologicalSupportPlatform.Authorization.Domain.DTOs;
using PsychologicalSupportPlatform.Authorization.Domain.Entities;
using PsychologicalSupportPlatform.Common;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace PsychologicalSupportPlatform.Authorization.Application.Services;

public class LoginService : ILoginService
{
    private readonly IUserRepository userRepository;
    private readonly IStudentRepository studentRepository;
    private readonly IOptions<AuthOptions> authOptions;
    private readonly IMapper mapper;
    private readonly IFormRepository formRepository;
    private readonly IEncryptionService encryption;

    public LoginService(IUserRepository userRepository,
                        IStudentRepository studentRepository,
                        IOptions<AuthOptions> authOptions,
                        IMapper mapper,
                        IFormRepository formRepository,
                        IEncryptionService encryption)
    {
        this.userRepository = userRepository;
        this.studentRepository = studentRepository;
        this.authOptions = authOptions;
        this.mapper = mapper;
        this.formRepository = formRepository;
        this.encryption = encryption;
    }

    public async Task<DataResponseInfo<string>> GetTokenAsync(LoginData data)
    {
        data.Password = encryption.HashPassword(data.Password);
        var user = await userRepository.AuthenticateUserAsync(data.Email, data.Password);

        if (user is null)
        {
            user = await studentRepository.AuthenticateStudentAsync(data.Email, data.Password);
            
            if (user is null) return new DataResponseInfo<string>(data: null, success: false, message: "user is not found");
        }

        var authParams = authOptions.Value;

        var claims = new List<Claim> {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("role", user.Role.ToString()),
        };

        var jwt = new JwtSecurityToken(
            authParams.Issuer,
            authParams.Audience,
            claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(authParams.TokenLifeTime)),
            signingCredentials: new SigningCredentials(
                authParams.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
        );

        return new DataResponseInfo<string>(data: new JwtSecurityTokenHandler().WriteToken(jwt), success: true, 
            message: "token");
    }

    public async Task<DataResponseInfo<List<User>>> GetAllUsersAsync(int pageNumber, int pageSize)
    {
        return new DataResponseInfo<List<User>>(data: await userRepository.GetAllUsersAsync(pageNumber, pageSize), success: true,
            message: "all users");
    }

    public async Task<DataResponseInfo<User>> GetUserByIdAsync(int id)
    {
        var user = await userRepository.GetUserByIdAsync(id);

        if (user is null) return new DataResponseInfo<User>(data: null, success: false, 
            message: $"user with id {id} not found");

        return new DataResponseInfo<User>(data: user, success: true, message: $"user with id {user.Id}");
    }

    public async Task<DataResponseInfo<AddStudentDTO>> GetStudentByIdAsync(int id)
    {
        var user = await studentRepository.GetStudentByIdAsync(id);

        if (user is null) return new DataResponseInfo<AddStudentDTO>(data: null, success: false, 
            message: $"user with id {id} not found");
        
        var studDTO = mapper.Map<AddStudentDTO>(user);

        return new DataResponseInfo<AddStudentDTO>(data: studDTO, success: true, message: $"student with id {user.Id}");
    }

    public async Task<DataResponseInfo<List<AddStudentDTO>>> GetAllStudentsAsync(int pageNumber, int pageSize)
    {
        var students = await studentRepository.GetAllStudentsAsync(pageNumber, pageSize);
        var studDTOs = students.Select(p => mapper.Map<AddStudentDTO>(p)).ToList();

        return new DataResponseInfo<List<AddStudentDTO>>(data: studDTOs, success: true,
            message: "all students");
    }

    public async Task<ResponseInfo> RegisterStudentAsync(AddStudentDTO studentDTO)
    {
        studentDTO.Password = encryption.HashPassword(studentDTO.Password);

        if (studentDTO is null) return new ResponseInfo(success: false, message: "wrong request data");

        var student = await studentRepository.GetStudentByEmailAsync(studentDTO.Email);

        if (student is not null)
        {
            return new ResponseInfo(success: false, message: "this email is already in use");
        }
        
        var user = await userRepository.GetUserByEmailAsync(studentDTO.Email);

        if (user is not null)
        {
            return new ResponseInfo(success: false, message: "this email is already in use by user");
        }

        var form = await formRepository.GetFormAsync(studentDTO.Parallel, studentDTO.Letter);

        if (form is null) return new ResponseInfo(success: false, message: "no such form");

        var newUser = mapper.Map<Student>(studentDTO);
        await studentRepository.RegisterStudentAsync(newUser);
        student = await studentRepository.GetStudentByEmailAsync(newUser.Email);

        return new ResponseInfo(success: true, message: $"user with id {student.Id} registered");    
    }

    public async Task<ResponseInfo> RegisterUserAsync(AddUserDTO userDto)
    {
        userDto.Password = encryption.HashPassword(userDto.Password);

        if (userDto is null) return new ResponseInfo(success: false, message: "wrong request data");

        var user = await userRepository.GetUserByEmailAsync(userDto.Email);

        if (user is not null)
        {
            return new ResponseInfo(success: false, message: "this email is already in use");
        }
        
        var student = await studentRepository.GetStudentByEmailAsync(userDto.Email);

        if (student is not null)
        {
            return new ResponseInfo(success: false, message: "this email is already in use by student");
        }
        
        var newUser = mapper.Map<User>(userDto);
        await userRepository.RegisterUserAsync(newUser);
        user = await userRepository.GetUserByEmailAsync(newUser.Email);

        return new ResponseInfo(success: true, message: $"user with id {user.Id} registered");
    }

    public async Task<ResponseInfo> DeleteUserAsync(int id)
    {
        var user = await userRepository.GetUserByIdAsync(id);

        if (user is null) return new ResponseInfo(success: false, message: $"user with id {id} not found");

        await userRepository.DeleteUserAsync(id);

        return new ResponseInfo(success: true, message: $"user with id {id} deleted");
    }

    public async Task<ResponseInfo> DeleteStudentAsync(int id)
    {
        var user = await studentRepository.GetStudentByIdAsync(id);

        if (user is null) return new ResponseInfo(success: false, message: $"student with id {id} not found");

        await studentRepository.DeleteStudentAsync(id);

        return new ResponseInfo(success: true, message: $"student with id {id} deleted");    
    }

    public async Task<ResponseInfo> UpdateUserAsync(UpdateUserDTO userDto)
    {
        userDto.Password = encryption.HashPassword(userDto.Password);
        var user = await userRepository.GetUserByEmailAsync(userDto.Email);
        var student = await studentRepository.GetStudentByEmailAsync(userDto.Email);

        if (user is not null || student is not null)
        {
            return new ResponseInfo(success: false, message: "user with email already registered");
        }
        
        if (userDto == null) return new ResponseInfo(success: false, message: "wrong request data");
        
        var newUser = mapper.Map<User>(userDto);
        user = await userRepository.GetUserByIdAsync(userDto.Id);
        
        if (user is null) return new ResponseInfo(success: false, message: "user with id {newUser.Id} not found");

        await userRepository.EditUserAsync(newUser);

        return new ResponseInfo(success: true, message: $"user with id {newUser.Id} updated");
    }

    public async Task<ResponseInfo> UpdateStudentAsync(UpdateStudentDTO studentDTO)
    {
        studentDTO.Password = encryption.HashPassword(studentDTO.Password);

        if (studentDTO == null) return new ResponseInfo(success: false, message: "wrong request data");
        
        var user = await userRepository.GetUserByEmailAsync(studentDTO.Email);
        var student = await studentRepository.GetStudentByEmailAsync(studentDTO.Email);

        if (user is not null || student is not null)
        {
            return new ResponseInfo(success: false, message: "user with email already registered");
        }
        
        var newUser = mapper.Map<Student>(studentDTO);
        user = await studentRepository.GetStudentByIdAsync(studentDTO.Id);
        
        if (user is null) return new ResponseInfo(success: false, message: "user with id {newUser.Id} not found");

        await studentRepository.EditStudentAsync(newUser);

        return new ResponseInfo(success: true, message: $"user with id {newUser.Id} updated");    
    }
}
