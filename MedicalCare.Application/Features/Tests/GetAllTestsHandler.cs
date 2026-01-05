using MedicalCare.Application.Interfaces;
using MedicalCare.Domain.Entities;

namespace MedicalCare.Application.Features.Tests
{
    public class GetAllTestsHandler
    {
        private readonly ITestRepository _repository;
        public GetAllTestsHandler(ITestRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<Test>> HandleAsync()
        {
            return await _repository.GetAllWithCategoryAsync();
        }
    }
}
