using Microsoft.AspNetCore.Mvc;

namespace MiWebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PresupuestosController : ControllerBase
{

    private readonly ILogger<PresupuestosController> _logger;

    private PresupuestoRepository repoPresupuestos;

    public PresupuestosController(ILogger<PresupuestosController> logger)
    {
        _logger = logger;
        repoPresupuestos = new PresupuestoRepository();
    }
    [HttpPost("api/Presupuesto")]
    public IActionResult CrearPresupuesto(Presupuestos presupuesto)
    {
        if(!repoPresupuestos.CrearPresupuesto(presupuesto)) return BadRequest();
        return Created();
    }

    [HttpPost("api/Presupuesto/{id}/ProductoDetalle")]
    public IActionResult AgregarProductoAlPresupuesto(int idPresupuesto, int idProducto, int cantidad)
    {
        if(!repoPresupuestos.AgregarProducto(idPresupuesto, idProducto, cantidad)) return BadRequest();
        return Created();
    }

    [HttpGet("api/Presupuestos")]
    public ActionResult<List<Presupuestos>> GetPresupuestos()
    {
        return Ok(repoPresupuestos.GetPresupuestos());
    }

    [HttpGet("api/Presupuestos/{id}")]
    public ActionResult<Presupuestos> GetPresupuestoPorId(int id)
    {
        return Ok(repoPresupuestos.ObtenerPresupuestoPorId(id));
    }

    [HttpDelete]

    public IActionResult BorrarPresupuesto(int id)
    {
        repoPresupuestos.DeletePresupuesto(id);
        return Ok();
    }



}