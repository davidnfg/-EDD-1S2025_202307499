using System;

namespace ListaDobleUnsafe
{
        public unsafe struct NodeVehi
    {
        public int ID;
        public int ID_Usuario;
        public fixed char Marca[50];
        public int Modelo;
        public fixed char Placa[50];
        public NodeVehi* Next;
        public NodeVehi* Prev;

        public NodeVehi(int id, int id_usuario,string marca, int modelo, string placa)
        {
            ID = id;
            ID_Usuario = id_usuario;
            Modelo = modelo;
            Next = null;
            Prev = null;

            fixed (char* ma = Marca)
            {
                for (int i = 0; i < marca.Length && i < 50; i++)
                    ma[i] = Marca[i];
            }


            fixed (char* pa = Placa)
            {
                for (int i = 0; i < placa.Length && i < 100; i++)
                    pa[i] = Placa[i];
            }
        }
            public override string ToString()
    {
        fixed (char* ma = Marca, pa = Placa)
        {
            string MarcaStr = new string(ma);
            string PlacaStr = new string(pa);

            int MarcaLength = MarcaStr.IndexOf('\0');
            int PlacaLength = PlacaStr.IndexOf('\0');

            if (MarcaLength == -1) MarcaLength = MarcaStr.Length;
            if (PlacaLength == -1) PlacaLength = PlacaStr.Length;

            return $"ID: {ID}, ID_Usuario: {ID_Usuario}, Marca: {MarcaStr.Substring(0, MarcaLength)},Modelo: {Modelo}, Placa: {PlacaStr.Substring(0, PlacaLength)}";
        }
    }
        }
}


