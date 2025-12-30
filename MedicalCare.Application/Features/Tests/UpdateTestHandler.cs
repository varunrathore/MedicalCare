using MedicalCare.Application.Interfaces;

namespace MedicalCare.Application.Features.Tests
{
    public class UpdateTestHandler
    {
        private readonly ITestRepository _repository;
        public UpdateTestHandler(ITestRepository repository)
        {
            _repository = repository;
        }

        public async Task HandleAsync(UpdateTestCommand command)
        {
            var test = await _repository.GetByIdAsync(command.Id);
            if (test == null)
                throw new Exception("Test not found");

            var name = command.Name?.Trim();
            if (string.IsNullOrWhiteSpace(name))
                throw new InvalidOperationException("Test name cannot be empty");

            if (name.Length < 3)
                throw new InvalidOperationException("Test name must be at least 3 characters long");

            var existingTest = await _repository.GetByNameAsync(name);
            if (existingTest != null && existingTest.Id != command.Id)
                throw new InvalidOperationException("A test with the same name already exists");

            test.Name = name;
            test.Price = command.Price;
            test.DurationInMinutes = command.DurationInMinutes;
            test.CategoryId = command.CategoryId;
            test.UpdatedOn = DateTime.UtcNow;
            await _repository.UpdateAsync(test);
        }
    }
}
