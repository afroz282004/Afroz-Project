<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="MonthlyRFDReport.aspx.cs" Inherits="KVICWeb.MonthlyRFDReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentplaceholder1" runat="server">

<script type="text/javascript">
    jQuery(document).ready(function() {

//        PopulateCluster();
        $("#ctl00_contentplaceholder1_txtdate").datepicker({ dateFormat: 'mm/dd/yy' });

      
    });
</script>

 
 
 <div id="techagencydiv">
        <table class="fieldtable">
            <tr>
                <td>
                   Cluster Name:
                </td>
                <td>
                   <%-- <select id="cluster" name="cluster">
                        <option value="select">--------Select----------</option>
                    </select>--%>
                    <asp:DropDownList ID="cluster" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Date:
                </td>
                <td>
                    <%--<input type="text" name="date" id="date"  />&nbsp;--%>
                    <asp:TextBox ID="txtdate" runat="server"></asp:TextBox>
                </td>
            </tr>
            
            <tr>
                <td>
                    
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            
            <tr>
                <td colspan="2" align="center">
                        <asp:Button ID="save" runat="server" class="button green small" Text="Show Report" 
                        onclick="save_Click"/>
                        <asp:Button ID="cancel" runat="server" class="button orange small" Text="Cancel"/>
                </td>
            </tr>
        </table>
    </div>
    
    
    <script type="text/javascript">
        function PopulateCluster() 
        {        
            $.post("handler/clusters.ashx?type=PoppulateCluster", $("#form").serialize(), ShowCluster);
        }

        function ShowCluster(result) {
            if (result != "") {
               
                var options = result.split("$");
                alert(options);
                var addoption, addoption1;
                var i;
                for (i = 0; i < options.length - 1; i++) {
                    var splitoption = options[i].split("#");
                    addoption = $('<option></option>').attr("value", splitoption[0]).text(splitoption[1]);
                    $("#cluster").append(addoption);
                }
            }
        }


        $("#save").click(function() {
        var cluster = document.getElementById("cluster").value;
        var date = document.getElementById("date").value;


            if (date == "") {
                alert("Enter Date");
                document.getElementById("date").focus();
                return;
            }
            if (cluster == "select" || cluster == "0") {
                alert("Enter Cluster");
                document.getElementById("cluster").focus();
                return;
            }
            $.post("handler/MonthlyRFDReport.ashx?type=Report&cluster=" + cluster + "&date=" + date , $("#form").serialize());
        });
        
 </script>
</asp:Content>
