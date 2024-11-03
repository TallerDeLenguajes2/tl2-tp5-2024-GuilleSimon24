using Microsoft.AspNetCore.Mvc;

namespace MiWebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductosController : ControllerBase
{

    private readonly ILogger<ProductosController> _logger;

    private ProductoRepository repoProductos;

    public ProductosController(ILogger<ProductosController> logger)
    {
        _logger = logger;
        repoProductos = new ProductoRepository();
    }

    [HttpPost("api/Producto")]
    public IActionResult CrearProductos(Productos producto)
    {
        repoProductos.CrearProducto(producto);
        return Created();

    }

    [HttpGet("api/Productos")]
    public ActionResult<List<Productos>> GetProductos()
    {
        return Ok(repoProductos.getProductos());
    }


    [HttpPut("api/Producto/{id}")]
    public IActionResult ModificarProductos(int id, Productos producto)
    {
        repoProductos.ModificarProducto(id, producto);
        return Ok();

    }

}