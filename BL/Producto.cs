using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Producto
    {
       public static ML.Result GetAll()
        {
            ML.Result result=new ML.Result();
            try
            {
                using (SqlConnection context=new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    var query = "ProductoGetAll";
                    SqlCommand cmd=new SqlCommand(query,context);
                    cmd.CommandType=CommandType.StoredProcedure;

                    SqlDataAdapter adapter=new SqlDataAdapter(cmd);
                    DataTable tableProducto = new DataTable();
                    adapter.Fill(tableProducto);
                    if (tableProducto.Rows.Count>0)
                    {
                        result.Objects = new List<object>();
                        foreach (DataRow row in tableProducto.Rows)
                        {
                            ML.Producto producto = new ML.Producto();
                            producto.IdProducto = int.Parse(row[0].ToString());
                            producto.Nombre= row[1].ToString();
                            producto.Precio = int.Parse(row[2].ToString());
                            producto.TipoProducto=char.Parse(row[3].ToString());

                            result.Objects.Add(producto);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay registros de productos";
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
        public static ML.Result GetById(int IdProducto)
        {
            ML.Result result=new ML.Result();
            try
            {
                using (SqlConnection context=new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    var query = "ProductoGetById";
                    SqlCommand cmd=new SqlCommand(query,context);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable tablaProducto=new DataTable();

                    SqlParameter[] collection = new SqlParameter[1];
                    collection[0] = new SqlParameter("@IdProducto", SqlDbType.Int);
                    collection[0].Value = IdProducto;
                    cmd.Parameters.AddRange(collection);
                    adapter.Fill(tablaProducto);
                    if (tablaProducto.Rows.Count>0)
                    {
                        DataRow row = tablaProducto.Rows[0];
                        ML.Producto producto = new ML.Producto();
                        producto.IdProducto = int.Parse(row[0].ToString());
                        producto.Nombre = row[1].ToString();
                        producto.Precio=int.Parse(row[2].ToString());
                        producto.TipoProducto=char.Parse(row[3].ToString());

                        result.Object=producto;
                        result.Correct=true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage= ex.Message;
                result.Ex=ex;
            }
            return result;
        }
    }
}
