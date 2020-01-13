<%@ Page Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="paymenttype.aspx.cs" Inherits="JVVNLWeb.paymenttype" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentplaceholder1" runat="server">

    <script type="text/javascript" src="js/jquery.dataTables.js"></script>
<link rel="stylesheet" href="css/demo_table_jui.css" />
    <link href="css/jquery-ui-1.7.2.custom.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    
			jQuery(document).ready(function() {
			 var url = "CheckSession.aspx?hgUserName=" + document.getElementById("username").value;
         $(document).idleTimeout({ inactivity: 600000, noconfirm: 0, sessionAlive: 0, redirect_url: url });
                PopulatePaymentType();
                
			} );
			function NoSpace(e)
		    {
		        
		        var keycode=e.charCode? e.charCode : e.keyCode;
		        if (keycode == 32 || keycode==17)
		        {
		            return false;
		        }
		    }
		</script>
<div class="grid_12">
						<div class="box">
							<h2>
								PaymentType Search
								<span class="l"></span><span class="r"></span>
							</h2>
							<div class="block">
								<div class="form_place"  id="form1">
								<input type="hidden" id="username" value="<% Response.Write(Session["username"].ToString()); %>" />
									<table class="fieldtable" style="width:50%">
									    <tr>
									        <td>Payment Type:</td>
									        <td><input type="text" id="PaymentTypename" name="PaymentTypename" size="20px" maxlength="50" onkeypress ="return NoSpace(event);"/> </td>
									        <td><button type="button" class="button red small submit alignleft" id="search">SEARCH</button>
									        <button type="button" class="button blue small submit alignleft" id="new">NEW</button></td>
									    </tr>
									</table>
								</div>
							</div>
						</div>
					</div>
					<div id="PaymentType">
					    <table class="fieldtable">
					        
					        <tr>
					            <td>Payment Type</td>
					            <td><input type="text" name="sPaymentType" id="sPaymentType" size="35px"  maxlength="50" onkeypress ="return NoSpace(event);" /></td>
					            </tr>
					        <tr><td colspan="2" align="center"><button type="button" class="button green small" id="save">SAVE</button>
					        <button type="button" class="button orange small" id="cancel">CANCEL</button>
					        </td></tr>
					    </table>
					</div>
					<div class="grid_12">
						<div class="box">
							<h2>
								Payment Type Search Result
								<span class="l"></span><span class="r"></span>
							</h2>
							<div class="block">
								<div class="block_in" id="resulttable">
									<!-- BEGIN TABLE -->
									<table class="table display" id="basictable">
										<thead>
    									<tr>
												<th>Payment Type</th>
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
					    $("#PaymentType").dialog('open');
					});
					$("#save").click(function(){
					    var PaymentType=document.getElementById("sPaymentType").value ;
					     
					    $.post("handler/paymenttype.ashx?type=insert&paymenttype=" + PaymentType   ,$("#form").serialize(),SaveData);
					 });
					function SaveData(result)
					{
					    alert(result );
					    if (result.toString().indexOf ("already")>=0)
					        document.getElementById("sPaymentType").focus();
					    else
					        ClearControls(); 
					}
					function ClearControls()
					{
					    
					    document.getElementById("sPaymentType").value="" ;
					}
					  $("#cancel").click(function(){
					    ClearControls();
					  });
					  $("#search").click(function(){
					    location.reload();
					    
					});
					function PopulatePaymentType()
					{
					    var PaymentTypename=document.getElementById("PaymentTypename").value ;
					    $.post("handler/paymenttype.ashx?type=search&paymenttype=" + PaymentTypename   ,$("#form").serialize(),SearchResult);
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
				    function DeletePaymentType(id)
				    {
				        var data=id.toString().split('#'); 
				        if (confirm("Are you sure you want to delete " + data[1]))
					    {
					        $.post("handler/paymenttype.ashx?type=delete&paymentid=" + data[0] ,$("#form").serialize(),MessageAlertDelete);
					    }
				    }
				    function MessageAlertDelete(result)
					{
					    alert(result);
					    location.reload();					    
					}
					$("#PaymentType").dialog({
                    autoOpen: false,
                    title: 'PaymentType Details',
                    modal: true,
                    autoresize:true ,
                    width:450,
                    height:110
                }); 
					</script>
</asp:Content>
