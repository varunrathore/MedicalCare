using MedicalCare.Application.Features.Tests;
using MedicalCare.Presentation.ViewModels.Admin;
using Microsoft.AspNetCore.Mvc;

namespace MedicalCare.Presentation.Controllers.Admin
{
    [Route("Admin/[controller]/[action]")]
    public class TestsController : Controller
    {
        private readonly CreateTestHandler _createHandler;
        private readonly GetAllTestsHandler _getAllHandler;
        private readonly UpdateTestHandler _updateHandler;
        private readonly ToggleTestStatusHandler _toggleHandler;

        public TestsController(
            CreateTestHandler createHandler,
            UpdateTestHandler updateHandler,
            GetAllTestsHandler getAllHandler,
            ToggleTestStatusHandler toggleHandler)
        {
            _createHandler = createHandler;
            _getAllHandler = getAllHandler;
            _updateHandler = updateHandler;
            _toggleHandler = toggleHandler;
        }

        public async Task<IActionResult> Index()
        {
            var tests = await _getAllHandler.HandleAsync();
            return View(tests);
        }
        public IActionResult Create()
        {
            return PartialView("_TestModal", new TestModalVm());
        }
        public IActionResult Edit(Guid id, string name, decimal price, int durationInMinutes)
        {
            return PartialView("_TestModal", new TestModalVm
            {
                Id = id,
                Name = name,
                Price = price,
                DurationInMinutes = durationInMinutes
            });
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
                        DurationInMinutes = vm.DurationInMinutes
                    });
                }
                else
                {
                    await _createHandler.HandleAsync(new CreateTestCommand
                    {
                        Name = vm.Name,
                        Price = vm.Price,
                        DurationInMinutes = vm.DurationInMinutes
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
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
