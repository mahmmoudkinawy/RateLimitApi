using Microsoft.AspNetCore.Mvc;

namespace RateLimitStartupApi.Controllers;

[Route("api/products")]
[ApiController]
public class ProductsController : ControllerBase
{
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { Random.Shared.Next(1, 1000).ToString() };
    }

    [HttpGet("{id}")]
    public string Get(int id)
    {
        return $"Product: {Random.Shared.Next(1, 1000)} with Id: {id}";
    }

}
