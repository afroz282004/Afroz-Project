<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="JVVNLWeb._default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" dir="ltr" lang="en-US"> 

	<head>
<!--========= STYLES =========-->
		<link rel="stylesheet" href="css/reset.css" />
		<link rel="stylesheet" href="css/grid.css" />
		<link rel="stylesheet" href="css/uniform.default.css" />
		<link rel="stylesheet" href="css/chosen.css" />
		<link rel="stylesheet" href="css/jquery.ui.all.css" />
		<link rel="stylesheet" href="css/style.css" />
		<link rel="stylesheet" href="css/demo_table_jui.css" />
        <link href="css/jquery-ui-1.7.2.custom.css" rel="stylesheet" type="text/css" />
		<!--[if gte IE 8]><link rel="stylesheet" href="css/ie8.css" /><![endif]-->
		
		<!--============ JQUERY =============-->
		<script src="js/jquery.js" type="text/javascript"></script>
		<script src="js/jquery.ui.core.js" type="text/javascript"></script>
        <script src="js/jquery.ui.widget.js" type="text/javascript"></script>
		<script src="js/jquery.uniform.js" type="text/javascript"></script>
		<script src="js/chosen.jquery.js" type="text/javascript"></script>
		<script src="js/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
        <script type="text/javascript" src="js/jquery.dataTables.js"></script>
        <script src="js/jquery.ui.datepicker.js" type="text/javascript"></script>
	

		<script type="text/javascript">
		    function ClearData()
        {
            document.getElementById("username").value ="";
            document.getElementById("password").value ="";
            document.getElementById("username").focus();  
        }
		    jQuery(document).ready(function() {
		          
		            ClearData();
		            PopulateSDO();
		            
		    });
		    
		    
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
		</script>				
		<!--=== ENABLE HTML5 TAGS FOR IE ===-->
		<!--[if IE]><script src="js/html5.js"></script><![endif]-->
		
		<title>JVVNL Bill Payment </title>
	</head>
    
	<body class="login_background">
	    <div id="user" style="display:none ">
					    <table class="fieldtable">
					        <tr>
					            <td>First Name</td>
					            <td><input type="text" name="firstname" id="firstname" size="35" maxlength="20"  onkeypress="return OnlyAlpha(event);" onpaste="return false;" oncut="return false;" oncontextmenu="return false;" /></td>
					            </tr><tr>
					            <td>Last Name</td>
					            <td><input type="text" name="lastname" id="lastname" size="35" maxlength="20" onkeypress="return OnlyAlpha(event);" onpaste="return false;" oncut="return false;" oncontextmenu="return false;" /></td>
					        </tr>
					        <tr>
					            <td>User Name</td>
					            <td><input type="text" name="fusername" id="fusername" size="25" maxlength="15" onkeypress="return NoSpace(event);" onpaste="return false;" oncut="return false;" oncontextmenu="return false;" /></td>
					        </tr>
					        <tr>
					            <td>Password</td>
					            <td><input type="password" name="password" id="npassword" size="25" maxlength="15" onkeypress="return NoSpace(event);" onpaste="return false;" oncut="return false;" oncontextmenu="return false;"/></td>
					        </tr>
					        <tr>
					            <td>Confirm Password</td>
					            <td><input type="password" name="cpassword" id="cpassword" size="25" maxlength="15" onkeypress="return NoSpace(event);" onpaste="return false;" oncut="return false;" oncontextmenu="return false;" /></td>
					        </tr>
					        <tr>
					            <td>Subdivision Code</td>
					            <td><select id="subdivision" name="subdivision"><option value="select">----------Select------------</option> </select></td>
					        </tr>
					        <tr>
					            <td>Account No</td>
					            <td><input type="text" id="accountno" name="accountno" size="25" maxlength="8" onkeypress ="return OnlyNumber(event);"/></td>
					        </tr>
					        <tr>
					        <td>Phone No</td>
                            <td><input type="text" name="phoneno" id="phoneno" size="25" maxlength="10" onkeypress ="return OnlyNumber(event);"/></td>
                            </tr><tr>
                            <td>Email ID</td>
                            <td><input type="text" name="emailid" id="emailid" size="50" maxlength="30" /></td>
							</tr>		            
					        <tr><td colspan="2" align="center"><button type="button" class="button green small" id="save">SAVE</button>
					        <button type="button" class="button orange small" id="cancel">CANCEL</button>
					        </td></tr>
					    </table>
					</div>
		<center><a class="logo" href="#" ></a></center>
		<div class="headertitle">
		Cash Collection System
		</div>
		<section class="container">
		                    
			<section class="container_12">
			    
				<section id="page_content" class="page_content">
                        
						<div class="login_box">
						    
							<h2>
								<span class="title">JVVNL Log In</span>
								<span class="l"></span><span class="r"></span>
							</h2>
							<div class="login_block_in">
								<form action="index.html">
									<div class="login_row">

										<input type="text" id="username" class="username" maxlength="15" placeholder="username" onkeydown="return NoSpace(event);" onpaste="return false;" oncut="return false;" oncontextmenu="return false;"  />
									</div>
									<div class="clear"></div>
									<div class="login_row">
										<input type="password" id="password" class="password" maxlength="15" placeholder="password" onkeypress="return NoSpace(event);" onpaste="return false;" oncut="return false;" oncontextmenu="return false;" />
									</div>
									
									<input type="button" id="submit" class="button blue alignright login_submit" value="Login" onclick="return submit_onclick()" />
									<button id="new" type="button" class="button orange alignright login_submit"  >New User</button>
								</form>
							</div>
						</div>
					<div class="clear"></div>
				</section><!-- end of #page_content -->
			</section><!-- end of #container_12 -->
	
		</section><!-- end of #container -->
				   <div  class="footertitle">
		Powered by Trimax IT InfraStructure & Services LTD.</a>
		<br />
		This Application Runs Only In Firefox..
	</div>
	<script src="js/ui_calls.js" type="text/javascript"></script>

	<script src="js/scripts.js" type="text/javascript"></script>
    <script type="text/javascript">
        $("#submit").click(function(){
            
            ValidateLogin ();
        });
        function ValidateLogin()
        {
             var username=document.getElementById("username").value ;
             var password=document.getElementById("password").value ;
            if (username ==""  )
	        {
	            alert("Please Enter User Name")  ;
	            document.getElementById("username").focus();   
	            return false;
	        }
	        if (password  ==""  )
	        {
	            alert("Please Enter Password" ) ;
	            document.getElementById("user_pass").focus();   
	            return false;
	        }	        
            $.post("handler/user.ashx?type=login&username=" + username  + "&password=" + password   ,$("#form").serialize(),CheckLogin);
            
        } 
        function CheckLogin(result)
        {
            
            if (result.toString().indexOf("success")>=0 )
            {
                var data= result.toString().split("#");  
                var username=document.getElementById("username").value ;
                if( data[1]=="1")
                {
                
                    location.replace("counter.aspx?username=" + username + "&groupid=" + data[1]);
                    }
                else if ( data[1]=="2")
                {
                 
                    location.replace("payment.aspx?username=" + username + "&groupid=" + data[1]);
                }
                else if (data[1] == "5") {

                location.replace("report.aspx?username=" + username + "&groupid=" + data[1] + "&sdocode=" + data[2]);
                }
                else
                {
                 
                    location.replace("paymentsummary.aspx?username=" + username + "&groupid=" + data[1]);
                    }
            } 
            else
            {
                alert(result) ;
                document.getElementById("username").focus();  
            }
        }
        $("#password").keypress(function (event){
            if (event.which==13)
            {
                ValidateLogin(); 
            }
        });
        $("#user").dialog({
                    autoOpen: false,
                    title: 'User Details',
                    modal: true,
                    autoresize:true ,
                    width:430,
                    height:330
                }); 
                $("#new").click(function(){
					    $("#user").dialog('open');
					});
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
                                    $("#subdivision").append(addoption); 
                                }   
                         }
					    }
					    $("#save").click(function(){
					    
					    var firstname=document.getElementById("firstname").value ;
					    var lastname=document.getElementById("lastname").value ;
					    var username=document.getElementById("fusername").value ;
					    var password=document.getElementById("npassword").value ;
					    var cpassword=document.getElementById("cpassword").value ;
					    var subdivision = document.getElementById("subdivision").value   ;
					    var accountno = document.getElementById("accountno").value   ;
					    var phoneno = document.getElementById("phoneno").value   ;
					    var emailid = document.getElementById("emailid").value   ;
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
					        document.getElementById("npassword").focus();
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
					    if (subdivision =="select" || subdivision=="0")
					    {
					        alert("Select Subdivision");
					        document.getElementById("subdivision").focus();
					        return ;   
					    }
					    if (accountno =="")
					    {
					        alert("Enter Account No");
					        document.getElementById("accountno").focus();
					        return ;
					    }
					    if (accountno.length != 8)
					    {
					        alert("Account No should be 8 digit long");
					        document.getElementById("accountno").focus();
					        return ;
					    } 
					    if (phoneno=="")
					    {
					            alert("Enter Phone No"); 
					            document.getElementById("phoneno").focus();
					            return ;   
					    }
					    if (phoneno.length != 10)
					    {
					            alert("Phone No should be 10 digit long"); 
					            document.getElementById("phoneno").focus();
					            return ;   
					    }
					    if (emailid =="")
					    {
					            alert("Enter Email ID"); 
					            document.getElementById("emailid").focus();
					            return ;   
					    }
					    var expre=/^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
					    if (expre.test(emailid)==false)
                        {
                            alert("Enter valid Email ID");
                            document.getElementById("emailid").focus();
                            return false;
                        } 
					    
					    $.post("handler/user.ashx?type=insert&firstname="+ firstname + "&lastname=" + lastname +"&username=" + username   +"&password=" + password  +"&countername=0&groupname=3&subdivisioncode=" + subdivision  +"&accountno="+ accountno  +"&phoneno=" + phoneno  +"&emailid=" + emailid + "&ipaddress="   ,$("#form").serialize(),SaveData);
					});
					function ClearControls()
					{
					    document.getElementById("firstname").value="";
					    document.getElementById("lastname").value="" ;
					    document.getElementById("fusername").value="" ;
					    document.getElementById("npassword").value ="";
					    document.getElementById("cpassword").value="" ;
					    document.getElementById("subdivision").value="select" ;
					    document.getElementById("accountno").value="";
					    document.getElementById("phoneno").value="";
					    document.getElementById("emailid").value="";
					}
					function SaveData(result)
					{
					    alert(result );
					    if (result.toString().indexOf ("already")>=0) 
					        document.getElementById("fusername").focus(); 
					    else
					    {
					        ClearControls(); 
					        $("#user").dialog('open');
					   }
					        
				}
				$("#cancel").click(function(){
				    ClearControls();
				});
				
function submit_onclick() {

}

    </script>	
 
	</body>

</html>

