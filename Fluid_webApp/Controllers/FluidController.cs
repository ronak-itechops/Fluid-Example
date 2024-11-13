using Microsoft.AspNetCore.Mvc;

public class FluidController : Controller
{
    public IActionResult Index()
    {
        var model = new
        {
            Name = "John Doe",
            Age = 30
        };

        return View(model);
    }
}
