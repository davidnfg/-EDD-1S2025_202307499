using System;
using System.Runtime.InteropServices;

namespace ListaDobleUnsafe
{
    public unsafe class ListaDEservicios
{
    public NodoServi* head;
    private NodoServi* tail;

    public ListaDEservicios()
    {
        head = null;
        tail = null;
    }

    public void Insertar(int id, int id_repuesto, int id_vehiculo, string detalles, double costoS)
    {
        NodoServi* nuevoNodo = (NodoServi*)NativeMemory.Alloc((nuint)sizeof(NodoServi));
        *nuevoNodo = new NodoServi(id, id_repuesto, id_vehiculo, detalles, costoS);

        if (head == null)
        {
            head = tail = nuevoNodo;
        }
        else
        {
            tail->Next = (NodoServi*)nuevoNodo;
            nuevoNodo->Prev = (NodoServi*)tail;
            tail = nuevoNodo;
        }
    }

    public void Eliminar(int id)
    {
        NodoServi* actual = head;
        while (actual != null)
        {
            if (actual->ID == id)
            {
                if (actual->Prev != null)
                    ((NodoServi*)actual->Prev)->Next = actual->Next;
                else
                    head = (NodoServi*)actual->Next;

                if (actual->Next != null)
                    ((NodoServi*)actual->Next)->Prev = actual->Prev;
                else
                    tail = (NodoServi*)actual->Prev;

                NativeMemory.Free(actual);
                return;
            }
            actual = (NodoServi*)actual->Next;
        }
    }

    public void Mostrar()
    {
        NodoServi* actual = head;
        while (actual != null)
        {
            Console.WriteLine(actual->ToString());
            actual = (NodoServi*)actual->Next;
        }
    }

    public void MostrarReversa()
    {
        NodoServi* actual = tail;
        while (actual != null)
        {
            Console.WriteLine(actual->ToString());
            actual = (NodoServi*)actual->Prev;
        }
    }
    public unsafe NodoServi* GetHead()
    {
        return head;
    }


    ~ListaDEservicios()
    {
        NodoServi* actual = head;
        while (actual != null)
        {
            NodoServi* temp = actual;
            actual = (NodoServi*)actual->Next;
            NativeMemory.Free(temp);
        }
    }
}
}
