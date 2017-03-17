using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntity;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace DataAccess
{
    public class SucursalDA
    {

        string cadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings["CSPrueba"].ConnectionString;

        public void FillParameters(SqlCommand pSqlCmd, SucursalBE pObj)
        {

            pSqlCmd.Parameters.Add(new SqlParameter("@NOMBRE", pObj.Nombre));
            pSqlCmd.Parameters.Add(new SqlParameter("@BANCO", pObj.Banco.Id));
            pSqlCmd.Parameters.Add(new SqlParameter("@DIRECCION", pObj.Direccion));
            pSqlCmd.Parameters.Add(new SqlParameter("@FECHA_REGISTRO", pObj.FechaRegistro));

        }


        public SucursalBE Insert(SucursalBE pSucursalBE)
        {

            using (SqlConnection sqlConnection = new SqlConnection(cadenaConexion))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(DbRecursos.uspSucursalInsert, sqlConnection))
                {
                    FillParameters(sqlCommand, pSucursalBE);
                    pSucursalBE.FechaRegistro = DateTime.Now;
                    pSucursalBE.FechaRegistroString = DateTime.Now.ToShortDateString();
                    pSucursalBE.Id = (int)sqlCommand.ExecuteScalar();

                }

            }
            return pSucursalBE;
        }
        public void Delete(SucursalBE pSucursalBE)
        {

            using (SqlConnection sqlConnection = new SqlConnection(cadenaConexion))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(DbRecursos.uspSucursalDelete, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@ID", pSucursalBE.Id));
                    sqlCommand.ExecuteNonQuery();
                }

            }
        }
        public List<SucursalBE> Get(SucursalBE.Criterio pCriterio)
        {
            List<SucursalBE> ltsResultado = new List<SucursalBE>();
            using (SqlConnection sqlConnection = new SqlConnection(cadenaConexion))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    StringBuilder strb = new StringBuilder();
                    strb.Append(DbRecursos.uspSucursalGet);

                    if (pCriterio != null)
                    {
                        strb.Append(" WHERE ");
                        if (!string.IsNullOrEmpty(pCriterio.BancoNombre))
                        {
                            strb.Append("BANCO.NOMBRE LIKE @BANCO_NOMBRE");
                            sqlCommand.Parameters.Add(new SqlParameter("@BANCO_NOMBRE", pCriterio.BancoNombre));
                        }

                        if (pCriterio.BancoId!=0)
                        {
                            strb.Append("BANCO.ID LIKE @BANCO_ID");
                            sqlCommand.Parameters.Add(new SqlParameter("@BANCO_ID", pCriterio.BancoId));
                        }

                    }
                    sqlCommand.CommandText = strb.ToString();



                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            SucursalBE oSucursalBE = new SucursalBE();
                            oSucursalBE.Id = Convert.ToInt32(dataReader.GetValue(dataReader.GetOrdinal("ID")));
                            BancoBE oBancoBE = new BancoBE();
                            oBancoBE.Id = Convert.ToInt32(dataReader.GetValue(dataReader.GetOrdinal("BANCO")));
                            oBancoBE.Nombre = dataReader.GetValue(dataReader.GetOrdinal("BANCO_NOMBRE")).ToString();
                            oBancoBE.Direccion = dataReader.GetValue(dataReader.GetOrdinal("DIRECCION")).ToString();                            
                            oBancoBE.FechaRegistro = Convert.ToDateTime(dataReader.GetValue(dataReader.GetOrdinal("FECHA_REGISTRO")));
                            oSucursalBE.Banco = oBancoBE;
                            oSucursalBE.Nombre = dataReader.GetValue(dataReader.GetOrdinal("NOMBRE")).ToString();
                            oSucursalBE.FechaRegistroString = Convert.ToDateTime(dataReader.GetValue(dataReader.GetOrdinal("FECHA_REGISTRO"))).ToShortDateString();
                            oSucursalBE.Direccion = Convert.ToString(dataReader.GetValue(dataReader.GetOrdinal("DIRECCION")));
                            oSucursalBE.FechaRegistro = Convert.ToDateTime(dataReader.GetValue(dataReader.GetOrdinal("FECHA_REGISTRO")));
                            oSucursalBE.FechaRegistroString = oSucursalBE.FechaRegistro.ToShortDateString();
                            oSucursalBE.NombreCombo = oBancoBE.Nombre + " | " + oSucursalBE.Nombre;
                            ltsResultado.Add(oSucursalBE);
                        }
                    }
                }

            }


            return ltsResultado;
        }
        public SucursalBE Update(SucursalBE pSucursalBE)
        {

            using (SqlConnection sqlConnection = new SqlConnection(cadenaConexion))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(DbRecursos.uspSucursalUpdate, sqlConnection))
                {
                    FillParameters(sqlCommand, pSucursalBE);
                    pSucursalBE.Id = sqlCommand.ExecuteNonQuery();

                }

            }
            return pSucursalBE;
        }
    }
}
