using MedicalCare.Domain.Policies;

namespace MedicalCare.Domain.Entities
{
    public class TestCategory
    {
        public Guid Id { get; set; }
        public bool IsSystem()
        {
            return TestCategoryPolicy.IsSystem(Id);
        }
        public required string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
