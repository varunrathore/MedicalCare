using MedicalCare.Application.Features.Tests;
using MedicalCare.Presentation.ViewModels.Admin;
using Microsoft.AspNetCore.Mvc;
using MedicalCare.Application.Interfaces;

namespace MedicalCare.Presentation.Controllers.Admin
{
    [Route("Admin/[controller]/[action]")]
    public class TestsController : Controller
    {
        private readonly CreateTestHandler _createHandler;
        private readonly GetAllTestsHandler _getAllHandler;
        private readonly UpdateTestHandler _updateHandler;
        private readonly ToggleTestStatusHandler _toggleHandler;
        private readonly ITestRepository _testRepository;
        private readonly ITestCategoryRepository _testCategoryRepository;

        public TestsController(
            CreateTestHandler createHandler,
            UpdateTestHandler updateHandler,
            GetAllTestsHandler getAllHandler,
            ToggleTestStatusHandler toggleHandler,
            ITestRepository testRepository,
            ITestCategoryRepository testCategoryRepository)
        {
            _createHandler = createHandler;
            _getAllHandler = getAllHandler;
            _updateHandler = updateHandler;
            _toggleHandler = toggleHandler;
            _testCategoryRepository = testCategoryRepository;
            _testRepository = testRepository;
        }

        public async Task<IActionResult> Index()
        {
            var tests = await _getAllHandler.HandleAsync();
            return View(tests);
        }
        public async Task<IActionResult> Create()
        {
            var categories =  await _testCategoryRepository.GetAllAsync();
            var vm = new TestModalVm
            {
                Categories = categories
            };
            return PartialView("_TestModal", vm);
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            var test = await _testRepository.GetByIdAsync(id);
            if (test == null)
                return NotFound();

            var categories = await _testCategoryRepository.GetAllAsync();

            var vm = new TestModalVm
            {
                Id = id,
                Name = test.Name,
                Price = test.Price,
                DurationInMinutes = test.DurationInMinutes,
                CategoryId = test.CategoryId,
                Categories = categories
            };
            return PartialView("_TestModal", vm);
        }
        public  async Task<IActionResult> Save(TestModalVm vm)
        {
            try
            {
                if(ModelState.IsValid == false)
                    return BadRequest();

                if(vm.IsEdit)
                {
                    await _updateHandler.HandleAsync(new UpdateTestCommand
                    {
                        Id = vm.Id!.Value,
                        Name = vm.Name,
                        Price = vm.Price,
                        DurationInMinutes = vm.DurationInMinutes,
                        CategoryId = vm.CategoryId
                    });
                }
                else
                {
                    await _createHandler.HandleAsync(new CreateTestCommand
                    {
                        Name = vm.Name,
                        Price = vm.Price,
                        DurationInMinutes = vm.DurationInMinutes,
                        CategoryId = vm.CategoryId
                    });

                }
                    // Implementation for saving test details goes here
                    return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> ToggleStatus(Guid id)
        {
            try
            {
                await _toggleHandler.HandleAsync(new ToggleTestStatusCommand
                {
                    Id = id
                });
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
