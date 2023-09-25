using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace DataReader
{
    public class DataSetAp
    {
        /* Aqui utilizamos un DataSet para que realicemos una Actualizacion, Borrado*/
        private OracleConnection conexionOracle;
        private OracleDataAdapter adaptador = null;
        private DataSet ds = null;

        public void LeerDeBaseDeDatos()
        {
            // crear conexion a la base de datos 
            string strConexionOracle = "Aqui va la cadena de conexion de base de datos;";
            conexionOracle = new OracleConnection(strConexionOracle);
            // creamos una consulta 
            String consultaOracle = "SELECT TOP 10 * FROM NAME_TABLE WHERE ROWNUM <= 10;";

            // Creamos el adaptadors 
            adaptador = new OracleDataAdapter(consultaOracle, conexionOracle);
            // crear el conjunto de datos 
            ds = new DataSet("name_columns");
            // Recuperamos los datos del origen de datos mediante el SELECT 
            // del adaptador y mostrar los resultados

            adaptador.Fill(ds);
            // recorrer las filas  de la tabla
            if (ds.Tables.Count == 0) return;  // -> Verifica mas esta linea
            for(int i = 0; i < ds.Tables[0].Rows.Count; ++i)
            {
                Console.WriteLine($"{ds.Tables[0].Rows[i]["name_columns"].ToString()}" +
                    $"{ds.Tables[0].Rows[i]["nombre_columns"].ToString()}");
            }
        }

        public void CerrraConexion()
        {
            if (conexionOracle != null) conexionOracle.Close();
        }

        /* Debes de crearte una clase independiente para que llame a cada una de las opciones */

        public void CerrarConexion()
        {
            try
            {
                //leer base de datos
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                // cerramos la conexion()
            }
        }
    }   
}
