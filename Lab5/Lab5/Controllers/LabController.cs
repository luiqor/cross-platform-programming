using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Lab5.ViewModels;

namespace Lab5.Controllers;

[Authorize]
public class LabController : Controller
{
    public ActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Submit(LabViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // TODO: call the lab code here
        // Lab1 lab1 = new();
        // answer = lab1.Run(inputPath);

        Console.WriteLine(model.InputData);
        return RedirectToAction("Index");
    }
}