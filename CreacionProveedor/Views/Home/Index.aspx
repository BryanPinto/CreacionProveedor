<%@ Page Title="" Language="C#" MasterPageFile="~/Views/DisenoBootstrap3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <script src="<%: Url.Content("~/Styles/js/moment.js") %>"></script>
    <script src="<%: Url.Content("~/Styles/js/datatable.min.js") %>"></script>
    <link href="<%: Url.Content("~/Styles/css/datatable.min.css") %>" rel="stylesheet" />
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
    <link href="<%: Url.Content("~/Styles/css/custom.css") %>" rel="stylesheet" />
    <title>Formulario de ingreso</title>
    <script>
        $(document).ready(function () {
           <%--console.log("urlvalida:"+"<%= ViewData["UrlValida"] %>");
            if ("<%= ViewData["UrlValida"] %>" == "1") {
                console.log("2");
                $(function () {
                    console.log("3");
                    $.post('<%: Url.Content("~/Home/UrlValida/") %>',
                        { value: '0' }, function (data) {
                            console.log("4");
                            console.log("Estado " + data.estado);
                            //cambiar valor
                            sessionStorage.setItem('UrlValida', data.estado);
                            console.log("5");
                        });
                });
            }
            else {
                console.log("6");
                window.location.href = '<%: Url.Content("~/Home/Caducado") %>'; 
            }--%>

            <%--$("#btnEnviar").click(function (e) {
                e.preventDefault();
                swal({
                    title: "¿Estás seguro?",
                    text: "Se ingresarán los datos al sistema.",
                    icon: "warning",
                    buttons: true,
                    successMode: true,
                    buttons: ["Cancelar", "Confirmar"],
                }).then((willDelete) =>
                    {
                    if (willDelete)
                    {                  
                        var md = $("#processing-modal");
                        md.modal("show");
                    $.ajax({
                        url: '<%: Url.Content("~/CrearProveedor/IngresarDatos/") %>',
                        data: $("#formIndex").serialize(),
                        type: "POST",
                        enctype: "multipart/form-data",
                        contentType: false,
                        cache: false,
                        processData:false,
                        success: function (data)
                        {
                            md.modal("hide");
                            swal({
                            title: 'Ingreso exitoso',
                            text: 'La información ha sido cargada al sistema',
                            icon: 'success'
                            }).then(function () {
                                window.location.href = '<%: Url.Content("~/Home/SolicitudExitosa") %>';                     
                            });
                            if (data != "error") {
                            }
                            else {
                                md.modal("hide");
                                swal("Error al ingresar la información", "Contacte a un administrador", "error");
                            }
                        },
                        error: function () {
                            md.modal("hide");
                            swal("Error de comunicación de datos", "Contacte a un administrador", "warning");
                        }
                        });
                    }
                    else
                    {
                        swal("Se ha cancelado el ingreso de datos.");
                    }
                });                                                                                     
            });--%>

            //// Cambiar mensajes por defecto
            //jQuery.extend(jQuery.validator.messages, {
            //    required: "Este campo es requerido.",
            //    remote: "Este campo necesita corrección.",
            //    email: "Ingrese un correo válido.",
            //    url: "Ingrese una URL válida.",
            //    date: "Ingrese una fecha válida.",
            //    dateISO: "Ingrese una fecha válida.",
            //    number: "Ingrese un número válido.",
            //    digits: "Ingrese solo dígitos.",
            //    creditcard: "Ingrese un tarjeta de crédito válida.",
            //    equalTo: "Repita el mismo campo.",
            //    accept: "Ingrese un archivo con una extensión válida (PDF, DOC, DOCX, JPG o PNG).",
            //    extension: "Ingrese un archivo con una extensión válida (PDF, DOC, DOCX, JPG o PNG).",
            //    maxlength: jQuery.validator.format("Ingrese hasta {0} caracteres."),
            //    minlength: jQuery.validator.format("Ingrese mínimo {0} caracteres."),
            //    rangelength: jQuery.validator.format("Ingrese un valor en un rango entre {0} y {1}."),
            //    range: jQuery.validator.format("Ingrese un valor entre {0} y {1}."),
            //    max: jQuery.validator.format("Ingrese un valor igual o menor a {0}."),
            //    min: jQuery.validator.format("Ingrese un valor mayor o igual a {0}.")
            //});

            ////validar que se seleccione un tipo de proveedor
            //$("#txtTipoProveedor").rules("add", {
            //    required: true,
            //    selectProveedor: true
            //});

            ////Validar que se seleccione un tipo de proveedor
            //$.validator.addMethod("selectProveedor", function (value, element) {
            //    return value !== "0";
            //}, "Seleccione un Tipo de proveedor");

            ////validar campos
            // $("#formIndex").validate({
            //        //ignore: ":disabled",
            //        rules: {                        
            //        txtRazonSocialProveedor: {
            //            required: true
            //          },
            //          txtDocTributarioProveedor: {
            //            required: true
            //          },
            //          txtGiroProveedor: {
            //            required: true
            //          },
            //          txtSitioWebProveedor: {
            //            required: true
            //          },
            //          txtRutProveedor: {
            //            required: true
            //          },
            //          txtDivisaProveedor: {
            //            required: true
            //          },
            //          txtCalleNumeroDirección: {
            //              required: true
            //          },
            //          txtCodigoPostalDirección: {
            //            required: true
            //          },
            //          txtComunaCiudadDirección: {
            //            required: true
            //          },
            //          txtRegionDirección: {
            //            required: true
            //          },
            //          txtNombreContacto: {
            //            required: true
            //          },                      
            //          txtExtensionContacto: {
            //            required: true
            //          },                     
            //          txtCargoContacto: {
            //            required: true
            //          },
            //          txtCelularContacto: {
            //            required: true
            //          },
            //          txtTelefonoContacto: {
            //            required: true
            //          },
            //          txtCorreoContacto: {
            //            required: true
            //          },
            //          txtCedulaRepresentante: {
            //            required: true
            //          },
            //          txtRutEmpresaProveedora: {
            //            required: true
            //          },
            //          txtCertificadoCuenta: {
            //            required: true
            //          },
            //          txtEscrituraEmpresa: {
            //            required: true
            //          },
            //          txtDicom: {
            //            required: true
            //          }
            //      }
            //});
            //$("#txtCelularContacto", "#txtTelefonoContacto", "#txtCodigoPostalDirección").numeric("{ negative : false , decimalPlaces : 0 , decimal : ',' }");
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%--FILTROS--%>
    <img src="https://www.hunterdouglas.cl/cortinas/uploads/cl/logos/logo.png" alt="HunterDouglas" />
    <h1 style="text-align:center;color:#393945;padding-bottom:15px">FICHA SOLICITUD DE CREACIÓN DE PROVEEDOR</h1>
    <div style="text-align:center">
        <a href="<%: Url.Content("~/Home/IndexEnglish") %>" style="font-size:30px">Formulario ingles</a>
    </div>
<form id="formIndex" enctype="multipart/form-data" method="post" action="<%: Url.Content("~/CrearProveedor/IngresarDatos/") %>">
    <div class="panel panel-primary">
        <div class="panel-heading">Datos generales del proveedor</div>
        <div class="panel-body">
            <div class="row">
                <fieldset class="form-group col-md-1">
                </fieldset>
                <fieldset class="form-group col-md-5">
                    <label for="txtRazonSocialProveedor">Razón social</label>
                    <input type="text" class="form-control" id="txtRazonSocialProveedor" name="txtRazonSocialProveedor" />
                </fieldset>
                <fieldset class="form-group col-md-5">
                    <label for="txtDocTributarioProveedor">Documento tributario</label>
                    <input type="text" class="form-control" id="txtDocTributarioProveedor" name="txtDocTributarioProveedor" />
                </fieldset>                    
            </div>
            <div class="row">
                <fieldset class="form-group col-md-1">
                </fieldset>
                <fieldset class="form-group col-md-5">
                    <label for="txtGiroProveedor">Giro</label>
                    <input type="text" class="form-control" id="txtGiroProveedor" name="txtGiroProveedor" />
                </fieldset>
                <fieldset class="form-group col-md-5">
                    <label for="txtSitioWebProveedor">Sitio web</label>
                    <input type="text" class="form-control" id="txtSitioWebProveedor" name="txtSitioWebProveedor" />
                </fieldset>                    
            </div>
            <div class="row">
                <fieldset class="form-group col-md-1">
                </fieldset>
                <fieldset class="form-group col-md-5">
                    <label for="txtRutProveedor">RUT</label>
                    <input type="text" class="form-control" id="txtRutProveedor" name="txtRutProveedor" />
                </fieldset>
                    <fieldset class="form-group col-md-5">
                    <label for="txtMoneda">Moneda</label>
                    <select name="txtMoneda" id="txtMoneda" class="form-control">
                        <option value="0">Seleccione opción</option>
                        <%=ViewData["txtMoneda"]%>
                    </select>
                </fieldset>                     
            </div>
            <%--<div class="row">
                <fieldset class="form-group col-md-1">
                </fieldset>
                <fieldset class="form-group col-md-5">
                    <label for="txtMoneda">Moneda</label>
                    <select name="txtMoneda" id="txtMoneda" class="form-control">
                        <option value="0">Seleccione opción</option>
                        <%=ViewData["txtMoneda"]%>
                    </select>
                </fieldset>                   
            </div>--%>
        </div>
    </div>

    <div class="panel panel-primary">
        <div class="panel-heading">Dirección</div>
        <div class="panel-body">
            <div class="row">
                <fieldset class="form-group col-md-1">
                </fieldset>
                <fieldset class="form-group col-md-5">
                    <label for="txtCalleNumeroDirección">Calle y número</label>
                    <input type="text" class="form-control" id="txtCalleNumeroDirección" name="txtCalleNumeroDirección" />
                </fieldset>
                <fieldset class="form-group col-md-5">
                    <label for="txtCodigoPostalDirección">Código postal</label>
                    <input type="text" class="form-control" id="txtCodigoPostalDirección" name="txtCodigoPostalDirección" />
                </fieldset>                    
            </div>
            <div class="row">
                <fieldset class="form-group col-md-1">
                </fieldset>
                <fieldset class="form-group col-md-5">
                    <label for="txtCiudad">Ciudad</label>
                    <input type="text" class="form-control" id="txtCiudad" name="txtCiudad" />
                </fieldset>
                <fieldset class="form-group col-md-5">
                    <label for="txtRegionDirección">Región</label>
                    <input type="text" class="form-control" id="txtRegionDirección" name="txtRegionDirección" />
                </fieldset>                    
            </div>            
        </div>
    </div>
    
    <div class="panel panel-primary">
        <div class="panel-heading">Información de contacto</div>
        <div class="panel-body">
            <div class="row">
                <fieldset class="form-group col-md-1">
                </fieldset>
                <fieldset class="form-group col-md-5">
                    <label for="txtNombreContacto">Nombre</label>
                    <input type="text" class="form-control" id="txtNombreContacto" name="txtNombreContacto" />
                </fieldset>
                <fieldset class="form-group col-md-5">
                    <label for="txtExtensionContacto">Extensión</label>
                    <input type="text" class="form-control" id="txtExtensionContacto" name="txtExtensionContacto" />
                </fieldset>                    
            </div>
            <div class="row">
                <fieldset class="form-group col-md-1">
                </fieldset>
                <fieldset class="form-group col-md-5">
                    <label for="txtCargoContacto">Cargo</label>
                    <input type="text" class="form-control" id="txtCargoContacto" name="txtCargoContacto" />
                </fieldset>
                <fieldset class="form-group col-md-5">
                    <label for="txtCelularContacto">Celular</label>
                    <input type="text" class="form-control" id="txtCelularContacto" name="txtCelularContacto" placeholder="+56 9 23940015"/>
                </fieldset>                    
            </div>      
            <div class="row">
                <fieldset class="form-group col-md-1">
                </fieldset>
                <fieldset class="form-group col-md-5">
                    <label for="txtTelefonoContacto">Teléfono fijo</label>
                    <input type="text" class="form-control" id="txtTelefonoContacto" name="txtTelefonoContacto"  placeholder="+56 22 3940015"/>
                </fieldset>
                <fieldset class="form-group col-md-5">
                    <label for="txtCorreoContacto">Correo eléctronico</label>
                    <input type="text" class="form-control" id="txtCorreoContacto" name="txtCorreoContacto" />
                </fieldset>                    
            </div>     
        </div>
    </div>

    <div class="panel panel-primary">
        <div class="panel-heading">Documentación adicional a enviar</div>
        <div class="panel-body">
            <div class="row">
                <fieldset class="form-group col-md-1">
                </fieldset>
                <fieldset class="form-group col-md-5">
                    <label for="txtCedulaRepresentante">Cédula de identidad representante legal</label>
                    <input type="file" class="form-control" id="txtCedulaRepresentante" name="txtCedulaRepresentante" />
                </fieldset>
                <fieldset class="form-group col-md-5">
                    <label for="txtRutEmpresaProveedora">RUT de la empresa proveedora</label>
                    <input type="file" class="form-control" id="txtRutEmpresaProveedora" name="txtRutEmpresaProveedora" />
                </fieldset>                    
            </div>
            <div class="row">
                <fieldset class="form-group col-md-1">
                </fieldset>
                <fieldset class="form-group col-md-5">
                    <label for="txtCertificadoCuenta">Certificado de cuenta corriente</label>
                    <input type="file" class="form-control" id="txtCertificadoCuenta" name="txtCertificadoCuenta" />
                </fieldset>
                <fieldset class="form-group col-md-5">
                    <label for="txtEscrituraEmpresa">Escritura de empresa</label>
                    <input type="file" class="form-control" id="txtEscrituraEmpresa" name="txtEscrituraEmpresa" />
                </fieldset>                    
            </div>
            <div class="row">
                <fieldset class="form-group col-md-1">
                </fieldset>
                <fieldset class="form-group col-md-5">
                    <label for="txtDicom">DICOM</label>
                    <input type="file" class="form-control" id="txtDicom" name="txtDicom" />
                </fieldset>                   
            </div>
        </div>
    </div>

    <div class="panel-body" style="text-align:right">           
        <fieldset class="form-group col-md-12">
            <div class="row">
                <label>Cualquier duda contactarse con: compraslogistica.chi@hdlao.com</label>               
            </div>
        </fieldset> 
    </div>

    <div class="panel-body" style="text-align:center">           
        <fieldset class="form-group col-md-12">
            <div class="row">
                <%--<button type="button" class="btn btn-primary btn-lg" id="btnEnviar">Enviar</button>                 --%>
                <input type="submit" class="btn btn-primary btn-lg" id="btnEnviar" value="Enviar"/>     
            </div>
        </fieldset> 
    </div>

</form>

<%--modal de progreso--%>
<div class="modal modal-static fade" id="processing-modal" role="dialog" aria-hidden="true" style="vertical-align:middle">
    <div class="modal-dialog" style="vertical-align:middle">
        <div class="modal-content">
            <div class="modal-body">
                <div class="text-center">
                    <img src="../Styles/css/gif-load.gif" class="icon" id="gif" />
                    <h3>Realizando ingreso de datos</h3>
                </div>
            </div>
        </div>
    </div>
</div>
</asp:Content>
