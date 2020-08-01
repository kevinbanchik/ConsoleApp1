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
            Console.WriteLine("Bienvenido/a al Sistema de Solicitud de Inscripción");
            
            while(!Alumno.existeAlumno(Helper.listaMaestroAlumnos))
            {
                Console.WriteLine("No existe ningún alumno con ese número de registro. Ingreselo nuevamente.");
            }
                
            Console.WriteLine("Bienvenido/a " + Helper.listaMaestroAlumnos[Alumno.indiceAlumnoLogueado][0] + ' ' + Helper.listaMaestroAlumnos[Alumno.indiceAlumnoLogueado][1] + Environment.NewLine);
            Carrera.verYElegirCarrera();
            Materia.verMateriasParaAlumno();
            Console.WriteLine("Tocá enter para cerrar el programa");
            Console.ReadLine();

        }

    }
}
