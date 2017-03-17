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
    public class BancoDA
    {
        string cadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings["CSPrueba"].ConnectionString;
    
        public void  FillParameters(SqlCommand pSqlCmd, BancoBE pObj){

            pSqlCmd.Parameters.Add(new SqlParameter("@NOMBRE", pObj.Nombre));
            pSqlCmd.Parameters.Add(new SqlParameter("@DIRECCION", pObj.Direccion));
            pSqlCmd.Parameters.Add(new SqlParameter("@FECHA_REGISTRO", pObj.FechaRegistro));
        
        }


        public BancoBE Insert(BancoBE pBancoBE)
        {
            

            using (SqlConnection sqlConnection = new SqlConnection(cadenaConexion))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(DbRecursos.uspBancoInsert, sqlConnection))
                {
                    pBancoBE.FechaRegistro = DateTime.Now;
                    pBancoBE.FechaRegistroString = DateTime.Now.ToShortDateString();
                    FillParameters(sqlCommand, pBancoBE);
                     pBancoBE.Id = (int)sqlCommand.ExecuteScalar();
                   // sqlCommand.ExecuteNonQuery();

                }
                sqlConnection.Close();

            }
            return pBancoBE;        
        }
        public void Delete(BancoBE pBancoBE)
        {

            using (SqlConnection sqlConnection = new SqlConnection(cadenaConexion))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(DbRecursos.uspBancoDelete, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@ID", pBancoBE.Id));
                    sqlCommand.ExecuteNonQuery();
                }

            }
        }
        public List<BancoBE> Get()
        {
            List<BancoBE> ltsResultado = new List<BancoBE>();
            using (SqlConnection sqlConnection = new SqlConnection(cadenaConexion))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(DbRecursos.uspBancoGet, sqlConnection))
                {
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            BancoBE oBancoBE = new BancoBE();
                            oBancoBE.Id = Convert.ToInt32(dataReader.GetValue(dataReader.GetOrdinal("ID")));
                            oBancoBE.Nombre = dataReader.GetValue(dataReader.GetOrdinal("NOMBRE")).ToString();
                            oBancoBE.Direccion = Convert.ToString(dataReader.GetValue(dataReader.GetOrdinal("DIRECCION")));
                            oBancoBE.FechaRegistro = Convert.ToDateTime(dataReader.GetValue(dataReader.GetOrdinal("FECHA_REGISTRO")));
                            oBancoBE.FechaRegistroString = Convert.ToDateTime(dataReader.GetValue(dataReader.GetOrdinal("FECHA_REGISTRO"))).ToShortDateString();
                            ltsResultado.Add(oBancoBE);
                        }
                    }
                }

            }


            return ltsResultado;
        }
        public BancoBE Update(BancoBE pBancoBE)
        {

            using (SqlConnection sqlConnection = new SqlConnection(cadenaConexion))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(DbRecursos.uspBancoUpdate, sqlConnection))
                {
                    FillParameters(sqlCommand, pBancoBE);
                    pBancoBE.Id = sqlCommand.ExecuteNonQuery();

                }

            }
            return pBancoBE;
        }
    }
}
