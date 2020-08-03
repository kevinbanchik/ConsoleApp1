using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    static class Carrera
    {
        public static string carreraSeleccionada;
        public static void verCarreras()
        {
            // IMPRIME CARRERAS EN PANTALLA
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Carreras de la Facultad:".ToUpper()  + Environment.NewLine);
            foreach (var carrera in Helper.listaMaestroCarreras)
            {
                Console.WriteLine(carrera[1] + ", Numero: " + carrera[0]);
            }
        }
        public static void elegirCarrera()
        {
            verCarreras();
            do
            {
                Console.ResetColor();
                Console.WriteLine(Environment.NewLine + "Ingresá el NÚMERO de la carrera de las materias a las cuales te quieras anotar y presiona ENTER." + Environment.NewLine);
            } while (!carreraEsValida(Console.ReadLine()));
        }

        private static bool carreraEsValida(string carreraIngresada)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            if (carreraIngresada.Length == 0)
            {
                Console.WriteLine("Por favor ingrese un número válido de una carrera");
                return false;
            }
            if (carreraIngresada.Length > 1)
            {
                Console.WriteLine("Solo puede ingresar 1 digito numérico entre 1 y 5");
                return false;
            }
            if (!int.TryParse(carreraIngresada, out int carreraEnFormatoNumerico))
            {
                Console.WriteLine("Solo puede ingresar caracteres numéricos");
                return false;
            }
            if (carreraEnFormatoNumerico < 1 || carreraEnFormatoNumerico > 5)
            {
                Console.WriteLine("Solo puede ingresar valores entre 1 y 5");
                return false;
            }
            carreraSeleccionada = carreraIngresada;
            Console.ResetColor();
            return true;
        }
    }
}
