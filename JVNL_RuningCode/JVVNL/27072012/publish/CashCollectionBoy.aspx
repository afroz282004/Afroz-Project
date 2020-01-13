<%@ Page Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="CashCollectionBoy.aspx.cs"
    Inherits="JVVNLWeb.CashCollectionBoy" Title="JVVNL - Cash Collection Boy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentplaceholder1" runat="Server">
    <link rel="stylesheet" href="css/demo_table_jui.css" />
    <link href="css/jquery-ui-1.7.2.custom.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="js/jquery.dataTables.js"></script>
    <script type="text/javascript">
        jQuery(document).ready(function() {
          var url = "CheckSession.aspx?hgUserName=" + document.getElementById("username").value;
         $(document).idleTimeout({ inactivity: 600000, noconfirm: 1000, sessionAlive: 600000, redirect_url: url });
            PopulateCounter();
        });

        function isAlpha(keyCode) {
            return ((keyCode >= 65 && keyCode <= 90) || keyCode == 8 || keyCode == 32)
        }            
    </script>
      
    <div class="grid_12">
        <div class="box">
            <h2>
                Cash Collection Boy Details <span class="l"></span><span class="r"></span>
            </h2>
            <div class="block">
                <div class="form_place" id="cashDiv">
                <input type="hidden" id="username" value="<% Response.Write(Session["username"].ToString()); %>" />
                    <table class="fieldtable">
                        <tr>
                            <td>
                                Cash Counter Name:
                            </td>
                            <td>
                                <select id='counter'>
                                    <option value="0">-------Select--------</option>
                                </select>
                            </td>
                            <td>
                                Cash Collection Boy Name:
                            </td>
                            <td>
                                <input type="text" id="boyname" name="boyname" size="15" onkeydown = "return isAlpha(event.keyCode);"/>
                            </td>
                            <td>
                                Date:
                            </td>
                            <td >
                                <input type="text" id="cdate" name="cdate" size="15" value="<% Response.Write( DateTime.Today.ToString("dd/MM/yyyy")); %>" readonly=readonly/>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <button type="button" id="save" class="button green small submit alignleft">
                                    SAVE</button>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <div id="msg">
    </div>

    <script type="text/javascript">
        $("#save").click(function() {
            var counterid = document.getElementById("counter").value;
            var boyname = document.getElementById("boyname").value;
            var cdate = document.getElementById("cdate").value;
            
            if (counterid == 0) 
            {
                alert('Please select the counter name');
            }
            else if (boyname.trim() == "") 
            {
                alert('Please enter the boy name');
            }
            else 
            {                
                $.post("handler/CashCollectionBoy.ashx?type=insert&counterid=" + counterid + "&boyname=" + boyname + "&cdate=" + cdate, $("#form").serialize(), SaveData);
            }
        });

        function SaveData(result) {
            alert(result);
            ClearControls();
        }

        function ClearControls() {
            document.getElementById("counter").value = 0;
            document.getElementById("boyname").value = "";
        }

        function PopulateCounter()
	    {
	        $.post("handler/counter.ashx?type=populate", $("#form").serialize(), ShowCounter);
	    }

	    function ShowCounter(result)
	    {
	        if (result !="")
            {
                var options= result.split("$");
                var addoption,addoption1;
                var i;
                for(i=0; i <options.length-1; i++)
                {
                    var splitoption = options[i].split("#");     
                    addoption = $('<option></option>').attr("value", splitoption[0]).text(splitoption[1]);
                    $("#counter").append(addoption); 
                }   
            }
	    }
</script>
</asp:Content>
