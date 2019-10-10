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
        public ActionResult Index()//COMO PARAMETRO DE ENTRADA DEBE VENIR EL LINK CIFRADO
        {
            string respuestaCaso = string.Empty;            
            try
            {
                ////Obtener ultimo parametro de la url de la página que será el link

                ////Descifrar link

                ////Obtener informacion del idCase, numero de caso (y id Entidad de la coleccion en caso de ser corrección)

                //var idCase = 0;
                //// XML Búsqueda
                //string xmlGetEntities = @"
                //    <BizAgiWSParam>
                //        <EntityData>
                //            <EntityName>M_ProcesoCreacionDeProve</EntityName>
                //            <Filters>
                //                <![CDATA[idCase = " + idCase + @"]]>
                //            </Filters>
                //        </EntityData>
                //    </BizAgiWSParam>";

                ////Escribir log CSV
                //Util.EscribirLog("Consultar id de caso", "Index", xmlGetEntities);
                ////Fin CSV

                //// Abrir conexión a servicio web
                //var servicioQuery = "";//DesEntity.EntityManagerSOASoapClient servicioQuery = new DesEntity.EntityManagerSOASoapClient();

                //respuestaCaso = servicioQuery.getEntitiesAsString(xmlGetEntities);
                //respuestaCaso = respuestaCaso.Replace("\n", "");
                //respuestaCaso = respuestaCaso.Replace("\t", "");
                //respuestaCaso = respuestaCaso.Replace("\r", "");

                ////Escribir log CSV
                //Util.EscribirLog("respuestaCaso", "Index", respuestaCaso);
                ////Fin CSV

                ////Transformar respuesta STRING de Bizagi a XML para poder recorrer los nodos
                //XmlDocument doc = new XmlDocument();
                //doc.LoadXml(respuestaCaso);

                //string numeroCaso = "";
                //string idEntidadSolicitud = "";
                //var fechaGeneracionLink = "";//VERIFICAR ESTA FECHA Y LA OBTENCION EN EL XML
                //string duracionLink = "";
                //var fechaActual = DateTime.Today;
                //var resultadoFechas = "";

                ////Guardar "fecha generacion link", "duracion de link", idEntidadSolicitud, numeroCaso y declarar la fecha actual
                //if (doc.SelectSingleNode("/BizAgiWSResponse/Entities/M_ProcesoCreacionDeProve/NumerodeCaso").InnerText != null)
                //{
                //    numeroCaso = doc.SelectSingleNode("/BizAgiWSResponse/Entities/M_ProcesoCreacionDeProve/NumerodeCaso").InnerText;
                //    System.Web.HttpContext.Current.Session["NroCaso"] = numeroCaso;
                //}
                ////REVISAR SI PUEDO OBTENER ASI LA KEY O HACERLO DE OTRA MANERA
                //if (doc.SelectSingleNode("/BizAgiWSResponse/Entities/M_ProcesoCreacionDeProve/SolicitudCreaciondeProve").InnerText != null)
                //{
                //    idEntidadSolicitud = doc.SelectSingleNode("/BizAgiWSResponse/Entities/M_ProcesoCreacionDeProve/SolicitudCreaciondeProve").InnerText;
                //}
                //if (doc.SelectSingleNode("/BizAgiWSResponse/Entities/M_ProcesoCreacionDeProve/Fechageneracionlink").InnerText != null)
                //{
                //    fechaGeneracionLink = doc.SelectSingleNode("/BizAgiWSResponse/Entities/M_ProcesoCreacionDeProve/Fechageneracionlink").InnerText;
                //    Convert.ToDateTime(fechaGeneracionLink);
                //}
                //if (doc.SelectSingleNode("/BizAgiWSResponse/Entities/M_ProcesoCreacionDeProve/Diasduracionlink").InnerText != null)
                //{
                //    duracionLink = doc.SelectSingleNode("/BizAgiWSResponse/Entities/M_ProcesoCreacionDeProve/Diasduracionlink").InnerText;
                //}

                ////Restar la fecha actual con la fecha generacion de link y los dias que de resultado, comparar con duracion de link, asi determinar si es válido o no.
                //resultadoFechas = fechaActual - fechaGeneracionLink;

                ////Si es valido retornar la vista del Index(formulario). Si no es valido, retornar a página de expiracion




                //Cargar tabla de tipo de proveedor
                string listaTipoProveedor = Util.ListarParametrica("P_TipodeProveedor", "TipodeProveedor");
                ViewData["txtTipoProveedor"] = listaTipoProveedor;
            }
            catch(Exception ex)
            {
                return RedirectToAction("Caducado", "Home");
            }
            return View();
        }

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
