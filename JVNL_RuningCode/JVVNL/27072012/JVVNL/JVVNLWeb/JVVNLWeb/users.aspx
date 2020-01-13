<%@ Page Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="users.aspx.cs" Inherits="JVVNLWeb.users" Title="JVVNL - New User" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentplaceholder1" runat="server">

    <script type="text/javascript">
			jQuery(document).ready(function() {
			 var url = "CheckSession.aspx?hgUserName=" + document.getElementById("hdnusername").value;
         $(document).idleTimeout({ inactivity: 600000, noconfirm: 0, sessionAlive: 0, redirect_url: url });
                PopulateUsers();
                PopulateGroupName();				
                PopulateCounterName();
			} );
			function NoSpace(e)
		    {
		        
		        var keycode=e.charCode? e.charCode : e.keyCode;
		        if (keycode == 32 || keycode==17)
		        {
		            return false;
		        }
		    }
		    	function OnlyNumberwithdot(e)
		    {
		        var keycode=e.charCode? e.charCode : e.keyCode;
		        if ((keycode < 48 || keycode >57) && keycode  != 8 && keycode  != 9 & keycode !=46  )
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
								User Search
								<span class="l"></span><span class="r"></span>
							</h2>
							<div class="block">
								<div class="form_place"  id="form1">
								<input type="hidden" id="hdnusername" value="<% Response.Write(Session["username"].ToString()); %>" />
									<table class="fieldtable">
									    <tr>
									        <td> User Name:</td>
									        <td><input   type="text" id="username" name="username" size="20px" maxlength="20" /></td>
									        <td>Group Name:</td>
									        <td><input   type="text" id="grpname" name="grpname" size="20px" maxlength="20" /></td>
									        <td>Counter Name:</td>
									        <td><input   type="text" id="countername" name="countername" size="20px"maxlength="20" /></td>
									        <td><button type="button" class="button red small submit alignleft" id="search">SEARCH</button>
									        <button type="button" class="button blue small submit alignleft" id="new">NEW</button></td>
									    </tr>
									</table>
								</div>
							</div>
						</div>
					</div>
					<div id="user">
					    <table class="fieldtable">
					        <tr>
					            <td>First Name</td>
					            <td><input type="text" name="firstname" id="firstname" size="35px" maxlength="20"  onkeypress="return OnlyAlpha(event);" onpaste="return false;" oncut="return false;" oncontextmenu="return false;" /></td>
					            </tr><tr>
					            <td>Last Name</td>
					            <td><input type="text" name="lastname" id="lastname" size="35px" maxlength="20" onkeypress="return OnlyAlpha(event);" onpaste="return false;" oncut="return false;" oncontextmenu="return false;" /></td>
					        </tr>
					        <tr>
					            <td>User Name</td>
					            <td><input type="text" name="fusername" id="fusername" size="25px" maxlength="15" onkeypress="return NoSpace(event);" onpaste="return false;" oncut="return false;" oncontextmenu="return false;" /></td>
					        </tr>
					        <tr>
					            <td>Password</td>
					            <td><input type="password" name="password" id="password" size="25px" maxlength="15" onkeypress="return NoSpace(event);" onpaste="return false;" oncut="return false;" oncontextmenu="return false;"/></td>
					        </tr>
					        <tr>
					            <td>Confirm Password</td>
					            <td><input type="password" name="cpassword" id="cpassword" size="25px" maxlength="15" onkeypress="return NoSpace(event);" onpaste="return false;" oncut="return false;" oncontextmenu="return false;" /></td>
					        </tr>
					        <tr>
					            <td>Counter Name</td>
					            <td><select id='ccountername' ><option value="select">-------Select--------</option></select></td>
					        </tr>
					        <tr>
					            <td>Group Name</td>
					            <td><select id='groupname' ><option value="select">-------Select--------</option></select></td>
					        </tr>
					         <tr>
					            <td>IP Address</td>
					            <td><input type="text" name="ipaddress" id="ipaddress" size="25px" maxlength="15" onkeypress="return OnlyNumberwithdot(event);" onpaste="return false;" oncut="return false;" oncontextmenu="return false;" /></td>
					        </tr>
					        <tr><td colspan="2" align="center"><button type="button" class="button green small" id="save">SAVE</button>
					        <button type="button" class="button orange small" id="cancel">CANCEL</button>
					        </td></tr>
					    </table>
					</div>
					<div id="edituser">
					    <table class="fieldtable">
					        <tr>
					            <td>User Name</td>
					            <td><input type="text" name="eusername" id="eusername" size="25px" readonly="readonly"  /></td>
					        </tr>
					        <tr>
					            <td>First Name</td>
					            <td><input type="text" name="efirstname" id="efirstname" size="35px" maxlength="20"  /></td>
					            </tr><tr>
					            <td>Last Name</td>
					            <td><input type="text" name="elastname" id="elastname" size="35px" maxlength="20" /></td>
					        </tr>
					        <tr>
					            <td>Counter Name</td>
					            <td><select id='ecountername' ><option value="select">-------Select--------</option></select></td>
					        </tr>
					        <tr>
					            <td>Group Name</td>
					            <td><select id='egroupname' ><option value="select">-------Select--------</option></select></td>
					        </tr>
					        <tr><td colspan="2" align="center"><button type="button" class="button green small" id="editsave">SAVE</button>
					        <button type="button" class="button orange small" id="editcancel">CANCEL</button>
					        </td></tr>
					    </table>
					</div>
					<div class="grid_12">
						<div class="box">
							<h2>
								User Search Result
								<span class="l"></span><span class="r"></span>
							</h2>
							<div class="block">
								<div class="block_in" id="resulttable">
									<!-- BEGIN TABLE -->
									<table class="table display" id="basictable">
										<thead>
											<tr>

												<th>User Name</th>
												<th>First Name</th>
												<th>Counter Name</th>
												<th>Group Name</th>
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
					<script type="text/javascript" >
					$("#new").click(function(){
					    $("#user").dialog('open');
					});
					$("#save").click(function(){
					    
					    var firstname=document.getElementById("firstname").value ;
					    var lastname=document.getElementById("lastname").value ;
					    var username=document.getElementById("fusername").value ;
					    var password=document.getElementById("password").value ;
					    var cpassword=document.getElementById("cpassword").value ;
					    var ipaddress=document.getElementById("ipaddress").value ;
					    var countername=document.getElementById("ccountername").value ;
					    var groupname=document.getElementById("groupname").value ;
					    if (firstname=="")
					    {
					        alert("Enter First Name");
					        document.getElementById("firstname").focus();
					        return;
					    }
					    if (lastname=="")
					    {
					        alert("Enter Last Name");
					        document.getElementById("lastname").focus();
					        return;
					    }
					    if (username=="")
					    {
					        alert("Enter username");
					        document.getElementById("fusername").focus();
					        return;
					    }
					    if (username.length <5)
					    {
					        alert("username should be 5 characters long");
					        document.getElementById("fusername").focus();
					        return;
					    }
					    if (password =="")
					    {
					        alert("Enter password");
					        document.getElementById("password").focus();
					        return;
					    }
					    if (password.length <5)
					    {
					        alert("pasword should be 5 characters long");
					        document.getElementById("password").focus();
					        return;
					    }
					    if (cpassword =="")
					    {
					        alert("Enter confirm password");
					        document.getElementById("cpassword").focus();
					        return;
					    }
					    if (cpassword !=password )
					    {
					        alert("Password and confirm password not matching");
					        document.getElementById("cpassword").focus();
					        return;
					    }
					    if (countername =="select" || countername=="0")
					    {
					        alert("Select Counter Name");
					        document.getElementById("ccountername").focus();
					        return;
					    }
					    if (groupname  =="select" || groupname=="0")
					    {
					        alert("Select group Name");
					        document.getElementById("groupname").focus();
					        return;
					    }
					    if (document.getElementById("groupname").value="2")
					    {
					        if (document.getElementById("ipaddress").value=="")
					        {
					            alert("Please Enter IP Address");
					            document.getElementById("ipaddress").focus();
					            return ;
					        }  
					    }
					      
					    $.post("handler/user.ashx?type=insert&firstname="+ firstname + "&lastname=" + lastname +"&username=" + username   +"&password=" + password  +"&countername=" + countername + "&groupname=" + groupname +"&subdivisioncode=0&accountno=0&phoneno=&emailid=0&ipaddress=" + ipaddress   ,$("#form").serialize(),SaveData);
					});
					function SaveData(result)
					{
					    alert(result );
					    if (result.toString().indexOf ("already")>=0) 
					        document.getElementById("fusername").focus(); 
					    else
					        ClearControls(); 
					        
					}
					$("#editsave").click(function(){
					    var firstname=document.getElementById("efirstname").value ;
					    var lastname=document.getElementById("elastname").value ;
					    var username=document.getElementById("eusername").value ;
					    var countername=document.getElementById("ecountername").value ;
					    var groupname=document.getElementById("egroupname").value ;
					    if (firstname=="")
					    {
					        alert("Enter First Name");
					        document.getElementById("efirstname").focus();
					        return;
					    }
					    if (lastname=="")
					    {
					        alert("Enter Last Name");
					        document.getElementById("elastname").focus();
					        return;
					    }
					    if (countername =="select" || countername=="0")
					    {
					        alert("Select Counter Name");
					        document.getElementById("ecountername").focus();
					        return;
					    }
					    if (groupname  =="select" || groupname=="0")
					    {
					        alert("Select group Name");
					        document.getElementById("egroupname").focus();
					        return;
					    }
					    $.post("handler/user.ashx?type=update&firstname="+ firstname + "&lastname=" + lastname +"&username=" + username    +"&countername=" + countername + "&groupname=" + groupname  ,$("#form").serialize(),SaveEditData);
					});
					function SaveEditData(result)
					{
					    alert(result );
					    location.reload();
					}
					
					function ClearControls()
					{
					    document.getElementById("firstname").value="";
					    document.getElementById("lastname").value="" ;
					    document.getElementById("fusername").value="" ;
					    document.getElementById("password").value ="";
					    document.getElementById("cpassword").value="" ;
					    document.getElementById("ipaddress").value="" ;
					    document.getElementById("ccountername").value="select" ;
					    document.getElementById("groupname").value="select";
					}
					$("#search").click(function(){
					    location.reload();
					    
					});
					function PopulateUsers()
					{
					    var username=document.getElementById("username").value;
					    var grpname= document.getElementById("grpname").value;
					    var countername=document.getElementById("countername").value;  
					    $.post("handler/user.ashx?type=search&username=" + username + "&grpname=" + grpname + "&countername=" + countername   ,$("#form").serialize(),SearchResult);
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
					function  BlockUser(id)
					{
					    if (confirm("Are you sure you want to block " + id))
					    {
					        $.post("handler/user.ashx?type=block&username=" + id  ,$("#form").serialize(),MessageAlertBlock);
					    }
					    
					}
					function MessageAlertBlock(result)
					{
					    alert(result);
					    location.reload();					    
					}
					function  ResetPassword(id)
					{
					    if (confirm("Are you sure you want to reset password of " + id))
					    {
					        $.post("handler/user.ashx?type=reset&username=" + id  ,$("#form").serialize(),MessageAlertReset);
					    }
					}
					function MessageAlertReset(result)
					{
					    alert(result);				    
					}
					function EditRecord(id)
					{
					    $.post("handler/user.ashx?type=edit&username=" + id  ,$("#form").serialize(),ShowEditRec);
					    
					}
					function ShowEditRec(result)
					{
					    if (result.toString().indexOf('#')>=0)
					    {
					        var data= result.toString().split('#')   
					        document.getElementById("eusername").value=data[0] ;
					        document.getElementById("efirstname").value=data[1] ;
					        document.getElementById("elastname").value=data[2] ;
					        document.getElementById("ecountername").value=data[3] ;
					        document.getElementById("egroupname").value=data[4] ;
					        $("#edituser").dialog('open');
					        document.getElementById("efirstname").focus();
					    }  
					    else
					        alert(result); 
					    
					}
					function PopulateGroupName()
					{
                        $.post("handler/user.ashx?type=selectgroup",$("#form").serialize(),PopulateGroup);                        					    
					}
					function PopulateGroup(result)
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
                                    $("#groupname").append(addoption); 
                                    addoption1 = $('<option></option>').attr("value", splitoption[0]).text(splitoption[1]);
                                    $("#egroupname").append(addoption1); 
                                }   
                         }
					}
					function PopulateCounterName()
					{
                        $.post("handler/counter.ashx?type=populate",$("#form").serialize(),PopulateCounter);                        					    
					}
					function PopulateCounter(result)
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
                                    $("#ccountername").append(addoption); 
                                    addoption1 = $('<option></option>').attr("value", splitoption[0]).text(splitoption[1]);
                                    $("#ecountername").append(addoption1); 
                                }   
                         }
					}
					
					$("#user").dialog({
                    autoOpen: false,
                    title: 'User Details',
                    modal: true,
                    autoresize:true ,
                    width:450,
                    height:280
                }); 
                $("#edituser").dialog({
                    autoOpen: false,
                    title: 'User Details',
                    modal: true,
                    autoresize:true ,
                    width:450,
                    height:230
                }); 
					</script>
</asp:Content>
