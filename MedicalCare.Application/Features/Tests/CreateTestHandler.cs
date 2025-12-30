using MedicalCare.Application.Interfaces;
using MedicalCare.Domain.Entities;

namespace MedicalCare.Application.Features.Tests
{
    public class CreateTestHandler
    {
        private readonly ITestRepository _repository;
        public CreateTestHandler(ITestRepository repository)
        {
            _repository = repository;
        }
        public async Task HandleAsync(CreateTestCommand command)
        {
            if(string.IsNullOrWhiteSpace(command.Name))
                throw new ArgumentException("Test name cannot be empty");
            if (command.Name.Length < 3)
                throw new AggregateException("Test name must be at least 3 characters long");
            if(command.Price <= 0)
                throw new ArgumentException("Test price must be greater than zero");
            if(command.DurationInMinutes <= 0)
                throw new ArgumentException("Test duration must be greater than zero");

            var existingTest = await _repository.GetByNameAsync(command.Name);
            if (existingTest != null)
                throw new InvalidOperationException("A test with the same name already exists");
            var test = new Test
            {
                Id = Guid.NewGuid(),
                Name = command.Name,
                CategoryId = command.CategoryId,
                Price = command.Price,
                DurationInMinutes = command.DurationInMinutes,
                IsActive = true,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };
            await _repository.AddAsync(test);

        }
    }
}
