using System;

namespace ListaDobleUnsafe
{
    public unsafe struct NodoFac
{
    public int ID;
    public int ID_orden;
    public double CostoTotal;
    public NodoFac* Next;
    public NodoFac * Prev;

    public NodoFac(int id, int id_orden, double costoT)
    {
        ID = id;
        ID_orden = id_orden;
        CostoTotal = costoT;
        Next = null;
        Prev = null;
    }
    public override string ToString()
    {

        return $"ID: {ID}, ID_Orden: {ID_orden}, Costo Total: Q{CostoTotal}";
    }
}
}




