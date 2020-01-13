<%@ Page Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="bank.aspx.cs" Inherits="JVVNLWeb.bank" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentplaceholder1" runat="server">

    <link href="css/jquery-ui-1.7.2.custom.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    
			jQuery(document).ready(function() {
			  var url = "CheckSession.aspx?hgUserName=" + document.getElementById("username").value;
         $(document).idleTimeout({ inactivity: 600000, noconfirm: 0, sessionAlive: 0, redirect_url: url });
                PopulateBank();
                
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
		    function OnlyAlphawithspace(e)
		    {
		        
		        var keycode=e.charCode? e.charCode : e.keyCode;
		        if ((keycode >32 && keycode<= 57) || keycode==64 || (keycode >=94 && keycode <=96)|| (keycode >=123 && keycode <=127) )
		        {
		            return false;
		        }
		    }
		</script>
<div class="grid_12">
						<div class="box">
							<h2>
								Bank Search
								<span class="l"></span><span class="r"></span>
							</h2>
							<div class="block">
								<div class="form_place"  id="form1">
									<table class="fieldtable">
									    <tr>
									        <td>Bank Code:</td>
									        <td><input   type="text" id="bankcode" name="bankcode" size="20px" maxlength="15" onkeypress ="return OnlyNumber(event);" /></td>
									        <td>Bank Name:</td>
									        <td><input   type="text" id="bankname" name="bankname" size="20px" maxlength="50" onkeypress ="return NoSpace(event);"  /></td>
									        <td><button type="button" class="button red small submit alignleft" id="search">SEARCH</button>
									        <button type="button" class="button blue small submit alignleft" id="new">NEW</button></td>
									    </tr>
									</table>
								</div>
							</div>
						</div>
					</div>
					<div id="bank">
					    <table class="fieldtable">
					        <tr>
					            <td>Bank Code</td>
					            <td><input type="text" name="sbankcode" id="sbankcode" size="25px" maxlength="15" onkeypress ="return OnlyAlphawithspace(event);"  /></td>
					        </tr>
					        <tr>
					            <td>Bank Name</td>
					            <td><input type="text" name="sbankname" id="sbankname" size="35px" maxlength="50" onkeypress ="return NoSpace(event);"  /></td>
					            </tr>
					        <tr><td colspan="2" align="center"><button type="button" class="button green small" id="save">SAVE</button>
					        <button type="button" class="button orange small" id="cancel">CANCEL</button>
					        </td></tr>
					    </table>
					</div>
					<div class="grid_12">
						<div class="box">
							<h2>
								Bank Search Result
								<span class="l"></span><span class="r"></span>
							</h2>
							<div class="block">
								<div class="block_in" id="resulttable">
									<!-- BEGIN TABLE -->
									<table class="table display" id="basictable">
										<thead>
    									<tr>
											<th>Bank Code</th>
												<th>Bank Name</th>
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
					    $("#bank").dialog('open');
					});
					$("#save").click(function(){
					    var bankcode=document.getElementById("sbankcode").value ;
					    var bankname=document.getElementById("sbankname").value ;
					    if (bankcode=="")
					    {
					        alert("Enter Bank Code");
					        document.getElementById("sbankcode").focus();
					        return ;  
					    }
					    if (bankname=="")
					    {
					        alert("Enter Bank Name");
					        document.getElementById("sbankname").focus();
					        return ;  
					    }
					    $.post("handler/bank.ashx?type=insert&bankcode="+ bankcode + "&bankname=" + bankname   ,$("#form").serialize(),SaveData);
					 });
					function SaveData(result)
					{
					    alert(result );
					    if (result.toString().indexOf ("already")>=0)
					        document.getElementById("sbankcode").focus();
					    else
					    ClearControls(); 
					}
					function ClearControls()
					{
					    document.getElementById("sbankcode").value="";
					    document.getElementById("sbankname").value="" ;
					}
					  $("#cancel").click(function(){
					    ClearControls();
					  });
					  $("#search").click(function(){
					    location.reload();
					    
					});
					function PopulateBank()
					{
					    var bankcode=document.getElementById("bankcode").value ;
					    var bankname=document.getElementById("bankname").value ;
					    $.post("handler/bank.ashx?type=search&bankcode="+ bankcode + "&bankname=" + bankname   ,$("#form").serialize(),SearchResult);
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
				    function DeleteBank(id)
				    {
				        if (confirm("Are you sure you want to delete " + id))
					    {
					        $.post("handler/bank.ashx?type=delete&bankcode=" + id  ,$("#form").serialize(),MessageAlertDelete);
					    }
				    }
				    function MessageAlertDelete(result)
					{
					    alert(result);
					    location.reload();					    
					}
					$("#bank").dialog({
                    autoOpen: false,
                    title: 'Bank Details',
                    modal: true,
                    autoresize:true ,
                    width:450,
                    height:150
                }); 
					</script>
</asp:Content>
