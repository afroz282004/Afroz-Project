<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ProductChilde.aspx.cs" Inherits="KVICWeb.ProductChilde" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentplaceholder1" runat="server">

<div class="grid_12">
        <div class="box">
            <h2>
                Product Master Search <span class="l"></span><span class="r"></span>
            </h2>
            <div class="block">
                <div class="form_place" id="form1">
                    <table class="fieldtable">
                        <tr>
                            <td>
                                Product Name:
                            </td>
                            <td>
                                <input type="text" id="serchproductname" name="serchproductname" size="20px" maxlength="20" />
                            </td>
                            <td>
                                <button type="button" class="button red small submit alignleft" id="search">
                                    SEARCH</button>
                                <button type="button" class="button blue small submit alignleft" id="new">
                                    NEW</button>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <div id="productdiv">
        <table class="fieldtable">
            <tr>
                <td>
                    Product Name
                </td>
                <td>
                    <input type="text" name="productname" id="productname" size="35px" maxlength="20"
                        onkeypress="return OnlyAlpha(event);" onpaste="return false;" oncut="return false;"
                        oncontextmenu="return false;" />
                </td>
            </tr>
            
            <tr>
                <td colspan="2" align="center">
                    <button type="button" class="button green small" id="save">
                        SAVE</button>
                    <button type="button" class="button orange small" id="cancel">
                        CANCEL</button>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
