using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Helper.convertirArchivosMaestros("maestroalumnos.csv", "maestromaterias.csv", "maestrocarreras.csv");
            Console.WriteLine("Bienvenido/a al Sistema de Solicitud de Inscripción" + Environment.NewLine);

            do
            {
                Console.WriteLine(Environment.NewLine + "Ingresá tu número de registro");
            } while (!Alumno.registroEsValido(Console.ReadLine()));

            Console.WriteLine(Environment.NewLine + "Bienvenido/a " + Helper.listaMaestroAlumnos[Alumno.indiceAlumnoLogueado][0] + ' ' + Helper.listaMaestroAlumnos[Alumno.indiceAlumnoLogueado][1] + Environment.NewLine);
           

            while (Materia.indicesSeleccionados.Count < 3 && Materia.quiereAnotarse())
            {
                Carrera.elegirCarrera();
                Materia.elegirMaterias();
            }
            

            Materia.generarSolicitudInscripcion();
            Console.WriteLine(Environment.NewLine +  "Tocá enter para cerrar el programa");
            Console.ReadLine();

        }

    }
}
