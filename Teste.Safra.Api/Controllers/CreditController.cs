using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Teste.Safra.Api.Classes;
using Teste.Safra.Api.Services;
namespace Teste.Safra.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CreditController : ControllerBase
{
    private readonly CreditServices _service;
    private readonly ILogger<CreditController> _logger;

    public CreditController(ILogger<CreditController> logger, CreditServices creditServices)
    {
        _logger = logger;
        _service = creditServices;
    }

    [HttpPost]
    public IActionResult AskForLoan(CreditInputDto input)
    {
        var output = _service.CalcLoan(input);

        return Ok(output);
    }
}
