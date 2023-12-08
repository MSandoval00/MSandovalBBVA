using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Producto producto=new ML.Producto();
            producto.Productos = new List<object>();

            ML.Result result= BL.Producto.GetAll();
            if (result.Correct)
            {
                producto.Productos =result.Objects;
            }
            return View(producto);
        }
        [HttpGet]
        public ActionResult ComprarProducto(int? IdProducto)
        {
            ML.Producto producto = new ML.Producto();
            if (IdProducto!=null)
            {
                ML.Result result = BL.Producto.GetById(IdProducto.Value);
                if (result.Correct)
                {
                    producto = (ML.Producto)result.Object;
                }
            }
            
            return View(producto);
        }
    }
}