using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

namespace tl2_tp5_2024_GuilleSimon24.Controllers;


[ApiController]
[Route("[controller]")]

public class ConductoresController : ControllerBase
{

    private readonly ILogger<ConductoresController> _logger;

    public ConductoresController(ILogger<ConductoresController> logger)
    {
        _logger = logger;
        

    }

    
}