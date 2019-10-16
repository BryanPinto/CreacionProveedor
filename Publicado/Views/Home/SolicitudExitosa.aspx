<%@ Page Title="" Language="C#" MasterPageFile="~/Views/DisenoBootstrap3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <script>
        $(document).ready(function () {
            "<%= ViewData["UrlValida"] %>" = "0"        
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<img src="https://www.hunterdouglas.cl/cortinas/uploads/cl/logos/logo.png" alt="HunterDouglas" /><br />
    <br /> <br /> <br /> <br />
<h1 style="text-align:center;color:#393945;padding-bottom:15px;font-size:50px">SOLICITUD EXITOSA</h1>
<h2 style="text-align:center;color:#393945;padding-bottom:9px">Sus datos serán revisados por el encargado de la solicitud</h2>
<div style="text-align:center;padding-bottom:20px">
    <img src="<%: Url.Content("~/Styles/image/vistobueno.png") %>"/>
</div>


</asp:Content>
