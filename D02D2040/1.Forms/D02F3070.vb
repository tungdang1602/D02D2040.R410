Public Class D02F3070
    Private _formIDPermission As String = "D02F3070"
    Public WriteOnly Property FormIDPermission() As String
        Set(ByVal Value As String)
            _formIDPermission = Value
        End Set
    End Property

#Region "Const of tdbg - Total of Columns: 2"
    Private Const COL_IsNotPrint As Integer = 0 ' IsNotPrint
    Private Const COL_StyleExcel As Integer = 1 ' StyleExcel
#End Region

    Private dtGrid, dtPeriod, dtCol As DataTable

    Private Sub LoadLanguage()
        '================================================================ 
        Me.Text = rl3("Truy_van_thuyet_minh_tang_giam_tai_san") & " - " & Me.Name & UnicodeCaption(gbUnicode) 'Truy vÊn thuyÕt minh tŸng gi¶m tªi s¶n
        '================================================================ 
        lblPeriod.Text = rL3("Ky") 'Kỳ
        lblReportCode.Text = rl3("Bao_cao") 'Báo cáo
        lblDivisionID.Text = rl3("Don_vi") 'Đơn vị
        lblReportType.Text = rl3("Loai_bao_cao") 'Loại báo cáo
        '================================================================ 
        btnFilter.Text = rl3("Loc") & " (F5)" 'Lọc
        '================================================================ 
        grpFilter.Text = rl3("Dieu_kien_loc") 'Điều kiện lọc
        grpDetail.Text = rl3("Thuyet_minh_tang_giam_tai_san") 'Thuyết minh tăng giảm tài sản
        '================================================================ 
        tdbcReportType.Columns("ReportTypeID").Caption = rl3("Ma") 'Mã
        tdbcReportType.Columns("ReportTypeName").Caption = rl3("Ten") 'Tên
        tdbcReportCode.Columns("ReportCode").Caption = rl3("Ma") 'Mã
        tdbcReportCode.Columns("ReportName").Caption = rL3("Ten") 'Tên
        tdbcDivisionID.Columns("DivisionID").Caption = rl3("Ma") 'Mã
        tdbcDivisionID.Columns("DivisionName").Caption = rl3("Ten") 'Tên
        tdbcPeriodFrom.Columns("TranMonth").Caption = rl3("Month") 'Month
        tdbcPeriodFrom.Columns("TranYear").Caption = rl3("Year") 'year
        tdbcPeriodTo.Columns("TranMonth").Caption = rl3("Month") 'Month
        tdbcPeriodTo.Columns("TranYear").Caption = rL3("Year") 'year


    End Sub



    Private Sub D02F3070_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Cursor = Cursors.WaitCursor
        LoadInfoGeneral() 'Load System/ Option /... in DxxD9940
        ResetColorGrid(tdbg)
        gbEnabledUseFind = False
        LoadLanguage()
        SetShortcutPopupMenu(Me, Nothing, ContextMenuStrip1)
        InputbyUnicode(Me, gbUnicode)
        LoadTDBCombo()
        SetBackColorObligatory()
        'Nếu form có nút Lọc thì mở ra
        CheckMenu("-1", Nothing, tdbg.RowCount, gbEnabledUseFind, False, ContextMenuStrip1)
        SetResolutionForm(Me, ContextMenuStrip1)
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub LoadTDBCombo()
        dtPeriod = LoadTablePeriodReport("D02")
        LoadCboDivisionIDAll(tdbcDivisionID, D02, False, gbUnicode)
        tdbcDivisionID.SelectedIndex = 0

        'LoadDataSource(tdbcReportType, SQLStoreD02P3071, gbUnicode)

        'LoadtdbcReportCode("-1")
    End Sub

    Private Sub LoadtdbcReportCode(ByVal sID As String)
        LoadDataSource(tdbcReportCode, SQLStoreD02P3072, gbUnicode)
    End Sub

    Private Sub D02F3070_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt Then
        ElseIf e.Control Then
        Else
            Select Case e.KeyCode
                Case Keys.Enter
                    UseEnterAsTab(Me, True)
                Case Keys.F5
                    btnFilter_Click(sender, Nothing)
                Case Keys.F11
                    HotKeyF11(Me, tdbg)
            End Select
        End If
    End Sub

    Private Sub LoadTDBGrid(Optional ByVal FlagAdd As Boolean = False, Optional ByVal sKey As String = "")
        If FlagAdd Then
            ' Thêm mới thì gán sFind ="" và gán FilterText =""
            ResetFilter(tdbg, sFilter, bRefreshFilter)
            sFind = ""
        End If
        Dim sSQL As String = SQLStoreD02P3074()
        dtGrid = ReturnDataTable(sSQL)
        'Cách mới theo chuẩn: Tìm kiếm và Liệt kê tất cả luôn luôn sáng Khi(dt.Rows.Count > 0)
        gbEnabledUseFind = dtGrid.Rows.Count > 0
        LoadDataSource(tdbg, dtGrid, gbUnicode)
        Lemon3.Functions.CommonGrid.FormatColorGrid(tdbg)
        ReLoadTDBGrid()
    End Sub

    Dim sFind As String = ""
    Private Sub ReLoadTDBGrid()
        Dim strFind As String = sFind
        If sFilter.ToString.Equals("") = False And strFind.Equals("") = False Then strFind &= " And "
        strFind &= sFilter.ToString
        If strFind <> "" Then strFind &= " and "
        strFind &= " IsNotPrint =0 "
        dtGrid.DefaultView.RowFilter = strFind
        ResetGrid()
    End Sub

    Private Sub ResetGrid()
        CheckMenu(Me.Name, Nothing, tdbg.RowCount, gbEnabledUseFind, False, ContextMenuStrip1)
        'FooterTotalGrid(tdbg, COL_XXXX)
        'FooterSumNew(tdbg, COL_ConvertedAmount, COL_ConvertedQuantity)
    End Sub

    Dim sFilter As New System.Text.StringBuilder()
    'Dim sFilterServer As New System.Text.StringBuilder()
    Dim bRefreshFilter As Boolean = False
    Private Sub tdbg_FilterChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles tdbg.FilterChange
        Try
            If (dtGrid Is Nothing) Then Exit Sub
            If bRefreshFilter Then Exit Sub
            FilterChangeGrid(tdbg, sFilter) 'Nếu có Lọc khi In
            ReLoadTDBGrid()
        Catch ex As Exception
            'Update 11/05/2011: Tạm thời có lỗi thì bỏ qua không hiện message
            WriteLogFile(ex.Message) 'Ghi file log TH nhập số >MaxInt cột Byte
        End Try
    End Sub

    Private Sub tdbg_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tdbg.KeyPress
        If tdbg.Columns(tdbg.Col).ValueItems.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.CheckBox Then
            e.Handled = CheckKeyPress(e.KeyChar)
        ElseIf tdbg.Splits(tdbg.SplitIndex).DisplayColumns(tdbg.Col).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Far Then
            e.Handled = CheckKeyPress(e.KeyChar, EnumKey.NumberDot)
        End If
    End Sub

    'Lưu ý: gọi hàm ResetFilter(tdbg, sFilter, bRefreshFilter) tại btnFilter_Click và tsbListAll_Click
    'Bổ sung vào đầu sự kiện tdbg_DoubleClick(nếu có) câu lệnh If tdbg.RowCount <= 0 OrElse tdbg.FilterActive Then Exit Sub

    Private Function AllowFilter() As Boolean
        If tdbcDivisionID.Text.Trim = "" Then
            D99C0008.MsgNotYetChoose(lblDivisionID.Text)
            tdbcDivisionID.Focus()
            Return False
        End If

        If Not CheckValidPeriodFromTo(tdbcPeriodFrom, tdbcPeriodTo) Then Return False

        If tdbcReportType.Text.Trim = "" Then
            D99C0008.MsgNotYetChoose(lblReportType.Text)
            tdbcReportType.Focus()
            Return False
        End If
        If tdbcReportCode.Text.Trim = "" Then
            D99C0008.MsgNotYetChoose(lblReportCode.Text)
            tdbcReportCode.Focus()
            Return False
        End If
        Return True
    End Function

    Private Sub SetBackColorObligatory()
        tdbcDivisionID.EditorBackColor = COLOR_BACKCOLOROBLIGATORY
        tdbcPeriodFrom.EditorBackColor = COLOR_BACKCOLOROBLIGATORY
        tdbcPeriodTo.EditorBackColor = COLOR_BACKCOLOROBLIGATORY
        tdbcReportType.EditorBackColor = COLOR_BACKCOLOROBLIGATORY
        tdbcReportCode.EditorBackColor = COLOR_BACKCOLOROBLIGATORY
    End Sub



    Private Sub btnFilter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFilter.Click
        btnFilter.Focus()
        If btnFilter.Focused = False Then Exit Sub
        If Not AllowFilter() Then Exit Sub
        Me.Cursor = Cursors.WaitCursor
        dtCol = ReturnDataTable(SQLStoreD02P3073)
        LoadCaptionAndColumns()
        LoadTDBGrid(True)
        Me.Cursor = Cursors.Default
    End Sub

#Region "Events tdbcDivisionID with txtDivisionName"

    Private Sub tdbcDivisionID_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tdbcDivisionID.SelectedValueChanged
        If tdbcDivisionID.SelectedValue Is Nothing Then
            txtDivisionName.Text = ""
        Else
            txtDivisionName.Text = tdbcDivisionID.Columns(1).Value.ToString
        End If
        tdbcPeriodFrom.Text = ""
        'LoadCboPeriodReport(tdbcPeriodFrom, tdbcPeriodTo, D90, tdbcDivisionID.Text)
        LoadCboPeriodReport(tdbcPeriodFrom, tdbcPeriodTo, dtPeriod, tdbcDivisionID.Text)
        LoadDataSource(tdbcReportType, SQLStoreD02P3071, gbUnicode)
        If tdbcPeriodTo.DataSource IsNot Nothing Then
            tdbcPeriodTo.SelectedIndex = 0
            Dim str As String = ReturnDataTable(LoadPeriodMin(tdbcDivisionID.Text)).Rows(0)(0).ToString
            Dim sWhere As String = (IIf(str.Length = 2, str, "0" + str).ToString + "/" + ReturnValueC1Combo(tdbcPeriodTo, "Year")).ToString

            tdbcPeriodFrom.Text = sWhere
        End If
    End Sub
    Private Sub tdbcDivisionID_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tdbcDivisionID.LostFocus
        If tdbcDivisionID.FindStringExact(tdbcDivisionID.Text) = -1 Then
            tdbcDivisionID.Text = ""
            tdbcReportType.Text = ""
            tdbcReportCode.Text = ""
        End If
    End Sub

    Private Function LoadPeriodMin(ByVal sDivision As String) As String
        Dim sSQL As String = ""
        If sDivision <> "%" Then
            sSQL = "SELECT MIN(TranMonth) FROM D02T9999 WITH(NOLOCK) WHERE DivisionID=" & SQLString(sDivision) & " and TRANYEAR=" & ReturnValueC1Combo(tdbcPeriodTo, "Year")
        Else
            sSQL = "SELECT MIN(TranMonth) FROM D02T9999 WITH(NOLOCK) WHERE  TRANYEAR=" & ReturnValueC1Combo(tdbcPeriodTo, "Year")
        End If


        Return sSQL
    End Function
#End Region

    '#---------------------------------------------------------------------------------------------------
    '# Title: SQLStoreD02P3071
    '# Created User: KIM LONG
    '# Created Date: 21/11/2016 12:05:47
    '#---------------------------------------------------------------------------------------------------
    Private Function SQLStoreD02P3071() As String
        Dim sSQL As String = ""
        sSQL &= ("-- Combo loai bao cao" & vbCrLf)
        sSQL &= "Exec D02P3071 "
        sSQL &= SQLString(ReturnValueC1Combo(tdbcDivisionID)) & COMMA 'DivisionID, varchar[50], NOT NULL
        sSQL &= SQLString("02") & COMMA 'ModuleID, varchar[50], NOT NULL
        sSQL &= SQLNumber(gbUnicode) & COMMA 'CodeTable, tinyint, NOT NULL
        sSQL &= SQLString(gsLanguage) 'Language, varchar[2], NOT NULL
        Return sSQL
    End Function

    '#---------------------------------------------------------------------------------------------------
    '# Title: SQLStoreD02P3072
    '# Created User: KIM LONG
    '# Created Date: 21/11/2016 02:39:44
    '#---------------------------------------------------------------------------------------------------
    Private Function SQLStoreD02P3072() As String
        Dim sSQL As String = ""
        sSQL &= ("-- --Do nguon combo bao cao" & vbCrlf)
        sSQL &= "Exec D02P3072 "
        sSQL &= SQLString(ReturnValueC1Combo(tdbcDivisionID)) & COMMA 'DivisionID, varchar[50], NOT NULL
        sSQL &= SQLString(ReturnValueC1Combo(tdbcReportType)) & COMMA 'ReportType, varchar[50], NOT NULL
        sSQL &= SQLNumber(gbUnicode) & COMMA 'CodeTable, tinyint, NOT NULL
        sSQL &= SQLString(gsLanguage) 'Language, varchar[2], NOT NULL
        Return sSQL
    End Function

    '#---------------------------------------------------------------------------------------------------
    '# Title: SQLStoreD02P3073
    '# Created User: KIM LONG
    '# Created Date: 21/11/2016 01:17:54
    '#---------------------------------------------------------------------------------------------------
    Private Function SQLStoreD02P3073() As String
        Dim sSQL As String = ""
        sSQL &= ("-- --Tao cot dong cho luoi" & vbCrlf)
        sSQL &= "Exec D02P3073 "
        sSQL &= SQLString(ReturnValueC1Combo(tdbcReportType)) & COMMA 'ReportType, varchar[50], NOT NULL
        sSQL &= SQLString(ReturnValueC1Combo(tdbcReportCode)) & COMMA 'ReportCode, varchar[50], NOT NULL
        sSQL &= SQLNumber(0) & COMMA 'TypePivot, tinyint, NOT NULL
        sSQL &= SQLNumber(gbUnicode) & COMMA 'CodeTable, tinyint, NOT NULL
        sSQL &= SQLString(gsLanguage) 'Language, varchar[2], NOT NULL
        Return sSQL
    End Function

    '#---------------------------------------------------------------------------------------------------
    '# Title: SQLStoreD02P3074
    '# Created User: KIM LONG
    '# Created Date: 21/11/2016 01:19:36
    '#---------------------------------------------------------------------------------------------------
    Private Function SQLStoreD02P3074() As String
        Dim sSQL As String = ""
        sSQL &= ("-- --Do du lieu cho luoi" & vbCrlf)
        sSQL &= "Exec D02P3074 "
        sSQL &= SQLString(ReturnValueC1Combo(tdbcDivisionID)) & COMMA 'DivisionID, varchar[50], NOT NULL
        sSQL &= SQLString(ReturnValueC1Combo(tdbcReportType)) & COMMA 'ReportType, varchar[50], NOT NULL
        sSQL &= SQLString(ReturnValueC1Combo(tdbcReportCode)) & COMMA 'ReportCode, varchar[50], NOT NULL
        sSQL &= SQLString(ReturnValueC1Combo(tdbcPeriodFrom)) & COMMA 'PeriodFrom, varchar[8], NOT NULL
        sSQL &= SQLString(ReturnValueC1Combo(tdbcPeriodTo)) & COMMA 'PeriodTo, varchar[8], NOT NULL
        sSQL &= SQLString(gsUserID) & COMMA 'UserID, varchar[50], NOT NULL
        sSQL &= SQLString(My.Computer.Name) & COMMA 'HostID, varchar[50], NOT NULL
        sSQL &= SQLNumber(gbUnicode) & COMMA 'CodeTable, tinyint, NOT NULL
        sSQL &= SQLString(gsLanguage) 'Language, varchar[2], NOT NULL
        Return sSQL
    End Function

    Dim iIndexCol As Integer = 0
    Dim arr() As FormatColumn
    Private Sub LoadCaptionAndColumns()
        'If tdbg.Splits.Count = 1 Then
        '    tdbg.InsertHorizontalSplit(1)
        'End If
        While tdbg.Columns.Count > 2
            tdbg.Columns.RemoveAt(tdbg.Columns.Count - 1)
        End While
        'For i As Integer = 0 To tdbg.Columns.Count - 1
        '    tdbg.Splits(1).DisplayColumns(i).Visible = False
        'Next
        If dtCol.Rows.Count > 0 Then
            arr = Nothing
            'Add cột động vào lưới
            For i As Integer = 0 To dtCol.Rows.Count - 1
                AddColumns(i, dtCol)
            Next
            'Format các cột động
            If arr IsNot Nothing AndAlso arr.Length > 0 Then
                InputNumber(tdbg, arr)
            End If
            ResetFooterGrid(tdbg, 0, tdbg.Splits.Count - 1)
        End If
        tdbg.RefreshCol()
        tdbg.Update()
    End Sub

    Private Sub AddColumns(ByVal i As Integer, ByVal dtCol As DataTable)
        Dim sField As String = dtCol.Rows(i).Item("Column").ToString
        Dim dc As New C1.Win.C1TrueDBGrid.C1DataColumn
        dc.Caption = dtCol.Rows(i).Item("Caption").ToString
        dc.DataField = sField
        iIndexCol = 2
        tdbg.Columns.Insert(iIndexCol + i, dc)
        Select Case L3String(dtCol.Rows(i).Item("DataType"))
            Case "N"
                AddDecimalColumns(arr, sField, dtCol.Rows(i).Item("DataFormat").ToString, 28, 8, True)
                'AddDecimalColumns(arr, sField, "N2", 28, 8, True)
                tdbg.Splits(0).DisplayColumns(sField).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Far
            Case "D"
                tdbg.Splits(0).DisplayColumns(sField).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
            Case "S"
                tdbg.Splits(0).DisplayColumns(sField).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Near
        End Select

        'AddDecimalColumns(arr, sField, DxxFormat.DecimalPlaces, 28, 8)
        'tdbg.Splits(0).DisplayColumns(sField).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Far

        tdbg.Splits(0).DisplayColumns(sField).HeadingStyle.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Center
        tdbg.Splits(0).DisplayColumns(sField).HeadingStyle.Font = FontUnicode(gbUnicode)
        tdbg.Splits(0).DisplayColumns(sField).Style.Font = FontUnicode(gbUnicode)
        tdbg.Splits(0).DisplayColumns(sField).Width = L3Int(dtCol.Rows(i).Item("Length"))
        'If sField <> "Format" And sField <> "StyleColor" Then
        tdbg.Splits(0).DisplayColumns(sField).Visible = True
        tdbg.Splits(0).HScrollBar.Style = C1.Win.C1TrueDBGrid.ScrollBarStyleEnum.Always
        tdbg.Splits(0).RecordSelectors = False
        tdbg.Splits(0).AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.IndividualRows
        'End If
    End Sub

    Dim dtCaptionCols As DataTable
    Private Sub mnsExportToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnsExportToExcel.Click
        'Lưới không có nút Hiển thị
        'Nếu lưới không có Group thì mở dòng code If dtCaptionCols Is Nothing Then
        'và truyền đối số cuối cùng là False vào hàm AddColVisible
        'If dtCaptionCols Is Nothing OrElse dtCaptionCols.Rows.Count < 1 Then
        '    'Dim arrColObligatory() As Integer = {}
        '    Dim Arr As New ArrayList
        '    AddColVisible(tdbg, SPLIT0, Arr, , , True, gbUnicode)
        '    'Tạo tableCaption: đưa tất cả các cột trên lưới có Visible = True vào table 
        '    dtCaptionCols = CreateTableForExcelOnly(tdbg, Arr)
        'End If
        'Form trong DLL
        ''CallShowD99F2222(Me, ResetTableByGrid(usrOption, dtCaptionCols.DefaultView.ToTable), dtFind, gsGroupColumns)'Nếu có sử dụng F12 cũ D09U1111

        Dim dtMaster As New DataTable

        Dim dr() As DataRow = dtGrid.Select("IsNotPrint=0")
        Dim dtFilter As New DataTable
        dtFilter = dtGrid.Clone
        For i As Integer = 0 To dr.Length - 1
            dtFilter.ImportRow(dr(i))
        Next
        CreateTableCaption()
        AddDataRowTable(dtMaster, "Tên công ty", "DivisionName", L3String(dtGrid.Rows(0)("DivisionName")), 0)
        AddDataRowTable(dtMaster, "Địa chỉ công ty", "DivisionAddress", L3String(dtGrid.Rows(0)("DivisionAddress")), 0)
        AddDataRowTable(dtMaster, "Điện thoại công ty", "DivisionPhone", L3String(dtGrid.Rows(0)("DivisionPhone")), 0)
        AddDataRowTable(dtMaster, "Fax công ty", "DivisionFax", L3String(dtGrid.Rows(0)("DivisionFax")), 0)
        AddDataRowTable(dtMaster, "Tên kế toán trưởng", "ChiefAccountantName", L3String(dtGrid.Rows(0)("ChiefAccountantName")), 0)
        AddDataRowTable(dtMaster, "Tên giám đốc", "DirectorName", L3String(dtGrid.Rows(0)("DirectorName")), 0)
        AddDataRowTable(dtMaster, "Thông tin thời gian kỳ kế toán tiếng Việt", "VTimeCaption1", L3String(dtGrid.Rows(0)("VTimeCaption1")), 0)
        AddDataRowTable(dtMaster, "Thông tin thời gian kỳ kế toán tiếng Anh", "ETimeCaption1", L3String(dtGrid.Rows(0)("ETimeCaption1")), 0)
        AddDataRowTable(dtMaster, "Thông tin thời gian tháng thực tế tiếng Việt", "VtimeCaption2", L3String(dtGrid.Rows(0)("VtimeCaption2")), 0)
        AddDataRowTable(dtMaster, "Thông tin thời gian tháng thực tế tiếng Anh", "EtimeCaption2", L3String(dtGrid.Rows(0)("EtimeCaption2")), 0)
        CallShowD99F2222(Me, dtCaptionCols, dtFilter, gsGroupColumns, dtMaster)
    End Sub
    Private Sub CreateTableCaption()
        Dim Arr As New ArrayList
        For i As Integer = 0 To tdbg.Splits.Count - 1
            If tdbg.Splits(i).SplitSize = 0 Then Continue For
            If tdbg.Splits(i).SplitSize = 1 And tdbg.Splits(i).SplitSizeMode = C1.Win.C1TrueDBGrid.SizeModeEnum.Exact Then Continue For
            AddColVisible(tdbg, i, Arr, , False, False, gbUnicode)
        Next
        dtCaptionCols = CreateTableForExcelOnly(tdbg, Arr)
    End Sub


    Private Sub mnsPrint_Click(sender As Object, e As EventArgs) Handles mnsPrint.Click
        Me.Cursor = Cursors.WaitCursor
        Print(Me)
        Me.Cursor = Cursors.Default
    End Sub

    Dim report As D99C2003
    Private Sub Print(ByVal form As Form, Optional ByVal sReportTypeID As String = "D02F3070", Optional ByVal ModuleID As String = "02")
        'Dim sReportName As String = tdbcReportCode.Columns("ReportID").Value.ToString()
        'Dim sReportPath As String = ""
        'Dim sReportTitle As String = ""
        'Dim sCustomReport As String = ""
        'Dim sReportID As String = ""

        'Dim file As String = D99D0541.GetReportPathNew(ModuleID, sReportTypeID, sReportName, sCustomReport, sReportPath, sReportTitle)
        'If sReportName = "" Then Exit Sub
        'form.Cursor = Cursors.WaitCursor
        'Dim sSQL As String = ""

        'Try
        '    Select Case file.ToLower
        '        Case "rpt"
        '            If Not AllowNewD99C2003(report, Me) Then form.Cursor = Cursors.Default : Exit Sub
        '            '************************************
        '            Dim conn As New SqlConnection(gsConnectionString)
        '            Dim sReportCaption As String = ""
        '            Dim sSubReportName As String = ""
        '            Dim sSQLSub As String = ""

        '            sReportTitle = "" 'Thêm biến
        '            sCustomReport = ""

        '            sReportCaption = rL3("Truy_van_thuyet_minh_tang_giam_tai_san") & " - " & sReportName

        '            UnicodeSubReport(sSubReportName, sSQLSub, tdbcDivisionID.SelectedValue, True)
        '            sSQL = SQLStoreD02P3074()
        '            With report
        '                .OpenConnection(conn)
        '                .AddSub(sSQLSub, sSubReportName & ".rpt")
        '                .AddMain(sSQL)
        '                .PrintReport(sReportPath, sReportCaption)
        '            End With
        '        Case Else
        '            sSQL = SQLStoreD02P3074()
        '            D99D0541.PrintOfficeType(sReportTypeID, sReportName, sReportPath, file, sSQL)
        '    End Select
        'Catch ex As Exception
        '    form.Cursor = Cursors.Default
        'End Try
        Me.Cursor = Cursors.WaitCursor
        'Dim report As New D99C1003
        'Đưa vể đầu tiên hàm In trước khi gọi AllowPrint()
        If Not AllowNewD99C2003(report, Me) Then Exit Sub
        '************************************
        Dim conn As New SqlConnection(gsConnectionString)
        Dim sReportName As String = tdbcReportCode.Columns("ReportID").Value.ToString()
        Dim sSubReportName As String = ""
        Dim sReportCaption As String = ""
        Dim sPathReport As String = ""
        Dim sSQL As String = ""
        Dim sSQLSub As String = ""
        Dim sReportPath As String = ""
        Dim sReportTitle As String = ""
        Dim sCustomReport As String = ""
        If sReportName <> "" Then
            sReportCaption = rL3("Truy_van_thuyet_minh_tang_giam_tai_san") & " - " & sReportName
            sPathReport = UnicodeGetReportPath(gbUnicode, D02Options.ReportLanguage, "") & sReportName & ".rpt"
            ' sSQL = "Select * From D02V0100"
            sSQLSub = "Select Top 1 * From D91T0025 WITH(NOLOCK)"
            UnicodeSubReport(sSubReportName, sSQLSub, , gbUnicode)

            With report
                .OpenConnection(conn)
                .AddSub(sSQLSub, sSubReportName & ".rpt")
                .AddMain(SQLStoreD02P3074)
                .PrintReport(sPathReport, sReportCaption)
            End With
        Else
            Dim file As String = D99D0541.GetReportPathNew(ModuleID, sReportTypeID, sReportName, sCustomReport, sReportPath, sReportTitle)
            If sReportName = "" Then Exit Sub
            form.Cursor = Cursors.WaitCursor
            sSQL = ""

            Try
                Select Case file.ToLower
                    Case "rpt"
                        If Not AllowNewD99C2003(report, Me) Then form.Cursor = Cursors.Default : Exit Sub
                        '************************************
                        'Dim conn As New SqlConnection(gsConnectionString)
                        sReportCaption = ""
                        sSubReportName = ""
                        sSQLSub = ""

                        sReportTitle = "" 'Thêm biến
                        sCustomReport = ""

                        sReportCaption = rL3("Truy_van_thuyet_minh_tang_giam_tai_san") & " - " & sReportName

                        UnicodeSubReport(sSubReportName, sSQLSub, tdbcDivisionID.SelectedValue, True)
                        sSQL = SQLStoreD02P3074()
                        With report
                            .OpenConnection(conn)
                            .AddSub(sSQLSub, sSubReportName & ".rpt")
                            .AddMain(sSQL)
                            .PrintReport(sReportPath, sReportCaption)
                        End With
                    Case Else
                        sSQL = SQLStoreD02P3074()
                        D99D0541.PrintOfficeType(sReportTypeID, sReportName, sReportPath, file, sSQL)
                End Select
            Catch ex As Exception
                form.Cursor = Cursors.Default
            End Try
        End If
       
        Me.Cursor = Cursors.Default


    End Sub

#Region "Events tdbcReportType load tdbcReportCode"

    Private Sub tdbcReportType_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tdbcReportType.SelectedValueChanged
        If tdbcReportType.SelectedValue Is Nothing OrElse tdbcReportType.Text = "" Then
            LoadtdbcReportCode("-1")
            tdbcReportCode.Text = ""
            Exit Sub
        End If
        LoadtdbcReportCode(tdbcReportType.SelectedValue.ToString())
        tdbcReportCode.Text = ""
        tdbcReportCode.Enabled = tdbcReportType.Text <> ""
    End Sub

    Private Sub tdbcReportType_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tdbcReportType.LostFocus
        If tdbcReportType.FindStringExact(tdbcReportType.Text) = -1 Then
            tdbcReportType.Text = ""
            tdbcReportCode.Text = ""
            Exit Sub
        End If
    End Sub

#End Region

#Region "Events tdbcPeriodFrom"

    Private Sub tdbcPeriodFrom_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tdbcPeriodFrom.LostFocus
        If tdbcPeriodFrom.FindStringExact(tdbcPeriodFrom.Text) = -1 Then tdbcPeriodFrom.Text = ""
    End Sub

#End Region

#Region "Events tdbcPeriodTo"

    Private Sub tdbcPeriodTo_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tdbcPeriodTo.LostFocus
        If tdbcPeriodTo.FindStringExact(tdbcPeriodTo.Text) = -1 Then tdbcPeriodTo.Text = ""
    End Sub

#End Region

#Region "Events tdbcReportCode with txtReportName1"

    Private Sub tdbcReportCode_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tdbcReportCode.SelectedValueChanged
        If tdbcReportCode.SelectedValue Is Nothing Then
            txtReportName1.Text = ""
        Else
            txtReportName1.Text = tdbcReportCode.Columns(1).Value.ToString
        End If
    End Sub

    Private Sub tdbcReportCode_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tdbcReportCode.LostFocus
        If tdbcReportCode.FindStringExact(tdbcReportCode.Text) = -1 Then
            tdbcReportCode.Text = ""
        End If
    End Sub

#End Region

End Class