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
    public class EstadoDA
    {
        string cadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings["CSPrueba"].ConnectionString;

        public List<EstadoBE> Get()
        {
            List<EstadoBE> ltsResultado = new List<EstadoBE>();
            using (SqlConnection sqlConnection = new SqlConnection(cadenaConexion))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(DbRecursos.uspEstadoGet, sqlConnection))
                {
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            EstadoBE oEstadoBE = new EstadoBE();
                            oEstadoBE.Id = Convert.ToInt32(dataReader.GetValue(dataReader.GetOrdinal("ID")));
                            oEstadoBE.Nombre = dataReader.GetValue(dataReader.GetOrdinal("NOMBRE")).ToString();
                            oEstadoBE.Codigo = Convert.ToString(dataReader.GetValue(dataReader.GetOrdinal("CODIGO")));
                            ltsResultado.Add(oEstadoBE);
                        }
                    }
                }

            }
            return ltsResultado;
        }
    }
}
