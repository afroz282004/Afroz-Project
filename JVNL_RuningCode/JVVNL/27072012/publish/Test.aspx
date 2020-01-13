<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="JVVNLWeb.Test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<HEAD>
</HEAD>
<BODY>

<SCRIPT type="text/javascript" LANGUAGE="JavaScript">
function executeCommands(inputparms)
{
// Instantiate the Shell object and invoke
// its execute method.

//var oShell = new ActiveXObject("Shell.Application"); WScript.Shell
var oShell = new ActiveXObject("WScript.Shell");


//sRegVal = "HKCUSoftwareMicrosoftWindows "
//sRegVal = sRegVal & "NTCurrentVersionWindowsDevice"
sRegVal = 'HKEY_CURRENT_USER\\Software\\Microsoft\\Windows NT\\CurrentVersion\\Windows\\Device';
sDefault = "";
sDefault = oShell.RegRead(sRegVal);
//sDefault = Left(sDefault, InStr(sDefault, ",") - 1)

GetDefaultPrinter = sDefault;

//var commandtoRun = "notepad.exe";
//if (inputparms != "")
// {
// var commandParms = document.Form1.filename.value;
// }

// Invoke the execute method.
//oShell.Run(commandtoRun, commandParms, "", "open", "1");
//oShell.Run(commandtoRun);
alert(GetDefaultPrinter);
document.write(GetDefaultPrinter);
}
</SCRIPT>

<FORM name="Form1">
<CENTER>
<BR><BR>
<!--
<H1>Execute PC Commands From HTML </H1>
<BR><BR>
<File Name to Open:> <Input type="text"
name="filename"/>
<BR><BR>
-->
<input type="Button" name="Button1"
value="Get default printer" onClick="executeCommands()" />
<!--
<BR><BR>
<input type="Button" name="Button2"
value="Run Notepad.exe with Parameters"
onClick="executeCommands(' + hasPARMS + ')" />
-->
</CENTER>
</BODY>
</FORM>

</html>
