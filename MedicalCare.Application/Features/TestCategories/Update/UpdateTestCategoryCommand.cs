namespace MedicalCare.Application.Features.TestCategories.Update
{
    public class UpdateTestCategoryCommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
