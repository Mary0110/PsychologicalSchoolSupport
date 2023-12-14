using AutoMapper;
using PsychologicalSupportPlatform.Authorization.Application.Interfaces;
using PsychologicalSupportPlatform.Authorization.Domain.DTOs;
using PsychologicalSupportPlatform.Authorization.Domain.Entities;
using PsychologicalSupportPlatform.Common;

namespace PsychologicalSupportPlatform.Authorization.Application.Services;

public class FormService : IFormService
{
    private readonly IMapper _mapper;
    private readonly IFormRepository _repository;

    public FormService(IFormRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<DataResponseInfo<List<AddFormDTO>>> GetAllFormsAsync(int pageNumber, int pageSize)
    {
        var forms = await _repository.GetAllFormsAsync(pageNumber, pageSize);
        var formDTOs =  _mapper.Map<List<Form>, List<AddFormDTO>>(forms);

        return new DataResponseInfo<List<AddFormDTO>>(data: formDTOs, success: true,
            message: "all forms");
    }

    public async Task<DataResponseInfo<List<AddFormDTO>>> GetFormsByParallelAsync(int num, int pageNumber, int pageSize)
    {
        var forms = await _repository.GetFormsByParallelAsync(num, pageNumber, pageSize);

        if (forms is null) 
        {
            return new DataResponseInfo<List<AddFormDTO>>(data: null, success: false, 
            message: $"parallel {num} not found");
        }
        
        var formDTOs =  _mapper.Map<List<Form>, List<AddFormDTO>>(forms);

        return new DataResponseInfo<List<AddFormDTO>>(data: formDTOs, success: true, message: $"forms of parallel {num}");    
    }

    public async Task<ResponseInfo> DeleteFormAsync(int Num, char Letter)
    {
        var form = await _repository.GetFormAsync(Num, Letter);

        if (form is null)
        {
            return new ResponseInfo(success: false, message: $"form {Num} {Letter} not found");
        }

        await _repository.DeleteFormAsync(form);

        return new ResponseInfo(success: true, message: $"form {form.Parallel} {form.Letter} deleted");    
    }

    public async Task<ResponseInfo> UpdateFormAsync(AddFormDTO formDTO)
    {
        if (formDTO == null)
        {
            return new ResponseInfo(success: false, message: "wrong request data");
        }
        
        var newForm = _mapper.Map<Form>(formDTO);
        var form = await _repository.GetFormAsync(formDTO.Parallel, formDTO.Letter);

        if (form is null)
        {
            return new ResponseInfo(success: false, message: $"form {newForm.Parallel} '{formDTO.Letter}' not found");
        }

        await _repository.EditFormAsync(newForm);

        return new ResponseInfo(success: true, message: $"form {newForm.Parallel} '{formDTO.Letter}' updated");        
    }

    public async Task<ResponseInfo> AddFormAsync(int num, char letter)
    {
        var form = await _repository.GetFormAsync(num, letter);

        if (form is not null)
        {
            return new ResponseInfo(success: false, message: "this form already exists");
        }

        var newForm = new Form{Parallel = num, Letter = letter};
        await _repository.AddFormAsync(newForm);

        form = await _repository.GetFormAsync(newForm.Parallel, newForm.Letter);

        return new ResponseInfo(success: true, message: $"form {form.Parallel} '{form.Letter}' registered");    
    }
}
