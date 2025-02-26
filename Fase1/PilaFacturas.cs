using System;
using System.Runtime.InteropServices;

namespace ListaDobleUnsafe
{
    public unsafe class PilaFacturas
    {
        private NodoFac* top;

        public PilaFacturas()
        {
            top = null;
        }

        public void Push(int id, int idOrden, double costoTotal)
        {
            NodoFac* nuevoNodo = (NodoFac*)NativeMemory.Alloc((nuint)sizeof(NodoFac));
            *nuevoNodo = new NodoFac(id, idOrden, costoTotal);

            nuevoNodo->Next = top;
            top = nuevoNodo;
        }

        public NodoFac* Pop()
        {
            if (top == null)
                return null;

            NodoFac* nodo = top;
            top = top->Next;
            return nodo;
        }

        public void Mostrar()
        {
            NodoFac* actual = top;
            while (actual != null)
            {
                Console.WriteLine(actual->ToString());
                actual = actual->Next;
            }
        }

        ~PilaFacturas()
        {
            while (top != null)
            {
                NodoFac* temp = top;
                top = top->Next;
                NativeMemory.Free(temp);
            }
        }
    }
}
