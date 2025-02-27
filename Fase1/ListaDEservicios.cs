using System;
using System.Runtime.InteropServices;
using ListaDobleUnsafe;

namespace ListaDobleUnsafe
{
    public unsafe class ColaServicios
    {
        public NodoServi* head;
        private NodoServi* tail;

        public ColaServicios()
        {
            head = null;
            tail = null;
        }

        public void Enqueue(int id, int id_repuesto, int id_vehiculo, string detalles, double costoS)
        {
            NodoServi* nuevoNodo = (NodoServi*)NativeMemory.Alloc((nuint)sizeof(NodoServi));
            *nuevoNodo = new NodoServi(id, id_repuesto, id_vehiculo, detalles, costoS);

            if (head == null)
            {
                head = tail = nuevoNodo;
            }
            else
            {
                tail->Next = nuevoNodo;
                tail = nuevoNodo;
            }
        }

        public void Dequeue()
        {
            if (head == null) return;

            NodoServi* temp = head;
            head = head->Next;

            if (head == null)
            {
                tail = null;
            }

            NativeMemory.Free(temp);
        }

        public void Mostrar()
        {
            NodoServi* actual = head;
            while (actual != null)
            {
                Console.WriteLine(actual->ToString());
                actual = actual->Next;
            }
        }

        public void Insertar(int id, int idRepuesto, int idVehiculo, string detalles, double costo)
        {
            Enqueue(id, idRepuesto, idVehiculo, detalles, costo);
        }

        public unsafe NodoServi* GetHead()
        {
            return head;
        }

        ~ColaServicios()
        {
            NodoServi* actual = head;
            while (actual != null)
            {
                NodoServi* temp = actual;
                actual = actual->Next;
                NativeMemory.Free(temp);
            }
        }
    }
}