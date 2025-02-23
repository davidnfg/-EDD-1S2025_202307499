public class Servicio
    {
        public int ID { get; set; }
        public int Id_Repuesto { get; set; }
        public int Id_Vehiculo { get; set; }
        public string Detalles { get; set; }
        public double Costo { get; set; }
    }

    public class ColaServicios
    {
        private Queue<Servicio> servicios = new Queue<Servicio>();

        public void AgregarServicio(int id, int idRepuesto, int idVehiculo, string detalles, double costo)
        {
            servicios.Enqueue(new Servicio { ID = id, Id_Repuesto = idRepuesto, Id_Vehiculo = idVehiculo, Detalles = detalles, Costo = costo });
        }
    }

    public class Factura
    {
        public int ID { get; set; }
        public int ID_Orden { get; set; }
        public double Total { get; set; }
    }