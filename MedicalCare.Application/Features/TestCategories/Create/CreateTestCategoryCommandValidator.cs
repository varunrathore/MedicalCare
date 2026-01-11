using MedicalCare.Application.Features.TestCategories.Validators;

namespace MedicalCare.Application.Features.TestCategories.Create;

public class CreateTestCategoryCommandValidator
    : TestCategoryBaseValidator<CreateTestCategoryCommand>
{
    public CreateTestCategoryCommandValidator()
    {
        ValidateName(x => x.Name);
    }
}



