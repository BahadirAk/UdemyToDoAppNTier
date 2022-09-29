using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.ToDoAppNTier.Business.Extensions;
using Udemy.ToDoAppNTier.Business.Interfaces;
using Udemy.ToDoAppNTier.Common.ResponseObjects;
using Udemy.ToDoAppNTier.DataAccess.UnitOfWork;
using Udemy.ToDoAppNTier.Dtos.Interfaces;
using Udemy.ToDoAppNTier.Dtos.WorkDtos;
using Udemy.ToDoAppNTier.Entities.Domains;

namespace Udemy.ToDoAppNTier.Business.Services
{
    public class WorkService : IWorkService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<WorkCreateDto> _createDtoValidator;
        private readonly IValidator<WorkUpdateDto> _updateDtoValidator;

        public WorkService(IUow uow, IMapper mapper, IValidator<WorkCreateDto> createDtoValidator, IValidator<WorkUpdateDto> updateDtoValidator)
        {
            _uow = uow;
            _mapper = mapper;
            _createDtoValidator = createDtoValidator;
            _updateDtoValidator = updateDtoValidator;
        }

        public async Task<IResponse<WorkCreateDto>> Create(WorkCreateDto dto)
        {
            var validationResult = _createDtoValidator.Validate(dto);
            if (validationResult.IsValid)
            {
                await _uow.GetRepository<Work>().Create(_mapper.Map<Work>(dto));
                await _uow.SaveChanges();
                return new Response<WorkCreateDto>(ResponseType.Success, dto);
            }
            else
            {
                return new Response<WorkCreateDto>(ResponseType.ValidationError, dto, validationResult.ConvertToCustomValidationError());
            }
        }

        public async Task<IResponse<List<WorkListDto>>> GetAll()
        {
            var list = await _uow.GetRepository<Work>().GetAll();

            //var workList = new List<WorkListDto>();

            //if(list != null && list.Count > 0)
            //{
            //    foreach (var work in list)
            //    {
            //        workList.Add(new()
            //        {
            //            Id = work.Id,
            //            Definition = work.Definition,
            //            IsCompleted = work.IsCompleted
            //        });
            //    }
            //}
            //return workList;

            var data = _mapper.Map<List<WorkListDto>>(list);
            return new Response<List<WorkListDto>>(ResponseType.Success, data);
        }

        public async Task<IResponse<IDto>> GetById<IDto>(int id)
        {
            var work = await _uow.GetRepository<Work>().GetByFilter(w => w.Id == id);

            //return new()
            //{
            //    Id = work.Id,
            //    Definition = work.Definition,
            //    IsCompleted = work.IsCompleted
            //};

            var mappedData = _mapper.Map<IDto>(work);
            if (mappedData == null)
            {
                return new Response<IDto>(ResponseType.NotFound, "Data Bulunamadı!!!");
            }
            return new Response<IDto>(ResponseType.Success, mappedData);
        }

        public async Task<IResponse> Remove(int id)
        {
            var removedEntity = await _uow.GetRepository<Work>().GetByFilter(x => x.Id == id);
            if (removedEntity != null)
            {
                _uow.GetRepository<Work>().Remove(removedEntity);
                await _uow.SaveChanges();
                return new Response(ResponseType.Success);
            }
            return new Response(ResponseType.NotFound, "Data Bulunamadı!!!");
        }

        public async Task<IResponse<WorkUpdateDto>> Update(WorkUpdateDto dto)
        {
            //var work = await _uow.GetRepository<Work>().GetById(dto.Id);

            //work.Definition = dto.Definition;
            //work.IsCompleted = dto.IsCompleted;

            var validationResult = _updateDtoValidator.Validate(dto);
            if (validationResult.IsValid)
            {
                var updatedEntity = await _uow.GetRepository<Work>().Find(dto.Id);
                if (updatedEntity != null)
                {
                    _uow.GetRepository<Work>().Update(_mapper.Map<Work>(dto), updatedEntity);
                    await _uow.SaveChanges();
                    return new Response<WorkUpdateDto>(ResponseType.Success, dto);
                }
                return new Response<WorkUpdateDto>(ResponseType.NotFound, "Data Bulunamadı!!!");
            }
            else
            {
                return new Response<WorkUpdateDto>(ResponseType.ValidationError, dto, validationResult.ConvertToCustomValidationError());
            }
        }
    }
}
