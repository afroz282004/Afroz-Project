<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true"
    CodeBehind="Cluster.aspx.cs" Inherits="KVICWeb.Cluster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentplaceholder1" runat="server">

    <script type="text/javascript">
        jQuery(document).ready(function() {
            PopulateClusters();
            PopulateState();
            PopulateImpmAgency();
            PopulateTechAgency();

            document.getElementById("address").disabled = true;
            
        });
        function NoSpace(e) {

            var keycode = e.charCode ? e.charCode : e.keyCode;
            if (keycode == 32 || keycode == 17) {
                return false;
            }
        }
        function OnlyAlpha(e) {
            var keycode = e.charCode ? e.charCode : e.keyCode;
            if ((keycode >= 33 && keycode <= 57) || keycode == 64 || (keycode >= 94 && keycode <= 96) || (keycode >= 123 && keycode <= 127)) {
                return false;
            }
        }
        function imposeMaxLength(Object, MaxLen) {
            return (Object.value.length <= MaxLen);
        }
    </script>

    <div class="grid_12">
        <div class="box">
            <h2>
                Cluster Search <span class="l"></span><span class="r"></span>
            </h2>
            <div class="block">
                <div class="form_place" id="form1">
                    <table class="fieldtable">
                        <tr>
                            <td>
                                Cluster Name:
                            </td>
                            <td>
                                <input type="text" id="sclustername" name="sclustername" size="20px" maxlength="20" />
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
    <div id="clusterdiv">
        <table class="fieldtable">
            <tr>
                <td>
                    Cluster Name
                </td>
                <td>
                    <input type="text" name="clustername" id="clustername" size="50px" maxlength="100"
                        onkeypress="return OnlyAlpha(event);" oncontextmenu="return false;" />
                </td>
            </tr>
            <tr>
                <td>
                    Address
                </td>
                <td>
                    <textarea id="address" style="height: 100px; width: 255px" onkeypress="return imposeMaxLength(this, 100);"></textarea>
                </td>
            </tr>
            <tr>
                <td>
                    State
                </td>
                <td>
                    <select id="state" name="state">
                        <option value="select">----------Select------------</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td>
                    Implementing Agency
                </td>
                <td>
                    <select id="implementinAgency" name="implementinAgency">
                        <option value="select">----------Select------------</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td>
                    Technical Agency
                </td>
                <td>
                    <select id="technicalAgency" name="technicalAgency">
                        <option value="select">----------Select------------</option>
                    </select>
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
    
    <div id="editclusterdiv">
        <table class="fieldtable">
            <tr>
                <td>
                    Cluster Name
                </td>
                <td>
                    <input type="text" name="eclustername" id="eclustername" size="50px" maxlength="100"
                        onkeypress="return OnlyAlpha(event);" oncontextmenu="return false;" />
                </td>
            </tr>
            <tr>
                <td>
                    Address
                </td>
                <td>
                    <textarea id="eaddress" style="height: 100px; width: 255px" onkeypress="return imposeMaxLength(this, 100);"></textarea>
                </td>
            </tr>
            <tr>
                <td>
                    State
                </td>
                <td>
                    <select id="estate" name="estate">
                        <option value="select">----------Select------------</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td>
                    Implementing Agency
                </td>
                <td>
                    <select id="eimplementingAgency" name="eimplementingAgency">
                        <option value="select">----------Select------------</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td>
                    Technical Agency
                </td>
                <td>
                    <select id="etechnicalAgency" name="etechnicalAgency">
                        <option value="select">----------Select------------</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <button type="button" class="button green small" id="editsave">
                        SAVE</button>
                </td>
            </tr>
        </table>
    </div>
    
    <div class="grid_12">
        <div class="box">
            <h2>
                Cluster Search Result <span class="l"></span><span class="r"></span>
            </h2>
            <div class="block">
                <div class="block_in" id="resulttable">
                    <!-- BEGIN TABLE -->
                    <table class="table display" id="basictable">
                        <thead>
                            <tr>
                                <th>
                                    Cluster Name
                                </th>
                                <th>
                                    Address
                                </th>
                                <th>
                                    Implementing Agency
                                </th>
                                <th>
                                    Technical Agency
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

    <script type="text/javascript">
        $("#new").click(function() {
            $("#clusterdiv").dialog('open');
        });
        $("#save").click(function() {
            var clustername = document.getElementById("clustername").value;
            var cname = clustername.replace("&", "zxc");
            var address = document.getElementById("address").value;
            var stateid = document.getElementById("state").value;
            var impagencyid = document.getElementById("implementinAgency").value;
            var techagencyid = document.getElementById("technicalAgency").value;
            if (clustername == "") {
                alert("Enter Cluster Name");
                document.getElementById("clustername").focus();
                return;
            }
            if (address == "") {
                alert("Enter Address");
                document.getElementById("address").focus();
                return;
            }
            if (impagencyid == "select" || impagencyid == "0") {
                alert("Select Implementing Agency Name");
                document.getElementById("implementinAgency").focus();
                return;
            }

            if (stateid == "select" || stateid == "0") {
                alert("Select State");
                document.getElementById("state").focus();
                return;
            }


            if (techagencyid == "select" || techagencyid == "0") {
                alert("Select Technical Agency Name");
                document.getElementById("technicalAgency").focus();
                return;
            }
            $.post("handler/clusters.ashx?type=insert&clustername=" + cname + "&address=" + address + "&stateid=" + stateid + "&impagency=" + impagencyid + "&techagency=" + techagencyid, $("#form").serialize(), SaveData);
        });
        function SaveData(result) {
            alert(result);
            if (result.toString().indexOf("already") >= 0)
                document.getElementById("clustername").focus();
            else
                ClearControls();

        }
        function ClearControls() {
            document.getElementById("clustername").value = "";
            document.getElementById("address").value = "";
            document.getElementById("implementinAgency").value = "select";
            document.getElementById("technicalAgency").value = "select";
            location.reload();
            
        }
        $("#search").click(function() {
            location.reload();

        });
        function PopulateClusters() {
            var clustername = document.getElementById("sclustername").value;
            $.post("handler/clusters.ashx?type=search&clustername=" + clustername, $("#form").serialize(), SearchResult);
        }
        function SearchResult(result) {
            $("#basictable > tbody").html("");
            $("#basictable").append(result);
            oTable = $('#basictable').dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers"
            });

        }

        function PopulateImpmAgency() {
            $.post("handler/Transaction.ashx?type=popImpAgency", $("#form").serialize(), ShowImpmAgency);
        }

        function ShowImpmAgency(result) {
            if (result != "") {
                var options = result.split("$");
                var addoption, addoption1;
                var i;
                for (i = 0; i < options.length - 1; i++) {
                    var splitoption = options[i].split("#");
                    addoption = $('<option></option>').attr("value", splitoption[0]).text(splitoption[1]);
                    $("#implementinAgency, #eimplementingAgency").append(addoption);
                }
            }
        }

        function PopulateTechAgency() {
            $.post("handler/Transaction.ashx?type=popTechAgency", $("#form").serialize(), ShowTechAgenc);
        }
        function ShowTechAgenc(result) {
            if (result != "") {
                var options = result.split("$");
                var addoption, addoption1;
                var i;
                for (i = 0; i < options.length - 1; i++) {
                    var splitoption = options[i].split("#");
                    addoption = $('<option></option>').attr("value", splitoption[0]).text(splitoption[1]);
                    $("#technicalAgency, #etechnicalAgency").append(addoption);
                }
            }
        }

        function PopulateState() {
            $.post("handler/Transaction.ashx?type=popState", $("#form").serialize(), ShowState);
        }
        function ShowState(result) {
            if (result != "") {
                var options = result.split("$");
                var addoption, addoption1;
                var i;
                for (i = 0; i < options.length - 1; i++) {
                    var splitoption = options[i].split("#");
                    addoption = $('<option></option>').attr("value", splitoption[0]).text(splitoption[1]);
                    $("#state, #estate").append(addoption);
                }
            }
        }
        $("#clusterdiv").dialog({
            autoOpen: false,
            title: 'Cluster Details',
            modal: true,
            autoresize: true,
            width: 450,
            height: 320
        });

        $("#editclusterdiv").dialog({
            autoOpen: false,
            title: 'Cluster Details',
            modal: true,
            autoresize: true,
            width: 450,
            height: 320
        });

        function EditRecord(id) {
            $.post("handler/clusters.ashx?type=edit&clusterid=" + id, $("#form").serialize(), ShowEditRec);
        }
        function ShowEditRec(result) {
            if (result.toString().indexOf('#') >= 0) {
                var data = result.toString().split('#')
                document.getElementById("eclustername").value = data[0];
                document.getElementById("eaddress").value = data[1];
                document.getElementById("estate").value = data[2];
                document.getElementById("eimplementingAgency").value = data[3];
                document.getElementById("etechnicalAgency").value = data[4]

                $("#editclusterdiv").dialog('open');
                document.getElementById("eaddress").focus();
            }
            else
                alert(result);
        }

        $("#editsave").click(function() {
            alert(document.getElementById("eclustername").value);
            var eclustername = document.getElementById("eclustername").value;
            var eaddress = document.getElementById("eaddress").value;
            var estate = document.getElementById("estate").value;
            var eimplementingAgency = document.getElementById("eimplementingAgency").value;
            var etechnicalAgency = document.getElementById("etechnicalAgency").value;

            if (eaddress == "") {
                alert("Enter Address");
                document.getElementById("address").focus();
                return;
            }

            if (estate == 'select') {
                alert('Select Product');
                document.getElementById("products").focus();
                return;
            }

            if (eimplementingAgency == 'select') {
                alert('Select Implementing Agency');
                document.getElementById("products").focus();
                return;
            }

            if (etechnicalAgency == 'select') {
                alert('Select Technical Agency');
                document.getElementById("products").focus();
                return;
            }
            var cname = eclustername.replace("&", "zxc");
            $.post("handler/clusters.ashx?type=update&clustername=" + cname + "&address=" + eaddress + "&stateid=" + estate + "&impagency=" + eimplementingAgency + "&techagency=" + etechnicalAgency, $("#form").serialize(), SaveEditData);
        });

        function SaveEditData(result) {
            alert(result);
            location.reload();
        }

        $(document).ready(function() {
            $("#implementinAgency").change(function() {
                var ddlVal = document.getElementById("implementinAgency").value;

                if (ddlVal == 'select') {
                    alert('Select Product');
                }
                else {
                    $.post("handler/clusters.ashx?type=ImplmentingAddress&id=" + ddlVal, $("#form").serialize(), FillAddress);
                }

            });
        });


        function FillAddress(result) {
            var data = result.toString();
            document.getElementById("address").disabled = false;
            document.getElementById("address").value = data;
            
            
        }
    </script>

</asp:Content>
