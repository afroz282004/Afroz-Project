<%@ Page Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="sdo.aspx.cs" Inherits="JVVNLWeb.sdo" Title="SDO" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentplaceholder1" runat="server">

    <script type="text/javascript" src="js/jquery.dataTables.js"></script>
<link rel="stylesheet" href="css/demo_table_jui.css" />
    <link href="css/jquery-ui-1.7.2.custom.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    
			jQuery(document).ready(function() {
			  var url = "CheckSession.aspx?hgUserName=" + document.getElementById("username").value;
         $(document).idleTimeout({ inactivity: 600000, noconfirm: 0, sessionAlive: 0, redirect_url: url });
                PopulateSDO();
                
			} );
			function NoSpace(e)
		    {
		        
		        var keycode=e.charCode? e.charCode : e.keyCode;
		        if (keycode == 32 || keycode==17)
		        {
		            return false;
		        }
		    }
		    function OnlyNumber(e)
		    {
		        var keycode=e.charCode? e.charCode : e.keyCode;
		        if ((keycode < 48 || keycode >57) && keycode  != 8 && keycode  != 9  )
		        {
		            return false;
		        }
		    }
		    function OnlyAlpha(e)
		    {
		        
		        var keycode=e.charCode? e.charCode : e.keyCode;
		        if ((keycode >=32 && keycode<= 57) || keycode==64 || (keycode >=94 && keycode <=96)|| (keycode >=123 && keycode <=127) )
		        {
		            return false;
		        }
		    }
		    
		</script>
<div class="grid_12">
						<div class="box">
							<h2>
								SDO Search
								<span class="l"></span><span class="r"></span>
							</h2>
							<div class="block">
								<div class="form_place"  id="form1">
								<input type="hidden" id="username" value="<% Response.Write(Session["username"].ToString()); %>" />
									<table class="fieldtable">
									    <tr>
									        <td>SDO Code:</td>
									        <td><input   type="text" id="sdocode" name="sdocode" size="20px" maxlength="15" onkeypress ="return OnlyNumber(event);" /></td>
									        <td>SDO Name:</td>
									        <td><input   type="text" id="sdoname" name="sdoname" size="20px" maxlength="50" onkeypress ="return NoSpace(event);" /></td>
									        <td><button type="button" class="button red small submit alignleft" id="search">SEARCH</button>
									        <button type="button" class="button blue small submit alignleft" id="new">NEW</button></td>
									    </tr>
									</table>
								</div>
							</div>
						</div>
					</div>
					<div id="sdo">
					    <table class="fieldtable">
					        <tr>
					            <td>SDO Code</td>
					            <td><input type="text" name="ssdocode" id="ssdocode" size="25px" maxlength="15" onkeypress ="return OnlyNumber(event);"   /></td>
					        </tr>
					        <tr>
					            <td>SDO Name</td>
					            <td><input type="text" name="ssdoname" id="ssdoname" size="35px" maxlength="15" onkeypress ="return NoSpace(event);"   /></td>
					            </tr>
					        <tr><td colspan="2" align="center"><button type="button" class="button green small" id="save">SAVE</button>
					        <button type="button" class="button orange small" id="cancel">CANCEL</button>
					        </td></tr>
					    </table>
					</div>
					<div class="grid_12">
						<div class="box">
							<h2>
								SDO Search Result
								<span class="l"></span><span class="r"></span>
							</h2>
							<div class="block">
								<div class="block_in" id="resulttable">
									<!-- BEGIN TABLE -->
									<table class="table display" id="basictable">
										<thead>
    									<tr>
											<th>SDO Code</th>
												<th>SDO Name</th>
												<th></th>
											</tr>

										</thead>
										<tbody id="tbody">
										</tbody>
									</table>
									<!-- END TABLE -->
								</div>
							</div>

						</div>
					</div> <!-- End of .grid_12 -->
					<script type="text/javascript">
					    $("#new").click(function(){
					    $("#sdo").dialog('open');
					});
					$("#save").click(function(){
					    var sdocode=document.getElementById("ssdocode").value ;
					    var sdoname=document.getElementById("ssdoname").value ;
					    if (sdocode=="")
					    {
					        alert("Enter SDO Code");
					        document.getElementById("ssdocode").focus();
					        return ;  
					    }
					    if (sdoname=="")
					    {
					        alert("Enter SDO Name");
					        document.getElementById("ssdoname").focus();
					        return ;  
					    }
					    $.post("handler/sdo.ashx?type=insert&sdocode="+ sdocode + "&sdoname=" + sdoname   ,$("#form").serialize(),SaveData);
					 });
					function SaveData(result)
					{
					    alert(result );
					    if (result.toString().indexOf ("already")>=0)
					        document.getElementById("ssdocode").focus();
					    else
					        ClearControls(); 
					}
					function ClearControls()
					{
					    document.getElementById("ssdocode").value="";
					    document.getElementById("ssdoname").value="" ;
					}
					  $("#cancel").click(function(){
					    ClearControls();
					  });
					  $("#search").click(function(){
					    location.reload();
					    
					});
					function PopulateSDO()
					{
					    var sdocode=document.getElementById("sdocode").value ;
					    var sdoname=document.getElementById("sdoname").value ;
					    $.post("handler/sdo.ashx?type=search&sdocode="+ sdocode + "&sdoname=" + sdoname   ,$("#form").serialize(),SearchResult);
					}
					function SearchResult(result)
					{
					    $("#basictable > tbody").html("");
					    $("#basictable").append(result );
					    oTable = $('#basictable').dataTable({
					    "bJQueryUI": true,
					    "sPaginationType": "full_numbers"
				        });
				    }
				    function DeleteSDO(id)
				    {
				        if (confirm("Are you sure you want to delete " + id))
					    {
					        $.post("handler/sdo.ashx?type=delete&sdocode=" + id  ,$("#form").serialize(),MessageAlertDelete);
					    }
				    }
				    function MessageAlertDelete(result)
					{
					    alert(result);
					    location.reload();					    
					}
					$("#sdo").dialog({
                    autoOpen: false,
                    title: 'SDO Details',
                    modal: true,
                    autoresize:true ,
                    width:450,
                    height:150
                }); 
					</script>
					
</asp:Content>
