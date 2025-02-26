using System;
using System.IO;
using System.Diagnostics;

public class Reporte
{
    public static void GenerarReporteGeneral()
    {
        string filePath = "reporte_general.dot";
        using (StreamWriter sw = new StreamWriter(filePath))
        {
            sw.WriteLine("digraph ReporteGeneral {");
            sw.WriteLine("    rankdir=LR;");
            sw.WriteLine("    node [shape=record];");
            
            sw.WriteLine(ListaGlobal.Lista_Usuarios.GenerarDot());
            sw.WriteLine(ListaGlobal.Lista_Vehiculos.GenerarDot());
            
            sw.WriteLine("}");
        }
        EjecutarGraphviz(filePath, "reporte_general.png");
    }

    public static void GenerarReporteUsuarios()
    {
        string filePath = "reporte_usuarios.dot";
        using (StreamWriter sw = new StreamWriter(filePath))
        {
            sw.WriteLine("digraph ReporteUsuarios {");
            sw.WriteLine("    rankdir=LR;");
            sw.WriteLine("    node [shape=record];");
            
            sw.WriteLine(ListaGlobal.Lista_Usuarios.GenerarDot());
            
            sw.WriteLine("}");
        }
        EjecutarGraphviz(filePath, "reporte_usuarios.png");
    }

    public static void GenerarReporteVehiculos()
    {
        string filePath = "reporte_vehiculos.dot";
        using (StreamWriter sw = new StreamWriter(filePath))
        {
            sw.WriteLine("digraph ReporteVehiculos {");
            sw.WriteLine("    rankdir=LR;");
            sw.WriteLine("    node [shape=record];");
            
            sw.WriteLine(ListaGlobal.Lista_Vehiculos.GenerarDot());
            
            sw.WriteLine("}");
        }
        EjecutarGraphviz(filePath, "reporte_vehiculos.png");
    }

    private static void EjecutarGraphviz(string dotFilePath, string outputFilePath)
    {
        Process process = new Process();
        process.StartInfo.FileName = "dot";
        process.StartInfo.Arguments = $"-Tpng {dotFilePath} -o {outputFilePath}";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;
        process.Start();
        process.WaitForExit();
    }
}
