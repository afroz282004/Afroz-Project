Imports clsOraDBCon
Imports System.Data.OleDb
Imports System.IO
Imports System
'Imports System.IO
Imports System.Collections
Imports System.Text
Imports System.Data
'Imports System.Data.OleDb
Imports System.Web
Imports System.Drawing.Printing
Imports System.IO.StringWriter
Imports System.Web.UI.Html32TextWriter
Imports System.Web.UI.WebControls
Imports System.ComponentModel.Component
Partial Class salary_slip_2
    Inherits System.Web.UI.Page
    Dim Con As New clsOraDBCon
    Dim oCmnfns As New clsCommonfns
    Dim aPermission As Array
    Dim totMCM As Double = 0.0
    Dim totMCA As Double = 0.0
    Dim totMCMR As Double = 0.0
    Dim totMCAR As Double = 0.0
    Dim totMCM1 As Double = 0.0
    Dim totMCA1 As Double = 0.0
    Dim totMCMR1 As Double = 0.0
    Dim totMCAR1 As Double = 0.0
    Dim totMH As Integer
    Dim totYH As Integer
    Dim commentStr As String = ""
    Dim AALAmt As Double = 0.0
    Dim lncnt As Integer
    Dim UnitHeadDesig As String = ""
    Dim UnitHeadName As String = ""
    Dim lastdate, strsql As String
    Dim flagAAL As Boolean
    Dim projStartDate As String
    Dim ProjectId As String
    Dim sql As String
    'Dim strsql As String
    Dim strdate As String
    Dim x, x2 As Integer
    Dim dayInf As String
    Dim fromDate As String
    Dim toDate As String
    Dim totearning As Double = 0.0
    Dim totdeduction As Double = 0.0
    Dim totLoans As Double = 0.0
    Dim totreductions As Double = 0.0
    Dim totadditions As Double = 0.0
    Dim totcompany As Double = 0.0
    Dim sqlstr As String
    Dim n As Integer
    Dim StringWrite As StringWriter
    Dim HtmlWrite As Html32TextWriter
    Dim finaltable As New Table
    Dim rdr As OleDbDataReader
    Dim cmdsal As OleDbCommand
    Dim unitArr As String = ""
    Dim count As Integer
    Dim filename As String
    Dim flg As Boolean = False
    Dim RwCnt As Integer = 0
    Dim RwCnt2 As Integer = 0
    Dim fp As StreamWriter
    Dim tab As Table = New Table()
    Dim pgCount As UInt16 = 1
    Dim Flag_Rec_entry As Boolean = True
    Dim printStr As String
    Dim str3 As String
    Dim str4 As String
    Dim dsE As Data.DataSet
    Dim dsD As Data.DataSet
    Dim dsP As Data.DataSet
    Dim dsL As Data.DataSet
    Dim dsPP As Data.DataSet
    Dim daE As OleDbDataAdapter
    Dim daD As OleDbDataAdapter
    Dim daP As OleDbDataAdapter
    Dim daL As OleDbDataAdapter
    'Dim daPP As valu

    Dim arrernt As String
    Dim arrern As String
    Dim arrded As String
    Dim arrdedamt As String
    Dim payable As Integer
    Dim arrernamt As String
    Dim totalern As String
    Dim earnmax As Integer
    Dim dedmax As Integer
    Dim i As Integer
    Dim totcount As Integer
    Dim present As Double
    Dim lwp As Double
    Dim Leave(500, 2) As Double
    Dim Lcnt As Integer = 0
    Sub locationtype()
        Dim sql As String

        sql = "Select office_type_id as office_type_id, office_type_name as office_type_name" & _
        " from office_type where h_level"
        If (Session("HRights") = "Y") Then
            sql &= ">"
        End If
        sql &= "=(select h_level from office_type " & _
        " where office_type_id=(select distinct office_type from user_table " & _
        " where office_code='" & Session("officecode") & "') ) order by h_level "


        Bind(sql, ddllocationtype, "--Select One--")
    End Sub
    Protected Sub ddllocationtype_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddllocationtype.SelectedIndexChanged
        Dim cmdrole As OleDbCommand
        Dim rdrrole As OleDbDataReader
        Dim sql As String


        Sql = "select Office_code , upper(office_name)office_name from office_master where" & _
          " OFFICE_CODE= '" & Session("officecode") & "'"
        Try
            cmdrole = New OleDbCommand(Sql, con.Con)
            rdrrole = cmdrole.ExecuteReader()
            If rdrrole.Read() Then
                x = rdrrole.GetValue(0)
            End If
            rdrrole.Close()
        Catch exole As OleDbException
            lblError.Text += exole.Message & "<Br>"
        Catch ex As Exception
            lblError.Text += ex.Message & "<Br>"
        End Try
        If x = 1 And ddllocationtype.SelectedItem.Text = "Unit" Then
            unit11()
            lunit1.Style("display") = ""
        Else
            office1()
            lunit1.Style("display") = "none"
        End If
        employee()
        mltvTab.ActiveViewIndex = 0
    End Sub
    Sub unit11()
        If ddloffice.SelectedValue <> "0" Then
            ddloffice.SelectedValue = "0"
        End If
        sql = "select office_code, office_name from office_master where parent_office_code=1 order by office_name  "
        '   Response.Write(SQL)
        Bind(sql, ddlunit, "--All--")
    End Sub
    Protected Sub ddlunit_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlunit.SelectedIndexChanged
        employee()
        office2()
        mltvTab.ActiveViewIndex = 0
    End Sub
    Sub office1()
        SQL = "select Office_code , upper(office_name)office_name from office_master "
        If (Session("officecode") <> "0" Or Session("officecode") <> "") Then
            SQL = SQL & " where office_code in " & _
                       " (SELECT office_code FROM office_master " & _
                     " CONNECT BY PRIOR office_code = parent_office_code START" & _
                     " WITH OFFICE_CODE= '" & Session("officecode") & "')   "
        End If
        If ddllocationtype.SelectedValue <> "0" And ddllocationtype.SelectedValue <> "" Then
            sql = sql & " and office_type_id  = '" & ddllocationtype.SelectedValue & "'"
        ElseIf ddllocationtype.SelectedValue = "0" Then
            If (Session("HRights") = "Y") Then
                If Session("officecode") <> "0" Then
                    sql += " and office_code in (select office_code from office_master  Start With OFFICE_CODE='" & Session("officecode") & "' Connect By Prior OFFICE_CODE=PARENT_OFFICE_CODE )"
                Else
                    sql += " and office_code in (select office_code from office_master  Start With PARENT_OFFICE_CODE=0 Connect By Prior OFFICE_CODE=PARENT_OFFICE_CODE )"
                End If
            Else
                sql += " and OFFICE_CODE='" & Session("officecode") & "' "
            End If
        End If
        SQL = SQL & " order by initcap(office_name) "
        Bind(sql, ddloffice, "--Select One--")
    End Sub
    Sub office2()
        SQL = "select Office_code , upper(office_name)office_name from office_master "
        'If (Session("HRights") = "Y") Then
        '    If Session("officecode") <> "0" Then
        '        SQL += " Start With OFFICE_CODE='" & Session("officecode") & "' Connect By Prior OFFICE_CODE=PARENT_OFFICE_CODE "
        '    Else
        '        SQL += " Start With PARENT_OFFICE_CODE=0 Connect By Prior OFFICE_CODE=PARENT_OFFICE_CODE "
        '    End If
        'Else
        '    SQL += " Where OFFICE_CODE='" & Session("officecode") & "' "
        'End If
        If (Session("officecode") <> "0" Or Session("officecode") <> "") Then
            SQL = SQL & " where office_code in " & _
                       " (SELECT office_code FROM office_master " & _
                     " CONNECT BY PRIOR office_code = parent_office_code START" & _
                     " WITH OFFICE_CODE= '" & Session("officecode") & "')   "
        End If
        If ddlunit.SelectedValue <> "0" Then
            SQL = SQL & " and (parent_office_code = '" & ddlunit.SelectedValue & "' or office_code= '" & ddlunit.SelectedValue & "')"
        End If
        SQL = SQL & " order by initcap(office_name) "
        Bind(sql, ddloffice, "--Select One--")
    End Sub
    Sub office4()
        SQL = "select Office_code , office_name from office_master "
        If (Session("officecode") <> "0" Or Session("officecode") <> "") Then
            SQL = SQL & " where office_code in " & _
                       " (SELECT office_code FROM office_master " & _
                     " CONNECT BY PRIOR office_code = parent_office_code START" & _
                     " WITH OFFICE_CODE= '" & Session("officecode") & "')   "
        End If
        SQL = SQL & " order by initcap(office_name) "
        '   Response.Write(sql)
        'sql = "select OFFICE_CODE,OFFICE_NAME from OFFICE_MASTER"
        Bind(sql, ddloffice, "--Select One--")
    End Sub
    Sub dept()
        sql = "select dept_id,dept_name from department_master order by dept_name"
        Bind(sql, ddldept, "--All--")
    End Sub
    Protected Sub ddldept_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddldept.SelectedIndexChanged
        employee()
    End Sub
    Sub Bind(ByVal sql As String, ByVal ddl As DropDownList, ByVal InsertStr As String)
        Dim adapter As OleDbDataAdapter
        Dim ds As New DataSet
        Dim lst As New ListItem
        Try
            If sql <> "" Then
                adapter = New OleDbDataAdapter(sql, Con.Con)
                adapter.Fill(ds, "dataset1")
                ddl.DataSource = ds.Tables("dataset1").DefaultView
                ddl.DataBind()
            End If
            lst.Value = "0"
            lst.Text = InsertStr
            If InsertStr <> "" Then ddl.Items.Insert(0, lst)
            If ddl.Items.Count = 2 Then ddl.SelectedValue = ddl.Items(1).Value
            ds.Clear()
            ds = Nothing
            adapter = Nothing
        Catch exole As OleDbException
            Throw New Exception(exole.Message & "<Br/>")
        Catch ex As Exception
            Throw New Exception(ex.Message & "<Br/>")
        End Try
    End Sub
    Sub employee()
        Dim sql As String
        sql = "select m.employee_id,employee_name ||' - '|| m.employee_id as employee_name from employee_master m, employee_detail d, office_master o" & _
        " where m.company_id=d.company_id and m.employee_id= d.employee_id and o.office_code= d.office_code "
        If ddllocationtype.SelectedValue <> "0" And ddloffice.SelectedValue = "0" And ddllocationtype.SelectedValue <> "2" Then
            sql = sql & " and o.office_type_id='" & ddllocationtype.SelectedValue & "' "
        End If

        If ddllocationtype.SelectedValue <> "0" And ddloffice.SelectedValue <> "0" Then
            sql = sql & " and o.office_code='" & ddloffice.SelectedValue & "' "
        End If
        If ddllocationtype.SelectedValue = "2" Then
            If ddlunit.SelectedValue = "0" Or ddlunit.SelectedValue = "" Then
                sql = sql & " and d.office_code in (select o.office_code from office_master o where  o.office_type_id =" & ddllocationtype.SelectedValue & _
                " union select o2.office_code from office_master o2 where  o2.parent_office_code IN (SELECT" & _
                "  office_code FROM office_master CONNECT BY PRIOR office_code = parent_office_code START WITH office_code =" & Session("officecode") & "))"
            Else
                sql = sql & " and d.office_code in (select office_code from office_master where (parent_office_code='" & ddlunit.SelectedValue & "' or office_code='" & ddlunit.SelectedValue & "') )"
            End If
        End If
        If ddldept.SelectedValue <> "0" Then
            sql = sql & " And d.DEPARTMENT_CODE=" & ddldept.SelectedValue
        End If
        sql = sql & " and effective_date=(select max(effective_date) from employee_detail tmp where tmp.employee_id= d.employee_id and tmp.company_id=d.company_id ) order by employee_id"
        ' Bind(sql, ddlemployee, "--All--")
    End Sub
    Protected Sub ddloffice_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddloffice.SelectedIndexChanged
        employee()
    End Sub
    Function ExistCount(ByVal strsql As String) As String
        '---- get count ----------
        Dim objCmd As New OleDbCommand
        Dim objReader As OleDbDataReader
        Dim Cnt As String
        Cnt = "0"
        Try
            objCmd.Connection = con.Con
            objCmd.CommandText = strsql
            objReader = objCmd.ExecuteReader
            While objReader.Read
                Cnt = objReader.GetValue(0).ToString
            End While
            objReader.Close()
            objCmd.Cancel()
            objReader = Nothing
            objCmd = Nothing
        Catch exole As OleDbException
            ' Response.Write(strsql)
            lblError.Text += exole.Message & "<Br>"
        Catch ex As Exception
            'Response.Write(strsql)
            lblError.Text += ex.Message & "<Br>"
        End Try
        '----------------------------------
        ExistCount = Cnt
    End Function
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnReset.Click
        Response.Redirect("salary_slip.aspx?pageid=" & PageId.Value)
    End Sub
    Sub No_Of_Daysinformation()
        If ddlMonth.SelectedValue = "1" Then
            dayInf = 31
        End If

        If ddlMonth.SelectedValue = "2" Then
            If CInt(ddlYear.SelectedValue) Mod 4 = 0 Then
                dayInf = 29
            Else
                dayInf = 28
            End If
        End If

        If ddlMonth.SelectedValue = "3" Then
            dayInf = 31
        End If

        If ddlMonth.SelectedValue = "4" Then
            dayInf = 30
        End If

        If ddlMonth.SelectedValue = "5" Then
            dayInf = 31
        End If

        If ddlMonth.SelectedValue = "6" Then
            dayInf = 30
        End If

        If ddlMonth.SelectedValue = "7" Then
            dayInf = 31
        End If

        If ddlMonth.SelectedValue = "8" Then
            dayInf = 31
        End If

        If ddlMonth.SelectedValue = "9" Then
            dayInf = 30
        End If

        If ddlMonth.SelectedValue = "10" Then
            dayInf = 31
        End If

        If ddlMonth.SelectedValue = "11" Then
            dayInf = 30
        End If

        If ddlMonth.SelectedValue = "12" Then
            dayInf = 31
        End If
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        con.Disconnect()
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("UserId").ToString() = "" Or Session("UserId").ToString() = "-1" Then Response.Redirect("login.aspx")
        Try
            Con.Connect()
            PageId.Value = Request("PageId")
            If PageId.Value = "" Then
                PageId.Value = "134"
            End If
            If Not IsPostBack Then

                Dim monthstr As String = ""
                If CInt(Now.Month) < CInt("10") Then
                    monthstr = "0" & Now.Month

                    If Now.Month <> "9" Then
                        monthstr = "0" & (Now.Month + 1)
                    Else
                        monthstr = "10"
                    End If

                Else
                    monthstr = Now.Month
                End If

                ddlMonth.SelectedValue = monthstr
                ddlYear.Items.Clear()

                Dim i As Integer
                Dim cmdyr As OleDbCommand
                Dim sql As String = ""
                sql = "select min(salary_year) from salary where company_id=" & Session("CompanyId")
                cmdyr = New OleDbCommand(sql, Con.Con)
                Try
                    For i = cmdyr.ExecuteScalar() To Now.Year
                        ddlYear.Items.Add(CStr(i))
                    Next
                Catch ex As Exception
                    lblError.Text = ex.Message & "<br/>"
                Finally
                    cmdyr.Dispose()
                End Try
                ddlYear.SelectedValue = Now.Year
                divcriteria.Style("display") = ""
                divSlip.Style("display") = "none"

                Dim cmdrole As OleDbCommand
                Dim rdrrole As OleDbDataReader
                sql = "select Office_code , upper(office_name)office_name from office_master where" & _
                           " OFFICE_CODE= '" & Session("officecode") & "'"
                Try
                    cmdrole = New OleDbCommand(sql, Con.Con)
                    rdrrole = cmdrole.ExecuteReader()
                    If rdrrole.Read() Then
                        x = rdrrole.GetValue(0)
                        If x = 1 Then
                            lunit1.Style("display") = "none"
                            loc.Style("display") = ""
                        Else
                            lunit1.Style("display") = "none"
                            loc.Style("display") = ""
                        End If
                    End If
                    rdrrole.Close()
                Catch exole As OleDbException
                    lblError.Text += exole.Message & "<Br>"
                Catch ex As Exception
                    lblError.Text += ex.Message & "<Br>"
                End Try

                Dim cmdrole1 As OleDbCommand
                Dim rdrrole1 As OleDbDataReader
                sql = "select Office_code , upper(office_name)office_name, office_type_id from office_master where" & _
                   " OFFICE_CODE= '" & Session("officecode") & "'"
                Try
                    cmdrole1 = New OleDbCommand(sql, Con.Con)
                    rdrrole1 = cmdrole1.ExecuteReader()
                    If rdrrole1.Read() Then

                        x2 = rdrrole1.GetValue(2)
                        If x2 = 5 Then
                            ltype.Style("display") = "none"
                            lunit1.Style("display") = "none"
                            office4()
                        Else
                            ltype.Style("display") = ""
                            ' lunit1.visible = True
                        End If
                    End If
                Catch exole As OleDbException
                    lblError.Text += exole.Message & "<Br>"
                Catch ex As Exception
                    lblError.Text += ex.Message & "<Br>"
                End Try
                locationtype()
                office1()
                dept()
                employee()
            Else
                'hfdoc.Value = "Reports/" & filename
                ' ''printheader(ddloffice.SelectedValue, ddldept.SelectedValue, rdr.GetValue(0).ToString, ddlMonth.SelectedValue, ddlYear.SelectedValue)
                'detailsalarysheet()
            End If
        Catch ex As Exception
            lblError.Text = ex.Message & "<Br/>"
        End Try
    End Sub
    Protected Sub btnView_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnView.Click
        If ddllocationtype.SelectedValue = 0 Then
            lblError.Text = "Select Location Type First"
            Exit Sub
        Else
        End If
        If ddllocationtype.SelectedValue <> "0" And ddllocationtype.SelectedValue = "2" And ddlunit.SelectedValue = "0" And Session("officecode") = 1 Then
            lblError.Text = "Select Unit First"
            Exit Sub
        Else
        End If

        If ddloffice.SelectedValue = "0" Or ddloffice.SelectedValue = "" Then
            lblError.Text = "Select Office First"
            Exit Sub
        Else
        End If
        mltvTab.ActiveViewIndex = 1
        employee_count()   
    End Sub

    

    Protected Sub showEarnings(ByVal ofc As String, ByVal emp As String, ByVal fromYear As String, ByVal FromMon As String)
        RwCnt = 0
        Dim finalrow_1 As New TableRow
        'Dim totearning As Double = 0.0
        totearning = 0
        Dim I As Integer
        Dim strSql, strWhere, strGroup As String
        Dim objCmd As New OleDbCommand()
        Dim fromDate As String
        Call No_Of_Daysinformation()
        fromDate = "1-" & MonthName(FromMon, True) & "-" & fromYear
        strSql = "select initcap(Head_Name) headName , (Amount+arrear)"
        strWhere = " and A.salary_month =" & FromMon & _
            " And A.salary_year =" & fromYear
        strWhere = strWhere & " And a.office_code='" & ofc & " '"
        If ddldept.SelectedValue <> "0" Then
            strWhere = strWhere & " And b.DEPARTMENT_CODE=" & ddldept.SelectedValue
        End If
        strGroup = ""
        strSql = strSql & " from salary A,Earning_Deduction_Head C, " & _
            " Employee_Detail B where A.company_id=" & Session("CompanyID")
        If emp <> "0" Then
            strWhere = strWhere & "  And a.Employee_ID='" & emp & " '"
        End If
        strSql = strSql & strWhere
        strSql = strSql & " And (Amount+ARREAR) <> 0 And a.Head_ID = c.Head_ID " & _
            " and a.company_id=c.company_id and ( c.Head_Type='E' or c.Head_Type='A')  and a.employee_id=b.employee_id " & _
            " and a.company_id=b.Company_ID  " & _
            " And to_Char(B.Effective_date,'DD-Mon-YYYY') in (" & _
            " select To_Char(max(Effective_date),'DD-Mon-YYYY')" & _
            " from Employee_Detail T Where B.Employee_ID=T.Employee_ID and " & _
            " B.Company_ID= T.Company_ID AND T.Company_ID=" & Session("CompanyID") & " AND to_char(t.effective_date,'YYYYMM')<='" & fromYear & fromMon & "')"

        strSql &= strGroup & " order by order_of_listing"
        'Response.Write(strSql)
        'Response.End()

        'Dim dsE As Data.DataSet
        dsE = New Data.DataSet
        daE = New OleDbDataAdapter(strSql, Con.Con)
        daE.Fill(dsE, "Earnings")
        '    ' Response.End()

    End Sub
    'Protected Sub showCompany(ByVal ofc As String, ByVal emp As String, ByVal fromYear As String, ByVal fromMon As String)
    '    Dim I As Integer
    '    totcompany = 0
    '    ' Dim totcompany As Double = 0.0
    '    Dim strSql, strWhere, strGroup As String
    '    Dim objCmd As New OleDbCommand()
    '    Dim fromDate As String
    '    Call No_Of_Daysinformation()
    '    fromDate = "1-" & MonthName(fromMon, True) & "-" & fromYear
    '    strSql = "select initcap(Head_Name) headName , (Amount+Arrear)"
    '    strWhere = " and A.salary_month =" & fromMon & _
    '               " And A.salary_year =" & fromYear
    '    strWhere = strWhere & " And a.office_code='" & ofc & "'"
    '    strGroup = ""
    '    strSql = strSql & " from salary A,Earning_Deduction_Head C, " & _
    '        " Employee_Detail B where A.company_id=" & Session("CompanyID")
    '    If emp <> "0" Then
    '        strWhere = strWhere & "  And a.Employee_ID='" & emp & "'"
    '    End If

    '    If ddldept.SelectedValue <> "0" Then
    '        strWhere = strWhere & " And b.DEPARTMENT_CODE=" & ddldept.SelectedValue
    '    End If

    '    strSql = strSql & strWhere


    '    strSql = strSql & " And (Amount+ARREAR) <> 0 And a.Head_ID = c.Head_ID " & _
    '        " and a.company_id=c.company_id and c.Head_Type='I' and a.employee_id=b.employee_id " & _
    '        " and a.company_id=b.Company_ID  " & _
    '        " And to_Char(B.Effective_date,'DD-Mon-YYYY') in (" & _
    '        " select To_Char(max(Effective_date),'DD-Mon-YYYY')" & _
    '        " from Employee_Detail T Where B.Employee_ID=T.Employee_ID and " & _
    '        " B.Company_ID= T.Company_ID AND T.Company_ID=" & Session("CompanyID") & ")"

    '    strSql &= strGroup & " order by order_of_listing"
    '    Dim tab As Table = New Table()
    '    tab.Style("border-color") = "#d3d3d3"
    '    tab.BorderWidth = 2
    '    tab.BorderStyle = BorderStyle.Ridge
    '    Dim r As TableRow = New TableRow()
    '    r.CssClass = "headStyle"
    '    Dim tc As TableHeaderCell = New TableHeaderCell()
    '    tab.ID = "tblCompany"
    '    tab.Width = Unit.Percentage(100)
    '    tab.CellSpacing = 0
    '    tab.CellPadding = 1
    '    tc.HorizontalAlign = HorizontalAlign.Left
    '    tc.ColumnSpan = 3
    '    tc.Text = "Company Contribution"
    '    tc.Font.Underline = "true"
    '    tc.Style.Add("horizontalAlign", "Left")
    '    r.Cells.Add(tc)
    '    tab.Rows.Add(r)
    '    Dim r1 As TableRow = New TableRow()
    '    Dim tcL As TableCell = New TableCell
    '    tcL.Width = 300
    '    tcL.HorizontalAlign = HorizontalAlign.Left
    '    tcL.VerticalAlign = VerticalAlign.Top
    '    Dim tblLeft As Table = New Table
    '    tblLeft.CellSpacing = 0
    '    tblLeft.Width = Unit.Percentage(100)
    '    tcL.Controls.Add(tblLeft)
    '    r1.Cells.Add(tcL)
    '    Dim tcM As TableCell = New TableCell
    '    tcM.Width = Unit.Pixel(40)
    '    tcM.VerticalAlign = VerticalAlign.Top
    '    r1.Cells.Add(tcM)
    '    Dim tcR As TableCell = New TableCell
    '    tcR.HorizontalAlign = HorizontalAlign.Left
    '    tcR.Width = Unit.Pixel(300)
    '    tcR.VerticalAlign = VerticalAlign.Top
    '    Dim tblRight As Table = New Table
    '    tblRight.CellSpacing = 0
    '    tblRight.Width = Unit.Percentage(100)
    '    tcR.Controls.Add(tblRight)
    '    r1.Cells.Add(tcR)
    '    objCmd.CommandText = strSql
    '    objCmd.Connection = Con.Con
    '    Dim objReader As OleDbDataReader
    '    objReader = objCmd.ExecuteReader
    '    I = 1
    '    lncnt += 1
    '    While (objReader.Read)
    '        'make table rows n cells accordingly
    '        Dim rr As New TableRow()
    '        rr.HorizontalAlign = HorizontalAlign.Left
    '        rr.Width = Unit.Pixel(300)
    '        Dim cL As New TableCell()
    '        cL.HorizontalAlign = HorizontalAlign.Left
    '        cL.VerticalAlign = VerticalAlign.Top
    '        cL.Width = Unit.Pixel(150)
    '        cL.Text = objReader.GetValue(0).ToString
    '        cL.Font.Bold = True
    '        rr.Cells.Add(cL)
    '        Dim cR As New TableCell()
    '        cR.HorizontalAlign = HorizontalAlign.Right
    '        cR.VerticalAlign = VerticalAlign.Top
    '        cR.Width = Unit.Pixel(150)
    '        cR.Text = Format(objReader.GetValue(1), "###0.00").ToString
    '        rr.Cells.Add(cR)
    '        totcompany = totcompany + CDbl(objReader.GetValue(1).ToString)
    '        If I Mod 2 = 0 Then
    '            tblRight.Rows.Add(rr)
    '        Else
    '            tblLeft.Rows.Add(rr)
    '        End If
    '        I = I + 1
    '        lncnt += 1
    '    End While
    '    objReader.Close()
    '    tab.Rows.Add(r1)
    '    Dim rTot As New TableRow()
    '    rTot.BackColor = Drawing.Color.LightGray
    '    Dim tcLTot As TableCell = New TableCell
    '    tcLTot.HorizontalAlign = HorizontalAlign.Left
    '    tcLTot.VerticalAlign = VerticalAlign.Top
    '    tcLTot.Text = "Total Company Contribution"
    '    rTot.Cells.Add(tcLTot)
    '    Dim tcMTot As TableCell = New TableCell
    '    tcMTot.Width = Unit.Pixel(40)
    '    tcMTot.VerticalAlign = VerticalAlign.Top
    '    rTot.Cells.Add(tcMTot)
    '    Dim tcRTot As TableCell = New TableCell
    '    tcRTot.HorizontalAlign = HorizontalAlign.Right
    '    tcRTot.VerticalAlign = VerticalAlign.Top
    '    tcRTot.Text = CStr(Format(totcompany, "###0.00"))
    '    rTot.Cells.Add(tcRTot)
    '    rTot.Font.Bold = True
    '    tab.Rows.Add(rTot)

    '    pnlCompany.Controls.Add(tab)
    '    If totcompany = 0 Then
    '        pnlCompany.Visible = False
    '    Else
    '        pnlCompany.Visible = True
    '    End If
    '    lncnt += 1

    'End Sub
    Protected Sub showCTC(ByVal ofc As String, ByVal emp As String, ByVal fromYear As String, ByVal fromMon As String)
        Dim tab As Table = New Table()
        tab.Style("border-color") = "#d3d3d3"
        tab.BorderWidth = 2
        tab.BorderStyle = BorderStyle.Ridge
        Dim r As TableRow = New TableRow()
        r.CssClass = "headStyle"
        Dim tc As TableHeaderCell = New TableHeaderCell()
        tab.ID = "tblCTC"
        tab.Width = Unit.Percentage(100)
        tab.CellSpacing = 0
        tab.CellPadding = 1
        tc.HorizontalAlign = HorizontalAlign.Left
        tc.ColumnSpan = 3
        tc.Text = "CTC"
        tc.Style.Add("horizontalAlign", "Left")
        r.Cells.Add(tc)
        tab.Rows.Add(r)
        Dim rTot As New TableRow()
        'rTot.BackColor = Drawing.Color.LightGray
        Dim tcLTot As TableCell = New TableCell
        tcLTot.HorizontalAlign = HorizontalAlign.Left
        tcLTot.VerticalAlign = VerticalAlign.Top
        tcLTot.Text = "CTC (Per Month)"
        rTot.Cells.Add(tcLTot)
        Dim tcMTot As TableCell = New TableCell
        tcMTot.Width = Unit.Pixel(40)
        tcMTot.VerticalAlign = VerticalAlign.Top
        rTot.Cells.Add(tcMTot)
        Dim tcRTot As TableCell = New TableCell
        tcRTot.HorizontalAlign = HorizontalAlign.Right
        tcRTot.VerticalAlign = VerticalAlign.Top
        tcRTot.Text = CStr(Format(totearning + totcompany, "###0.00"))
        rTot.Cells.Add(tcRTot)
        rTot.Font.Bold = True
        tab.Rows.Add(rTot)
        Dim rTot2 As New TableRow()
        'rTot.BackColor = Drawing.Color.LightGray
        Dim tcLTot2 As TableCell = New TableCell
        tcLTot2.HorizontalAlign = HorizontalAlign.Left
        tcLTot2.VerticalAlign = VerticalAlign.Top
        tcLTot2.Text = "CTC (Per Annum)"
        rTot2.Cells.Add(tcLTot2)
        Dim tcMTot2 As TableCell = New TableCell
        tcMTot2.Width = Unit.Pixel(40)
        tcMTot2.VerticalAlign = VerticalAlign.Top
        rTot2.Cells.Add(tcMTot2)
        Dim tcRTot2 As TableCell = New TableCell
        tcRTot2.HorizontalAlign = HorizontalAlign.Right
        tcRTot2.VerticalAlign = VerticalAlign.Top
        tcRTot2.Text = CStr(Format((totearning + totcompany) * 12, "###0.00"))
        rTot2.Cells.Add(tcRTot2)
        rTot2.Font.Bold = True
        tab.Rows.Add(rTot2)

        pnlCTC.Controls.Add(tab)
        If totearning + totcompany = 0 Then
            pnlCTC.Visible = False
        Else
            pnlCTC.Visible = True
        End If
    End Sub
    Protected Sub showpersonal(ByVal ofc As String, ByVal emp As String, ByVal fromYear As String, ByVal fromMon As String)
        Dim finalrow_1 As New TableRow
        Dim I As Integer
        Dim strSql, strWhere, strGroup As String
        Dim objCmd As New OleDbCommand()
        Dim fromDate As String
        'No_Of_Daysinformation()
        ' fromDate = "1-" & MonthName(fromMon, True) & "-" & YYYY
        fromDate = "1-" & MonthName(fromMon, True) & "-" & fromYear

        '  Response.Write("monrth=" & fromMon)
        '  Response.End()
        '   Dim strsql, strWhere As String
        strSql = "Select distinct a.employee_id || '  /  ' || a.employee_name || '  /   ' || a.emp_father_name ||' / '||internal_token_id fname, " & _
                " o.office_code , o.office_name,  DECODE (d.dept_name," & _
                " 'N/A', e.designation_name, " & _
                " e.designation_name || '  /  ' || dept_name " & _
                " ) designame, upper(bank_name) || ' / ' || NVL (bb.bank_branch_name, 'NIL') || ' / ' || bank_account_no  || ' / ' || " & _
                " pp.pay_scale_no || '-' || B1 || '-' || I1 bank ,a.employee_id " & _
                " From Employee_master A, Employee_Detail B  ," & _
                " department_master D ,tblparams F,Salary H,office_master o,designation e, hr_bank g,  hr_bank_branch bb,pay_scale pp " & _
                " where A.company_ID= B.Company_ID and A.Employee_ID= B.Employee_ID" & _
                " AND A.Company_ID=" & Session("CompanyID") & _
                " and o.office_code=b.office_code" & _
                " And g.bank_id(+)=b.bank_id " & _
                "   and b.pay_sacle_code=pp.pay_scale_code(+) " & _
                "  AND b.bank_branch_code = bb.bank_branch_id(+)" & _
                " AND b.department_code = d.dept_id " & _
                " AND b.designation_code = e.designation_code" & _
                " And To_Char(B.Effective_date,'DD-Mon-YYYY') in ( select To_Char(max(Effective_date),'DD-Mon-YYYY')" & _
                " from Employee_Detail T Where B.Employee_ID=T.Employee_ID and B.Company_ID= T.Company_ID " & _
                " AND T.Company_ID=" & Session("CompanyID") & " AND to_char(t.effective_date,'YYYYMM')<='" & fromYear & fromMon & "' )"
        'AND to_char(t.effective_date,'MMYYYY')<='" & fromMon & fromYear & "'
        strSql &= " AND B.department_code=D.dept_id    " & _
        " and f.ref_id(+)= b.emp_category_code" & _
            " And B.Employee_ID=H.Employee_ID and B.company_id=H.company_id " & _
        " and o.office_code=h.office_code "
        strSql = strSql & " And H.Salary_Month = " & fromMon 'toMon
        strSql = strSql & " And H.Salary_Year =" & fromYear

        If ddllocationtype.SelectedValue <> "0" And ddloffice.SelectedValue = "0" Then
            strSql = strSql & " And o.office_type_id =" & ddllocationtype.SelectedValue
        End If

        If ddllocationtype.SelectedValue <> "0" And ofc <> "0" Then
            strSql = strSql & " And b.office_code='" & ofc & "'"
        End If

        If ddldept.SelectedValue <> "0" Then
            strSql = strSql & " And b.DEPARTMENT_CODE=" & ddldept.SelectedValue
        End If

        'If emp <> "0" Then
        '    strSql = strSql & " And a.Employee_ID='" & emp & "'"
        'End If
        strSql = strSql & " order by a.employee_id"

        'Response.Write(strSql)
        '  Response.End()
        dsP = New Data.DataSet
        daP = New OleDbDataAdapter(strSql, Con.Con)
        daP.Fill(dsP, "Personal")
    End Sub
    Protected Sub showDeductions(ByVal ofc As String, ByVal emp As String, ByVal fromYear As String, ByVal fromMon As String)
        RwCnt2 = 0
        Dim I As Integer
        totdeduction = 0
        '  Dim totdeduction As Double = 0.0
        Dim strSql, strWhere, strGroup As String
        Dim objCmd As New OleDbCommand()
        Dim fromDate As String
        Call No_Of_Daysinformation()
        'employee_count()

        fromDate = "1-" & MonthName(fromMon, True) & "-" & fromYear
        strSql = "select initcap(Head_Name) headName , (Amount+Arrear)"
        strWhere = " and A.salary_month =" & fromMon & _
                   " And A.salary_year =" & fromYear
        strWhere = strWhere & " And a.office_code='" & ofc & "'"
        strGroup = ""
        strSql = strSql & " from salary A,Earning_Deduction_Head C, " & _
            " Employee_Detail B where A.company_id=" & Session("CompanyID")
        If emp <> "0" Then
            strWhere = strWhere & "  And a.Employee_ID='" & emp & "'"
        End If

        If ddldept.SelectedValue <> "0" Then
            strWhere = strWhere & " And b.DEPARTMENT_CODE=" & ddldept.SelectedValue
        End If

        strSql = strSql & strWhere
        strSql = strSql & " and  " & _
            "  (Amount+ARREAR) <> 0 And a.Head_ID = c.Head_ID " & _
            " and a.company_id=c.company_id and ( c.Head_Type='D' or c.Head_Type='L'or c.Head_Type='R') and a.employee_id=b.employee_id " & _
            " and a.company_id=b.Company_ID  " & _
            " And to_Char(B.Effective_date,'DD-Mon-YYYY') in (" & _
            " select To_Char(max(Effective_date),'DD-Mon-YYYY')" & _
            " from Employee_Detail T Where B.Employee_ID=T.Employee_ID and " & _
            " B.Company_ID= T.Company_ID AND T.Company_ID=" & Session("CompanyID") & " AND to_char(t.effective_date,'YYYYMM')<='" & fromYear & fromMon & "' )"

        strSql = strSql & strWhere
        strSql &= strGroup & " order by order_of_listing"

        dsD = New Data.DataSet
        daD = New OleDbDataAdapter(strSql, Con.Con)
        daD.Fill(dsD, "Deduction")

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '' Response.Write(strSql & "</br>" & "</br>")
        ''  Response.End()
        'Dim tab As Table = New Table()
        'tab.Style("border-color") = "#d3d3d3"
        'tab.BorderWidth = 2
        'tab.BorderStyle = BorderStyle.Ridge
        'Dim r As TableRow = New TableRow()
        'r.CssClass = "headStyle"
        'Dim tc As TableHeaderCell = New TableHeaderCell()
        'tab.ID = "tblDeduction"
        'tab.Width = Unit.Percentage(50%)
        'tab.CellSpacing = 0
        'tab.CellPadding = 0
        'tc.HorizontalAlign = HorizontalAlign.Left
        'tc.ColumnSpan = 2
        'tc.Text = "Deductions"
        'tc.Font.Size = "10"
        'tc.Font.Underline = "true"
        'tc.Style.Add("horizontalAlign", "Left")
        'r.Cells.Add(tc)
        'tab.Rows.Add(r)

        'Dim r1 As TableRow = New TableRow()
        'Dim tcL As TableCell = New TableCell
        'tcL.Width = Unit.Pixel(100)

        'tcL.HorizontalAlign = HorizontalAlign.Left
        'tcL.VerticalAlign = VerticalAlign.Top
        'tcL.Controls.Add(tab)
        'r1.Cells.Add(tcL)

        'objCmd.CommandText = strSql
        'objCmd.Connection = Con.Con
        'Dim objReader As OleDbDataReader
        'objReader = objCmd.ExecuteReader
        'I = 1
        'lncnt += 1

        'While (objReader.Read)
        '    Dim rr As New TableRow()
        '    rr.Width = Unit.Pixel(100)
        '    rr.Font.Size = "9"
        '    Dim cL As New TableCell()
        '    cL.VerticalAlign = VerticalAlign.Top
        '    cL.Width = Unit.Pixel(100)
        '    cL.Font.Size = "9"
        '    cL.Text = objReader.GetValue(0).ToString
        '    cL.Font.Bold = True
        '    rr.Cells.Add(cL)
        '    Dim cR As New TableCell()
        '    cR.VerticalAlign = VerticalAlign.Top
        '    cR.Width = Unit.Pixel(100)
        '    cR.Text = Format(objReader.GetValue(1), "###0.00").ToString
        '    cR.Font.Size = "9"
        '    rr.Cells.Add(cR)
        '    totdeduction = totdeduction + CDbl(objReader.GetValue(1).ToString)
        '   ' Response.Write("I=" & I & " " & I Mod 2 & "<br>")

        '    tab.Rows.Add(rr)

        '    I = I + 1
        '    lncnt += 1
        '    RwCnt2 += 1
        'End While
        'objReader.Close()
        'If RwCnt > RwCnt2 + 1 Then
        '    Dim diff As Integer
        '    diff = RwCnt - (RwCnt2 + 1)
        '    Response.Write("diff=" & diff)
        '    While (diff > 0)

        '        Dim rTot2 As New TableRow()
        '        'rTot2.BackColor = Drawing.Color.LightGray
        '        Dim tcLTot2 As TableCell = New TableCell
        '        tcLTot2.Text = ""
        '        tcLTot2.Font.Size = "9"
        '        rTot2.Cells.Add(tcLTot2)

        '        Dim tcRTot2 As TableCell = New TableCell
        '        tcRTot2.Text = "&nbsp;" 'CStr(Format(totdeduction, "###0.00"))
        '        tcRTot2.Font.Size = "9"
        '        rTot2.Cells.Add(tcRTot2)
        '        rTot2.Font.Bold = True
        '        tab.Rows.Add(rTot2)
        '        diff = diff - 1
        '    End While
        'End If
        '' tab.Rows.Add(r1)
        'Dim rTot As New TableRow()
        'rTot.BackColor = Drawing.Color.LightGray
        'Dim tcLTot As TableCell = New TableCell
        'tcLTot.Text = "Total Deduction"
        'tcLTot.Font.Size = "9"
        'rTot.Cells.Add(tcLTot)


        'Dim tcRTot As TableCell = New TableCell
        'tcRTot.Text = CStr(Format(totdeduction, "###0.00"))
        'tcRTot.Font.Size = "9"
        'rTot.Cells.Add(tcRTot)
        'rTot.Font.Bold = True
        'tab.Rows.Add(rTot)
        'pnlCTC.Controls.Add(tab)
        'lncnt += 1

        ''If totdeduction = 0 Then
        ''    pnlCTC.Visible = False
        ''Else
        ''    pnlCTC.Visible = True
        ''End If
        'pnlCTC.Controls.Add(tab)
        ''Response.Write("</br>")
    End Sub

    Protected Sub show()

        ' Response.Write(RwCnt & "</br>" & "</br>")
        ' Response.Write(RwCnt2 & "</br>" & "</br>")

        If RwCnt = RwCnt2 Then
            Dim rTot2 As New TableRow()
            'rTot2.BackColor = Drawing.Color.LightGray
            Dim tcLTot2 As TableCell = New TableCell
            tcLTot2.Text = ""
            tcLTot2.Font.Size = "9"
            rTot2.Cells.Add(tcLTot2)

            Dim tcRTot2 As TableCell = New TableCell
            tcRTot2.Text = "&nbsp;" 'CStr(Format(totdeduction, "###0.00"))
            tcRTot2.Font.Size = "9"
            rTot2.Cells.Add(tcRTot2)
            rTot2.Font.Bold = True
            tab.Rows.Add(rTot2)
        End If

    End Sub

    Protected Sub showLoans(ByVal ofc As String, ByVal emp As String, ByVal fromYear As String, ByVal fromMon As String)
        Dim I As Integer
        totLoans = 0
        ' Dim totLoans As Double = 0.0
        Dim strSql, strWhere, strGroup As String
        Dim objCmd As New OleDbCommand()
        Dim fromDate As String
        Call No_Of_Daysinformation()
        fromDate = "1-" & MonthName(fromMon, True) & "-" & fromYear
        strSql = "select initcap(Head_Name) headName , (Amount+Arrear)"
        strWhere = " and A.salary_month =" & fromMon & _
                   " And A.salary_year =" & fromYear
        strWhere = strWhere & " And a.office_code='" & ofc & " '"
        strGroup = ""
        strSql = strSql & " from salary A,Earning_Deduction_Head C, " & _
            " Employee_Detail B where A.company_id=" & Session("CompanyID")

        If emp <> "0" Then
            strWhere = strWhere & "  And a.Employee_ID='" & emp & "'"
        End If

        If ddldept.SelectedValue <> "0" Then
            strWhere = strWhere & " And b.DEPARTMENT_CODE=" & ddldept.SelectedValue
        End If

        strSql = strSql & strWhere

        strSql = strSql & " And (Amount+ARREAR) <> 0 And a.Head_ID = c.Head_ID " & _
            " and a.company_id=c.company_id and c.Head_Type='L' and a.employee_id=b.employee_id " & _
            " and a.company_id=b.Company_ID  " & _
            " And to_Char(B.Effective_date,'DD-Mon-YYYY') in (" & _
            " select To_Char(max(Effective_date),'DD-Mon-YYYY')" & _
            " from Employee_Detail T Where B.Employee_ID=T.Employee_ID and " & _
            " B.Company_ID= T.Company_ID AND T.Company_ID=" & Session("CompanyID") & ")"

        strSql &= strGroup & " order by order_of_listing"
        'Response.Write(strSql & "</br>" & "</br>")
        Dim tab As Table = New Table()
        tab.Style("border-color") = "#d3d3d3"
        tab.BorderWidth = 2
        tab.BorderStyle = BorderStyle.Ridge
        Dim r As TableRow = New TableRow()
        r.CssClass = "headStyle"
        Dim tc As TableHeaderCell = New TableHeaderCell()
        tab.ID = "tblLoan"
        tab.Width = Unit.Percentage(100)
        tab.CellSpacing = 0
        tab.CellPadding = 1
        tc.HorizontalAlign = HorizontalAlign.Left
        tc.ColumnSpan = 3
        tc.Text = "Loans"
        tc.Font.Size = "10"
        tc.Font.Underline = "true"
        tc.Style.Add("horizontalAlign", "Left")
        r.Cells.Add(tc)
        tab.Rows.Add(r)
        Dim r1 As TableRow = New TableRow()
        Dim tcL As TableCell = New TableCell
        tcL.Width = 300
        tcL.HorizontalAlign = HorizontalAlign.Left
        tcL.VerticalAlign = VerticalAlign.Top
        Dim tblLeft As Table = New Table
        tblLeft.CellSpacing = 0
        tblLeft.Width = Unit.Percentage(100)
        tcL.Controls.Add(tblLeft)
        r1.Cells.Add(tcL)
        Dim tcM As TableCell = New TableCell
        tcM.Width = Unit.Pixel(40)
        tcM.VerticalAlign = VerticalAlign.Top
        r1.Cells.Add(tcM)
        Dim tcR As TableCell = New TableCell
        tcR.HorizontalAlign = HorizontalAlign.Left
        tcR.Width = Unit.Pixel(300)
        tcR.VerticalAlign = VerticalAlign.Top
        Dim tblRight As Table = New Table
        tblRight.CellSpacing = 0
        tblRight.Width = Unit.Percentage(100)
        tcR.Controls.Add(tblRight)
        r1.Cells.Add(tcR)
        objCmd.CommandText = strSql
        objCmd.Connection = Con.Con
        Dim objReader As OleDbDataReader
        objReader = objCmd.ExecuteReader
        I = 1
        lncnt += 1
        While (objReader.Read)
            'make table rows n cells accordingly
            Dim rr As New TableRow()
            rr.HorizontalAlign = HorizontalAlign.Left
            rr.Width = Unit.Pixel(300)
            rr.Font.Size = "9"
            Dim cL As New TableCell()
            cL.HorizontalAlign = HorizontalAlign.Left
            cL.VerticalAlign = VerticalAlign.Top
            cL.Width = Unit.Pixel(150)
            cL.Font.Size = "9"
            cL.Text = objReader.GetValue(0).ToString
            cL.Font.Bold = True
            rr.Cells.Add(cL)
            Dim cR As New TableCell()
            cR.HorizontalAlign = HorizontalAlign.Right
            cR.VerticalAlign = VerticalAlign.Top
            cR.Width = Unit.Pixel(150)
            cR.Text = Format(objReader.GetValue(1), "###0.00").ToString
            cR.Font.Size = "9"
            rr.Cells.Add(cR)
            totLoans = totLoans + CDbl(objReader.GetValue(1).ToString)
            If I Mod 2 = 0 Then
                tblRight.Rows.Add(rr)
            Else
                tblLeft.Rows.Add(rr)
            End If
            I = I + 1
            lncnt += 1
        End While
        objReader.Close()
        If totLoans <> 0 Then

            tab.Rows.Add(r1)
            Dim rTot As New TableRow()
            rTot.BackColor = Drawing.Color.LightGray
            Dim tcLTot As TableCell = New TableCell
            tcLTot.HorizontalAlign = HorizontalAlign.Left
            tcLTot.VerticalAlign = VerticalAlign.Top
            tcLTot.Text = "Total Loans"
            tcLTot.Font.Size = "9"
            rTot.Cells.Add(tcLTot)
            Dim tcMTot As TableCell = New TableCell
            tcMTot.Width = Unit.Pixel(40)
            tcMTot.VerticalAlign = VerticalAlign.Top
            rTot.Cells.Add(tcMTot)
            Dim tcRTot As TableCell = New TableCell
            tcRTot.HorizontalAlign = HorizontalAlign.Right
            tcRTot.VerticalAlign = VerticalAlign.Top
            tcRTot.Text = CStr(Format(totLoans, "###0.00"))
            tcRTot.Font.Size = "9"
            rTot.Cells.Add(tcRTot)
            rTot.Font.Bold = True
            tab.Rows.Add(rTot)
            pnlCTC.Controls.Add(tab)
            lncnt += 1
        End If
        'If totLoans = 0 Then
        ' pnlCTC.Visible = False
        'Else
        'pnlCTC.Visible = True
        'End If
    End Sub
    Protected Sub showReductions(ByVal ofc As String, ByVal emp As String, ByVal fromYear As String, ByVal fromMon As String)
        Dim I As Integer
        totreductions = 0
        ' Dim totreductions As Double = 0.0
        Dim strSql, strWhere, strGroup As String
        Dim objCmd As New OleDbCommand()
        Dim fromDate As String
        Call No_Of_Daysinformation()
        fromDate = "1-" & MonthName(fromMon, True) & "-" & fromYear
        strSql = "select initcap(Head_Name) headName , (Amount+Arrear)"
        strWhere = " and A.salary_month =" & fromMon & _
                   " And A.salary_year =" & fromYear
        strWhere = strWhere & " And a.office_code='" & ofc & "'"
        strGroup = ""
        strSql = strSql & " from salary A,Earning_Deduction_Head C, " & _
            " Employee_Detail B where A.company_id=" & Session("CompanyID")
        If emp <> "0" Then
            strWhere = strWhere & "  And a.Employee_ID='" & emp & "'"
        End If

        If ddldept.SelectedValue <> "0" Then
            strWhere = strWhere & " And b.DEPARTMENT_CODE=" & ddldept.SelectedValue
        End If

        strSql = strSql & strWhere


        strSql = strSql & " And (Amount+ARREAR) <> 0 And a.Head_ID = c.Head_ID " & _
            " and a.company_id=c.company_id and c.Head_Type='R' and a.employee_id=b.employee_id " & _
            " and a.company_id=b.Company_ID  " & _
            " And to_Char(B.Effective_date,'DD-Mon-YYYY') in (" & _
            " select To_Char(max(Effective_date),'DD-Mon-YYYY')" & _
            " from Employee_Detail T Where B.Employee_ID=T.Employee_ID and " & _
            " B.Company_ID= T.Company_ID AND T.Company_ID=" & Session("CompanyID") & ")"
        'If fromMon = toMon And fromYear = toYear Then
        '    strSql &= " and effective_date<='" & toDate & "')"
        'Else
        '    strSql &= " and effective_date between '" & fromDate & "' and '" & toDate & "')"
        '    '  strSql &= " and effective_date<='" & toDate & "')"
        'End If

        strSql &= strGroup & " order by order_of_listing"

        Dim tab As Table = New Table()
        tab.Style("border-color") = "#d3d3d3"
        tab.BorderWidth = 2
        tab.BorderStyle = BorderStyle.Ridge
        Dim r As TableRow = New TableRow()
        r.CssClass = "headStyle"
        Dim tc As TableHeaderCell = New TableHeaderCell()
        tab.ID = "tblReduction"
        tab.Width = Unit.Percentage(100)
        tab.CellSpacing = 0
        tab.CellPadding = 1
        tc.HorizontalAlign = HorizontalAlign.Left
        tc.ColumnSpan = 3
        tc.Text = "Reductions"
        tc.Font.Size = "10"
        tc.Font.Underline = "true"
        tc.Style.Add("horizontalAlign", "Left")
        r.Cells.Add(tc)
        tab.Rows.Add(r)
        Dim r1 As TableRow = New TableRow()
        Dim tcL As TableCell = New TableCell
        tcL.Width = 300
        tcL.HorizontalAlign = HorizontalAlign.Left
        tcL.VerticalAlign = VerticalAlign.Top
        Dim tblLeft As Table = New Table
        tblLeft.CellSpacing = 0
        tblLeft.Width = Unit.Percentage(100)
        tcL.Controls.Add(tblLeft)
        r1.Cells.Add(tcL)
        Dim tcM As TableCell = New TableCell
        tcM.Width = Unit.Pixel(40)
        tcM.VerticalAlign = VerticalAlign.Top
        r1.Cells.Add(tcM)
        Dim tcR As TableCell = New TableCell
        tcR.HorizontalAlign = HorizontalAlign.Left
        tcR.Width = Unit.Pixel(300)
        tcR.VerticalAlign = VerticalAlign.Top
        Dim tblRight As Table = New Table
        tblRight.CellSpacing = 0
        tblRight.Width = Unit.Percentage(100)
        tcR.Controls.Add(tblRight)
        r1.Cells.Add(tcR)
        objCmd.CommandText = strSql
        objCmd.Connection = Con.Con
        Dim objReader As OleDbDataReader
        objReader = objCmd.ExecuteReader
        I = 1
        lncnt += 1
        While (objReader.Read)
            'make table rows n cells accordingly
            Dim rr As New TableRow()
            rr.HorizontalAlign = HorizontalAlign.Left
            rr.Width = Unit.Pixel(300)
            rr.Font.Size = "9"
            Dim cL As New TableCell()
            cL.HorizontalAlign = HorizontalAlign.Left
            cL.VerticalAlign = VerticalAlign.Top
            cL.Width = Unit.Pixel(150)
            rr.Font.Size = "9"
            cL.Text = objReader.GetValue(0).ToString
            cL.Font.Bold = True
            rr.Cells.Add(cL)
            Dim cR As New TableCell()
            cR.HorizontalAlign = HorizontalAlign.Right
            cR.VerticalAlign = VerticalAlign.Top
            cR.Width = Unit.Pixel(150)
            cR.Text = Format(objReader.GetValue(1), "###0.00").ToString
            cR.Font.Size = "9"
            rr.Cells.Add(cR)
            totreductions = totreductions + CDbl(objReader.GetValue(1).ToString)
            If I Mod 2 = 0 Then
                tblRight.Rows.Add(rr)
            Else
                tblLeft.Rows.Add(rr)
            End If
            I = I + 1
            lncnt += 1
        End While
        objReader.Close()
        If totreductions <> 0 Then
            tab.Rows.Add(r1)
            Dim rTot As New TableRow()
            rTot.BackColor = Drawing.Color.LightGray
            Dim tcLTot As TableCell = New TableCell
            tcLTot.HorizontalAlign = HorizontalAlign.Left
            tcLTot.VerticalAlign = VerticalAlign.Top
            tcLTot.Text = "Total Reductions"
            tcLTot.Font.Size = "9"
            rTot.Cells.Add(tcLTot)
            Dim tcMTot As TableCell = New TableCell
            tcMTot.Width = Unit.Pixel(40)
            tcMTot.VerticalAlign = VerticalAlign.Top
            rTot.Cells.Add(tcMTot)
            Dim tcRTot As TableCell = New TableCell
            tcRTot.HorizontalAlign = HorizontalAlign.Right
            tcRTot.VerticalAlign = VerticalAlign.Top
            tcRTot.Text = CStr(Format(totreductions, "###0.00"))
            tcRTot.Font.Size = "9"
            rTot.Cells.Add(tcRTot)
            rTot.Font.Bold = True
            tab.Rows.Add(rTot)
            pnlCTC.Controls.Add(tab)
            lncnt += 1
        End If
        'If totreductions = 0 Then
        'pnlCTC.Visible = False
        'Else
        'pnlCTC.Visible = True
        'End If
    End Sub
    Protected Sub showAdditions(ByVal ofc As String, ByVal emp As String, ByVal fromYear As String, ByVal fromMon As String)
        Dim I As Integer
        totadditions = 0
        '  Dim totadditions As Double = 0.0
        Dim strSql, strWhere, strGroup As String
        Dim objCmd As New OleDbCommand()
        Dim fromDate As String
        Call No_Of_Daysinformation()
        fromDate = "1-" & MonthName(fromMon, True) & "-" & fromYear
        strSql = "select initcap(Head_Name) headName , (Amount+Arrear)"
        strWhere = " and A.salary_month =" & fromMon & _
                   " And A.salary_year =" & fromYear
        strWhere = strWhere & " And a.office_code='" & ofc & "'"
        strGroup = ""
        strSql = strSql & " from salary A,Earning_Deduction_Head C, " & _
            " Employee_Detail B where A.company_id=" & Session("CompanyID")
        If emp <> "0" Then
            strWhere = strWhere & "  And a.Employee_ID='" & emp & "'"
        End If

        If ddldept.SelectedValue <> "0" Then
            strWhere = strWhere & " And b.DEPARTMENT_CODE=" & ddldept.SelectedValue
        End If

        strSql = strSql & strWhere

        strSql = strSql & " And (Amount+ARREAR) <> 0 And a.Head_ID = c.Head_ID " & _
            " and a.company_id=c.company_id and c.Head_Type='A' and a.employee_id=b.employee_id " & _
            " and a.company_id=b.Company_ID  " & _
            " And to_Char(B.Effective_date,'DD-Mon-YYYY') in (" & _
            " select To_Char(max(Effective_date),'DD-Mon-YYYY')" & _
            " from Employee_Detail T Where B.Employee_ID=T.Employee_ID and " & _
            " B.Company_ID= T.Company_ID AND T.Company_ID=" & Session("CompanyID") & ")"
        strSql &= strGroup & " order by order_of_listing"
        Dim tab As Table = New Table()
        tab.Style("border-color") = "#d3d3d3"
        tab.BorderWidth = 2
        tab.BorderStyle = BorderStyle.Ridge
        Dim r As TableRow = New TableRow()
        r.CssClass = "headStyle"
        Dim tc As TableHeaderCell = New TableHeaderCell()
        tab.ID = "tblAddition"
        tab.Width = Unit.Percentage(100)
        tab.CellSpacing = 0
        tab.CellPadding = 1
        tc.HorizontalAlign = HorizontalAlign.Left
        tc.ColumnSpan = 3
        tc.Text = "Additions"
        tc.Font.Size = "10"
        tc.Font.Underline = "true"
        tc.Style.Add("horizontalAlign", "Left")
        r.Cells.Add(tc)
        tab.Rows.Add(r)
        Dim r1 As TableRow = New TableRow()
        Dim tcL As TableCell = New TableCell
        tcL.Width = 300
        tcL.HorizontalAlign = HorizontalAlign.Left
        tcL.VerticalAlign = VerticalAlign.Top
        Dim tblLeft As Table = New Table
        tblLeft.CellSpacing = 0
        tblLeft.Width = Unit.Percentage(100)
        tcL.Controls.Add(tblLeft)
        r1.Cells.Add(tcL)
        Dim tcM As TableCell = New TableCell
        tcM.Width = Unit.Pixel(40)
        tcM.VerticalAlign = VerticalAlign.Top
        r1.Cells.Add(tcM)
        Dim tcR As TableCell = New TableCell
        tcR.HorizontalAlign = HorizontalAlign.Left
        tcR.Width = Unit.Pixel(300)
        tcR.VerticalAlign = VerticalAlign.Top
        Dim tblRight As Table = New Table
        tblRight.CellSpacing = 0
        tblRight.Width = Unit.Percentage(100)
        tcR.Controls.Add(tblRight)
        r1.Cells.Add(tcR)
        objCmd.CommandText = strSql
        objCmd.Connection = Con.Con
        Dim objReader As OleDbDataReader
        objReader = objCmd.ExecuteReader
        I = 1
        lncnt += 1
        While (objReader.Read)
            'make table rows n cells accordingly
            Dim rr As New TableRow()
            rr.HorizontalAlign = HorizontalAlign.Left
            rr.Width = Unit.Pixel(300)
            rr.Font.Size = "9"
            Dim cL As New TableCell()
            cL.HorizontalAlign = HorizontalAlign.Left
            cL.VerticalAlign = VerticalAlign.Top
            cL.Width = Unit.Pixel(150)
            cL.Font.Size = "9"
            cL.Text = objReader.GetValue(0).ToString
            cL.Font.Bold = True
            rr.Cells.Add(cL)
            Dim cR As New TableCell()
            cR.HorizontalAlign = HorizontalAlign.Right
            cR.VerticalAlign = VerticalAlign.Top
            cR.Width = Unit.Pixel(150)
            cR.Text = Format(objReader.GetValue(1), "###0.00").ToString
            cR.Font.Size = "9"
            rr.Cells.Add(cR)
            totadditions = totadditions + CDbl(objReader.GetValue(1).ToString)
            If I Mod 2 = 0 Then
                tblRight.Rows.Add(rr)
            Else
                tblLeft.Rows.Add(rr)
            End If
            I = I + 1
            lncnt += 1
        End While
        objReader.Close()
        If totadditions <> 0 Then
            tab.Rows.Add(r1)
            Dim rTot As New TableRow()
            rTot.BackColor = Drawing.Color.LightGray
            Dim tcLTot As TableCell = New TableCell
            tcLTot.HorizontalAlign = HorizontalAlign.Left
            tcLTot.VerticalAlign = VerticalAlign.Top
            tcLTot.Text = "Total Additions"
            tcLTot.Font.Size = "9"
            rTot.Cells.Add(tcLTot)
            Dim tcMTot As TableCell = New TableCell
            tcMTot.Width = Unit.Pixel(40)
            tcMTot.VerticalAlign = VerticalAlign.Top
            rTot.Cells.Add(tcMTot)
            Dim tcRTot As TableCell = New TableCell
            tcRTot.HorizontalAlign = HorizontalAlign.Right
            tcRTot.VerticalAlign = VerticalAlign.Top
            tcRTot.Text = CStr(Format(totadditions, "###0.00"))
            tcRTot.Font.Size = "9"
            rTot.Cells.Add(tcRTot)
            rTot.Font.Bold = True
            tab.Rows.Add(rTot)
            pnlCTC.Controls.Add(tab)
            lncnt += 1
        End If
        ' If totadditions = 0 Then
        'pnlCTC.Visible = False
        'Else
        'pnlCTC.Visible = True
        'End If
    End Sub
    Protected Sub pageBreak()
        Dim tab As Table = New Table()
        Dim r1 As TableRow = New TableRow()
        Dim tc As New TableCell()
        tc.Width = Unit.Percentage(100)
        tc.Text = "<hr style='page-break-after:always; color:white' />"

        r1.Cells.Add(tc)
        tab.Rows.Add(r1)
        pnlCTC.Controls.Add(tab)

    End Sub
    Protected Sub showpayable(ByVal ofc As String, ByVal emp As String, ByVal fromYear As String, ByVal fromMon As String)
        ' ''lblPayable.Text = CStr(Format(totearning - totdeduction - totLoans - totreductions + totadditions, "###0.00"))
        Dim payable As Integer
        payable = CStr(Format(totearning - totdeduction - totLoans - totreductions + totadditions, "###0.00"))

        Dim tab As Table = New Table()
        tab.Style("border-color") = "#d3d3d3"
        tab.BorderStyle = BorderStyle.Ridge
        Dim r As TableRow = New TableRow()
        r.BackColor = Drawing.Color.LightGray
        If (count Mod 2) = 0 Then
            'SSSS Response.Write("---" & count Mod 2)
            r.CssClass = "pagebreak"
        End If

        Dim tc As New TableCell()
        tc.HorizontalAlign = HorizontalAlign.Left

        tab.ID = "tblpayable"
        tab.BorderWidth = 2
        tab.Width = Unit.Percentage(100%)
        tab.CellSpacing = 0
        tab.CellPadding = 1
        tc.HorizontalAlign = HorizontalAlign.Left
        tc.Width = Unit.Percentage(20%)
        tc.VerticalAlign = VerticalAlign.Top
        tc.Text = "Net Payable"
        tc.Font.Size = "10"
        tc.Font.Underline = "true"
        tc.Font.Bold = "true"
        tc.Style.Add("horizontalAlign", "Left")
        tc.Style.Add("TEXT-ALIGN", "Left")

        r.Cells.Add(tc)
        '---------------------------------------------



        Dim c As New TableCell()
        Dim a As String

        c.HorizontalAlign = HorizontalAlign.Right
        c.VerticalAlign = VerticalAlign.Top
        c.Width = Unit.Percentage(100%)
        c.Font.Size = "8"
        c.Font.Bold = "true"
        '  word = RupeesToWord(payable)
        ' word.RupeesToWord(payable)
        'Response.Write(word)
        c.HorizontalAlign = HorizontalAlign.Center
        a = oCmnfns.RupeesToWord(payable)
        c.Text = "Rupees - " & a
        'c.Text = "aaaaaaaaaaa"
        r.Cells.Add(c)




        '---------------------------------------------
        Dim cR As New TableCell()
        cR.HorizontalAlign = HorizontalAlign.Right
        cR.VerticalAlign = VerticalAlign.Top
        cR.Width = Unit.Percentage(100%)
        cR.Font.Size = "9"
        cR.Font.Bold = "true"
        cR.Text = payable
        r.Cells.Add(cR)

        tab.Rows.Add(r)


      
        pnlCTC.Controls.Add(tab)
       

    End Sub
    Sub showlogohzl()
        ' EarnDed()
        Dim tabz As Table = New Table()
        'Dim img As New Image
        'Dim img1 As New Image
        'img.ImageUrl = "images/logo.jpg"
        ' img1.ImageUrl = "images/letterhead_hzl.gif"
        tabz.Width = Unit.Percentage(100)
        tabz.CellSpacing = 0
        tabz.CellPadding = 0


        'Response.Write("</br>")
        ' strsql = "Select description From TblParams WHERE description='RAJASTHAN STATE GANGANAGAR SUGAR MILLS LIMITED'" 
        Dim hrtop As New LiteralControl
        hrtop.Text = "</br>"

        pnlCTC.Controls.Add(hrtop)
        Dim r As TableRow = New TableRow()

        showpersonal(ddloffice.SelectedValue, rdr.GetValue(0).ToString, ddlYear.SelectedValue, ddlMonth.SelectedValue)
        Dim tctop As TableCell = New TableCell
        tctop.Text = "RAJASTHAN STATE GANGANAGAR SUGAR MILLS LIMITED," & ddloffice.SelectedItem.Text
        tctop.Style.Add("horizontalAlign", "center")
        tctop.Style.Add("TEXT-ALIGN", "center")
        tctop.Font.Bold = "True"
        tctop.Font.Size = "9"
        r.Cells.Add(tctop)
        tabz.Rows.Add(r)

        Dim r1 As TableRow = New TableRow()
        Dim tctop1 As TableCell = New TableCell
        tctop1.Text = " PAY-SLIP FOR " & ddlMonth.SelectedItem.Text & ", " & ddlYear.SelectedValue
        tctop1.Style.Add("horizontalAlign", "center")
        tctop1.Style.Add("TEXT-ALIGN", "center")
        tctop1.Font.Bold = "True"
        tctop1.Font.Size = "8"
        r1.Cells.Add(tctop1)
        tabz.Rows.Add(r1)

        pnlCTC.Controls.Add(tabz)

        lncnt += 4
    End Sub
    Protected Sub showleave(ByVal ofc As String, ByVal emp As String, ByVal fromYear As String, ByVal fromMon As String)
        Dim finalrow_1 As New TableRow
        Dim I As Integer
        Dim strSql, strWhere, strGroup As String
        Dim objCmd As New OleDbCommand()
        Dim fromDate As String

        No_Of_Daysinformation()

        strSql = "select " & dayInf & " -lwp_days days_work, LWP_days  LWP_days "
        strWhere = " And A.attendance_month =" & fromMon & _
            " And A.attendance_year =" & fromYear
        strWhere = strWhere & " And a.office_code=" & ofc
        ' strWhere = strWhere & " And a.Employee_ID=" & ddlemployee.SelectedValue
        strSql = strSql & " from Attendance A ,Employee_Detail B where " & _
                       " A.company_id=" & Session("CompanyID")



        If emp <> "0" Then
            strWhere = strWhere & "  And a.Employee_ID=" & emp
        End If

        strSql = strSql & strWhere

        strSql = strSql & " And a.employee_id=b.employee_id  and " & _
                       " A.company_id=b.Company_ID And to_Char(B.Effective_date,'DD-Mon-YYYY')" & _
                       " in ( select To_Char(max(Effective_date),'DD-Mon-YYYY')" & _
                       " from Employee_Detail T Where B.Employee_ID=T.Employee_ID and " & _
                       " B.Company_ID= T.Company_ID AND T.Company_ID=" & Session("companyID") & _
                       " and b.office_code=t.office_code AND to_char(t.effective_date,'YYYYMM')<='" & fromYear & fromMon & "' )"

        'Response.Write("aaa------>" & dayInf)
        ' Response.Write(strSql & "<br><br>")
        dsL = New Data.DataSet
        daL = New OleDbDataAdapter(strSql, Con.Con)
        daL.Fill(dsL, "Leave")


        'Response.Write(strSql & "</br>" & "</br>" & "</br>")


        'Dim tab As Table = New Table()
        'tab.Style("border-color") = "#d3d3d3"
        'tab.BorderStyle = BorderStyle.Ridge
        'Dim r As TableRow = New TableRow()

        'Dim tc As TableHeaderCell = New TableHeaderCell()
        'tab.ID = "tblleave"
        'tab.BorderWidth = 2
        'tab.Width = Unit.Percentage(100%)
        'tab.CellSpacing = 0
        'tab.CellPadding = 1
        ''tc.ColumnSpan = 4
        ''tc.Text = "Leave Information"
        ''tc.Font.Underline = "true"
        ''tc.Font.Size = "8"
        ''tc.Style.Add("horizontalAlign", "Left")
        ''tc.Style.Add("TEXT-ALIGN", "Left")
        ''r.CssClass = "headStyle"
        ''r.Cells.Add(tc)
        ''tab.Rows.Add(r)


        'objCmd.CommandText = strSql
        'objCmd.Connection = Con.Con
        'Dim objReader As OleDbDataReader
        'objReader = objCmd.ExecuteReader
        ''   I = 1
        'lncnt += 1
        'If objReader.Read Then


        '    Dim rr As New TableRow()
        '    rr.BackColor = Drawing.Color.LightGray
        '    rr.HorizontalAlign = HorizontalAlign.Left
        '    rr.Width = Unit.Pixel(600)

        '    Dim cL As New TableCell()
        '    cL.HorizontalAlign = HorizontalAlign.Left
        '    cL.VerticalAlign = VerticalAlign.Top
        '    cL.Width = Unit.Pixel(200)
        '    cL.Text = " Working Days"
        '    cL.Font.Size = "10"
        '    cL.Font.Bold = True
        '    rr.Cells.Add(cL)

        '    Dim cR As New TableCell()
        '    cR.HorizontalAlign = HorizontalAlign.Right
        '    cR.VerticalAlign = VerticalAlign.Top
        '    cR.Width = Unit.Pixel(150)
        '    cR.Font.Size = "9"
        '    cR.Text = objReader.GetValue(0).ToString
        '    rr.Cells.Add(cR)

        '    Dim cL1 As New TableCell()
        '    cL1.HorizontalAlign = HorizontalAlign.Left
        '    cL1.VerticalAlign = VerticalAlign.Top
        '    cL1.Width = Unit.Pixel(200)
        '    cL1.Font.Size = "10"
        '    cL1.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;LWP Days"
        '    cL1.Font.Bold = True
        '    rr.Cells.Add(cL1)

        '    Dim cR1 As New TableCell()
        '    cR1.HorizontalAlign = HorizontalAlign.Right
        '    cR1.VerticalAlign = VerticalAlign.Top
        '    cR1.Width = Unit.Pixel(200)
        '    cR1.Font.Size = "9"
        '    cR1.Text = objReader.GetValue(1).ToString
        '    rr.Cells.Add(cR1)
        '    tab.Rows.Add(rr)

        '    lncnt += 1

        'End If
        'objReader.Close()
        'pnlCTC.Controls.Add(tab)

        'Response.Write(strsql & "br")
    End Sub
    Sub showLogoContinue()
        Dim tabz As Table = New Table()
        Dim img As New Image
        ' Dim img1 As New Image
        img.ImageUrl = "images/letterhead_logo.gif"
        ' img1.ImageUrl = "images/letterhead_hzllogo_text.gif"
        tabz.Width = Unit.Percentage(100)
        tabz.CellSpacing = 0
        tabz.CellPadding = 0
        Dim r As TableRow = New TableRow()
        Dim tc As TableCell = New TableCell()
        tc.HorizontalAlign = HorizontalAlign.Left
        tc.Width = Unit.Percentage(50)
        tc.Controls.Add(img)
        r.Cells.Add(tc)
        '     Dim tc1 As TableCell = New TableCell()
        '   tc1.HorizontalAlign = HorizontalAlign.Right
        '   tc1.Width = Unit.Percentage(50)
        '   tc1.Controls.Add(img1)
        '   r.Cells.Add(tc1)
        tabz.Rows.Add(r)
        pnlCTC.Controls.Add(tabz)
        Dim hrtop As New LiteralControl
        'hrtop.Text = "<br><br>"
        pnlCTC.Controls.Add(hrtop)
    End Sub
    Sub showlogoSterlite()
        Dim tabz As Table = New Table()
        Dim img As New Image
        ' Dim img1 As New Image
        img.ImageUrl = "images/letterhead_logo.gif"
        '  img1.ImageUrl = "images/letterhead_sterlite.gif"
        tabz.Width = Unit.Percentage(100)
        tabz.CellSpacing = 0
        tabz.CellPadding = 0
        Dim r As TableRow = New TableRow()
        Dim tc As TableCell = New TableCell()
        tc.HorizontalAlign = HorizontalAlign.Left
        tc.Width = Unit.Percentage(50)
        tc.Controls.Add(img)
        r.Cells.Add(tc)
        '  Dim tc1 As TableCell = New TableCell()
        '  tc1.HorizontalAlign = HorizontalAlign.Right
        '  tc1.Width = Unit.Percentage(50)
        '  tc1.Controls.Add(img1)
        '   r.Cells.Add(tc1)
        '   tabz.Rows.Add(r)
        pnlCTC.Controls.Add(tabz)
        Dim hrtop As New LiteralControl
        hrtop.Text = "<br><br>"
        pnlCTC.Controls.Add(hrtop)
    End Sub

    Sub showPageHeader()
        strsql = "Select description From TblParams WHERE description='RAJASTHAN STATE GANGANAGAR SUGAR MILLS LIMITED' "

        'and fin_year=" & Session("FinYear").ToString
        Dim tabtop As Table = New Table()
        tabtop.CssClass = "Text_small"
        tabtop.Width = Unit.Percentage(100)
        tabtop.CellSpacing = 0
        tabtop.CellPadding = 0
        Dim rtop As TableRow = New TableRow()
        Dim tctop As TableCell = New TableCell
        tctop.HorizontalAlign = HorizontalAlign.Right
        tctop.Width = Unit.Percentage(50)
        'tctop.Font.Size = FontUnit.XSmall
        tctop.Font.Underline = True
        tctop.Text = ExistCount(strsql)
        'tctop.Text = "Letter Head " & Request("dt")
        tctop.Style.Add("horizontalAlign", "Center")
        tctop.Style.Add("TEXT-ALIGN", "Center")
        rtop.Cells.Add(tctop)
        tabtop.Rows.Add(rtop)
        pnlCTC.Controls.Add(tabtop)
        Dim hr As New LiteralControl
        hr.Text = "<br><br>"
        lncnt = lncnt + 3
        pnlCTC.Controls.Add(hr)
    End Sub
    Sub showPageFooter()
        Dim strsql As String
        Dim hr As New LiteralControl
        ' hr.Text = "<br><br>"
        lncnt = lncnt + 2
        pnlCTC.Controls.Add(hr)
        strsql = "Select description From TblParams WHERE description='RAJASTHAN STATE GANGANAGAR SUGAR MILLS LIMITED'" ' and fin_year=" & Session("FinYear").ToString
        Dim tabtop As Table = New Table()
        tabtop.CssClass = "text_small_footer"
        tabtop.Width = Unit.Percentage(100)
        tabtop.CellSpacing = 0
        tabtop.CellPadding = 0
        Dim rtop As TableRow = New TableRow()
        Dim tctop As TableCell = New TableCell
        tctop.Width = Unit.Percentage(50)
        'tctop.Font.Size = FontUnit.XSmall
        'tctop.Font.Underline = True
        tctop.Text = ExistCount(strsql)
        tctop.Style.Add("horizontalAlign", "Center")
        tctop.Style.Add("TEXT-ALIGN", "center")
        rtop.Cells.Add(tctop)
        tabtop.Rows.Add(rtop)
        lncnt = lncnt + 4
        pnlCTC.Controls.Add(tabtop)
        '   hr.Text = "<br><br>"
        lncnt = lncnt + 1
        pnlCTC.Controls.Add(hr)
    End Sub

    Sub employee_count()

        Dim month As String

        If ddlMonth.SelectedValue < 10 Then
            month = "0" + ddlMonth.SelectedValue
        Else
            month = ddlMonth.SelectedValue
        End If

        'If CInt(ddlMonth.SelectedValue) < CInt("10") Then
        '    month = "0" & ddlMonth.SelectedValue

        '    If ddlMonth.SelectedValue <> "9" Then
        '        month = "0" & (ddlMonth.SelectedValue + 1)
        '    Else
        '        month = "10"
        '    End If

        'Else
        '    month = ddlMonth.SelectedValue
        'End If
        '   If (ddlemployee.SelectedValue = "" Or ddlemployee.SelectedValue = "0") Then
        sql = " SELECT distinct ed.employee_id  FROM employee_detail ed, salary s  " & _
              " where  s.employee_id=ed.employee_id " & _
        " and effective_date=(select max(effective_date) from employee_detail tmp where tmp.employee_id=ed.employee_id and to_char(effective_date,'yyyymm')<='" & ddlYear.SelectedValue & month & "')" & _
          "   and s.salary_month='" & month & "'" & _
         "  and s.salary_year='" & ddlYear.SelectedValue & "' and ed.OFFICE_CODE=s.OFFICE_CODE  "
        'and ed.OFFICE_CODE=s.OFFICE_CODE
        If ddloffice.SelectedValue <> 0 Then
            sql = sql & " and ed.OFFICE_CODE='" & ddloffice.SelectedValue & "'"
        End If
        If ddldept.SelectedValue <> 0 Then
            sql = sql & " and ed.DEPARTMENT_CODE='" & ddldept.SelectedValue & "'"
        End If
        sql = sql & "ORDER BY EMPLOYEE_ID"

        'Response.Write(sql & "<BR>")
        'Response.End()
        cmdsal = New OleDbCommand(sql, Con.Con)
        rdr = cmdsal.ExecuteReader()
        If rdr.HasRows Then
            count = 0
            Dim ctr As Integer = 0
            While rdr.Read()
                lncnt = 0
                showlogohzl()
                showpersonal(ddloffice.SelectedValue, rdr.GetValue(0).ToString, ddlYear.SelectedValue, month)
                showleave(ddloffice.SelectedValue, rdr.GetValue(0).ToString, ddlYear.SelectedValue, month)
                showEarnings(ddloffice.SelectedValue, rdr.GetValue(0).ToString, ddlYear.SelectedValue, month)
                showDeductions(ddloffice.SelectedValue, rdr.GetValue(0).ToString, ddlYear.SelectedValue, month)
                EarnDed(ctr)
                ctr = ctr + 1
                showpayable(ddloffice.SelectedValue, rdr.GetValue(0).ToString, ddlYear.SelectedValue, month)

                count = count + 3
                If (count Mod 2) = 0 Then
                    Call pageBreak()
                End If
            End While

            filename = "Salary_slip_" & Date.Now.ToString("dd_MMM_yyyy_hh_mm") & ".txt"
            hfdoc.Value = "Reports/" & filename

            printheader()
        End If
        rdr.Close()
        cmdsal.Dispose()
        'Return empid
    End Sub

    Sub EarnDed(ByVal ctr As Integer)


        ''''''''''''''''''''''''''''''''''Personal''''''''''''''''''''''''''

        Dim tabE As Table = New Table()
        tabE.Style("border-color") = "#d3d3d3"
        tabE.BorderStyle = BorderStyle.Ridge
        Dim rE As TableRow = New TableRow()
        Dim tcE As TableHeaderCell = New TableHeaderCell()
        tabE.ID = "tblpersonal"
        tabE.BorderWidth = 2
        tabE.Width = Unit.Percentage(100%)
        tabE.CellSpacing = 0
        tabE.CellPadding = 1
        tcE.ColumnSpan = 2
        tcE.Text = "Personal Information"
        tcE.Font.Underline = "true"
        tcE.Font.Size = "10"

        tcE.Style.Add("horizontalAlign", "Left")
        tcE.Style.Add("TEXT-ALIGN", "Left")
        rE.CssClass = "headStyle"
        rE.Cells.Add(tcE)
        tabE.Rows.Add(rE)
        ' Dim i As Integer
        Dim count = dsP.Tables("personal").Rows.Count
        'For i = 0 To count - 1
        'If count <> 0 Then
        Dim rrP As New TableRow()
        rrP.HorizontalAlign = HorizontalAlign.Left
        rrP.Width = Unit.Pixel(600)

        Dim cLP As New TableCell()
        cLP.HorizontalAlign = HorizontalAlign.Left
        cLP.VerticalAlign = VerticalAlign.Top
        cLP.Width = Unit.Pixel(300)
        cLP.Text = " ID / Employee Name / Father / Husband Name / Token No."
        cLP.Font.Size = "9"
        cLP.Font.Bold = True
        rrP.Cells.Add(cLP)

        Dim cRP As New TableCell()
        cRP.HorizontalAlign = HorizontalAlign.Left
        cRP.VerticalAlign = VerticalAlign.Top
        cRP.Width = Unit.Pixel(250)
        cRP.Font.Size = "9"
        cRP.Text = dsP.Tables("Personal").Rows(ctr).Item(0)
        rrP.Cells.Add(cRP)
        tabE.Rows.Add(rrP)
        '---------------------------

        '----------------------------------
        Dim rr2 As New TableRow()
        rr2.HorizontalAlign = HorizontalAlign.Left
        rr2.Width = Unit.Pixel(600)

        Dim cL2 As New TableCell()
        cL2.HorizontalAlign = HorizontalAlign.Left
        cL2.VerticalAlign = VerticalAlign.Top
        cL2.Width = Unit.Pixel(230)
        cL2.Text = " Designation  /  Department"
        cL2.Font.Size = "9"

        cL2.Font.Bold = True
        rr2.Cells.Add(cL2)

        Dim cR2 As New TableCell()
        cR2.HorizontalAlign = HorizontalAlign.Left
        cR2.VerticalAlign = VerticalAlign.Top
        cR2.Width = Unit.Pixel(250)
        cR2.Font.Size = "9"
        'cR2.Text = objReader.GetValue(3).ToString
        cR2.Text = dsP.Tables("Personal").Rows(ctr).Item(3)
        rr2.Cells.Add(cR2)
        tabE.Rows.Add(rr2)
        '------------------------------------------------
        Dim rr4 As New TableRow()
        rr4.HorizontalAlign = HorizontalAlign.Left
        rr4.Width = Unit.Pixel(600)

        Dim cL4 As New TableCell()
        cL4.HorizontalAlign = HorizontalAlign.Left
        cL4.VerticalAlign = VerticalAlign.Top
        cL4.Width = Unit.Pixel(250)
        cL4.Text = " Bank Name / Branch Name / A/C No./ Pay Band"
        cL4.Font.Size = "9"

        cL4.Font.Bold = True
        rr4.Cells.Add(cL4)

        Dim cR4 As New TableCell()
        cR4.HorizontalAlign = HorizontalAlign.Left
        cR4.VerticalAlign = VerticalAlign.Top
        cR4.Width = Unit.Pixel(250)
        cR4.Font.Size = "9"
        'cR4.Text = objReader.GetValue(5).ToString
        cR4.Text = dsP.Tables("Personal").Rows(ctr).Item(4)
        rr4.Cells.Add(cR4)
        tabE.Rows.Add(rr4)
        ' -----------------------------------------------
        pnlCTC.Controls.Add(tabE)

        ''''''''''''''''''''''''''''''''End Personal''''''''''''''''''''''''
        '''''''''************ LEAVE ***************''''''''''''''''''
        Dim tabL As Table = New Table()
        tabL.Style("border-color") = "#d3d3d3"
        tabL.BorderStyle = BorderStyle.Ridge
        Dim rlv As TableRow = New TableRow()

        Dim tclv As TableHeaderCell = New TableHeaderCell()
        tabL.ID = "tblleave"
        tabL.BorderWidth = 2
        tabL.Width = Unit.Percentage(100%)
        tabL.CellSpacing = 0
        tabL.CellPadding = 1

        If dsL.Tables("Leave").Rows.Count <> 0 Then


            Dim rr As New TableRow()
            rr.BackColor = Drawing.Color.LightGray
            rr.HorizontalAlign = HorizontalAlign.Left
            rr.Width = Unit.Pixel(600)

            Dim cL As New TableCell()
            cL.HorizontalAlign = HorizontalAlign.Left
            cL.VerticalAlign = VerticalAlign.Top
            cL.Width = Unit.Pixel(200)
            cL.Text = " Working Days"
            cL.Font.Size = "10"
            cL.Font.Bold = True
            rr.Cells.Add(cL)

            Dim cR As New TableCell()

            cR.HorizontalAlign = HorizontalAlign.Right
            cR.VerticalAlign = VerticalAlign.Top
            cR.Width = Unit.Pixel(150)
            cR.Font.Size = "9"
            ' cR.Text = objReader.GetValue(0).ToString
            cR.Text = dsL.Tables("Leave").Rows(0).Item(0)

            Leave(Lcnt, 0) = dsL.Tables("Leave").Rows(0).Item(0)




            ' Response.Write(dsL.Tables("Leave").Rows(0).Item(0) & "<br>" & "<br>")
            '  present = dsL.Tables("Leave").Rows(0).Item(0)
            rr.Cells.Add(cR)

            Dim cL1 As New TableCell()
            cL1.HorizontalAlign = HorizontalAlign.Left
            cL1.VerticalAlign = VerticalAlign.Top
            cL1.Width = Unit.Pixel(200)
            cL1.Font.Size = "10"
            cL1.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;LWP Days"
            cL1.Font.Bold = True
            rr.Cells.Add(cL1)

            Dim cR1 As New TableCell()
            '  Dim lwp As Integer
            cR1.HorizontalAlign = HorizontalAlign.Right
            cR1.VerticalAlign = VerticalAlign.Top
            cR1.Width = Unit.Pixel(200)
            cR1.Font.Size = "9"
            '  cR1.Text = objReader.GetValue(1).ToString
            cR1.Text = dsL.Tables("Leave").Rows(0).Item(1)
            Leave(Lcnt, 1) = dsL.Tables("Leave").Rows(0).Item(1)
            Lcnt = Lcnt + 1
            ' Response.Write(dsL.Tables("Leave").Rows(0).Item(1) & "<br>")
            ' lwp = dsL.Tables("Leave").Rows(0).Item(1)
            rr.Cells.Add(cR1)
            tabL.Rows.Add(rr)
        End If
        pnlCTC.Controls.Add(tabL)
        '''''''''************ ENDLEAVE ***************''''''''''''''''''

        ''''''''''''''''''''''Earning'''''''''''''''''''''''''
        Dim tab As Table = New Table()
        tab.Style("border-color") = "#d3d3d3"
        tab.BorderStyle = BorderStyle.Ridge
        Dim r As TableRow = New TableRow()
        Dim tc As TableHeaderCell = New TableHeaderCell()
        tab.ID = "tblEarning"
        tab.BorderWidth = 2
        tab.Width = Unit.Percentage(50%)
        tab.HorizontalAlign = HorizontalAlign.Left
        tab.CellSpacing = 0
        tab.CellPadding = 0
        ' tc.ColumnSpan = 1
        tc.Text = "Earnings"
        tc.Font.Underline = "true"
        tc.Font.Size = "10"
        tc.Style.Add("horizontalAlign", "Left")
        tc.Style.Add("TEXT-ALIGN", "Left")
        r.CssClass = "headStyle"
        r.Cells.Add(tc)
        tab.Rows.Add(r)

        Dim r1 As TableRow = New TableRow()
        Dim tcL As TableCell = New TableCell
        tcL.Width = Unit.Pixel(100)

        tcL.HorizontalAlign = HorizontalAlign.Left
        tcL.VerticalAlign = VerticalAlign.Top
        tcL.Controls.Add(tab)
        r1.Cells.Add(tcL)
        'I = 1
        'lncnt += 1
        Dim CntE As Integer = 0
        Dim CntD As Integer = 0
        Dim e1 As Integer = 0
        Dim d1 As Integer = 0
        Dim er As Double = 0
        Dim de As Double = 0

        If dsE.Tables("Earnings").Rows.Count > dsD.Tables("Deduction").Rows.Count Then
            d1 = dsE.Tables("Earnings").Rows.Count - dsD.Tables("Deduction").Rows.Count
        Else
            e1 = dsD.Tables("Deduction").Rows.Count - dsE.Tables("Earnings").Rows.Count
        End If


        ' d1 = d1 + 1
        'e1 = e1 + 1
        Dim q As Integer
        Dim t As Integer

        earnmax = dsE.Tables("Earnings").Rows.Count
        For q = 0 To dsE.Tables("Earnings").Rows.Count - 1
            Dim rr As New TableRow()
            rr.Width = Unit.Pixel(100)
            rr.Font.Size = "9"
            Dim cL As New TableCell()
            cL.Width = Unit.Pixel(200)
            cL.Font.Size = "9"
            cL.Text = dsE.Tables("Earnings").Rows(q).Item(0)
            arrern = arrern & String.Format("{0,0 :G}", dsE.Tables("Earnings").Rows(q).Item(0)).PadRight(15) & " " & arrernamt & String.Format("{0,10 :G}", dsE.Tables("Earnings").Rows(q).Item(1)).PadLeft(13) & "@" '& arrded & dsD.Tables("Deduction").Rows(t).Item(0) & " " & arrdedamt & Format(dsD.Tables("Deduction").Rows(t).Item(1), "###0.00").ToString & "@"
            er = er + dsE.Tables("Earnings").Rows(q).Item(1)
            '.PadLeft(3)
            'Response.Write(er & "<br>" & "<br>")
            cL.Font.Bold = True
            rr.Cells.Add(cL)
            Dim cR As New TableCell()
            cR.Width = Unit.Pixel(170)
            cR.Text = Format(dsE.Tables("Earnings").Rows(q).Item(1), "###0.00").ToString
            cR.HorizontalAlign = HorizontalAlign.Right
            cR.Font.Size = "9"
            rr.Cells.Add(cR)
            totearning = totearning + CDbl(dsE.Tables("Earnings").Rows(q).Item(1))
            '  Response.Write("<br>" & "<br>" & totalern)
            Dim z, y, w As Integer
            ' z = totearning.ToString.Length
            ' y = dsD.Tables("Deduction").Rows(t).Item(1)
            ' w = z - y
            ' Response.Write("w====" & w)
            tab.Rows.Add(rr)
            RwCnt += 1
            CntE = CntE - 1
        Next


        '   Response.Write(arrern & "<br>")
        ' totalern = totalern & totearning & "!"
        'Response.Write("<br>" & "<br>" & totalern)


        While (e1 > 0)

            Dim rTot2 As New TableRow()
            Dim tcLTot2 As TableCell = New TableCell
            tcLTot2.Text = ""
            tcLTot2.Font.Size = "9"
            rTot2.Cells.Add(tcLTot2)

            Dim tcRTot2 As TableCell = New TableCell
            tcRTot2.Text = "&nbsp;" 'CStr(Format(totdeduction, "###0.00"))
            tcRTot2.Font.Size = "9"
            rTot2.Cells.Add(tcRTot2)
            rTot2.Font.Bold = True
            tab.Rows.Add(rTot2)
            arrern = arrern & "                             " & "@"
            e1 = e1 - 1
        End While
        arrern = arrern & er ' & "@" '& de

        Dim e2 As Integer
        Dim d2 As Integer

        e2 = e1 + 1
        d2 = d1 + 1

        'While (e2 > 0)
        '    arrern = arrern & "                             " & "@"
        '    e2 = e2 - 1
        'End While
        arrern = arrern & "$"

        Dim rTot As New TableRow()
        rTot.BackColor = Drawing.Color.LightGray
        Dim tcLTot As TableCell = New TableCell

        tcLTot.Text = "Gross"
        tcLTot.Font.Size = "9"
        rTot.Cells.Add(tcLTot)

        Dim tcRTot As TableCell = New TableCell
        tcRTot.Text = CStr(Format(totearning, "###0.00"))
        tcRTot.HorizontalAlign = HorizontalAlign.Right
        tcRTot.Font.Size = "9"
        rTot.Cells.Add(tcRTot)
        rTot.Font.Bold = True
        tab.Rows.Add(rTot)
        lncnt += 1
        RwCnt += 1
        pnlCTC.Controls.Add(tab)
        ''''''''''''''''''''''''End Earning'''''''''''''''''''''''
        ''''''''''''''''''''''Deduction'''''''''''''''''''''''''''
        Dim tabD As Table = New Table()
        tabD.Style("border-color") = "#d3d3d3"
        tabD.BorderWidth = 2
        tabD.BorderStyle = BorderStyle.Ridge
        Dim rD As TableRow = New TableRow()
        rD.CssClass = "headStyle"
        Dim tcD As TableHeaderCell = New TableHeaderCell()
        tabD.ID = "tblDeduction"
        tabD.Width = Unit.Percentage(50%)
        tabD.CellSpacing = 0
        tabD.CellPadding = 0
        tcD.HorizontalAlign = HorizontalAlign.Left
        tcD.ColumnSpan = 2
        tcD.Text = "Deductions"
        tcD.Font.Size = "10"
        tcD.Font.Underline = "true"
        tcD.Style.Add("horizontalAlign", "Left")
        rD.Cells.Add(tcD)
        tabD.Rows.Add(rD)

        Dim r1D As TableRow = New TableRow()
        Dim tcLD As TableCell = New TableCell
        tcLD.Width = Unit.Pixel(100)

        tcLD.HorizontalAlign = HorizontalAlign.Left
        tcLD.VerticalAlign = VerticalAlign.Top
        tcLD.Controls.Add(tabD)
        r1D.Cells.Add(tcLD)


        dedmax = dsD.Tables("Deduction").Rows.Count
        ' Response.Write("dedmax=" & dedmax)
        For t = 0 To dsD.Tables("Deduction").Rows.Count - 1
            Dim rr As New TableRow()
            rr.Width = Unit.Pixel(100)
            rr.Font.Size = "9"
            Dim cL As New TableCell()
            cL.Width = Unit.Pixel(150)
            cL.Font.Size = "9"
            cL.Text = dsD.Tables("Deduction").Rows(t).Item(0)
            arrded = arrded & String.Format("{0,0 :G}", dsD.Tables("Deduction").Rows(t).Item(0)).PadRight(20) & " " & arrdedamt & String.Format("{0,10 :G}", dsD.Tables("Deduction").Rows(t).Item(1)).PadLeft(10) & "@"
            'arrded = arrded & dsD.Tables("Deduction").Rows(q).Item(0)
            de = de + dsD.Tables("Deduction").Rows(t).Item(1)
            cL.Font.Bold = True
            rr.Cells.Add(cL)
            Dim cR As New TableCell()
            cR.Width = Unit.Pixel(200)
            cR.Text = Format(dsD.Tables("Deduction").Rows(t).Item(1), "###0.00").ToString
            '  arrdedamt = arrdedamt & Format(dsD.Tables("Deduction").Rows(q).Item(1), "###0.00").ToString
            cR.HorizontalAlign = HorizontalAlign.Right
            cR.Font.Size = "9"
            rr.Cells.Add(cR)
            totdeduction = totdeduction + CDbl(dsD.Tables("Deduction").Rows(t).Item(1).ToString)
            tabD.Rows.Add(rr)
            RwCnt2 += 1
        Next


        While (d1 > 0)

            Dim rTot2 As New TableRow()
            Dim tcLTot2 As TableCell = New TableCell
            tcLTot2.Text = ""
            tcLTot2.Font.Size = "9"
            rTot2.Cells.Add(tcLTot2)

            Dim tcRTot2 As TableCell = New TableCell
            tcRTot2.Text = "&nbsp;" 'CStr(Format(totdeduction, "###0.00"))
            tcRTot2.Font.Size = "9"
            rTot2.Cells.Add(tcRTot2)
            rTot2.Font.Bold = True
            tabD.Rows.Add(rTot2)
            arrded = arrded & "    " & "@"
            d1 = d1 - 1
        End While




        arrded = arrded & de '& "@"  '& de



        arrded = arrded & "$"
        Dim rTotD As New TableRow()
        rTotD.BackColor = Drawing.Color.LightGray
        Dim tcLTotD As TableCell = New TableCell
        tcLTotD.Width = Unit.Pixel(170)
        tcLTotD.Text = "Total Deduction"
        tcLTotD.Font.Size = "9"
        rTotD.Cells.Add(tcLTotD)


        Dim tcRTotD As TableCell = New TableCell
        tcRTotD.Text = CStr(Format(totdeduction, "###0.00"))
        tcRTotD.HorizontalAlign = HorizontalAlign.Right
        tcRTotD.Font.Size = "9"
        rTotD.Cells.Add(tcRTotD)
        rTotD.Font.Bold = True
        tabD.Rows.Add(rTotD)
        pnlCTC.Controls.Add(tabD)
        lncnt += 1
        pnlCTC.Controls.Add(tabD)
        '''''''''''''''''''''End Deduction'''''''''''''''''''''''
    End Sub

    Sub EarnDed1(ByVal ctr As Integer)




        ''''''''''''''''''''''''''''''''''Personal''''''''''''''''''''''''''

        Dim tabE As Table = New Table()
        tabE.Style("border-color") = "#d3d3d3"
        tabE.BorderStyle = BorderStyle.Ridge
        Dim rE As TableRow = New TableRow()
        Dim tcE As TableHeaderCell = New TableHeaderCell()
        tabE.ID = "tblpersonal"
        tabE.BorderWidth = 2
        tabE.Width = Unit.Percentage(100%)
        tabE.CellSpacing = 0
        tabE.CellPadding = 1
        tcE.ColumnSpan = 2
        tcE.Text = "Personal Information"
        tcE.Font.Underline = "true"
        tcE.Font.Size = "10"

        tcE.Style.Add("horizontalAlign", "Left")
        tcE.Style.Add("TEXT-ALIGN", "Left")
        rE.CssClass = "headStyle"
        rE.Cells.Add(tcE)
        tabE.Rows.Add(rE)
        ' Dim i As Integer
        Dim count = dsP.Tables("personal").Rows.Count
        'For i = 0 To count - 1
        'If count <> 0 Then
        Dim rrP As New TableRow()
        rrP.HorizontalAlign = HorizontalAlign.Left
        rrP.Width = Unit.Pixel(600)

        Dim cLP As New TableCell()
        cLP.HorizontalAlign = HorizontalAlign.Left
        cLP.VerticalAlign = VerticalAlign.Top
        cLP.Width = Unit.Pixel(300)
        cLP.Text = " ID / Employee Name / Father / Husband Name"
        cLP.Font.Size = "9"
        cLP.Font.Bold = True
        rrP.Cells.Add(cLP)

        'Response.Write("<BR> SSS     " & ctr & "<BR>")
        ' Response.Write(dsP.Tables("Personal").Rows(count))
        'Response.Write("<BR>AA       " & dsP.Tables("Personal").Rows(ctr).Item(4))
        ' Response.End()


        Dim cRP As New TableCell()
        cRP.HorizontalAlign = HorizontalAlign.Left
        cRP.VerticalAlign = VerticalAlign.Top
        cRP.Width = Unit.Pixel(250)
        cRP.Font.Size = "9"
        ' cRP.Text = dsP.Tables("Personal").Rows(ctr).Item(0)
        rrP.Cells.Add(cRP)
        tabE.Rows.Add(rrP)
        '---------------------------

        '----------------------------------
        Dim rr2 As New TableRow()
        rr2.HorizontalAlign = HorizontalAlign.Left
        rr2.Width = Unit.Pixel(600)

        Dim cL2 As New TableCell()
        cL2.HorizontalAlign = HorizontalAlign.Left
        cL2.VerticalAlign = VerticalAlign.Top
        cL2.Width = Unit.Pixel(230)
        cL2.Text = " Designation  /  Department"
        cL2.Font.Size = "9"

        cL2.Font.Bold = True
        rr2.Cells.Add(cL2)

        Dim cR2 As New TableCell()
        cR2.HorizontalAlign = HorizontalAlign.Left
        cR2.VerticalAlign = VerticalAlign.Top
        cR2.Width = Unit.Pixel(250)
        cR2.Font.Size = "9"
        ' cR2.Text = dsP.Tables("Personal").Rows(ctr).Item(3)
        rr2.Cells.Add(cR2)
        tabE.Rows.Add(rr2)
        '------------------------------------------------
        Dim rr4 As New TableRow()
        rr4.HorizontalAlign = HorizontalAlign.Left
        rr4.Width = Unit.Pixel(600)

        Dim cL4 As New TableCell()
        cL4.HorizontalAlign = HorizontalAlign.Left
        cL4.VerticalAlign = VerticalAlign.Top
        cL4.Width = Unit.Pixel(250)
        cL4.Text = " Bank Name / Branch Name / A/C No./ Pay Band"
        cL4.Font.Size = "9"

        cL4.Font.Bold = True
        rr4.Cells.Add(cL4)

        Dim cR4 As New TableCell()
        cR4.HorizontalAlign = HorizontalAlign.Left
        cR4.VerticalAlign = VerticalAlign.Top
        cR4.Width = Unit.Pixel(250)
        cR4.Font.Size = "9"
        'cR4.Text = dsP.Tables("Personal").Rows(ctr).Item(4)
        rr4.Cells.Add(cR4)
        tabE.Rows.Add(rr4)
        ' -----------------------------------------------
        pnlCTC.Controls.Add(tabE)

     


    End Sub
    

    Protected Sub btnCancelDetail_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCancelDetail.Click
        divSlip.Style("display") = "none"
        divcriteria.Style("display") = ""
        lblError.Text = ""
    End Sub

    Protected Sub fillDetailView(ByVal strsql As String, ByVal dv As DetailsView)
        Try
            Dim ds As New Data.DataSet
            Dim da As OleDbDataAdapter
            da = New OleDbDataAdapter(strsql, Con.Con)
            da.Fill(ds, "tblrole")
            lblError.Text = ""
            If ds.Tables("tblrole").Rows.Count > 0 Then
                lblError.Text = ""
            Else
                lblError.Text = "No data found"
                Exit Sub
            End If
            dv.DataSource = ds
            dv.DataBind()
            ds.Clear()
        Catch exole As OleDbException
            lblError.Text += exole.Message & "<Br>"
        Catch ex As Exception
            lblError.Text += ex.Message & "<Br>"
        End Try
    End Sub


    Sub printheader()
        totcount = 0
        Dim month As String
        Dim i As Integer
        Dim mnth As Integer
        month = ddlMonth.SelectedItem.Text
        ' fp = File.CreateText(Server.MapPath("~\Reports\") & filename)
        If CInt(ddlMonth.SelectedValue) < CInt("10") Then
            mnth = "0" & ddlMonth.SelectedValue
        End If

        '  Response.Write(arrern & "<br>" & "<br>" & "<br>")
        ' Response.Write(arrern & "<br>" & "<br>")
        fp = File.CreateText(Server.MapPath("~\Reports\") & filename)



        Dim linecnt As UInt16 = 0
        totcount = 0

        Dim a As Integer
        Dim c As Integer
        Dim arremp As Array
        Dim arrempd As Array
        Dim pgbr As Integer
        Dim nwpg As Integer
        'Response.Write(arrern)
        arremp = arrern.Split("$")
        arrempd = arrded.Split("$")
        Lcnt = 0

        For a = 0 To (UBound(arremp) - 1 And UBound(arrempd) - 1)
            nwpg = a
            pgbr = nwpg Mod 3
            If pgbr = 0 And a <> 0 Then
                Dim k As Integer
                Dim dif As Integer
                dif = 70 - totcount
                fp.WriteLine("")
                printStr += Chr(12)
                'For k = 0 To dif - 1
                '    fp.WriteLine(" ")
                '    printStr = printStr & "\n"
                'Next
                pgCount = pgCount + 1
                linecnt = 0
                i = 0
                totcount = 0
            End If
            linecnt = 0
            i = 1
            'fp.WriteLine("------------------------------------------------------------------------------------------------------------------------------------")
            'printStr = printStr & "------------------------------------------------------------------------------------------------------------------------------------ \n"
            ' linecnt = i
            fp.WriteLine("                 RAJASTHAN STATE GANGANAGAR SUGAR MILL, " & dsP.Tables("Personal").Rows(a).Item(2) & "                                               ")
            printStr = printStr & "                RAJASTHAN STATE GANGANAGAR SUGAR MILL, " & dsP.Tables("Personal").Rows(a).Item(2) & "                                                \n"
            'i = i + 1
            linecnt = i

            fp.WriteLine("                              Pay Slip for - " & ddlMonth.SelectedItem.Text & " " & ddlYear.SelectedItem.Text & "                                                      ")
            printStr = printStr & "                              Pay Slip for - " & ddlMonth.SelectedItem.Text & " " & ddlYear.SelectedItem.Text & "                                                       \n"

            ' fp.WriteLine("")
            i = i + 1
            linecnt = i

            fp.WriteLine("    ID / Employee Name / Father Name / Token No. :  " & dsP.Tables("Personal").Rows(a).Item(0) & "      ")
            printStr = printStr & "    ID / Employee Name / Father Name / Token No. :  " & dsP.Tables("Personal").Rows(a).Item(0) & "      \n"
            i = i + 1
            linecnt = i
            'fp.WriteLine("    Office Name                           :   " & dsP.Tables("Personal").Rows(a).Item(2) & "      ")
            'printStr = printStr & "    Office Name                           :   " & dsP.Tables("Personal").Rows(a).Item(2) & "       \n"
            'i = i + 1
            'linecnt = i
            fp.WriteLine("    Designation / Department                     :  " & dsP.Tables("Personal").Rows(a).Item(3) & "      ")
            printStr = printStr & "    Designation / Department                     :  " & dsP.Tables("Personal").Rows(a).Item(3) & "      \n"
            i = i + 1
            linecnt = i
            fp.WriteLine("    Bank Name / Branch Name / A/C No./Pay Band   :  " & dsP.Tables("Personal").Rows(a).Item(4) & "      ")
            printStr = printStr & "    Bank Name / Branch Name / A/C No./Pay Band   :  " & dsP.Tables("Personal").Rows(a).Item(4) & "       \n"
            i = i + 1
            linecnt = i

            'fp.WriteLine("------------------------------------------------------------------------------------------------------------------------------------")
            'printStr = printStr & "------------------------------------------------------------------------------------------------------------------------------------ \n"
            fp.WriteLine("    Working Days                                 :   " & Leave(Lcnt, 0) & "             LWP Days      :   " & Leave(Lcnt, 1) & "                                              ")
            printStr = printStr & "    Working Days                                 :   " & Leave(Lcnt, 0) & "             LWP Days      :   " & Leave(Lcnt, 1) & "                                               \n"
            i = i + 1
            Lcnt = Lcnt + 1
            linecnt = i
            fp.WriteLine("------------------------------------------------------------------------------------------------------------------------------------")
            printStr = printStr & "------------------------------------------------------------------------------------------------------------------------------------ \n"
            i = i + 1
            linecnt = i
            fp.WriteLine("            PAY & ALLOWANCES            |                    DEDUCTIONS                                                     ")
            printStr = printStr & "            PAY & ALLOWANCES            |                    DEDUCTIONS                                                      \n"
            i = i + 1
            linecnt = i
            fp.WriteLine("------------------------------------------------------------------------------------------------------------------------------------")
            printStr = printStr & "------------------------------------------------------------------------------------------------------------------------------------ \n"
            i = i + 1
            linecnt = i
            'Response.Write("<br>" & "lncnt1=" & linecnt & "<br>")
            '-----------------------------------------------------------
            Dim ln As Integer = 0
            Dim max As Integer
            'Response.Write("<br>" & "lncnt111=" & earnmax & "<br>")
            'Response.Write("<br>" & "lncnt122=" & dedmax & "<br>")
            If earnmax > dedmax Then
                max = earnmax
            Else
                max = dedmax
            End If
            ln = max
            ln = ln + 1

            Dim b As Integer
            Dim d As Integer
            Dim arrhead As Array
            Dim arrheadd As Array
            arrhead = arremp(a).ToString.Split("@")
            arrheadd = arrempd(a).ToString.Split("@")

            For b = 0 To (UBound(arrhead) - 1 And UBound(arrheadd) - 1)
                fp.WriteLine("    " & arrhead(b) & "                 " & arrheadd(b))
                printStr = printStr & "    " & arrhead(b) & "                 " & arrheadd(b) & " \n"
            Next

            '------------------------------------------------------------------------------
            fp.WriteLine("------------------------------------------------------------------------------------------------------------------------------------")
            printStr = printStr & "------------------------------------------------------------------------------------------------------------------------------------ \n"
            ' Response.Write("lncnt25=" & linecnt & "<br>")
            i = i + ln
            i = i + 1
            linecnt = i
            'Response.Write("lncnt23=" & linecnt & "<br>")
            fp.WriteLine("    GROSS           :   Rs. " & arrhead(b) & "                 TOTAL DEDUCTION     :   Rs." & arrheadd(b) & "                                    ")
            printStr = printStr & "    GROSS           :   Rs. " & arrhead(b) & "                 TOTAL DEDUCTION     :   Rs." & arrheadd(b) & "                                     \n"
            i = i + 1
            linecnt = i
            fp.WriteLine("------------------------------------------------------------------------------------------------------------------------------------")
            printStr = printStr & "------------------------------------------------------------------------------------------------------------------------------------ \n"
            i = i + 1
            linecnt = i
            fp.WriteLine("    NET PAYABLE     :  " & oCmnfns.RupeesToWord(arrhead(b) - arrheadd(b)) & "       Rs." & arrhead(b) - arrheadd(b) & "                                    ")
            printStr = printStr & "    NET PAYABLE     :  " & oCmnfns.RupeesToWord(arrhead(b) - arrheadd(b)) & "       Rs." & arrhead(b) - arrheadd(b) & "                                     \n"
            i = i + 1
            linecnt = i
            fp.WriteLine("------------------------------------------------------------------------------------------------------------------------------------")
            printStr = printStr & "------------------------------------------------------------------------------------------------------------------------------------ \n"
            ''  i = i + 1
            ' linecnt = i
            ' fp.WriteLine("")
            i = i + 1
            linecnt = i
            fp.WriteLine("************************************************************************************************************************************")
            printStr = printStr & "************************************************************************************************************************************ \n"
            i = i + 1
            linecnt = i

            ' Response.Write("lncnt=" & linecnt & "<br>")
            ' fp.WriteLine("")
            ' i = i + 1
            totcount = totcount + linecnt
            'ln = 0
            ' Response.Write(totcount & "<br>")
            'linecnt = 0

            'If totcount >= 60 And totcount <= 70 Then

            '    Dim k As Integer
            '    Dim dif As Integer
            '    dif = 71 - totcount
            '    For k = 0 To dif
            '        fp.WriteLine(" ")
            '        printStr = printStr & "\n"
            '    Next
            '    pgCount = pgCount + 1
            '    '  Header()
            '    linecnt = 0
            '    i = 0
            '    totcount = 0
            'End If

            'totcount = 0
            '  nwpg = nwpg + 1
        Next

        


        ' Next

        '  Next

        '     End If
        '    count = count + 1
        '  End While

        fp.Close()
        hidprint.Value = printStr
        ' Response.Write(hidprint.Value)
        '  End If
        '  rdr.Close()
        ' cmdsal.Dispose()

    End Sub
    
    'Sub header()


    '    Dim cmdsal As OleDbCommand
    '    Dim cmdsalary As OleDbDataAdapter
    '    Dim ds As New DataSet
    '    Dim rdr As OleDbDataReader

    '    Dim subsql As String = ""
    '    Dim earnsum As String = ""
    '    Dim dedsum As String = ""
    '    Dim days As String

    '    Dim netamt As String = ""
    '    Dim compsum As String = ""
    '    Dim compnet As String = ""
    '    Dim loansum As String = ""
    '    Dim redsum As String = ""
    '    Dim addsum As String = ""

    '    ' lbldate.Text = "Dated :" & Date.Today.ToString("dd-MMM-yyyy")

    '    hfUnit.Value = ""
    '    If Request("Unit") = "0" Then
    '        hfUnit.Value = Request("UntID") '-------Unit
    '    Else
    '        hfUnit.Value = Request("Unit") ''-------Office
    '    End If


    '    hfPayroll.Value = ""
    '    If Request("PayRoll") = "-1" Then
    '        hfPayroll.Value = "0"
    '    Else
    '        hfPayroll.Value = Request("PayRoll")
    '    End If

    '    If Request("deptmnt") = "-1" Then
    '        hfdept.Value = "0"
    '    Else
    '        hfdept.Value = Request("deptmnt")
    '    End If


    '    If CInt(Request("Month")) < CInt("10") Then
    '        hfMonth.Value = "0" & Request("Month")
    '    Else
    '        hfMonth.Value = Request("Month")
    '    End If


    '    hfYear.Value = Request("Year")
    '    ' officetype = ddlofficetype.SelectedItem.Text
    '    days = Date.DaysInMonth(Request("Year"), Request("Month"))
    '    ' lblPrintMonth.Text = MonthName(hfMonth.Value, False) & " " & hfYear.Value


    '    Dim cmdrole As OleDbCommand
    '    Dim rdrrole As OleDbDataReader


    '    sql = "    SELECT distinct employee_name || ' / ' emp_father_name," & _
    '            " em.employee_id || ' / ' internal_token_id, dept_name, designation_name," & _
    '            " ROUND (31 - lwp_days) days" & _
    '            " FROM employee_master em," & _
    '            " employee_detail ed," & _
    '            " salary s," & _
    '            " attendance a," & _
    '            " department_master dm," & _
    '            " designation d," & _
    '            " salary_lock sl" & _
    '            " where" & _
    '            " em.company_id = ed.company_id" & _
    '            " and em.employee_id=ed.employee_id" & _
    '            " and s.employee_id=ed.employee_id" & _
    '            " and s.employee_id=sl.employee_id" & _
    '            " and ed.designation_code=d.designation_code" & _
    '            " and dm.dept_id= ed.department_code" & _
    '            " and s.salary_month='7'" & _
    '            " and s.salary_year='2009'" & _
    '            " and s.salary_month=sl.MONTH" & _
    '            " and s.salary_year=sl.YEAR" & _
    '            " and ed.employee_id=a.employee_id" & _
    '            " and a.ATTENDANCE_MONTH=s.salary_month" & _
    '            " and a.ATTENDANCE_YEAR=s.salary_year" & _
    '            " and ed.employee_id='118'"




    'End Sub


End Class
