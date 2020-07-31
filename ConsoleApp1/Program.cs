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
        static private List<String[]> listaMaestroAlumnos = new List<String[]>();
        static private List<String[]> listaMaestroMateriasPorAlumno = new List<String[]>();
        static private List<String[]> listaMaestroCarreras = new List<String[]>();
        static private int indiceAlumnoLogueado;
        static private string registroIngresado;
        static private string carreraSeleccionada;
        static void Main(string[] args)
        {
            // agregarEstudiante(Console.ReadLine(), Console.ReadLine(), Console.ReadLine(), "maestroalumnos.csv");
            convertirArchivosMaestros("maestroalumnos.csv", "maestromaterias.csv", "maestrocarreras.csv");
            Console.WriteLine("Bienvenido/a al Sistema de Solicitud de Inscripción");
            
            while(!existeAlumno(listaMaestroAlumnos))
            {
                Console.WriteLine("No existe ningún alumno con ese número de registro. Ingreselo nuevamente.");
            }
                
            Console.WriteLine("Bienvenido/a " + listaMaestroAlumnos[indiceAlumnoLogueado][0] + ' ' + listaMaestroAlumnos[indiceAlumnoLogueado][1] + Environment.NewLine);
            carreraSeleccionada = verYElegirCarrera();
            verMateriasParaAlumno();
            Console.WriteLine("Tocá enter para cerrar el programa");
            Console.ReadLine();

        }

        public static string verYElegirCarrera()
        {
            // IMPRIME CARRERAS EN PANTALLA Y DEVUELVE NUMERO DE CARRERA SELECCIONADA
            Console.WriteLine("Ingresá el NÚMERO de la carrera de las materias a las cuales te quieras anotar" + Environment.NewLine);
            foreach (var carrera in listaMaestroCarreras)
            {               
                Console.WriteLine(carrera[1] + ", Numero: " + carrera[0]);
            }
            Console.WriteLine(Environment.NewLine);
            return Console.ReadLine();
        }

        public static bool existeAlumno(List<String[]> maestroAlumnos)
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
                } else
                {
                    i++;
                }
            }
            if (existe == true)
            {
                return true;
            } else
            {
                return false;
            }
        }

        public static void verMateriasParaAlumno()
        {
            Console.WriteLine("Estas son las materias a las cuales te podés inscribir:");

            foreach (var materia in listaMaestroMateriasPorAlumno)
            {
                if (materia[0] == registroIngresado && materia[1] == carreraSeleccionada)
                {
                    Console.WriteLine(materia[2] + ", Curso: " + materia[3]);
                }
            }
            Console.WriteLine(Environment.NewLine);
        }

        public static void agregarAlumno(string nombre, string apellido, string registro, string filepath)
        {
            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(filepath, true))
                {
                    file.WriteLine(nombre + ';' + apellido + ';' + registro + ';');
                }
            }

            catch(Exception ex)
            {
                throw new ApplicationException("error", ex);
            }
        }

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
