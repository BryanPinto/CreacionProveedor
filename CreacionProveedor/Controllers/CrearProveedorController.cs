using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace WebSolicitudes.Controllers
{
    public class CrearProveedorController : Controller
    {
        //
        // GET: /CrearProveedor/

        [HttpPost]
        public ActionResult IngresarDatos(FormCollection collection, IEnumerable<HttpPostedFileBase> files)
        {
            string respuestaBizagi = string.Empty;
            try
            {                
                int convertCelular = 0;
                int convertCodigoPostal = 0;
                int convertTelefono = 0;
                //if (System.Web.HttpContext.Current.Session["NroCaso"] != null)
                //{
                    int numCaso = Convert.ToInt32(System.Web.HttpContext.Current.Session["NroCaso"]);

                    var razonSocial = collection["txtRazonSocialProveedor"];
                    if (razonSocial != null && razonSocial != "")
                    {
                        razonSocial = razonSocial.Trim();
                    }

                    var docTributario = collection["txtDocTributarioProveedor"];
                    if (docTributario != null && docTributario != "")
                    {
                        docTributario = docTributario.Trim();
                    }

                    var giroProveedor = collection["txtGiroProveedor"];
                    if (giroProveedor != null && giroProveedor != "")
                    {
                        giroProveedor = giroProveedor.Trim();
                    }

                    var sitioWeb = collection["txtSitioWebProveedor"];
                    if (sitioWeb != null && sitioWeb != "")
                    {
                        sitioWeb = sitioWeb.Trim();
                    }

                    var rutProveedor = collection["txtRutProveedor"];
                    if (rutProveedor != null && rutProveedor != "")
                    {
                        rutProveedor = rutProveedor.Trim();
                    }

                    var moneda = collection["txtMoneda"];
                    //Obtener id del Moneda seleccionado
                    int convertMoneda = Convert.ToInt32(moneda);
                    if (convertMoneda == 0)
                    {
                        throw new Exception("El campo Moneda no puede estar vacío");
                    }
                    

                    var calleNumero = collection["txtCalleNumeroDirección"];
                    if (calleNumero != null && calleNumero != "")
                    {
                        calleNumero = calleNumero.Trim();
                    }

                    var codigoPostal = collection["txtCodigoPostalDirección"];
                    if (codigoPostal != null && codigoPostal != "")
                    {
                        codigoPostal = codigoPostal.Trim();
                        //convertir codigoPostal a entero
                        convertCodigoPostal = Convert.ToInt32(codigoPostal);
                    }
                    
                    var ciudad = collection["txtCiudad"];
                    if (ciudad != null && ciudad != "")
                    {
                        ciudad = ciudad.Trim();
                    }

                    var region = collection["txtRegionDirección"];
                    if (region != null && region != "")
                    {
                        region = region.Trim();
                    }

                    var nombreContacto = collection["txtNombreContacto"];
                    if (nombreContacto != null && nombreContacto != "")
                    {
                        nombreContacto = nombreContacto.Trim();
                    }

                    var extContacto = collection["txtExtensionContacto"];
                    if (extContacto != null && extContacto != "")
                    {
                        extContacto = extContacto.Trim();
                    }

                    var cargoContacto = collection["txtCargoContacto"];
                    if (cargoContacto != null && cargoContacto != "")
                    {
                        cargoContacto = cargoContacto.Trim();
                    }

                    var celularContacto = collection["txtCelularContacto"];
                    if (celularContacto != null && celularContacto != "")
                    {
                        celularContacto = celularContacto.Trim();
                    }

                    var telefonoContacto = collection["txtTelefonoContacto"];
                    if (telefonoContacto != null && telefonoContacto != "")
                    {
                        telefonoContacto = telefonoContacto.Trim();
                    }

                    var correoContacto = collection["txtCorreoContacto"];
 

                // Conversión de archivos
                string fileCedulaRepresentante = string.Empty;
                string extCedulaRepresentante = string.Empty;
                string fileRutEmpresaProveedora = string.Empty;
                string extRutEmpresaProveedora = string.Empty;
                string fileCertificadoCuenta = string.Empty;
                string extCertificadoCuenta = string.Empty;
                string fileEscrituraEmpresa = string.Empty;
                string extEscrituraEmpresa = string.Empty;
                string fileDicom = string.Empty;
                string extDicom = string.Empty;
                foreach (string upload in Request.Files)
                {
                    if (Request.Files[upload].FileName != "")
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "./";
                        string path = Path.GetTempPath();
                        string filename = "archivo_temporal";
                        string ext = Path.GetExtension(Request.Files[upload].FileName);
                        Request.Files[upload].SaveAs(Path.Combine(path, filename + ext));
                        string archivoConvertido = ConversorBase64.convertirABase64(path + filename + ext);
                        if (upload == "txtCedulaRepresentante")
                        {
                            fileCedulaRepresentante = archivoConvertido;
                            extCedulaRepresentante = ext;
                        }
                        else if (upload == "txtRutEmpresaProveedora")
                        {
                            fileRutEmpresaProveedora = archivoConvertido;
                            extRutEmpresaProveedora = ext;
                        }
                        else if (upload == "txtCertificadoCuenta")
                        {
                            fileCertificadoCuenta = archivoConvertido;
                            extCertificadoCuenta = ext;
                        }
                        else if (upload == "txtEscrituraEmpresa")
                        {
                            fileEscrituraEmpresa = archivoConvertido;
                            extEscrituraEmpresa = ext;
                        }
                        else if (upload == "txtDicom")
                        {
                            fileDicom = archivoConvertido;
                            extDicom = ext;
                        }
                    }
                }




                //XML PARA ACTUALIZAR VALORES DEL CASO
                string actualizar = @"<BizAgiWSParam>
                                                    <domain>domain</domain>
                                                    <userName>admon</userName>
                                                    <ActivityData>
                                                        <radNumber>"+ 53 + @"</radNumber>
                                                        <taskName>Event_3</taskName>
                                                    </ActivityData>
                                                    <Entities>
                                                        <M_ProcesoCreacionDeProve businessKey=""NumerodeCaso='" + 53 + @"'"">
                                                            <SolicitudCreaciondeProve>
                                                                    <DatosProveedor>
                                                                        <RazonSocial>" + razonSocial + @"</RazonSocial>
                                                                        <DocumentoTributario>" + docTributario + @"</DocumentoTributario>
                                                                        <Giro>" + giroProveedor + @"</Giro>
                                                                        <SitioWeb>" + sitioWeb + @"</SitioWeb>
                                                                        <Rut>" + rutProveedor + @"</Rut>
                                                                        <Moneda>" + convertMoneda + @"</Moneda>
                                                                        <CalleyNumero>" + calleNumero + @"</CalleyNumero>
                                                                        <CodigoPostal>" + convertCodigoPostal + @"</CodigoPostal>
                                                                        <Ciudad>" + ciudad + @"</Ciudad>
                                                                        <Region>" + region + @"</Region>
                                                                        <Nombre>" + nombreContacto + @"</Nombre>
                                                                        <Extension>" + extContacto + @"</Extension>
                                                                        <Cargo>" + cargoContacto + @"</Cargo>
                                                                        <Celular>" + celularContacto + @"</Celular>
                                                                        <TelefonoFijo>" + telefonoContacto + @"</TelefonoFijo>
                                                                        <CorreoElectronico>" + correoContacto + @"</CorreoElectronico>
                                                                    </DatosProveedor>
                                                                    <DocumentosAdjuntos>
                                                                        <CIRepresentanteLegal>
                                                                            <File fileName=""CIRepresentanteLegal" + extCedulaRepresentante + @""">" + fileCedulaRepresentante + @"</File>
                                                                        </CIRepresentanteLegal>
                                                                        <RUTempresaproveedora>
                                                                            <File fileName=""RUTempresaproveedora" + extRutEmpresaProveedora + @""">" + fileRutEmpresaProveedora + @"</File>
                                                                        </RUTempresaproveedora>
                                                                        <CertificadoCuentaCorriente>
                                                                            <File fileName=""CertificadoCuentaCorriente" + extCertificadoCuenta + @""">" + fileCertificadoCuenta + @"</File>
                                                                        </CertificadoCuentaCorriente>
                                                                        <EscrituradeEmpresa>
                                                                            <File fileName=""CertificadoCuentaCorriente" + extEscrituraEmpresa + @""">" + fileEscrituraEmpresa + @"</File>
                                                                        </EscrituradeEmpresa>
                                                                        <DICOM>
                                                                            <File fileName=""CertificadoCuentaCorriente" + extDicom + @""">" + fileDicom + @"</File>
                                                                        </DICOM>
                                                                    </DocumentosAdjuntos>
                                                            </SolicitudCreaciondeProve>
                                                        <CompletadoWeb>true</CompletadoWeb>
                                                        </M_ProcesoCreacionDeProve>
                                                    </Entities>
                                               </BizAgiWSParam>";

                    //Escribir log CSV
                    UtilController.EscribirLog("XML actualizar", "IngresarDatos", actualizar);
                    //Fin CSV

                    //ABRIR CONEXION A SERVICIO WEB
                    WorkflowEngine.WorkflowEngineSOASoapClient servicioQuery = new WorkflowEngine.WorkflowEngineSOASoapClient();


                    respuestaBizagi = servicioQuery.saveActivityAsString(actualizar);
                    respuestaBizagi = respuestaBizagi.Replace("\n", "");
                    respuestaBizagi = respuestaBizagi.Replace("\t", "");
                    respuestaBizagi = respuestaBizagi.Replace("\r", "");

                    //Escribir log CSV
                    UtilController.EscribirLog("Respuesta actualizar", "IngresarDatos", respuestaBizagi);
                    //Fin CSV
                //}
                /*else
                {
                    throw new Exception("Número de caso sin especificar");
                } */               
            }
            catch (Exception ex)
            {
                //Escribir log CSV
                UtilController.EscribirLog("ERROR", "IngresarDatos", ex.Message);
                //Fin CSV
            }
            return Json(respuestaBizagi);
        }

        [HttpPost]
        public string IngresarDatosIngles(FormCollection collection)
        {
            string respuestaBizagi = string.Empty;
            try
            {
                int convertMobile = 0;
                int convertPostalCode = 0;
                int convertPhone = 0;
                int convertTaxId = 0;
                if (System.Web.HttpContext.Current.Session["NroCaso"] != null)
                {
                    int numCaso = Convert.ToInt32(System.Web.HttpContext.Current.Session["NroCaso"]);

                    var legalName = collection["txtLegalName"];
                    if (legalName != null && legalName != "")
                    {
                        legalName = legalName.Trim();
                    }

                    var currency = collection["txtCurrency"];
                    if (currency != null && currency != "")
                    {
                        currency = currency.Trim();
                    }

                    var industry = collection["txtIndustry"];
                    if (industry != null && industry != "")
                    {
                        industry = industry.Trim();
                    }

                    var webSite = collection["txtWebsite"];
                    if (webSite != null && webSite != "")
                    {
                        webSite = webSite.Trim();
                    }

                    var taxId = collection["txtTaxId"];//entero
                    if (taxId != null && taxId != "")
                    {
                        taxId = taxId.Trim();
                        //converter taxId to int
                        convertTaxId = Convert.ToInt32(taxId);
                    }

                    var typeProvider = collection["txtTypeOfProvider"];
                    //Obtener id del tipoProveedor seleccionado
                    int convertTypeProvider = Convert.ToInt32(typeProvider);
                    if (convertTypeProvider == 0)
                    {
                        throw new Exception("The Provider Type field cannot be empty");
                    }

                    var street = collection["txtStreet"];
                    if (street != null && street != "")
                    {
                        street = street.Trim();
                    }
                    var number = collection["txtNumber"];
                    if (number != null && number != "")
                    {
                        number = number.Trim();
                    }

                    var country = collection["txtCountry"];
                    if (country != null && country != "")
                    {
                        country = country.Trim();
                    }

                    var city = collection["txtCity"];
                    if (city != null && city != "")
                    {
                        city = city.Trim();
                    }

                    var postalCode = collection["txtPostalCode"];
                    if (postalCode != null && postalCode != "")
                    {
                        postalCode = postalCode.Trim();
                        //converter postalCode to int
                        convertPostalCode = Convert.ToInt32(postalCode);
                    }

                    var name = collection["txtName"];
                    if (name != null && name != "")
                    {
                        name = name.Trim();
                    }

                    var extension = collection["txtExtension"];
                    if (extension != null && extension != "")
                    {
                        extension = extension.Trim();
                    }

                    var jobPosition = collection["txtJobPosition"];
                    if (jobPosition != null && jobPosition != "")
                    {
                        jobPosition = jobPosition.Trim();
                    }

                    var mobile = collection["txtMobile"];
                    if (mobile != null && mobile != "")
                    {
                        mobile = mobile.Trim();
                        //conver mobile to int
                        convertMobile = Convert.ToInt32(mobile);
                    }

                    var phone = collection["txtPhone"];
                    if (phone != null && phone != "")
                    {
                        phone = phone.Trim();
                        //conver phone to int
                        convertPhone = Convert.ToInt32(phone);
                    }

                    var email = collection["txtEmail"];
                    if (email != null && email != "")
                    {
                        email = email.Trim();
                    }                    

                    var businessCertificate = collection["txtBusinessCertificate"];//archivo
                    var bankCertificate = collection["txtBankCertificate"];//archivo
                    var dubAndBradseet = collection["txtDubAndBradseet"];//archivo
                    var taxIdCopy = collection["txtTaxIdCopy"];//archivo                

                    //XML PARA ACTUALIZAR VALORES DEL CASO
                    string actualizar = @"<BizAgiWSParam>
                                                    <domain>domain</domain>
                                                    <userName>admon</userName>
                                                    <ActivityData>
                                                        <radNumber>" + numCaso + @"</radNumber>
                                                        <taskName>Event_3</taskName>
                                                    </ActivityData>
                                                    <Entities>
                                                        <M_ProcesoCreacionDeProve businessKey=""NumerodeCaso='" + numCaso + @"'"">
                                                            <SolicitudCreaciondeProve>
                                                                    <DatosProveedor>
                                                                        <LegalName>" + legalName + @"</LegalName>
                                                                        <Currency>" + currency + @"</Currency>
                                                                        <Industry>" + industry + @"</Industry>
                                                                        <SitioWeb>" + webSite + @"</SitioWeb>
                                                                        <TAXID>" + convertTaxId + @"</TAXID>
                                                                        <TipodeProveedor>" + convertTypeProvider + @"</TipodeProveedor>
                                                                        <Street>" + street + @"</Street>
                                                                        <Numberaddress>" + number + @"</Numberaddress>
                                                                        <Country>" + country + @"</Country>
                                                                        <ComunayCiudad>" + city + @"</ComunayCiudad>
                                                                        <CodigoPostal>" + convertPostalCode + @"</CodigoPostal>
                                                                        <Nombre>" + name + @"</Nombre>
                                                                        <Extension>" + extension + @"</Extension>
                                                                        <Cargo>" + jobPosition + @"</Cargo>
                                                                        <Celular>" + convertMobile + @"</Celular>
                                                                        <TelefonoFijo>" + convertPhone + @"</TelefonoFijo>
                                                                        <CorreoElectronico>" + email + @"</CorreoElectronico>
                                                                    </DatosProveedor>
                                                                    <DocumentosAdjuntos>
                                                                        <BusinessCertificate>" + businessCertificate + @"</BusinessCertificate>
                                                                        <BankCertificate>" + bankCertificate + @"</BankCertificate>
                                                                        <DunBradsteet>" + dubAndBradseet + @"</DunBradsteet>
                                                                        <TAXID>" + taxIdCopy + @"</TAXID>
                                                                    </DocumentosAdjuntos>
                                                            </SolicitudCreaciondeProve>
                                                        <CompletadoWeb>true</CompletadoWeb>
                                                        </M_ProcesoCreacionDeProve>
                                                    </Entities>
                                               </BizAgiWSParam>";

                    //Escribir log CSV
                    UtilController.EscribirLog("XML actualizar", "IngresarDatosIngles", actualizar);
                    //Fin CSV

                    //ABRIR CONEXION A SERVICIO WEB



                    // respuestaBizagi = servicioQuery.saveEntityAsString(actualizar);
                    respuestaBizagi = respuestaBizagi.Replace("\n", "");
                    respuestaBizagi = respuestaBizagi.Replace("\t", "");
                    respuestaBizagi = respuestaBizagi.Replace("\r", "");

                    //Escribir log CSV
                    UtilController.EscribirLog("Respuesta actualizar", "IngresarDatosIngles", respuestaBizagi);
                    //Fin CSV
                }
                else
                {
                    throw new Exception("Unspecified case number");
                }
            }
            catch (Exception ex)
            {
                //Escribir log CSV
                UtilController.EscribirLog("ERROR", "IngresarDatosIngles", ex.Message);
                //Fin CSV
            }
            return (respuestaBizagi);
        }

        //Obtener información del caso. APLICA SOLO SI ES CORRECCION, ASI PRECARGAR LOS DATOS
        public ActionResult ObtenerCaso(int id)
        {
            string respuestaBizagi = string.Empty;
            try
            {
                //Abrir conexion
                EntityManager.EntityManagerSOASoapClient servicioQuery = new EntityManager.EntityManagerSOASoapClient();

                //Escribir log CSV
                UtilController.EscribirLog("Caso a obtenido", "ObtenerCaso", Convert.ToString(id));
                //Fin CSV

                //Crear XML para obtener la información del caso a modificar

                string queryObtenerCaso = @"
                <BizAgiWSParam>
                    <CaseInfo>
                        <IdCase>" + id + @"</IdCase>
                    </CaseInfo>
                    <XPaths>
                        <XPath XPath=""M_ProcesoCreacionDeProve.SolicitudCreaciondeProve.DatosProveedor.RazonSocial""/>
                        <XPath XPath=""M_ProcesoCreacionDeProve.SolicitudCreaciondeProve.DatosProveedor.DocumentoTributario""/>
                        <XPath XPath=""M_ProcesoCreacionDeProve.SolicitudCreaciondeProve.DatosProveedor.Giro""/>
                        <XPath XPath=""M_ProcesoCreacionDeProve.SolicitudCreaciondeProve.DatosProveedor.SitioWeb""/>
                        <XPath XPath=""M_ProcesoCreacionDeProve.SolicitudCreaciondeProve.DatosProveedor.Rut""/>
                        <XPath XPath=""M_ProcesoCreacionDeProve.SolicitudCreaciondeProve.DatosProveedor.TipodeProveedor""/>
                        <XPath XPath=""M_ProcesoCreacionDeProve.SolicitudCreaciondeProve.DatosProveedor.Divisa""/>
                        <XPath XPath=""M_ProcesoCreacionDeProve.SolicitudCreaciondeProve.DatosProveedor.CalleyNumero""/>
                        <XPath XPath=""M_ProcesoCreacionDeProve.SolicitudCreaciondeProve.DatosProveedor.CodigoPostal""/>
                        <XPath XPath=""M_ProcesoCreacionDeProve.SolicitudCreaciondeProve.DatosProveedor.ComunayCiudad""/>
                        <XPath XPath=""M_ProcesoCreacionDeProve.SolicitudCreaciondeProve.DatosProveedor.Region""/>
                        <XPath XPath=""M_ProcesoCreacionDeProve.SolicitudCreaciondeProve.DatosProveedor.Nombre""/>
                        <XPath XPath=""M_ProcesoCreacionDeProve.SolicitudCreaciondeProve.DatosProveedor.Extension""/>
                        <XPath XPath=""M_ProcesoCreacionDeProve.SolicitudCreaciondeProve.DatosProveedor.Cargo""/>
                        <XPath XPath=""M_ProcesoCreacionDeProve.SolicitudCreaciondeProve.DatosProveedor.Celular""/>
                        <XPath XPath=""M_ProcesoCreacionDeProve.SolicitudCreaciondeProve.DatosProveedor.TelefonoFijo""/>
                        <XPath XPath=""M_ProcesoCreacionDeProve.SolicitudCreaciondeProve.DatosProveedor.CorreoElectronico""/>
                        <XPath XPath=""M_ProcesoCreacionDeProve.SolicitudCreaciondeProve.DocumentosAdjuntos.CIRepresentanteLegal""/>
                        <XPath XPath=""M_ProcesoCreacionDeProve.SolicitudCreaciondeProve.DocumentosAdjuntos.RUTempresaproveedora""/>
                        <XPath XPath=""M_ProcesoCreacionDeProve.SolicitudCreaciondeProve.DocumentosAdjuntos.CertificadoCuentaCorriente""/>
                        <XPath XPath=""M_ProcesoCreacionDeProve.SolicitudCreaciondeProve.DocumentosAdjuntos.EscrituradeEmpresa""/>
                        <XPath XPath=""M_ProcesoCreacionDeProve.SolicitudCreaciondeProve.DocumentosAdjuntos.DICOM""/>
                    </XPaths>
                    </BizAgiWSParam>";

                //Escribir log CSV
                UtilController.EscribirLog("Campos solicitados", "ObtenerCaso", queryObtenerCaso);
                //Fin CSV

                respuestaBizagi = servicioQuery.getCaseDataUsingXPathsAsString(queryObtenerCaso);
                respuestaBizagi = respuestaBizagi.Replace("\n", "");
                respuestaBizagi = respuestaBizagi.Replace("\t", "");
                respuestaBizagi = respuestaBizagi.Replace("\r", "");

                //Escribir log CSV
                UtilController.EscribirLog("Respuesta", "ObtenerCaso", respuestaBizagi);
                //Fin CSV

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(respuestaBizagi);

                //Obtener valores del xml respuesta de Bizagi del numero de caso enviado

                //DATOS PROVEEDOR
                string razonSocial = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='SolicitudCreaciondeProve.DatosProveedor.RazonSocial']").InnerText;
                string docTributario = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='SolicitudCreaciondeProve.DatosProveedor.DocumentoTributario']").InnerText;
                string giro = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='SolicitudCreaciondeProve.DatosProveedor.Giro']").InnerText;
                string sitioWeb = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='SolicitudCreaciondeProve.DatosProveedor.SitioWeb']").InnerText;
                string rut = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='SolicitudCreaciondeProve.DatosProveedor.Rut']").InnerText;
                string tipoProveedor = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='SolicitudCreaciondeProve.DatosProveedor.TipodeProveedor']").InnerText;
                string divisa = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='SolicitudCreaciondeProve.DatosProveedor.Divisa']").InnerText;
                string calleNumero = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='SolicitudCreaciondeProve.DatosProveedor.CalleyNumero']").InnerText;
                string codigoPostal = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='SolicitudCreaciondeProve.DatosProveedor.CodigoPostal']").InnerText;
                string comunaCiudad = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='SolicitudCreaciondeProve.DatosProveedor.ComunayCiudad']").InnerText;
                string region = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='SolicitudCreaciondeProve.DatosProveedor.Region']").InnerText;
                string nombre = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='SolicitudCreaciondeProve.DatosProveedor.Nombre']").InnerText;
                string extension = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='SolicitudCreaciondeProve.DatosProveedor.Extension']").InnerText;
                string cargo = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='SolicitudCreaciondeProve.DatosProveedor.Cargo']").InnerText;
                string celular = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='SolicitudCreaciondeProve.DatosProveedor.Celular']").InnerText;
                string telefono = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='SolicitudCreaciondeProve.DatosProveedor.TelefonoFijo']").InnerText;
                string email = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='SolicitudCreaciondeProve.DatosProveedor.CorreoElectronico']").InnerText;

                
                bool CIRepresentanteLegal = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='SolicitudCreaciondeProve.DocumentosAdjuntos.CIRepresentanteLegal']/Items/Item") != null;
                XmlNodeList contieneCIRepresentanteLegal = doc.SelectNodes("/BizAgiWSResponse/XPath[@XPath='SolicitudCreaciondeProve.DocumentosAdjuntos.CIRepresentanteLegal']/Items/Item");

                if (CIRepresentanteLegal)
                {
                    foreach (XmlNode item in contieneCIRepresentanteLegal)
                    {
                        string txtCIRepresentanteLegalBase64 = item.InnerText;
                        string txtCIRepresentanteLegalNombre = item.Attributes["FileName"].InnerText;

                        ViewData["txtCedulaRepresentante"] += @"
                    <a download='" + txtCIRepresentanteLegalNombre + @"' href='data:application/octet-stream;charset=utf-16le;base64," + txtCIRepresentanteLegalBase64 + @"' class='btn btn-primary btn-md' style='width:300px;margin-bottom:3px'>
                        <span class='glyphicon glyphicon-save'></span>" + txtCIRepresentanteLegalNombre + @"
                    </a>
                ";

                        txtCIRepresentanteLegalBase64 = null;
                        txtCIRepresentanteLegalNombre = null;
                    }
                }
                
                bool RUTempresaproveedora = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='SolicitudCreaciondeProve.DocumentosAdjuntos.RUTempresaproveedora']/Items/Item") != null;
                XmlNodeList contieneRUTempresaproveedora = doc.SelectNodes("/BizAgiWSResponse/XPath[@XPath='SolicitudCreaciondeProve.DocumentosAdjuntos.RUTempresaproveedora']/Items/Item");

                if (RUTempresaproveedora)
                {
                    foreach (XmlNode item in contieneRUTempresaproveedora)
                    {
                        string txtRUTempresaproveedoraBase64 = item.InnerText;
                        string txtRUTempresaproveedoraNombre = item.Attributes["FileName"].InnerText;

                        ViewData["txtRutEmpresaProveedora"] += @"
                    <a download='" + txtRUTempresaproveedoraNombre + @"' href='data:application/octet-stream;charset=utf-16le;base64," + txtRUTempresaproveedoraBase64 + @"' class='btn btn-primary btn-md' style='width:310px;margin-bottom:3px'>
                        <span class='glyphicon glyphicon-save'></span>" + txtRUTempresaproveedoraNombre + @"
                    </a>
                    ";

                        txtRUTempresaproveedoraBase64 = null;
                        txtRUTempresaproveedoraNombre = null;
                    }
                }

                bool CertificadoCuentaCorriente = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='SolicitudCreaciondeProve.DocumentosAdjuntos.CertificadoCuentaCorriente']/Items/Item") != null;
                XmlNodeList contieneCertificadoCuentaCorriente = doc.SelectNodes("/BizAgiWSResponse/XPath[@XPath='SolicitudCreaciondeProve.DocumentosAdjuntos.CertificadoCuentaCorriente']/Items/Item");

                if (CertificadoCuentaCorriente)
                {
                    foreach (XmlNode item in contieneCertificadoCuentaCorriente)
                    {
                        string txtCertificadoCuentaCorrienteBase64 = item.InnerText;
                        string txtCertificadoCuentaCorrienteNombre = item.Attributes["FileName"].InnerText;

                        ViewData["txtCertificadoCuenta"] += @"
                    <a download='" + txtCertificadoCuentaCorrienteNombre + @"' href='data:application/octet-stream;charset=utf-16le;base64," + txtCertificadoCuentaCorrienteBase64 + @"' class='btn btn-primary btn-md' style='width:300px;margin-bottom:3px'>
                        <span class='glyphicon glyphicon-save'></span>" + txtCertificadoCuentaCorrienteNombre + @"
                    </a>
                ";

                        txtCertificadoCuentaCorrienteBase64 = null;
                        txtCertificadoCuentaCorrienteNombre = null;
                    }
                }
                
                bool EscrituradeEmpresa = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='SolicitudCreaciondeProve.DocumentosAdjuntos.EscrituradeEmpresa']/Items/Item") != null;
                XmlNodeList contieneEscrituradeEmpresa = doc.SelectNodes("/BizAgiWSResponse/XPath[@XPath='SolicitudCreaciondeProve.DocumentosAdjuntos.EscrituradeEmpresa']/Items/Item");

                if (EscrituradeEmpresa)
                {
                    foreach (XmlNode item in contieneEscrituradeEmpresa)
                    {
                        string txtEscrituradeEmpresaBase64 = item.InnerText;
                        string txtEscrituradeEmpresaNombre = item.Attributes["FileName"].InnerText;

                        ViewData["txtEscrituraEmpresa"] += @"
                    <a download='" + txtEscrituradeEmpresaNombre + @"' href='data:application/octet-stream;charset=utf-16le;base64," + txtEscrituradeEmpresaBase64 + @"' class='btn btn-primary btn-md' style='width:310px;margin-bottom:3px'>
                        <span class='glyphicon glyphicon-save'></span>" + txtEscrituradeEmpresaNombre + @"
                    </a>
                    ";

                        txtEscrituradeEmpresaBase64 = null;
                        txtEscrituradeEmpresaNombre = null;
                    }
                }

                bool dicom = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='SolicitudCreaciondeProve.DocumentosAdjuntos.DICOM']/Items/Item") != null;
                XmlNodeList contieneDicom = doc.SelectNodes("/BizAgiWSResponse/XPath[@XPath='SolicitudCreaciondeProve.DocumentosAdjuntos.DICOM']/Items/Item");

                if (dicom)
                {
                    foreach (XmlNode item in contieneDicom)
                    {
                        string txtDicomBase64 = item.InnerText;
                        string txtDicomNombre = item.Attributes["FileName"].InnerText;

                        ViewData["txtDicom"] += @"
                    <a download='" + txtDicomNombre + @"' href='data:application/octet-stream;charset=utf-16le;base64," + txtDicomBase64 + @"' class='btn btn-primary btn-md' style='width:310px;margin-bottom:3px'>
                        <span class='glyphicon glyphicon-save'></span>" + txtDicomNombre + @"
                    </a>
                    ";

                        txtDicomBase64 = null;
                        txtDicomNombre = null;
                    }
                }                

                //ASIGNAR VALORES RESCATADOS DE XML A CAMPOS DEL FORMULARIO
                ViewData["txtRazonSocialProveedor"] = razonSocial;
                ViewData["txtDocTributarioProveedor"] = docTributario;
                ViewData["txtGiroProveedor"] = giro;
                ViewData["txtSitioWebProveedor"] = sitioWeb;
                ViewData["txtRutProveedor"] = rut;
                ViewData["txtTipoProveedor"] = tipoProveedor;

                ViewData["txtDivisaProveedor"] = divisa;
                ViewData["txtCalleNumeroDirección"] = calleNumero;
                ViewData["txtCodigoPostalDirección"] = codigoPostal;
                ViewData["txtComunaCiudadDirección"] = comunaCiudad;

                ViewData["txtRegionDirección"] = region;
                ViewData["txtNombreContacto"] = nombre;
                ViewData["txtExtensionContacto"] = extension;
                ViewData["txtCargoContacto"] = cargo;
                ViewData["txtCelularContacto"] = celular;
                ViewData["txtTelefonoContacto"] = telefono;
                ViewData["txtCorreoContacto"] = email;
            }
            catch (Exception ex)
            {
                //Escribir log CSV
                UtilController.EscribirLog("ERROR", "ObtenerCaso", ex.Message);
                //Fin CSV
            }
            return View();
        }

        //Obtener información del caso. APLICA SOLO SI ES CORRECCION, ASI PRECARGAR LOS DATOS
        public ActionResult ObtenerCasoIngles(int id)
        {
            string respuestaBizagi = string.Empty;
            try
            {
                //Abrir conexion
                EntityManager.EntityManagerSOASoapClient servicioQuery = new EntityManager.EntityManagerSOASoapClient();

                //Escribir log CSV
                UtilController.EscribirLog("Caso a obtenido", "ObtenerCasoIngles", Convert.ToString(id));
                //Fin CSV

                //Crear XML para obtener la información del caso a modificar

                string queryObtenerCaso = @"
                <BizAgiWSParam>
                    <CaseInfo>
                        <IdCase>" + id + @"</IdCase>
                    </CaseInfo>
                    <XPaths>
                        <XPath XPath=""M_ProcesoCreacionDeProve.SolicitudCreaciondeProve.DatosProveedor.LegalName""/>
                        <XPath XPath=""M_ProcesoCreacionDeProve.SolicitudCreaciondeProve.DatosProveedor.Currency""/>
                        <XPath XPath=""M_ProcesoCreacionDeProve.SolicitudCreaciondeProve.DatosProveedor.Industry""/>
                        <XPath XPath=""M_ProcesoCreacionDeProve.SolicitudCreaciondeProve.DatosProveedor.SitioWeb""/>
                        <XPath XPath=""M_ProcesoCreacionDeProve.SolicitudCreaciondeProve.DatosProveedor.TAXID""/>
                        <XPath XPath=""M_ProcesoCreacionDeProve.SolicitudCreaciondeProve.DatosProveedor.TipodeProveedor""/>
                        <XPath XPath=""M_ProcesoCreacionDeProve.SolicitudCreaciondeProve.DatosProveedor.CalleyNumero""/>
                        <XPath XPath=""M_ProcesoCreacionDeProve.SolicitudCreaciondeProve.DatosProveedor.Country""/>
                        <XPath XPath=""M_ProcesoCreacionDeProve.SolicitudCreaciondeProve.DatosProveedor.ComunayCiudad""/>
                        <XPath XPath=""M_ProcesoCreacionDeProve.SolicitudCreaciondeProve.DatosProveedor.CodigoPostal""/>
                        <XPath XPath=""M_ProcesoCreacionDeProve.SolicitudCreaciondeProve.DatosProveedor.Nombre""/>
                        <XPath XPath=""M_ProcesoCreacionDeProve.SolicitudCreaciondeProve.DatosProveedor.Extension""/>
                        <XPath XPath=""M_ProcesoCreacionDeProve.SolicitudCreaciondeProve.DatosProveedor.Cargo""/>
                        <XPath XPath=""M_ProcesoCreacionDeProve.SolicitudCreaciondeProve.DatosProveedor.Celular""/>
                        <XPath XPath=""M_ProcesoCreacionDeProve.SolicitudCreaciondeProve.DatosProveedor.TelefonoFijo""/>
                        <XPath XPath=""M_ProcesoCreacionDeProve.SolicitudCreaciondeProve.DatosProveedor.CorreoElectronico""/>
                        <XPath XPath=""M_ProcesoCreacionDeProve.SolicitudCreaciondeProve.DocumentosAdjuntos.BusinessCertificate""/>
                        <XPath XPath=""M_ProcesoCreacionDeProve.SolicitudCreaciondeProve.DocumentosAdjuntos.BankCertificate""/>
                        <XPath XPath=""M_ProcesoCreacionDeProve.SolicitudCreaciondeProve.DocumentosAdjuntos.DunBradsteet""/>
                        <XPath XPath=""M_ProcesoCreacionDeProve.SolicitudCreaciondeProve.DocumentosAdjuntos.TAXID""/>
                    </XPaths>
                    </BizAgiWSParam>";

                //Escribir log CSV
                UtilController.EscribirLog("Campos solicitados", "ObtenerCasoIngles", queryObtenerCaso);
                //Fin CSV

                respuestaBizagi = servicioQuery.getCaseDataUsingXPathsAsString(queryObtenerCaso);
                respuestaBizagi = respuestaBizagi.Replace("\n", "");
                respuestaBizagi = respuestaBizagi.Replace("\t", "");
                respuestaBizagi = respuestaBizagi.Replace("\r", "");

                //Escribir log CSV
                UtilController.EscribirLog("Respuesta", "ObtenerCasoIngles", respuestaBizagi);
                //Fin CSV

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(respuestaBizagi);

                //Obtener valores del xml respuesta de Bizagi del numero de caso enviado

                //DATOS PROVEEDOR
                string legalName = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='SolicitudCreaciondeProve.DatosProveedor.LegalName']").InnerText;
                string currency = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='SolicitudCreaciondeProve.DatosProveedor.Currency']").InnerText;
                string industry = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='SolicitudCreaciondeProve.DatosProveedor.Industry']").InnerText;
                string webSite = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='SolicitudCreaciondeProve.DatosProveedor.SitioWeb']").InnerText;
                string taxId = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='SolicitudCreaciondeProve.DatosProveedor.TAXID']").InnerText;
                string typeProvider = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='SolicitudCreaciondeProve.DatosProveedor.TipodeProveedor']").InnerText;
                string streetNumber = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='SolicitudCreaciondeProve.DatosProveedor.CalleyNumero']").InnerText;
                string country = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='SolicitudCreaciondeProve.DatosProveedor.Country']").InnerText;
                string city = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='SolicitudCreaciondeProve.DatosProveedor.ComunayCiudad']").InnerText;
                string postalCode = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='SolicitudCreaciondeProve.DatosProveedor.CodigoPostal']").InnerText;
                string name = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='SolicitudCreaciondeProve.DatosProveedor.Nombre']").InnerText;
                string extension = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='SolicitudCreaciondeProve.DatosProveedor.Extension']").InnerText;
                string jobPosition = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='SolicitudCreaciondeProve.DatosProveedor.Cargo']").InnerText;
                string mobile = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='SolicitudCreaciondeProve.DatosProveedor.Celular']").InnerText;
                string phone = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='SolicitudCreaciondeProve.DatosProveedor.TelefonoFijo']").InnerText;
                string email = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='SolicitudCreaciondeProve.DatosProveedor.CorreoElectronico']").InnerText;

                
                bool businessCertificate = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='SolicitudCreaciondeProve.DocumentosAdjuntos.BusinessCertificate']/Items/Item") != null;
                XmlNodeList contieneBusinessCertificate = doc.SelectNodes("/BizAgiWSResponse/XPath[@XPath='SolicitudCreaciondeProve.DocumentosAdjuntos.BusinessCertificate']/Items/Item");

                if (businessCertificate)
                {
                    foreach (XmlNode item in contieneBusinessCertificate)
                    {
                        string txtbusinessCertificateBase64 = item.InnerText;
                        string txtbusinessCertificateNombre = item.Attributes["FileName"].InnerText;

                        ViewData["txtBusinessCertificate"] += @"
                    <a download='" + txtbusinessCertificateNombre + @"' href='data:application/octet-stream;charset=utf-16le;base64," + txtbusinessCertificateBase64 + @"' class='btn btn-primary btn-md' style='width:300px;margin-bottom:3px'>
                        <span class='glyphicon glyphicon-save'></span>" + txtbusinessCertificateNombre + @"
                    </a>
                ";

                        txtbusinessCertificateBase64 = null;
                        txtbusinessCertificateNombre = null;
                    }
                }
                
                bool bankCertificate = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='SolicitudCreaciondeProve.DocumentosAdjuntos.BankCertificate']/Items/Item") != null;
                XmlNodeList contieneBankCertificate = doc.SelectNodes("/BizAgiWSResponse/XPath[@XPath='SolicitudCreaciondeProve.DocumentosAdjuntos.BankCertificate']/Items/Item");

                if (bankCertificate)
                {
                    foreach (XmlNode item in contieneBankCertificate)
                    {
                        string txtbankCertificateBase64 = item.InnerText;
                        string txtbankCertificateNombre = item.Attributes["FileName"].InnerText;

                        ViewData["txtBankCertificate"] += @"
                    <a download='" + txtbankCertificateNombre + @"' href='data:application/octet-stream;charset=utf-16le;base64," + txtbankCertificateBase64 + @"' class='btn btn-primary btn-md' style='width:310px;margin-bottom:3px'>
                        <span class='glyphicon glyphicon-save'></span>" + txtbankCertificateNombre + @"
                    </a>
                    ";

                        txtbankCertificateBase64 = null;
                        txtbankCertificateNombre = null;
                    }
                }

                bool DunBradsteet = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='SolicitudCreaciondeProve.DocumentosAdjuntos.DunBradsteet']/Items/Item") != null;
                XmlNodeList contieneDunBradsteet = doc.SelectNodes("/BizAgiWSResponse/XPath[@XPath='SolicitudCreaciondeProve.DocumentosAdjuntos.DunBradsteet']/Items/Item");

                if (DunBradsteet)
                {
                    foreach (XmlNode item in contieneDunBradsteet)
                    {
                        string txtDunBradsteetBase64 = item.InnerText;
                        string txtDunBradsteetNombre = item.Attributes["FileName"].InnerText;

                        ViewData["txtDubAndBradseet"] += @"
                    <a download='" + txtDunBradsteetNombre + @"' href='data:application/octet-stream;charset=utf-16le;base64," + txtDunBradsteetBase64 + @"' class='btn btn-primary btn-md' style='width:300px;margin-bottom:3px'>
                        <span class='glyphicon glyphicon-save'></span>" + txtDunBradsteetNombre + @"
                    </a>
                ";

                        txtDunBradsteetBase64 = null;
                        txtDunBradsteetNombre = null;
                    }
                }
                
                bool TAXID = doc.SelectSingleNode("/BizAgiWSResponse/XPath[@XPath='SolicitudCreaciondeProve.DocumentosAdjuntos.TAXID']/Items/Item") != null;
                XmlNodeList contieneTAXID = doc.SelectNodes("/BizAgiWSResponse/XPath[@XPath='SolicitudCreaciondeProve.DocumentosAdjuntos.TAXID']/Items/Item");

                if (TAXID)
                {
                    foreach (XmlNode item in contieneTAXID)
                    {
                        string txtTAXIDBase64 = item.InnerText;
                        string txtTAXIDNombre = item.Attributes["FileName"].InnerText;

                        ViewData["txtTaxIdCopy"] += @"
                    <a download='" + txtTAXIDNombre + @"' href='data:application/octet-stream;charset=utf-16le;base64," + txtTAXIDBase64 + @"' class='btn btn-primary btn-md' style='width:310px;margin-bottom:3px'>
                        <span class='glyphicon glyphicon-save'></span>" + txtTAXIDNombre + @"
                    </a>
                    ";

                        txtTAXIDBase64 = null;
                        txtTAXIDNombre = null;
                    }
                }

                ////FORMATEAR FECHA DE VISITA
                //if (txtFechaDeVisita != string.Empty)
                //{
                //    DateTime fechaVisita = DateTime.Parse(txtFechaDeVisita);
                //    ViewData["txtFechaDeVisita"] = fechaVisita.ToString("yyyy-MM-dd");
                //}

                //ASIGNAR VALORES RESCATADOS DE XML A CAMPOS DEL FORMULARIO
                ViewData["txtLegalName"] = legalName;
                ViewData["txtCurrency"] = currency;
                ViewData["txtIndustry"] = industry;
                ViewData["txtWebsite"] = webSite;
                ViewData["txtTaxId"] = taxId;
                ViewData["txtTypeOfProvider"] = typeProvider;

                ViewData["txtStreetAndNumber"] = streetNumber;
                ViewData["txtCountry"] = country;
                ViewData["txtCity"] = city;
                ViewData["txtPostalCode"] = postalCode;

                ViewData["txtName"] = name;
                ViewData["txtExtension"] = extension;
                ViewData["txtJobPosition"] = jobPosition;
                ViewData["txtMobile"] = mobile;
                ViewData["txtPhone"] = phone;
                ViewData["txtEmail"] = email;                
            }
            catch (Exception ex)
            {
                //Escribir log CSV
                UtilController.EscribirLog("ERROR", "ObtenerCasoIngles", ex.Message);
                //Fin CSV
            }
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

    }
}
