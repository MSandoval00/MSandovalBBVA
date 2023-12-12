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
        [HttpPost]
        public ActionResult ComprarProducto(int monedasIngresadas, int IdProducto, string NombreUsuario)
        {
            int IdMoneda = 1;
            ML.Result result = BL.Producto.GetById(IdProducto);
            ML.Producto producto = (ML.Producto)result.Object;
            if (producto.Precio<=monedasIngresadas)
            {
                int cambio = monedasIngresadas - producto.Precio;
                if (cambio > 0)
                {

                    ML.Result resultDenominaciones = BL.Cambio.GetById(IdMoneda);
                    ML.Cambio denominaciones = (ML.Cambio)resultDenominaciones.Object;
                    Denominaciones(cambio,denominaciones);
                    if (cambio==(denominaciones.Monedas10*10+denominaciones.Billetes50*50+denominaciones.Billetes100*100))
                    {
                        ML.Compra compra = new ML.Compra();
                        compra.Producto = new ML.Producto();
                        compra.Producto.IdProducto = IdProducto;
                        compra.Ingreso = monedasIngresadas;
                        compra.Cambio = cambio;
                        compra.Total=producto.Precio;
                        compra.Nombre = NombreUsuario;

                        ML.Result resultOperacion = BL.Compra.Add(compra);
                        if (resultOperacion.Correct)
                        {
                            ViewBag.Mensaje=$"Gracias por la compra: Tu cambio es:{cambio} \n Moneda de 10:{denominaciones.Monedas10}" +
                                $"\n Billetes de cincuenta: {denominaciones.Billetes50}" +
                                $"\n Billetes de cien: {denominaciones.Billetes100}";
                        }
                        else
                        {
                            ViewBag.Mensaje = resultOperacion.ErrorMessage;
                        }

                    }
                    else
                    {
                        ViewBag.Mensaje = "La maquina no cuenta con el suficiente cambio";
                    }
                }
                else
                {
                    ML.Compra compra = new ML.Compra();
                    compra.Producto = new ML.Producto();
                    compra.Producto.IdProducto = IdProducto;
                    compra.Ingreso= monedasIngresadas;
                    compra.Cambio= cambio;
                    ML.Result resultOperacion = BL.Compra.Add(compra);
                    if (resultOperacion.Correct)
                    {
                        ViewBag.Mensaje = "Gracias por la compra";
                    }
                    else
                    {
                        ViewBag.Mensaje = resultOperacion.ErrorMessage;
                    }
                }
            }
            else
            {
                ViewBag.Mensaje = "El dinero no es suficiente";
            }
            return PartialView("Modal");
        }
        public ML.Cambio Denominaciones(int cambioDevolver,ML.Cambio cambio)
        {
            Dictionary  <int, int> monedasDisponibles=new Dictionary<int, int>
            {
                { 100, cambio.Billetes100},
                { 50,cambio.Billetes50},
                { 10,cambio.Monedas10}
            };
            List<int> denominaciones = new List<int>(monedasDisponibles.Keys);
            Dictionary<int, int> resultado = new Dictionary<int, int>
            {
                { 100, 0 },
                { 50, 0 },
                { 10, 0}
            };
            foreach (var denominacion in denominaciones)
            {
               int Disponible=monedasDisponibles[denominacion];
                int cantidadMonedas = Math.Min(cambioDevolver / denominacion, Disponible);
                if (cantidadMonedas>0)
                {
                    resultado[denominacion] += cantidadMonedas;
                    cambioDevolver -= cantidadMonedas * denominacion;
                    monedasDisponibles[denominacion] -= cantidadMonedas;
                    if (cambioDevolver==0)
                    {
                        break;
                    }
                }
            }
            cambio.Billetes100 = resultado[100];
            cambio.Billetes50=resultado[50];
            cambio.Monedas10 = resultado[10];
            return cambio;
        }

    }
}