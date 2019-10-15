<%@ Page Title="" Language="C#" MasterPageFile="~/Views/DisenoBootstrap3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <script src="<%: Url.Content("~/Styles/js/moment.js") %>"></script>
    <script src="<%: Url.Content("~/Styles/js/datatable.min.js") %>"></script>
    <link href="<%: Url.Content("~/Styles/css/datatable.min.css") %>" rel="stylesheet" />
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
    <link href="<%: Url.Content("~/Styles/css/custom.css") %>" rel="stylesheet" />
    <title>Entry Form</title>
    <script>
        $(document).ready(function () {
            $("#btnEnviar").click(function (e) {
                e.preventDefault();
                swal({
                    title: "Are you sure?",
                    text: "The data will be entered into the system.",
                    icon: "warning",
                    buttons: true,
                    successMode: true,
                    buttons: ["Cancel", "Confirm"],
                }).then((willDelete) =>
                    {
                    if (willDelete)
                    {                  
                        var md = $("#processing-modal");
                    md.modal("show");
                    $.ajax({
                        url: '<%: Url.Content("~/CrearProveedor/IngresarDatosIngles/") %>',
                        data: $("#formIndex").serialize(),
                        type: "POST",
                        success: function (data)
                        {
                            swal({
                            title: 'successful data entry',
                            text: 'The information has been uploaded to the system',
                            icon: 'success'
                            }).then(function () {
                                window.location.href = '<%: Url.Content("~/Home/SolicitudExitosa") %>';                     
                            });
                            if (data != "error") {
                            }
                            else {
                                md.modal("hide");
                                swal("Error entering information", "Contact an administrator", "error");
                            }
                        },
                        error: function () {
                            console.log("falló");
                            md.modal("hide");
                            swal("Data communication error", "Contact an administrator", "warning");
                        }
                        });
                    }
                    else
                    {
                        swal("Data entry has been canceled.");
                    }
                });                                                                                     
            });

            // Cambiar mensajes por defecto
            jQuery.extend(jQuery.validator.messages, {
                required: "This field is required.",
                remote: "This field needs correction.",
                email: "Enter a valid email.",
                url: "Enter a valid URL.",
                date: "Enter a valid date.",
                dateISO: "Enter a valid date.",
                number: "Enter a valid number.",
                digits: "Enter digits only.",
                creditcard: "Enter a valid credit card.",
                equalTo: "Repeat the same field.",
                accept: "Enter a file with a valid extension (PDF, DOC, DOCX, JPG o PNG).",
                extension: "Enter a file with a valid extension (PDF, DOC, DOCX, JPG o PNG).",
                maxlength: jQuery.validator.format("Enter up to {0} characters."),
                minlength: jQuery.validator.format("Enter minimum {0} characters."),
                rangelength: jQuery.validator.format("Enter a value in a range between {0} and {1}."),
                range: jQuery.validator.format("Enter a value between {0} and {1}."),
                max: jQuery.validator.format("Enter a value equal to or less than {0}."),
                min: jQuery.validator.format("Enter a value greater than or equal to {0}.")
            });

            //validar que se seleccione un tipo de proveedor
            $("#txtTipoProveedor").rules("add", {
                required: true,
                selectProveedor: true
            });

            //Validar que se seleccione un tipo de proveedor
            $.validator.addMethod("selectProveedor", function (value, element) {
                return value !== "0";
            }, "Select a Provider Type");

            //validar campos
             $("#formIndex").validate({
                    //ignore: ":disabled",
                    rules: {                        
                    txtRazonSocialProveedor: {
                        required: true
                      },
                      txtDocTributarioProveedor: {
                        required: true
                      },
                      txtGiroProveedor: {
                        required: true
                      },
                      txtSitioWebProveedor: {
                        required: true
                      },
                      txtRutProveedor: {
                        required: true
                      },
                      txtDivisaProveedor: {
                        required: true
                      },
                      txtCalleNumeroDirección: {
                          required: true
                      },
                      txtCodigoPostalDirección: {
                        required: true
                      },
                      txtComunaCiudadDirección: {
                        required: true
                      },
                      txtRegionDirección: {
                        required: true
                      },
                      txtNombreContacto: {
                        required: true
                      },                      
                      txtExtensionContacto: {
                        required: true
                      },                     
                      txtCargoContacto: {
                        required: true
                      },
                      txtCelularContacto: {
                        required: true
                      },
                      txtTelefonoContacto: {
                        required: true
                      },
                      txtCorreoContacto: {
                        required: true
                      },
                      txtCedulaRepresentante: {
                        required: true
                      },
                      txtRutEmpresaProveedora: {
                        required: true
                      },
                      txtCertificadoCuenta: {
                        required: true
                      },
                      txtEscrituraEmpresa: {
                        required: true
                      },
                      txtDicom: {
                        required: true
                      }
                  }
            });
            $("#txtCelularContacto, #txtTelefonoContacto, #txtCodigoPostalDirección").numeric("{ negative : false , decimalPlaces : 0 , decimal : ',' }");
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%--FILTROS--%>
    <h1 style="text-align:center;color:#393945;padding-bottom:15px">SUPPLIER CREATION REQUEST FORM</h1>
    <div style="text-align:center">
        <a href="<%: Url.Content("~/Home/Index") %>" style="font-size:30px">Spanish form</a>
    </div>
<form id="formIndex">
    <div class="panel panel-primary">
        <div class="panel-heading">Supplier data</div>
        <div class="panel-body">
            <div class="row">
                <fieldset class="form-group col-md-1">
                </fieldset>
                <fieldset class="form-group col-md-5">
                    <label for="txtLegalName">Legal name</label>
                    <input type="text" class="form-control" id="txtLegalName" name="txtLegalName" />
                </fieldset>
                <fieldset class="form-group col-md-5">
                    <label for="txtCurrency">Currency</label>
                    <input type="text" class="form-control" id="txtCurrency" name="txtCurrency" />
                </fieldset>                    
            </div>
            <div class="row">
                <fieldset class="form-group col-md-1">
                </fieldset>
                <fieldset class="form-group col-md-5">
                    <label for="txtIndustry">Industry</label>
                    <input type="text" class="form-control" id="txtIndustry" name="txtIndustry" />
                </fieldset>
                <fieldset class="form-group col-md-5">
                    <label for="txtWebsite">Website</label>
                    <input type="text" class="form-control" id="txtWebsite" name="txtWebsite" />
                </fieldset>                    
            </div>
            <div class="row">
                <fieldset class="form-group col-md-1">
                </fieldset>
                <fieldset class="form-group col-md-5">
                    <label for="txtTaxId">TAX ID</label>
                    <input type="text" class="form-control" id="txtTaxId" name="txtTaxId" />
                </fieldset>
                <fieldset class="form-group col-md-5">
                    <label for="txtTypeOfProvider">Type of provider</label>
                    <select name="txtTypeOfProvider" id="txtTypeOfProvider" class="form-control">
                        <option value="0">Select an option</option>
                        <%=ViewData["txtTipoProveedor"]%>
                    </select>
                </fieldset>                    
            </div>            
        </div>
    </div>

    <div class="panel panel-primary">
        <div class="panel-heading">Address</div>
        <div class="panel-body">
            <div class="row">
                <fieldset class="form-group col-md-1">
                </fieldset>
                <fieldset class="form-group col-md-5">
                    <label for="txtStreetAndNumber">Street and number</label>
                    <input type="text" class="form-control" id="txtStreetAndNumber" name="txtStreetAndNumber" />
                </fieldset>
                <fieldset class="form-group col-md-5">
                    <label for="txtCountry">Country</label>
                    <input type="text" class="form-control" id="txtCountry" name="txtCountry" />
                </fieldset>                    
            </div>
            <div class="row">
                <fieldset class="form-group col-md-1">
                </fieldset>
                <fieldset class="form-group col-md-5">
                    <label for="txtCity">City</label>
                    <input type="text" class="form-control" id="txtCity" name="txtCity" />
                </fieldset>
                <fieldset class="form-group col-md-5">
                    <label for="txtPostalCode">Postal code</label>
                    <input type="text" class="form-control" id="txtPostalCode" name="txtPostalCode" />
                </fieldset>                    
            </div>            
        </div>
    </div>
    
    <div class="panel panel-primary">
        <div class="panel-heading">Contact info</div>
        <div class="panel-body">
            <div class="row">
                <fieldset class="form-group col-md-1">
                </fieldset>
                <fieldset class="form-group col-md-5">
                    <label for="txtName">Name</label>
                    <input type="text" class="form-control" id="txtName" name="txtName" />
                </fieldset>
                <fieldset class="form-group col-md-5">
                    <label for="txtExtension">Extension</label>
                    <input type="text" class="form-control" id="txtExtension" name="txtExtension" />
                </fieldset>                    
            </div>
            <div class="row">
                <fieldset class="form-group col-md-1">
                </fieldset>
                <fieldset class="form-group col-md-5">
                    <label for="txtJobPosition">Job position</label>
                    <input type="text" class="form-control" id="txtJobPosition" name="txtJobPosition" />
                </fieldset>
                <fieldset class="form-group col-md-5">
                    <label for="txtMobile">Mobile</label>
                    <input type="text" class="form-control" id="txtMobile" name="txtMobile" />
                </fieldset>                    
            </div>      
            <div class="row">
                <fieldset class="form-group col-md-1">
                </fieldset>
                <fieldset class="form-group col-md-5">
                    <label for="txtPhone">Phone</label>
                    <input type="text" class="form-control" id="txtPhone" name="txtPhone" />
                </fieldset>
                <fieldset class="form-group col-md-5">
                    <label for="txtEmail">E-mail</label>
                    <input type="text" class="form-control" id="txtEmail" name="txtEmail" />
                </fieldset>                    
            </div>     
        </div>
    </div>

    <div class="panel panel-primary">
        <div class="panel-heading">Required documents to send</div>
        <div class="panel-body">
            <div class="row">
                <fieldset class="form-group col-md-1">
                </fieldset>
                <fieldset class="form-group col-md-5">
                    <label for="txtBusinessCertificate">Business registration certificate</label>
                    <input type="file" class="form-control" id="txtBusinessCertificate" name="txtBusinessCertificate" />
                </fieldset>
                <fieldset class="form-group col-md-5">
                    <label for="txtBankCertificate">Bank certificate form issued by your bank. This should indicate Tax ID and company legal name</label>
                    <input type="file" class="form-control" id="txtBankCertificate" name="txtBankCertificate" />
                </fieldset>                    
            </div>
            <div class="row">
                <fieldset class="form-group col-md-1">
                </fieldset>
                <fieldset class="form-group col-md-5">
                    <label for="txtDubAndBradseet">Dun & bradsteet or equivalent</label>
                    <input type="file" class="form-control" id="txtDubAndBradseet" name="txtDubAndBradseet" />
                </fieldset>
                <fieldset class="form-group col-md-5">
                    <label for="txtTaxIdCopy">Tax ID copy</label>
                    <input type="file" class="form-control" id="txtTaxIdCopy" name="txtTaxIdCopy" />
                </fieldset>                    
            </div>
        </div>
    </div>

    <div class="panel-body" style="text-align:right">           
        <fieldset class="form-group col-md-12">
            <div class="row">
                <label>If any doubt please contact: compraslogistica.chi@hdlao.com</label>               
            </div>
        </fieldset> 
    </div>

    <div class="panel-body" style="text-align:center">           
        <fieldset class="form-group col-md-12">
            <div class="row">
                <button type="button" class="btn btn-primary btn-lg" id="btnEnviar">Send</button>                 
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
                    <img src="../Styles/css/gif-load.gif" class="icon" />
                    <h3>Realizando ingreso de datos</h3>
                </div>
            </div>
        </div>
    </div>
</div>
</asp:Content>
