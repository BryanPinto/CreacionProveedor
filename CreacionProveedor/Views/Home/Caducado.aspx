<%@ Page Title="" Language="C#" MasterPageFile="~/Views/DisenoBootstrap3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <script src="<%: Url.Content("~/Styles/js/moment.js") %>"></script>
    <script src="<%: Url.Content("~/Styles/js/datatable.min.js") %>"></script>
    <link href="<%: Url.Content("~/Styles/css/datatable.min.css") %>" rel="stylesheet" />
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
    <link href="<%: Url.Content("~/Styles/css/custom.css") %>" rel="stylesheet" />
    <title>Página no disponible</title>
    <script>
        $(document).ready(function () {
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<img src="https://www.hunterdouglas.cl/cortinas/uploads/cl/logos/logo.png" alt="HunterDouglas" /><br />
    <br /> <br /> <br /> 
<h1 style="text-align:center;color:#393945;padding-bottom:15px;font-size:50px">PÁGINA NO DISPONIBLE</h1>
<h2 style="text-align:center;color:#393945;padding-bottom:16px">El tiempo de espera para la solicitud expiró. Cualquier duda contactarse con: compraslogistica.chi@hdlao.com</h2>
<div style="text-align:center">
    <img src="<%: Url.Content("~/Styles/image/reloj.png") %>"/>
</div>

</asp:Content>
