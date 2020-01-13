<%@ Page Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="DailyCollection.aspx.cs" Inherits="JVVNLWeb.DailyCollection" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentplaceholder1" runat="server">
    <%--<script src="js/jquery.min.js" type="text/javascript"></script>
    

    <script src="js/jquery-1.4.2.min.js" type="text/javascript"></script>--%>
    
    <script type="text/javascript">
        function isInteger(s) {
            var i;
            s = s.toString();
            for (i = 0; i < s.length; i++) {
                var c = s.charAt(i);
                if (isNaN(c)) {
                    alert("Given value is not a number");
                    return false;
                }
                else {
                    TotalAmount();
                }
            }
            return true;
        }
</script>

<script type = "text/javascript">
    var isShift = false;
    function keyUP(keyCode) {
        if (keyCode == 16 || (keyCode >= 65 && keyCode <= 90)) {
            isShift = false;
            alert('Only Number is allowed');
        }
        else {
            TotalAmount();
        }
    }
    
  function isNumeric(keyCode) {
      if (keyCode == 16)
          isShift = true;

      return ((keyCode >= 48 && keyCode <= 57 || keyCode == 8 ||    (keyCode >= 96 && keyCode <= 105)) && isShift == false);
      
  }
 </script>
 
<script type="text/javascript">

    jQuery(document).ready(function() {
 var url = "CheckSession.aspx?hgUserName=" + document.getElementById("username").value;
         $(document).idleTimeout({ inactivity: 600000, noconfirm: 1000, sessionAlive: 600000, redirect_url: url });
        FillCashAmount();

        //        FillCollection();
        $("#chequedate").datepicker({ dateFormat: 'dd/mm/yy' }); 
        var actualDate = new Date();
        var newDate = new Date(actualDate.getFullYear(), actualDate.getMonth(), actualDate.getDate() - 1);
        $("#chequedate").datepicker('setDate', newDate);
        $("#chequedate").focus();        
        $("#cashamount").focus();

        var date = document.getElementById("chequedate").value;
        var username = document.getElementById("username").value;
        $.post("handler/DailyCollection.ashx?type=GetCollectionDetails&cdate=" + date + "&username=" + username, $("#form").serialize(), GetCollectionDetails);

    });
</script>

<div class="grid_12">
						<div class="box">
                            <input type="hidden" id="t" runat="server" />
							<h2>
								Daily Collection
								<span class="l"></span><span class="r"></span>
							</h2>
							<div class="block">
								<div class="form_place"  id="Div1">
									<table class="fieldtable">
									    <tr>
									        <td>Date : <input type="text" name="chequedate" id="chequedate"  size="10" readonly="readonly" /></td>
									    </tr>
									    <tr>
									        <td>Cash Amount:</td>
									        <td><input type="hidden" id="username" value="<% Response.Write(Session["username"].ToString()); %>" /><input   type="text" value="0"  id="cashamount" name="cashamount" size="15"  readonly=readonly   /></td>
									        <td>Cheque Amount:</td>
									        <td><input   type="text" value="0"  id="chequeamount" name="chequeamount" size="15"   disabled="disabled"/></td>
									        <td>Total Amount:</td>
									        <td><input   type="text" value="0"  id="tamout" name="tamount" size="15"  disabled="disabled" /></td>
									    </tr>
									    
									     <tr>
									       <td colspan="2">
									         
									       </td>						       
									       <td colspan="2">
									          
									       </td>
									       <td colspan="2"></td>
									    </tr>
									    
									    <tr>
									       <td colspan="2">
									       <input   type="text" value="0"  id="txtOneThosand" name="txtOneThosand" size="15"    onkeyup = "keyUP(event.keyCode)" onkeydown = "return isNumeric(event.keyCode);" onpaste = "return false;" />
									         
									           
									       </td>						       
									       <td colspan="2">
									          <label id="lblOneThosand"  name="lblOneThosand" size="30">x&nbsp;&nbsp;1000</label>
									       </td>
									       <td colspan="2">
									        <input   type="text"  value="0" id="txtOneThosandAmount" name="txtOneThosand" size="15" disabled="disabled"/>
									       </td>
									    </tr>
									    <tr>
									       <td colspan="2">
									        
									         <input   type="text" value="0" id="txtFiveHundred" name="txtFiveHundred" size="15"  onkeyup = "keyUP(event.keyCode)" onkeydown = "return isNumeric(event.keyCode);" onpaste = "return false;" />
									       </td>						       
									       <td colspan="2">
									          <label id="lblFiveHundred" name="lblOneThosand" size="30">x&nbsp;&nbsp;500</label>
									       </td>
									       <td colspan="2">
									         <input   type="text" value="0"  id="txtFiveHundredAmount" name="txtFiveHundred" size="15" disabled="disabled"/>
									       </td>
									    </tr>
									    
									    <tr>
									       <td colspan="2">
									         <input   type="text" value="0" id="txtHundred" name="txtHundred" size="15"  onkeyup = "keyUP(event.keyCode)" onkeydown = "return isNumeric(event.keyCode);" onpaste = "return false;" />
									       </td>						       
									       <td colspan="2">
									          <label id="lbltxtHundred" name="lbltxtHundred" size="30">x&nbsp;&nbsp;100</label>
									       </td>
									       <td colspan="2">
									         <input   type="text" value="0"  id="txtHundredAmount" name="txtHundred" size="15" disabled="disabled"/>
									       </td>
									    </tr>
									    
									    <tr>
									       <td colspan="2">
									         <input   type="text" value="0" id="txtFifty" name="txtFifty" size="15"  onkeyup = "keyUP(event.keyCode)" onkeydown = "return isNumeric(event.keyCode);" onpaste = "return false;" />
									       </td>						       
									       <td colspan="2">
									          <label id="Label1" name="lbltxtFifty" size="30">x&nbsp;&nbsp;50</label>
									       </td>
									       <td colspan="2">
									         <input   type="text" value="0"  id="txtFiftyAmount" name="txtFiftyAmount" size="15" disabled="disabled"/>
									       </td>
									    </tr>
									    
									    <tr>
									       <td colspan="2">
									         <input   type="text" value="0" id="txtTwenty" name="txtTwenty" size="15"  onkeyup = "keyUP(event.keyCode)" onkeydown = "return isNumeric(event.keyCode);" onpaste = "return false;" />
									       </td>						       
									       <td colspan="2">
									          <label id="Label2" name="lbltxtTwenty" size="30">x&nbsp;&nbsp;20</label>
									       </td>
									       <td colspan="2">
									         <input   type="text" value="0"  id="txtTwentyAmount" name="txtTwentyAmount" size="15" disabled="disabled"/>
									       </td>
									    </tr>
									    
									     <tr>
									       <td colspan="2">
									         <input   type="text" value="0" id="txtTen" name="txtTen" size="15"  onkeyup = "keyUP(event.keyCode)" onkeydown = "return isNumeric(event.keyCode);" onpaste = "return false;" />
									       </td>						       
									       <td colspan="2">
									          <label id="lblTen" name="lblTen" size="30">x&nbsp;&nbsp;10</label>
									       </td>
									       <td colspan="2">
									         <input   type="text" value="0"  id="txtTenAmount" name="txtTen" size="15" disabled="disabled"/>
									       </td>
									    </tr>
									    
									     <tr>
									       <td colspan="2">
									         <input   type="text" value="0" id="txtFive" name="txtFive" size="15"  onkeyup = "keyUP(event.keyCode)" onkeydown = "return isNumeric(event.keyCode);" onpaste = "return false;" />
									       </td>						       
									       <td colspan="2">
									          <label id="Label3" name="lbltxtFive" size="30">x&nbsp;&nbsp;5</label>
									       </td>
									       <td colspan="2">
									         <input   type="text" value="0"  id="txtFiveAmount" name="txtFiveAmount" size="15"  disabled="disabled"/>
									       </td>
									    </tr>
									    
									      <tr>
									       <td colspan="2">
									         <input   type="text" value="0" id="txtTwo" name="txtTwo" size="15"  onkeyup = "keyUP(event.keyCode)" onkeydown = "return isNumeric(event.keyCode);" onpaste = "return false;" />
									       </td>						       
									       <td colspan="2">
									          <label id="Label4" name="lbltxtTwo" size="30">x&nbsp;&nbsp;2</label>
									       </td>
									       <td colspan="2">
									         <input   type="text" value="0"  id="txtTwoAmount" name="txtTwoAmount" size="15" disabled="disabled" />
									       </td>
									    </tr>
									    
									    <tr>
									       <td colspan="2">
									         <input   type="text" value="0" id="txtOne" name="txtOne" size="15"  onkeyup = "keyUP(event.keyCode)" onkeydown = "return isNumeric(event.keyCode);" onpaste = "return false;" />
									       </td>						       
									       <td colspan="2">
									          <label id="lblOne" name="lblOne" size="30">x&nbsp;&nbsp;1</label>
									       </td>
									       <td colspan="2">
									       
									         <input   type="text" value="0"  id="txtOneAmount" name="txtOneAmount" size="15" disabled="disabled"/>
									       </td>
									    </tr>
									    
									    <tr >
									       <td colspan="2">
									         
									       </td>	
									       <td></td>					       
									       <td  align="right">
									           Total Amount:
									       </td>
									       <td colspan="2">
									       <input   type="text"  id="totalamount" name="totalamount" size="15" disabled="disabled"/>
									       </td>
									    </tr>
									    <tr>
					                        <td colspan="2" align="center"><button type="button" class="button green small" id="save">SAVE</button>
					                        <button type="button" class="button orange small" id="cancel">CANCEL</button>
					                        </td>
		                                    <td colspan="2">
		                                    </td>
				                         </tr>
        
									</table>
								</div>
							</div>
						</div>

	</div>				
	<script type="text/javascript" >


	    function TotalAmount() {
	        var thousand = document.getElementById("txtOneThosand").value;
	        document.getElementById("txtOneThosandAmount").value = thousand * 1000;
	        var fivehundred = document.getElementById("txtFiveHundred").value;
	        document.getElementById("txtFiveHundredAmount").value = fivehundred * 500;
	        var hundred = document.getElementById("txtHundred").value;
	        document.getElementById("txtHundredAmount").value = hundred * 100;
	        var Fifty = document.getElementById("txtFifty").value;
	        document.getElementById("txtFiftyAmount").value = Fifty * 50;
	        var Twenty = document.getElementById("txtTwenty").value;
	        document.getElementById("txtTwentyAmount").value = Twenty * 20;
	        var ten = document.getElementById("txtTen").value;
	        document.getElementById("txtTenAmount").value = ten * 10;
	        var Five = document.getElementById("txtFive").value;
	        document.getElementById("txtFiveAmount").value = Five * 5;

	        var Two = document.getElementById("txtTwo").value;
	        document.getElementById("txtTwoAmount").value = Two * 2;
	        var one = document.getElementById("txtOne").value;
	        document.getElementById("txtOneAmount").value = one * 1
	        var thousandAmount = document.getElementById("txtOneThosandAmount").value;
	        var fivehundredAmount = document.getElementById("txtFiveHundredAmount").value;
	        var hundredAmount = document.getElementById("txtHundredAmount").value;
	        var FiftyAmount = document.getElementById("txtFiftyAmount").value;
	        var TwentyAmount = document.getElementById("txtTwentyAmount").value;
	        var tenAmount = document.getElementById("txtTenAmount").value;
	        var FiveAmount = document.getElementById("txtFiveAmount").value;
	        var TwoAmount = document.getElementById("txtTwoAmount").value;
	        var oneAmount = document.getElementById("txtOneAmount").value;

	        document.getElementById("totalamount").value = parseInt(thousandAmount) + parseInt(fivehundredAmount) + parseInt(hundredAmount) + parseInt(FiftyAmount) + parseInt(TwentyAmount) + parseInt(tenAmount) + parseInt(FiveAmount) + parseInt(TwoAmount) + parseInt(oneAmount); 

	     }

	</script>
	
	
	<script type="text/javascript" >
    $("#chequedate").datepicker({ onSelect: SelectedDay 
    
   
    });
    
    function SelectedDay(date, inst) {
    
        var username=document.getElementById("username").value;   
       $.post("handler/DailyCollection.ashx?type=GetCollectionDetails&cdate=" + date + "&username=" + username, $("#form").serialize(), GetCollectionDetails);
       
    }
    
    function GetCollectionDetails(result)
    {
        var data = result.toString().split('#');
        var i = 0;
        document.getElementById("cashamount").value =0;
        document.getElementById("chequeamount").value =0;
	    if (data.length > 1)
	    {
	        if (data[i] == "Bank" || data[i] == "bank")
	        {
                document.getElementById("chequeamount").value = data[i+1];  
                i = parseInt(i) + 2;
            }   
            if (data[i] == "Cash")
	        {
	            document.getElementById("cashamount").value = data[i+1];   
	            i = parseInt(i)+ 2;
	        }         
	        if (data.length > 6)
	        {
                document.getElementById("txtOneThosand").value = data[i];
                i=parseInt(i)+1;
                document.getElementById("txtFiveHundred").value = data[i];
                i=i+1;
                document.getElementById("txtHundred").value = data[i];
                i=i+1;
                document.getElementById("txtFifty").value = data[i];
                i=i+1;
                document.getElementById("txtTwenty").value = data[i];
                i=i+1;
                document.getElementById("txtTen").value = data[i];
                i=i+1;
                document.getElementById("txtFive").value = data[i];
                i=i+1;
                document.getElementById("txtTwo").value = data[i];
                i=i+1;
                document.getElementById("txtOne").value = data[i];                                                         
            } 
            else
            {
                document.getElementById("txtOneThosand").value = 0;
                document.getElementById("txtFiveHundred").value =0;
                document.getElementById("txtHundred").value = 0;
                document.getElementById("txtFifty").value = 0;
                document.getElementById("txtTwenty").value = 0;
                document.getElementById("txtTen").value = 0;
                document.getElementById("txtFive").value = 0;
                document.getElementById("txtTwo").value = 0;
                document.getElementById("txtOne").value = 0;          
            }            
        }
        else
        {
            document.getElementById("txtOneThosand").value = 0;
            document.getElementById("txtFiveHundred").value =0;
            document.getElementById("txtHundred").value = 0;
            document.getElementById("txtFifty").value = 0;
            document.getElementById("txtTwenty").value = 0;
            document.getElementById("txtTen").value = 0;
            document.getElementById("txtFive").value = 0;
            document.getElementById("txtTwo").value = 0;
            document.getElementById("txtOne").value = 0;          
        }
        
        var CashCollection = document.getElementById("cashamount").value;
        var ChequeCollection = document.getElementById("chequeamount").value;
        
        var totalCollection = parseInt(CashCollection) + parseInt(ChequeCollection)
        
        document.getElementById("tamout").value = totalCollection;
        
        
        TotalAmount(); 
    }

    $("#save").click(function() {
        var OneThosand = document.getElementById("txtOneThosand").value;
        var FiveHundred = document.getElementById("txtFiveHundred").value;
        var Hundred = document.getElementById("txtHundred").value;
        var Fifty = document.getElementById("txtFifty").value;
        var Twenty = document.getElementById("txtTwenty").value;
        var Ten = document.getElementById("txtTen").value;
        var Five = document.getElementById("txtFive").value;
        var Two = document.getElementById("txtTwo").value;
        var One = document.getElementById("txtOne").value;
        var username = document.getElementById("username").value;
        var cashAmt = document.getElementById("cashamount").value;
        var chequeAmt = document.getElementById("chequeamount").value;
        var cdate=document.getElementById("chequedate").value; 
//        if (document.getElementById("cashamount").value == 0) {
//            alert('Cash Collection is blank')
//            return;
//        }
        
        if (document.getElementById("totalamount").value == null) {
            alert('total Amount  blank')
            return;
        }

        var CashCollection = document.getElementById("cashamount").value;

        var totalamount = document.getElementById("totalamount").value;


        if (parseInt( CashCollection) == parseInt( totalamount)) {
            $.post("handler/DailyCollection.ashx?type=insert&txtOneThosand=" + OneThosand + "&txtFiveHundred=" + FiveHundred + "&txtHundred=" + Hundred + "&txtFifty=" + Fifty + "&txtTwenty=" + Twenty + "&txtTen=" + Ten + "&txtFive=" + Five + "&txtTwo=" + Two + "&txtOne=" + One + "&username=" + username + "&cdate=" + cdate + "&cashAmt=" + cashAmt + "&chequeAmt=" + chequeAmt, $("#form").serialize(), SaveData);

        }
        else {
            alert('Cash Amount is not Matched with Total Amount')

        }



    });
    function SaveData(result) {
        alert(result);
       
        ClearControls();
        FillCollection();
    }
    function ClearControls() {
        document.getElementById("txtOneThosand").value = 0;
        document.getElementById("txtFiveHundred").value = 0;
        document.getElementById("txtHundred").value = 0;
        document.getElementById("txtFifty").value = 0;
        document.getElementById("txtTwenty").value = 0;
        document.getElementById("txtTen").value = 0;
        document.getElementById("txtFive").value = 0;
        document.getElementById("txtTwo").value = 0;
        document.getElementById("txtOne").value = 0;

        document.getElementById("txtOneThosandAmount").value = "";
        document.getElementById("txtFiveHundredAmount").value = "";
        document.getElementById("txtHundredAmount").value = "";
        document.getElementById("txtFiftyAmount").value = "";
        document.getElementById("txtTwentyAmount").value = "";
        document.getElementById("txtTenAmount").value = "";
        document.getElementById("txtFiveAmount").value = "";
        document.getElementById("txtTwoAmount").value = "";
        document.getElementById("txtOneAmount").value = "";
        document.getElementById("totalamount").value = "";
    }

    function FillCollection() {
        var username=document.getElementById("username").value;   
        $.post("handler/DailyCollection.ashx?type=cashcollection&date=&username=" + username , $("#form").serialize(),FillDialog);
    }

        
    function FillDialog(result) {
    
	        var data = result.toString().split('#');
	        if (data.length > 1)
	       {
        document.getElementById("txtOneThosand").value = data[0];
        document.getElementById("txtFiveHundred").value = data[1];
        document.getElementById("txtHundred").value = data[2];
        document.getElementById("txtFifty").value = data[3];
        document.getElementById("txtTwenty").value = data[4];
        document.getElementById("txtTen").value = data[5];
        document.getElementById("txtFive").value = data[6];
        document.getElementById("txtTwo").value = data[7];
        document.getElementById("txtOne").value = data[8];

        TotalAmount();
        }
    }

    function FillCashAmount() {
        var username = document.getElementById("username").value;
        
        $.post("handler/dailycollection.ashx?type=cashamount&username=" + username, $("#form").serialize(), FillCashSelect);
    }

    function FillCashSelect(result) {               
        var data = result.toString().split('~');
        if (data.length > 1) {
            var i;
            for ( i=0; i < data.length-1; i++)
            {
                var Mode = data[i].toString().split('#');
                if (Mode[0].toString() =="Cash")
                    document.getElementById("cashamount").value= Mode[1];
                else
                    document.getElementById("chequeamount").value=Mode[1];
            }            
        }
        var CashCollection = document.getElementById("cashamount").value;
      
        var ChequeCollection = document.getElementById("chequeamount").value;
        
        var totalCollection = parseInt(CashCollection) + parseInt(ChequeCollection)
        document.getElementById("tamout").value = totalCollection;
    }
</script>		
</asp:Content>
