using MedicalCare.Application.Interfaces;

namespace MedicalCare.Application.Features.TestCategories
{
    public class ToggleTestCategoryStatusHandler
    {
        private readonly ITestRepository _repository;
        public ToggleTestCategoryStatusHandler(ITestRepository repository)
        {
            _repository = repository;
        }
        public async Task HandleAsync(ToggleTestCategoryStatusCommand command)
        {
            var category = await _repository.GetByIdAsync(command.Id);
            if (category == null)
                throw new Exception("Category not found");
            category.IsActive = !category.IsActive;
            await _repository.UpdateAsync(category);
        }
    }
}
