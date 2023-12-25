using System.Net;
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

        return new DataResponseInfo<List<AddFormDTO>>(data: formDTOs, status: HttpStatusCode.OK);
    }

    public async Task<DataResponseInfo<List<AddFormDTO>>> GetFormsByParallelAsync(int num, int pageNumber, int pageSize)
    {
        var forms = await _repository.GetFormsByParallelAsync(num, pageNumber, pageSize);

        if (forms is null) 
        {
            return new DataResponseInfo<List<AddFormDTO>>(data: null, status: HttpStatusCode.NotFound);
        }
        
        var formDTOs =  _mapper.Map<List<Form>, List<AddFormDTO>>(forms);

        return new DataResponseInfo<List<AddFormDTO>>(data: formDTOs, status: HttpStatusCode.OK);    
    }

    public async Task<ResponseInfo> DeleteFormAsync(int Num, char Letter)
    {
        var form = await _repository.GetFormAsync(Num, Letter);

        if (form is null)
        {
            return new ResponseInfo(status: HttpStatusCode.NotFound);
        }

        await _repository.DeleteFormAsync(form);

        return new ResponseInfo(status: HttpStatusCode.NoContent);    
    }

    public async Task<ResponseInfo> UpdateFormAsync(AddFormDTO formDTO)
    {
        var newForm = _mapper.Map<Form>(formDTO);
        var form = await _repository.GetFormAsync(formDTO.Parallel, formDTO.Letter);

        if (form is null)
        {
            return new ResponseInfo(status: HttpStatusCode.NotFound);
        }

        await _repository.EditFormAsync(newForm);

        return new ResponseInfo(status: HttpStatusCode.OK);        
    }

    public async Task<ResponseInfo> AddFormAsync(int num, char letter)
    {
        var form = await _repository.GetFormAsync(num, letter);

        if (form is not null)
        {
            return new ResponseInfo(status: HttpStatusCode.NotFound);
        }

        var newForm = new Form{Parallel = num, Letter = letter};
        await _repository.AddFormAsync(newForm);
        
        return new ResponseInfo(status: HttpStatusCode.OK);    
    }
}
