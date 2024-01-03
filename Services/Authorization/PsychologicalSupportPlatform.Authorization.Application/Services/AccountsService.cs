using AutoMapper;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using PsychologicalSupportPlatform.Authorization.Application.Interfaces;
using PsychologicalSupportPlatform.Authorization.Domain.DTOs;
using PsychologicalSupportPlatform.Authorization.Domain.Entities;
using PsychologicalSupportPlatform.Common;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PsychologicalSupportPlatform.Common.Config;

namespace PsychologicalSupportPlatform.Authorization.Application.Services;

public class AccountsService : IAccountsService
{
    private readonly IUserRepository _userRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly IOptions<AuthOptions> _authOptions;
    private readonly IMapper _mapper;
    private readonly IFormRepository _formRepository;
    private readonly IEncryptionService _encryption;

    public AccountsService(IUserRepository userRepository,
                        IStudentRepository studentRepository,
                        IOptions<AuthOptions> authOptions,
                        IMapper mapper,
                        IFormRepository formRepository,
                        IEncryptionService encryption)
    {
        _userRepository = userRepository;
        _studentRepository = studentRepository;
        _authOptions = authOptions;
        _mapper = mapper;
        _formRepository = formRepository;
        _encryption = encryption;
    }

    public async Task<DataResponseInfo<string?>> GetTokenAsync(LoginData data)
    {
        data.Password = _encryption.HashPassword(data.Password);
        var user = await _userRepository.AuthenticateUserAsync(data.Email, data.Password);

        if (user is null)
        {
            user = await _studentRepository.AuthenticateStudentAsync(data.Email, data.Password);

            if (user is null)
            {
                return new DataResponseInfo<string?>(data: null, status: HttpStatusCode.NotFound);
            }
        }

        var authParams = _authOptions.Value;

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

        return new DataResponseInfo<string?>(data: new JwtSecurityTokenHandler().WriteToken(jwt), status:HttpStatusCode.OK);
    }

    public async Task<DataResponseInfo<List<User>>> GetAllUsersAsync(int pageNumber, int pageSize)
    {
        return new DataResponseInfo<List<User>>(data: await _userRepository.GetAllUsersAsync(pageNumber, pageSize), 
            status:HttpStatusCode.OK);
    }

    public async Task<DataResponseInfo<User?>> GetUserByIdAsync(int id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);

        if (user is null) 
        {
            return new DataResponseInfo<User?>(data: null, status:HttpStatusCode.NotFound);
        }

        return new DataResponseInfo<User?>(data: user, status: HttpStatusCode.OK);
    }

    public async Task<DataResponseInfo<AddStudentDTO?>> GetStudentByIdAsync(int id)
    {
        var user = await _studentRepository.GetStudentByIdAsync(id);

        if (user is null) 
        {
            return new DataResponseInfo<AddStudentDTO?>(data: null, status:HttpStatusCode.NotFound);
        }
        
        var studDTO = _mapper.Map<AddStudentDTO>(user);

        return new DataResponseInfo<AddStudentDTO?>(data: studDTO, status: HttpStatusCode.OK);
    }

    public async Task<DataResponseInfo<List<AddStudentDTO>>> GetAllStudentsAsync(int pageNumber, int pageSize)
    {
        var students = await _studentRepository.GetAllStudentsAsync(pageNumber, pageSize);
        var studDTOs =  _mapper.Map<List<Student>, List<AddStudentDTO>>(students);

        return new DataResponseInfo<List<AddStudentDTO>>(data: studDTOs, status: HttpStatusCode.OK);
    }

    public async Task<ResponseInfo> RegisterStudentAsync(AddStudentDTO studentDTO)
    {
        studentDTO.Password = _encryption.HashPassword(studentDTO.Password);

        var student = await _studentRepository.GetStudentByEmailAsync(studentDTO.Email);

        if (student is not null)
        {
            return new ResponseInfo(status:HttpStatusCode.NotFound);
        }
        
        var user = await _userRepository.GetUserByEmailAsync(studentDTO.Email);

        if (user is not null)
        {
            return new ResponseInfo(status:HttpStatusCode.NotFound);
        }

        var form = await _formRepository.GetFormAsync(studentDTO.Parallel, studentDTO.Letter);

        if (form is null)
        {
            return new ResponseInfo(status:HttpStatusCode.NotFound);
        }

        var newUser = _mapper.Map<Student>(studentDTO);
        newUser.Role = Roles.Student;
        
        await _studentRepository.RegisterStudentAsync(newUser);

        return new ResponseInfo(status: HttpStatusCode.OK);    
    }

    public async Task<ResponseInfo> RegisterUserAsync(AddUserDTO userDto)
    {
        userDto.Password = _encryption.HashPassword(userDto.Password);

        var user = await _userRepository.GetUserByEmailAsync(userDto.Email);

        if (user is not null)
        {
            return new ResponseInfo(status: HttpStatusCode.Conflict);
        }
        
        var student = await _studentRepository.GetStudentByEmailAsync(userDto.Email);

        if (student is not null)
        {
            return new ResponseInfo(status: HttpStatusCode.Conflict);
        }
        
        var newUser = _mapper.Map<User>(userDto);
        await _userRepository.RegisterUserAsync(newUser);

        return new ResponseInfo(status: HttpStatusCode.OK);
    }

    public async Task<ResponseInfo> DeleteUserAsync(int id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);

        if (user is null)
        {
            return new ResponseInfo(status: HttpStatusCode.NotFound);
        }

        await _userRepository.DeleteUserAsync(id);

        return new ResponseInfo(status: HttpStatusCode.NoContent);
    }

    public async Task<ResponseInfo> DeleteStudentAsync(int id)
    {
        var user = await _studentRepository.GetStudentByIdAsync(id);

        if (user is null)
        {
            return new ResponseInfo(status: HttpStatusCode.NotFound);
        }

        await _studentRepository.DeleteStudentAsync(id);

        return new ResponseInfo(status: HttpStatusCode.NoContent);    
    }

    public async Task<ResponseInfo> UpdateUserAsync(int id, UpdateUserDTO userDto)
    {
        userDto.Password = _encryption.HashPassword(userDto.Password);
        var user = await _userRepository.GetUserByEmailAsync(userDto.Email);
        var student = await _studentRepository.GetStudentByEmailAsync(userDto.Email);

        if (user is not null && user.Id != id || student is not null && student.Id != id)
        {
            return new ResponseInfo(status: HttpStatusCode.Conflict);
        }
        
        var newUser = _mapper.Map<User>(userDto);
        newUser.Id = id;
        user = await _userRepository.GetUserByIdAsync(id);

        if (user is null)
        {
            return new ResponseInfo(status: HttpStatusCode.NotFound);
        }

        newUser.Role = user.Role;

        await _userRepository.EditUserAsync(newUser);

        return new ResponseInfo(status: HttpStatusCode.OK);
    }

    public async Task<ResponseInfo> UpdateStudentAsync(int id, UpdateStudentDTO studentDTO)
    {
        studentDTO.Password = _encryption.HashPassword(studentDTO.Password);

        var user = await _userRepository.GetUserByEmailAsync(studentDTO.Email);
        var student = await _studentRepository.GetStudentByEmailAsync(studentDTO.Email);

        if (user is not null  && user.Id != id || student is not null  && student.Id != id)
        {
            return new ResponseInfo(status:HttpStatusCode.Conflict);
        }
        
        var newUser = _mapper.Map<Student>(studentDTO);
        newUser.Id = id;
        user = await _studentRepository.GetStudentByIdAsync(id);

        if (user is null)
        {
            return new ResponseInfo(status:HttpStatusCode.NotFound);
        }
        
        newUser.Role = user.Role;
        await _studentRepository.EditStudentAsync(newUser);

        return new ResponseInfo(status: HttpStatusCode.OK);    
    }

    public async Task<DataResponseInfo<List<AddStudentDTO>>> GetStudentsByFormAsync(AddFormDTO formDTO, int pageNumber, int pageSize)
    {
        var form = await _formRepository.GetFormAsync(formDTO.Parallel, formDTO.Letter);
        
        if (form is null) 
        {
            return new DataResponseInfo<List<AddStudentDTO>>(data: null, status:HttpStatusCode.NotFound);
        }
        
        var users = await _studentRepository.GetStudentsByFormAsync(form, pageNumber, pageSize);

        var studDTOs = _mapper.Map<List<Student>, List<AddStudentDTO>>(users);

        return new DataResponseInfo<List<AddStudentDTO>>(data: studDTOs, status: HttpStatusCode.OK);
    }
    
    public async Task<DataResponseInfo<List<AddStudentDTO>>> GetStudentsByParallelAsync(int num, int pageNumber, int pageSize)
    {
        var users = await _studentRepository.GetStudentsByParallelAsync(num, pageNumber, pageSize);

        var studDTOs = _mapper.Map<List<Student>, List<AddStudentDTO>>(users);

        return new DataResponseInfo<List<AddStudentDTO>>(data: studDTOs, status: HttpStatusCode.OK);
    }
}
