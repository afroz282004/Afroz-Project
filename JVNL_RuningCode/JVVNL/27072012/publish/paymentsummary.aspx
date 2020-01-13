<%@ Page Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="paymentsummary.aspx.cs" Inherits="JVVNLWeb.paymentsummary" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentplaceholder1" runat="server">

    <script type="text/javascript">
jQuery(document).ready(function() {
  var url = "CheckSession.aspx?hgUserName=" + document.getElementById("username").value;
         $(document).idleTimeout({ inactivity: 600000, noconfirm: 1000, sessionAlive: 600000, redirect_url: url });
			    oTable = $('#basictable').dataTable({
					        "bJQueryUI": true,
					        "sPaginationType": "full_numbers"});
					        $("#fromdate").datepicker({dateFormat:'dd/mm/yy'});
					        $("#todate").datepicker({dateFormat:'dd/mm/yy'});
        
});
			</script>
 <div class="grid_12">
						<div class="box">
							<h2>
								Payment Summary Search
								<span class="l"></span><span class="r"></span>
							</h2>
							<div class="block">
								<div class="form_place"  id="form1">
									<table class="fieldtable">
									    <tr>
									        <td> From Date:<input type="hidden" id="username" value="<% Response.Write(Session["username"].ToString()); %>" /></td>
									        <td><input   type="text" id="fromdate" name="fromdate" size="20px" maxlength="20" readonly=readonly value="<% Response.Write( DateTime.Today.ToString("dd/MM/yyyy")); %>" /></td>
									        <td>To Date:</td>
									        <td><input   type="text" id="todate" name="todate" size="20px" maxlength="20" readonly=readonly value="<% Response.Write( DateTime.Today.ToString("dd/MM/yyyy")); %>" /></td>
									        <td><button type="button" class="button red small submit alignleft" id="search">SEARCH</button></td>
									        
									    </tr>
									</table>
								</div>
							</div>
						</div>
					</div>
					<div class="grid_12">
						<div class="box">
							<h2>
								Payment Summary Result
								<span class="l"></span><span class="r"></span>
							</h2>
							<div class="block">
								<div class="block_in" id="resulttable">
									<!-- BEGIN TABLE -->
									<table class="table display" id="basictable">
										<thead>
											<tr>

												<th>Subdivision Code</th>
												<th>Account No</th>
												<th>Payment Date</th>
												<th>Payment Type</th>
												<th>Amount</th>
												<th>User Name</th>
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
					    $("#search").click(function(){
					        
					       PopulatePaymentSummary();  
					    });
					    function PopulatePaymentSummary()
					{
					    var username=document.getElementById("username").value;
					    var fromdate= document.getElementById("fromdate").value;
					    var todate=document.getElementById("todate").value;  
					    $.post("handler/payment.ashx?type=usersummary&username=" + username + "&fromdate=" + fromdate + "&todate=" + todate   ,$("#form").serialize(),SearchResult);
					}
					function SearchResult(result)
					{
					    $("#basictable > tbody").html("");
					    $("#basictable").append(result );
					    }
					</script>
</asp:Content>
