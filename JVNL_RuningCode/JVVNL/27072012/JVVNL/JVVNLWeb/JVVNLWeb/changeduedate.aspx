<%@ Page Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="changeduedate.aspx.cs" Inherits="JVVNLWeb.changeduedate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentplaceholder1" Runat="Server">
<script type="text/javascript">
jQuery(document).ready(function() {
var url = "CheckSession.aspx?hgUserName=" + document.getElementById("username").value;
$(document).idleTimeout({ inactivity: 600000, noconfirm:0, sessionAlive:0, redirect_url: url });
    $("#duedate").datepicker({dateFormat:'dd/mm/yy'});
  
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
									<table class="fieldtable">
									    <tr>
									       <td><label class="lblfield" >Binder No </label></td>
									            <td><input type="text" id="binderno" name="binderno" size="8" maxlength="4" onkeypress ="return OnlyNumber(event);" /></td>
									        <td><button type="button" class="button red small submit alignleft" id="search">SEARCH</button></td>
									       <%-- <td><button type="button" class="button red small submit alignleft" id="selectall">Select All</button></td>--%>
									       
									   </tr>
									   <tr>  <td><label class="lblfield" >Due Date </label></td>
					            <td>
					            <input type="text" id="duedate" name="duedate"/>
					          <input type="hidden" id="username" value="<% Response.Write(Session["username"].ToString()); %>" /></td>
					           
					        </tr>
					       
					        <tr><td colspan="2" align="center"><button type="button" class="button green small" id="save">SAVE</button>
					        <button type="button" class="button orange small" id="cancel">CANCEL</button>
					        </td></tr>
									</table>
								</div>
							</div>
						</div>
					</div>
					<div id="user1">
					    <table class="fieldtable">

					        
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
	                                            <th>Bill Id</th>
												<th>Name</th>
											
												<th>Bill Month</th>
												<th>Due Date</th>
												<th>Bill Amount</th>
										
												<th>
												<button type="button" class="button red small submit alignleft" id="selectall">Select All</button>
												<input type="checkbox"   id="chkBoxAll" checked="checked"  />
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
					
						<script type="text/javascript">
						  $("#selectall").click(function(){
						 			  
				checkAllBoxes('id');
					  
					});
				function checkAllBoxes(id)
               {
             
                      var gvControl = document.getElementById('basictable');
                              
                      //this is the checkbox in the item template...this has to be the same name as the ID of it
                      var gvChkBoxControl = "chkBoxChild";  
                               
                      //this is the checkbox in the header template
                      var mainChkBox = document.getElementById("chkBoxAll");
                               
                      //get an array of input types in the gridview
                      var inputTypes = gvControl.getElementsByTagName("input");
                               
                      for(var i = 0; i < inputTypes.length; i++)
                      {  
                     
                         //if the input type is a checkbox and the id of it is what we set above
                         //then check or uncheck according to the main checkbox in the header template 
                                  
                         if(inputTypes[i].type == 'checkbox' && inputTypes[i].id.indexOf(gvChkBoxControl,0) >= 0)
                        
                              inputTypes[i].checked = mainChkBox.checked;  
                      }
                     
  	                    uncheck();	
               }
               function uncheck()
               {
                var mainChkBox = document.getElementById("chkBoxAll");
              
                          if(mainChkBox.checked==true)
						  {
						 
						  mainChkBox.checked=false;
						  }
						else
						  {
						 
						  mainChkBox.checked=true;
						  }
						       
               }
					$("#save").click(function(){
					    
					     var duedate=document.getElementById("duedate").value ;
					     var username=document.getElementById("username").value;
					       
					    if (duedate=="")
					    {
					        alert("Enter Date");
					        document.getElementById("duedate").focus();
					        return ;  
					    }
					      var gvControl = document.getElementById('basictable');
                              
                      //this is the checkbox in the item template...this has to be the same name as the ID of it
                      var gvChkBoxControl = "chkBoxChild";  
                               
                      //this is the checkbox in the header template
                                                  
                      //get an array of input types in the gridview
                      var inputTypes = gvControl.getElementsByTagName("input");
                            
                      for(var i = 0; i < inputTypes.length; i++)
                      {  
                      var oCells = gvControl.rows.item(i).cells;

                   

                         //if the input type is a checkbox and the id of it is what we set above
                         //then check or uncheck according to the main checkbox in the header template 
                                  
                         if(inputTypes[i].type == 'checkbox' && inputTypes[i].id.indexOf(gvChkBoxControl,0) >= 0)
                        
                             if (inputTypes[i].checked ==true) 
                            {
                             var inputs =oCells.item(0).innerHTML;
                        
				          	 $.post("handler/Changeduedate.ashx?type=ChangeBillDuedate&billid="+ inputs + "&username=" + username  + "&duedate=" + duedate,$("#form").serialize());
						
						    } 
                      }
					    
					 });
				
					function ClearControls()
					{
					    document.getElementById("duedate").value="" ;
					      
					}
					  $("#cancel").click(function(){
					    ClearControls();
					  });
					  $("#search").click(function(){
					fillgrid();
					   
					});
					function fillgrid()
					{
					     var binderno= document.getElementById("binderno").value ;
				                                     
                            $.post("handler/Changeduedate.ashx?type=consumerselect&binderno=" + binderno, $("#form").serialize(),SearchResult);                        					    
                    }
					   function SearchResult(result)
                        {
                      
                            $("#basictable > tbody").html("");
					        $("#basictable tbody").append(result );
					         
				     
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
