using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Helper.convertirArchivosMaestros("maestroalumnosregulares.csv", "maestromaterias.csv", "maestrocarreras.csv");
            Console.WriteLine("Bienvenido/a al Sistema de Solicitud de Inscripción".ToUpper() + Environment.NewLine);

            Alumno.ingresarRegistro();
            Console.WriteLine(Environment.NewLine + "Bienvenido/a ".ToUpper() + Helper.listaMaestroAlumnos[Alumno.indiceAlumnoLogueado][0].ToUpper() + ' ' + Helper.listaMaestroAlumnos[Alumno.indiceAlumnoLogueado][1].ToUpper() + Environment.NewLine);

            while (Materia.materiasElegidas.Count < 3)
            {
                if (Alumno.quiereAnotarse())
                {
                    Carrera.elegirCarrera();
                    Materia.elegirMaterias();
                } else if(Materia.materiasElegidas.Count > 0)
                {
                    break;
                }
                else
                {
                    Carrera.elegirCarrera();
                    Materia.verMateriasParaAlumno();
                    break;
                }
            }

            if (Materia.materiasElegidas.Count > 0)
            {
                Materia.generarSolicitudInscripcion();
            }
            Console.WriteLine(Environment.NewLine + "Gracias por usar el Sistema de Solicitud de Inscripción");
            Console.WriteLine(Environment.NewLine + "Tocá ENTER para cerrar el programa");
            Console.ReadLine();

        }

    }
}
