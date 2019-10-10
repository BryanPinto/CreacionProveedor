<%@ Page Title="" Language="C#" MasterPageFile="~/Views/DisenoBootstrap3.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <script src="<%: Url.Content("~/Styles/js/moment.js") %>"></script>
    <script src="<%: Url.Content("~/Styles/js/datatable.min.js") %>"></script>
    <link href="<%: Url.Content("~/Styles/css/datatable.min.css") %>" rel="stylesheet" />
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
    <link href="<%: Url.Content("~/Styles/css/custom.css") %>" rel="stylesheet" />
    <title>Link caducado</title>
    <script>
        $(document).ready(function () {
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h1 style="text-align:center;color:#393945;padding-bottom:15px">EL LINK A CADUCADO</h1>

</asp:Content>
