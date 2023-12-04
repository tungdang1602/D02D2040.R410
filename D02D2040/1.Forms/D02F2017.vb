Imports System
Public Class D02F2017

#Region "Const of tdbg - Total of Columns: 18"
    Private Const COL_IsCheck As Integer = 0            ' Chọn
    Private Const COL_DateChangeDivision As Integer = 1 ' Ngày điều chuyển
    Private Const COL_AssetID As Integer = 2            ' Mã tài sản
    Private Const COL_NewAssetID As Integer = 3         ' Mã tài sản mới
    Private Const COL_AssetName As Integer = 4          ' Tên tài sản
    Private Const COL_CurrentCost As Integer = 5        ' Nguyên giá
    Private Const COL_DepCurrentCost As Integer = 6     ' Giá trị đã khấu hao
    Private Const COL_RemainAmount As Integer = 7       ' Giá trị còn lại
    Private Const COL_ServiceLife As Integer = 8        ' Số kỳ khẩu hao
    Private Const COL_DepreciatedPeriod As Integer = 9  ' Số kỳ đã khấu hao
    Private Const COL_AssetAccountID As Integer = 10    ' TK Tài sản
    Private Const COL_DepAccountID As Integer = 11      ' TK Khấu hao
    Private Const COL_EquityAccountID As Integer = 12   ' Tài khoản nguồn vốn
    Private Const COL_VoucherTypeID As Integer = 13     ' VoucherTypeID
    Private Const COL_VoucherNo As Integer = 14         ' VoucherNo
    Private Const COL_VoucherDate As Integer = 15       ' VoucherDate
    Private Const COL_Notes As Integer = 16             ' Notes
    Private Const COL_ToDivisionID As Integer = 17      ' ToDivisionID
#End Region


    Dim dtObjectID, dtGrid As DataTable
    Dim sTableTmp As String = "[D02F2017_" & gsUserID & "_" & My.Computer.Name & "]"
    Private sOldVoucherNo As String 'Bi?n toàn c?c c?a form, luu l?i s? phi?u cu
    Private bEditVoucher As Boolean = False
    Private bEditVoucherNo As Boolean = False

    Private _bSaved As Boolean = False
    Public ReadOnly Property  bSaved() As Boolean
        Get
            Return _bSaved
        End Get
    End Property

    Private _sVoucherID As String = ""
    Public Property sVoucherID() As String
        Get
            Return _sVoucherID
        End Get
        Set(ByVal Value As String)
            _sVoucherID = Value
        End Set
    End Property

    Dim bLoadFormState As Boolean = False
    Private _FormState As EnumFormState = EnumFormState.FormAdd
    Public WriteOnly Property FormState() As EnumFormState
        Set(ByVal value As EnumFormState)
            bLoadFormState = True
            LoadInfoGeneral() ' hàm trong DxxD9940
            _FormState = value

            LoadTDBCombo()
            LoadTDBDropDown()
            tdbcToDivisionID.Tag = ""
            Select Case _FormState
                Case EnumFormState.FormAdd
                    LoadAddNew()
                Case EnumFormState.FormEdit
                    LoadTDBGrid()
                    LoadEdit()
                Case EnumFormState.FormView
                    LoadTDBGrid()
                    LoadEdit()
                    btnSave.Enabled = False
            End Select
        End Set
    End Property

    Private Sub D02F2017_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        PerformFormClosed()
    End Sub

    Private Sub D02F2017_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter
                UseEnterAsTab(Me)
            Case Keys.F5
                btnFilter.PerformClick()
            Case Keys.F11
                HotKeyF11(Me, tdbg)
        End Select
    End Sub


    Private Sub D02F2017_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If bLoadFormState = False Then FormState = _FormState
        InputbyUnicode(Me, gbUnicode)
        LoadLanguage()
        InputDateInTrueDBGrid(tdbg, COL_DateChangeDivision)
        ResetFooterGrid(tdbg, SPLIT0, SPLIT1)
        tdbg_LockedColumns()
        tdbg_NumberFormat()
        SetBackColorObligatory()
        SetResolutionForm(Me)
    End Sub

    Private Sub tdbg_LockedColumns()
        tdbg.Splits(SPLIT0).DisplayColumns(COL_AssetID).Style.BackColor = Color.FromArgb(COLOR_BACKCOLOR)
        tdbg.Splits(SPLIT0).DisplayColumns(COL_NewAssetID).Style.BackColor = Color.FromArgb(COLOR_BACKCOLOR)
        tdbg.Splits(SPLIT0).DisplayColumns(COL_AssetName).Style.BackColor = Color.FromArgb(COLOR_BACKCOLOR)
        tdbg.Splits(SPLIT0).DisplayColumns(COL_CurrentCost).Style.BackColor = Color.FromArgb(COLOR_BACKCOLOR)
        tdbg.Splits(SPLIT0).DisplayColumns(COL_DepCurrentCost).Style.BackColor = Color.FromArgb(COLOR_BACKCOLOR)
        tdbg.Splits(SPLIT0).DisplayColumns(COL_RemainAmount).Style.BackColor = Color.FromArgb(COLOR_BACKCOLOR)
        tdbg.Splits(SPLIT0).DisplayColumns(COL_ServiceLife).Style.BackColor = Color.FromArgb(COLOR_BACKCOLOR)
        tdbg.Splits(SPLIT0).DisplayColumns(COL_DepreciatedPeriod).Style.BackColor = Color.FromArgb(COLOR_BACKCOLOR)
        tdbg.Splits(SPLIT1).DisplayColumns(COL_AssetAccountID).Style.BackColor = Color.FromArgb(COLOR_BACKCOLOR)
        tdbg.Splits(SPLIT1).DisplayColumns(COL_DepAccountID).Style.BackColor = Color.FromArgb(COLOR_BACKCOLOR)
    End Sub

    Private Sub tdbg_NumberFormat()
        Dim arr() As FormatColumn = Nothing
        AddDecimalColumns(arr, tdbg.Columns(COL_CurrentCost).DataField, DxxFormat.D90_ConvertedDecimals, 28, 8)
        AddDecimalColumns(arr, tdbg.Columns(COL_DepCurrentCost).DataField, DxxFormat.D90_ConvertedDecimals, 28, 8)
        AddDecimalColumns(arr, tdbg.Columns(COL_RemainAmount).DataField, DxxFormat.D90_ConvertedDecimals, 28, 8)
        AddDecimalColumns(arr, tdbg.Columns(COL_ServiceLife).DataField, DxxFormat.DefaultNumber0, 28, 8)
        AddDecimalColumns(arr, tdbg.Columns(COL_DepreciatedPeriod).DataField, DxxFormat.DefaultNumber0, 28, 8)
        InputNumber(tdbg, arr)
    End Sub


    Private Sub LoadLanguage()
        '================================================================ 
        Me.Text = rl3("Dieu_chuyen_don_vi_TSCDF") & " - " & Me.Name & UnicodeCaption(gbUnicode) '˜iÒu chuyÓn ¢¥n vÜ TSC˜
        '================================================================ 
        lblPeriodFrom.Text = rl3("Ky_hinh_thanh_tu") 'Kỳ hình thành từ
        lblObjectTypeID.Text = rl3("Bo_phan_tiep_nhan") 'Bộ phận tiếp nhận
        lblEmployeeID.Text = rl3("Nguoi_tiep_nhan") 'Người tiếp nhận
        lblVoucherTypeID.Text = rl3("Loai_phieu") 'Loại phiếu
        lblVoucherNum.Text = rl3("So_phieu") 'Số phiếu
        lblteVoucher.Text = rl3("Ngay_phieu") 'Ngày phiếu
        lblToDivisionID.Text = rl3("Don_vi") 'Đơn vị
        lblNotes.Text = rl3("Dien_giai") 'Diễn giải
        '================================================================ 
        btnFilter.Text = rl3("Loc") & " (F5)" 'Lọc
        btnSave.Text = rl3("_Luu") '&Lưu
        btnClose.Text = rl3("Do_ng") 'Đó&ng
        '================================================================ 
        chkShowChooseAsset.Text = rl3("Chi_hien_tai_san_da_chon") 'Chỉ hiện tài sản đã chọn
        '================================================================ 
        grpFilterAsset.Text = rL3("Loc_tai_san") 'Lọc tài sản
        grpVoucherInfo.Text = rl3("Thong_tin_phieu") 'Thông tin phiếu
        '================================================================ 
        tdbcEmployeeID.Columns("EmployeeID").Caption = rl3("Ma") 'Mã
        tdbcEmployeeID.Columns("EmployeeName").Caption = rl3("Ten") 'Tên
        tdbcObjectID.Columns("ObjectID").Caption = rl3("Ma") 'Mã
        tdbcObjectID.Columns("ObjectName").Caption = rl3("Ten") 'Tên
        tdbcObjectTypeID.Columns("ObjectTypeID").Caption = rl3("Ma") 'Mã
        tdbcObjectTypeID.Columns("ObjectTypeName").Caption = rl3("Ten") 'Tên
        tdbcToDivisionID.Columns("DivisionID").Caption = rl3("Ma") 'Mã
        tdbcToDivisionID.Columns("DivisionName").Caption = rl3("Ten") 'Tên
        tdbcVoucherTypeID.Columns("VoucherTypeID").Caption = rl3("Ma") 'Mã
        tdbcVoucherTypeID.Columns("VoucherTypeName").Caption = rL3("Ten") 'Tên
        '================================================================ 
        tdbdEquityAccountID.Columns("AccountID").Caption = rL3("Ma") 'Mã
        tdbdEquityAccountID.Columns("AccountName").Caption = rL3("Ten") 'Tên
        '================================================================ 
        tdbg.Columns(COL_IsCheck).Caption = rl3("Chon") 'Chọn
        tdbg.Columns(COL_DateChangeDivision).Caption = rl3("Ngay_dieu_chuyen") 'Ngày điều chuyển
        tdbg.Columns(COL_AssetID).Caption = rl3("Ma_tai_san") 'Mã tài sản
        tdbg.Columns(COL_NewAssetID).Caption = rl3("Ma_tai_san_moi") 'Mã tài sản mới
        tdbg.Columns(COL_AssetName).Caption = rl3("Ten_tai_san") 'Tên tài sản
        tdbg.Columns(COL_CurrentCost).Caption = rL3("Nguyen_gia") 'Nguyên giá
        tdbg.Columns(COL_DepCurrentCost).Caption = rl3("Gia_tri_da_khau_hao") 'Giá trị đã khấu hao
        tdbg.Columns(COL_RemainAmount).Caption = rl3("Gia_tri_con_lai") 'Giá trị còn lại
        tdbg.Columns(COL_ServiceLife).Caption = rl3("So_ky_khau_haoU") 'Số kỳ khẩu hao
        tdbg.Columns(COL_DepreciatedPeriod).Caption = rl3("So_ky_da_khau_hao") 'Số kỳ đã khấu hao
        tdbg.Columns(COL_AssetAccountID).Caption = rl3("TK_tai_san") 'TK Tài sản
        tdbg.Columns(COL_DepAccountID).Caption = rl3("TK_khau_hao") 'TK Khấu hao
        tdbg.Columns(COL_EquityAccountID).Caption = rl3("Tai_khoan_nguon_von") 'Tài khoản nguồn vốn
    End Sub

    Private Sub LoadTDBCombo()
        Dim sSQL As String = ""
        'Load tdbcPeriodFrom, tdbcPeriodTo
        LoadCboPeriodReport(tdbcPeriodFrom, tdbcPeriodTo, D02, gsDivisionID)

        'Load tdbcObjectTypeID
        LoadObjectTypeIDAll(tdbcObjectTypeID, gbUnicode)

        'Load tdbcObjectID
        Using obj As Lemon3.Data.LoadData.ObjectID = New Lemon3.Data.LoadData.ObjectID
            obj.LoadObjectID(tdbcObjectID, dtObjectID)
        End Using
        tdbcObjectTypeID.Text = "%"

        'Load tdbcEmployeeID
        LoadCboCreateBy(tdbcEmployeeID, gbUnicode)

        'Load tdbcToDivisionID
        LoadCboDivisionID(tdbcToDivisionID, D02, False, gbUnicode)

    End Sub

    ' 'Load tdbcObjectID
    Private Sub LoadtdbcObjectID(ByVal sObjectTypeID As String)
        If sObjectTypeID = "%" Then
            LoadDataSource(tdbcObjectID, dtObjectID, gbUnicode)
        Else
            LoadDataSource(tdbcObjectID, ReturnTableFilter(dtObjectID, "ObjectTypeID = " & SQLString(sObjectTypeID), True), gbUnicode)
        End If
    End Sub

    'Load tdbcVoucherTypeID
    Private Sub LoadTDBCVoucherTypeID(Optional ByVal sEditVoucherTypeID As String = "")
        LoadVoucherTypeID(tdbcVoucherTypeID, D02, sEditVoucherTypeID, gbUnicode)
    End Sub


    Private Sub LoadTDBDropDown()
        Dim sSQL As String = ""
        'Load tdbdEquityAccountID
        'Dim dtAccountID As DataTable = ReturnTableAccountIDAndOffAccountForDivision()
        'LoadDataSource(tdbdEquityAccountID, ReturnTableFilter(dtAccountID, "GroupID IN ('10','11','12','13','23') ", True), gbUnicode)

        LoadAccountIDAndOffAccountForDivision(tdbdEquityAccountID, "GroupID = '15'", gbUnicode)
    End Sub

    Private Sub LoadAddNew()
        tdbcPeriodFrom.Text = giTranMonth.ToString("00") & "/" & giTranYear.ToString
        tdbcPeriodTo.Text = tdbcPeriodFrom.Text
        LoadTDBCVoucherTypeID()
        c1dateVoucherDate.Value = Date.Today
    End Sub

    Private Sub LoadEdit()
        tdbcPeriodFrom.Text = giTranMonth.ToString("00") & "/" & giTranYear.ToString
        tdbcPeriodTo.Text = tdbcPeriodFrom.Text

        If tdbg.RowCount > 0 Then
            LoadTDBCVoucherTypeID(tdbg.Columns(COL_VoucherTypeID).Text)
            tdbcVoucherTypeID.Text = tdbg.Columns(COL_VoucherTypeID).Text
            txtVoucherNo.Text = tdbg.Columns(COL_VoucherNo).Text
            c1dateVoucherDate.Value = tdbg.Columns(COL_VoucherDate).Text
            tdbcToDivisionID.Text = tdbg.Columns(COL_ToDivisionID).Text
            txtNotes.Text = tdbg.Columns(COL_Notes).Text
        End If

        ReadOnlyControl(tdbcVoucherTypeID, txtVoucherNo)
    End Sub

    Private Sub LoadTDBGrid(Optional ByVal bFilter As Boolean = False)
        Dim sSQL As String = ""
        If bFilter Then
            sSQL = SQLStoreD02P0017()
        Else
            sSQL = SQLStoreD02P2017()
        End If
        dtGrid = ReturnDataTable(sSQL)
        LoadDataSource(tdbg, dtGrid, gbUnicode)
    End Sub

    Private Sub ReloadTDBGrid()
        dtGrid.DefaultView.RowFilter = IIf(chkShowChooseAsset.Checked, "IsCheck = True", "").ToString
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

#Region "Events tdbcObjectTypeID load tdbcObjectID"

    Private Sub tdbcObjectTypeID_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tdbcObjectTypeID.SelectedValueChanged
        If tdbcObjectTypeID.SelectedValue Is Nothing OrElse tdbcObjectTypeID.Text = "" Then
            LoadtdbcObjectID("%")
            tdbcObjectID.Text = ""
            Exit Sub
        End If
        LoadtdbcObjectID(tdbcObjectTypeID.SelectedValue.ToString())
        tdbcObjectID.Text = ""
    End Sub

    Private Sub tdbcObjectTypeID_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tdbcObjectTypeID.LostFocus
        If tdbcObjectTypeID.FindStringExact(tdbcObjectTypeID.Text) = -1 Then
            tdbcObjectTypeID.Text = "%"
            tdbcObjectID.Text = ""
            Exit Sub
        End If
    End Sub

    Private Sub tdbcObjectID_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tdbcObjectID.LostFocus
        If tdbcObjectID.FindStringExact(tdbcObjectID.Text) = -1 Then tdbcObjectID.Text = ""
    End Sub

#End Region

#Region "Events tdbcEmployeeID"

    Private Sub tdbcEmployeeID_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tdbcEmployeeID.LostFocus
        If tdbcEmployeeID.FindStringExact(tdbcEmployeeID.Text) = -1 Then tdbcEmployeeID.Text = ""
    End Sub

#End Region

#Region "Events tdbcVoucherTypeID with txtVoucherNo"

    Private Sub tdbcVoucherTypeID_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tdbcVoucherTypeID.LostFocus
        If tdbcVoucherTypeID.FindStringExact(tdbcVoucherTypeID.Text) = -1 Then
            tdbcVoucherTypeID.Text = ""
            txtVoucherNo.Text = ""
        End If
    End Sub

    Private Sub tdbcVoucherTypeID_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tdbcVoucherTypeID.SelectedValueChanged
        If _FormState <> EnumFormState.FormAdd Then Exit Sub

        bEditVoucher = False
        bEditVoucherNo = False

        If tdbcVoucherTypeID.SelectedValue Is Nothing OrElse tdbcVoucherTypeID.Text = "" Then
            txtVoucherNo.Text = ""
            ReadOnlyControl(txtVoucherNo)
            Exit Sub
        End If

        If tdbcVoucherTypeID.Columns("Auto").Text = "1" Then 'Sinh tu dong
            txtVoucherNo.Text = CreateIGEVoucherNo(tdbcVoucherTypeID, False)
            ReadOnlyControl(txtVoucherNo)
        Else
            txtVoucherNo.Text = ""
            UnReadOnlyControl(txtVoucherNo, True)
        End If

    End Sub

#End Region

#Region "Events tdbcToDivisionID"

    Private Sub tdbcToDivisionID_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles tdbcToDivisionID.GotFocus
        tdbcToDivisionID.Tag = tdbcToDivisionID.Text
    End Sub

    Private Sub tdbcToDivisionID_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tdbcToDivisionID.MouseDown
        tdbcToDivisionID.Tag = tdbcToDivisionID.Text
    End Sub


    Private Sub tdbcToDivisionID_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles tdbcToDivisionID.Validated
        If tdbcToDivisionID.Tag.ToString = tdbcToDivisionID.Text Then Exit Sub
        If tdbcToDivisionID.FindStringExact(tdbcToDivisionID.Text) = -1 Then tdbcToDivisionID.Text = ""
        SetNewAssetID(True)
    End Sub
#End Region


    '#---------------------------------------------------------------------------------------------------
    '# Title: SQLStoreD02P0017
    '# Created User: NGOCTHOAI
    '# Created Date: 10/04/2017 10:49:06
    '#---------------------------------------------------------------------------------------------------
    Private Function SQLStoreD02P0017() As String
        Dim sSQL As String = ""
        sSQL &= ("-- Loc " & vbCrlf)
        sSQL &= "Exec D02P0017 "
        sSQL &= SQLString(gsDivisionID) & COMMA 'DivisionID, varchar[20], NOT NULL
        sSQL &= SQLString(tdbcObjectTypeID.Text) & COMMA 'ObjectTypeID, varchar[20], NOT NULL
        sSQL &= SQLString(tdbcObjectID.Text) & COMMA 'ObjectID, varchar[50], NOT NULL
        sSQL &= SQLString(tdbcEmployeeID.Text) & COMMA 'EmployeeID, varchar[20], NOT NULL
        sSQL &= SQLNumber(ReturnValueC1Combo(tdbcPeriodFrom, "TranMonth")) & COMMA 'FromMonth, tinyint, NOT NULL
        sSQL &= SQLNumber(ReturnValueC1Combo(tdbcPeriodFrom, "TranYear")) & COMMA 'FromYear, int, NOT NULL
        sSQL &= SQLNumber(ReturnValueC1Combo(tdbcPeriodTo, "TranMonth")) & COMMA 'ToMonth, tinyint, NOT NULL
        sSQL &= SQLNumber(ReturnValueC1Combo(tdbcPeriodTo, "TranYear")) & COMMA 'ToYear, int, NOT NULL
        sSQL &= SqlString(gsLanguage) & COMMA 'Language, varchar[20], NOT NULL
        sSQL &= SQLNumber(gbUnicode) & COMMA 'CodeTable, tinyint, NOT NULL
        sSQL &= SQLNumber(IIf(_FormState = EnumFormState.FormAdd, 0, 1)) & COMMA 'Mode, tinyint, NOT NULL
        sSQL &= SQLString(_sVoucherID) & COMMA 'VoucherID, varchar[20], NOT NULL
        sSQL &= SQLNumber(giTranMonth) & COMMA 'TranMonth, int, NOT NULL
        sSQL &= SQLNumber(giTranYear) 'TranYear, int, NOT NULL
        Return sSQL
    End Function


    '#---------------------------------------------------------------------------------------------------
    '# Title: SQLStoreD02P2017
    '# Created User: NGOCTHOAI
    '# Created Date: 19/05/2017 11:28:44
    '#---------------------------------------------------------------------------------------------------
    Private Function SQLStoreD02P2017() As String
        Dim sSQL As String = ""
        sSQL &= ("-- Do nguon luoi khi xem/ sua " & vbCrLf)
        sSQL &= "Exec D02P2017 "
        sSQL &= SQLString(gsDivisionID) & COMMA 'DivisionID, varchar[20], NOT NULL
        sSQL &= SQLNumber(giTranMonth) & COMMA 'TranMonth, int, NOT NULL
        sSQL &= SQLNumber(giTranYear) & COMMA 'TranYear, int, NOT NULL
        sSQL &= SQLString(Me.Name) & COMMA 'FormID, varchar[20], NOT NULL
        sSQL &= SQLString(My.Computer.Name) & COMMA 'HostID, varchar[20], NOT NULL
        sSQL &= SQLNumber(gbUnicode) & COMMA 'CodeTable, tinyint, NOT NULL
        sSQL &= SqlString(gsLanguage) & COMMA 'Language, varchar[20], NOT NULL
        sSQL &= SQLString(_sVoucherID) 'VoucherID, varchar[20], NOT NULL
        Return sSQL
    End Function

   


    Private Sub PerformFormClosed()
        Dim sSQL As New StringBuilder
        sSQL.Append("IF EXISTS (SELECT TOP 1 1  FROM DBO.SYSOBJECTS WHERE ID = OBJECT_ID(N'[DBO]." & sTableTmp & "') AND OBJECTPROPERTY(ID, N'IsTable') = 1) " & vbCrLf)
        sSQL.Append("BEGIN " & vbCrLf)
        sSQL.Append("DROP TABLE " & sTableTmp & vbCrLf)
        sSQL.Append(" END")

        ExecuteSQLNoTransaction(sSQL.ToString)
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
        If tdbcPeriodFrom.Text.Trim = "" Then
            D99C0008.MsgNotYetChoose(lblPeriodFrom.Text)
            tdbcPeriodFrom.Focus()
            Return False
        End If
        If tdbcPeriodTo.Text.Trim = "" Then
            D99C0008.MsgNotYetChoose("")
            tdbcPeriodTo.Focus()
            Return False
        End If
        If CheckValidPeriodFromTo(tdbcPeriodFrom, tdbcPeriodTo) = False Then Return False
        Return True
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        'Chặn lỗi khi đang vi phạm trên lưới mà nhấn Alt + L
        btnSave.Focus()
        If btnSave.Focused = False Then Exit Sub
        '************************************
        If AskSave() = Windows.Forms.DialogResult.No Then Exit Sub
        Dim dr() As DataRow
        If Not AllowSave(dr) Then Exit Sub

        'Kiểm tra Ngày phiếu có phù hợp với kỳ kế toán hiện tại không

        If Not CheckVoucherDateInPeriod(c1dateVoucherDate.Text) Then c1dateVoucherDate.Focus() : Exit Sub
        btnSave.Enabled = False
        btnClose.Enabled = False

        Me.Cursor = Cursors.WaitCursor
        Dim sSQL As New StringBuilder
        Select Case _FormState
            Case EnumFormState.FormAdd
                'Sinh IGE cho khóa của Phiếu trước
                If _sVoucherID = "" Then _sVoucherID = CreateIGE("D02T0012", "VoucherID", "02", "GP", gsStringKey)
                If tdbcVoucherTypeID.Columns("Auto").Text = "1" And bEditVoucherNo = False Then 'Tự động
                    txtVoucherNo.Text = CreateIGEVoucherNoNew(tdbcVoucherTypeID, "D02T0012", _sVoucherID)
                Else 'Không sinh tự động hay có nhấn F2
                    If bEditVoucherNo = False Then
                        If CheckDuplicateVoucherNoNew(D02, "D02T0012", _sVoucherID, txtVoucherNo.Text) = True Then btnSave.Enabled = True : btnClose.Enabled = True : Me.Cursor = Cursors.Default : Exit Sub
                    Else 'Có nhấn F2 để sửa số phiếu
                        SQLInsertD02T5558(_sVoucherID, sOldVoucherNo, txtVoucherNo.Text)
                    End If
                    InsertVoucherNoD91T9111(txtVoucherNo.Text, "D02T0012", _sVoucherID)
                End If
                bEditVoucherNo = False
                sOldVoucherNo = ""
                bEditVoucher = False
        End Select

        sSQL.Append(SQLCreateTableD02F2017)
        sSQL.Append(SQLInsertD02F2017s(dr))
        sSQL.Append(SQLStoreD02P2019)
        If CheckStore(sSQL.ToString) = False Then
            btnClose.Enabled = True
            btnSave.Enabled = True
            PerformFormClosed()
            Me.Cursor = Cursors.Default
            Exit Sub
        End If

        Dim bRunSQL As Boolean = ExecuteSQL(SQLStoreD02P2022)
        Me.Cursor = Cursors.Default

        If bRunSQL Then
            SaveOK()
            PerformFormClosed()
            btnClose.Enabled = True
            Select Case _FormState
                Case EnumFormState.FormAdd

                Case EnumFormState.FormEdit
                    btnSave.Enabled = True
                    btnClose.Focus()
            End Select
        Else
            SaveNotOK()
            btnClose.Enabled = True
            btnSave.Enabled = True
        End If
    End Sub

    Private Function AllowSave(ByRef dr() As DataRow) As Boolean
        If tdbcVoucherTypeID.Text.Trim = "" Then
            D99C0008.MsgNotYetChoose(lblVoucherTypeID.Text)
            tdbcVoucherTypeID.Focus()
            Return False
        End If
        If txtVoucherNo.Text.Trim = "" Then
            D99C0008.MsgNotYetEnter("")
            txtVoucherNo.Focus()
            Return False
        End If
        If c1dateVoucherDate.Value.ToString = "" Then
            D99C0008.MsgNotYetEnter("")
            c1dateVoucherDate.Focus()
            Return False
        End If
        If tdbcToDivisionID.Text.Trim = "" Then
            D99C0008.MsgNotYetChoose(lblToDivisionID.Text)
            tdbcToDivisionID.Focus()
            Return False
        End If
        tdbg.UpdateData()
        If tdbg.RowCount <= 0 Then
            D99C0008.MsgNoDataInGrid()
            tdbg.Focus()
            Return False
        End If
        tdbg.UpdateData()
        If tdbg.RowCount <= 0 Then
            D99C0008.MsgNoDataInGrid()
            tdbg.Focus()
            Return False
        End If
        dr = dtGrid.Select(" IsCheck = True")
        If dr.Length < 1 Then
            D99C0008.MsgL3(rL3("MSG000010"))
            tdbg.Focus()
            tdbg.SplitIndex = SPLIT0
            tdbg.Col = COL_IsCheck
            tdbg.Row = 0
            Return False
        End If
        For i As Integer = 0 To dr.Length - 1
            If dr(i).Item("DateChangeDivision").ToString = "" Then
                D99C0008.MsgNotYetEnter(tdbg.Columns(COL_DateChangeDivision).Caption)
                tdbg.Focus()
                tdbg.SplitIndex = 0
                tdbg.Col = COL_DateChangeDivision
                tdbg.Row = findrowInGrid(tdbg, dr(i).Item("AssetID").ToString, "AssetID")
                Return False
            End If
            If dr(i).Item("EquityAccountID").ToString = "" Then
                D99C0008.MsgNotYetEnter(tdbg.Columns(COL_EquityAccountID).Caption)
                tdbg.Focus()
                tdbg.SplitIndex = 1
                tdbg.Col = COL_EquityAccountID
                tdbg.Row = findrowInGrid(tdbg, dr(i).Item("AssetID").ToString, "AssetID")
                Return False
            End If
        Next
        Return True
    End Function

    Private Sub SetBackColorObligatory()
        tdbcPeriodFrom.EditorBackColor = COLOR_BACKCOLOROBLIGATORY
        tdbcPeriodTo.EditorBackColor = COLOR_BACKCOLOROBLIGATORY
        tdbcVoucherTypeID.EditorBackColor = COLOR_BACKCOLOROBLIGATORY
        txtVoucherNo.BackColor = COLOR_BACKCOLOROBLIGATORY
        c1dateVoucherDate.BackColor = COLOR_BACKCOLOROBLIGATORY
        tdbcToDivisionID.EditorBackColor = COLOR_BACKCOLOROBLIGATORY
        tdbg.Splits(SPLIT0).DisplayColumns(COL_DateChangeDivision).Style.BackColor = COLOR_BACKCOLOROBLIGATORY
        tdbg.Splits(SPLIT1).DisplayColumns(COL_EquityAccountID).Style.BackColor = COLOR_BACKCOLOROBLIGATORY
    End Sub

    '#---------------------------------------------------------------------------------------------------
    '# Title: SQLInsertD02T5558
    '# Created User: NGOCTHOAI
    '# Created Date: 19/05/2017 04:38:34
    '#---------------------------------------------------------------------------------------------------
    Private Function SQLInsertD02T5558(ByVal sBatchID As String, ByVal sOldVoucherNo As String, ByVal sNewVoucherNo As String) As StringBuilder
        Dim sSQL As New StringBuilder
        sSQL.Append("-- s" & vbCrLf)
        sSQL.Append("Insert Into D02T5558(")
        sSQL.Append("BatchID, OldVoucherNo, NewVoucherNo, CreateUserID, CreateDate, " & vbCrLf)
        sSQL.Append("TranMonth, TranYear, DivisionID")
        sSQL.Append(") Values(" & vbCrLf)
        sSQL.Append(SQLString(sBatchID) & COMMA) 'BatchID, varchar[20], NOT NULL
        sSQL.Append(SQLString(sOldVoucherNo) & COMMA) 'OldVoucherNo, varchar[50], NOT NULL
        sSQL.Append(SQLString(sNewVoucherNo) & COMMA) 'NewVoucherNo, varchar[50], NOT NULL
        sSQL.Append(SQLString(gsUserID) & COMMA) 'CreateUserID, varchar[20], NOT NULL
        sSQL.Append(SQLDateTimeSave(gsGetDate) & COMMA & vbCrLf) 'CreateDate, datetime, NULL
        sSQL.Append(SQLNumber(giTranMonth) & COMMA) 'TranMonth, tinyint, NOT NULL
        sSQL.Append(SQLNumber(giTranYear) & COMMA) 'TranYear, int, NOT NULL
        sSQL.Append(SQLString(gsDivisionID)) 'DivisionID, varchar[50], NOT NULL
        sSQL.Append(")")

        Return sSQL
    End Function

    Private Function SQLCreateTableD02F2017() As StringBuilder
        Dim sSQL As New StringBuilder
        sSQL.Append("IF EXISTS (SELECT TOP 1 1  FROM DBO.SYSOBJECTS WHERE ID = OBJECT_ID(N'[DBO]." & sTableTmp & "') AND OBJECTPROPERTY(ID, N'IsTable') = 1) " & vbCrLf)
        sSQL.Append("BEGIN " & vbCrLf)
        sSQL.Append("DROP TABLE " & sTableTmp & vbCrLf)
        sSQL.Append("CREATE TABLE " & sTableTmp & " (" & vbCrLf)
        sSQL.Append("VoucherTypeID Varchar(20) Default(''), ")
        sSQL.Append("VoucherNo Varchar(50) Default(''), ")
        sSQL.Append("VoucherDesc Varchar(1000), ")
        sSQL.Append("VoucherDescU NVarchar(500), ")
        sSQL.Append("VoucherDate Datetime, ")
        sSQL.Append("AssetID Varchar(50) Default(''), ")
        sSQL.Append("AssetName Varchar(1000) Default(''), ")
        sSQL.Append("AssetNameU NVarchar(500) Default(N''), ")
        sSQL.Append("DivisionID Varchar(20) Default(''), ")
        sSQL.Append("AssetAccountID Varchar(20) Default(''), ")
        sSQL.Append("DepAccountID Varchar(20) Default(''), ")
        sSQL.Append("ConvertedAmount Decimal(28,8) Default(0), ")
        sSQL.Append("DepCurrentCost Decimal(28,8) Default(0), ")
        sSQL.Append("RemainAmount Decimal(28,8) Default(0), ")
        sSQL.Append("ServiceLife Int Default(0), ")
        sSQL.Append("DepreciatedPeriod Int Default(0), ")
        sSQL.Append("ToDivisionID Varchar(20) Default(''), ")
        sSQL.Append("NewAssetID Varchar(50) Default(''), ")
        sSQL.Append("EquityAccountID Varchar(20), ")
        sSQL.Append("DateChangeDivision DateTime ) " & vbCrLf)
        sSQL.Append("END" & vbCrLf)
        sSQL.Append("ELSE " & vbCrLf)
        sSQL.Append("BEGIN " & vbCrLf)
        sSQL.Append("CREATE TABLE " & sTableTmp & " (" & vbCrLf)
        sSQL.Append("VoucherTypeID Varchar(20) Default(''), ")
        sSQL.Append("VoucherNo Varchar(50) Default(''), ")
        sSQL.Append("VoucherDesc Varchar(1000), ")
        sSQL.Append("VoucherDescU NVarchar(500), ")
        sSQL.Append("VoucherDate Datetime, ")
        sSQL.Append("AssetID Varchar(50) Default(''), ")
        sSQL.Append("AssetName Varchar(1000) Default(''), ")
        sSQL.Append("AssetNameU NVarchar(500) Default(N''), ")
        sSQL.Append("DivisionID Varchar(20) Default(''), ")
        sSQL.Append("AssetAccountID Varchar(20) Default(''), ")
        sSQL.Append("DepAccountID Varchar(20) Default(''), ")
        sSQL.Append("ConvertedAmount Decimal(28,8) Default(0), ")
        sSQL.Append("DepCurrentCost Decimal(28,8) Default(0), ")
        sSQL.Append("RemainAmount Decimal(28,8) Default(0), ")
        sSQL.Append("ServiceLife Int Default(0), ")
        sSQL.Append("DepreciatedPeriod Int Default(0), ")
        sSQL.Append("ToDivisionID Varchar(20) Default(''), ")
        sSQL.Append("NewAssetID Varchar(50) Default(''), ")
        sSQL.Append("EquityAccountID Varchar(20), ")
        sSQL.Append("DateChangeDivision DateTime) " & vbCrLf)
        sSQL.Append("END" & vbCrLf)

        Return sSQL
    End Function

    '#---------------------------------------------------------------------------------------------------
    '# Title: SQLInsertD02T0012s
    '# Created User: NGOCTHOAI
    '# Created Date: 19/05/2017 04:57:29
    '#---------------------------------------------------------------------------------------------------
    Private Function SQLInsertD02F2017s(ByVal dr() As DataRow) As StringBuilder
        Dim sRet As New StringBuilder
        Dim sSQL As New StringBuilder

        For i As Integer = 0 To dr.Length - 1
            If sSQL.ToString = "" And sRet.ToString = "" Then sSQL.Append("-- Luu du lieu bang tam " & vbCrLf)
            sSQL.Append("Insert Into " & sTableTmp & "(")
            sSQL.Append("VoucherTypeID, VoucherNo, " & vbCrLf)
            sSQL.Append("VoucherDescU, VoucherDate, AssetID, DivisionID, AssetAccountID, DepAccountID, " & vbCrLf)
            sSQL.Append("AssetNameU, ConvertedAmount, DepCurrentCost, RemainAmount, ServiceLife, DepreciatedPeriod, " & vbCrLf)
            sSQL.Append("ToDivisionID, NewAssetID, EquityAccountID, DateChangeDivision" & vbCrLf)
            sSQL.Append(") Values(" & vbCrLf)
            sSQL.Append(SQLString(tdbcVoucherTypeID.Text) & COMMA) 'VoucherTypeID, varchar[20], NULL
            sSQL.Append(SQLString(txtVoucherNo.Text) & COMMA & vbCrLf) 'VoucherNo, varchar[20], NULL

            sSQL.Append(SQLStringUnicode(txtNotes, True) & COMMA & vbCrLf) 'VoucherDesc, varchar[20], NULL
            sSQL.Append(SQLDateSave(c1dateVoucherDate.Value) & COMMA) 'VoucherDate, varchar[20], NULL
            sSQL.Append(SQLString(dr(i).Item("AssetID")) & COMMA) 'AssetID, varchar[50], NULL
            sSQL.Append(SQLString(gsDivisionID) & COMMA) 'DivisionID, varchar[50], NULL

            sSQL.Append(SQLString(dr(i).Item("AssetAccountID")) & COMMA) 'AssetAccountID, datetime, NULL
            sSQL.Append(SQLString(dr(i).Item("DepAccountID")) & COMMA & vbCrLf) 'DepAccountID, datetime, NULL

            sSQL.Append(SQLStringUnicode(dr(i).Item("AssetName"), gbUnicode, True) & COMMA) 'AssetName, datetime, NULL
            sSQL.Append(SQLMoney(dr(i).Item("CurrentCost")) & COMMA) 'ConvertedAmount, money, NULL
            sSQL.Append(SQLMoney(dr(i).Item("DepCurrentCost")) & COMMA) 'DepCurrentCost, money, NULL
            sSQL.Append(SQLMoney(dr(i).Item("RemainAmount")) & COMMA) 'RemainAmount, money, NULL
            sSQL.Append(SQLNumber(dr(i).Item("ServiceLife")) & COMMA) 'ServiceLife, money, NULL
            sSQL.Append(SQLNumber(dr(i).Item("DepreciatedPeriod")) & COMMA & vbCrLf) 'DepreciatedPeriod, money, NULL

            sSQL.Append(SQLString(tdbcToDivisionID.Text) & COMMA) 'ToDivisionID, datetime, NULL
            sSQL.Append(SQLString(dr(i).Item("NewAssetID")) & COMMA) 'NewAssetID, datetime, NULL
            sSQL.Append(SQLString(dr(i).Item("EquityAccountID")) & COMMA) 'EquityAccountID, datetime, NULL
            sSQL.Append(SQLDateSave(dr(i).Item("DateChangeDivision"))) 'DateChangeDivision, datetime, NULL

            sSQL.Append(")")

            sRet.Append(sSQL.ToString & vbCrLf)
            sSQL.Remove(0, sSQL.Length)
        Next
        Return sRet
    End Function

    Private Function SQLStoreD02P2019() As String
        Dim sSQL As String = ""
        sSQL &= ("-- Kiem tra truoc khi sua/xoa " & vbCrLf)
        sSQL &= "Exec D02P2019 "
        sSQL &= SQLString(gsLanguage) & COMMA 'Language, varchar[20], NOT NULL
        sSQL &= SQLString(gsUserID) & COMMA 'UserID, varchar[20], NOT NULL
        sSQL &= SQLString(My.Computer.Name) & COMMA 'HostID, varchar[20], NOT NULL
        sSQL &= SQLNumber(0) & COMMA 'Mode, int, NOT NULL
        sSQL &= SQLString(Me.Name) & COMMA 'FormID, varchar[20], NOT NULL
        sSQL &= SQLString(sTableTmp) & COMMA 'Key01ID, varchar[20], NOT NULL
        sSQL &= SQLString(_sVoucherID) & COMMA 'Key02ID, varchar[20], NOT NULL
        sSQL &= SQLString(giTranMonth) & COMMA 'Key03ID, varchar[20], NOT NULL
        sSQL &= SQLString(giTranYear) & COMMA 'Key04ID, varchar[20], NOT NULL
        sSQL &= SQLString("") 'Key05ID, varchar[20], NOT NULL
        Return sSQL
    End Function

    '#---------------------------------------------------------------------------------------------------
    '# Title: SQLStoreD02P2022
    '# Created User: NGOCTHOAI
    '# Created Date: 22/05/2017 02:08:13
    '#---------------------------------------------------------------------------------------------------
    Private Function SQLStoreD02P2022() As String
        Dim sSQL As String = ""
        sSQL &= ("-- Thanh ly TSCD tai tong cong ty va tao tai san moi cho don vi den " & vbCrlf)
        sSQL &= "Exec D02P2022 "
        sSQL &= SqlString(gsLanguage) & COMMA 'Language, varchar[20], NOT NULL
        sSQL &= SQLString(gsUserID) & COMMA 'UserID, varchar[20], NOT NULL
        sSQL &= SQLString(My.Computer.Name) & COMMA 'HostID, varchar[20], NOT NULL
        sSQL &= SQLString(Me.Name) & COMMA 'FormID, varchar[20], NOT NULL
        sSQL &= SQLString(gsDivisionID) & COMMA 'DivisionID, varchar[20], NOT NULL
        sSQL &= SQLNumber(giTranMonth) & COMMA 'TranMonth, int, NOT NULL
        sSQL &= SQLNumber(giTranYear) & COMMA 'TranYear, int, NOT NULL
        sSQL &= SQLString(sTableTmp) & COMMA 'DataTable, varchar[50], NOT NULL
        sSQL &= SQLNumber(IIf(_FormState = EnumFormState.FormAdd, 0, 1)) & COMMA 'Mode, int, NOT NULL
        sSQL &= SQLString(_sVoucherID) 'VoucherID, varchar[20], NOT NULL
        Return sSQL
    End Function


    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub chkShowChooseAsset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkShowChooseAsset.Click
        If dtGrid Is Nothing Then Exit Sub
        ReloadTDBGrid()
    End Sub

    Private Sub tdbg_AfterColUpdate(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.ColEventArgs) Handles tdbg.AfterColUpdate
        If e.ColIndex = COL_IsCheck Then
            SetNewAssetID()
        End If
    End Sub

    Private Sub tdbg_BeforeColUpdate(ByVal sender As System.Object, ByVal e As C1.Win.C1TrueDBGrid.BeforeColUpdateEventArgs) Handles tdbg.BeforeColUpdate
        '--- Kiểm tra giá trị hợp lệ
        Select Case e.ColIndex
            Case COL_EquityAccountID
                If tdbg.Columns(e.ColIndex).Text <> tdbg.Columns(e.ColIndex).DropDown.Columns(tdbg.Columns(e.ColIndex).DropDown.DisplayMember).Text Then
                    tdbg.Columns(e.ColIndex).Text = ""
                End If
        End Select
    End Sub

    Private Sub SetNewAssetID(Optional ByVal bAllGrid As Boolean = False, Optional ByVal irow As Integer = -1)
        If tdbg.RowCount < 1 Then Exit Sub

        If bAllGrid Then
            tdbg.UpdateData()

            Dim dr() As DataRow = dtGrid.Select("IsCheck = True")
            If dr.Length > 0 Then
                For i As Integer = 0 To dr.Length - 1
                    dr(i).Item("NewAssetID") = IIf(tdbcToDivisionID.Text <> "", tdbcToDivisionID.Text & "_", "").ToString & dr(i).Item("AssetID").ToString
                Next
            End If
        Else
            If irow <> -1 Then
                tdbg(irow, COL_NewAssetID) = IIf(L3Bool(tdbg(irow, COL_IsCheck)), IIf(tdbcToDivisionID.Text <> "", tdbcToDivisionID.Text & "_", "").ToString & tdbg(irow, COL_AssetID).ToString, "").ToString
            Else
                tdbg.Columns(COL_NewAssetID).Text = IIf(L3Bool(tdbg.Columns(COL_IsCheck).Text), IIf(tdbcToDivisionID.Text <> "", tdbcToDivisionID.Text & "_", "").ToString & tdbg.Columns(COL_AssetID).Text, "").ToString
            End If

        End If
    End Sub

    Private Sub tdbg_ComboSelect(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.ColEventArgs) Handles tdbg.ComboSelect
        tdbg.UpdateData()
    End Sub

    Dim bSelect As Boolean = False 'Mặc định Uncheck - tùy thuộc dữ liệu database
    Private Sub HeadClick(ByVal iCol As Integer)
        If tdbg.RowCount <= 0 Then Exit Sub
        Select Case iCol
            Case COL_IsCheck
                MyHeadClick(tdbg, iCol, bSelect)
            Case COL_DateChangeDivision, COL_EquityAccountID
                tdbg.AllowSort = False
                'Copy 1 cột
                CopyColumns(tdbg, iCol, tdbg.Columns(iCol).Text, tdbg.Bookmark)
        End Select
    End Sub

    Private Sub MyHeadClick(ByVal tdbg As C1.Win.C1TrueDBGrid.C1TrueDBGrid, ByVal COL_Choose As Integer, ByRef bSelected As Boolean)
        Dim bSelect As Boolean = Not bSelected
        tdbg.AllowSort = False
        tdbg.UpdateData()

        Dim i As Integer = tdbg.RowCount - 1

        While i >= 0
            tdbg(i, COL_Choose) = bSelect
            SetNewAssetID(, i)
            i -= 1
        End While

        bSelected = bSelect
    End Sub

    Private Sub tdbg_HeadClick(ByVal sender As Object, ByVal e As C1.Win.C1TrueDBGrid.ColEventArgs) Handles tdbg.HeadClick
        HeadClick(e.ColIndex)
    End Sub

    Private Sub tdbg_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tdbg.KeyDown
        If e.Control And e.KeyCode = Keys.S Then HeadClick(tdbg.Col)
    End Sub

   
End Class