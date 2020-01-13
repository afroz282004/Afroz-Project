<%@ Page Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="Report123.aspx.cs" Inherits="JVVNLWeb.Report123" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentplaceholder1" Runat="Server">
<script type="text/javascript">
    jQuery(document).ready(function() {
    PopulateCounter();
    PopulateSubDivision();
    PopulateBank();
    ShowAllData();
       
        $("#fromdate").datepicker({ dateFormat: 'dd/mm/yy' });
        $("#todate").datepicker({ dateFormat: 'dd/mm/yy' });
        $("#txtselectDate").datepicker({ dateFormat: 'dd/mm/yy' });



        ShowVisible();
        

    });
</script>
<div class="grid_12">
						<div class="box">

							<h2>
								Reports
								<span class="l"></span><span class="r"></span>
							</h2>
							<div class="block">
								<div class="form_place"  id="Div1">
									<table class="fieldtable">
									    <tr>
									      <td></td>
									      <td><input type="hidden" id="username" value="<% Response.Write(Session["username"].ToString()); %>" /><input type="hidden" id="counterid" value="" /></td>
									      <td colspan="4">
									      <label class="lblfield"  >Select Report:</label>
									       <select id="ddlReportType" runat="server">
				                                <option value="0">----------Select------------</option>
				                                <option value="1">Daily Payment ControlSheet</option>
				                                <option value="2">Daily Cheque Report Transaction Wise</option>
				                                <option value="3">Daily Cheque Report Cheque Wise</option>
				                                <option value="4">Daily Sub Division Wise Summary</option>                        
				                                <option value="5">Daily Cashup Report</option>
				                                <option value="6">Daily Collection Summary User Wise</option>
				                                <option value="7">Daily Login Logout Report</option>
				                                <option value="8">Cancellation Transaction Report</option>
				                                <option value="9">Date wise Subdivision wise Payment Summary</option>
				                                <option value="10">Date wise Subdivision wise Transaction Summary</option>
				                                <option value="11">Date wise & Subdivision wise Payment transaction Summary</option>
				                                <option value="12">Cashier location allocation report</option>
				                                <option value="13">Print Receipt</option>
				                                <option value="14">Download Payment Information</option>
				                           </select>
									      </td>
									      
									      <td></td>
									      <td></td>
									    </tr>
									    <tr>
									      <td></td>
									      <td></td>
									      <td></td>
									      <td></td>
									      <td></td>
									      <td></td>
									      <td></td>
									      <td></td>
									    </tr>
									    <tr>
									      <td></td>
									      <td></td>
									      <td></td>
									      <td></td>
									      <td></td>
									      <td></td>
									      <td></td>
									      <td></td>
									    </tr>
									    <tr>
									      <td></td>
									      <td></td>
									      <td></td>
									      <td></td>
									      <td></td>
									      <td></td>
									      <td></td>
									      <td></td>
									    </tr>
									    <tr>
									        <td>
									          <label class="lblfield" id="SubDivision" style="display:none" >Sub&nbsp;Division:</label>
									          
									        </td>
									        <td>
									        <input type="hidden" id="hddlSubDivision" runat="server" value="" />
									        <input type="hidden" id="hdndivision"  value="" />
									          <select id="ddlSubDivision" style="display:none" runat="server"><option value="select">----------Select------------</option>
									           <option value="ALL">----------ALL------------</option></select>
									        </td>
									        <td><label class="lblfield"  id="CounterName" style="display:none">Select&nbsp;Counter&nbsp;Name:</label></td>
									        <td>
									        <input type="hidden" id="hddlCounterName" runat="server" value="" />
									        <input type="hidden" id="hdncounter"  value="" />
									           <select id="ddlCounterName" style="display:none" runat="server">
				                                <option value="select">----------Select------------</option>
				                                 <option value="ALL">----------ALL------------</option></select>
				                           
									         </td>
									         </tr>
									         <tr>
									        <td><label class="lblfield"  style="display:none" id="FromDate">From&nbsp;Date:</label></td>
									        <td>
									          <input   type="text"   id="fromdate" name="fromdate" size="10"  style="display:none" />
									        </td>
									        <td>
									           <label class="lblfield" id="Todate" style="display:none">To&nbsp;Date:</label>
									           <label class="lblfield" id="selectDate" style="display:none">Select&nbsp;Date:</label>
									        </td>
									        <td>
									         <input   type="text"   id="todate" name="todate" size="10" style="display:none"    />
									         <input   type="text"   id="txtselectDate" name="txtselectDate" size="10"  style="display:none" />
									        </td>
									    </tr>
									    <tr>
									      <td><label class="lblfield" id="Bank"  style="display:none">Bank</label></td>
									      <td><input type="hidden" id="hddlBank" value="" runat="server" />
									      <input type="hidden" id="hdnbank" value=""  />
									      <select id="ddlBank" style="display:none" runat="server"><option value="select">----------Select------------</option> <option value="ALL">----------ALL------------</option> </select></td>
									
									      <td><label class="lblfield"  id="User" style="display:none">User:</label></td>
									       <td>
									       <input type="hidden" id="hddlUser" value="" runat="server" />
									       <input type="hidden" id="hdnuser" value=""  />
									         <select id="ddlUser" style="display:none" runat="server"><option value="select">----------Select------------</option>
									         <option value="ALL">----------ALL------------</option></select>
									       </td>
									       <td><label class="lblfield"  id="lReceipt">Receipt No:</label></td>
									       <td>
									        <input type="text" id="Receipt" name="Receipt"   size="8" maxlength="10" onkeypress ="return OnlyNumber(event);" />
									       </td>
									       
									      <td></td>
									      <td></td>
									       <td></td>
									      <td></td>
									      <td></td>
									    </tr>
									    
									   
									    <tr>
					                        <td colspan="2" align="center"><button type="button" class="button green small" id="serch">Generate Reports</button>
					                        
					                        </td>
					                        <td colspan="2">
		                                    <asp:Button ID="btnexport" runat="server" OnClick="btnexport_click"  OnClientClick="javascript:return assignvalue();"   Text="Export To Excel" class="button green red"  />
		                                    </td>
		                                    <td colspan="2">
		                                    <asp:Button ID="btnpdf" runat="server" OnClick="btnpdf_click"  OnClientClick="javascript:return assignvalue();"  Text="Export To PDF" class="button green red"  />
		                                    </td>
		                                    <td colspan="2">
		                                    </td>
				                         </tr>
        
									</table>
								</div>
							</div>
						</div>
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
										    <thead id="thead">
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

    function PopulateSubDivision() {
  
        $.post("handler/sdo.ashx?type=populate", $("#form").serialize(), ShowSubDivision);
    }
    function ShowSubDivision(result) {
        
        if (result != "") {
        alert(result);
            var options = result.split("$");
            var addoption, addoption1;
            var i;
            
                        
                for (i = 0; i < options.length - 1; i++) {
                    var splitoption = options[i].split("#");
                    addoption = $('<option></option>').attr("value", splitoption[0]).text(splitoption[1]);
                    $("#ctl00_contentplaceholder1_ddlSubDivision").append(addoption);
                    
                }
            
            

            
        }
    }
    

    function OnlyNumber(e) {
        var keycode = e.charCode ? e.charCode : e.keyCode;
        if ((keycode < 48 || keycode > 57) && keycode != 8 && keycode != 9) {
            return false;
        }
    }
    function PopulateCounter() {
        $.post("handler/counter.ashx?type=populate", $("#form").serialize(), ShowCounter);
    }

    function enableCounter(result) {

        
        var splitoption = result.split("#");
        var cname = splitoption[1].split("$");
        var s = splitoption[0];
        document.getElementById("counterid").value = s;
        
        document.getElementById("ctl00_contentplaceholder1_ddlCounterName").value = cname[0];

    }

    function ShowCounter(result) {
        if (result != "") {
            var options = result.split("$");
            var addoption, addoption1;
            var i;
            for (i = 0; i < options.length - 1; i++) {
                var splitoption = options[i].split("#");
                addoption = $('<option></option>').attr("value", splitoption[0]).text(splitoption[1]);
                $("#ctl00_contentplaceholder1_ddlCounterName").append(addoption);
            }
            
            if (document.getElementById("hdncounter").value == "") {
                var UserId = document.getElementById("username").value;
                $.post("handler/Report.ashx?type=Populatecouter&UserId=" + UserId, $("#form").serialize(), enableCounter);
            }
            else {
               document.getElementById("ctl00_contentplaceholder1_ddlCounterName").value = document.getElementById("hdncounter").value;
            }
        }
        
    }
    

    function PopulateBank() {
        $.post("handler/bank.ashx?type=populate", $("#form").serialize(), ShowBank);
    }
    function ShowBank(result) {
        if (result != "") {
            var options = result.split("$");
            var addoption, addoption1;
            var i;
            for (i = 0; i < options.length - 1; i++) {
                var splitoption = options[i].split("#");
                addoption = $('<option></option>').attr("value", splitoption[0]).text(splitoption[1]);
                $("#ctl00_contentplaceholder1_ddlBank").append(addoption);
            }
        }
    }
    function assignvalue() {
        var res = ShowAllData();
        if (res == false) {
        return false;
        
        }

        document.getElementById("ctl00_contentplaceholder1_hddlSubDivision").value = document.getElementById("ctl00_contentplaceholder1_ddlSubDivision").value;
        document.getElementById("ctl00_contentplaceholder1_hddlCounterName").value = document.getElementById("ctl00_contentplaceholder1_ddlCounterName").value;
        document.getElementById("ctl00_contentplaceholder1_hddlBank").value = document.getElementById("ctl00_contentplaceholder1_ddlBank").value;
        document.getElementById("ctl00_contentplaceholder1_hddlUser").value = document.getElementById("ctl00_contentplaceholder1_ddlUser").value;
        document.getElementById("ctl00_contentplaceholder1_hddlUser").value = document.getElementById("ctl00_contentplaceholder1_ddlUser").value;


    }



    function ShowAllData() {
        var SubDivision = "";
         if (document.getElementById("hdndivision").value != "") {
             SubDivision = document.getElementById("hdndivision").value;
             $("#ctl00_contentplaceholder1_ddlSubDivision").val(SubDivision);
             SubDivision = document.getElementById("hdndivision").value;
        }
        
        
        var CounterName = "";
        if (document.getElementById("hdncounter").value != "") {
            CounterName = document.getElementById("hdncounter").value;
            document.getElementById("ctl00_contentplaceholder1_ddlCounterName").value = document.getElementById("hdncounter").value;
        }
        else
            CounterName = document.getElementById("counterid").value;
        
        var fromdate = document.getElementById("fromdate").value;
        var todate = document.getElementById("todate").value;

        var Bank = "";
        if (document.getElementById("hdnbank").value != "") {
            Bank = document.getElementById("hdnbank");
            $("#ctl00_contentplaceholder1_ddlBank").val(Bank);
            Bank = document.getElementById("hdnbank").value;
        }
        
        //var Bank = document.getElementById("ctl00_contentplaceholder1_ddlBank").value;
        //var User = document.getElementById("ctl00_contentplaceholder1_ddlUser").value;
        var User = "";
        if (document.getElementById("hdnuser").value != "") {
            User = document.getElementById("hdnuser").value;
            $("#ctl00_contentplaceholder1_ddlUser").val(User);
            User = document.getElementById("hdnuser").value;
        }
        
        
        var selectDate = document.getElementById("txtselectDate").value;
        var UserId = document.getElementById("username").value;
        var ReportType = document.getElementById("ctl00_contentplaceholder1_ddlReportType").value;
        var Receiptno = document.getElementById("Receipt").value;
           
        if (ReportType == 1) {
            if (SubDivision == 'select') {
                alert('Select Sub Division');
                document.getElementById("ctl00_contentplaceholder1_ddlSubDivision").focus();
                return false;
            }
            
            else if (CounterName == 'select') {
            document.getElementById("ctl00_contentplaceholder1_ddlCounterName").focus();
            return false;
           }

            else if (fromdate == "") {
            alert('Select from date');
            document.getElementById("fromdate").focus();
            return false;
            }

            else if (todate == "") {
            alert('Select to date');
            document.getElementById("todate").focus();
            return false;
            }
            else {
                $.post("handler/Report.ashx?type=DailyPCSheet&ddlSubDivision=" + SubDivision + "&ddlCounterName=" + CounterName + "&fromdate=" + fromdate + "&todate=" + todate +"&UserId=" +UserId, $("#form").serialize(), SearchResult);

            }
        }



        if (ReportType == 14) {
            if (SubDivision == 'select') {
                alert('Select Sub Division');
                document.getElementById("ctl00_contentplaceholder1_ddlSubDivision").focus();
                return false;
            }

            else if (CounterName == 'select') {
                document.getElementById("ctl00_contentplaceholder1_ddlCounterName").focus();
                return false;
            }

            else if (fromdate == "") {
                alert('Select from date');
                document.getElementById("fromdate").focus();
                return false;
            }

            else if (todate == "") {
                alert('Select to date');
                document.getElementById("todate").focus();
                return false;
            }
            else {
                $.post("handler/Report.ashx?type=PaymentData&ddlSubDivision=" + SubDivision + "&ddlCounterName=" + CounterName + "&fromdate=" + fromdate + "&todate=" + todate + "&UserId=" + UserId, $("#form").serialize(), SearchResult);

            }
        }

        
        

        if (ReportType == 2) {
           
            if (SubDivision == 'select') {
                alert('Select Sub Division');
                document.getElementById("ctl00_contentplaceholder1_ddlSubDivision").focus();
                return false;

            }
            else if (CounterName == 'select') {
            
            document.getElementById("ctl00_contentplaceholder1_ddlCounterName").focus();
            return false;
            }

            else if (fromdate == "") {
            alert('Select from date');
            document.getElementById("fromdate").focus();
            return false;
            }

            else if (todate == "") {
            alert('Select to date');
            document.getElementById("todate").focus();
            return false;
            }
            else {
                $.post("handler/Report.ashx?type=DailyCRTransactionWise&ddlSubDivision=" + SubDivision + "&ddlCounterName=" + CounterName + "&fromdate=" + fromdate + "&todate=" + todate +"&UserId=" +UserId, $("#form").serialize(), SearchResult);

            }
        }

    if (ReportType == 3) {
        
            if (SubDivision == 'select') {
                alert('Select Sub Division');
                document.getElementById("ctl00_contentplaceholder1_ddlSubDivision").focus();
                return false;


            }
            else if (CounterName == 'select') {
            alert('Select CounterName');
            document.getElementById("ctl00_contentplaceholder1_ddlCounterName").focus();
            return false;
            }
          else if (Bank == 'select') {
          alert('Select BankName');
          document.getElementById("ctl00_contentplaceholder1_ddlBank").focus();
          return false;
            }
            else if (User == 'select') {
            alert('Select UserName');
            document.getElementById("ctl00_contentplaceholder1_ddlUser").focus();
            return false;
            }
            else if (fromdate == "") {
            alert('Select from date');
            document.getElementById("fromdate").focus();
            return false;
            }

            else if (todate == "") {
            alert('Select to date');
            document.getElementById("todate").focus();
            return false;
            }
            else {
            
                $.post("handler/Report.ashx?type=DailyChequeWise&ddlSubDivision=" + SubDivision + "&ddlCounterName=" + CounterName + "&Bank=" + Bank + "&User=" + User + "&fromdate=" + fromdate + "&todate=" + todate +"&UserId=" +UserId, $("#form").serialize(), SearchResult);

            }
        }
        if (ReportType == 4) {
            
            if (SubDivision == 'select') {
                alert('Select Sub Division');
                document.getElementById("ctl00_contentplaceholder1_ddlSubDivision").focus();
                return false;

            }
            else if (CounterName == 'select') {
            alert('Select CounterName');
            document.getElementById("ctl00_contentplaceholder1_ddlCounterName").focus();
            return false;
            }

            else if (fromdate == "") {
            alert('Select from date');
            document.getElementById("fromdate").focus();
            return false;
            }

            else if (todate == "") {
            alert('Select to date');
            document.getElementById("todate").focus();
            return false;
            }
            else {
                $.post("handler/Report.ashx?type=DailySubDWiseSummary&ddlSubDivision=" + SubDivision + "&ddlCounterName=" + CounterName + "&fromdate=" + fromdate + "&todate=" + todate +"&UserId=" +UserId, $("#form").serialize(), SearchResult);
            }
        }


           if (ReportType == 6) {
          if (CounterName == 'select') {
              alert('Select CounterName');
              document.getElementById("ctl00_contentplaceholder1_ddlCounterName").focus();
              return false;
            }
           else if (User == 'select') {
           alert('Select UserName');
           document.getElementById("ctl00_contentplaceholder1_ddlUser").focus();
           return false;
            }
            else if (fromdate == "") {
            alert('Select from date');
            document.getElementById("fromdate").focus();
            return false;
            }

            else if (todate == "") {
            alert('Select to date');
            document.getElementById("todate").focus();
            return false;
            }
            else {
           
                $.post("handler/Report.ashx?type=DailyCSUserWise&ddlCounterName=" + CounterName + "&User=" + User + "&fromdate=" + fromdate + "&todate=" + todate +"&UserId=" +UserId, $("#form").serialize(), SearchResult);

            }
        }
        
        
        if (ReportType == 7) {
           
            
            if (CounterName == 'select') {
                alert('Select CounterName');
                document.getElementById("ctl00_contentplaceholder1_ddlCounterName").focus();
                return false;
            }

            else if (fromdate == "") {
            alert('Select from date');
            document.getElementById("fromdate").focus();
            return false;
            }

            else if (todate == "") {
            alert('Select to date');
            document.getElementById("todate").focus();
            return false;
            }
            else {
                $.post("handler/Report.ashx?type=DailyLoginLogout&ddlCounterName=" + CounterName + "&fromdate=" + fromdate + "&todate=" + todate +"&UserId=" +UserId, $("#form").serialize(), SearchResult);

            }
        }
            if (ReportType == 8) {
           
            
            if (CounterName == 'select') {
                alert('Select CounterName');
                document.getElementById("ctl00_contentplaceholder1_ddlCounterName").focus();
                return false;
            }

            else if (fromdate == "") {
            alert('Select from date');
            document.getElementById("fromdate").focus();
            return false;
            }

            else if (todate == "") {
            alert('Select to date');
            document.getElementById("todate").focus();
            return false;
            }
            else {
                $.post("handler/Report.ashx?type=CancelTrasaction&ddlCounterName=" + CounterName + "&fromdate=" + fromdate + "&todate=" + todate +"&UserId=" +UserId, $("#form").serialize(), SearchResult);
            }
         }
        if (ReportType == 9) {

            
            if (CounterName == 'select') {
            
                alert('Select CounterName');
            }

            else if (fromdate == "") {
                alert('Select from date');
            }

            else if (todate == "") {
                alert('Select to date');
            }
            else {
                $.post("handler/Report.ashx?type=DatewiseSPS&ddlCounterName=" + CounterName + "&fromdate=" + fromdate + "&todate=" + todate +"&UserId=" +UserId, $("#form").serialize(), SearchResult);
            }
         }
         
          if (ReportType == 10) {
           
            
            if (CounterName == 'select') {
                alert('Select CounterName');
                document.getElementById("ctl00_contentplaceholder1_ddlCounterName").focus();
                return false;
            }

            else if (fromdate == "") {
            alert('Select from date');
            document.getElementById("fromdate").focus();
            return false;
            }

            else if (todate == "") {
            alert('Select to date');
            document.getElementById("todate").focus();
            return false;
            }
            else {
                $.post("handler/Report.ashx?type=DatewiseSTS&ddlCounterName=" + CounterName + "&fromdate=" + fromdate + "&todate=" + todate +"&UserId=" +UserId, $("#form").serialize(), SearchResult);
            }
         }
         
                if (ReportType == 11) {
           if (SubDivision == 'select') {
               alert('Select Sub Division');
               document.getElementById("ctl00_contentplaceholder1_ddlSubDivision").focus();
               return false;

            }
            
            else if (CounterName == 'select') {
            alert('Select CounterName');
            document.getElementById("ctl00_contentplaceholder1_ddlCounterName").focus();
            return false;
            }

            else if (fromdate == "") {
            alert('Select from date');
            document.getElementById("fromdate").focus();
            return false;
            }

            else if (todate == "") {
            alert('Select to date');
            document.getElementById("todate").focus();
            return false;
            }
            else {
                $.post("handler/Report.ashx?type=DatewiseSPTS&SubDivision=" + SubDivision + "&ddlCounterName=" + CounterName + "&fromdate=" + fromdate + "&todate=" + todate +"&UserId=" +UserId, $("#form").serialize(), SearchResult);
            }
        }
        if (ReportType == 13) {
            if (Receiptno == "") {
                Receiptno = 'ALL';
            }
            else {
                Receiptno = Receiptno;
            }
            if (SubDivision == 'select') {
                alert('Select Sub Division');
                document.getElementById("ctl00_contentplaceholder1_ddlSubDivision").focus();
                return false;
                

            }
            else if (CounterName == 'select') {
            alert('Select CounterName');
            document.getElementById("ctl00_contentplaceholder1_ddlCounterName").focus();
            return false;
                
            }

            else if (fromdate == "") {
            alert('Select from date');
            document.getElementById("fromdate").focus();
            return false;
                
            }

            else if (todate == "") {
            alert('Select to date');
            document.getElementById("todate").focus();
            return false;
                
            }
            else {
                $.post("handler/Report.ashx?type=repReceiptno&ddlSubDivision=" + SubDivision + "&ddlCounterName=" + CounterName + "&fromdate=" + fromdate + "&todate=" + todate + "&UserId=" + UserId + "&Receiptno=" + Receiptno, $("#form").serialize(), SearchResult);
            }
            
        }

    }
    function PrintReceipt(result) {

        //$.post("handler/payment.ashx?type=pdf&paymentid="+ result, $("#form").serialize(),showww);   
        window.open('crreport.aspx?ID=' + result, '', 'width=200,height=100');
    }
    function showww(result) {
        //alert ('HI');    
        //alert(result );
    }
    

    function SearchResult(result) {

        
        var tbl = result.split("$");
        var head = tbl[0];
        var body = tbl[1];

        $("#basictable > thead").html("");
        $("#basictable > tbody").html("");
        $("#basictable thead").append(head);
        $("#basictable tbody").append(body);
        if (typeof oTable == 'undefined') {
            oTable = $('#basictable').dataTable({
            "bJQueryUI": true,
            "bDestroy" :true,
            "sPaginationType": "full_numbers"
            });
        }        
    }



    
</script>
<script type="text/javascript">



    $("#ctl00_contentplaceholder1_ddlReportType").change(function() {

           ShowVisible ();

        });
    
    function ShowVisible()
    {
        var ddlVal = document.getElementById("ctl00_contentplaceholder1_ddlReportType").value


            if (ddlVal == 0) {
                $('#ctl00_contentplaceholder1_SubDivision').hide();
                $('#ctl00_contentplaceholder1_ddlSubDivision').hide();
                $('#CounterName').hide();
                $('#ctl00_contentplaceholder1_ddlCounterName').hide();
                $('#FromDate').hide();
                $('#fromdate').hide();
                $('#Todate').hide();
                $('#todate').hide();
                $('#Bank').hide();
                $('#ctl00_contentplaceholder1_ddlBank').hide();
                $('#User').hide();
                $('#ctl00_contentplaceholder1_ddlUser').hide();
                $('#selectDate').hide();
                $('#txtselectDate').hide();
                $('#Receipt').hide();
                $('#lReceipt').hide();
            }

            if (ddlVal == 1 || ddlVal == 2 || ddlVal == 4 || ddlVal == 11 || ddlVal == 14) {
                $('#SubDivision').show();
                $('#ctl00_contentplaceholder1_ddlSubDivision').show();

                $('#CounterName').show();

                $('#ctl00_contentplaceholder1_ddlCounterName').show();
                if (document.getElementById("counterid").value != '0') {
                    $("#ctl00_contentplaceholder1_ddlCounterName").val(document.getElementById("counterid").value);
                    document.getElementById("ctl00_contentplaceholder1_ddlCounterName").disabled = true;
                };
                $('#FromDate').show();
                $('#fromdate').show();

                $('#Todate').show();
                $('#todate').show();


                $('#Bank').hide();
                $('#ctl00_contentplaceholder1_ddlBank').hide();
                $('#User').hide();
                $('#ctl00_contentplaceholder1_ddlUser').hide();

                $('#selectDate').hide();
                $('#txtselectDate').hide();
                $('#Receipt').hide();
                $('#lReceipt').hide();

            }
            

            if (ddlVal == 3) {


                $('#SubDivision').show();
                $('#ctl00_contentplaceholder1_ddlSubDivision').show();
                $('#Bank').show();
                $('#ctl00_contentplaceholder1_ddlBank').show();
                $('#User').show();
                $('#ctl00_contentplaceholder1_ddlUser').show();
                $('#CounterName').show();

                $('#ctl00_contentplaceholder1_ddlCounterName').show();
                if (document.getElementById("counterid").value != '0') {
                    $("#ctl00_contentplaceholder1_ddlCounterName").val(document.getElementById("counterid").value);
                    document.getElementById("ctl00_contentplaceholder1_ddlCounterName").disabled = true;
                };

                $('#FromDate').show();
                $('#fromdate').show();

                $('#Todate').show();
                $('#todate').show();
                $('#Receipt').hide();
                $('#lReceipt').hide();

            }
            else {


            }

            if (ddlVal == 6) {



                $('#CounterName').show();

                $('#ctl00_contentplaceholder1_ddlCounterName').show();
                if (document.getElementById("counterid").value != '0') {
                    $("#ctl00_contentplaceholder1_ddlCounterName").val(document.getElementById("counterid").value);
                    document.getElementById("ctl00_contentplaceholder1_ddlCounterName").disabled = true;
                };
                $('#FromDate').show();
                $('#fromdate').show();

                $('#Todate').show();
                $('#todate').show();

                $('#SubDivision').hide();
                $('#ctl00_contentplaceholder1_ddlSubDivision').hide();
                $('#Bank').hide();
                $('#ctl00_contentplaceholder1_ddlBank').hide();
                $('#User').show();
                $('#ctl00_contentplaceholder1_ddlUser').show();

                $('#selectDate').hide();
                $('#txtselectDate').hide();
                $('#Receipt').hide();
                $('#lReceipt').hide();

            }

            if (ddlVal == 7 || ddlVal == 8 || ddlVal == 9 || ddlVal == 10) {
                $('#CounterName').show();

                $('#ctl00_contentplaceholder1_ddlCounterName').show();
                if (document.getElementById("counterid").value != '0') {
                    $("#ctl00_contentplaceholder1_ddlCounterName").val(document.getElementById("counterid").value);
                    document.getElementById("ctl00_contentplaceholder1_ddlCounterName").disabled = true;
                };
                $('#FromDate').show();
                $('#fromdate').show();

                $('#Todate').show();
                $('#todate').show();

                $('#SubDivision').hide();
                $('#ctl00_contentplaceholder1_ddlSubDivision').hide();


                $('#Bank').hide();
                $('#ddlBank').hide();
                $('#User').hide();
                $('#ctl00_contentplaceholder1_ddlUser').hide();
                $('#selectDate').hide();
                $('#txtselectDate').hide();
                $('#Receipt').hide();
                $('#lReceipt').hide();

            }
            else {

            }

            if (ddlVal == 5 || ddlVal == 12) {
                $('#selectDate').show();
                $('#txtselectDate').show();

                $('#SubDivision').hide();
                $('#ctl00_contentplaceholder1_ddlSubDivision').hide();

                $('#Bank').hide();
                $('#ctl00_contentplaceholder1_ddlBank').hide();
                $('#User').hide();
                $('#ctl00_contentplaceholder1_ddlUser').hide();

                $('#Todate').hide();
                $('#todate').hide();

                $('#fromdate').hide();
                $('#FromDate').hide();
                $('#Receipt').hide();
                $('#lReceipt').hide();
            }
            else {


            }
            if (ddlVal == 13) {
                $('#SubDivision').show();
                $('#ctl00_contentplaceholder1_ddlSubDivision').show();

                $('#CounterName').show();

                $('#ctl00_contentplaceholder1_ddlCounterName').show();
                if (document.getElementById("counterid").value != '0') {
                    $("#ctl00_contentplaceholder1_ddlCounterName").val(document.getElementById("counterid").value);
                    document.getElementById("ctl00_contentplaceholder1_ddlCounterName").disabled = true;
                };
                
                $('#FromDate').show();
                $('#fromdate').show();

                $('#Todate').show();
                $('#todate').show();
                $('#Receipt').show();
                $('#lReceipt').show();

                $('#Bank').hide();
                $('#ctl00_contentplaceholder1_ddlBank').hide();
                $('#User').hide();
                $('#ctl00_contentplaceholder1_ddlUser').hide();

                $('#selectDate').hide();
                $('#txtselectDate').hide();
            }
    }




    $("#ctl00_contentplaceholder1_ddlCounterName").change(function() {
        var ddlVal = document.getElementById("ctl00_contentplaceholder1_ddlCounterName").value;

        document.getElementById("hdncounter").value = ddlVal;

        $.post("handler/Report.ashx?type=populate&id=" + ddlVal, $("#form").serialize(), ShowUser);



    });

    $("#serch").click(function() {
    if (document.getElementById("ctl00_contentplaceholder1_ddlSubDivision").value != 'select') {
            document.getElementById("hdndivision").value = document.getElementById("ctl00_contentplaceholder1_ddlSubDivision").value;
        }

        if (document.getElementById("ctl00_contentplaceholder1_ddlBank").value != 'select') {
            document.getElementById("hdnbank").value = document.getElementById("ctl00_contentplaceholder1_ddlBank").value;
        }
        if (document.getElementById("ctl00_contentplaceholder1_ddlUser").value != 'select') {
            document.getElementById("hdnuser").value = document.getElementById("ctl00_contentplaceholder1_ddlUser").value;
        }
        location.reload();

        //ShowAllData();




    }


);
    function ShowUser(result) {
       
        if (result != "") {
            var options = result.split("$");
            var addoption, addoption1;
            var i;
            for (i = 0; i < options.length - 1; i++) {
                var splitoption = options[i].split("#");
                addoption = $('<option></option>').attr("value", splitoption[0]).text(splitoption[1]);
                $("#ctl00_contentplaceholder1ddlUser").append(addoption);
            }
        }
    }
    
</script>
</asp:Content>