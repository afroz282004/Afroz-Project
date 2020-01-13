<%@ Page Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="payment.aspx.cs" Inherits="JVVNLWeb.payment" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentplaceholder1" runat="server">

    <script type="text/javascript">
			jQuery(document).ready(function() {
			 var url = "CheckSession.aspx?hgUserName=" + document.getElementById("username").value;
         $(document).idleTimeout({ inactivity: 600000, noconfirm: 1000, sessionAlive: 600000, redirect_url: url });
			    oTable = $('#basictable').dataTable({
					        "bJQueryUI": true,
					        "sPaginationType": "full_numbers"
					    });
					    
					       
				checkclosedtrasction();	        
			    PopulateSDO();
			    PopulateBank();
			    PopulatePaymentType();
			   //FunPrint();
			    PopulatePaymentSummary();
			    $("#chequedate").datepicker({dateFormat:'dd/mm/yy'});
//			  alert($("#paymenttype")[0].selectedIndex);
//                     $("#paymenttype")[0].selectedIndex = "7";
//                  alert($("#paymenttype")[0].selectedIndex);
                 Default();
                 
			});
			
			function Default()
		    {
		  
		         $("#paymenttype").val('7'); 
                
		    }
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
		<div id="AccCode" style="display:none">
					    <table class="fieldtable">
					        <tr>
					            <td>Subdivision</td>
					            <td><select id="subdivisionsearch" name="subdivision" style="width: 150px; height:20px;"><option value="select">----------Select------------</option> </select></td>
					            <td>Search By</td>
					            <td><select id="searchby"><option value="name" selected="selected">Name</option><option value="accountno">Account No</option></select></td>
					            <td><input type ="text" id="searchtext" maxlength="30" /></td>
					            <td colspan="2" align="center"><button type="button" class="button green small" id="searchacc">Search</button>	</td></tr>
					    </table>
					    <div class="block ">
					        <table class="table display" id="basictable">
										<thead>
    									<tr>
											<th>Account No</th>
											<th>Name</th>
											<th>Address</th>
											</tr>

										</thead>
										<tbody id="tbody">
										</tbody>
									</table>
					    </div>
					</div>
<div class="grid_12">
						<div class="box">
							<h2>
								Make Payment - Single
								<span class="l"></span><span class="r"></span>
							</h2>
							<div class="block">
								<div class="form_place"  id="form1">
									<fieldset>
									    <legend>Search</legend>
									
									<table class="fieldtable" >
									    <tr>
									        <td><input type="hidden" id="username" value="<% Response.Write(Session["username"].ToString()); %>" />Sub Division</td>
									        <td><select id="subdivision" name="subdivision" style="width: 150px; height:20px;"><option value="select">----------Select------------</option> </select></td>
									        <td>Binder No</td>
									        <td><input type="text" id="binderno" name="binderno" size="8" maxlength="4" onkeypress ="return OnlyNumber(event);" /></td>
									        <td>Account No</td>
									        <td><input type="text" id="accountno" name="accountno" size="8" maxlength="4" onkeypress ="return OnlyNumber(event);"/></td>
									        <td><button type="button" class="button red small submit alignleft" id="search">
                                                SEARCH</button></td>
									        <td><button type="button" class="button red small submit alignleft" id="GetNo" onclick="return GetNo_onclick()">Know 
                                                ACCNO</button></td>
                                                <td><button type="button" class="button orange  small submit alignleft" id="multiple"> 
                                                Multiple</button></td>
									    </tr>
									</table>
									</fieldset>
									<fieldset id="consumer" >
									    <legend>Consumer Details</legend>
									    <fieldset>
									    <table class="display" >
									        <tr>
									            <td ><label class="lblfield" >Name</label></td>
									            <td><label class="lblvalue" id="name">-</label></td>
									            <td><label class="lblfield"> Bill Month</label></td>
									            <td><label class="lblvalue" id="billmonth">-</label></td>
									            <td> <label class="lblfield" >Due Date</label ></td>
									            <td><label class="lblvalue" id="duedate">-</label ></td>
									        </tr>
									        <tr>
									            <td ><label class="lblfield"  >Father Name</label></td>
									            <td><label class="lblvalue" id="fathername">-</label></td>
									            <td><label class="lblfield" >Bill Amount</label></td>
									            <td><label class="lblvalue" id="billamt">-</label ></td>    
									            <td ><label class="lblfield">Amount After Due Date </label></td>
									            <td ><label class="lblvalue" id="dueamt" >-</label ></td>
									            
									            
									        </tr>
									        <tr>
									            <td><label class="lblfield" >Address</label></td>
									            <td colspan="5"><label class="lblvalue"  id="address">-</label ></td>
                                                
									        </tr>
									        
									        </table>   
									    </fieldset>
									<fieldset>
									    <legend>Payment Details</legend>
									    <table class="fieldtable">
									        <tr>
									            <td><input type="hidden" id="billid" name="billid"/><label class="lblfield" >Pay Towards</label></td>
									            <td><select id="paymenttype" style="width: 150px; height:20px;"><option value="7">Energy</option></select></td>
									            <td><label class="lblfield" > Mode</label></td>
									            <td><select id="mode" style="height:20px"><option value="Cash" style="width: 100px; height:20px;" >Cash</option><option value="Bank">Cheque</option> </select></td>
									            <td><label class="lblfield" >Amount</label></td>
									            <td><input type="text" name="amt" id="amt" size="10" maxlength="10" onkeypress ="return OnlyNumber(event);" /></td>
									            </tr><tr>
									            <td><label class="lblfield" >Mobile No</label></td>
									            <td><input type="text" name="phoneno" id="phoneno" size="15" maxlength="10" onkeypress ="return OnlyNumber(event);"/></td>
									            <td><label class="lblfield" >Email Id</label></td>
									            <td><input type="text" name="EmailId" id="EmailId" size="15" maxlength="30" /></td>
									            </tr><tr>
									            <td><label class="lblfield" >Bank Name</label></td>
									            <td><select id="bank" disabled="disabled" style="width: 150px; height:20px;"><option value="select" >-------------Select----------</option></select></td>
									            <td><label class="lblfield" >Cheque Date</label></td>
									            <td><input  type="text" name="chequedate" disabled="disabled" id="chequedate" size="10" readonly="readonly" /></td>
									            <td><label class="lblfield"  >Cheque No</label></td>
									            <td><input type="text" name="chequeno" id="chequeno" disabled="disabled" onkeypress ="return OnlyNumber(event);" size="10"  maxlength="6" /></td>
									            
									            <td colspan="2" align="right"><button type="button" class="button green small right" id="save" >SUBMIT</button></td>
									        </tr>
									    </table>
									</fieldset>
									</fieldset>
									<fieldset>
									    <legend>Transaction Details</legend>
									    <table class="fieldtable">
									        <tr>
									            <td><label class="lblfield" >Last Receipt No</label></td>
									            <td><label class="lblfield" >Last Receipt Amount</label></td>
									            <td><label class="lblfield" >Total Receipts</label></td>
									            <td><label class="lblfield" >Total Cash</label></td>
									            <td><label class="lblfield" >Total Cheques</label></td>
									            <td><label class="lblfield" >Total DD/Cheque Amt</label></td>
									            
									            
									            <td><label class="lblfield" >Total Amount</label></td>
									        </tr>
									        <tr>
									            <td><label id="LastReceiptNo" class="lblvalue" >0</label></td>
									            <td><label id="LastReceiptAmount" class="lblvalue" >0</label></td>
									            <td><label id= "TotalReceipt" class="lblvalue" >0</label></td>
									            <td><label id="TotalCash" class="lblvalue" >0</label></td>
									            <td><label id="NoofCheques" class="lblvalue" >0</label></td>
									            <td><label id ="TotalCheque" class="lblvalue" >0</label></td>
									                    
									            <td><label id="TotalAmount" class="lblvalue" >0</label></td>
									        </tr>
									    </table>
									</fieldset>
								</div>
							</div>
						<%--</div>
					</div>--%>
	   <input type="hidden" id="hfdoc"   value="D:\\Working Project\\FinnalJVVNL\\JVVNL\\JVVNLWeb\\JVVNLWeb\\files\\anil.txt"/>
            <input type="hidden" id="hidprint"  value="HI Test Printer" />
	<div id="Print" style="display:none">
	  
	</div>
					<script type="text/javascript"> 
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
                                    addoption1 = $('<option></option>').attr("value", splitoption[0]).text(splitoption[1]);
                                    $("#subdivisionsearch").append(addoption1); 
                                }   
                         }
					    }
					    function PopulateBank()
					    {
                            $.post("handler/bank.ashx?type=populate",$("#form").serialize(),ShowBank);                        					    
					    }
					    function ShowBank(result)
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
                                    $("#bank").append(addoption); 
                                }   
                         }
					    }
					    function PopulatePaymentType()
					    {
                            $.post("handler/paymenttype.ashx?type=populate",$("#form").serialize(),ShowPaymentType);                        					    
                                Default();
					    }
					    function ShowPaymentType(result)
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
                                    $("#paymenttype").append(addoption); 
                                }   
                           
                         }
					    }
					    $("#save").click(function (){
					   // alert("Save");
					        var billid = document.getElementById("billid").value   ;
					        var paymenttype = document.getElementById("paymenttype").value   ;
					        var mode = document.getElementById("mode").value   ;
					        var amt = document.getElementById("amt").value   ;
					        var phoneno = document.getElementById("phoneno").value   ;
					        var bankid= document.getElementById("bank").value   ;
					        var chequedate = document.getElementById("chequedate").value   ;
					        var chequeno=document.getElementById("chequeno").value;
					        var username=document.getElementById("username").value;
					        var EmailId = document.getElementById("EmailId").value   ;
					        if (document.getElementById("name").innerHTML=="-" )
					        {
					            alert("Search value for payment"); 
					           document.getElementById("subdivision").focus();
					            return ;   
					        }
					        if (paymenttype =="select" || paymenttype =="0")
					        {
					            alert("Select Payment Type"); 
					            document.getElementById("paymenttype").focus();
					            return ;   
					        }
					        if (amt=="")
					        {
					            alert("Enter Amount"); 
					            document.getElementById("amt").focus();
					            return ;   
					        }
					        
					        if (parseInt(amt) <= 0)
					            {
					                alert("Enter valid Amount"); 
					                document.getElementById("amt").focus();
					                return ;   
					            }
//					        if (phoneno=="")
//					        {
//					            alert("Enter Phone No"); 
//					            document.getElementById("phoneno").focus();
//					            return ;   
//					        }
//					        if (phoneno.length != 10)
//					        {
//					            alert("Phone No should be 10 digit long"); 
//					            document.getElementById("phoneno").focus();
//					            return ;   
//					        }
					         if (EmailId.length > 0)
					         {
					     
					              var expre=/^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
					  
					                if (expre.test(EmailId)==false)
                                     {
                                        alert("Enter valid Email ID");
                                        document.getElementById("EmailId").focus();
                                        return ;
                                     } 
					         }
					        
					        if (mode=="Bank")
					        {
					            if (bankid=="select" || bankid=="0")
					            {
					                alert("Select Bank"); 
					                document.getElementById("bank").focus();
					                return ;   
					            }
					            if (chequedate =="")
					            {
					                alert("Enter Cheque Date"); 
					                document.getElementById("chequedate").focus();
					                return ;   
					            }
					            if (chequedate !="" )
					            {
					                var currentDate = new Date();
                                    var getm = currentDate.getMonth();
                                    var add = parseInt(getm)+parseInt(1);
                                    var newcurdt = currentDate.getDate() +"/"+ add +"/" + currentDate.getFullYear();
					                 if (daysBetween( newcurdt , chequedate)>0) 
                                     {
                                        alert("No Future date is Allowed in cheque date");
                                        document.getElementById("chequedate").focus();
                                        return;
                                    }
                                    else
                                    {
                                        if (daysBetween(chequedate,newcurdt)> 89)
                                        {
                                        alert("Cheque date should not be 89 days old")
                                        document.getElementById("chequedate").focus();
                                        return;
                                        }
                                    }
					            }
					            if (chequeno=="")
					            {
					                alert ("Enter cheque No");
					                document.getElementById("chequeno").focus();
					                return ;   
					            }
					            if (chequeno.length != 6)
					            {
					                alert("Cheque No should be 6 digit long"); 
					                document.getElementById("chequeno").focus();
					                return ;   
					            }
					        }
					       $.post("handler/payment.ashx?type=insert_single&form=single&BillID=" + billid   + "&paymenttype=" + paymenttype  + "&mode=" + mode + "&amount=" + amt + "&bankid=" + bankid + "&chequedate=" + chequedate + "&chequeno=" + chequeno + "&phoneno=" + phoneno +"&username=" + username+"&EmailId=" + EmailId , $("#form").serialize(),ShowPayment);   
					      
					       // ShowPayment("success#21")                     					    
					    });
					    function ShowPayment(result)
					    {

					        var data= result.toString().split("#");  
					              alert(data[0]);
					        if (data[0].indexOf("success")>=0)
					        {
					            if (data.length>2)
					            {
					                alert(data[2]);
					            }
					           // $.post("handler/payment.ashx?type=pdf&paymentid="+ data[1], $("#form").serialize(),ShowPaymentReceipt);                        					    					            
					           // $.post("crreport.aspx?ID="+ data[1], $("#form").serialize(),ShowPaymentReceipt);                        					    					            
//					             window.open('crreport.aspx?ID='+ data[1],'','width=200,height=100');
					            var div = document.getElementById("Print");
					            div.innerHTML = "<iframe src='crreport.aspx?ID=" + data[1] + "'  onload='this.contentWindow.print();'></iframe>";

					            ClearControls(); 
					           changemode();
					           PopulatePaymentSummary(); 
					       }
					       else
					       {
					        document.getElementById("paymenttype").focus(); 
					       }
					    }
					      function FunPrint()
                            {
                            alert('FunPrint');
                          var hfdoc=document.getElementById("hfdoc").value;
					        var hidprint = document.getElementById("hidprint").value   ;
	                           // window.open(hfdoc,"SalarySlip","toolbar=n,scrollbars=Yes,location=n,status=n,menubar=n,resizable=yes,width=780,height=580,left=0,top=0,offscreenBuffering=true")
	                            var fso, tf;
	                          
	                            fso = new ActiveXObject("Scripting.FileSystemObject");
	                              alert(fso);
	                            //alert(hfdoc)
	                            tf = fso.CreateTextFile("LPT1:",true);
	                           // tf.Writeline (hidprint);
	                             alert(fso);
	                            tf.Close();
	                             alert(hidprint)
                            }

					    function ShowPaymentReceipt(result)
					    {   alert('printername');
					    //FunPrint();
					           alert(result);
					           ClearControls(); 
					           changemode();
					           PopulatePaymentSummary(); 
//					           var WinPrint = window.open('', '', 'letf=0,top=0,width=1,height=1,toolbar=0,scrollbars=0,status=0');
//                               WinPrint.document.write(result);
//                               WinPrint.document.close();
//                               WinPrint.focus();
//                               WinPrint.print();
//                               WinPrint.close();
					        
					    }
					    function printpdf(filepath)
					    {
					     if (result.toString().indexOf("#")>=0 )
					        {
					         var data= result.split('#');
					         document.getElementById("billid").value=data[0];
					         document.getElementById("name").innerHTML=data[8]; 
					         document.getElementById("billmonth").innerHTML=data[2] + "/" + data[3]; 
					        document.getElementById("duedate").innerHTML=data[5] ; 
					       
					        document.getElementById("fathername").innerHTML=data[9] ; 
					        document.getElementById("billamt").innerHTML= Math.round(  data[6],2) ; 
					        document.getElementById("dueamt").innerHTML=Math.round( data[7]) ; 
					        document.getElementById("address").innerHTML=data[10] ; 
					        document.getElementById("amt").value =Math.round(  data[6],2) ; 
					        document.getElementById("amt").disabled =true;
					        document.getElementById("phoneno").value =data[11];
					         document.getElementById("paymenttype").value ="7";
					        if (document.getElementById("phoneno").value!="")
					        {
					            document.getElementById("phoneno").readOnly=true;
					        }
					        }
					        else
					        {
					            alert(result );
					            document.getElementById("billid").value="";
					            document.getElementById("name").innerHTML="-" 
					            document.getElementById("billmonth").innerHTML="-" 
					            document.getElementById("duedate").innerHTML="-"; 
					            document.getElementById("fathername").innerHTML="-"; 
					            document.getElementById("billamt").innerHTML= "-" ; 
					            document.getElementById("dueamt").innerHTML="-" ; 
					            document.getElementById("address").innerHTML="-"; 
					            document.getElementById("amt").value ="" ; 
					            document.getElementById("phoneno").value="";
					            
					        }
					        document.getElementById("paymenttype").focus();  
					    
					    }
					    function ShowSMSMessage(result)
					    {
					        alert(result);
					        ClearControls();
					    }
					    function ClearControls()
					    {
					        document.getElementById("billid").value ="";
					        document.getElementById("paymenttype").value ="select";
					        document.getElementById("mode").value="Cash";
					        document.getElementById("amt").value="";
					        document.getElementById("phoneno").value ="";
					          document.getElementById("EmailId").value ="";
					        document.getElementById("bank").value="select";
					        document.getElementById("chequedate").value ="";
					        document.getElementById("chequeno").value ="";
					        document.getElementById("billid").value="";
					        document.getElementById("name").innerHTML="-" 
					        document.getElementById("billmonth").innerHTML="-" 
					        document.getElementById("duedate").innerHTML="-"; 
					        document.getElementById("fathername").innerHTML="-"; 
					        document.getElementById("billamt").innerHTML= "-" ; 
					        document.getElementById("dueamt").innerHTML="-" ; 
					        document.getElementById("address").innerHTML="-"; 
					        document.getElementById("subdivision").value ="select";
					        document.getElementById("binderno").value="";
					        document.getElementById("accountno").value ="";
					        document.getElementById("subdivision").focus();
					        changemode();
					    }
					    $("#search").click(function(){
					    var subdivision = document.getElementById("subdivision").value   ;
					    var binderno = document.getElementById("binderno").value   ;
					    var accountno = document.getElementById("accountno").value   ;
					    if (subdivision =="select" || subdivision=="0")
					    {
					        alert("Select Subdivision");
					        document.getElementById("subdivision").focus();
					        return ;
					        
					    }
					    if (binderno=="")
					    {
					        alert("Enter Binder No");
					        document.getElementById("binderno").focus();
					        return ;
					    }
					    if (binderno.length != 4)
					    {
					        alert("Binder No should be 4 digit long");
					        document.getElementById("binderno").focus();
					        return ;
					    } 
					    if (accountno =="")
					    {
					        alert("Enter Account No");
					        document.getElementById("accountno").focus();
					        return ;
					    }
					    if (accountno.length != 4)
					    {
					        alert("Account No should be 4 digit long");
					        document.getElementById("accountno").focus();
					        return ;
					    } 
					     $.post("handler/payment.ashx?type=billing&subdivision=" + subdivision  + "&binderno=" + binderno + "&accountno=" + accountno  , $("#form").serialize(),ShowBillingData);                        					    
					    });
					    function ShowBillingData(result)
					    {
					     
					       document.getElementById("phoneno").readOnly=false;
					        if (result.toString().indexOf("#")>=0 )
					        {
					         var data= result.split('#');
					        
					         document.getElementById("billid").value=data[0];
					         //alert(data[13]);
					      //   data[1].substr(0,4)=2106; where data[1]=21060011;
					     // data[1].substr(4) = 0011;where data[1]=21060011;
					         document.getElementById("accountno").value=data[1].substr(4);
					         document.getElementById("binderno").value=data[1].substr(0,4);
					         document.getElementById("subdivision").value=data[13];
					         document.getElementById("name").innerHTML=data[8]; 
					         document.getElementById("billmonth").innerHTML=data[2] + "/" + data[3]; 
					        document.getElementById("duedate").innerHTML=data[5] ; 
					        
					        document.getElementById("fathername").innerHTML=data[9] ; 
					        document.getElementById("billamt").innerHTML= Math.round(  data[6],2) ; 
					        document.getElementById("dueamt").innerHTML=Math.round( data[7]) ; 
					        document.getElementById("address").innerHTML=data[10] ; 
		                    document.getElementById("amt").value =Math.round(  data[12],2) ; 
					        document.getElementById("amt").disabled =true;
					        document.getElementById("phoneno").value = data[11];
					        document.getElementById("mode").disabled = false;
					        if (document.getElementById("amt").value > 20000)
					        {
					            document.getElementById("mode").value="Bank";
					            document.getElementById("bank").disabled = false;
					            document.getElementById("mode").disabled = true;
					            document.getElementById("chequedate").disabled = false;
					            document.getElementById("chequeno").disabled = false;
					        }
					        else
					        {
					            document.getElementById("mode").value="Cash";
					            document.getElementById("bank").disabled = true;
					            document.getElementById("chequedate").disabled = true;
					            document.getElementById("chequeno").disabled = true;
					        }
   			                document.getElementById("paymenttype").value ="7";
					        
					        if (document.getElementById("phoneno").value!="")
					        {
					            document.getElementById("phoneno").readOnly=true;
					        }
					        }
					        else
					        {
					            alert(result );
					            document.getElementById("billid").value="";
					            document.getElementById("name").innerHTML="-" 
					            document.getElementById("billmonth").innerHTML="-" 
					            document.getElementById("duedate").innerHTML="-"; 
					            document.getElementById("fathername").innerHTML="-"; 
					            document.getElementById("billamt").innerHTML= "-" ; 
					            document.getElementById("dueamt").innerHTML="-" ; 
					            document.getElementById("address").innerHTML="-"; 
					            document.getElementById("amt").value ="" ; 
					            document.getElementById("phoneno").value="";
					            
					        }
					        document.getElementById("paymenttype").focus();  
					    }
//					    $("#AccCode").dialog({
//                            autoOpen: false,
//                            title: 'Search',
//                            modal: true,
//                            autoresize:true ,
//                            width:750,
//                            height:300
////                            z-index:1001
//                        }); 
                        $("#GetNo").click(function(){
                         $("#AccCode").dialog({
                            autoOpen: false,
                            title: 'Search',
                            modal: true,
                            autoresize:true ,
                            width:750,
                            height:300
//                            z-index:1001
                        }); 
                            $("#AccCode").dialog('open');
                        });
                        function Populate(result)
                        {
                        
                            var data= result.toString().split("#");  
                            var subdivision = data[0];
					        var binderno = "" ;
					        var accountno = data[1];
                            $.post("handler/payment.ashx?type=billing&subdivision=" + subdivision  + "&binderno=" + binderno + "&accountno=" + accountno  , $("#form").serialize(),ShowBilling);                        					    
                        }
                        function ShowBilling(result)
                        {
                            ShowBillingData(result);
                            $("#AccCode").dialog('close');
                        }
                        $("#searchacc").click(function(){
                            var subdivision= document.getElementById("subdivisionsearch").value ;
                            var name="";
                            var accountno="";
                            if (document.getElementById("searchby").value=="name") 
                             name=document.getElementById("searchtext").value ;   
                            else
                             accountno=document.getElementById("searchtext").value ;   
                            $.post("handler/payment.ashx?type=consumerselect&subdivision=" + subdivision  + "&name=" + name + "&accountno=" + accountno  , $("#form").serialize(),ShowAccSearchResult);                        					    
                        });
                        function ShowAccSearchResult(result)
                        {
                            $("#basictable > tbody").html("");
					        $("#basictable").append(result );


					    }

					    function showmultipledelete(result) { 
					    
					    }
                         function PopulatePaymentSummary()
					    {
					    $.post("handler/payment.ashx?type=summary", $("#form").serialize(),ShowSummary);                        					    
					    }
					    function checkclosedtrasction()
					    {
					     var username=document.getElementById("username").value;
					     $.post("handler/payment.ashx?type=checkclosedtrasction&user="+ username, $("#form").serialize(),disblebtn); 
					    }
					    function disblebtn(result)
					    {
					   
					      if (result!='1')
					      {
					        //alert("result!=1 " + result)
					        document.getElementById("save").disabled =true;
					        alert("Please Close Trasaction of "+result);
					      }
					    }
					    function ShowSummary(result)
					    {
					        var data=result.toString().split("#"); 
					        document.getElementById("LastReceiptNo").innerHTML=data[0]  ;
					        document.getElementById("LastReceiptAmount").innerHTML=data[1]  ;
					        document.getElementById("TotalCash").innerHTML=data[2]  ;
					        document.getElementById("TotalCheque").innerHTML=data[3]  ;
					        document.getElementById("TotalReceipt").innerHTML=data[4]  ;
					        document.getElementById("NoofCheques").innerHTML=data[5]  ;
					        document.getElementById("TotalAmount").innerHTML=data[6]  ;
					        
					    }
					    $("#multiple").click(function(){
					        location.replace("multiple.aspx");
					    });
					   
					    $("#mode").keyup(function(){
					        changemode();
					    });
					    $("#mode").change(function(){
					        changemode();
					    });
					    function changemode() {
					        document.getElementById("mode").disabled = false;
					        if (document.getElementById("mode").value=="Cash")  
					        {
					            
					            document.getElementById("bank").value="select";
					            document.getElementById("chequedate").value ="";
					            document.getElementById("chequeno").value ="";
					            document.getElementById("bank").disabled=true;
					            document.getElementById("chequedate").disabled=true;
					            document.getElementById("chequeno").disabled =true;
					            document.getElementById("amt").disabled =true;
					        }
					        else
					        {
					            document.getElementById("bank").disabled=false;
					            document.getElementById("chequedate").disabled=false;
					            document.getElementById("chequeno").disabled =false;
					            document.getElementById("amt").disabled =true;
					        }
					    }
					        function daysBetween(first, second) {
					    
					    var firstdata=first.split("/")
					    var seconddata=second.split("/")

                            // Copy date parts of the timestamps, discarding the time parts.
                            var one = new Date(firstdata[2], firstdata[1]-1, firstdata[0]);
                            var two = new Date(seconddata[2], seconddata[1]-1, seconddata[0]);
                        
                            // Do the math.
                            var millisecondsPerDay = 1000 * 60 * 60 * 24;
                            var millisBetween = two.getTime() - one.getTime();
                            var days = millisBetween / millisecondsPerDay;
    
                            // Round down.
                            
                            return Math.floor(days);
                    }


					</script>
</asp:Content>
