<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="MonthllyRFDReportRPT.aspx.cs" Inherits="KVICWeb.MonthllyRFDReportRPT" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentplaceholder1" runat="server">

<%--    <table style="width: 755px"><tr><td align="center">
     <asp:Button ID="btn_back" runat="server" Text="Back" Height="22px" Width="66px" />
     </td></tr></table>--%>
    <br />
    <table style="width: 767px; height: 8px;" align="center">
    <tr>
    <td class="style1" ></td>
    <td>
  
            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server"
                AutoDataBind="true" DisplayGroupTree="False" EnableDatabaseLogonPrompt="False" 
        EnableParameterPrompt="False" ReuseParameterValuesOnRefresh="True" 
            BestFitPage="True"  />
    </td>
    </tr>
    </table>
    
</asp:Content>
