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

        public void Push(int id, int idOrden, double total)
        {
        NodoFac* nuevaFactura = (NodoFac*)NativeMemory.Alloc((nuint)sizeof(NodoFac));
        *nuevaFactura = new NodoFac(id, idOrden, total);
        nuevaFactura->Next = top;
        top = nuevaFactura;
        }

        public NodoFac* Pop()
        {
            if (top == null)
                return null;

            NodoFac* nodo = top;
            top = top->Next;
            return nodo;
        }

        public NodoFac* Peek()
            {
             return top;
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
