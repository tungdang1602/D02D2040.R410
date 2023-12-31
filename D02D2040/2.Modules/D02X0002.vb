''' <summary>
''' Module này dùng để khai báo các Sub và Function toàn cục
''' </summary>
''' <remarks>Các khai báo Sub và Function ở đây không được trùng với các khai báo
''' ở các module D99Xxxxx
''' </remarks>
Module D02X0002

    '''' <summary>
    '''' Trả về là quyền của màn hình truyền vào
    '''' </summary>
    '''' <param name="FormName">Màn hình cần lấy quyền</param>
    '<DebuggerStepThrough()> _
    'Public Function ReturnPermission(ByVal FormName As String) As Integer
    '    Return D00D0041.D00C0001.GetScreenPermission(gsUserID, gsCompanyID, D02, FormName)
    'End Function

    ''' <summary>
    ''' Cập nhật số thứ tự cho lưới
    ''' </summary>
    Public Sub UpdateOrderNum(ByVal TDBGrid As C1.Win.C1TrueDBGrid.C1TrueDBGrid, ByVal iCol As Integer)
        For i As Integer = 0 To TDBGrid.RowCount - 1
            TDBGrid(i, iCol) = i + 1
        Next
    End Sub

    ''' <summary>
    ''' Kiểm tra sự tồn tại của 1 giá trị trong 1 cột trên lưới với nguồn dữ liệu trong TDBDropdown
    ''' </summary>
    Public Function CheckExist(ByVal pTDBD As C1.Win.C1TrueDBGrid.C1TrueDBDropdown, ByVal piCol As Integer, ByVal sText As String) As Boolean
        For i As Integer = 0 To pTDBD.RowCount - 1
            pTDBD.Row = i
            If pTDBD.Columns(piCol).Text = sText Then Return True
        Next
        Return False
    End Function

    Private Function FindSxType(ByVal nType As String, ByVal s As String) As String
        Select Case nType.Trim
            Case "1" ' Theo tháng
                Return giTranMonth.ToString("00")
            Case "2" ' Theo năm
                Return giTranYear.ToString
            Case "3" ' Theo loại chứng từ
                Return s
            Case "4" ' Theo đơn vị
                Return gsDivisionID
            Case "5" ' Theo hằng
                Return s
            Case Else
                Return ""
        End Select
    End Function

    Public Sub GetNewVoucherNo(ByVal strS1 As String, ByVal strS2 As String, ByVal strS3 As String, ByVal sOutputOrder As String, ByVal iOutputLength As Integer, ByVal sSeperator As String, ByVal txtVoucherNo As TextBox, ByVal bState As Boolean, ByVal sTableName As String)
        Dim conn As New SqlConnection(gsConnectionString)
        'Dim strS1 As String = ""
        'Dim strS2 As String = ""
        'Dim strS3 As String = ""
        'If c1Combo.Columns("S1Type").Text <> "0" Then
        '    strS1 = FindSxType(c1Combo.Columns("S1Type").Text, c1Combo.Columns("S1").Text)
        'End If
        'If c1Combo.Columns("S2Type").Text <> "0" Then
        '    strS2 = FindSxType(c1Combo.Columns("S2Type").Text, c1Combo.Columns("S2").Text)
        'End If
        'If c1Combo.Columns("S3Type").Text <> "0" Then
        '    strS3 = FindSxType(c1Combo.Columns("S3Type").Text, c1Combo.Columns("S3").Text)
        'End If

        Dim frm As New D99F1111
        frm.TableName = sTableName

        frm.NewKeyString = strS1 & strS2 & strS3
        If bState Then
            frm.ShowDialog()
            If frm.Result = True Then
                Dim iOutputOrder As Integer = -1
                Select Case sOutputOrder
                    Case "NSSS"
                        iOutputOrder = D99D0041.OutOrderEnum.lmNSSS
                    Case "SNSS"
                        iOutputOrder = D99D0041.OutOrderEnum.lmSNSS
                    Case "SSNS"
                        iOutputOrder = D99D0041.OutOrderEnum.lmSSNS
                    Case "SSSN"
                        iOutputOrder = D99D0041.OutOrderEnum.lmSSSN
                End Select
                txtVoucherNo.Text = CreateIGEVoucherNo(strS1, strS2, strS3, CType(iOutputOrder, D99D0041.OutOrderEnum), iOutputLength, sSeperator, bState, sTableName) 'D99C0004.IGEVoucherNo(conn, False, gnNewLastKey, strS1, strS2, strS3, CType(iOutputOrder, D99D0041.OutOrderEnum), iOutputLength, sSeperator)

                frm.Dispose()
            Else
                frm.Dispose()
            End If
        Else
            Dim iOutputOrder As Integer = -1
            Select Case sOutputOrder
                Case "NSSS"
                    iOutputOrder = D99D0041.OutOrderEnum.lmNSSS
                Case "SNSS"
                    iOutputOrder = D99D0041.OutOrderEnum.lmSNSS
                Case "SSNS"
                    iOutputOrder = D99D0041.OutOrderEnum.lmSSNS
                Case "SSSN"
                    iOutputOrder = D99D0041.OutOrderEnum.lmSSSN
            End Select
            txtVoucherNo.Text = CreateIGEVoucherNo(strS1, strS2, strS3, CType(iOutputOrder, D99D0041.OutOrderEnum), iOutputLength, sSeperator, bState, sTableName)
            frm.Dispose()
        End If
    End Sub

    ''' <summary>
    ''' Xác định ví trí hiện hành của lưới
    ''' </summary>
    Public Sub SetCurrentRow(ByVal TDBGrid As C1.Win.C1TrueDBGrid.C1TrueDBGrid, ByVal iCol As Integer, ByVal sText As String)
        If TDBGrid.RowCount > 0 Then
            For i As Integer = 0 To TDBGrid.RowCount - 1
                If TDBGrid(i, iCol).ToString() = sText Then
                    TDBGrid.Row = i
                    Exit Sub
                End If
            Next
            TDBGrid.Row = 0
        End If
    End Sub

    ''' <summary>
    ''' Tính tổng cho 1 cột tương ứng trên lưới
    ''' </summary>
    ''' <param name="ipCol"></param>
    ''' <param name="C1Grid"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Public Function Sum(ByVal ipCol As Integer, ByVal C1Grid As C1.Win.C1TrueDBGrid.C1TrueDBGrid) As Double
        Dim lSum As Double = 0
        For i As Integer = 0 To C1Grid.RowCount - 1
            If C1Grid(i, ipCol) Is Nothing OrElse TypeOf (C1Grid(i, ipCol)) Is DBNull Or C1Grid(i, ipCol).ToString = "" Then Continue For
            lSum += Convert.ToDouble(C1Grid(i, ipCol))
        Next
        Return lSum
    End Function

    ''' <summary>
    ''' TONG CONG SO DONG TREN LUOI
    ''' </summary>
    ''' <param name="iRow"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function TongCong(ByVal iRow As Int32) As String
        Return IIf(geLanguage = EnumLanguage.Vietnamese, "Toång coäng", "Total").ToString & " (" & iRow & ")"
    End Function
    '#--------------------------------------------------------------------------
    '#CreateUser: Trần Thị Ái Trâm
    '#CreateDate: 04/09/2007
    '#ModifiedUser:
    '#ModifiedDate:
    '#Description: Hàm kiểm tra Audit log
    '#--------------------------------------------------------------------------
    Public Function PermissionAudit(ByVal sAuditCode As String) As Byte
        Dim sSQL As String
        Dim dt As DataTable

        sSQL = "Select Audit From D91T9200 WITH(NOLOCK) " & vbCrLf
        sSQL &= "Where AuditCode=" & SQLString(sAuditCode)

        dt = ReturnDataTable(sSQL)
        If dt.Rows.Count > 0 Then
            If CByte(dt.Rows(0).Item("Audit")) = 1 Then
                Return 1
            Else
                Return 0
            End If
        Else
            Return 0
        End If
    End Function

    '#---------------------------------------------------------------------------------------------------
    '# Title: SQLStoreD91P9106
    '# Created User: Trần Thị ÁiTrâm
    '# Created Date: 04/09/2007 11:30:16
    '# Modified User: 
    '# Modified Date: 
    '# Description: 
    '#---------------------------------------------------------------------------------------------------
    'Private Function SQLStoreD91P9106(ByVal sAuditCode As String, ByVal sEventID As String, ByVal sDesc1 As String, ByVal sDesc2 As String, ByVal sDesc3 As String, ByVal sDesc4 As String, ByVal sDesc5 As String) As String
    '    Dim sSQL As String = ""
    '    sSQL &= "Exec D91P9106 "
    '    sSQL &= SQLDateTimeSave(Date.Now) & COMMA 'AuditDate, datetime, NOT NULL
    '    sSQL &= SQLString(sAuditCode) & COMMA 'AuditCode, varchar[20], NOT NULL
    '    sSQL &= SQLString("") & COMMA 'DivisionID, varchar[20], NOT NULL
    '    sSQL &= SQLString("02") & COMMA 'ModuleID, varchar[2], NOT NULL
    '    sSQL &= SQLString(gsUserID) & COMMA 'UserID, varchar[20], NOT NULL
    '    sSQL &= SQLString(sEventID) & COMMA 'EventID, varchar[20], NOT NULL
    '    sSQL &= SQLString(sDesc1) & COMMA 'Desc1, varchar[250], NOT NULL
    '    sSQL &= SQLString(sDesc2) & COMMA 'Desc2, varchar[250], NOT NULL
    '    sSQL &= SQLString(sDesc3) & COMMA 'Desc3, varchar[250], NOT NULL
    '    sSQL &= SQLString(sDesc4) & COMMA 'Desc4, varchar[250], NOT NULL
    '    sSQL &= SQLString(sDesc5) & COMMA 'Desc5, varchar[250], NOT NULL
    '    sSQL &= SQLNumber(0) & COMMA 'IsAuditDetail, varchar[250], NOT NULL
    '    sSQL &= SQLString("")  'AuditItemID, varchar[250], NOT NULL
    '    Return sSQL
    'End Function

    '#--------------------------------------------------------------------------
    '#CreateUser: Trần Thị ÁiTrâm
    '#CreateDate: 04/09/2007
    '#ModifiedUser:
    '#ModifiedDate:
    '#Description: Thực thi store Audit Log
    '#--------------------------------------------------------------------------
    'Public Sub ExecuteAuditLog(ByVal sAuditCode As String, ByVal sEventID As String, Optional ByVal sDesc1 As String = "", Optional ByVal sDesc2 As String = "", Optional ByVal sDesc3 As String = "", Optional ByVal sDesc4 As String = "", Optional ByVal sDesc5 As String = "")
    '    Dim sSQL As String
    '    sSQL = SQLStoreD91P9106(sAuditCode, sEventID, sDesc1, sDesc2, sDesc3, sDesc4, sDesc5)
    '    ExecuteSQL(sSQL)
    'End Sub

    Public Function GetVoucherNoCus( _
 ByVal TableName As String, _
 ByVal TDBC As C1.Win.C1List.C1Combo _
                        ) As String
        Dim sSQL As String
        Dim sKey1 As String
        Dim sKey2 As String
        Dim sKey3 As String
        Dim sResult As String = ""
        Dim dt As New DataTable

        If IsDBNull(TDBC.Columns("S1Type").Text) Or CInt(Val(TDBC.Columns("S1Type").Text)) = 0 Then
            sKey1 = ""
        Else
            sKey1 = FindSxType(TDBC.Columns("S1Type").Text, TDBC.Columns("S1").Text)
        End If
        If IsDBNull(TDBC.Columns("S2Type").Text) Or CInt(Val(TDBC.Columns("S2Type").Text)) = 0 Then
            sKey2 = ""
        Else
            sKey2 = FindSxType(TDBC.Columns("S2Type").Text, TDBC.Columns("S2").Text)
        End If
        If IsDBNull(TDBC.Columns("S3Type").Text) Or CInt(Val(TDBC.Columns("S3Type").Text)) = 0 Then
            sKey3 = ""
        Else
            sKey3 = FindSxType(TDBC.Columns("S3Type").Text, TDBC.Columns("S3").Text)
        End If

        sSQL = ""
        sSQL = " Exec D91P9111 " & SQLString(TableName) & "," & SQLString(sKey1) & " "
        sSQL = sSQL & " , " & SQLString(sKey2) & " , " & SQLString(sKey3)
        sSQL = sSQL & " , " & SQLNumber(TDBC.Columns("OutputLength").Text)
        sSQL = sSQL & " , " & SQLNumber(TDBC.Columns("OutputOrder").Text)
        If (TDBC.Columns("Separator").Text.Trim).Length = 0 Then
            sSQL = sSQL & " , 0 "
        Else
            sSQL = sSQL & " , 1 "
        End If
        sSQL = sSQL & " , " & SQLString(TDBC.Columns("Separator").Text) & " ," & " '' ,'' ,'' "
        dt = ReturnDataTable(sSQL)
        If dt.Rows.Count > 0 Then
            'GetVoucherNoCus = dt.Rows(0).Item("VoucherNo").ToString
            sResult = dt.Rows(0).Item("VoucherNo").ToString
        Else
            'GetVoucherNoCus = ""
            sResult = ""
        End If
        Return sResult
    End Function

    Public Function TypeLength(ByVal nType As Integer) As Integer
        Dim sSQL As String = ""
        Dim dt As New DataTable
        Dim iTypeLength As Integer

        Select Case nType

            Case 1 's1Length
                sSQL = " Select  S1Length From  D02T0000 WITH(NOLOCK)  Where AssetS1Enabled  = 1 "

            Case 2 's2Length
                sSQL = " Select  S2Length From  D02T0000 WITH(NOLOCK)  Where AssetS2Enabled  = 1 "

            Case 3 's3Length
                sSQL = " Select  S3Length From  D02T0000 WITH(NOLOCK)  Where AssetS3Enabled  = 1 "

            Case 4 'AssetOutputLength
                sSQL = " Select  AssetOutputLength From  D02T0000 WITH(NOLOCK) Where  AssetOutputLength <> 0 "
        End Select
        dt = ReturnDataTable(sSQL)
        If dt.Rows.Count > 0 Then
            Select Case nType

                Case 1 's1Length
                    iTypeLength = CInt(dt.Rows(0).Item("S1Length").ToString)

                Case 2 's2Length
                    iTypeLength = CInt(dt.Rows(0).Item("S2Length").ToString)

                Case 3 's3Length
                    iTypeLength = CInt(dt.Rows(0).Item("S3Length").ToString)
                Case 4 'AssetOutputLength
                    iTypeLength = CInt(dt.Rows(0).Item("AssetOutputLength").ToString)
            End Select
        End If
        Return iTypeLength
    End Function

    Public Function InserZero(ByVal NumZero As Byte) As String
        If NumZero = 0 Then
            InserZero = ""
        Else
            InserZero = "."
            InserZero &= StrDup(NumZero, "0")
        End If
    End Function

    ''' <summary>
    ''' Tìm kiếm mở rộng theo Tiêu thức
    ''' </summary>
    ''' <param name="sSQLSelection">Required. Câu đổ nguồn của combo</param>
    ''' <param name="tdbcFrom">Required. Tiêu thức Từ</param>
    ''' <param name="tdbcTo">Optional. Tiêu thức Đến</param>
    ''' <param name="iModeSelect">Optional. Default. 0: In theo giá trị Từ Đến. 1: In nhiều giá trị</param>
    ''' <returns>Chuỗi tìm kiếm. Khác rỗng khi lấy tập hợp</returns>
    ''' <remarks></remarks>
    Public Function HotKeyF2D91F6020(ByVal sSQLSelection As String, ByRef tdbcFrom As C1.Win.C1List.C1Combo, Optional ByRef tdbcTo As C1.Win.C1List.C1Combo = Nothing, Optional ByVal iModeSelect As Integer = 0) As String
        Dim sKeyID As String = ""
        'Dim f As New D91F6020
        'With f
        '    .SQLSelection = sSQLSelection 'Theo TL phân tích 
        '    .ModeSelect = iModeSelect.ToString
        '    .ShowDialog()
        '    sKeyID = .OutPut01
        '    .Dispose()
        'End With
        Dim arrPro() As StructureProperties = Nothing
        SetProperties(arrPro, "SQLSelection", sSQLSelection)
        SetProperties(arrPro, "ModeSelect", iModeSelect.ToString)
        Dim frm As Form = CallFormShowDialog("D91D0240", "D91F6020", arrPro)
        sKeyID = GetProperties(frm, "ReturnField").ToString
        If sKeyID <> "" Then
            If sKeyID.Substring(0, 1) <> "(" Then
                'Lấy theo giá trị Từ đến:
                '+ Gán lại giá trị cho 2 combo tiêu thức từ đến
                '+ Chuỗi tiêu thức gán bằng rỗng, sSQLOutput1= ""  
                Dim arrResult() As String = sKeyID.Split(";"c)
                tdbcFrom.Text = arrResult(0)

                If tdbcTo IsNot Nothing Then
                    If arrResult.Length = 1 Then
                        tdbcTo.Text = arrResult(0)
                    Else
                        tdbcTo.Text = arrResult(1)
                    End If
                End If

                sKeyID = ""
            Else
                'Lấy theo tập hợp:
                '+ Gán giá trị mặc định cho 2 combo tiêu thức từ đến
                '+ Chuỗi tiêu thức sSQLOutput1= sResult
                tdbcFrom.Text = "%"
                If tdbcTo IsNot Nothing Then tdbcTo.Text = "%"
            End If
        End If
        Return sKeyID
    End Function
End Module
