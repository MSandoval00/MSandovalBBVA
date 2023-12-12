using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Compra
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context=new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    var query = "ComprasGetAll";
                    SqlCommand cmd=new SqlCommand(query,context);
                    cmd.CommandType=CommandType.StoredProcedure;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable tablaCompra=new DataTable();
                    adapter.Fill(tablaCompra);
                    if (tablaCompra.Rows.Count>0)
                    {
                        result.Objects = new List<object>();
                        foreach (DataRow row in tablaCompra.Rows)
                        {
                            ML.Compra compra = new ML.Compra();
                            compra.Producto=new ML.Producto();
                            compra.IdCompra = int.Parse(row[0].ToString());
                            compra.Producto.IdProducto=int.Parse(row[1].ToString());
                            compra.Producto.Nombre=row[2].ToString();
                            compra.Producto.Precio=int.Parse(row[3].ToString());
                            compra.Total=int.Parse(row[4].ToString());
                            compra.Ingreso=int.Parse(row[5].ToString());
                            compra.Cambio=int.Parse(row[6].ToString());
                            result.Objects.Add(compra);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct=false;
                        result.ErrorMessage = "Error no se pudo obtener las compras";
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
        public static ML.Result GetById(int IdCompra)
        {
            ML.Result result=new ML.Result();
            try
            {
                using (SqlConnection context=new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    var query = "ComprasGetById";
                    SqlCommand cmd = new SqlCommand(query, context);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adapter=new SqlDataAdapter(cmd);
                    DataTable tablaCompra = new DataTable();

                    SqlParameter[] collection = new SqlParameter[1];
                    collection[0] = new SqlParameter("@IdCompre", SqlDbType.Int);
                    collection[0].Value = IdCompra;
                    cmd.Parameters.AddRange(collection);
                    adapter.Fill(tablaCompra);
                    if (tablaCompra.Rows.Count>0)
                    {
                        result.Object=tablaCompra.Rows[0];

                        DataRow row = tablaCompra.Rows[0];
                        ML.Compra compra = new ML.Compra();
                        compra.Producto = new ML.Producto();
                        compra.IdCompra = int.Parse(row[0].ToString());
                        compra.Producto.IdProducto = int.Parse(row[1].ToString());
                        compra.Producto.Nombre = row[2].ToString();
                        compra.Producto.Precio = int.Parse(row[3].ToString());
                        compra.Total = int.Parse(row[4].ToString());
                        compra.Ingreso = int.Parse(row[5].ToString());
                        compra.Cambio = int.Parse(row[6].ToString());
                        result.Object=compra;
                        result.Correct = true;

                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage=ex.Message;
                result.Ex=ex;
            }
            return result;
        }
        public static ML.Result Add(ML.Compra compra)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    var query = "CompraAdd";
                    SqlCommand cmd=new SqlCommand(query,context);
                    SqlParameter[] collection = new SqlParameter[5];
                    cmd.CommandType = CommandType.StoredProcedure;
                    compra.Producto = new ML.Producto();

                    collection[0] = new SqlParameter("@IdCompra", SqlDbType.Int);
                    collection[0].Value = compra.IdCompra;
                    collection[1]=new SqlParameter("@IdProducto",SqlDbType.Int);
                    collection[1].Value = compra.Producto.IdProducto;
                    collection[2] = new SqlParameter("@Total", SqlDbType.Int);
                    collection[2].Value = compra.Total;
                    collection[3] = new SqlParameter("@Ingreso", SqlDbType.Int);
                    collection[3].Value = compra.Ingreso;
                    collection[4] = new SqlParameter("@Cambio", SqlDbType.Int);
                    collection[4].Value = compra.Cambio;
                    cmd.Parameters.AddRange(collection);
                    cmd.Connection.Open();
                    int filas=cmd.ExecuteNonQuery();
                    if (filas > 0)
                    {
                        result.Correct=true;
                    }
                    else
                    {
                        result.Correct=false;
                        result.ErrorMessage = "Error al agregar el registro";
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
