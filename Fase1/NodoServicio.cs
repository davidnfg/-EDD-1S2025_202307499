using System;

namespace ListaDobleUnsafe
{
    public unsafe struct NodoServi
{
    public int ID;
    public int ID_repuesto;
    public int ID_vehiculo;
    public fixed char Detalles[50];
    public double CostoServi;
    public NodoServi* Next;
    public NodoServi * Prev;

    public NodoServi(int id, int id_repuesto, int id_vehiculo, string detalles, double costoS)
    {
        ID = id;
        ID_repuesto = id_repuesto;
        ID_vehiculo = id_vehiculo;

        CostoServi = costoS;
        Next = null;
        Prev = null;

        fixed (char* de = Detalles)
        {
            for (int i = 0; i < detalles.Length && i < 50; i++)
                de[i] = detalles[i];
        }

    }

        public override string ToString()
{
    fixed (char* de = Detalles)
    {
        string detallesStr = new string(de);

        int detallesLength = detallesStr.IndexOf('\0');

        if (detallesLength == -1) detallesLength = detallesStr.Length;

        return $"ID: {ID}, ID_Repuesto: {ID_repuesto}, ID_Vehiculo: {ID_vehiculo} Detalles: {detallesStr.Substring(0, detallesLength)}, Costo: Q{CostoServi}";
    }
}
}
}



