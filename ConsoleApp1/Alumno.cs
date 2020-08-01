using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    static class Alumno
    {
        public static int indiceAlumnoLogueado;
        public static string registroIngresado;
        public static bool existeAlumno(List<string[]> maestroAlumnos)
        {
            Console.WriteLine("Ingresá tu número de registro");
            registroIngresado = Console.ReadLine();
            bool existe = false;
            int i = 0;
            foreach (var alumno in maestroAlumnos)
            {
                if (registroIngresado == alumno[2])
                {
                    existe = true;
                    indiceAlumnoLogueado = i;
                }
                else
                {
                    i++;
                }
            }
            if (existe == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
