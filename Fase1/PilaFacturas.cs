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

        public unsafe string GenerarDot()
        {
            string dot = "digraph PilaFacturas {\n";
            dot += "    rankdir=TB;\n";
            dot += "    node [shape=record];\n";

            NodoFac* actual = top;
            while (actual != null)
            {
                dot += $"    Nodo{actual->ID} [label=\"{{ID: {actual->ID} | ID_Orden: {actual->ID_orden} | Total: Q{actual->CostoTotal}}}\"];\n";

                if (actual->Next != null)
                {
                    dot += $"    Nodo{actual->ID} -> Nodo{actual->Next->ID};\n";
                }

                actual = actual->Next;
            }

            dot += "}";
            return dot;
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
