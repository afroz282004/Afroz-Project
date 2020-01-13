<%@ Page Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="multiple.aspx.cs" Inherits="JVVNLWeb.multiple" Title="Payment - Multiple" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentplaceholder1" runat="server">

    <script type="text/javascript">
jQuery(document).ready(function() {
      var url = "CheckSession.aspx?hgUserName=" + document.getElementById("username").value;
         $(document).idleTimeout({ inactivity: 600000, noconfirm: 1000, sessionAlive: 600000, redirect_url: url });
    $("#chequedate").datepicker({dateFormat:'dd/mm/yy'});
    oTable = $('#basictable').dataTable({
					        "bJQueryUI": true,
					        "sPaginationType": "full_numbers"
					    });
					    
					      
    checkclosedtrasction();	        					        
    PopulateSDO();
	PopulateBank();
	PopulatePaymentType();
	PopulatePaymentSummary();
	DisableFields();
	//document.getElementById('complete').disabled = true;
	
	
	
//					        var bankid= '8'  ;
//					        var chequeno='789877';
//					        $.post("handler/payment.ashx?type=chequeselect&bankid=" + bankid + "&chequeno=" + chequeno , $("#form").serialize(),ShowRecordsTable);                        					    
//	  
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
<div id="AccCode" style="display:none">
					    <table class="fieldtable">
					        <tr>
					            <td>Subdivision</td>
					            <td><select id="subdivisionsearch" name="subdivision" style="width: 150px; height:20px;"><option value="select">----------Select------------</option> </select></td>
					            <td>Search By</td>
					            <td><select id="searchby" style="width: 150px; height:20px;"><option value="name" selected="selected">Name</option><option value="accountno">Account No</option></select></td>
					            <td><input type ="text" id="searchtext" /></td>
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
										<tbody id="tbody1">
										</tbody>
									</table>
					    </div>
					</div>
<div class="grid_12">
						<div class="box">
							<h2>
								Make Payment - Multiple
								<span class="l"></span><span class="r"></span>
							</h2>
							<div class="block">
								<div class="form_place"  id="form1">
									<fieldset>
									    <legend>Bank Details</legend>
									
									<table class="fieldtable" >
									    <tr>
									        <td><input type="hidden" id="username" value="<% Response.Write(Session["username"].ToString()); %>" /><label class="lblfield" >Bank Name</label></td>
									        <td><label class="lblfield" >Cheque Date</label></td>
									        <td><label class="lblfield" >Cheque No</label></td>
									        <td><label class="lblfield" >Amount</label></td>
									        <td><label class="lblfield" >Mobile No</label></td>
									           <td><label class="lblfield" >Email Id</label></td>
									        <td><label class="lblfield" >Balance</label></td>
									        <td><button type="button" class="button orange  small submit alignleft" id="single"> 
                                                Single</button></td>
									    </tr>
									    <tr>
									     <td><input type="hidden" id="hbank" value="" runat ="server"  /><input type="hidden" id="receipt" value="" />
									     <select id="bank" style="width: 150px; height:20px;" ><option value="select">------------Select-----------------</option></select></td>
									     <td><input type="text" name="chequedate" id="chequedate"  size="10" readonly="readonly" /></td>
									     <td><input type="text" name="chequeno" id="chequeno" onkeypress ="return OnlyNumber(event);" size="10"  maxlength="6" /></td>
									     <td><input type="text" name="amt" id="amt" maxlength="10" size="10" onkeypress ="return OnlyNumber(event);" /></td>
									     <td><input type="text" name="phoneno" id="phoneno" maxlength="10" onkeypress ="return OnlyNumber(event);" /></td>
									      <td><input type="text" name="EmailId" id="EmailId" size="15" maxlength="30" /></td>
									     <td><label id="ramount" class="lblvalue">0</label><input type="hidden" name="paymentdata" id="paymentdata" /> </td>
									     <td><center><button type="button" class="button green small" id="add">SAVE</button><button type="button" class="button green small" id="reset">Reset</button></center></td>
									     
									    </tr>
									 
									           
									</table>
									</fieldset>
									<fieldset id="multiple" >
									    <legend>Multiple Receipts</legend>
									    
									
									    <table class="fieldtable">
									        <tr>
									            <td><label class="lblfield" >Subdivision Code</label></td>
									            <td><select id="subdivision" name="subdivision" style="width: 150px;height:20px;"><option value="select">----------Select------------</option> </select></td>
									            <td><label class="lblfield" >Account No</label></td>
									            <td ><input type="text" id="accountno" name="accountno" size="20" maxlength="8" onkeypress ="return OnlyNumber(event);" /></td>
									            <td colspan="2"><button type="button" class="button red small submit alignleft" id="show">show</button>
									            <button type="button" class="button red small submit alignleft" id="GetNo">Know ACCNO</button></td>
									            </tr><tr>
									            <td><input type="hidden" id="billid" name="billid"/><label class="lblfield" >Name</label></td>
									            <td><input type="text" name="payname" id="payname"  readonly="readonly"/></td>
									            <td><label class="lblfield" >Amount</label></td>
									            <td><input type="text" name="payamt" id="payamt" maxlength="10" disabled="disabled" onkeypress ="return OnlyNumber(event);"  /></td>
									            <td><label class="lblfield" >Pay Towards</label></td>
									            <%--<td><select id="paymenttype"><option value="select">----------Select------------</option></select></td>--%>
									            <td><select id="paymenttype" style="width: 150px; height:20px;"><option value="7">Energy</option></select></td>
									            
									            <td><center><button type="button" class="button green small" id="save" onclick="return save_onclick()">SAVE</button></center></td>
									            
									        </tr>
									    </table>
									
									
									
									</fieldset>
									<fieldset>
									    <legend>Show Receipts</legend>
									    	<table class="table display" id="table">
										<thead>
                                                <tr>
                                                   <th>S.No</th>
                                                   <th>SDO Code</th>
                                                   <th>Acc No</th>
                                                   <th>Recd Date</th>
                                                   <th>Towards</th>
                                                   <th>Amount</th>
                                                   <th>Delete</th>                                                    
                                                </tr>
                                            </thead>
                                            <tbody id="tbody">
										
										    </tbody>
									</table>
									    <center>									        
									        <center><%--<asp:Button class="button green small" id="complete" runat="server" Text="Complete"    OnClientClick="javascript:return storevalue();" />--%>
									        <button type="button" class="button green small" id="complete">Submit</button>
									        </center>
									        <%--<center><asp:Button class="button green small" id="Button1" runat="server" Text="Complete"   OnClick="complete_onclick" OnClientClick="javascript:return storevalue();" /></center>--%>
									    </center>
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
						</div>
					</div>
					
					<div id="Print" style="display:none">
	  
	</div>
					<script type="text/javascript">
					
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
					    $("#complete").click(function() {

					        var grid = document.getElementById('table');
					        var bills;
					        var totalgridamt = 0;
					        var newamt = document.getElementById("ramount").innerHTML;
					        if (grid.rows.length > 1 && newamt != 0) {
					            alert('Cheque Amount is not fully utilized ');
					            return false;
					        }
					        if (grid.rows.length > 1) {
					            if (grid.rows[1].cells[0].innerHTML != "No Record Found") {
					                var paymentdata = document.getElementById("paymentdata").value;
					                var paydata = paymentdata.split("$");
					                for (i = 0; i < paydata.length - 1; i++) {
					                    var data = paydata[i].split("#");
					                    var billid = data[0];
					                    var mode = data[1];
					                    var amt = data[2];
					                    var bankid = data[3];
					                    var chequedate = data[4];
					                    var chequeno = data[5];
					                    var phoneno = data[6];
					                    var paymenttype = data[7];
					                    var username = data[8];
					                    var EmailId = data[9];
					                    document.getElementById("receipt").value = document.getElementById("receipt").value + "," + data[0];
					                    $.post("handler/payment.ashx?type=insert&form=multiple&BillID=" + billid + "&paymenttype=" + paymenttype + "&mode=" + mode + "&amount=" + amt + "&bankid=" + bankid + "&chequedate=" + chequedate + "&chequeno=" + chequeno + "&phoneno=" + phoneno + "&username=" + username + "&EmailId=" + EmailId, $("#form").serialize(), ShowPayment);
					                }
					                document.getElementById("paymentdata").value = "";
					                ClearFields();
					                document.getElementById("phoneno").value = "";
					                document.getElementById("bank").value = "select";
					                document.getElementById("EmailId").value = "";
					                document.getElementById("chequedate").value = "";
					                document.getElementById("chequeno").value = "";
					                document.getElementById("amt").value = "";

					                document.getElementById("phoneno").disabled = false;
					                document.getElementById("bank").disabled = false;
					                document.getElementById("EmailId").disabled = false;
					                document.getElementById("chequedate").disabled = false;
					                document.getElementById("chequeno").disabled = false;
					                document.getElementById("amt").disabled = false;

					                DisableFields();
					                $("#table > tbody").html("");
					                //document.getElementById('complete').disabled = true;


					            }
					            //ShowReceiptdata();
					            alert("Payment Added Successfully!!!");

					        }
					        // GenerateReceipt();  
					    })

//					    function ShowReceiptdata() {
//					        var data = document.getElementById("receipt").value;
//					        var splitdata = data.split(",");
//					        $.post("handler/payment.ashx?type=billids&billids=" + data, $("#form").serialize(), ShowPrintReceipt);
//					    }
//					    function ShowPrintReceipt(result) {
//					        
//					        if (result != "")
//					        {
//					            var splitdata = result.toString().split(",")

//					            for (var i = 1; i < splitdata.length; i++) {

//					                GenerateReceipt(splitdata[i]);
//					                
//					                alert("Printing " + splitdata[i]);
//					            }
//					        }
//					        
//					     }

					
					    $("#save").click(function() {
					        var sdocode = document.getElementById("subdivision").value;
					        var billid = document.getElementById("billid").value;
					        var paymenttype = document.getElementById("paymenttype").value;
					        var mode = "bank";
					        var amt = document.getElementById("payamt").value;
					        var phoneno = document.getElementById("phoneno").value;
					        var bankid = document.getElementById("bank").value;
					        var chequedate = document.getElementById("chequedate").value;
					        var chequeno = document.getElementById("chequeno").value;
					        var username = document.getElementById("username").value;
					        var ramt = document.getElementById("ramount").innerHTML;
					        var EmailId = document.getElementById("EmailId").value;
					        var accno = document.getElementById("accountno").value;

					        if (amt == "") {
					            alert("Enter Amount");
					            document.getElementById("payamt").focus();
					            return;
					        }
					        if (accno.length != 8) {
					            alert(accno);
					            alert("Account No Should Be 8 Digit");
					            document.getElementById("accountno").focus();
					            return;
					        }

					        if (parseInt(amt) <= 0) {
					            alert("Enter valid Amount");
					            document.getElementById("payamt").focus();
					            return;
					        }
					        if (parseInt(ramt) - parseInt(amt) < 0) {
					            alert("Balace amount is less than available amount");
					            document.getElementById("payamt").focus();
					            return;
					        }
					        if (paymenttype == "select" || paymenttype == "0") {
					            alert("Select Payment Type");
					            document.getElementById("paymenttype").focus();
					            return;
					        }
					        if (EmailId.length > 0) {

					            var expre = /^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;

					            if (expre.test(EmailId) == false) {
					                alert("Enter valid Email ID");
					                document.getElementById("EmailId").focus();
					                return;
					            }
					        }
					        var paytype=document.getElementById("paymenttype");
					        var paytypetext=paytype.options[paytype.selectedIndex].innerHTML;
					        var currentDate = new Date();
					        var newcurdt = currentDate.getMonth()+1 + "/" + currentDate.getDate() + "/" + currentDate.getFullYear();
					        var data=sdocode + "#" + accno + "#" + newcurdt +"#" + paytypetext + "#" + amt ;
					        var paymentdata=billid +"#" + mode +"#" + amt +"#" + bankid +"#" + chequedate +"#" + chequeno +"#" + phoneno +"#" + paymenttype +"#" + username +"#" + EmailId +"$";
					        if (document.getElementById("paymentdata").value.indexOf(billid +"#")>=0)
					        {
					            alert("Data Already exists")
					            document.getElementById("subdivision").focus();
					            return;
					            
					        }  
					        document.getElementById("paymentdata").value= document.getElementById ("paymentdata").value + paymentdata ;
					        ShowReceipt(data);
					        //$.post("handler/payment.ashx?type=insert&form=multiple&BillID=" + billid + "&paymenttype=" + paymenttype + "&mode=" + mode + "&amount=" + amt + "&bankid=" + bankid + "&chequedate=" + chequedate + "&chequeno=" + chequeno + "&phoneno=" + phoneno + "&username=" + username + "&EmailId=" + EmailId, $("#form").serialize(), ShowPayment);
					    });
					    function ShowReceipt(data)
					    {
					        var val=$("#table > tbody").html();
					        var result="";
					        if (val=="<tr><td>No Record Found</td><td></td><td></td><td></td><td></td><td></td><td></td></tr>")
					        {
					            $("#table > tbody").html("");
					        }
					        else
					        {
					            result=val;
					        }
					        var count=result.split("<tr>");
					        var amt = document.getElementById("payamt").value;
					        result = result + "<tr><td>" + count.length +"</td><td>" + data.replace(/#/g,"</td><td>") + "</td><td><img src='images/icon_delete.png' alt='reset' onclick='DeleteRecord(" + count.length + "," + amt  + ")'></td></tr>";
					        $("#table > tbody").html("");
					        $("#table").append(result);
					        var ramount = document.getElementById("ramount").innerHTML;
					        document.getElementById("ramount").innerHTML = parseInt(ramount) - parseInt(amt);
//					        if (document.getElementById("ramount").innerHTML == 0) 
//					                document.getElementById('complete').disabled = false;
//					        else
//					        {
//					                document.getElementById('complete').disabled = true;
					            
					        ClearFields();
					        
					    }
					    $("#add").click(function() {
					        var phoneno = document.getElementById("phoneno").value;
					        var bankid = document.getElementById("bank").value;
					        var chequedate = document.getElementById("chequedate").value;

					        var chequeno = document.getElementById("chequeno").value;
					        var amt = document.getElementById("amt").value;
					        var currentDate = new Date();
					        var getm = currentDate.getMonth();
					        var add = parseInt(getm) + parseInt(1);
					        var newcurdt = currentDate.getDate() + "/" + add + "/" + currentDate.getFullYear();
					        $.post("handler/payment.ashx?type=chequeselectnew&bankid=" + bankid + "&chequeno=" + chequeno + "&chequedate=" + chequedate, $("#form").serialize(), ShowRecordsTable);

					        if (chequedate != "") {

					            if (daysBetween(newcurdt, chequedate) > 0) {
					                alert("No Future date is Allowed in cheque date");
					                document.getElementById("chequedate").focus();
					                return;
					            }
					            else {
					                if (daysBetween(chequedate, newcurdt) > 89) {
					                    alert("Cheque date should not be 89 days old")
					                    document.getElementById("chequedate").focus();
					                    return;
					                }
					            }
					        }

					        if (bankid == "select" || bankid == "0") {
					            alert("Select Bank");
					            document.getElementById("bank").focus();
					            return;
					        }
					        if (chequedate == "") {
					            alert("Enter Cheque Date");
					            document.getElementById("chequedate").focus();
					            return;
					        }
					        if (chequeno == "") {
					            alert("Enter cheque No");
					            document.getElementById("chequeno").focus();
					            return;
					        }
					        if (chequeno == "000000") {
					            alert("cheque No '000000' is not allowed");
					            document.getElementById("chequeno").focus();
					            return;
					        }
					        if (chequeno.length != 6) {
					            alert("Cheque No should be 6 digit long");
					            document.getElementById("chequeno").focus();
					            return;
					        }
					        if (amt == "") {
					            alert("Enter Amount");
					            document.getElementById("amt").focus();
					            return;
					        }
					        if (parseInt(amt) <= 0) {
					            alert("Enter valid Amount");
					            document.getElementById("amt").focus();
					            return;
					        }
					        if (phoneno != "") {

					            if (phoneno.length != 10) {
					                alert("Phone No should be 10 digit long");
					                document.getElementById("phoneno").focus();
					                return;
					            }
					        }
					        document.getElementById("bank").disabled = true;
					        document.getElementById("chequedate").disabled = true;
					        document.getElementById("chequeno").disabled = true;
					        document.getElementById("amt").disabled = true;
					        document.getElementById("phoneno").disabled = true;
					        document.getElementById("EmailId").disabled = true;
					        document.getElementById("ramount").innerHTML = document.getElementById("amt").value;
					        EnableFields();
					        document.getElementById("subdivision").focus();
					        
					    });
					    function DisableFields()
					    {
					        document.getElementById("subdivision").disabled =true; 
					        document.getElementById("accountno").disabled =true; 
					        document.getElementById("payname").disabled =true;
					        document.getElementById("payamt").disabled =true; 
					        document.getElementById("paymenttype").disabled =true; 
					    }
					    function EnableFields()
					    {
					        document.getElementById("subdivision").disabled =false; 
					        document.getElementById("accountno").disabled =false; 
					         document.getElementById("payname").disabled =false; 
					        //document.getElementById("payamt").disabled =false; 
					        document.getElementById("paymenttype").disabled =false; 
					    }
					   
                        $("#GetNo").click(function(){
                         $("#AccCode").dialog({
                            autoOpen: false,
                            title: 'Search',
                            modal: true,
                            autoresize:true ,
                            width:750,
                            height:300
                        }); 
                            $("#AccCode").dialog('open');
                        });
                        function Populate(result)
                        {
                           
                            var data= result.toString().split("#"); 
                            //alert('Populate' + data[0]+"   Part2" + data[1]); 
                            var subdivision = data[0];
					        var binderno = "" ;
					        var accountno = data[1];
                            $.post("handler/payment.ashx?type=billing&subdivision=" + subdivision  + "&binderno=" + binderno + "&accountno=" + accountno  , $("#form").serialize(),ShowBilling);                        					    
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
                                    //  alert("subdivision value=" + splitoption[0]).text(splitoption[1])
                                    $("#subdivision").append(addoption); 
                                
                                    addoption1 = $('<option></option>').attr("value", splitoption[0]).text(splitoption[1]);
                                    $("#subdivisionsearch").append(addoption1); 
                                }   
                         }
					    }
					    function PopulatePaymentType()
					    {
                            $.post("handler/paymenttype.ashx?type=populate",$("#form").serialize(),ShowPaymentType);                        					    
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
                        function ShowBilling(result)
                        {
                            ShowBillingData(result);
                            $("#AccCode").dialog('close');
                        }
                        function ShowBillingData(result)
					    {
					       

					        if (result.toString().indexOf("#")>=0 )
					        {				
					           // alert('Multiple Select Result ' + result);       
					            var data= result.split('#');
					            document.getElementById("billid").value=data[0];
					            document.getElementById("accountno").value=data[1];
					            document.getElementById("payname").value=data[8]; 
					            document.getElementById("payamt").value =Math.round(  data[12],2) ; 
					          //  alert('data[13]' + data[13]);
					              document.getElementById("subdivision").value=data[13];
					        }
					        else
					        {
					      
					            alert(result );
					            document.getElementById("billid").value="";
					            document.getElementById("payname").value="-" 
					            document.getElementById("payamt").value ="" ; 
					        }
					        
					    }
					    $("#show").click(function(){
					        
					        var subdivision = document.getElementById("subdivision").value   ;
					        var binderno = ""  ;
					        var accountno = document.getElementById("accountno").value   ;
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
					     $.post("handler/payment.ashx?type=billing&subdivision=" + subdivision  + "&binderno=" + binderno + "&accountno=" + accountno  , $("#form").serialize(),ShowBillingData);                        					    
					    });
					    
					    function ShowPayment(result)
					    {
					        //alert(result);
					        var data = result.split("#");

					        //alert(document.getElementById("receipt").value);
					        GenerateReceipt(data[1]);
					        
					       PopulatePaymentSummary();
					    }
//					    function ShowPayment(result)
//					    {
//					       
//					        var data=result.toString().split("#");  
//					        alert(data[0]);
//					        if (data[0].indexOf("added")>=0) {
//					        var ramount=document.getElementById("ramount").innerHTML;
////					        if (ramount == 0) {
////					            document.getElementById('ctl00_contentplaceholder1_complete').disabled = false;
////					        }
////					        else {
//					            //document.getElementById('ctl00_contentplaceholder1_complete').disabled = true;
//					            var ramount = document.getElementById("ramount").innerHTML;
//					            var amt = document.getElementById("payamt").value;

//					            document.getElementById("ramount").innerHTML = parseInt(ramount) - parseInt(amt);
//					            if (document.getElementById("ramount").innerHTML == 0) {
//					            
//					                document.getElementById('ctl00_contentplaceholder1_complete').disabled = false;
//					            }
//					            else {
//					            
//					                document.getElementById('ctl00_contentplaceholder1_complete').disabled = true;
//					            }
//					            ShowRecords();
//					            ClearFields();
//					            PopulatePaymentSummary();
////					        }
//					             
//					       }
//					       else
//					       {
//					        document.getElementById("paymenttype").focus();
//					         document.getElementById('ctl00_contentplaceholder1_complete').disabled = true;
//					        }
//					        
//					        if((data[0].indexOf("added") > 30))
//					        {
//					        alert("Can Not Add Records More Than 30");
//					        document.getElementById('ctl00_contentplaceholder1_complete').disabled = true;
//					        }
//					    }
					    function ShowRecords()
					    {
					        var bankid= document.getElementById("bank").value   ;
					        var chequeno=document.getElementById("chequeno").value;
					        var chequedate=document.getElementById("chequedate").value;
					        $.post("handler/payment.ashx?type=chequeselect&bankid=" + bankid + "&chequeno=" + chequeno + "&chequedate=" + chequedate  , $("#form").serialize(),ShowRecordsTable);                        					    
					    }
					    
					    function ClearFields()
					    {
					        document.getElementById("subdivision").value="select";
					        document.getElementById("accountno").value="";
					        document.getElementById("payamt").value="";
					        document.getElementById("payname").value="";
					        //document.getElementById("paymenttype").value="select";
					    }
					    function ShowRecordsTable(result)
					    {
					        $("#table > tbody").html("");
					        $("#table").append(result );
					    }
					    function DeleteRecord(id,amt)
					    {
					        if (confirm("Are you sure you want to delete " + id)) {
					            var data= $("#table > tbody").html();
					            var datarow= data.split("</tr>");
					            var paymentdata= document.getElementById("paymentdata").value; 
					            var paydata=paymentdata.split("$");
					            var i;
					            var result="";
					            document.getElementById("paymentdata").value=""; 
					            var num=1;
					            for ( i=0; i < datarow.length -1; i++)
					            {
					                var dataid= datarow[i].split("<td>");
					                if (dataid[1].replace("</td>","") != id)
					                {
					                    document.getElementById("paymentdata").value=document.getElementById("paymentdata").value + paydata[i];
					                    var j=0;
					                    for (j=0; j < dataid.length; j++)
					                    {
					                        if (j==0)
					                            result = result + dataid[j];
					                        else if (j==1)
					                        {
					                            result = result + "<td>" + num + "</td>"; 
					                        }
					                        else
					                        {
					                           result = result + "<td>" + dataid[j]; 
					                        }
					                    }
					                    result = result +  "</tr>"; 
					                    num=num+1;
					                }
					            }
					            //alert(doument.getElementById("paymentdata").value);
					            $("#table > tbody").html("");
					            $("#table > tbody").html(result);
					            var ramount = document.getElementById("ramount").innerHTML;
					            document.getElementById("ramount").innerHTML = parseInt(ramount) + parseInt(amt);
					        //$.post("handler/payment.ashx?type=delete&paymentid=" + id , $("#form").serialize(),ShowRecords);
					        PopulatePaymentSummary();
					        
					        
					        }
					    }
					    function PopulatePaymentSummary()
					    {
					    $.post("handler/payment.ashx?type=summary", $("#form").serialize(),ShowSummary);                        					    
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
					        document.getElementById("TotalAmount").innerHTML = data[6];
					        
					    }

					    function showmultipledelete(result) {

					    }
					    
					    $("#single").click(function (){
					        location.replace("payment.aspx");
					    });
					    function sleep(milliSeconds)
					    {
                        var startTime = new Date().getTime(); // get the current time
                        while (new Date().getTime() < startTime + milliSeconds); // hog cpu
                        }
                        function GenerateReceipt(id) {

                            //alert("Payment Added Successfully!!!");
					        
					        //$.post("handler/payment.ashx?type=pdf&paymentid="+ id, $("#form").serialize(),ShowPaymentReceipt);
                            //					         window.open('crreport.aspx?ID='+ id,'','width=200,height=100');

                            alert("Printing Receipt: " + id + " Close window after print");
					        var div = document.getElementById("Print");
					        div.innerHTML = "<iframe src='crreport.aspx?ID=" + id + "'  onload='this.contentWindow.print();'></iframe>";
					        
					    }
					    function ShowPaymentReceipt(filepath)
					    {
					        alert(filepath);
					        window.open(filepath); 
					    }
					    function SendSMS(id)
					    {
					        $.post("handler/payment.ashx?type=sms&paymentid="+ id, $("#form").serialize(),ShowSMSReceipt);                        					    
					    }
					    function ShowSMSReceipt(result)
					    {
					        alert(result);
					    }
//					    $("#complete").click(function(){					          
//					        location.reload();
//					    });
                        

                        function checkclosedtrasction()
					    {
					     var username=document.getElementById("username").value;
					     $.post("handler/payment.ashx?type=checkclosedtrasction&user="+ username, $("#form").serialize(),disblebtn); 
					    }
					    function disblebtn(result)
					    {
					        document.getElementById("add").disabled = false;
					      if (result!='1')
					      {

					          document.getElementById("save").disabled = false;
					          document.getElementById("add").disabled = false;
					        alert("Please Close Trasaction of " + result );
					      }
					    }
					    function save_onclick() {
//					        var accountno = document.getElementById("accountno").value;
//					        alert(accountno);
//					        if (accountno.length != 8) {
//					            alert("Account No should be 8 digit long");
//					            document.getElementById("accountno").focus();
//					            return;
					       // } 

					    }
					    function ResetBankFields()
					    {
					     document.getElementById("bank").disabled=false;    
					     document.getElementById("chequedate").disabled=false;    
					     document.getElementById("chequeno").disabled=false;    
					     document.getElementById("amt").disabled=false;    
					     document.getElementById("phoneno").disabled=false;    
					     document.getElementById("EmailId").disabled=false;    
					     
					     document.getElementById("bank").value="select";    
					     document.getElementById("chequedate").value="";    
					     document.getElementById("chequeno").value="";    
					     document.getElementById("amt").value="";    
					     document.getElementById("phoneno").value="";    
					     document.getElementById("EmailId").value="";    
					     document.getElementById("ramount").innerHTML ="0";    
					     
					    }
					    $("#reset").click(function(){
					    checkclosedtrasction();	        					        
                    	PopulatePaymentSummary();
	                    DisableFields();
	                    document.getElementById('complete').disabled = true;
	                    ResetBankFields(); 
					    ClearFields(); 
					    ShowRecordsTable("<tr><td>No Record Found</td><td></td><td></td><td></td><td></td><td></td><td></td></tr>");
					    
					    })
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
