 public class PilaFacturas
    {
        private Stack<Factura> facturas = new Stack<Factura>();

        public void AgregarFactura(int id, int idOrden, double total)
        {
            facturas.Push(new Factura { ID = id, ID_Orden = idOrden, Total = total });
        }
    }