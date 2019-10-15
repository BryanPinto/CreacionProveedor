using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace WebSolicitudes.Controllers
{
    public class UtilController : Controller
    {
        //
        // GET: /Util/

        /// <summary>
        /// Método para crear listas desplegables desde una paramétrica de Bizagi
        /// </summary>
        /// <param name="tabla">Nombre de tabla paramétrica</param>
        /// <param name="campoVisual">Campo que se va a mostrar como opción</param>
        /// <returns></returns>
        public static string ListarParametrica(string tabla, string campoVisual)
        {
            string lista = string.Empty;

            try
            {
                // XML Búsqueda
                string xmlGetEntities = @"
                    <BizAgiWSParam>
                        <EntityData>
                            <EntityName>" + tabla + @"</EntityName>
                            <Filters>
                                <![CDATA[dsbl" + tabla + @" = " + false + @"]]>
                            </Filters>
                        </EntityData>
                    </BizAgiWSParam>
                ";
                //Escribir log CSV
                EscribirLog("Listar motivo/submotivo", "ListarParametrica", xmlGetEntities);
                //Fin CSV

                // Abrir conexión a servicio web
                EntityManager.EntityManagerSOASoapClient servicioQuery = new EntityManager.EntityManagerSOASoapClient();

                // Buscar en Bizagi
                string respuesta = servicioQuery.getEntitiesAsString(xmlGetEntities);

                //Escribir log CSV
                EscribirLog("Respuesta", "ListarParametrica", respuesta);
                //Fin CSV

                // Convertir a XML
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(respuesta);

                // Recorrer los resultados
                foreach (XmlNode item in doc.SelectNodes("/BizAgiWSResponse/Entities/" + tabla))
                {
                    // Obtener campos
                    string id = item.Attributes["key"].Value;
                    string campo = item.SelectSingleNode(campoVisual).InnerText;

                    // Crear opción
                    lista += "<option value='" + id + @"'>" + campo + @"</option>";
                }

                //Escribir log CSV
                EscribirLog("Lista opciones generada", "ListarParametrica", lista);
                //Fin CSV

            }
            catch (Exception ex)
            {
                //Escribir log CSV
                EscribirLog("ERROR", "ListarParametrica", ex.Message);
                //Fin CSV
            }

            return (lista);
        }

        /// <summary>
        /// Método para escribir un log
        /// Se guardar dentro de un CSV con nombre LogPortal.CSV dentro de la carpeta del proyecto
        /// </summary>
        /// <param name="proceso">Nombre del proceso</param>
        /// <param name="metodo">Método actual</param>
        /// <param name="mensaje">Mensaje que se va a guardar</param>
        public static void EscribirLog(string proceso, string metodo, string mensaje)
        {
            try
            {
                // Crear CSV
                string rutaLog = HttpRuntime.AppDomainAppPath;
                //string nombreArchivo = "LogPortal.csv";
                string rutaCompleta = rutaLog + "LogPortal.csv";
                var csv = new StringBuilder();

                // Revisar si tiene cabecera
                string primeraLinea = string.Empty;
                bool existeArchivo = System.IO.File.Exists(rutaCompleta);
                if (existeArchivo)
                    primeraLinea = System.IO.File.ReadLines(rutaCompleta).FirstOrDefault();
                //Si cabecera no existe, crear con las siguientes columnas
                if (!existeArchivo || (existeArchivo && (primeraLinea == null || primeraLinea == string.Empty)))
                {
                    string cabecera =
                        string.Format("{0};{1};{2};{3};{4}"
                        , "Fecha"
                        , "Hora"
                        , "Proceso"
                        , "Metodo"
                        , "Mensaje"
                        );
                    csv.Append(cabecera);
                    System.IO.File.AppendAllText(rutaCompleta, csv.ToString());
                    csv.Clear();
                }

                // Si existe cabecera, escribir linea
                string nuevaLinea = Environment.NewLine +
                    string.Format("{0};{1};{2};{3};{4}"
                    , "\"" + DateTime.Now.ToShortDateString() + "\""
                    , "\"" + DateTime.Now.ToShortTimeString() + "\""
                    , "\"" + proceso + "\""
                    , "\"" + metodo + "\""
                    , "\"" + mensaje + "\""
                    );

                // Agregar a archivo
                csv.Append(nuevaLinea);
                System.IO.File.AppendAllText(rutaCompleta, csv.ToString());
                csv.Clear();
            }
            catch (Exception)
            {
            }
        }

        public ActionResult Index()
        {
            return View();
        }

    }
}
