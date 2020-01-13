<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="crreport.aspx.cs" Inherits="JVVNLWeb.crreport" %>
<%@ Register assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title></title>
<script language="JavaScript" type="text/javascript">
 
DA = (document.all) ? 1 : 0
 
</script>
 <script language='VBScript' type="text/vbscript">  

     Sub Print()   

        OLECMDID_PRINT = 6   

        OLECMDEXECOPT_DONTPROMPTUSER = 2  

        OLECMDEXECOPT_PROMPTUSER = 1   

       if DA then
 
         call WB.ExecWB(OLECMDID_PRINT, OLECMDEXECOPT_DONTPROMPTUSER,1)
       else
 
         call WB.IOleCommandTarget.Exec(OLECMDID_PRINT ,OLECMDEXECOPT_DONTPROMPTUSER,"","")
       end if

     End Sub   

     document.write "<object ID='WB' WIDTH=0 HEIGHT=0 CLASSID='CLSID:8856F961-340A-11D0-A96B-00C04FD705A2'></object>"  

 </script>   
 <script type="text/javascript">

    jQuery(document).ready(function() {
     var url = "CheckSession.aspx?hgUserName=" + document.getElementById("username").value;
         $(document).idleTimeout({ inactivity: 600000, noconfirm: 1000, sessionAlive: 600000, redirect_url: url });
    PopulateCounter();
    });
</script>

 <script language="javascript" type="text/javascript">  

     function mycontrol(idDiv) {  
     
     

//      window.print()  

//        window.close()  
//            var w = 600;
//            var h = 400;
//            var l = (window.screen.availWidth - w)/2;
//            var t = (window.screen.availHeight - h)/2;
//        
//            var sOption="toolbar=no,location=no,directories=no,menubar=no,scrollbars=yes,width=" + w + ",height=" + h + ",left=" + l + ",top=" + t; 
//            // Get the HTML content of the div
//            var sDivText = window.document.getElementById(idDiv).innerHTML;
//            // Open a new window
//          var objWindow =window.open("", "Print", sOption);;
//            // Write the div element to the window
//           objWindow.document.write(sDivText);
//            objWindow.document.close(); 
//            // Print the window            
//            objWindow.print();
//            // Close the window
//            objWindow.close(); 

     }  

 </script>  
 </head>
<body onload="mycontrol('AccessCard')">

   <input type="hidden" id="username" value="<% Response.Write(Session["username"].ToString()); %>" />
     <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
            AutoDataBind="true"  DisplayToolbar="False" />

  
</body>
</html>
