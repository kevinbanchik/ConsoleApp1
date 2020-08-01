using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    static class Helper
    {
        public static List<string[]> listaMaestroAlumnos = new List<string[]>();
        public static List<string[]> listaMaestroMateriasPorAlumno = new List<string[]>();
        public static List<string[]> listaMaestroCarreras = new List<string[]>();
        public static void convertirArchivosMaestros(string filepathAlumnos, string filepathMaterias, string filepathCarreras)
        // CONVIERTE ARCHIVOS MAESTROS EN LISTAS DE ARRAYS DE STRING
        {
            using (StreamReader sr = new StreamReader(filepathAlumnos))
            {
                string linea;
                while ((linea = sr.ReadLine()) != null)
                {
                    listaMaestroAlumnos.Add(linea.Split(';'));
                }
            }

            using (StreamReader sr2 = new StreamReader(filepathMaterias))
            {
                string linea;
                while ((linea = sr2.ReadLine()) != null)
                {
                    listaMaestroMateriasPorAlumno.Add(linea.Split(';'));
                }
            }

            using (StreamReader sr3 = new StreamReader(filepathCarreras))
            {
                string linea;
                while ((linea = sr3.ReadLine()) != null)
                {
                    listaMaestroCarreras.Add(linea.Split(';'));
                }
            }
        }
    }
}
