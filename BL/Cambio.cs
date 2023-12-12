using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Cambio
    {
        public static ML.Result GetById(int IdMaquina)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context=new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    var query = "MonedaGetById";
                    SqlCommand cmd=new SqlCommand(query,context);
                    cmd.CommandType=CommandType.StoredProcedure;
                    
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable tablaMoneda=new DataTable();
                    SqlParameter[] collection = new SqlParameter[1];
                    collection[0] = new SqlParameter("@IdCambio", SqlDbType.Int);
                    collection[0].Value = IdMaquina;
                    cmd.Parameters.AddRange(collection);
                    adapter.Fill(tablaMoneda);

                    if (tablaMoneda.Rows.Count>0)
                    {
                        result.Object = tablaMoneda.Rows[0];
                        DataRow row = tablaMoneda.Rows[0];
                        
                            ML.Cambio cambio = new ML.Cambio();
                            cambio.IdCambio = int.Parse(row[0].ToString());
                            cambio.Billetes100=int.Parse(row[1].ToString());
                            cambio.Billetes50=int.Parse(row[2].ToString());
                            cambio.Monedas10=int.Parse(row[3].ToString());
                            result.Object=cambio;
                            result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay monedas registradas";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex=ex;
            }
            return result;  
        }
    }
}
