using System;
using Microsoft.Data.Sqlite;

namespace RRHH
{
    class Program
    {
        static void Main(string[] args)
        {
            int opcion;
            opcion = MuestraMenu();
            
            while(opcion!=0)
            {
                switch(opcion)
                {
                    case 1:
                        ListaPersonas();
                        break;

                    case 2:
                        AgregaPersona();
                        break;
                    
                    case 0:
                        return;

                }
                opcion = MuestraMenu();
            }
        }

        static int MuestraMenu()
        {
            // Limpia la pantalla
            Console.Clear();

            Console.WriteLine("Sistema de Directorio");
            Console.WriteLine("---------------");
            Console.WriteLine();
            Console.WriteLine("1. Lista Personas");
            Console.WriteLine("2. Agrega Persona");
            Console.WriteLine("0. Salir");
            Console.WriteLine();
            Console.Write("Ingrese opción:");
            
            int opcion = int.Parse(Console.ReadLine());

            return opcion;
        }

        static void ListaPersonas()
        {
            SqliteConnection conexion = new SqliteConnection(@"Data Source=..\Directorio\Directorio.db");
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "SELECT id, nombre, apellido, telefono, id_unidad from persona";
            
            conexion.Open();
            
            // Lee datos de personas
            SqliteDataReader lector = comando.ExecuteReader();

            Console.WriteLine();
            Console.WriteLine("Personas");
            Console.WriteLine("--------");
            Console.WriteLine();
            Console.WriteLine("Id  Nombre          Apellido        Telefono        ID departamento");
            Console.WriteLine("--- --------------- --------------- --------------- ---------------");

            // Imprime datos de personas
            while(lector.Read())
            {
                Console.WriteLine("{0,3} {1,-15} {2,-15} {3,-15} {4,-15}"
                    ,lector.GetInt32(0)
                    ,lector.GetString(1)
                    ,lector.GetString(2)
                    ,lector.GetString(3)
                    ,lector.GetString(4));
            }

            conexion.Close();

            Console.WriteLine();
            Console.WriteLine("Presione Enter para continuar");
            Console.ReadLine();
        }

        static void AgregaPersona()
        {
            SqliteConnection conexion = new SqliteConnection(@"Data Source=..\Directorio\Directorio.db");
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "INSERT INTO persona(nombre, apellido, telefono, id_unidad) values($nombre,$apellido,$telefono,$id_unidad)";
            
            // Lee datos ingresados por el usuario
            Console.WriteLine();
            Console.Write("Ingrese Nombre:");
            string nombre = Console.ReadLine();

            Console.Write("Ingrese Apellido:");
            string apellido = Console.ReadLine();

            Console.Write("Ingrese Telefono:");
            string telefono = Console.ReadLine();

            Console.Write("Ingrese id de departamento:");
            string idUnidad = Console.ReadLine();

            comando.Parameters.Add(new SqliteParameter() {ParameterName = "$nombre", Value = nombre});
            comando.Parameters.Add(new SqliteParameter() {ParameterName = "$apellido", Value = apellido});
            comando.Parameters.Add(new SqliteParameter() {ParameterName = "$telefono", Value = telefono});
            comando.Parameters.Add(new SqliteParameter() {ParameterName = "$id_unidad", Value = idUnidad});

            conexion.Open();

            comando.ExecuteNonQuery();

            conexion.Close();

            Console.WriteLine();
            Console.WriteLine("Presione Enter para continuar");
            Console.ReadLine();

        }
    }
}
