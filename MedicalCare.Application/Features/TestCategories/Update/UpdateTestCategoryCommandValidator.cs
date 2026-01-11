using FluentValidation;
using MedicalCare.Application.Features.TestCategories.Validators;

namespace MedicalCare.Application.Features.TestCategories.Update;

public class UpdateTestCategoryCommandValidator : TestCategoryBaseValidator<UpdateTestCategoryCommand>
{
   public UpdateTestCategoryCommandValidator()
   {
      RuleFor(x => x.Id)
         .NotEmpty().WithMessage("Invalid Category Id");
      
      ValidateName(x => x.Name);
   }
}