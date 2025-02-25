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
                    ma[i] = marca[i];
                    ma[Math.Min(marca.Length, 49)] = '\0'; 
            }


            fixed (char* pa = Placa)
            {
                for (int i = 0; i < placa.Length && i < 100; i++)
                    pa[i] = placa[i];

                     pa[Math.Min(placa.Length, 49)] = '\0';
            }
        }
            public override string ToString()
    {
        fixed (char* ma = Marca, pa = Placa)
        {
            string MarcaStr = new string(ma, 0, 50).TrimEnd('\0');

            string PlacaStr = new string(pa);

            
            int MarcaLength = MarcaStr.IndexOf('\0');
                if (MarcaLength == -1) // Si no encuentra '\0', usamos la cadena completa
                    MarcaLength = MarcaStr.Length;
            string MarcaReal = MarcaStr.Substring(0, MarcaLength);
            
            int PlacaLength = PlacaStr.IndexOf('\0');
                if (PlacaLength == -1) // Si no encuentra '\0', usamos la cadena completa
                    PlacaLength = PlacaStr.Length;

                string PlacaReal = PlacaStr.Substring(0, PlacaLength);


            return $"ID: {ID}, ID_Usuario: {ID_Usuario}, Marca: {MarcaStr.Substring(0, MarcaLength)},Modelo: {Modelo}, Placa: {PlacaStr.Substring(0, PlacaLength)}";
        }
    }
        }
}


