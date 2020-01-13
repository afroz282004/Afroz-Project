<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true"
    CodeBehind="ImpAgency.aspx.cs" Inherits="KVICWeb.ImpAgency" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentplaceholder1" runat="server">

    <script type="text/javascript">
        jQuery(document).ready(function() {
            PopulateImpAgency();
            PopulateProduct();
        });
        function OnlyNumeric(e) {
            var keycode = e.charCode ? e.charCode : e.keyCode;
            alert(keycode);
            if ((keycode >= 47 && keycode <= 57) || keycode == 8 || keycode == 46 || keycode == 45 || keycode == 95 || keycode == 44) {
                return true;
            }
            else {
                return false;
            }
        }
        function OnlyAlpha(e) 
        {
            var keycode = e.charCode ? e.charCode : e.keyCode;
            if ((keycode >= 33 && keycode <= 57) || keycode == 64 || (keycode >= 94 && keycode <= 96) || (keycode >= 123 && keycode <= 127)) 
            {
                return false;
            }
        }

    </script>

    <script type="text/javascript">
        function OnlyNumeric(e) {
            var keycode = e.charCode ? e.charCode : e.keyCode;
            if ((keycode >= 47 && keycode <= 57) || keycode == 9 || keycode == 8 || keycode == 46 || keycode == 45 || keycode == 95 || keycode == 44) {
                return true;
            }
            else {
                return false;
            }
        }

        function checkEmail(myForm) {
            alert('HHH');
            if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(myForm.email.value)) {
                return (true)
            }
            alert("Invalid E-mail Address! Please re-enter.")
            return (false)
        }


        function verifyEmail() {
            alert('lll');
            var status = false;
            var emailRegEx = /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i;
            var myform = document.getElementById("email");
            alert(myform);
            if (document.myform.email.value.search(emailRegEx) == -1) {
                alert("Please enter a valid email address.");
            }
            else if (document.myform.email.value != document.myform.email2.value) {
                alert("Email addresses do not match.  Please retype them to make sure they are the same.");
            }
            else {
                alert("Woohoo!  The email address is in the correct format and they are the same.");
                status = true;
            }
            return status;
        }
    </script>

    <div class="grid_12">
        <div class="box">
            <h2>
                Implementing Agency Search <span class="l"></span><span class="r"></span>
            </h2>
            <div class="block">
                <div class="form_place" id="form1">
                    <table class="fieldtable">
                        <tr>
                            <td>
                                Agency Name:
                            </td>
                            <td>
                                <input type="text" id="simpagencyname" name="simpagencyname" size="20px" maxlength="20" />
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
    <div id="impagencydiv">
        <table class="fieldtable">
            <tr>
                <td>
                    Implementing Agency Name
                </td>
                <td>
                    <input type="text" name="impagencyname" id="impagencyname" size="35px" maxlength="100"
                        onkeypress="return OnlyAlpha(event);" onpaste="return false;" oncut="return false;"
                        oncontextmenu="return false;" />
                </td>
            </tr>
            <tr>
                <td>
                    Address
                </td>
                <td>
                    <%-- <input type="text" name="address" id="address" style="height:35px;" size="35px" maxlength="200" 
                        onpaste="return false;" oncut="return false;" oncontextmenu="return false;" />--%>
                    <textarea id="address" style="height: 70px; width: 200px;"></textarea>
                </td>
            </tr>
            <tr>
                <td>
                    Phone
                </td>
                <td>
                    <input type="text" name="phone" id="phone" size="35px" maxlength="100" onkeypress="return OnlyNumeric(event);"
                        onpaste="return false;" oncut="return false;" oncontextmenu="return false;" />
                </td>
            </tr>
            <tr>
                <td>
                    Fax
                </td>
                <td>
                    <input type="text" name="fax" id="fax" size="35px" maxlength="100" onkeypress="return OnlyNumeric(event);"
                        onpaste="return false;" oncut="return false;" oncontextmenu="return false;" />
                </td>
            </tr>
            <tr>
                <td>
                    E-mail
                </td>
                <td>
                    <input type="text" name="email" id="email" size="35px" maxlength="50" />
                </td>
            </tr>
            <tr>
                <td>
                    Products
                </td>
                <td>
                    <select id="products" name="products">
                        <option value="select">--------Select----------</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <%-- <select id="productChild" name="productChild" multiple="multiple" onchange='showselection()'>
                        <option value="select">--------Select----------</option>
                    </select>--%>
                    <input type="hidden" id="hiddenv" />
                    <%--<input type="text" name="productChild" id="productChild" size="35px" maxlength="50"  />--%>
                    <textarea id="productChild" style="height: 70px; width: 200px;"></textarea>
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
    
    <div id="editimpagency">
        <table class="fieldtable">
            <tr>
                <td>
                    Implementing Agency Name
                </td>
                <td>
                    <input type="text" name="eimpagencyname" id="eimpagencyname" size="35px" maxlength="100" readonly="readonly" />
                </td>
            </tr>
            <tr>
                <td>
                    Address
                </td>
                <td>
                    <%-- <input type="text" name="eaddress" id="eaddress" style="height:35px;" size="35px" maxlength="200" 
                        onpaste="return false;" oncut="return false;" oncontextmenu="return false;" />--%>
                    <textarea id="eaddress" style="height: 70px; width: 200px;"></textarea>
                </td>
            </tr>
            <tr>
                <td>
                    Phone
                </td>
                <td>
                    <input type="text" name="ephone" id="ephone" size="35px" maxlength="100" onkeypress="return OnlyNumeric(event);"
                        onpaste="return false;" oncut="return false;" oncontextmenu="return false;" />
                </td>
            </tr>
            <tr>
                <td>
                    Fax
                </td>
                <td>
                    <input type="text" name="efax" id="efax" size="35px" maxlength="100" onkeypress="return OnlyNumeric(event);"
                        onpaste="return false;" oncut="return false;" oncontextmenu="return false;" />
                </td>
            </tr>
            <tr>
                <td>
                    E-mail
                </td>
                <td>
                    <input type="text" name="eEmail" id="eEmail" size="35px" maxlength="50" />
                </td>
            </tr>
            <tr>
                <td>
                    Products
                </td>
                <td>
                    <select id="eproducts" name="products">
                        <option value="select">--------Select----------</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <textarea id="eproductChild" style="height: 70px; width: 200px;"></textarea>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <button type="button" class="button green small" id="editsave">
                        SAVE</button>
<%--                    <button type="button" class="button orange small" id="editcancel">
                        CANCEL</button>--%>
                </td>
            </tr>
        </table>
    </div>
    
    <div class="grid_12">
        <div class="box">
            <h2>
                Implementing Agency Search Result <span class="l"></span><span class="r"></span>
            </h2>
            <div class="block">
                <div class="block_in" id="resulttable">
                    <!-- BEGIN TABLE -->
                    <table class="table display" id="basictable">
                        <thead>
                            <tr>
                                <th>
                                    Agency Name
                                </th>
                                <th>
                                    Address
                                </th>
                                <th>
                                    Phone
                                </th>
                                <th>
                                    Fax
                                </th>
                                <th>
                                    Email
                                </th>
                                <th>
                                    Action
                                </th>
                            </tr>
                        </thead>
                        <tbody id="tbody">
                        </tbody>
                    </table>
                    <!-- END TABLE -->
                </div>
            </div>
        </div>
    </div>
    <!-- End of .grid_12 -->

    <script language='javascript'>
        function showselection() {

            var opt = document.getElementById("productChild");
           
            var numofoptions = opt.length
            
            var selValue = new Array

            var j = 0
            for (i = 0; i < numofoptions; i++) {
                if (opt[i].selected === true) {
                    selValue[j] = opt[i].value
                    j++
                }
            }

            selValue = selValue.join("+")

            document.getElementById("hiddenv").value = selValue
           
        }  
    </script>

    <script type="text/javascript">
        $("#new").click(function() {
            $("#impagencydiv").dialog('open');
        });
        $("#save").click(function() {
            var agencyname = document.getElementById("impagencyname").value;
            var address = document.getElementById("address").value;
            var iaddress = address.replace("#", "zxc");
            
            var phone = document.getElementById("phone").value;
            var fax = document.getElementById("fax").value;
            var email = document.getElementById("email").value;

            var products = document.getElementById("products").value;

            var productChild = document.getElementById("productChild").value;


            if (agencyname == "") {
                alert("Enter Agency Name");
                document.getElementById("impagencyname").focus();
                return;
            }
            if (address == "") {
                alert("Enter Address");
                document.getElementById("address").focus();
                return;
            }

            if (products == 'select') {
                alert('Select Product');
                document.getElementById("products").focus();
                return;

            }

            if (productChild == "") {
                alert("Enter product details");
                document.getElementById("productChild").focus();
                return;
            }
            $.post("handler/impagency.ashx?type=insert&impagencyname=" + agencyname + "&address=" + iaddress + "&phone=" + phone + "&fax=" + fax + "&email=" + email + "&products=" + products + "&productChild=" + productChild, $("#form").serialize(), SaveData);
        });
        
        function SaveData(result) {
            alert(result);
            if (result.toString().indexOf("already") >= 0)
                document.getElementById("impagencyname").focus();
            else
                ClearControls();
        }


        $("#cancel").click(function() {
            ClearControls();
        });
        
        
        
        function ClearControls() {
            document.getElementById("impagencyname").value = "";
            document.getElementById("address").value = "";
            location.reload();
        }
        $("#search").click(function() {
            location.reload();
        });
        function PopulateImpAgency() {
            var impagencyname = document.getElementById("simpagencyname").value;
            $.post("handler/impagency.ashx?type=search&impagencyname=" + impagencyname, $("#form").serialize(), SearchResult);
        }
        function SearchResult(result) {
            $("#basictable > tbody").html("");
            $("#basictable").append(result);
            oTable = $('#basictable').dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers"
            });

        }
        
        

        $("#impagencydiv").dialog({
            autoOpen: false,
            title: 'Implementing Agency Details',
            modal: false,
            autoresize: true,
            width: 500,
            height: 380
        });

        $("#editimpagency").dialog({
            autoOpen: false,
            title: 'Implementing Agency Details',
            modal: true,
            autoresize: true,
            width: 500,
            height: 380
        });
        function PopulateProduct() {
           
            $.post("handler/impagency.ashx?type=popProduct", $("#form").serialize(), ShowCluster);
        }

        function ShowCluster(result) {
            if (result != "") {
                var options = result.split("$");
                var addoption, addoption1;
                var i;
                for (i = 0; i < options.length - 1; i++) {
                    var splitoption = options[i].split("#");
                    addoption = $('<option></option>').attr("value", splitoption[0]).text(splitoption[1]);
                    $("#products,#eproducts").append(addoption);                   
                }
            }
        }

        function EditRecord(id) {
            $.post("handler/impagency.ashx?type=edit&id=" + id, $("#form").serialize(), ShowEditRec);

        }
        function ShowEditRec(result) {
            if (result.toString().indexOf('#') >= 0) {
                var data = result.toString().split('#')
                document.getElementById("eimpagencyname").value = data[0];
                document.getElementById("eaddress").value = data[1];
                document.getElementById("ephone").value = data[2];
                document.getElementById("efax").value = data[3];
                document.getElementById("eEmail").value = data[4]
                document.getElementById("eproducts").value = data[5]
                document.getElementById("eproductChild").value = data[6];

                $("#editimpagency").dialog('open');
                document.getElementById("eaddress").focus();
            }
            else
                alert(result);
        }

        $("#editsave").click(function() {
            var eimpagencyname = document.getElementById("eimpagencyname").value;
            var eaddress = document.getElementById("eaddress").value;
            var iaddress = eaddress.replace("#", "zxc");
            
            var ephone = document.getElementById("ephone").value;
            var efax = document.getElementById("efax").value;
            var eEmail = document.getElementById("eEmail").value;
            var eproducts = document.getElementById("eproducts").value;
            var eproductChild = document.getElementById("eproductChild").value;

            if (eimpagencyname == "") {
                alert("Enter Agency Name");
                document.getElementById("eimpagencyname").focus();
                return;
            }
            if (eaddress == "") {
                alert("Enter Address");
                document.getElementById("address").focus();
                return;
            }

            if (eproducts == 'select') {
                alert('Select Product');
                document.getElementById("products").focus();
                return;

            }

            if (eproductChild == "") {
                alert("Enter product details");
                document.getElementById("productChild").focus();
                return;
            }
            $.post("handler/impagency.ashx?type=update&impagencyname=" + eimpagencyname + "&address=" + eaddress + "&phone=" + ephone + "&fax=" + efax + "&email=" + eEmail + "&products=" + eproducts + "&productChild=" + eproductChild, $("#form").serialize(), SaveEditData);
        });

        function SaveEditData(result) {
            alert(result);
            location.reload();
        }
        


//        $(document).ready(function() {

//            $("#email").change(function() {
//                verifyEmail();
//            });
//        });

//        function ShowProductChid(result) {
//            $('#productChild').empty();
//            
//            if (result != "") {
//                var options = result.split("$");
//                var addoption, addoption1;                
//                var i;
//                for (i = 0; i < options.length - 1; i++) {
//                    var splitoption = options[i].split("#");
//                    addoption = $('<option></option>').attr("value", splitoption[0]).text(splitoption[1]);
//                    $("#productChild").append(addoption);
//                }
//            }
//        }
    </script>

</asp:Content>
