namespace MedicalCare.Domain.Policies;

public static class TestCategoryPolicy
{
    public static bool IsSystem(Guid categoryId)
    {
        return categoryId == Guid.Parse("11111111-1111-1111-1111-111111111111")
            || categoryId == Guid.Parse("22222222-2222-2222-2222-222222222222")
            || categoryId == Guid.Parse("33333333-3333-3333-3333-333333333333")
            || categoryId == Guid.Parse("44444444-4444-4444-4444-444444444444")
            || categoryId == Guid.Parse("55555555-5555-5555-5555-555555555555")
            || categoryId == Guid.Parse("66666666-6666-6666-6666-666666666666")
            || categoryId == Guid.Parse("77777777-7777-7777-7777-777777777777")
            || categoryId == Guid.Parse("88888888-8888-8888-8888-888888888888")
            || categoryId == Guid.Parse("99999999-9999-9999-9999-999999999999")
            || categoryId == Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
    }
}
