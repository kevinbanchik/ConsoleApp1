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
        public static List<string> materiasElegidas = new List<string>();
        public static List<string> indicesMostrados = new List<string>();
        public static List<string[]> materiasMostradas = new List<string[]>();
        public static bool continuarEligiendo = true;
        public static List<string> nombresDeMateriasDeCursosSeleccionados = new List<string>();

        public static void verMateriasParaAlumno()
        {
            // MUESTRA LA OFERTA PERSONALIZADA PARA EL ALUMNO EN BASE A LA CARRERA SELECCIONADA

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
            Console.WriteLine(Environment.NewLine + "Te quedan " + (3 - materiasElegidas.Count).ToString() + " materia/s para elegir" + Environment.NewLine);
            verMateriasParaAlumno();
            do
            {
                Console.ResetColor();
                Console.WriteLine("Ingresá el IDENTIFICADOR del curso elegido. Si elegis más de uno separalos por una coma (Ejemplo: 0,2,3)");
                Console.WriteLine("Recordá que podés elegir solo 1 curso por materia, y hasta 3 cursos en total. Al finalizar presiona ENTER." + Environment.NewLine);
            } while (!materiaEsValida(Console.ReadLine()));
        }

        private static bool materiaEsValida(string indicesIngresados)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            string regexSoloEnterosYComa = @"^\d+(,\d+)*$";
            if (indicesIngresados.Length == 0)
            {
                Console.WriteLine("Por favor ingrese un identificador v");
                return false;
            }

            if (!Regex.IsMatch(indicesIngresados, regexSoloEnterosYComa))
            {
                Console.WriteLine("Debe respetar el formato de ingreso" + Environment.NewLine);
                return false;
            }
            // SI INGRESA MÁS DE UNA MATERIA
            if (indicesIngresados.Contains(','))
            {
                List<string> indicesIngresadosSinComa = indicesIngresados.Split(',').ToList();
                if ((materiasElegidas.Count + indicesIngresadosSinComa.Count) > 3)
                {
                    Console.WriteLine("Solo puede anotarse a un máximo de 3 materias" + Environment.NewLine);

                    return false;
                    
                }
                else
                {
                    // SI EN LOS INDICES ELEGIDOS HAY REPETICIONES
                    if (indicesIngresadosSinComa.GroupBy(n => n).Any(c => c.Count() > 1))
                    {
                        Console.WriteLine("No puede seleccionar un mismo curso más de una vez" + Environment.NewLine);
                        return false;
                    }
                    
                    // SI LOS INDICES ELEGIDOS NO COINCIDEN CON LOS MOSTRADOS EN LISTA
                    List<string> indicesCoincidentesEntreMostradosEIngresados = indicesMostrados.Intersect(indicesIngresadosSinComa).ToList();
                    if (indicesCoincidentesEntreMostradosEIngresados.Count != indicesIngresadosSinComa.Count)
                    {
                        Console.WriteLine("Debes elegir materias que esten en la lista" + Environment.NewLine);
                        return false;
                    }

                    // SI YA ELIGIO MATERIAS, GUARDA EL NOMBRE EN UNA LISTA 
                    nombresDeMateriasDeCursosSeleccionados.Clear();
                    if (materiasElegidas.Count > 0)
                    {
                        foreach (var indiceMateria in materiasElegidas)
                        {
                            nombresDeMateriasDeCursosSeleccionados.Add(Helper.listaMaestroMateriasPorAlumno[int.Parse(indiceMateria) - 1][3]);
                        }
                    }

                    // GUARDO LOS NOMBRES DE LAS MATERIAS INGRESADAS EN LA LISTA 
                    foreach (var IdCurso in indicesIngresadosSinComa)
                    {
                        nombresDeMateriasDeCursosSeleccionados.Add(Helper.listaMaestroMateriasPorAlumno[int.Parse(IdCurso) - 1][3]);
                    }

                    // VALIDO QUE NO SE ANOTE EN UNA MATERIA YA ELEGIDA
                    if (nombresDeMateriasDeCursosSeleccionados.GroupBy(n => n).Any(c => c.Count() > 1))
                    {
                        Console.WriteLine(Environment.NewLine + "No podés seleccionar más de un curso de una misma materia" + Environment.NewLine);
                        return false;
                    }

                    // QUE NO SE ANOTE EN UN CURSO YA ELEGIDO
                    List<string> indicesCoincidentesEntreElegidosEIngresadas = materiasElegidas.Intersect(indicesIngresadosSinComa).ToList();
                    if (indicesCoincidentesEntreElegidosEIngresadas.Count > 0)
                    {
                        Console.WriteLine("Ya elegiste la materia con identificador " + indicesCoincidentesEntreElegidosEIngresadas[0] + Environment.NewLine);
                        return false;
                    }
                    else
                    {
                        materiasElegidas.AddRange(indicesIngresadosSinComa);
                    }
                }
            }
            else // CUANDO INGRESA DE A UNA MATERIA
            {
                // QUE LA MATERIA INGRESADA EXISTA EN LA LISTA MOSTRADA
                if (!indicesMostrados.Contains(indicesIngresados))
                {
                    Console.WriteLine("La materia con identificador " + indicesIngresados + " no está en la lista mostrada" + Environment.NewLine);
                    return false;
                }
                // QUE LA MATERIA INGRESADA NO HAYA SIDO ELEGIDA
                if (materiasElegidas.Contains(indicesIngresados)) 
                {
                    Console.WriteLine("Ya elegiste la materia con identificador " + indicesIngresados + Environment.NewLine);
                    return false;
                }
                else
                {
                    // SI YA ELIGIO MATERIAS, GUARDA EL NOMBRE EN LA LISTA 
                    if (materiasElegidas.Count > 0)
                    {
                        nombresDeMateriasDeCursosSeleccionados.Clear();
                        foreach (var indiceMateria in materiasElegidas)
                        {
                            nombresDeMateriasDeCursosSeleccionados.Add(Helper.listaMaestroMateriasPorAlumno[int.Parse(indiceMateria) - 1][3]);
                        }
                        // VALIDO QUE NO SE ANOTE EN UNA MATERIA YA ELEGIDA
                        if (nombresDeMateriasDeCursosSeleccionados.Count > 0 && nombresDeMateriasDeCursosSeleccionados.Contains(Helper.listaMaestroMateriasPorAlumno[int.Parse(indicesIngresados) - 1][3]))
                        {
                            Console.WriteLine(Environment.NewLine + "No podés seleccionar más de un curso de una misma materia" + Environment.NewLine);
                            return false;
                        }
                    }
                    materiasElegidas.Add(indicesIngresados);
                    nombresDeMateriasDeCursosSeleccionados.Add(Helper.listaMaestroMateriasPorAlumno[int.Parse(indicesIngresados) - 1][3]);
                }
            }
            Console.ResetColor();
            return true;
        }

        public static void generarSolicitudInscripcion()
        {
            List<string[]> listaMateriasElegidas = Helper.listaMaestroMateriasPorAlumno.FindAll(materia => materiasElegidas.Contains(materia[0]));

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("SOLICITUD DE INSCRIPCIÓN DE " + Helper.listaMaestroAlumnos[Alumno.indiceAlumnoLogueado][0].ToUpper() + ' ' + Helper.listaMaestroAlumnos[Alumno.indiceAlumnoLogueado][1].ToUpper() + Environment.NewLine);

            foreach(var materia in listaMateriasElegidas)
            {
                try
                {
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter("solicitud_" + Helper.listaMaestroAlumnos[Alumno.indiceAlumnoLogueado][0] + '_' + Helper.listaMaestroAlumnos[Alumno.indiceAlumnoLogueado][1] + ".csv", false))
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
