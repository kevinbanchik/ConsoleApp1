using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    static class Materia
    {
        public static void verMateriasParaAlumno()
        {
            Console.WriteLine("Estas son las materias a las cuales te podés inscribir:");

            foreach (var materia in Helper.listaMaestroMateriasPorAlumno)
            {
                if (materia[0] == Alumno.registroIngresado && materia[1] == Carrera.carreraSeleccionada)
                {
                    Console.WriteLine(materia[2] + ", Curso: " + materia[3]);
                }
            }
            Console.WriteLine(Environment.NewLine);
        }
    }
}
