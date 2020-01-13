<%@ Page Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="CancelTransaction.aspx.cs" Inherits="JVVNLWeb.CancelTransaction" Title="Cancel Transaction" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentplaceholder1" Runat="Server">

    <script type="text/javascript">
jQuery(document).ready(function() {
var url = "CheckSession.aspx?hgUserName=" + document.getElementById("username").value;
           $(document).idleTimeout({ inactivity: 600000, noconfirm:0, sessionAlive:0, redirect_url: url });
         //fillgridnew();
         oTable = $('#basictable').dataTable({
             "bJQueryUI": true,
             "sPaginationType": "full_numbers"
         });
  // PopulateSDO();

	  
});
			function OnlyNumber(e)
		    {
		        var keycode=e.charCode? e.charCode : e.keyCode;
		        if ((keycode < 48 || keycode >57) && keycode  != 8 && keycode  != 9  )
		        {
		            return false;
		        }
		    }
		    function NoSpace(e)
		    {
		        
		        var keycode=e.charCode? e.charCode : e.keyCode;
		        if (keycode == 32 || keycode==17)
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
								Bill Search
								<span class="l"></span><span class="r"></span>
							</h2>
							<div class="block">
								<div class="form_place"  id="Div1">
								<input type="hidden" id="username" value="<% Response.Write(Session["username"].ToString()); %>" />
									<table class="fieldtable">
									    <%--<tr>
									       <td><label class="lblfield" >Subdivision Code</label></td>
									            <td><select id="subdivision" name="subdivision"><option value="select">----------Select------------</option> </select></td>
									        <td><button type="button" class="button red small submit alignleft" id="search">SEARCH</button></td>
									   </tr>--%>
									   <tr>
									        <td><label class="lblfield" >Subdivision Code</label></td>
									            <td><asp:DropDownList runat ="server" ID="subdivision"></asp:DropDownList></td>
									        <td>Binder No</td>
									        <td><input type="text" id="binderno" name="binderno" runat="server" size="8" maxlength="4" onkeypress ="return OnlyNumber(event);" /></td>
									        <td>Account No</td>
									        <td><input type="text" id="accountno" name="accountno" runat="server" size="8" maxlength="10" onkeypress ="return OnlyNumber(event);"/></td>
									        <td>Receipt No</td>
									        <td><input type="text" id="receiptno" name="receiptno" runat="server" size="8" maxlength="10" onkeypress ="return OnlyNumber(event);"/></td>
									        <td><%--<button type="button" class="button red small submit alignleft" id="search">SEARCH</button>--%>
									        <asp:Button runat="server" ID="btnsearch" 
                                                    CssClass ="button red small submit alignleft" Text="SEARCH" 
                                                    onclick="btnsearch_Click" />
									        </td>
									        
                                                
									    </tr>
									</table>
								</div>
							</div>
						</div>
					</div>
					<div id="user1">
					    <table class="fieldtable">

					        <tr>
					            <td><input type="hidden" id="PayAmount" name="PayAmount"/><input type="hidden" id="billid" name="billid"/>
					            <input type="hidden" id="phoneno" name="phoneno"/><input type="hidden" id="EmailId" name="EmailId"/>Cancel Request User Name</td>
					            <td><input type="text" name="fusername" value="<% Response.Write(Session["username"].ToString()); %>"   id="fusername" size="25px" maxlength="15" onkeypress="return NoSpace(event);" onpaste="return false;" oncut="return false;" oncontextmenu="return false;" /></td>
					        </tr>
					       
					        <tr><td colspan="2" align="center"><button type="button" class="button green small" id="save">SAVE</button>
					        <button type="button" class="button orange small" id="cancel">CANCEL</button>
					        </td></tr>
					    </table>
					</div>
					
					<div class="grid_12">
						<div class="box">
							<h2>
								Bill Search Result
								<span class="l"></span><span class="r"></span>
							</h2>
							<div class="block">
								<div class="block_in" >
									<!-- BEGIN TABLE -->
									<table class="table display" id="basictable">
										<thead>
											<tr>

												<th>Name</th>
												<th>SDO Code</th>
												<th>Account No</th>
												<th>Receipt No</th>
												<th>Counter Name</th>
												<th>Payment Date</th>
												<th>Amount Paid</th>
												<th>Cancel Payment</th>
											</tr>

										</thead>
										<tbody id="tbody" runat="server">
										</tbody>
									</table>
									<!-- END TABLE -->
								</div>
							</div>

						</div>
					</div>
					
						<script type="text/javascript">

						    $("#save").click(function() {
						        var fusername = document.getElementById("fusername").value;
						        var billid = document.getElementById("billid").value;
						        var phoneno = document.getElementById("phoneno").value;
						        var email = document.getElementById("EmailId").value;
						        var PayAmount = document.getElementById("PayAmount").value;
						       
						        $.post("handler/CancelTrasaction.ashx?type=CancelBill&billid=" + billid + "&fusername=" + fusername + "&phoneno=" + phoneno + "&email=" + email + "&PayAmount=" + PayAmount, $("#form").serialize(), SaveData);
						    });
					function SaveData(result)
					{
					   
					    if (result.toString().indexOf ("already")>=0)
					        document.getElementById("fusername").focus();
					    else
					    
					        ClearControls(); 
					        document.getElementById("billid").value="" ;
					        document.getElementById("phoneno").value="";
					        document.getElementById("EmailId").value="";
					        document.getElementById("PayAmount").value="";
					        MessageAlertDelete(result);
					}
					function ClearControls()
					{
					    document.getElementById("fusername").value="" ;
					      
					}
					  $("#cancel").click(function(){
					    ClearControls();
					  });
					  $("#search").click(function() {

					  

					      fillgridnew();

					  });
					
					function fillgridnew()
					{
					    var subdivision = document.getElementById("ctl00_contentplaceholder1_subdivision").value;
					    var binderno = document.getElementById("ctl00_contentplaceholder1_binderno").value;
					    var accountno = document.getElementById("ctl00_contentplaceholder1_accountno").value;
					    var receiptno = document.getElementById("ctl00_contentplaceholder1_receiptno").value;
					  
					     $.post("handler/CancelTrasaction.ashx?type=consumerselect&subdivision=" + subdivision  + "&binderno=" + binderno + "&accountno=" + accountno + "&receiptno=" + receiptno, $("#form").serialize(),SearchResult);                        					    
					    };
					
					function fillgrid()
					{
					    var subdivision = document.getElementById("ctl00_contentplaceholder1_subdivision").value;
					     var name="";
                            var accountno="";
                              
                            $.post("handler/CancelTrasaction.ashx?type=consumerselect&subdivision=" + subdivision  + "&name=" + name + "&accountno=" + accountno  , $("#form").serialize(),SearchResult);                        					    
                    }
					   function SearchResult(result)
                        {
                       
                            $("#basictable > tbody").html("");
					        $("#basictable tbody").append(result );
					         
				         if (typeof oTable == 'undefined') 
				         {
                           
                          } 
                        }
				    function PopulateSDO()
					    {
                            $.post("handler/sdo.ashx?type=populate",$("#form").serialize(),ShowSDO);                        					    
					    }
					    function ShowSDO(result)
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
                                    $("#ctl00_contentplaceholder1_subdivision").append(addoption); 
                                    addoption1 = $('<option></option>').attr("value", splitoption[0]).text(splitoption[1]);
                                    $("#ctl00_contentplaceholder1_subdivisionsearch").append(addoption1); 
                                }   
                            }
					    }
				    function DeleteSDO(result)
				    {
				      //alert(result);
				        var splitoption = result.split("$");  
				        if (confirm("Are you sure you want to Cancel Payment " + splitoption[0]))
					    {
					    
					        document.getElementById("billid").value=splitoption[0];					        
					        document.getElementById("phoneno").value=splitoption[1];
					        document.getElementById("EmailId").value=splitoption[2];
					        document.getElementById("PayAmount").value = splitoption[3];

					       var fusername = document.getElementById("fusername").value;
					       var billid = document.getElementById("billid").value;
					       var phoneno = document.getElementById("phoneno").value;
					       var email = document.getElementById("EmailId").value;
					       var PayAmount = document.getElementById("PayAmount").value;
					   $.post("handler/CancelTrasaction.ashx?type=CancelBill&billid=" + billid + "&fusername=" + fusername + "&phoneno=" + phoneno + "&email=" + email + "&PayAmount=" + PayAmount, $("#form").serialize(), SaveData);



//                       $("#user1").dialog('open');
					    }
				    }
				    function MessageAlertDelete(result)
					{
					    alert(result);
					    location.replace("CancelTransaction.aspx");
					}
					$("#user1").dialog({
                    autoOpen: false,
                    title: 'Bill Details',
                    modal: true,
                    autoresize:true ,
                    width:450,
                    height:150
                }); 
					</script>
</asp:Content>
