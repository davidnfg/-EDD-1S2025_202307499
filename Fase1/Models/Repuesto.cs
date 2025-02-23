using System;
using System.Runtime.InteropServices;

namespace AutoGestPro.Models
{
    public unsafe class Repuesto
    {
        public int ID;
        public string RepuestoNombre;
        public string Detalles;
        public double Costo;
        public Repuesto* Siguiente;
    }

    public unsafe class ListaRepuestos
    {
        private Repuesto* cabeza;

        public void AgregarRepuesto(int id, string repuestoNombre, string detalles, double costo)
        {
            Repuesto* nuevo = (Repuesto*)Marshal.AllocHGlobal(sizeof(Repuesto));
            nuevo->ID = id;
            nuevo->RepuestoNombre = repuestoNombre;
            nuevo->Detalles = detalles;
            nuevo->Costo = costo;
            nuevo->Siguiente = null;

            if (cabeza == null)
            {
                cabeza = nuevo;
            }
            else
            {
                Repuesto* actual = cabeza;
                while (actual->Siguiente != null)
                {
                    actual = actual->Siguiente;
                }
                actual->Siguiente = nuevo;
            }
        }

        public bool EliminarRepuesto(int id)
        {
            if (cabeza == null) return false;
            if (cabeza->ID == id)
            {
                Repuesto* temp = cabeza;
                cabeza = cabeza->Siguiente;
                Marshal.FreeHGlobal((IntPtr)temp);
                return true;
            }

            Repuesto* actual = cabeza;
            while (actual->Siguiente != null && actual->Siguiente->ID != id)
            {
                actual = actual->Siguiente;
            }

            if (actual->Siguiente == null) return false;
            Repuesto* temp2 = actual->Siguiente;
            actual->Siguiente = actual->Siguiente->Siguiente;
            Marshal.FreeHGlobal((IntPtr)temp2);
            return true;
        }

        public Repuesto* BuscarRepuesto(int id)
        {
            Repuesto* actual = cabeza;
            while (actual != null)
            {
                if (actual->ID == id)
                    return actual;
                actual = actual->Siguiente;
            }
            return null;
        }

        public void MostrarRepuestos()
        {
            Repuesto* actual = cabeza;
            while (actual != null)
            {
                Console.WriteLine($"ID: {actual->ID}, Repuesto: {actual->RepuestoNombre}, Detalles: {actual->Detalles}, Costo: {actual->Costo}");
                actual = actual->Siguiente;
            }
        }
    }
}