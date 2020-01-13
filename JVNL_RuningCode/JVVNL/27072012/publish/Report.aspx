<%@ Page Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="JVVNLWeb.Report" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentplaceholder1" runat="server">

    <script type="text/javascript">
    jQuery(document).ready(function() {
      var url = "CheckSession.aspx?hgUserName=" + document.getElementById("username").value;
         $(document).idleTimeout({ inactivity: 600000, noconfirm: 1000, sessionAlive: 600000, redirect_url: url });
    //ShowAllData();
        $("#ctl00_contentplaceholder1_txtfromdate").datepicker({ dateFormat: 'dd/mm/yy' });
       $("#ctl00_contentplaceholder1_txttodate").datepicker({ dateFormat: 'dd/mm/yy' });
      $("#ctl00_contentplaceholder1_txtselectDate").datepicker({ dateFormat: 'dd/mm/yy' });
 oTable = $('#basictable').dataTable({
					    "bJQueryUI": true,
					    "sScrollX": "1000px",
					    "sPaginationType": "full_numbers"
				        });    
    });
</script>

<div class="grid_12">
						<div class="box">

							<h2>
								lReports
								<span class="l"></span><span class="r"></span>
							</h2>
							<%--<div class="block">--%>
							<div>
								<div class="form_place"  id="Div1">
									<table class="fieldtable">
									    <tr>
									      <td></td>
									      <td><input type="hidden" id="username"  value="<% Response.Write(Session["username"].ToString());  %>"  /><input type="hidden" id="counterid" value="" runat="server" /></td>
									      <td colspan="4">
									      <label class="lblfield"  >Select Report:</label>
									      
                                              <asp:DropDownList ID="ddlReportType" runat="server" AutoPostBack="True" 
                                                  onselectedindexchanged="ddlReportType_SelectedIndexChanged" 
                                                  >
                                                  
                                              <asp:ListItem Value = "0">----------Select------------</asp:ListItem>
                                              <asp:ListItem Value = "1">Daily Payment ControlSheet</asp:ListItem>
                                              <asp:ListItem Value = "2">Daily Cheque Report Transaction Wise</asp:ListItem>
                                              <asp:ListItem Value = "3">Daily Cheque Report Cheque Wise</asp:ListItem>
                                              <asp:ListItem Value = "4">Daily Sub Division Wise Summary</asp:ListItem>
                                              <%--<asp:ListItem Value = "5">Daily Cashup Report</asp:ListItem>--%>
                                              <asp:ListItem Value = "6">Daily Collection Summary User Wise</asp:ListItem>
                                              <asp:ListItem Value = "7">Daily Login Logout Report</asp:ListItem>
                                              <asp:ListItem Value = "8">Cancellation Transaction Report</asp:ListItem>
                                              <asp:ListItem Value = "9">Date wise Subdivision wise Payment Summary</asp:ListItem>
                                              <asp:ListItem Value = "10">Date wise Subdivision wise Transaction Summary</asp:ListItem>
                                              <asp:ListItem Value = "11">Date wise & Subdivision wise Payment transaction Summary</asp:ListItem>
                                              <asp:ListItem Value = "12">Cashier location allocation report</asp:ListItem>
                                              <asp:ListItem Value = "13">Print Receipt</asp:ListItem>
                                             
                                            
                                              
                                              </asp:DropDownList>
									      </td>
									      
									      <td></td>
									      <td></td>
									    </tr>
									    <tr>
									      <td ></td>
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
									           <%--<label class="lblfield" id="SubDivision"  runat="server">Sub&nbsp;Division:</label>--%>
									          <asp:Label runat="server" id="SubDivision" Text="Sub Division: " Visible="False"  class="lblfield" ></asp:Label>
									        </td>
									        <td>
									        <input type="hidden" id="hddlSubDivision" runat="server" value="" />
									        <input type="hidden" id="hdndivision"  value="" runat="server" />
									        
                                                  <asp:DropDownList ID="ddlSubDivision" runat="server" Visible="False">
                                                   <asp:ListItem Value="select">----------Select------------</asp:ListItem>
                                                  <asp:ListItem Value="ALL">----------ALL------------</asp:ListItem>
                                                  </asp:DropDownList>
									         
									        </td>
									        <td>
									       <%-- <label class="lblfield"  id="CounterName" runat="server" >Select&nbsp;Counter&nbsp;Name:</label>--%>
									       <asp:Label runat="server" id="CounterName" Text="Select Counter Name:" Visible="False"  class="lblfield" ></asp:Label>
									        </td>
									        <td>
									        <input type="hidden" id="hddlCounterName" runat="server" value="" />
									        <input type="hidden" id="hdncounter"  value=""  runat="server" />
									         <asp:DropDownList ID="ddlCounterName" runat="server"  
                                                    OnSelectedIndexChanged ="ddlCounterName_SelectedIndexChanged" 
                                                    AutoPostBack="True" Visible="False">
                                                
                                                  </asp:DropDownList>
									    
				                           
									         </td>
									         </tr>
									         <tr>
									        <td>
									          <%--<label class="lblfield"   id="lFromDate" runat="server">From&nbsp;Date:</label>--%>
									           <asp:Label runat="server" id="lFromDate" Text="From Date:" Visible="False"  class="lblfield" ></asp:Label>
									        </td>
									        <td>
									        
                                                      <asp:TextBox ID="txtfromdate" runat="server" Visible="False"></asp:TextBox>
									     
									        </td>
									        <td>
									          <%-- <label class="lblfield" id="lTodate" runat="server" >To&nbsp;Date:</label>--%>
									            <asp:Label runat="server" id="lTodate" Text="To Date:" Visible="False"  class="lblfield" ></asp:Label>
									          <%-- <label class="lblfield" id="lselectDate" runat="server" >Select&nbsp;Date:</label>--%>
									            <asp:Label runat="server" id="lselectDate" Text="Select Date:" Visible="False"  class="lblfield" ></asp:Label>
									        </td>
									        <td>
									          <asp:TextBox ID="txttodate" runat="server" Visible="False"></asp:TextBox>
									           <asp:TextBox ID="txtselectDate" runat="server" Visible="False"></asp:TextBox>
									   
									        </td>
									    </tr>
									    <tr>
									      <td>
									     <%-- <label class="lblfield" id="lBank" runat="server">Bank</label>--%>
									      <asp:Label runat="server" id="lBank" Text="Bank:" Visible="False"  class="lblfield" ></asp:Label>
									      </td>
									      <td>
									  
									      <input type="hidden" id="hddlBank" value="" runat="server" />
									      <input type="hidden" id="hdnbank" value="" runat="server"  />
									  
									      
									        <asp:DropDownList ID="ddlBank" runat="server" Visible="False">
                                                   <asp:ListItem Value="select">----------Select------------</asp:ListItem>
                                                  <asp:ListItem Value="ALL">----------ALL------------</asp:ListItem>
                                                  </asp:DropDownList>
									      </td>
									
									      <td>
									      <%--<label class="lblfield"  id="lUser"  runat="server">User:</label>--%>
									      <asp:Label runat="server" id="lUser" Text="User:" Visible="False"  class="lblfield" ></asp:Label>
									      </td>
									       <td>
									   
									     <input type="hidden" id="hddlUser" value="" runat="server" />
									       <input type="hidden" id="hdnuser" value="" runat="server" />
									         
									           <asp:DropDownList ID="ddlUser" runat="server" Visible="False">
                                                 <%--  <asp:ListItem Value="select">----------Select------------</asp:ListItem>--%>
                                                  <asp:ListItem Value="ALL">----------ALL------------</asp:ListItem>
                                                  </asp:DropDownList>
									       </td>
									       <td>
									     &nbsp;<asp:Label runat="server" id="lReceipt" Text="Receipt No:" Visible="False"  class="lblfield" ></asp:Label>
									       </td>
									     <td>
									        <asp:TextBox ID="txtReceipt" runat="server" Visible="False" ></asp:TextBox>
									       
									       </td>
									       
									      <td></td>
									      <td></td>
									       <td></td>
									      <td></td>
									      <td></td>
									    </tr>
									    
									   
									    <tr>
					                       <td colspan="2" align="center">
					                        <%-- <button type="button" class="button green small" id="serch">Generate Reports</button>--%>
					                        <asp:Button ID="btnGenerateReport" runat="server"  
                                                    OnClientClick="javascript:ShowAllData();"   Text="Generate Reports" 
                                                    class="button green small" onclick="btnGenerateReport_Click"  />
		
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
									<table class="table display" id="basictable" >
										    <thead id="thead" runat = "server">
                                            </thead>                                            
                                            <tbody id="tbody" runat = "server">										
										    </tbody>
									</table>
									<!-- END TABLE -->
								</div>
							</div>

						</div>
					</div>
<div id="Print" style="display:none">
	  
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
                    $("#ddlSubDivision").append(addoption);
                    
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
        document.getElementById("ctl00_contentplaceholder1_counterid").value = s;
        
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
                $("#ddlCounterName").append(addoption);
            }
            
            if (document.getElementById("ctl00_contentplaceholder1_hdncounter").value == "") {
                var UserId = document.getElementById("username").value;
                $.post("handler/Report.ashx?type=Populatecouter&UserId=" + UserId, $("#form").serialize(), enableCounter);
            }
            else {
               document.getElementById("ctl00_contentplaceholder1_ddlCounterName").value = document.getElementById("ctl00_contentplaceholder1_hdncounter").value;
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
                $("#ddlBank").append(addoption);
            }
        }
    }
    function assignvalue() {
//        var res = ShowAllData();
//        if (res == false) {
//        return false;
//        
//        }
              
        document.getElementById("ctl00_contentplaceholder1_hddlCounterName").value = document.getElementById("ctl00_contentplaceholder1_ddlCounterName").value;
        if (document.getElementById("ctl00_contentplaceholder1_ddlSubDivision")!= null)
        {
        document.getElementById("ctl00_contentplaceholder1_hddlSubDivision").value = document.getElementById("ctl00_contentplaceholder1_ddlSubDivision").value;
        }
        if (document.getElementById("ctl00_contentplaceholder1_ddlBank")!= null )
        {
        document.getElementById("ctl00_contentplaceholder1_hddlBank").value = document.getElementById("ctl00_contentplaceholder1_ddlBank").value;
        }
        if (document.getElementById("ctl00_contentplaceholder1_ddlUser")!=null)
        {
        document.getElementById("ctl00_contentplaceholder1_hddlUser").value = document.getElementById("ctl00_contentplaceholder1_ddlUser").value;
        }
       // document.getElementById("ctl00_contentplaceholder1_hddlUser").value = document.getElementById("ctl00_contentplaceholder1_ddlUser").value;


    }



    function ShowAllData() {
   
        var SubDivision = "";
         if (document.getElementById("ctl00_contentplaceholder1_ddlSubDivision") !=  null)
            {
                if (document.getElementById("ctl00_contentplaceholder1_ddlSubDivision").value != "") {
                SubDivision = document.getElementById("ctl00_contentplaceholder1_ddlSubDivision").value;
           
                $("#ddlSubDivision").val(SubDivision);
             }
        }
        
        
        var CounterName = "";
       
      
        if (document.getElementById("ctl00_contentplaceholder1_hdncounter").value != "") {
            CounterName = document.getElementById("ctl00_contentplaceholder1_hdncounter").value;
            if (document.getElementById("ctl00_contentplaceholder1_ddlCounterName") !=  null)
            {
                document.getElementById("ctl00_contentplaceholder1_ddlCounterName").value = CounterName; //document.getElementById("ctl00_contentplaceholder1_hdncounter").value;
            }
           
        }
        else
        {
            CounterName = document.getElementById("ctl00_contentplaceholder1_counterid").value;
             
            }
       
        if (document.getElementById("ctl00_contentplaceholder1_txtfromdate")!= null)
        {
        var fromdate = document.getElementById("ctl00_contentplaceholder1_txtfromdate").value;
        }
         if (document.getElementById("ctl00_contentplaceholder1_txttodate")!= null)
        {
        var todate = document.getElementById("ctl00_contentplaceholder1_txttodate").value;
        
        }
   
        var Bank = "";
         if (document.getElementById("ctl00_contentplaceholder1_ddlBank")!= null)
        {
         //alert("bank" + document.getElementById("ctl00_contentplaceholder1_ddlBank").value);
             if (document.getElementById("ctl00_contentplaceholder1_ddlBank").value != "") 
             {
                 Bank = document.getElementById("ctl00_contentplaceholder1_ddlBank").value;
             }
        }
       
        
        
        var User = "";
         if (document.getElementById("ctl00_contentplaceholder1_ddlUser")!= null)
        {
            if (document.getElementById("ctl00_contentplaceholder1_ddlUser").value != "") {
                User = document.getElementById("ctl00_contentplaceholder1_ddlUser").value;
             //   $("#ddlUser").val(User);
              //  User = document.getElementById("ctl00_contentplaceholder1_hdnuser").value;
            }
        }
        
        if (document.getElementById("ctl00_contentplaceholder1_txtselectDate")!= null)
        {
        var selectDate = document.getElementById("ctl00_contentplaceholder1_txtselectDate").value;
        
        }
        if (document.getElementById("ctl00_contentplaceholder1_ddlReportType")!= null)
        {
        var ReportType = document.getElementById("ctl00_contentplaceholder1_ddlReportType").value;
        }
        if (document.getElementById("ctl00_contentplaceholder1_txtReceipt")!= null)
        {
         var Receiptno  = document.getElementById("ctl00_contentplaceholder1_txtReceipt").value;
        }
        var UserId = document.getElementById("username").value;
       
           
        if (ReportType == 1) {
        
            if (SubDivision == 'select' || SubDivision == '') {
                alert('Select Sub Division');
                document.getElementById("ctl00_contentplaceholder1_ddlSubDivision").focus();
                event.returnValue=false;
                return false;
            }
            
            else if (CounterName == 'select' || CounterName == '') {
            alert('Select Counter Name');
            document.getElementById("ctl00_contentplaceholder1_ddlCounterName").focus();
            event.returnValue=false;
            return false;
           }

            else if (fromdate == "") {
            alert('Select from date');
            document.getElementById("ctl00_contentplaceholder1_txtfromdate").focus();
            event.returnValue=false;
            return false;
            }

            else if (todate == "") {
            alert('Select to date');
            document.getElementById("ctl00_contentplaceholder1_txttodate").focus();
           event.returnValue=false;
            return false;
            }
           
            
        }



        if (ReportType == 14) {
            if (SubDivision == 'select' || SubDivision == '') {
                alert('Select Sub Division');
                document.getElementById("ctl00_contentplaceholder1_ddlSubDivision").focus();
                event.returnValue=false;
                return false;
            }

            else if (CounterName == 'select' || CounterName == '' ) {
                document.getElementById("ctl00_contentplaceholder1_ddlCounterName").focus();
                 event.returnValue=false;
                 return false;
            }

            else if (fromdate == "") {
                alert('Select from date');
                document.getElementById("ctl00_contentplaceholder1_txtfromdate").focus();
                event.returnValue=false;
                 return false;
            }

            else if (todate == "") {
                alert('Select to date');
                document.getElementById("ctl00_contentplaceholder1_txttodate").focus();
                event.returnValue=false;
                 return false;
            }
            
        }

        if (ReportType == 2) {
           
            if (SubDivision == 'select' || SubDivision == '') {
                alert('Select Sub Division');
                document.getElementById("ctl00_contentplaceholder1_ddlSubDivision").focus();
                event.returnValue=false;
                 return false;

            }
            else if (CounterName == 'select' || CounterName == '') {
            
            document.getElementById("ctl00_contentplaceholder1_ddlCounterName").focus();
           event.returnValue=false;
                 return false;
            }

            else if (fromdate == "") {
            alert('Select from date');
            document.getElementById("ctl00_contentplaceholder1_txtfromdate").focus();
            event.returnValue=false;
                 return false;
            }

            else if (todate == "") {
            alert('Select to date');
            document.getElementById("ctl00_contentplaceholder1_txttodate").focus();
            event.returnValue=false;
                 return false;
            }
           
        }

    if (ReportType == 3) {
//        alert("Select BankName3" + Bank);
            if (SubDivision == 'select' || SubDivision == '') {
                alert('Select Sub Division');
                document.getElementById("ctl00_contentplaceholder1_ddlSubDivision").focus();
               event.returnValue=false;
                 return false;


            }
            else if (CounterName == 'select' || CounterName == '') {
            alert('Select CounterName');
            document.getElementById("ctl00_contentplaceholder1_ddlCounterName").focus();
           event.returnValue=false;
                 return false;
            }
            
          else if (Bank == 'select' || Bank == '') {
          
          alert('Select BankName' );
          document.getElementById("ctl00_contentplaceholder1_ddlBank").focus();
         event.returnValue=false;
                 return false;
            }
            else if (User == 'select' || User == '') {
            alert('Select UserName');
            document.getElementById("ctl00_contentplaceholder1_ddlUser").focus();
           event.returnValue=false;
                 return false;
            }
            else if (fromdate == "") {
            alert('Select from date');
            document.getElementById("ctl00_contentplaceholder1_txtfromdate").focus();
           event.returnValue=false;
                 return false;
            }

            else if (todate == "") {
            alert('Select to date');
            document.getElementById("ctl00_contentplaceholder1_txttodate").focus();
            event.returnValue=false;
                 return false;
            }
            
        }
        if (ReportType == 4) {
            
            if (SubDivision == 'select' || SubDivision == '') {
                alert('Select Sub Division');
                document.getElementById("ctl00_contentplaceholder1_ddlSubDivision").focus();
                event.returnValue=false;
                 return false;

            }
            else if (CounterName == 'select' || CounterName == '') {
            alert('Select CounterName');
            document.getElementById("ctl00_contentplaceholder1_ddlCounterName").focus();
            event.returnValue=false;
                 return false;
            }

            else if (fromdate == "") {
            alert('Select from date');
            document.getElementById("ctl00_contentplaceholder1_txtfromdate").focus();
           event.returnValue=false;
                 return false;
            }

            else if (todate == "") {
            alert('Select to date');
            document.getElementById("ctl00_contentplaceholder1_txttodate").focus();
            event.returnValue=false;
                 return false;
            }
            
        }


           if (ReportType == 6) {
          if (CounterName == 'select' || CounterName == '') {
              alert('Select CounterName');
              document.getElementById("ctl00_contentplaceholder1_ddlCounterName").focus();
              event.returnValue=false;
                 return false;
            }
           else if (User == 'select') {
           alert('Select UserName');
           document.getElementById("ctl00_contentplaceholder1_ddlUser").focus();
           event.returnValue=false;
                 return false;
            }
            else if (fromdate == "") {
            alert('Select from date');
            document.getElementById("ctl00_contentplaceholder1_txtfromdate").focus();
            event.returnValue=false;
                 return false;
            }

            else if (todate == "") {
            alert('Select to date');
            document.getElementById("ctl00_contentplaceholder1_txttodate").focus();
           event.returnValue=false;
                 return false;
            }
           
        }
        
        
        if (ReportType == 7) {
           
            
            if (CounterName == 'select' || CounterName == '') {
                alert('Select CounterName');
                document.getElementById("ctl00_contentplaceholder1_ddlCounterName").focus();
                event.returnValue=false;
                 return false;
            }

            else if (fromdate == "") {
            alert('Select from date');
            document.getElementById("ctl00_contentplaceholder1_txtfromdate").focus();
           event.returnValue=false;
                 return false;
            }

            else if (todate == "") {
            alert('Select to date');
            document.getElementById("ctl00_contentplaceholder1_txttodate").focus();
           event.returnValue=false;
                 return false;
            }
            
        }
            if (ReportType == 8) {
           
            
            if (CounterName == 'select' || CounterName == '') {
                alert('Select CounterName');
                document.getElementById("ctl00_contentplaceholder1_ddlCounterName").focus();
               event.returnValue=false;
                 return false;
            }

            else if (fromdate == "") {
            alert('Select from date');
            document.getElementById("ctl00_contentplaceholder1_txtfromdate").focus();
           event.returnValue=false;
                 return false;
            }

            else if (todate == "") {
            alert('Select to date');
            document.getElementById("ctl00_contentplaceholder1_txttodate").focus();
            event.returnValue=false;
                 return false;
            }
            
         }
        if (ReportType == 9) {

            
            if (CounterName == 'select' || CounterName == '') {
            
                alert('Select CounterName');
                event.returnValue=false;
                 return false;
            }

            else if (fromdate == "") {
                alert('Select from date');
                event.returnValue=false;
                 return false;
            }

            else if (todate == "") {
                alert('Select to date');
                event.returnValue=false;
                 return false;
            }
         
         }
         
          if (ReportType == 10) {
           
            
            if (CounterName == 'select' || CounterName == '') {
                alert('Select CounterName');
                document.getElementById("ctl00_contentplaceholder1_ddlCounterName").focus();
               event.returnValue=false;
                 return false;
            }

            else if (fromdate == "") {
            alert('Select from date');
            document.getElementById("ctl00_contentplaceholder1_txtfromdate").focus();
            event.returnValue=false;
                 return false;
            }

            else if (todate == "") {
            alert('Select to date');
            document.getElementById("ctl00_contentplaceholder1_txttodate").focus();
           event.returnValue=false;
                 return false;
            }
            
         }
         
                if (ReportType == 11) {
           if (SubDivision == 'select' || SubDivision == '') {
               alert('Select Sub Division');
               document.getElementById("ctl00_contentplaceholder1_ddlSubDivision").focus();
               event.returnValue=false;
                 return false;

            }
            
            else if (CounterName == 'select' || CounterName == '') {
            alert('Select CounterName');
            document.getElementById("ctl00_contentplaceholder1_ddlCounterName").focus();
           event.returnValue=false;
                 return false;
            }

            else if (fromdate == "") {
            alert('Select from date');
            document.getElementById("ctl00_contentplaceholder1_txtfromdate").focus();
           event.returnValue=false;
                 return false;
            }

            else if (todate == "") {
            alert('Select to date');
            document.getElementById("ctl00_contentplaceholder1_txttodate").focus();
            event.returnValue=false;
                 return false;
            }
            
        }
        if (ReportType == 13) {
            if (Receiptno == "") {
                Receiptno = 'ALL';
            }
            else {
                Receiptno = Receiptno;
            }
            if (SubDivision == 'select' || SubDivision == '') {
                alert('Select Sub Division');
                document.getElementById("ctl00_contentplaceholder1_ddlSubDivision").focus();
                event.returnValue=false;
                 return false;
                

            }
            else if (CounterName == 'select' || CounterName == '') {
            alert('Select CounterName');
            document.getElementById("ctl00_contentplaceholder1_ddlCounterName").focus();
           event.returnValue=false;
                 return false;
                
            }

            else if (fromdate == "") {
            alert('Select from date');
            document.getElementById("ctl00_contentplaceholder1_txtfromdate").focus();
            event.returnValue=false;
                 return false;
                
            }

            else if (todate == "") {
            alert('Select to date');
            document.getElementById("ctl00_contentplaceholder1_txttodate").focus();
            event.returnValue=false;
                 return false;
                
            }
          
            
        }

    }
    function PrintReceipt(result) {

        //$.post("handler/payment.ashx?type=pdf&paymentid="+ result, $("#form").serialize(),showww);
        //        window.open('crreport.aspx?ID=' + result, '', 'width=200,height=100');
        var div = document.getElementById("Print");
        div.innerHTML = "<iframe src='crreport.aspx?ID=" + result + "'  onload='this.contentWindow.print();'></iframe>";
    }
   
    

    function SearchResult(result) {

        
     
//        if (result.indexOf('$')>0) 
//        {
           var tbl = result.split("$");
        var head = tbl[0];
        var body = tbl[1];
       
        $("#basictable > thead").html("");
        $("#basictable > tbody").html("");
        $("#basictable thead").append(head);
        
        if  (body != undefined && body !=""  )
        {
      
        $("#basictable tbody").append(body);

//          oTable = $('#basictable').dataTable({
//					    "bJQueryUI": true,
//					    "sPaginationType": "full_numbers"
//				        });
				        }
//				        }
    }



    
</script>
<script type="text/javascript">

//  $("#serch").click(function() {
// 
//    if (document.getElementById("ctl00_contentplaceholder1_ddlSubDivision")!=null && document.getElementById("ctl00_contentplaceholder1_ddlSubDivision").value != 'select') {
//            document.getElementById("ctl00_contentplaceholder1_hdndivision").value = document.getElementById("ctl00_contentplaceholder1_ddlSubDivision").value;
//           
//        }

//        if (document.getElementById("ctl00_contentplaceholder1_ddlBank")!=null && document.getElementById("ctl00_contentplaceholder1_ddlBank").value != 'select' ) {
//            document.getElementById("ctl00_contentplaceholder1_hdnbank").value = document.getElementById("ctl00_contentplaceholder1_ddlBank").value;
//            
//        }
//        if (document.getElementById("ctl00_contentplaceholder1_ddlUser")!=null && document.getElementById("ctl00_contentplaceholder1_ddlUser").value != 'select') {
//            document.getElementById("ctl00_contentplaceholder1_hdnuser").value = document.getElementById("ctl00_contentplaceholder1_ddlUser").value;
//           
//        }
//        //location.reload();
// 
//       ShowAllData();




//    }


//);

function GenerateReport()
{
    if (document.getElementById("ctl00_contentplaceholder1_ddlSubDivision")!=null && document.getElementById("ctl00_contentplaceholder1_ddlSubDivision").value != 'select') {
            document.getElementById("ctl00_contentplaceholder1_hdndivision").value = document.getElementById("ctl00_contentplaceholder1_ddlSubDivision").value;
           
        }

        if (document.getElementById("ctl00_contentplaceholder1_ddlBank")!=null && document.getElementById("ctl00_contentplaceholder1_ddlBank").value != 'select' ) {
            document.getElementById("ctl00_contentplaceholder1_hdnbank").value = document.getElementById("ctl00_contentplaceholder1_ddlBank").value;
            
        }
        if (document.getElementById("ctl00_contentplaceholder1_ddlUser")!=null && document.getElementById("ctl00_contentplaceholder1_ddlUser").value != 'select') {
            document.getElementById("ctl00_contentplaceholder1_hdnuser").value = document.getElementById("ctl00_contentplaceholder1_ddlUser").value;
           
        }
        //location.reload();
 
       ShowAllData();
}
    function ShowUser(result) {
       
        if (result != "") {
            var options = result.split("$");
            var addoption, addoption1;
            var i;
            for (i = 0; i < options.length - 1; i++) {
                var splitoption = options[i].split("#");
                addoption = $('<option></option>').attr("value", splitoption[0]).text(splitoption[1]);
                $("#ddlUser").append(addoption);
            }
        }
    }
    


  
</script>
</asp:Content>
