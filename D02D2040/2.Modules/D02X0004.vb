''' <summary>
''' Các vấn đề liên quan đến Thông tin hệ thống và Tùy chọn
''' </summary>
Module D02X0004
    ''' <summary>
    ''' Load toàn bộ các thông số tùy chọn vào biến D02Options
    ''' </summary>
    Public Sub LoadOptions()
        With D02Options
            'Kiểm tra tồn tại đường dẫn mới lưu .Net thì lấy dữ liệu, ngược lại thì lấy theo đường dẫn cũ (Lemon3_Dxx)
            'Kiem tra ky cac ten luu xuong cua VB6 de gan vao NET

            If D99C0007.GetModulesSetting(D02, ModuleOption.lmOptions, "MessageAskBeforeSave") = "" Then 'Lay duong dan cu VB6
                Dim D02LocalOptionsLocations As String = "D02"
                Dim Options As String = "Options"

                With D02Options
                    .DefaultDivisionID = GetSetting(D02LocalOptionsLocations, Options, "Division", "")
                    .MessageAskBeforeSave = CType(GetSetting(D02LocalOptionsLocations, Options, "AskBeforeSave", "True"), Boolean)
                    .MessageWhenSaveOK = CType(GetSetting(D02LocalOptionsLocations, Options, "MessageWhenSaveOK", "True"), Boolean)
                    .SaveLastRecent = CType(GetSetting(D02LocalOptionsLocations, Options, "SaveRecentValues", "False"), Boolean)
                    .RoundConvertedAmount = CType(GetSetting(D02LocalOptionsLocations, Options, "RoundConvertedAmount", "False"), Boolean)
                    .LockConvertedAmount = CType(GetSetting(D02LocalOptionsLocations, Options, "LockConvertedAmount", "False"), Boolean)
                    .ViewFormPeriodWhenAppRun = CType(GetSetting(D02LocalOptionsLocations, Options, "AcountingScreen", "False"), Boolean)
                    .ReportLanguage = CType(GetSetting(D02LocalOptionsLocations, Options, "nRPLang", "0"), Byte)
                    .ViewWorkflow = CType(GetSetting(D02LocalOptionsLocations, Options, "ShowDiagramTransaction", "False"), Boolean)
                End With
            Else 'Lấy đường dẫn mới .Net
                With D02Options
                    .DefaultDivisionID = D99C0007.GetModulesSetting(D02, ModuleOption.lmOptions, "DefaultDivisionID", "")
                    .MessageAskBeforeSave = CType(D99C0007.GetModulesSetting(D02, ModuleOption.lmOptions, "MessageAskBeforeSave", "True"), Boolean)
                    .MessageWhenSaveOK = CType(D99C0007.GetModulesSetting(D02, ModuleOption.lmOptions, "MessageWhenSaveOK", "True"), Boolean)
                    .SaveLastRecent = CType(D99C0007.GetModulesSetting(D02, ModuleOption.lmOptions, "SaveLastRecent", "False"), Boolean)
                    .RoundConvertedAmount = CType(D99C0007.GetModulesSetting(D02, ModuleOption.lmOptions, "RoundConvertedAmount", "False"), Boolean)
                    .LockConvertedAmount = CType(D99C0007.GetModulesSetting(D02, ModuleOption.lmOptions, "LockConvertedAmount", "False"), Boolean)
                    .ViewFormPeriodWhenAppRun = CType(D99C0007.GetModulesSetting(D02, ModuleOption.lmOptions, "ViewFormPeriodWhenAppRun", "False"), Boolean)
                    .ReportLanguage = CType(D99C0007.GetModulesSetting(D02, ModuleOption.lmOptions, "ReportLanguage", "0"), Byte)
                    .ViewWorkflow = CType(D99C0007.GetModulesSetting(D02, ModuleOption.lmOptions, "ViewWorkflow", "False"), Boolean)
                End With
            End If
        End With
    End Sub

    ''' <summary>
    ''' Load toàn bộ các thông số format cho biến DxxFormat theo chuẩn chung mới lấy từ D91P9300
    ''' </summary>
    Public Sub LoadFormatsNew()
        Const Number2 As String = "#,##0.00"
        Dim sSQL As String = "Exec D91P9300 "
        Dim dt As DataTable
        dt = ReturnDataTable(sSQL)
        With D02Format
            If dt.Rows.Count > 0 Then
                .ExchangeRate = InsertFormat(dt.Rows(0).Item("ExchangeRateDecimals"))
                .DecimalPlaces = InsertFormat(dt.Rows(0).Item("DecimalPlaces"))
                .MyOriginal = .DecimalPlaces
                .D90_Converted = InsertFormat(dt.Rows(0).Item("D90_ConvertedDecimals"))
                .D07_Quantity = InsertFormat(dt.Rows(0).Item("D07_QuantityDecimals"))
                .D07_UnitCost = InsertFormat(dt.Rows(0).Item("D07_UnitCostDecimals"))
                .D08_Quantity = InsertFormat(dt.Rows(0).Item("D08_QuantityDecimals"))
                .D08_UnitCost = InsertFormat(dt.Rows(0).Item("D08_UnitCostDecimals"))
                .D08_Ratio = InsertFormat(dt.Rows(0).Item("D08_RatioDecimals"))
                .BOMQty = InsertFormat(dt.Rows(0).Item("BOMQtyDecimals"))
                .BOMPrice = InsertFormat(dt.Rows(0).Item("BOMPriceDecimals"))
                .BOMAmt = InsertFormat(dt.Rows(0).Item("BOMAmtDecimals"))
            Else
                .ExchangeRate = Number2
                .D90_Converted = Number2
                .D07_Quantity = Number2
                .D07_UnitCost = Number2
                .D08_Quantity = Number2
                .D08_UnitCost = Number2
                .D08_Ratio = Number2
                .BOMQty = Number2
                .BOMPrice = Number2
                .BOMAmt = Number2
            End If

            .DefaultNumber2 = "#,##0"
            .DefaultNumber2 = Number2
            .DefaultNumber6 = "#,##0.000000"
        End With
    End Sub

    Private Function InsertFormat(ByVal ONumber As Object) As String
        Dim iNumber As Int16 = Convert.ToInt16(ONumber)
        Dim sRet As String = "#,##0"
        If iNumber = 0 Then
        Else
            sRet &= "." & Strings.StrDup(iNumber, "0")
        End If
        Return sRet
    End Function

    Public Function GetOriginalDecimal(ByVal sCurrencyID As String) As String

        Dim sSQL As String
        sSQL = "Select OriginalDecimal From D91V0010 Where CurrencyID = " & SQLString(sCurrencyID)
        Dim dt As DataTable
        dt = ReturnDataTable(sSQL)
        If dt.Rows.Count > 0 Then
            Return InsertFormat(dt.Rows(0).Item("OriginalDecimal"))
        Else
            Return D02Format.DecimalPlaces
        End If
    End Function

    ''' <summary>
    ''' Hỏi trước khi lưu tùy thuộc vào thiết lập ở phần Tùy chọn
    ''' </summary>
    Public Function AskSave() As DialogResult
        If D02Options.MessageAskBeforeSave Then
            Return D99C0008.MsgAskSave()
        Else
            Return DialogResult.Yes
        End If
    End Function

    ''' <summary>
    ''' Thông báo trước khi xóa
    ''' </summary>    
    Public Function AskDelete() As DialogResult
        If D02Options.MessageAskBeforeSave Then
            Return D99C0008.MsgAskDelete
        Else
            Return DialogResult.Yes
        End If
    End Function

    ''' <summary>
    ''' Thông báo khi lưu thành công tùy theo phần thiết lập ở tùy chọn
    ''' </summary>
    Public Sub SaveOK()
        If D02Options.MessageWhenSaveOK Then D99C0008.MsgSaveOK()
    End Sub

    ''' <summary>
    ''' Thông báo sau khi xóa thành công
    ''' </summary>
    Public Sub DeleteOK()
        If D02Options.MessageWhenSaveOK Then D99C0008.MsgL3(rl3("MSG000008"))

    End Sub

    ''' <summary>
    ''' Thông báo không lưu được dữ liệu
    ''' </summary>
    Public Sub SaveNotOK()
        D99C0008.MsgSaveNotOK()
    End Sub

    ''' <summary>
    ''' Thông báo không xóa được dữ liệu
    ''' </summary>
    Public Sub DeleteNotOK()
        'D99C0008.MsgL3("Không xóa được dữ liệu")
        D99C0008.MsgCanNotDelete()
    End Sub


    ''' <summary>
    ''' Load toàn bộ các thống số thiết lập hệ thống vào biến D02Systems
    ''' </summary>
    Public Sub LoadSystems()
        Dim sSQL As String = "Select * From D02T0000"
        Dim dt As DataTable = ReturnDataTable(sSQL)
        If dt.Rows.Count > 0 Then
            With D02Systems
                .DefaultDivisionID = dt.Rows(0).Item("DefaultDivisionID").ToString
                ' TK tài sản
                .DefAssetAccountID = dt.Rows(0).Item("DefAssetAccountID").ToString
                ' TK khấu hao
                .DefDepreciationAccountID = dt.Rows(0).Item("DefDepreciationAccountID").ToString
                ' Nguồn vốn
                .DefSourceID = dt.Rows(0).Item("DefSourceID").ToString
                ' Phân bổ KH
                .DefAssignmentID = dt.Rows(0).Item("DefAssignmentID").ToString
                ' Phương pháp KH
                .MethodID = dt.Rows(0).Item("MethodID").ToString
                ' Xử lý KH kỳ cuối
                .MethodEndID = dt.Rows(0).Item("MethodEndID").ToString
                ' Các bút toán giảm TS
                .DecreaseAsset = L3Bool(dt.Rows(0).Item("MethodEndID"))
                ' Tạo mã tự động cho tài sản cố định
                .AssetAuto = L3Int(dt.Rows(0).Item("AssetAuto"))
                ' Phân loại 1
                .AssetS1Enabled = L3Bool(dt.Rows(0).Item("AssetS1Enabled"))
                ' Phân loại 2
                .AssetS2Enabled = L3Bool(dt.Rows(0).Item("AssetS2Enabled"))
                ' Phân loại 3
                .AssetS3Enabled = L3Bool(dt.Rows(0).Item("AssetS3Enabled"))
                ' AssetS1Default
                .AssetS1Default = dt.Rows(0).Item("AssetS1Default").ToString
                ' AssetS2Default
                .AssetS2Default = dt.Rows(0).Item("AssetS2Default").ToString
                ' AssetS3Default
                .AssetS3Default = dt.Rows(0).Item("AssetS3Default").ToString
                ' S1Length
                .S1Length = L3Int(dt.Rows(0).Item("S1Length"))
                ' S2Length
                .S2Length = L3Int(dt.Rows(0).Item("S2Length"))
                ' S3Length
                .S3Length = L3Int(dt.Rows(0).Item("S3Length"))
                ' Dấu phân cách
                .AssetSeperated = L3Bool(dt.Rows(0).Item("AssetSeperated"))
                ' Dấu phân cách
                .AssetSeperator = dt.Rows(0).Item("AssetSeperator").ToString
                ' Dạng hiển thị
                .AssetOutputOrder = dt.Rows(0).Item("AssetOutputOrder").ToString
                ' Độ dài số
                .AutoNumberLength = L3Int(dt.Rows(0).Item("AutoNumberLength"))
                ' Độ dài mã
                .AssetOutputLength = L3Int(dt.Rows(0).Item("AssetOutputLength"))
                'Tạo mã tự động Incident 69247
                .CIPAuto = L3Bool(dt.Rows(0).Item("CIPAuto"))
                .IsAssetIDForD02D43 = L3Bool(dt.Rows(0).Item("IsAssetIDForD02D43"))
                .UseProperty = L3Bool(dt.Rows(0).Item("UseProperty"))
            End With
            dt.Dispose()
        Else
            With D02Systems
                .DefaultDivisionID = ""
                ' TK tài sản
                .DefAssetAccountID = ""
                ' TK khấu hao
                .DefDepreciationAccountID = ""
                ' Nguồn vốn
                .DefSourceID = ""
                ' Phân bổ KH
                .DefAssignmentID = ""
                ' Phương pháp KH
                .MethodID = ""
                ' Xử lý KH kỳ cuối
                .MethodEndID = ""
                ' Các bút toán giảm TS
                .DecreaseAsset = False
                ' Tạo mã tự động cho tài sản cố định
                .AssetAuto = 0
                ' Phân loại 1
                .AssetS1Enabled = False
                ' Phân loại 2
                .AssetS2Enabled = False
                ' Phân loại 3
                .AssetS3Enabled = False
                ' AssetS1Default
                .AssetS1Default = ""
                ' AssetS2Default
                .AssetS2Default = ""
                ' AssetS3Default
                .AssetS3Default = ""
                ' S1Length
                .S1Length = 0
                ' S2Length
                .S2Length = 0
                ' S3Length
                .S3Length = 0
                ' Dấu phân cách
                .AssetSeperated = False
                ' Dấu phân cách
                .AssetSeperator = ""
                ' Dạng hiển thị
                .AssetOutputOrder = ""
                ' Độ dài số
                .AutoNumberLength = 0
                ' Độ dài mã
                .AssetOutputLength = 0
            End With
        End If
    End Sub
End Module
