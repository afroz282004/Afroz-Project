<%@ Page Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="Counter.aspx.cs" Inherits="JVVNLWeb.Counter" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentplaceholder1" runat="server">
 <script type="text/javascript" src="js/jquery.dataTables.js"></script>
<link rel="stylesheet" href="css/demo_table_jui.css" />
<link href="css/jquery-ui-1.7.2.custom.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">

    jQuery(document).ready(function() {
     var url = "CheckSession.aspx?hgUserName=" + document.getElementById("username").value;
         $(document).idleTimeout({ inactivity: 600000, noconfirm: 1000, sessionAlive: 600000, redirect_url: url });
    PopulateCounter();
    });
</script>
<script type = "text/javascript">
    var isShift = false;
    function keyUP(keyCode) {
        if (keyCode == 16 || (keyCode >= 65 && keyCode <= 90)) {
            isShift = false;
            return false;
        }
        else {
          
        }
    }   

    function isNumeric(keyCode) {
        if (keyCode == 16)
            isShift = true;

        return ((keyCode >= 48 && keyCode <= 57 || keyCode == 8 || (keyCode >= 96 && keyCode <= 105)) && isShift == false);

    }
 </script>

<div class="grid_12">
						<div class="box">
						<input type="hidden" id="username" value="<% Response.Write(Session["username"].ToString()); %>" />
<input type="hidden" name="CounterID" id="CounterID" value="" />
							<h2>
								Counter Search
								<span class="l"></span><span class="r"></span>
							</h2>
							<div class="block">
								<div class="form_place"  id="Div1">
									<table class="fieldtable">
									    
									    <tr>
									        <td>Counter Name:</td>
									        <td>
									        <input   type="text" id="countername" name="countername" size="35"/>
									        
									        </td>
									        <td>
									         <button  type="button"   class="button red small submit alignleft" id="search">
                                                 SEARCH</button>
									         <button  type="button"   class="button blue small submit alignleft" id="new">NEW</button>

									        </td>
									        
									    </tr>
									</table>
								</div>
							</div>
						</div>
					</div>
					
<div   id="CounterDiv" >
<table class="fieldtable">
				    
				    <tr>
				        <td>Counter Name:</td>
				        <td><input   type="text"  id="txtCounterName" name="txtCounterName" size="35" maxlength="100"/></td>
				        </tr><tr>
				        <td>Address:</td>
				        <td>
				        <%--<textarea id="txtAddress" name="txtAddress" cols="35"></textarea>--%>
				        <input   type="text"   id="txtAddress" name="txtAddress" size="50" height="30px"   maxlength="200"/>
				       
				        </td>
				    </tr>
				    <tr>
				        <td>Counter Person:</td>
				        <td><input   type="text"  id="txtContactperson" name="txtContactperson" size="35" maxlength="75" /></td>
				        </tr><tr>
				        <td>ContactNo</td>
				        <td><input   type="text" id="txtContactNumber" name="txtContactNumber" size="25" maxlength="12" onkeyup = "keyUP(event.keyCode)" onkeydown = "return isNumeric(event.keyCode);" onpaste = "return false;"/></td>
				    </tr>
				    <tr>
	                    
	                       <%--<button  type="button" id="btnSubmit" runat="server"  class="button green small submit alignleft" >Submit</button>--%>
	                       <%--<button  type="button" id="btnCancel"  class="button orange small submit alignleft" >Cancel</button>--%>
					        <td colspan="2" align="center"><button type="button" class="button green small" id="save">
                                SAVE</button>
					        <button type="button" class="button orange small" id="cancel">CANCEL</button>
					        </td>	
					 </tr>			    
				</table>
  
</div>
					
<div class="grid_12">
						<div class="box">
							<h2>

								Dynamic Data Table
								<span class="l"></span><span class="r"></span>
							</h2>
							<div class="block">
								<div class="block_in">
									<!-- BEGIN TABLE -->
									<table class="table display" id="basictable">
										<thead>
                                                <tr>
                                                   <th>
                                                        Id
                                                    </th>
                                                    <th>
                                                        CounterName
                                                    </th>
                                                    <th>
                                                        Address
                                                    </th>
                                                    <th>
                                                        ContactPerson
                                                    </th>
                                                    <th>
                                                        ContactNo
                                                    </th>
                                                    <th>
                                                        Edit Delete
                                                    </th>                                                    
                                                </tr>
                                            </thead>
                                            
                                            <tbody id="tbody">
										
										</tbody>
									</table>
									<!-- END TABLE -->
								</div>
							</div>

						</div>
					</div>
	
	
	
	<script type="text/javascript" >
	    $("#new").click(function() {

	        document.getElementById("txtCounterName").value = "";
	        document.getElementById("txtAddress").value = "";
	        document.getElementById("txtContactperson").value = "";
	        document.getElementById("txtContactNumber").value = "";
	        document.getElementById("CounterID").value = "";

	        $("#CounterDiv").dialog('open');


	    });

	$("#cancel").click(function() {
	$("#CounterDiv").dialog('close');
	});
	$("#save").click(function() {

	    var Contactname = document.getElementById("txtCounterName").value;
	    var Address = document.getElementById("txtAddress").value;
	    var ContactPerson = document.getElementById("txtContactperson").value;
	    var ContactNumber = document.getElementById("txtContactNumber").value;

	    if (Contactname == "" || Address == "") {
	        alert('Please Enter Contact name Address');
	        return;
	    }
	    if (ContactPerson == "" || ContactNumber == "") {
	        alert('Please Enter Contact Person and  ContactNumber');
	        return;
	    }


	    if (document.getElementById("CounterID").value == "") {
	       
	        $.post("handler/Counter.ashx?type=insert&txtCounterName=" + Contactname + "&txtAddress=" + Address + "&txtContactperson=" + ContactPerson + "&txtContactNumber=" + ContactNumber, $("#form").serialize(), SaveData);
	    }
	    else {
	       
	        var idCounter = document.getElementById("CounterID").value
	        $.post("handler/Counter.ashx?type=EditSave&id=" + idCounter + "&txtCounterName=" + Contactname + "&txtAddress=" + Address + "&txtContactperson=" + ContactPerson + "&txtContactNumber=" + ContactNumber, $("#form").serialize(), SaveData);
	    }
	});
	    function SaveData(result) {
	      
	        ClearControls();
	        Success();
	    }

	    function Success() {
	        location.reload();
	    }
	    
	    function ClearControls() {
	        document.getElementById("txtCounterName").value = "";
	        document.getElementById("txtAddress").value = "";
	        document.getElementById("txtContactperson").value = "";
	        document.getElementById("txtContactNumber").value = "";
	    }
	    $("#search").click(function() {
	    location.reload();
	});

	function PopulateCounter()
	{

	    var countername = document.getElementById("countername").value;
	    $.post("handler/Counter.ashx?type=search&countername=" + countername, $("#form").serialize(), SearchResult);
					    				    
    }
	    function SearchResult(result) {
	             $("#basictable > tbody").html("");
                 $("#basictable").append(result);
                 oTable = $('#basictable').dataTable({
                 "bJQueryUI": true,
                 "sPaginationType": "full_numbers"
                                });       
	    }

	    $("#CounterDiv").dialog({
	        autoOpen: false,
	        title: 'Counter Details',
	        modal: true,
	        autoresize: true,
	        width: 450,
	        height: 250
	    });

	    function EditRecord(id) {
	       
	        var Counterid = document.getElementById("CounterID");
	        Counterid.value = id;
	       
	        $.post("handler/Counter.ashx?type=Edit&id=" + id, $("#form").serialize(), FillDialog);

	    }
	    function DeleteSuccess(result) {
	        location.reload();
	    };
	        

	    function DeleteRecord(id) {
	        var where = confirm("Do you want to delete??");
	        if (where == true) {
	            var Delid = id;
	            $.post("handler/Counter.ashx?type=Delete&id=" + Delid, $("#form").serialize(), DeleteSuccess);

	        }
	}        

	    function FillDialog(result) {
	        var data = result.toString().split('#')
	        document.getElementById("txtCounterName").value = data[0];
	        document.getElementById("txtCounterName").disabled = true;
	        document.getElementById("txtAddress").value = data[1];
	        document.getElementById("txtContactperson").value = data[2];
	        document.getElementById("txtContactNumber").value = data[3];
	        $("#CounterDiv").dialog('open');
	    }
	    	    </script>						
</asp:Content>
