using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    static class Materia
    {
        public static List<string> indicesSeleccionados = new List<string>();
        public static void verMateriasParaAlumno()
        {
            Console.WriteLine(Environment.NewLine + "Estas son las materias a las cuales te podés inscribir:");

            foreach (var materia in Helper.listaMaestroMateriasPorAlumno)
            {
                if (materia[1] == Alumno.registroIngresado && materia[2] == Carrera.carreraSeleccionada)
                {
                    Console.WriteLine("- " + materia[3] + ", Curso: " + materia[4] + ", Identificador único: " + materia[0]);
                }
            }
            Console.WriteLine(Environment.NewLine);
        }

        public static void elegirMaterias()
        {
            string indicesIngresados;
            verMateriasParaAlumno();

            Console.WriteLine("Ingresá los IDENTIFICADORES de los cursos elegidos separados por una coma (Ejemplo: 0,2,3). Recordá que podés elegir solo 1 curso por materia, y hasta 3 cursos en total." + Environment.NewLine);
            indicesIngresados = Console.ReadLine();
            if (indicesIngresados.Contains(','))
            {
                List<string> indicesPorComa = indicesIngresados.Split(',').ToList();
                indicesSeleccionados.AddRange(indicesPorComa);
            } else
            {
                indicesSeleccionados.Add(indicesIngresados);
            }
        }

        public static void generarSolicitudInscripcion()
        {
            List<string[]> listaMateriasElegidas = Helper.listaMaestroMateriasPorAlumno.FindAll(materia => indicesSeleccionados.Contains(materia[0]));

            Console.WriteLine(Environment.NewLine + "SOLICITUD DE INSCRIPCIÓN" + Environment.NewLine);

            foreach(var materia in listaMateriasElegidas)
            {
                try
                {
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter("solicitud.csv", true))
                    {
                        file.WriteLine(materia[0] + ';' + materia[1] + ';' + materia[2] + ';' + materia[3] + ';' + materia[4]);
                        Console.WriteLine("Materia: " + materia[3] + ", Curso: " + materia[4] + ", Identificador único: " + materia[0]);
                    }
                }

                catch (Exception ex)
                {
                    throw new ApplicationException("error", ex);
                }
            }

        }
    }
}
