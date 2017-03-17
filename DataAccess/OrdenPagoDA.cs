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
    public class OrdenPagoDA
    {

        string cadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings["CSPrueba"].ConnectionString;

        public void FillParameters(SqlCommand pSqlCmd, OrdenPagoBE pObj)
        {

            pSqlCmd.Parameters.Add(new SqlParameter("@MONTO", pObj.Monto));
            pSqlCmd.Parameters.Add(new SqlParameter("@SUCURSAL", pObj.Sucursal.Id));
            pSqlCmd.Parameters.Add(new SqlParameter("@MONEDA", pObj.Moneda.Id));
            pSqlCmd.Parameters.Add(new SqlParameter("@ESTADO", pObj.Estado.Id));
            pSqlCmd.Parameters.Add(new SqlParameter("@FECHA_PAGO", pObj.FechaPago));

        }


        public OrdenPagoBE Insert(OrdenPagoBE pOrdenPagoBE)
        {

            using (SqlConnection sqlConnection = new SqlConnection(cadenaConexion))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(DbRecursos.uspOrdenPagoInsert, sqlConnection))
                {

                    FillParameters(sqlCommand, pOrdenPagoBE);
                    pOrdenPagoBE.Id = (int)sqlCommand.ExecuteScalar();
                }

            }
            return pOrdenPagoBE;
        }
        public void Delete(OrdenPagoBE pOrdenPagoBE)
        {

            using (SqlConnection sqlConnection = new SqlConnection(cadenaConexion))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(DbRecursos.uspOrdenPagoDelete, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@ID", pOrdenPagoBE.Id));
                    sqlCommand.ExecuteNonQuery();
                }

            }
        }
        public List<OrdenPagoBE> Get(OrdenPagoBE.Criterio pCriterio)
        {
            List<OrdenPagoBE> ltsResultado = new List<OrdenPagoBE>();
            using (SqlConnection sqlConnection = new SqlConnection(cadenaConexion))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;


                    StringBuilder strb = new StringBuilder();
                    strb.Append(DbRecursos.uspOrdenPagoGet);

                    if (pCriterio != null)
                    {
                        strb.Append(" WHERE ");
                        if (!string.IsNullOrEmpty(pCriterio.CodigoMoneda))
                        {
                            strb.Append("MONEDA.CODIGO=@MONEDA_CODIGO");
                            sqlCommand.Parameters.Add(new SqlParameter("@MONEDA_CODIGO", pCriterio.CodigoMoneda));
                        }

                        if (pCriterio.SucursalId != 0)
                        {
                            strb.Append("ORDEN_PAGO.SUCURSAL = @SUCURSAL");
                            sqlCommand.Parameters.Add(new SqlParameter("@SUCURSAL", pCriterio.SucursalId));
                        }
                           
                    }
                    sqlCommand.CommandText = strb.ToString();


                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            OrdenPagoBE oOrdenPagoBE = new OrdenPagoBE();
                            oOrdenPagoBE.Id = Convert.ToInt32(dataReader.GetValue(dataReader.GetOrdinal("ID")));
                            MonedaBE oMonedaBE = new MonedaBE();
                            EstadoBE oEstadoBE = new EstadoBE();
                            SucursalBE oSucursalBE = new SucursalBE();
                            BancoBE oBancoBE = new BancoBE();
                            
                            oMonedaBE.Id= Convert.ToInt32(dataReader.GetValue(dataReader.GetOrdinal("MONEDA")));
                            oMonedaBE.Nombre= dataReader.GetValue(dataReader.GetOrdinal("MONEDA_NOMBRE")).ToString(); 
                            oMonedaBE.Codigo= dataReader.GetValue(dataReader.GetOrdinal("MONEDA_CODIGO")).ToString(); 

                            oEstadoBE.Id= Convert.ToInt32(dataReader.GetValue(dataReader.GetOrdinal("ESTADO")));
                            oEstadoBE.Nombre= dataReader.GetValue(dataReader.GetOrdinal("ESTADO_NOMBRE")).ToString(); 
                            oEstadoBE.Codigo= dataReader.GetValue(dataReader.GetOrdinal("ESTADO_CODIGO")).ToString(); 

                            oSucursalBE.Id= Convert.ToInt32(dataReader.GetValue(dataReader.GetOrdinal("SUCURSAL")));
                            oSucursalBE.Nombre= dataReader.GetValue(dataReader.GetOrdinal("SUCURSAL_NOMBRE")).ToString();

                            oBancoBE.Id = Convert.ToInt32(dataReader.GetValue(dataReader.GetOrdinal("BANCO_ID")));
                            oBancoBE.Nombre = Convert.ToString(dataReader.GetValue(dataReader.GetOrdinal("BANCO_NOMBRE")));

                            oOrdenPagoBE.Moneda = oMonedaBE;
                            oOrdenPagoBE.Sucursal = oSucursalBE;
                            oOrdenPagoBE.Estado = oEstadoBE;
                            oSucursalBE.Banco = oBancoBE;

                            oOrdenPagoBE.Monto = Convert.ToDecimal(dataReader.GetValue(dataReader.GetOrdinal("MONTO")));
                            oOrdenPagoBE.FechaPago = Convert.ToDateTime(dataReader.GetValue(dataReader.GetOrdinal("FECHA_PAGO")));
                            ltsResultado.Add(oOrdenPagoBE);
                        }
                    }
                }

            }


            return ltsResultado;
        }


        public OrdenPagoBE Update(OrdenPagoBE pOrdenPagoBE)
        {

            using (SqlConnection sqlConnection = new SqlConnection(cadenaConexion))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(DbRecursos.uspOrdenPagoUpdate, sqlConnection))
                {
                    FillParameters(sqlCommand, pOrdenPagoBE);
                    pOrdenPagoBE.Id = sqlCommand.ExecuteNonQuery();

                }

            }
            return pOrdenPagoBE;
        }
    }
}
