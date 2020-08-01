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
        public static void verYElegirCarrera()
        {
            // IMPRIME CARRERAS EN PANTALLA Y DEVUELVE NUMERO DE CARRERA SELECCIONADA
            Console.WriteLine("Ingresá el NÚMERO de la carrera de las materias a las cuales te quieras anotar" + Environment.NewLine);
            foreach (var carrera in Helper.listaMaestroCarreras)
            {
                Console.WriteLine(carrera[1] + ", Numero: " + carrera[0]);
            }
            Console.WriteLine(Environment.NewLine);
            carreraSeleccionada = Console.ReadLine();
        }
    }
}
