using Npgsql;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data.SqlClient;

namespace DataReader
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            /* Falta crear y hacer ka referencia al conector de base de datos  */
            // creamos un objeto que tendra todos los 
            BaseDeDatos bd = new BaseDeDatos();
            try
            {
                bd.LeerBaseDeDatos();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                // Ocurra lo que ocurra, siempre debemos de cerrar la conexion, 
                // buenas practicas
                bd.CerrarConexion();
            }

        }

                





        public class BaseDeDatos
        {
            /* Crea variables privadas */
            /**
             * Puedes utilizar comandos para Oracle en sulugar estamos utilizando la aplicacion para 
             * conectarnos a una base de datos de MYSQL             
             */
            private OracleConnection ConexionDBOracle;
            private NpgsqlConnection ConexionConPostgrest;
            private SqlConnection ConexionSqlServer;
            
            /* Variables para realizar una consulta en diferente bases de datos */
            private OracleCommand OrdenSQLOracle;
            private NpgsqlCommand OrdenSQLPostgre;
            private SqlCommand OrdenSQLServer;

            /* DataReader la utilizamos para solo lectura */
            private OracleDataReader LectorOracle;
            private NpgsqlDataReader LectorPostgre;
            private SqlDataReader LectorSqlServer;
            public void LeerBaseDeDatos()
            {
                // Crear objeto de conexion

                string strConexionPostgres = "SERVER=localhost;Database=CURSO;User name=postgrest;Password=qwertyuiop";
                string strConexionOracle = "tiene otra arquitectura";
                string strConexionSQLServer = "tiene la misma estructura que la conexion a la base de datos de Postgrest";

                ConexionConPostgrest = new NpgsqlConnection(strConexionPostgres);
                ConexionDBOracle = new OracleConnection(strConexionOracle);
                ConexionSqlServer = new SqlConnection(strConexionSQLServer);

                // Crear una accion en SQL, un SELECT 
                string consultaPostgrest = "SELECT TOP 10 * FROM NAME_TABLE;";
                OrdenSQLPostgre = new NpgsqlCommand(consultaPostgrest, ConexionConPostgrest);
                string consultaOracle = "SELECT * FROM NAME_TABLE WHERE ROWNUM <= 10;";
                OrdenSQLOracle = new OracleCommand(consultaOracle, ConexionDBOracle);
                string consultaSQLServer = "SELECT TOP 10 * FROM NAME_TABLE;";
                OrdenSQLServer = new SqlCommand(consultaSQLServer, ConexionSqlServer);

                /* Abrimos la conexion de la base de datos, vamos por pasos luego
                 * lo manejamos con las excepciones correspondientes 
                 */
                ConexionConPostgrest.Open();
                // ExecuteReader hace la consulta y devuelve un DataReader
                LectorPostgre = OrdenSQLPostgre.ExecuteReader();
                //Llamar a Read antes de acceder a los registros 
                while (LectorPostgre.Read())  // -> siguiente registro
                {
                    Console.WriteLine(LectorPostgre.GetString(0) + "," +
                        LectorPostgre.GetString(1));
                }
                //Llamar siempre a Close una vez finalizada la lectura 
                LectorPostgre.Close();
                LectorPostgre = null;
            }
            //private SqlCommand ordenSQL = new SqlCommand("SELECT TOP 10 * FROM usuarios", conexionConBD);
            public void CerrarConexion()
            {
                // Cerrar la conexion cuando ya no sea necesaria 
                if (LectorPostgre != null) LectorPostgre.Close();
                if (ConexionConPostgrest != null) ConexionConPostgrest.Close();
            }
        }
    }
}
