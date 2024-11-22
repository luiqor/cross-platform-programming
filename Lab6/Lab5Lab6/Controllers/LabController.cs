using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Lab5Lab6.Models;
using Lab1 = LabLibrary.Lab1;
using Lab2 = LabLibrary.Lab2;
using Lab3 = LabLibrary.Lab3;

namespace Lab5Lab6.Controllers;

[Authorize]
public class LabController : Controller
{
    public ActionResult Lab1()
    {
        var model = new LabViewModel
        {
            InputData = "3 2\n3 2"
        };
        return View(model);
    }

    public ActionResult Lab2()
    {
        var model = new LabViewModel
        {
            InputData = "3\n8:19:16\n2:05:11\n12:50:07"
        };
        return View(model);
    }

    public ActionResult Lab3()
    {
        var model = new LabViewModel
        {
            InputData = "3 3 3\n1..\noo.\n...\n\nooo\n..o\n.oo\n\nooo\no..\no.2"
        };
        return View(model);
    }

    private IActionResult RunLab<TLab>(LabViewModel model, int labNumber) where TLab : new()
    {
        try
        {
            string answer;
            string tempFilePath = Path.GetTempFileName();
            System.IO.File.WriteAllText(tempFilePath, model.InputData);

            dynamic lab = new TLab();
            answer = lab.Run(tempFilePath);

            if (string.IsNullOrEmpty(answer))
            {
                ModelState.AddModelError(string.Empty, "No answer was returned from the lab assignment.");
                return View(model);
            }

            System.IO.File.Delete(tempFilePath);

            ViewBag.Answer = answer;
            ViewBag.Number = labNumber;
            ViewBag.InputData = model.InputData;
            return View("Output");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Error creating user: {ex.Message}");
            return View(model);
        }
    }

    [HttpPost]
    public IActionResult Lab1(LabViewModel model)
    {
        return RunLab<Lab1>(model, 1);
    }

    [HttpPost]
    public IActionResult Lab2(LabViewModel model)
    {
        return RunLab<Lab2>(model, 2);
    }

    [HttpPost]
    public IActionResult Lab3(LabViewModel model)
    {
        return RunLab<Lab3>(model, 3);
    }
}