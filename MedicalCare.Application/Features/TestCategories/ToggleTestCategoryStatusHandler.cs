using MedicalCare.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalCare.Application.Features.TestCategories
{
    public class ToggleTestCategoryStatusHandler
    {
        private readonly ITestCategoryRepository _repository;
        public ToggleTestCategoryStatusHandler(ITestCategoryRepository repository)
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
