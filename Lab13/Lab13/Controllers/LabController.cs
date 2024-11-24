using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Lab1 = LabLibrary.Lab1;
using Lab2 = LabLibrary.Lab2;
using Lab3 = LabLibrary.Lab3;
using Lab13.Models;

namespace Lab13.Controllers;

[Route("api/lab")]
[ApiController]
[Authorize]
public class LabController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        var response = new { message = "Hello from LabController!" };
        return Ok(response);
    }

    private IActionResult RunLab<TLab>(LabDto model, int labNumber) where TLab : new()
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
                return BadRequest(new { error = "No answer was returned from the lab assignment." });
            }

            System.IO.File.Delete(tempFilePath);

            var result = new
            {
                Answer = answer,
                Number = labNumber,
                InputData = model.InputData
            };

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = $"Error running lab: {ex.Message}" });
        }
    }

    [HttpPost("lab1")]
    public IActionResult Lab1([FromBody] LabDto model)
    {
        return RunLab<Lab1>(model, 1);
    }

    [HttpPost("lab2")]
    public IActionResult Lab2([FromBody] LabDto model)
    {
        return RunLab<Lab2>(model, 2);
    }

    [HttpPost("lab3")]
    public IActionResult Lab3([FromBody] LabDto model)
    {
        return RunLab<Lab3>(model, 3);
    }
}
