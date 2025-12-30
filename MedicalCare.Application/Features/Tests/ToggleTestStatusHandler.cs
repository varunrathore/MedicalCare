using MedicalCare.Application.Interfaces;


namespace MedicalCare.Application.Features.Tests
{
    public class ToggleTestStatusHandler
    {
        private readonly ITestRepository _repository;
        public ToggleTestStatusHandler(ITestRepository repository)
        {
            _repository = repository;
        }
        public async Task HandleAsync(ToggleTestStatusCommand command)
        {
            var test = await _repository.GetByIdAsync(command.Id);
            if (test == null)
                throw new Exception("Test not found");
            test.IsActive = !test.IsActive;
            await _repository.UpdateAsync(test);
        }
    }
}
