using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    static class Materia
    {
        public static List<string> indicesSeleccionados = new List<string>();
        public static List<string> indicesMostrados = new List<string>();
        public static List<string[]> materiasMostradas = new List<string[]>();
        public static bool continuarEligiendo = true;
        public static List<string> materiasDeCursosSeleccionados = new List<string>();

        public static void verMateriasParaAlumno()
        {
            // MUESTRA LA OFERTA PERSONALIZADA PARA EL ALUMNO
            
            Console.WriteLine(Environment.NewLine + "Estas son las materias a las cuales te podés inscribir:");
            materiasMostradas.Clear();
            indicesMostrados.Clear();
        
            foreach (var materia in Helper.listaMaestroMateriasPorAlumno)
            {
                if (materia[1] == Alumno.registroSeleccionado && materia[2] == Carrera.carreraSeleccionada)
                {
                    materiasMostradas.Add(materia);
                    indicesMostrados.Add(materia[0]);
                    Console.WriteLine("- " + materia[3] + ", Curso: " + materia[4] + ", Identificador único: " + materia[0]);
                }
            }
            Console.WriteLine(Environment.NewLine);
        }

        public static void elegirMaterias()
        {
            Console.WriteLine(Environment.NewLine + "Te quedan " + (3 - Materia.indicesSeleccionados.Count).ToString() + " materia/s para elegir" + Environment.NewLine);
            verMateriasParaAlumno();
            do
            {
                Console.WriteLine("Ingresá los IDENTIFICADORES de los cursos elegidos separados por una coma (Ejemplo: 0,2,3). Recordá que podés elegir solo 1 curso por materia, y hasta 3 cursos en total." + Environment.NewLine);
            } while (!materiaEsValida(Console.ReadLine()));
        }

        private static bool materiaEsValida(string indicesIngresados)
        {
            string regexSoloEnterosYComa = @"^\d+(,\d+)*$";
            
            if (!Regex.IsMatch(indicesIngresados, regexSoloEnterosYComa))
            {
                Console.WriteLine("Debe respetar el formato de ingreso" + Environment.NewLine);
                return false;
            }
            if (indicesIngresados.Contains(','))
            {
                List<string> indicesSinComa = indicesIngresados.Split(',').ToList();
                if ((indicesSeleccionados.Count + indicesSinComa.Count) > 3)
                {
                    Console.WriteLine("Solo puede anotarse a un máximo de 3 materias" + Environment.NewLine);

                    return false;
                    
                }
                else
                {
                    List<string> indicesRepetidos = indicesMostrados.Intersect(indicesSinComa).ToList();
                    if (indicesSinComa.GroupBy(n => n).Any(c => c.Count() > 1))
                    {
                        Console.WriteLine("No puede seleccionar un mismo curso más de una vez" + Environment.NewLine);
                        return false;
                    }
                    
                    // SI NO LOS INDICES ELEGIDOS NO COINCIDEN CON LOS MOSTRADOS 
                    if (indicesRepetidos.Count != indicesSinComa.Count)
                    {
                        Console.WriteLine("Debes elegir materias que esten en la lista" + Environment.NewLine);
                        return false;
                    }

                    // TO DO: FALTA VALIDAR QUE NO SE REPITA LA MATERIA

                    if (indicesSeleccionados.Count > 0)
                    {
                        foreach (var indice in indicesSeleccionados)
                        {
                            materiasDeCursosSeleccionados.Add(Helper.listaMaestroMateriasPorAlumno[int.Parse(indice) - 1][3]);
                        }
                    }

                    foreach (var indice in indicesSinComa)
                    {
                        materiasDeCursosSeleccionados.Add(Helper.listaMaestroMateriasPorAlumno[int.Parse(indice) - 1][3]);
                    }

                    if (materiasDeCursosSeleccionados.GroupBy(n => n).Any(c => c.Count() > 1))
                    {
                        Console.WriteLine(Environment.NewLine + "No podés seleccionar más de un curso de una misma materia" + Environment.NewLine);
                        return false;
                    }

                    List<string> indicesDuplicados = indicesSeleccionados.Intersect(indicesSinComa).ToList();
                    if (indicesDuplicados.Count > 0)
                    {
                        Console.WriteLine("Ya elegiste la materia con identificador " + indicesDuplicados[0] + Environment.NewLine);
                        return false;
                    }
                    else
                    {
                        indicesSeleccionados.AddRange(indicesSinComa);
                    }
                }
            } 
            else
            {
                if (!indicesMostrados.Contains(indicesIngresados))
                {
                    Console.WriteLine("La materia con identificador " + indicesIngresados + " no está en la lista mostrada" + Environment.NewLine);
                    return false;
                }
                if (indicesSeleccionados.Contains(indicesIngresados)) 
                {
                    Console.WriteLine("Ya elegiste la materia con identificador " + indicesIngresados + Environment.NewLine);
                    return false;
                }
                else
                {
                    if (materiasDeCursosSeleccionados.Count > 0 && materiasDeCursosSeleccionados.Contains(Helper.listaMaestroMateriasPorAlumno[int.Parse(indicesIngresados) - 1][3]))
                    {
                        Console.WriteLine(Environment.NewLine + "No podés seleccionar más de un curso de una misma materia" + Environment.NewLine);
                        return false;

                    }
                    indicesSeleccionados.Add(indicesIngresados);
                    materiasDeCursosSeleccionados.Add(Helper.listaMaestroMateriasPorAlumno[int.Parse(indicesIngresados) - 1][3]);
                }
            }
            return true;
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
