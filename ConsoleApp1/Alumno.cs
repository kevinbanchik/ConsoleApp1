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
        public static string registroSeleccionado;
        public static bool existeAlumno(string registroIngresado, List<string[]> maestroAlumnos)
        {
            int i = 0;
            foreach (var alumno in maestroAlumnos)
            {
                if (registroIngresado == alumno[2])
                {
                    indiceAlumnoLogueado = i;
                    return true;
                }
                else
                {
                    i++;
                }
            }
            return false;
        }

        public static bool registroEsValido(string registroIngresado)
        {
            if (registroIngresado.Length > 6 )
            {
                Console.WriteLine("El registro solo puede tener hasta 6 digitos numéricos");
                return false;
            }
            if (!int.TryParse(registroIngresado, out int registroEnFormatoNumerico))
            {
                Console.WriteLine("El registro solo puede contener caracteres numéricos");
                return false;
            }
            if (registroEnFormatoNumerico < 1 || registroEnFormatoNumerico > 999999)
            {
                Console.WriteLine("El registro tiene que ser mayor a 1 y menor a 999999");
                return false;
            }
            if (!existeAlumno(registroIngresado, Helper.listaMaestroAlumnos))
            {
                Console.WriteLine("No existe ningún alumno con ese número de registro. Ingreselo nuevamente.");
                return false;
            }
            registroSeleccionado = registroIngresado;
            return true;
        }

        public static bool quiereAnotarse()
        {
            // MUESTRA CANTIDAD DE MATERIAS POSIBLES PARA INSCRIBIRSE
            // PRESIONA 'ENTER' SI QUIERE ANOTARSE
            ConsoleKeyInfo tecla;
            do
            {
                if (Materia.indicesSeleccionados.Count == 0)
                {
                    Console.WriteLine(Environment.NewLine + "Presioná la tecla A para inscribirte o la tecla B para solo ver tu oferta de materias");
                } else
                {
                    Console.WriteLine(Environment.NewLine + "Presioná la tecla A para inscribirte a otras materias o la tecla B para finalizar la inscripción");
                }
                tecla = Console.ReadKey();
            } while (!Equals(ConsoleKey.A, tecla.Key) && !Equals(ConsoleKey.B, tecla.Key));
            
            if (Equals(ConsoleKey.A, tecla.Key))
            {
                //Console.WriteLine(Environment.NewLine + "apretaste enter " + tecla.Key);
                return true;
            }
            else
            {
                //Console.WriteLine(Environment.NewLine + "apretaste " + tecla.Key);
                return false;
            }
        }
    }
}
