using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.DirectoryServices;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Xml;

namespace WebSolicitudes.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            string respuestaCaso = string.Empty;
            //string datosJSON = string.Empty;
            try
            {
                ////Obtener ultimo parametro de la url de la página que será el link
                string hola = "https://10.2.0.230/CreacionProveedor/PMRE45LNMVZG6ZDFINQXG3ZCHIRCANJTEARCYITJMRBWC43FEI5CEIBVGMQCELBCMR2XEYLDNFXW4TDJNZVSEORCEAYTAIBCFQRGMZLDNBQUOZLOMVZGCY3JN5XCEORCEBKHKZJAJ5RXIIBRGUQDAMB2GAYDUMBQEBKVIQZNGQQDEMBRHEQCELBCNFSFG33MNFRWS5DVMQRDUIRAGUZSAIT5";
                //string[] url = Request.Url.Query.Split('/');
                string[] url = hola.Split('/');
                string linkEncriptado = url.Last();
                ////Descifrar link

                Base32.Net4.Encoder enco = new Base32.Net4.Encoder();
                string decodificado = enco.DecodeFromBase32String(linkEncriptado);
                //datosJSON = JsonConvert.SerializeObject(decodificado);
                var datosJSON = JObject.Parse(decodificado);
                var numeroCaso = datosJSON["NumerodeCaso"].ToString();
                if(numeroCaso != null && numeroCaso != "")
                {
                    numeroCaso = numeroCaso.Trim();

                    System.Web.HttpContext.Current.Session["NroCaso"] = numeroCaso;

                    //Escribir log CSV
                    UtilController.EscribirLog("Numero de caso obtenido", "Index", numeroCaso);
                    //Fin CSV

                    ////Obtener informacion del idCase, numero de caso (y id Entidad de la coleccion en caso de ser corrección)

                    // XML Búsqueda
                    string xmlGetEntities = @"
                    <BizAgiWSParam>
                        <EntityData>
                            <EntityName>M_ProcesoCreacionDeProve</EntityName>
                            <Filters>
                                <![CDATA[NumerodeCaso = '" + numeroCaso + @"']]>
                            </Filters>
                        </EntityData>
                    </BizAgiWSParam>";

                    //Escribir log CSV
                    UtilController.EscribirLog("Consultar numero de caso", "Index", xmlGetEntities);
                    //Fin CSV

                    // Abrir conexión a servicio web
                    EntityManager.EntityManagerSOASoapClient servicioQuery = new EntityManager.EntityManagerSOASoapClient();

                    respuestaCaso = servicioQuery.getEntitiesAsString(xmlGetEntities);
                    respuestaCaso = respuestaCaso.Replace("\n", "");
                    respuestaCaso = respuestaCaso.Replace("\t", "");
                    respuestaCaso = respuestaCaso.Replace("\r", "");

                    //Escribir log CSV
                    UtilController.EscribirLog("respuestaCaso", "Index", respuestaCaso);
                    //Fin CSV

                    //Transformar respuesta STRING de Bizagi a XML para poder recorrer los nodos
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(respuestaCaso);

                    var idEntidadSolicitud = "";
                    var fechaGeneracionLink = "";
                    var duracionLink = "";
                    DateTime fechaActual = DateTime.Today;
                    string completadoWeb = "";
                    string idioma = "";

                    //Guardar "fecha generacion link", "duracion de link", idEntidadSolicitud, numeroCaso y declarar la fecha actual
                    if (doc.SelectSingleNode("/BizAgiWSResponse/Entities/M_ProcesoCreacionDeProve/Fechageneracionlink").InnerText != null)
                    {
                        fechaGeneracionLink = doc.SelectSingleNode("/BizAgiWSResponse/Entities/M_ProcesoCreacionDeProve/Fechageneracionlink").InnerText;
                    }
                    if (doc.SelectSingleNode("/BizAgiWSResponse/Entities/M_ProcesoCreacionDeProve/Diasduracionlink").InnerText != null)
                    {
                        duracionLink = doc.SelectSingleNode("/BizAgiWSResponse/Entities/M_ProcesoCreacionDeProve/Diasduracionlink").InnerText;                        
                    }
                    if (doc.SelectSingleNode("/BizAgiWSResponse/Entities/M_ProcesoCreacionDeProve/CompletadoWeb").InnerText != null)
                    {
                        completadoWeb = doc.SelectSingleNode("/BizAgiWSResponse/Entities/M_ProcesoCreacionDeProve/CompletadoWeb").InnerText;
                    }
                    if (doc.SelectSingleNode("/BizAgiWSResponse/Entities/M_ProcesoCreacionDeProve/SolicitudCreaciondeProve.Idioma.Idioma").InnerText != null)
                    {
                        idioma = doc.SelectSingleNode("/BizAgiWSResponse/Entities/M_ProcesoCreacionDeProve/SolicitudCreaciondeProve.Idioma.Idioma").InnerText;
                    }
                    foreach (XmlNode item in doc.SelectNodes("/BizAgiWSResponse/Entities/M_ProcesoCreacionDeProve/SolicitudCreaciondeProve"))
                    {
                        // Obtener campos
                        idEntidadSolicitud = item.Attributes["key"].Value;
                        Convert.ToInt32(idEntidadSolicitud);
                    }

                    //Restar la fecha actual con la fecha generacion de link y los dias que de resultado, comparar con duracion de link, asi determinar si es válido o no.
                    TimeSpan resultadoFechas = fechaActual.Subtract(Convert.ToDateTime(fechaGeneracionLink));
                    int diasDiferencia = resultadoFechas.Days;
                    if (completadoWeb != "True")
                    {
                        //Paginas español. CREAR CONDICION EVALUANDO CAMPO DE IDIOMA ELEGIDO
                        if (idioma == "Español")
                        {
                            if (Convert.ToInt32(duracionLink) >= diasDiferencia)
                            {
                                //mostrar index
                                System.Web.HttpContext.Current.Session["UrlValida"] = "1";
                                ViewData["UrlValida"] = System.Web.HttpContext.Current.Session["UrlValida"];
                            }
                            else
                            {
                                throw new Exception("Página caducada");
                            }
                        }
                        else if(idioma == "Ingles")
                        {
                            if (Convert.ToInt32(duracionLink) >= diasDiferencia)
                            {
                                //mostrar index
                                System.Web.HttpContext.Current.Session["UrlValida"] = "1";
                                ViewData["UrlValida"] = System.Web.HttpContext.Current.Session["UrlValida"];
                            }
                            else
                            {
                                throw new Exception("Página caducada");
                            }
                        }
                        //Paginas ingles
                    }
                    else
                    {
                        throw new Exception("Página ya fue completada. Se redirecciona a página caducada");
                    }
                    
                    //Cargar tabla de tipo de proveedor
                    string listaTipoProveedor = UtilController.ListarParametrica("P_Moneda", "Moneda");
                    ViewData["txtMoneda"] = listaTipoProveedor;
                }
            }
            catch(Exception ex)
            {
                return RedirectToAction("Caducado", "Home");
            }
            //ViewData["UrlValida"] = "1";
            return View();
        }

        //public ActionResult UrlValida(string value)
        //{
        //    System.Web.HttpContext.Current.Session["UrlValida"] = value;
        //    ViewData["UrlValida"] = "0";
        //    return this.Json(new { estado = '0' });
        //}

        public ActionResult Caducado()
        {
            return View();
        }

        public ActionResult SolicitudExitosa()
        {
            return View();
        }

        public ActionResult IndexEnglish()
        {
            return View();
        }

        //
        // GET: /Home/Login

        public ActionResult Login()
        {
            return View();
        }

        // GET: /Home/CerrarSesion

        public ActionResult CerrarSesion()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Home");
        }

        //
        // GET: /Home/Error404

        public ActionResult Error404()
        {
            return View();
        }

    }
}
