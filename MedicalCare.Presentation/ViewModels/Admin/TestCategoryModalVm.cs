namespace MedicalCare.Presentation.ViewModels.Admin
{
    public class TestCategoryModalVm
    {
        public Guid? Id { get; set; }   // null = Create, value = Edit
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; }

        public bool IsEdit => Id.HasValue;
        public bool IsSystemCategory { get; set; }
    }
}
