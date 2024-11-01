class Presupuestos
{
    private int idPresupuesto;
    private string nombreDestinatario;
    private List<PresupuestoDetalle> detalle;

    public Presupuestos()
    {
    }

    public Presupuestos(int idPresupuesto, string nombreDestinatario, List<PresupuestoDetalle> detalle)
    {
        this.idPresupuesto = idPresupuesto;
        this.nombreDestinatario = nombreDestinatario;
        this.detalle = detalle;
    }

    public int IdPresupuesto { get => idPresupuesto; set => idPresupuesto = value; }
    public string NombreDestinatario { get => nombreDestinatario; set => nombreDestinatario = value; }
    internal List<PresupuestoDetalle> Detalle { get => detalle; set => detalle = value; }



    public int montoPresupuesto()
    {
        int monto = 0;
        foreach (var compra in detalle){
            monto += compra.Producto.Precio * compra.Cantidad;
        }
        return monto;
    }

    public double montoPresupuestoConIVA()
    {
        int monto = montoPresupuesto();
        return monto * 1.21;
    }

    public int cantidadDeProductos()
    {
        int cant = 0;
        foreach (var item in detalle)
        {
            cant += item.Cantidad;
        }
        return cant;
    }
}