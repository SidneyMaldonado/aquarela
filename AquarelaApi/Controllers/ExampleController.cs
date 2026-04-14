using Microsoft.AspNetCore.Mvc;

namespace AquarelaApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExampleController : ControllerBase
{
    [HttpGet("hello")]
    public IActionResult GetHello()
    {
        return Ok(new { message = "Hello from AquarelaApi" });
    }

    [HttpGet("values")]
    public IActionResult GetValues()
    {
        var values = new[] { "red", "blue", "green" };
        return Ok(values);
    }
}
