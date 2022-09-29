using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.ToDoAppNTier.Dtos.WorkDtos;

namespace Udemy.ToDoAppNTier.Business.ValidationRules
{
    public class WorkCreateDtoValidator : AbstractValidator<WorkCreateDto>
    {
        public WorkCreateDtoValidator()
        {
            //RuleFor(x => x.Definition).NotEmpty().WithMessage("Definition is required!!!").When(x => x.IsCompleted).Must(NotBeBahadir).WithMessage("Definition Bahadır olamaz!!!");
            RuleFor(x => x.Definition).NotEmpty();
        }

        //private bool NotBeBahadir(string arg)
        //{
        //    return arg != "Bahadır" && arg != "bahadır";
        //}
    }
}
