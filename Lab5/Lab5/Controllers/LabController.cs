using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Lab5.ViewModels;
using Lab1 = LabLibrary.Lab1;
using Lab2 = LabLibrary.Lab2;
using Lab3 = LabLibrary.Lab3;

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

        string answer;
        string tempFilePath = Path.GetTempFileName();
        System.IO.File.WriteAllText(tempFilePath, model.InputData);

        switch (model.Number)
        {
            case 1:
                Lab1 lab1 = new();
                answer = lab1.Run(tempFilePath);
                break;
            case 2:
                Lab2 lab2 = new();
                answer = lab2.Run(tempFilePath);
                break;
            case 3:
                Lab3 lab3 = new();
                answer = lab3.Run(tempFilePath);
                break;
            default:
                ModelState.AddModelError(string.Empty, $"Lab number {model.Number} is invalid.");
                return View(model);
        }

        if (string.IsNullOrEmpty(answer))
        {
            ModelState.AddModelError(string.Empty, "No answer was returned from the lab assignment.");
            return View(model);
        }

        System.IO.File.Delete(tempFilePath);

        ViewBag.Answer = answer;
        ViewBag.Number = model.Number;
        ViewBag.InputData = model.InputData;
        return View("Output");
    }
}