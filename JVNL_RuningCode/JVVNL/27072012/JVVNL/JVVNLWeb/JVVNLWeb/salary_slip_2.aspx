<%@ Page Language="VB" AutoEventWireup="false" CodeFile="salary_slip_2.aspx.vb" Inherits="salary_slip_2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
     <title>Salary Slip,RSGSM</title>
<link rel="Stylesheet" href="App_Themes/CSS/style.css" />
  <link rel="Stylesheet" href="App_Themes/CSS/report.css" />
<!--<link rel="Stylesheet" href="App_Themes/CSS/test.css" />-->
<style type="text/css">
.sample {
	font-style: normal;
	font-variant: normal;
	font-weight: normal;
	font-size: 20px;
	line-height: 100%;
	word-spacing: normal;
	letter-spacing: normal;
	text-decoration: none;
	text-transform: none;
	text-align: left;
	text-indent: 0ex;
}

.text_small {
	font-family: verdana;
	font-style: normal;
	font-variant: normal;
	font-weight: normal;
	font-size: 12px;
  text-align: left;
}

.text_small_footer {
	font-family: verdana;
	font-style: normal;
	font-variant: normal;
	font-weight: normal;
	font-size: 12px;
    text-align: left;
    page-break-after:always;
}

 .pagebreak {page-break-after: always;}
  
</style>

<%-- <script language="javascript" type="text/javascript" src="common.js"></script>--%>
 <script language="javascript" type="text/javascript">
 
 
 
 
 function fprintf()
  {
  //  document.getElementById('TABLE1vgfh').style.display='none'
  //  document.getElementById('tdLeft').style.display='none'
    document.getElementById('trprint').style.display='none'
    document.getElementById('trimage').style.display='none'
  //  document.getElementById('tabLogo').style.display=''trimage2
  document.getElementById('trimage2').style.display='none'
  // ' document.getElementById('lnkCriteria').style.display='none'
  //  document.getElementById('lblError').style.display='none'
   // document.getElementById('TABLE1').style.display='none'
  //  document.getElementById('tdRight').style.display='none'
  //  document.getElementById('tabBottom').style.display='none'
    window.print();
   // document.getElementById('TABLE1vgfh').style.display=''
  //  document.getElementById('tdLeft').style.display=''
 //   document.getElementById('tabLogo').style.display='none'
  // ' document.getElementById('lnkCriteria').style.display=''
  //  document.getElementById('lblError').style.display=''
  //  document.getElementById('TABLE1').style.display=''
  //  document.getElementById('tdRight').style.display=''
 //   document.getElementById('tabBottom').style.display=''
  } 
 
 
// function fprintf()
//        {
//              window.print();             
//        }

function UL(act)
    {
        if(act=="over")
        {
            window.event.srcElement.style.cursor = "hand"
            window.event.srcElement.style.textDecoration="underline"
        }
        else
        {
            window.event.srcElement.style.cursor = "none"
            window.event.srcElement.style.textDecoration="none"
        }
    }
  var cnt1 = 0 
  
//  function window.document.documentElement.onclick()
//  {
//  if(event.srcElement.id == "btnView")
//  {
//       FunPrint()
//  }
//  }
//  
  
  
  
  function FunPrint()
{

	window.open("<%=hfdoc.value%>","SalarySlip","toolbar=n,scrollbars=Yes,location=n,status=n,menubar=n,resizable=yes,width=780,height=580,left=0,top=0,offscreenBuffering=true")
	var fso, tf;
	fso = new ActiveXObject("Scripting.FileSystemObject");
	//alert("<%=hfdoc.value%>")
	tf = fso.CreateTextFile("LPT1:",true);
	tf.Writeline ("<%=hidprint.value%>");
	alert("<%=hidprint.value%>")
	tf.Close();
}


  </script>
</head>
<body>
    <form id="form1" runat="server">
    
    
    
    <table id="TABLE1vgfh" runat="server" width="95%" border="0" bordercolor="#d4d9db" cellpadding="0"
      cellspacing="2" bgcolor="#ffffff" align="center">
      <tr>
        <td align="center" valign="top" style="height: 700px">
         <asp:HiddenField ID="HiddenField1" runat="server" />
          <asp:HiddenField ID="PageId" runat="server" />
           <asp:HiddenField ID="hfdoc" runat="server" />
            <asp:HiddenField ID="hidprint" runat="server" />
            
             
          <asp:HiddenField ID="hfUnit" runat="server" />
          <asp:HiddenField ID="hfMonth" runat="server" />
          <asp:HiddenField ID="hfYear" runat="server" />
          <asp:HiddenField ID="hfPayroll" runat="server" />
          <asp:HiddenField ID="hfofficetype" runat="server" />
          <asp:HiddenField ID="hfunittype" runat="server" />
           <asp:HiddenField ID="hfdept" runat="server" />
            
            &nbsp;
          <br />
          <table width="100%" cellspacing="0" cellpadding="0">
            <tr>
              <td>
                <table width="90%" cellspacing="0" cellpadding="0" id="tabTop">
                  <tr id="trimage" runat="server">
                    <td rowspan="2" valign="bottom" style="width: 9px">
                      <img name="form_template_r3_c2" src="images/form_template_r3_c2.gif" width="9" height="8"
                        border="0" alt=""></td>
                    <td class="pageTitle" style="height: 22px">Pay Slip(Mass Printing) - Version-2</td>
                    <td rowspan="2" valign="bottom" style="width: 9px">
                      <img name="form_template_r3_c2" src="images/form_template_r3_c2_rt.gif" width="9"
                        height="8" border="0" alt=""></td>
                  </tr>
                  <tr id="trimage2" runat="server" >
                    <td valign="bottom" style="height: 8px">
                      <img src="images/8x1_grey.jpg" height="8" width="100%"></td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <table width="90%" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td id="tdLeft" width="12" background="images/center_left.gif">
                      &nbsp;</td>
                    <td align="center">
                      <asp:Label ID="lblError" runat="server" CssClass="clsError"></asp:Label>
                      <table id="tabLogo" runat="server" border="0" cellspacing="0" width="100%" style="display:none;">
                            <tr>
                              <td align="center">
                                <asp:Label ID="l1" runat="server" Font-Bold="true"  Font-Size="14px">RAJASTHAN STATE GANGANAGARSUGAR MILLS</asp:Label>
                              </td> 
                              </tr>

                             <tr>
                              <td align="center" >
                                <hr />
                              </td>
                              </tr>     
                              </table>
                      <asp:MultiView ID="mltvTab" runat="server" ActiveViewIndex="0">
                        <asp:View ID="Criteria" runat="server">
                        <center>
                          <div id="divcriteria"  runat="server" style="width: 90%;">
<br /><center>
                            <table >
                            
                            
                            <tr id="ltype" runat="server">
                                    <td>
                                        Location Type</td>
                                    <td align="center" style="width: 20px">
                                        :</td>
                                    <td style="width: 246px">
                                        <asp:DropDownList ID="ddllocationtype" runat="server" AutoPostBack="True" DataTextField="office_type_name"
                                            DataValueField="office_type_id" Width="240px">
                                        </asp:DropDownList></td>
                                </tr>
                                
                                
                                
                                <tr id="lunit1" runat="server">
                                    <td>
                                        Unit</td>
                                    <td align="center" style="width: 20px">
                                        :</td>
                                    <td style="width: 246px">
                                        <asp:DropDownList ID="ddlunit" runat="server" AutoPostBack="True" DataTextField="office_name"
                                            DataValueField="office_code" Width="240px">
                                        </asp:DropDownList></td>
                                </tr>
                            
                            
                            
                            
                                <tr id="loc" runat="server">
                                    <td>
                                        Location </td>
                                    <td align="center" style="width: 20px">
                                        :</td>
                                    <td style="width: 246px">
                                        <asp:DropDownList ID="ddloffice" runat="server" AutoPostBack="True" DataTextField="office_name"
                                            DataValueField="office_code" Width="240px">
                                        </asp:DropDownList></td>
                                </tr>
                                <tr id="Tr1" runat="server">
                                    <td style="height: 16px">
                                        Department</td>
                                    <td align="center" style="width: 20px; height: 16px">
                                        :</td>
                                    <td style="width: 246px; height: 16px">
                                        <asp:DropDownList ID="ddldept" runat="server" AutoPostBack="True" DataTextField="dept_name"
                                            DataValueField="dept_id" Width="240px">
                                        </asp:DropDownList></td>
                                </tr>
                             <%--   <tr>
                                    <td>
                                        Employee Name</td>
                                    <td align="center" style="width: 20px">
                                        :</td>
                                    <td style="width: 246px">
                                        <asp:DropDownList ID="ddlemployee" runat="server" DataTextField="employee_name" DataValueField="employee_id"
                                            Width="240px">
                                        </asp:DropDownList></td>
                                </tr>--%>
                              <tr>
                                <td>
                                  Month &amp; Year</td>
                                <td align="center" style="width: 20px">
                                    :</td>
                                <td style="width: 246px">
                                    <asp:DropDownList ID="ddlMonth" runat="server" Width="88px" >
                                        <asp:ListItem Value="1">January</asp:ListItem>
                                        <asp:ListItem Value="2">February</asp:ListItem>
                                        <asp:ListItem Value="3">March</asp:ListItem>
                                        <asp:ListItem Value="4">April</asp:ListItem>
                                        <asp:ListItem Value="5">May</asp:ListItem>
                                        <asp:ListItem Value="6">June</asp:ListItem>
                                        <asp:ListItem Value="7">July</asp:ListItem>
                                        <asp:ListItem Value="8">August</asp:ListItem>
                                        <asp:ListItem Value="9">September</asp:ListItem>
                                        <asp:ListItem Value="10">October</asp:ListItem>
                                        <asp:ListItem Value="11">November</asp:ListItem>
                                        <asp:ListItem Value="12">December</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlYear" runat="server">
                                    </asp:DropDownList></td>
                              </tr>
                              <tr><td colspan="3">&nbsp;</td></tr>
                              <tr>
                                <td colspan="2">&nbsp;</td>
                                <td align="left" style="width: 246px">
                                <asp:ImageButton ID="btnView" runat="server" ImageUrl="~/images/view.gif"/>
                                <asp:ImageButton ID="btnReset" runat="server" ImageUrl="~/images/reset.jpg" />
                                </td>
                              </tr>
                            </table>
                            </center><br />
                          </div></center>
                        </asp:View>
                        <asp:View ID="criteria2" runat="server">         
         <table id="TABLE1" runat="server" width="100%" border="0" bordercolor="#d4d9db" cellpadding="0"
            cellspacing="2" bgcolor="#ffffff">
            <tr id= "trprint" runat="server" >        
            <td align="left" width="100%">
            <%--<img id="btnPrint" runat="server" onclick='FunPrint()' alt="Print" style="cursor:hand" src="images/print.gif" title="Print"  />--%>
            
            
            <% If Request("action") <> "Print" Then%>
            <A style='text-decoration:none;border:none' onmouseover="javascript:UL('over') "
            onmouseout="javascript:UL('out')" onClick='FunPrint()'  >
            <img src="images/print.gif" align="absBottom">Dot Matrix</A>
            <% End If%>
         <%--  
            <% If Request("action") <> "Print" Then%>
            <A style='text-decoration:none;border:none'  onclick='FunPrint()' style="cursor:hand"
              >
            <img src="images/print.gif" align="absBottom"   >Dot Matrix</A>
            <% End If%>--%>
         </td>
            <td align="right" width="100%">
            <% If Request("action") <> "Print" Then%>
            <A style='text-decoration:none;border:none' onmouseover="javascript:UL('over') "
            onmouseout="javascript:UL('out')" onClick="javascript:fprintf()"  >
            <img src="images/print.gif" align="absBottom">Laser</A>
            <% End If%>
            </td>

            </tr>
            <tr>
                <td align="center" valign="top">
                    <asp:HiddenField ID="HiddenField2" runat="server" />
                    <asp:HiddenField ID="PROJECT_ID" runat="server" />
                    
                  <asp:Label ID="lblErrMsg" runat="server" Font-Size="Small" ForeColor="Red"></asp:Label>
                     <br />
                        <asp:Panel ID="pnlCTC" runat="server" Width="95%">
                        
                        </asp:Panel>
                         <br />
                         <br />
                     </td>
                     </tr>
                    </table>
                    
                       <div id="divSlip" style="width: 100%" runat="server"><center><br />
                              <table cellpadding="0" cellspacing="0" width="90%">
                              <tr style="width:100%">
                                <td style="width:100%">
                                    <table cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                        <td colspan="7" align="center">
                                        <div style="width: 100%" align="center">      
                                                        <table id="report" >
                                                            <tr>
                                                                <td valign="top" style="width: 5px">                                       
                                                                      <asp:Panel ID="pnlpersonal" runat="server" Width="100%" BackColor="#ffffff">
                                                                    
                                                                    </asp:Panel>                                                      
                                                                     
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                            
                                                            
                                                            <tr >
                                                            <td>
                                                                    <asp:Panel ID="pnlEarnings" runat="server" Width="100%" BackColor="#ffffff">
                                                                 <%--    <br />--%>
                                                                    </asp:Panel>
                                                            </td>
                                                            <td style="width: 5px">
                                                                    <asp:Panel ID="pnlDeduction" runat="server" Width="100%" BackColor="#ffffff">
                                                                   <%-- <br />--%>
                                                                    </asp:Panel>
                                                                    
                                                                    
                                                                    
                                                            </td>
                                                            
                                                            </tr>
                                                            <tr>
                                                            <td>
                                                            
                                                             <asp:Panel ID="pnlpayable" runat="server" Width="100%" BackColor="#ffffff">
                                                                     <br />
                                                                    </asp:Panel>
                                                            </td>
                                                            
                                                            
                                                            </tr>
                                                            
                                                        </table>
                                                        <br />
                                                    </div>
                                        </td> 
                                        </tr>
                                        
                                        <tr ><td colspan="7" align="center">
                                        <img id="btnPrintDetail" src="images/print_new.jpg" onclick="javascript:fprintf()" runat="server" alt=""/>
                                        <%--<asp:ImageButton ID="btnPrintDetail" runat="server" ImageUrl="images/print_new.jpg" OnClientClick="javascript:printDetail()"/>--%>
                                        <asp:ImageButton ID="ImageButton1" runat="server"  ImageUrl="images/export.jpg" />
                                        <asp:ImageButton ID="btnCancelDetail" runat="server"  ImageUrl="images/cancel.jpg" />

                                        </td></tr>
                                    </table>
                                </td>
                              </tr>
                              </table>
                          </center></div>

                        </asp:View>
                      </asp:MultiView></td>
                    <td id="tdRight" background="images/center_rt.gif" style="width: 12px" width="12px">
                      <img src="images/spacer.gif" width="1" height="1"></td>
                  </tr>
                </table>
              </td>
            </tr>
            <tr>
              <td>
                <table id="tabBottom" width="90%" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td width="1%" style="height: 8px">
                      <img name="form_template_r6_c2" src="images/form_template_r6_c2.gif" width="9" height="8"
                        border="0" alt=""></td>
                    <td width="98%" background="images/form_template_r6_c3.gif" style="height: 8px">
                      <img src="images/spacer.gif" width="1" height="1" alt="" /></td>
                    <td width="1%" style="height: 8px">
                      <div align="left">
                        <img name="form_template_r6_c6" src="images/form_template_r6_c6.gif" width="9" height="8"
                          border="0" alt=""></div>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
        </td>
      </tr>
    </table>
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
     
           </form>
</body>
</html>