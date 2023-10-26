using AutoMapper;
using PsychologicalSupportPlatform.Authorization.Application.Interfaces;
using PsychologicalSupportPlatform.Authorization.Domain.DTOs;
using PsychologicalSupportPlatform.Authorization.Domain.Entities;
using PsychologicalSupportPlatform.Common;

namespace PsychologicalSupportPlatform.Authorization.Application.Services;

public class FormService : IFormService
{
    private readonly IMapper mapper;
    private readonly IFormRepository repository;

    public FormService(IFormRepository repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<DataResponseInfo<List<AddFormDTO>>> GetAllFormsAsync(int pageNumber, int pageSize)
    {
        var forms = await repository.GetAllFormsAsync(pageNumber, pageSize);
        var formDTOs =  mapper.Map<List<Form>, List<AddFormDTO>>(forms);

        return new DataResponseInfo<List<AddFormDTO>>(data: formDTOs, success: true,
            message: "all forms");
    }

    public async Task<DataResponseInfo<List<AddFormDTO>>> GetFormsByParallelAsync(int num, int pageNumber, int pageSize)
    {
        var forms = await repository.GetFormsByParallelAsync(num, pageNumber, pageSize);

        if (forms is null) return new DataResponseInfo<List<AddFormDTO>>(data: null, success: false, 
            message: $"parallel {num} not found");
        
        var formDTOs =  mapper.Map<List<Form>, List<AddFormDTO>>(forms);

        return new DataResponseInfo<List<AddFormDTO>>(data: formDTOs, success: true, message: $"forms of parallel {num}");    
    }

    public async Task<ResponseInfo> DeleteFormAsync(AddFormDTO formDTO)
    {
        var form = mapper.Map<Form>(formDTO);
        
        if (form is null) return new ResponseInfo(success: false, message: $"form {formDTO.Parallel} {formDTO.Letter} not found");

        await repository.DeleteFormAsync(form);

        return new ResponseInfo(success: true, message: $"form {form.Parallel} {form.Letter} deleted");    
    }

    public async Task<ResponseInfo> UpdateFormAsync(AddFormDTO formDTO)
    {
        if (formDTO == null) return new ResponseInfo(success: false, message: "wrong request data");
        
        var newForm = mapper.Map<Form>(formDTO);
        var form = await repository.GetFormAsync(formDTO.Parallel, formDTO.Letter);
        
        if (form is null) return new ResponseInfo(success: false, message: $"form {newForm.Parallel} '{formDTO.Letter}' not found");

        await repository.EditFormAsync(newForm);

        return new ResponseInfo(success: true, message: $"form {newForm.Parallel} '{formDTO.Letter}' updated");        
    }

    public async Task<ResponseInfo> AddFormAsync(AddFormDTO formDTO)
    {
        if (formDTO is null) return new ResponseInfo(success: false, message: "wrong request data");
        
        var form = await repository.GetFormAsync(formDTO.Parallel, formDTO.Letter);

        if (form is not null)
        {
            return new ResponseInfo(success: false, message: "this form already exists");
        }

        var newForm = mapper.Map<Form>(formDTO);
        await repository.AddFormAsync(newForm);

        form = await repository.GetFormAsync(newForm.Parallel, newForm.Letter);

        return new ResponseInfo(success: true, message: $"form {form.Parallel} '{form.Letter}' registered");    
    }
}
