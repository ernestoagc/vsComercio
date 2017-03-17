using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using BusinessEntity;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace DataAccess
{
    public class MonedaDA
    {
        string cadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings["CSPrueba"].ConnectionString;


        public List<MonedaBE> Get()
        {
            List<MonedaBE> ltsResultado = new List<MonedaBE>();
            using (SqlConnection sqlConnection = new SqlConnection(cadenaConexion))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(DbRecursos.uspMonedaGet, sqlConnection))
                {
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            MonedaBE oMonedaBE = new MonedaBE();
                            oMonedaBE.Id = Convert.ToInt32(dataReader.GetValue(dataReader.GetOrdinal("ID")));
                            oMonedaBE.Nombre = dataReader.GetValue(dataReader.GetOrdinal("NOMBRE")).ToString();
                            oMonedaBE.Codigo = Convert.ToString(dataReader.GetValue(dataReader.GetOrdinal("CODIGO")));
                            ltsResultado.Add(oMonedaBE);
                        }
                    }
                }

            }
            return ltsResultado;
        }
    }
}
