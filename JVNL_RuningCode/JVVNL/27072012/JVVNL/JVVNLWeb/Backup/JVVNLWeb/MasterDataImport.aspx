<%@ Page Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="MasterDataImport.aspx.cs"
    Inherits="JVVNLWeb.MasterDataImport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentplaceholder1" runat="Server">

    <script type="text/javascript">
        // Declare known file extensions in an array
        //var validFilesTypes = ["bmp", "gif", "png", "jpg", "jpeg", "doc", "xls"];
        var validFilesTypes = ["txt"];

        function ValidateFile() {
            var file = document.getElementById("<%=FileUpload1.ClientID%>");
            var lblMessage = document.getElementById("<%=lblMessage.ClientID%>");
            var path = file.value;
            var ext = path.substring(path.lastIndexOf(".") + 1, path.length).toLowerCase();
            var isValidFile = false;
            for (var i = 0; i < validFilesTypes.length; i++) {
                if (ext == "txt") {
                    isValidFile = true;
                    break;
                }
            }
            if (!isValidFile) {
                lblMessage.style.color = "red";
                lblMessage.innerHTML = "Invalid File. Please upload a File with" + " extension:\n\n" + validFilesTypes.join(", ");
            }
            else {
                lblMessage.innerHTML = "";
            }
            return isValidFile;
        }
    </script>

    <div class="grid_12">
        <div class="box">
            <h2>
                Data Import - Consumer Bill Details <span class="l"></span><span class="r"></span>
            </h2>
            <div class="block">
                <div class="form_place" id="Divform">
                    <table>
                        <tr>
                            <td>
                                <asp:FileUpload ID="FileUpload1" runat="server" />&nbsp;&nbsp;
                            </td>
                            <td>
                                <asp:Button ID="btnUpload" Text="Upload Data" runat="server" class="button orange small submit alignleft"
                                    OnClientClick="return ValidateFile();" onclick="btnUpload_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
