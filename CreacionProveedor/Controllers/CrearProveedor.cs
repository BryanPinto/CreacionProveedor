using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSolicitudes.Controllers
{
    public class CrearProveedor : Controller
    {
        //
        // GET: /CrearProveedor/
        [HttpPost]
        public string IngresarDatos(FormCollection collection)
        {
            string respuestaBizagi = string.Empty;
            try
            {                
                int convertCelular = 0;
                int convertCodigoPostal = 0;
                int convertTelefono = 0;
                if (System.Web.HttpContext.Current.Session["NroCaso"] != null)
                {
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

                    var tipoProveedor = collection["txtTipoProveedor"];
                    //Obtener id del tipoProveedor seleccionado
                    int convertTipoProveedor = Convert.ToInt32(tipoProveedor);
                    if(convertTipoProveedor == 0)
                    {
                        throw new Exception("El campo Tipo de proveedor no puede estar vacío");
                    }

                    var divisaProveedor = collection["txtDivisaProveedor"];
                    if (divisaProveedor != null && divisaProveedor != "")
                    {
                        divisaProveedor = divisaProveedor.Trim();
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
                    
                    var comunaCiudad = collection["txtComunaCiudadDirección"];
                    if (comunaCiudad != null && comunaCiudad != "")
                    {
                        comunaCiudad = comunaCiudad.Trim();
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
                        //convertir codigoPostal a entero
                        convertCelular = Convert.ToInt32(celularContacto);
                    }

                    var telefonoContacto = collection["txtTelefonoContacto"];
                    if (telefonoContacto != null && telefonoContacto != "")
                    {
                        telefonoContacto = telefonoContacto.Trim();
                        //convertir codigoPostal a entero
                        convertTelefono = Convert.ToInt32(telefonoContacto);
                    }

                    var correoContacto = collection["txtCorreoContacto"];

                    //var condicionPago = collection["txtCondicionPagoCompra"];
                    //var condicionEntrega = collection["txtCondicionEntregaCompra"];
                    //var modoEntrega = collection["txtModoEntregaCompra"];

                    //var banco = collection["txtBanco"];
                    //var numCuenta = collection["txtNumCuentaBanco"];

                    var cedulaRepresentante = collection["txtCedulaRepresentante"];//archivo
                    var rutEmpresaProveedora = collection["txtRutEmpresaProveedora"];//archivo
                    var certificadoCuenta = collection["txtCertificadoCuenta"];//archivo
                    var escrituraEmpresa = collection["txtEscrituraEmpresa"];//archivo
                    var dicom = collection["txtDicom"];//archivo                    

                    //XML PARA ACTUALIZAR VALORES DEL CASO
                    string actualizar = @"<BizAgiWSParam>
                                                    <domain>domain</domain>
                                                    <userName>admon</userName>
                                                    <ActivityData>
                                                        <radNumber>"+ numCaso + @"</radNumber>
                                                        <taskName>Event_3</taskName>
                                                    </ActivityData>
                                                    <Entities>
                                                        <M_ProcesoCreacionDeProve businessKey=""NumerodeCaso='" + numCaso + @"'"">
                                                            <SolicitudCreaciondeProve>
                                                                    <DatosProveedor>
                                                                        <RazonSocial>" + razonSocial + @"</RazonSocial>
                                                                        <DocumentoTributario>" + docTributario + @"</DocumentoTributario>
                                                                        <Giro>" + giroProveedor + @"</Giro>
                                                                        <SitioWeb>" + sitioWeb + @"</SitioWeb>
                                                                        <Rut>" + rutProveedor + @"</Rut>
                                                                        <TipodeProveedor>" + convertTipoProveedor + @"</TipodeProveedor>
                                                                        <Divisa>" + divisaProveedor + @"</Divisa>
                                                                        <CalleyNumero>" + calleNumero + @"</CalleyNumero>
                                                                        <CodigoPostal>" + convertCodigoPostal + @"</CodigoPostal>
                                                                        <ComunayCiudad>" + comunaCiudad + @"</ComunayCiudad>
                                                                        <Region>" + region + @"</Region>
                                                                        <Nombre>" + nombreContacto + @"</Nombre>
                                                                        <Extension>" + extContacto + @"</Extension>
                                                                        <Cargo>" + cargoContacto + @"</Cargo>
                                                                        <Celular>" + convertCelular + @"</Celular>
                                                                        <TelefonoFijo>" + convertTelefono + @"</TelefonoFijo>
                                                                        <CorreoElectronico>" + correoContacto + @"</CorreoElectronico>
                                                                    </DatosProveedor>
                                                                    <DocumentosAdjuntos>
                                                                        <CIRepresentanteLegal>" + cedulaRepresentante + @"</CIRepresentanteLegal>
                                                                        <RUTempresaproveedora>" + rutEmpresaProveedora + @"</RUTempresaproveedora>
                                                                        <CertificadoCuentaCorriente>" + certificadoCuenta + @"</CertificadoCuentaCorriente>
                                                                        <EscrituradeEmpresa>" + escrituraEmpresa + @"</EscrituradeEmpresa>
                                                                        <DICOM>" + dicom + @"</DICOM>
                                                                    </DocumentosAdjuntos>
                                                            </SolicitudCreaciondeProve>
                                                        <CompletadoWeb>true</CompletadoWeb>
                                                        </M_ProcesoCreacionDeProve>
                                                    </Entities>
                                               </BizAgiWSParam>";

                    //Escribir log CSV
                    Util.EscribirLog("XML actualizar", "IngresarDatos", actualizar);
                    //Fin CSV

                    //ABRIR CONEXION A SERVICIO WEB



                   // respuestaBizagi = servicioQuery.saveEntityAsString(actualizar);
                    respuestaBizagi = respuestaBizagi.Replace("\n", "");
                    respuestaBizagi = respuestaBizagi.Replace("\t", "");
                    respuestaBizagi = respuestaBizagi.Replace("\r", "");

                    //Escribir log CSV
                    Util.EscribirLog("Respuesta actualizar", "IngresarDatos", respuestaBizagi);
                    //Fin CSV
                }
                else
                {
                    throw new Exception("Número de caso sin especificicar");
                }                
            }
            catch (Exception ex)
            {
                //Escribir log CSV
                Util.EscribirLog("ERROR", "IngresarDatos", ex.Message);
                //Fin CSV
            }
            return (respuestaBizagi);
        }

        public ActionResult Index()
        {
            return View();
        }

    }
}
