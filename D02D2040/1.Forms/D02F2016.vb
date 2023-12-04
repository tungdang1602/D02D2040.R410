Public Class D02F2016

#Region "Const of tdbg - Total of Columns: 15"
    Private Const COL_VoucherNo As Integer = 0         ' Số phiếu
    Private Const COL_VoucherDate As Integer = 1       ' Ngày phiếu
    Private Const COL_VoucherDesc As Integer = 2       ' Diễn giải
    Private Const COL_AssetID As Integer = 3           ' Mã tài sản
    Private Const COL_AssetName As Integer = 4         ' Tên tài sản
    Private Const COL_FromDivisionID As Integer = 5    ' Đơn vị gốc
    Private Const COL_FromDivisionName As Integer = 6  ' Tên đơn vị gốc
    Private Const COL_ToDivisionID As Integer = 7      ' Đơn vị chuyển đến
    Private Const COL_ToDivisionName As Integer = 8    ' Tên đơn vị chuyển đến
    Private Const COL_CreateUserID As Integer = 9      ' Người tạo
    Private Const COL_CreateDate As Integer = 10       ' Ngày tạo
    Private Const COL_LastModifyUserID As Integer = 11 ' Người cập nhật cuối cùng
    Private Const COL_LastModifyDate As Integer = 12   ' Ngày cập nhật cuối cùng
    Private Const COL_VoucherID As Integer = 13        ' VoucherID
    Private Const COL_Period As Integer = 14           ' Period
#End Region



    Dim dtObjectID, dtGrid As DataTable

    Private Sub D02F2016_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter
                UseEnterAsTab(Me)
            Case Keys.F5
                btnFilter.PerformClick()
            Case Keys.F11
                HotKeyF11(Me, tdbg)
        End Select
    End Sub

    Private Sub D02F2016_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadInfoGeneral()
        InputbyUnicode(Me, gbUnicode)
        CheckIdTextBox(txtStrVoucherNo, txtStrVoucherNo.MaxLength)
        gbEnabledUseFind = False
        LoadLanguage()
        InputDateInTrueDBGrid(tdbg, COL_VoucherDate)
        SetShortcutPopupMenuNew(Me, tbr1, ContextMenuStrip1)
        LoadTDBCombo()
        LoadDefault()
        ResetColorGrid(tdbg)
        SetBackColorObligatory()
        SetCheckMenu()
        SetResolutionForm(Me, ContextMenuStrip1)
    End Sub

    Private Sub LoadLanguage()
        '================================================================ 
        Me.Text = rl3("Truy_van_dieu_chuyen_don_vi_TSCD") & " - " & Me.Name & UnicodeCaption(gbUnicode) 'Truy vÊn ¢iÒu chuyÓn ¢¥n vÜ TSC˜
        '================================================================ 
        lblObjectTypeID.Text = rl3("Bo_phan_tiep_nhan") 'Bộ phận tiếp nhận
        lblStrVoucherNo.Text = rl3("So_phieu_co_chua") 'Số phiếu có chứa
        lblFromAssetID.Text = rl3("Ma_tai_san") 'Mã tài sản
        '================================================================ 
        btnFilter.Text = rl3("Loc") & " (F5)" 'Lọc
        '================================================================ 
        optDate.Text = rl3("Ngay") 'Ngày
        optPeriod.Text = rl3("Ky") 'Kỳ
        '================================================================ 
        tdbcToAssetID.Columns("AssetID").Caption = rl3("Ma") 'Mã
        tdbcToAssetID.Columns("AssetName").Caption = rl3("Ten") 'Tên
        tdbcFromAssetID.Columns("AssetID").Caption = rl3("Ma") 'Mã
        tdbcFromAssetID.Columns("AssetName").Caption = rl3("Ten") 'Tên
        tdbcToObjectID.Columns("ObjectID").Caption = rl3("Ma") 'Mã
        tdbcToObjectID.Columns("ObjectName").Caption = rl3("Ten") 'Tên
        tdbcFromObjectID.Columns("ObjectID").Caption = rl3("Ma") 'Mã
        tdbcFromObjectID.Columns("ObjectName").Caption = rl3("Ten") 'Tên
        tdbcObjectTypeID.Columns("ObjectTypeID").Caption = rl3("Ma") 'Mã
        tdbcObjectTypeID.Columns("ObjectTypeName").Caption = rl3("Ten") 'Tên
        '================================================================ 
        tdbg.Columns(COL_VoucherNo).Caption = rL3("So_phieu") 'Số phiếu
        tdbg.Columns(COL_VoucherDate).Caption = rL3("Ngay_phieu") 'Ngày phiếu
        tdbg.Columns(COL_VoucherDesc).Caption = rL3("Dien_giai") 'Diễn giải
        tdbg.Columns(COL_AssetID).Caption = rL3("Ma_tai_san") 'Mã tài sản
        tdbg.Columns(COL_AssetName).Caption = rL3("Ten_tai_san") 'Tên tài sản
        tdbg.Columns(COL_FromDivisionID).Caption = rL3("Don_vi_goc") 'Đơn vị gốc
        tdbg.Columns(COL_FromDivisionName).Caption = rL3("Ten_don_vi_goc") 'Tên đơn vị gốc
        tdbg.Columns(COL_ToDivisionID).Caption = rL3("Don_vi_chuyen_den") 'Đơn vị chuyển đến
        tdbg.Columns(COL_ToDivisionName).Caption = rL3("Ten_don_vi_chuyen_den") 'Tên đơn vị chuyển đến

    End Sub

    Private Sub LoadTDBCombo()
        Dim sSQL As String = ""
        Dim dtAssetID As DataTable
        'Load tdbcPeriodFrom, tdbcPeriodTo
        LoadCboPeriodReport(tdbcPeriodFrom, tdbcPeriodTo, D02, gsDivisionID)

        'Load tdbcObjectTypeID
        LoadObjectTypeIDAll(tdbcObjectTypeID, gbUnicode)

        'Load tdbcFromObjectID, tdbcToObjectID
        Using obj As Lemon3.Data.LoadData.ObjectID = New Lemon3.Data.LoadData.ObjectID
            obj.LoadObjectID(tdbcFromObjectID, dtObjectID)
        End Using
        tdbcObjectTypeID.Text = "%"
      
        'Load tdbcFromAssetID, tdbcToAssetID
        sSQL = "Select '%' As AssetID, " & AllName & " As AssetName, 0 As DisplayOrder " & vbCrLf
        sSQL &= "UNION ALL " & vbCrLf
        sSQL &= "Select AssetID, AssetName" & UnicodeJoin(gbUnicode) & " As AssetName, 1 As DisplayOrder " & vbCrLf
        sSQL &= "FROM D02T0001 WITH(NOLOCK) " & vbCrLf
        sSQL &= "WHERE DivisionID = " & SQLString(gsDivisionID)
        dtAssetID = ReturnDataTable(sSQL)
        LoadDataSource(tdbcFromAssetID, dtAssetID, gbUnicode)
        LoadDataSource(tdbcToAssetID, dtAssetID.DefaultView.ToTable, gbUnicode)

    End Sub

    Private Sub LoadtdbcObjectID(ByVal sObjectTypeID As String)
        Dim dt As DataTable

        If sObjectTypeID = "%" Then
            dt = dtObjectID.DefaultView.ToTable
            LoadDataSource(tdbcFromObjectID, dt, gbUnicode)
            LoadDataSource(tdbcToObjectID, dt.DefaultView.ToTable, gbUnicode)
        Else
            dt = ReturnTableFilter(dtObjectID, "ObjectTypeID = " & SQLString(sObjectTypeID), True)
            LoadDataSource(tdbcFromObjectID, dt, gbUnicode)
            LoadDataSource(tdbcToObjectID, dt.DefaultView.ToTable, gbUnicode)
        End If

    End Sub

    Private Sub LoadDefault()
        tdbcPeriodFrom.Text = giTranMonth.ToString("00") & "/" & giTranYear.ToString
        tdbcPeriodTo.Text = tdbcPeriodFrom.Text
        c1dateDateFrom.Value = Date.Today
        c1dateDateTo.Value = Date.Today
        tdbcFromAssetID.Text = "%"
        tdbcToAssetID.Text = "%"
    End Sub
    
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

#Region "Events tdbcObjectTypeID load tdbcFromObjectID"

    Private Sub tdbcObjectTypeID_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tdbcObjectTypeID.SelectedValueChanged
        If tdbcObjectTypeID.SelectedValue Is Nothing OrElse tdbcObjectTypeID.Text = "" Then
            LoadtdbcObjectID("%")
            tdbcFromObjectID.Text = ""
            Exit Sub
        End If
        LoadtdbcObjectID(tdbcObjectTypeID.SelectedValue.ToString())
        tdbcFromObjectID.Text = ""
    End Sub

    Private Sub tdbcObjectTypeID_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tdbcObjectTypeID.LostFocus
        If tdbcObjectTypeID.FindStringExact(tdbcObjectTypeID.Text) = -1 Then
            tdbcObjectTypeID.Text = "%"
            tdbcFromObjectID.Text = ""
            tdbcToObjectID.Text = ""
            Exit Sub
        End If
    End Sub

    Private Sub tdbcFromObjectID_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tdbcFromObjectID.LostFocus
        If tdbcFromObjectID.FindStringExact(tdbcFromObjectID.Text) = -1 Then tdbcFromObjectID.Text = ""
    End Sub

#End Region

#Region "Events tdbcToObjectID"

    Private Sub tdbcToObjectID_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tdbcToObjectID.LostFocus
        If tdbcToObjectID.FindStringExact(tdbcToObjectID.Text) = -1 Then tdbcToObjectID.Text = ""
    End Sub

#End Region

#Region "Events tdbcFromAssetID"

    Private Sub tdbcFromAssetID_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tdbcFromAssetID.LostFocus
        If tdbcFromAssetID.FindStringExact(tdbcFromAssetID.Text) = -1 Then tdbcFromAssetID.Text = "%"
    End Sub

#End Region

#Region "Events tdbcToAssetID"

    Private Sub tdbcToAssetID_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tdbcToAssetID.LostFocus
        If tdbcToAssetID.FindStringExact(tdbcToAssetID.Text) = -1 Then tdbcToAssetID.Text = "%"
    End Sub

#End Region

    Private Sub SetBackColorObligatory()
        tdbcPeriodFrom.EditorBackColor = COLOR_BACKCOLOROBLIGATORY
        tdbcPeriodTo.EditorBackColor = COLOR_BACKCOLOROBLIGATORY
        c1dateDateFrom.BackColor = COLOR_BACKCOLOROBLIGATORY
        c1dateDateTo.BackColor = COLOR_BACKCOLOROBLIGATORY
    End Sub

    Private Sub optPeriod_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optPeriod.CheckedChanged, optDate.CheckedChanged
        If optPeriod.Checked Then
            UnReadOnlyControl(True, tdbcPeriodFrom, tdbcPeriodTo)
            ReadOnlyControl(c1dateDateFrom, c1dateDateTo)
        ElseIf optDate.Checked Then
            ReadOnlyControl(tdbcPeriodFrom, tdbcPeriodTo)
            UnReadOnlyControl(True, c1dateDateFrom, c1dateDateTo)
        End If
    End Sub

    Private Sub btnFilter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFilter.Click
        btnFilter.Focus()
        If btnFilter.Focused = False Then Exit Sub
        If Not AllowFilter() Then Exit Sub
        Me.Cursor = Cursors.WaitCursor

        LoadTDBGrid(True)
        Me.Cursor = Cursors.Default
    End Sub

    Private Function AllowFilter() As Boolean
        If optPeriod.Checked Then
            If CheckValidPeriodFromTo(tdbcPeriodFrom, tdbcPeriodTo) = False Then Return False
        ElseIf optDate.Checked Then
            If CheckValidDateFromTo(c1dateDateFrom, c1dateDateTo) = False Then Return False
        End If

        Return True
    End Function

    Private Sub LoadTDBGrid(Optional ByVal FlagAdd As Boolean = False, Optional ByVal sKeyID As String = "")
        If FlagAdd Then
            '' Thêm mới thì gán sFind ="" và gán FilterText =’’
            ResetFilter(tdbg, sFilter, bRefreshFilter) ', sFilterServer)
            sFind = ""
        End If

        Dim sSQL As String = SQLStoreD02P2018()
        dtGrid = ReturnDataTable(sSQL)
        LoadDataSource(tdbg, dtGrid, gbUnicode)
        'Cách mới theo chuẩn: Tìm kiếm và Liệt kê tất cả luôn luôn sáng khi(dt.Rows.Count > 0)
        gbEnabledUseFind = dtGrid.Rows.Count > 0
        ReLoadTDBGrid()
        If sKeyID <> "" Then
            tdbg.Row = findrowInGrid(tdbg, sKeyID, "VoucherID")
            If Not tdbg.Focused Then tdbg.Focus() 'Nếu con trỏ chưa đứng trên lưới thì Focus về lưới
        End If
    End Sub

    Private Sub ReLoadTDBGrid()
        Dim strFind As String = sFind
        If sFilter.ToString.Equals("") = False And strFind.Equals("") = False Then strFind &= " And "
        strFind &= sFilter.ToString
        dtGrid.DefaultView.RowFilter = strFind
        ResetGrid()
    End Sub

    Private Sub ResetGrid()
        SetCheckMenu()
        FooterTotalGrid(tdbg, COL_VoucherNo)
    End Sub

    Private Sub SetCheckMenu()
        CheckMenu(Me.Name, tbr1, tdbg.RowCount, gbEnabledUseFind, True, ContextMenuStrip1)
    End Sub

    '#---------------------------------------------------------------------------------------------------
    '# Title: SQLStoreD02P2018
    '# Created User: NGOCTHOAI
    '# Created Date: 10/04/2017 09:26:22
    '#---------------------------------------------------------------------------------------------------
    Private Function SQLStoreD02P2018() As String
        Dim sSQL As String = ""
        sSQL &= ("-- Do nguon cho luoi " & vbCrLf)
        sSQL &= "Exec D02P2018 "
        sSQL &= SQLString(gsDivisionID) & COMMA 'DivisionID, varchar[20], NOT NULL
        sSQL &= SQLString(My.Computer.Name) & COMMA 'HostID, varchar[20], NOT NULL
        sSQL &= SQLString(Me.Name) & COMMA 'FormID, varchar[20], NOT NULL
        sSQL &= SQLNumber(IIf(optPeriod.Checked = True, 0, 1)) & COMMA 'IsTime, int, NOT NULL
        sSQL &= SQLNumber(ReturnValueC1Combo(tdbcPeriodFrom, "TranMonth")) & COMMA 'FromMonth, int, NOT NULL
        sSQL &= SQLNumber(ReturnValueC1Combo(tdbcPeriodFrom, "TranYear")) & COMMA 'FromYear, int, NOT NULL
        sSQL &= SQLNumber(ReturnValueC1Combo(tdbcPeriodTo, "TranMonth")) & COMMA 'ToMonth, int, NOT NULL
        sSQL &= SQLNumber(ReturnValueC1Combo(tdbcPeriodTo, "TranYear")) & COMMA 'ToYear, int, NOT NULL
        sSQL &= SQLDateSave(c1dateDateFrom.Value) & COMMA 'FromDate, datetime, NOT NULL
        sSQL &= SQLDateSave(c1dateDateTo.Value) & COMMA 'ToDate, datetime, NOT NULL
        sSQL &= SQLString(tdbcObjectTypeID.Text) & COMMA 'ObjectTypeID, varchar[20], NOT NULL
        sSQL &= SQLString(tdbcFromObjectID.Text) & COMMA 'FromObjectID, varchar[20], NOT NULL
        sSQL &= SQLString(tdbcToObjectID.Text) & COMMA 'ToObjectID, varchar[20], NOT NULL
        sSQL &= SQLString(tdbcFromAssetID.Text) & COMMA 'FromAsetID, varchar[20], NOT NULL
        sSQL &= SQLString(tdbcToAssetID.Text) & COMMA 'ToAssetID, varchar[20], NOT NULL
        sSQL &= SQLString(txtStrVoucherNo.Text) 'StrVoucherNo, varchar[8000], NOT NULL
        Return sSQL
    End Function


    Dim iHeight As Integer = 0 ' Lấy tọa độ Y của chuột click tới
    Private Sub tdbg_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tdbg.MouseClick
        iHeight = e.Location.Y
    End Sub

    Private Sub tdbg_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tdbg.DoubleClick
        If iHeight <= tdbg.Splits(0).ColumnCaptionHeight Then Exit Sub
        If tdbg.RowCount <= 0 OrElse tdbg.FilterActive Then Exit Sub
        Me.Cursor = Cursors.WaitCursor
        If tsbEdit.Enabled Then
            tsbEdit_Click(sender, Nothing)
        ElseIf tsbView.Enabled Then
            tsbView_Click(sender, Nothing)
        End If
        Me.Cursor = Cursors.Default
    End Sub


    Dim sFilter As New System.Text.StringBuilder()
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

    Private Sub tdbg_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tdbg.KeyDown
        Me.Cursor = Cursors.WaitCursor
        If e.KeyCode = Keys.Enter Then tdbg_DoubleClick(Nothing, Nothing)
        HotKeyCtrlVOnGrid(tdbg, e)
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub tdbg_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tdbg.KeyPress
        If tdbg.Columns(tdbg.Col).ValueItems.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.CheckBox Then
            e.Handled = CheckKeyPress(e.KeyChar)
        ElseIf tdbg.Splits(tdbg.SplitIndex).DisplayColumns(tdbg.Col).Style.HorizontalAlignment = C1.Win.C1TrueDBGrid.AlignHorzEnum.Far Then
            e.Handled = CheckKeyPress(e.KeyChar, EnumKey.NumberDot)
        End If
    End Sub


    Private Sub tdbg_RowColChange(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.RowColChangeEventArgs) Handles tdbg.RowColChange
  If e IsNot Nothing AndAlso e.LastRow = -1 Then Exit Sub
        SetCheckMenu()
    End Sub

    Private Sub tsbAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbAdd.Click, tsmAdd.Click, mnsAdd.Click
        If Not CheckVoucherDateInPeriodFormLoad() Then Exit Sub
        Dim bSaved As Boolean = False
        Dim sKeyID As String = ""
        Dim frm As New D02F2017
        With frm
            .sVoucherID = ""
            .FormState = EnumFormState.FormAdd
            .ShowDialog()
            bSaved = .bSaved
            sKeyID = .sVoucherID
            .Dispose()
        End With

        If bSaved Then
            LoadTDBGrid(True, sKeyID)
        End If
    End Sub

    Private Sub tsbView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbView.Click, tsmView.Click, mnsView.Click
        Dim frm As New D02F2017
        With frm
            .sVoucherID = tdbg.Columns(COL_VoucherID).Text
            .FormState = EnumFormState.FormView
            .ShowDialog()
            .Dispose()
        End With
    End Sub

    Private Sub tsbEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbEdit.Click, tsmEdit.Click, mnsEdit.Click
        If (giTranYear.ToString & giTranMonth.ToString("00")) <> tdbg.Columns(COL_Period).Text Then
            D99C0008.MsgL3(rL3("MSG000001"), L3MessageBoxIcon.Exclamation) 'Dữ liệu không thuộc kỳ này. Bạn không thể thay đổi được.
            Exit Sub
        End If

        If CheckStore(SQLStoreD02P2019(1)) = False Then Exit Sub

        Dim bSaved As Boolean = False
        Dim frm As New D02F2017
        With frm
            .sVoucherID = tdbg.Columns(COL_VoucherID).Text
            .FormState = EnumFormState.FormEdit
            .ShowDialog()
            bSaved = .bSaved
            .Dispose()
        End With
        If bSaved Then
            LoadTDBGrid()
        End If
    End Sub

    Private Sub tsbDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbDelete.Click, tsmDelete.Click, mnsDelete.Click
        If AskDelete() = Windows.Forms.DialogResult.No Then Exit Sub

        If (giTranYear.ToString & giTranMonth.ToString("00")) <> tdbg.Columns(COL_Period).Text Then
            D99C0008.MsgL3(rL3("MSG000001"), L3MessageBoxIcon.Exclamation) 'Dữ liệu không thuộc kỳ này. Bạn không thể thay đổi được.
            Exit Sub
        End If
        If CheckStore(SQLStoreD02P2019(2)) = False Then Exit Sub

        Dim bResult As Boolean = False

        bResult = ExecuteSQL(SQLStoreD02P2022)
        If bResult Then
            'DeleteVoucherNoD91T9111(tdbg.Columns(COL_VoucherID).Text, "D02T0016", "VoucherNum")
            DeleteGridEvent(tdbg, dtGrid, gbEnabledUseFind, , "VoucherID")
            ResetGrid()
            DeleteOK()
        Else
            DeleteNotOK()
        End If
    End Sub

    '#---------------------------------------------------------------------------------------------------
    '# Title: SQLStoreD02P2022
    '# Created User: NGOCTHOAI
    '# Created Date: 19/05/2017 03:40:27
    '#---------------------------------------------------------------------------------------------------
    Private Function SQLStoreD02P2022() As String
        Dim sSQL As String = ""
        sSQL &= ("-- Xoa du lieu " & vbCrlf)
        sSQL &= "Exec D02P2022 "
        sSQL &= SqlString(gsLanguage) & COMMA 'Language, varchar[20], NOT NULL
        sSQL &= SQLString(gsUserID) & COMMA 'UserID, varchar[20], NOT NULL
        sSQL &= SQLString(My.Computer.Name) & COMMA 'HostID, varchar[20], NOT NULL
        sSQL &= SQLString(Me.Name) & COMMA 'FormID, varchar[20], NOT NULL
        sSQL &= SQLString(gsDivisionID) & COMMA 'DivisionID, varchar[20], NOT NULL
        sSQL &= SQLNumber(giTranMonth) & COMMA 'TranMonth, int, NOT NULL
        sSQL &= SQLNumber(giTranYear) & COMMA 'TranYear, int, NOT NULL
        sSQL &= SQLString("") & COMMA 'DataTable, varchar[50], NOT NULL
        sSQL &= SQLNumber(2) & COMMA 'Mode, int, NOT NULL
        sSQL &= SQLString(tdbg.Columns(COL_VoucherID).Text) 'VoucherID, varchar[20], NOT NULL
        Return sSQL
    End Function



    '#---------------------------------------------------------------------------------------------------
    '# Title: SQLStoreD02P2019
    '# Created User: NGOCTHOAI
    '# Created Date: 19/05/2017 03:33:10
    '#---------------------------------------------------------------------------------------------------
    Private Function SQLStoreD02P2019(ByVal byMode As Byte) As String
        Dim sSQL As String = ""
        sSQL &= ("-- Kiem tra truoc khi sua/xoa " & vbCrLf)
        sSQL &= "Exec D02P2019 "
        sSQL &= SQLString(gsLanguage) & COMMA 'Language, varchar[20], NOT NULL
        sSQL &= SQLString(gsUserID) & COMMA 'UserID, varchar[20], NOT NULL
        sSQL &= SQLString(My.Computer.Name) & COMMA 'HostID, varchar[20], NOT NULL
        sSQL &= SQLNumber(byMode) & COMMA 'Mode, int, NOT NULL
        sSQL &= SQLString(Me.Name) & COMMA 'FormID, varchar[20], NOT NULL
        sSQL &= SQLString(tdbg.Columns(COL_VoucherID).Text) & COMMA 'Key01ID, varchar[20], NOT NULL
        sSQL &= SQLString(gsDivisionID) & COMMA 'Key02ID, varchar[20], NOT NULL
        sSQL &= SQLString(giTranMonth) & COMMA 'Key03ID, varchar[20], NOT NULL
        sSQL &= SQLString(giTranYear) & COMMA 'Key04ID, varchar[20], NOT NULL
        sSQL &= SQLString(tdbg.Columns(COL_ToDivisionID).Text) 'Key05ID, varchar[20], NOT NULL
        Return sSQL
    End Function


#Region "Active Find - List All (Client)"
    Private WithEvents Finder As New D99C1001
    Private sFind As String = ""

    Dim dtCaptionCols As DataTable

    'DLL sử dụng Properties
    Public WriteOnly Property strNewFind() As String
        Set(ByVal Value As String)
            sFind = Value
            ReLoadTDBGrid() 'Giống sự kiện Finder_FindClick
        End Set
    End Property

    '*****************************
    Private Sub tsbFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbFind.Click, tsmFind.Click, mnsFind.Click
        gbEnabledUseFind = True
        'Chuẩn hóa D09U1111 : Tìm kiếm dùng table caption có sẵn
        tdbg.UpdateData()
        'Những cột bắt buộc nhập
        Dim arrColObligatory() As Integer = {COL_VoucherNo, COL_AssetName, COL_ToDivisionName}
        Dim Arr As New ArrayList
        For i As Integer = 0 To tdbg.Splits.Count - 1
            AddColVisible(tdbg, i, Arr, arrColObligatory, False, False, gbUnicode)
        Next
        'Tạo tableCaption: đưa tất cả các cột trên lưới có Visible = True vào table 
        dtCaptionCols = CreateTableForExcelOnly(tdbg, Arr)

        'ShowFindDialogClient(Finder, dtCaptionCols, Me.Name, "0", gbUnicode)
        ShowFindDialogClient(Finder, dtCaptionCols, Me, "0", gbUnicode) ' Dùng DLL 
    End Sub

    'DLL không sử dụng sự kiện Finder_FindClick
    Private Sub Finder_FindClick(ByVal ResultWhereClause As Object) Handles Finder.FindClick
        If ResultWhereClause Is Nothing Or ResultWhereClause.ToString = "" Then Exit Sub
        sFind = ResultWhereClause.ToString()
        ReLoadTDBGrid()
    End Sub

    Private Sub tsbListAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbListAll.Click, tsmListAll.Click, mnsListAll.Click
        sFind = ""
        ResetFilter(tdbg, sFilter, bRefreshFilter)
        ReLoadTDBGrid()
    End Sub

#End Region

    Private Sub tsbSysInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbSysInfo.Click, tsmSysInfo.Click, mnsSysInfo.Click
        ShowSysInfoDialog(tdbg)
    End Sub

    Private Sub tsbClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbClose.Click
        Me.Close()
    End Sub
End Class