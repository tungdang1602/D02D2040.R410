<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class D02F3070
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(D02F3070))
        Me.grpFilter = New System.Windows.Forms.GroupBox()
        Me.btnFilter = New System.Windows.Forms.Button()
        Me.tdbcReportType = New C1.Win.C1List.C1Combo()
        Me.lblPeriod = New System.Windows.Forms.Label()
        Me.tdbcReportCode = New C1.Win.C1List.C1Combo()
        Me.tdbcDivisionID = New C1.Win.C1List.C1Combo()
        Me.txtReportName1 = New System.Windows.Forms.TextBox()
        Me.lblReportCode = New System.Windows.Forms.Label()
        Me.lblCommon = New System.Windows.Forms.Label()
        Me.txtDivisionName = New System.Windows.Forms.TextBox()
        Me.tdbcPeriodFrom = New C1.Win.C1List.C1Combo()
        Me.lblDivisionID = New System.Windows.Forms.Label()
        Me.tdbcPeriodTo = New C1.Win.C1List.C1Combo()
        Me.lblReportType = New System.Windows.Forms.Label()
        Me.grpDetail = New System.Windows.Forms.GroupBox()
        Me.tdbg = New C1.Win.C1TrueDBGrid.C1TrueDBGrid()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnsPrint = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnsExportToExcel = New System.Windows.Forms.ToolStripMenuItem()
        Me.grpFilter.SuspendLayout()
        CType(Me.tdbcReportType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tdbcReportCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tdbcDivisionID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tdbcPeriodFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tdbcPeriodTo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpDetail.SuspendLayout()
        CType(Me.tdbg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpFilter
        '
        Me.grpFilter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpFilter.Controls.Add(Me.btnFilter)
        Me.grpFilter.Controls.Add(Me.tdbcReportType)
        Me.grpFilter.Controls.Add(Me.lblPeriod)
        Me.grpFilter.Controls.Add(Me.tdbcReportCode)
        Me.grpFilter.Controls.Add(Me.tdbcDivisionID)
        Me.grpFilter.Controls.Add(Me.txtReportName1)
        Me.grpFilter.Controls.Add(Me.lblReportCode)
        Me.grpFilter.Controls.Add(Me.lblCommon)
        Me.grpFilter.Controls.Add(Me.txtDivisionName)
        Me.grpFilter.Controls.Add(Me.tdbcPeriodFrom)
        Me.grpFilter.Controls.Add(Me.lblDivisionID)
        Me.grpFilter.Controls.Add(Me.tdbcPeriodTo)
        Me.grpFilter.Controls.Add(Me.lblReportType)
        Me.grpFilter.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.grpFilter.Location = New System.Drawing.Point(3, 3)
        Me.grpFilter.Name = "grpFilter"
        Me.grpFilter.Size = New System.Drawing.Size(1002, 77)
        Me.grpFilter.TabIndex = 0
        Me.grpFilter.TabStop = False
        Me.grpFilter.Text = "Điều kiện lọc"
        '
        'btnFilter
        '
        Me.btnFilter.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnFilter.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.btnFilter.Location = New System.Drawing.Point(903, 47)
        Me.btnFilter.Name = "btnFilter"
        Me.btnFilter.Size = New System.Drawing.Size(90, 22)
        Me.btnFilter.TabIndex = 1
        Me.btnFilter.Text = "Lọc"
        Me.btnFilter.UseVisualStyleBackColor = True
        '
        'tdbcReportType
        '
        Me.tdbcReportType.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.tdbcReportType.AllowColMove = False
        Me.tdbcReportType.AllowSort = False
        Me.tdbcReportType.AlternatingRows = True
        Me.tdbcReportType.AutoCompletion = True
        Me.tdbcReportType.AutoDropDown = True
        Me.tdbcReportType.Caption = ""
        Me.tdbcReportType.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.tdbcReportType.ColumnWidth = 100
        Me.tdbcReportType.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.tdbcReportType.DisplayMember = "ReportTypeID"
        Me.tdbcReportType.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown
        Me.tdbcReportType.DropDownWidth = 350
        Me.tdbcReportType.EditorBackColor = System.Drawing.SystemColors.Window
        Me.tdbcReportType.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcReportType.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.tdbcReportType.EmptyRows = True
        Me.tdbcReportType.ExtendRightColumn = True
        Me.tdbcReportType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcReportType.Images.Add(CType(resources.GetObject("tdbcReportType.Images"), System.Drawing.Image))
        Me.tdbcReportType.Location = New System.Drawing.Point(96, 46)
        Me.tdbcReportType.MatchEntryTimeout = CType(2000, Long)
        Me.tdbcReportType.MaxDropDownItems = CType(8, Short)
        Me.tdbcReportType.MaxLength = 32767
        Me.tdbcReportType.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.tdbcReportType.Name = "tdbcReportType"
        Me.tdbcReportType.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.tdbcReportType.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.tdbcReportType.Size = New System.Drawing.Size(128, 21)
        Me.tdbcReportType.TabIndex = 68
        Me.tdbcReportType.ValueMember = "ReportTypeID"
        Me.tdbcReportType.PropBag = resources.GetString("tdbcReportType.PropBag")
        '
        'lblPeriod
        '
        Me.lblPeriod.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPeriod.AutoSize = True
        Me.lblPeriod.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblPeriod.Location = New System.Drawing.Point(694, 23)
        Me.lblPeriod.Name = "lblPeriod"
        Me.lblPeriod.Size = New System.Drawing.Size(19, 13)
        Me.lblPeriod.TabIndex = 67
        Me.lblPeriod.Text = "Kỳ"
        Me.lblPeriod.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tdbcReportCode
        '
        Me.tdbcReportCode.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.tdbcReportCode.AllowColMove = False
        Me.tdbcReportCode.AllowSort = False
        Me.tdbcReportCode.AlternatingRows = True
        Me.tdbcReportCode.AutoCompletion = True
        Me.tdbcReportCode.AutoDropDown = True
        Me.tdbcReportCode.AutoSelect = True
        Me.tdbcReportCode.Caption = ""
        Me.tdbcReportCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.tdbcReportCode.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.tdbcReportCode.DisplayMember = "ReportCode"
        Me.tdbcReportCode.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown
        Me.tdbcReportCode.DropDownWidth = 455
        Me.tdbcReportCode.EditorBackColor = System.Drawing.SystemColors.Window
        Me.tdbcReportCode.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcReportCode.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.tdbcReportCode.EmptyRows = True
        Me.tdbcReportCode.Enabled = False
        Me.tdbcReportCode.ExtendRightColumn = True
        Me.tdbcReportCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcReportCode.Images.Add(CType(resources.GetObject("tdbcReportCode.Images"), System.Drawing.Image))
        Me.tdbcReportCode.Location = New System.Drawing.Point(294, 46)
        Me.tdbcReportCode.MatchEntryTimeout = CType(2000, Long)
        Me.tdbcReportCode.MaxDropDownItems = CType(8, Short)
        Me.tdbcReportCode.MaxLength = 20
        Me.tdbcReportCode.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.tdbcReportCode.Name = "tdbcReportCode"
        Me.tdbcReportCode.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.tdbcReportCode.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.tdbcReportCode.Size = New System.Drawing.Size(142, 21)
        Me.tdbcReportCode.TabIndex = 70
        Me.tdbcReportCode.ValueMember = "ReportCode"
        Me.tdbcReportCode.PropBag = resources.GetString("tdbcReportCode.PropBag")
        '
        'tdbcDivisionID
        '
        Me.tdbcDivisionID.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.tdbcDivisionID.AllowColMove = False
        Me.tdbcDivisionID.AllowSort = False
        Me.tdbcDivisionID.AlternatingRows = True
        Me.tdbcDivisionID.AutoCompletion = True
        Me.tdbcDivisionID.AutoDropDown = True
        Me.tdbcDivisionID.Caption = ""
        Me.tdbcDivisionID.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.tdbcDivisionID.ColumnWidth = 100
        Me.tdbcDivisionID.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.tdbcDivisionID.DisplayMember = "DivisionID"
        Me.tdbcDivisionID.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown
        Me.tdbcDivisionID.DropDownWidth = 350
        Me.tdbcDivisionID.EditorBackColor = System.Drawing.SystemColors.Window
        Me.tdbcDivisionID.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcDivisionID.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.tdbcDivisionID.EmptyRows = True
        Me.tdbcDivisionID.ExtendRightColumn = True
        Me.tdbcDivisionID.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcDivisionID.Images.Add(CType(resources.GetObject("tdbcDivisionID.Images"), System.Drawing.Image))
        Me.tdbcDivisionID.Location = New System.Drawing.Point(96, 18)
        Me.tdbcDivisionID.MatchEntryTimeout = CType(2000, Long)
        Me.tdbcDivisionID.MaxDropDownItems = CType(8, Short)
        Me.tdbcDivisionID.MaxLength = 32767
        Me.tdbcDivisionID.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.tdbcDivisionID.Name = "tdbcDivisionID"
        Me.tdbcDivisionID.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.tdbcDivisionID.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.tdbcDivisionID.Size = New System.Drawing.Size(128, 21)
        Me.tdbcDivisionID.TabIndex = 4
        Me.tdbcDivisionID.ValueMember = "DivisionID"
        Me.tdbcDivisionID.PropBag = resources.GetString("tdbcDivisionID.PropBag")
        '
        'txtReportName1
        '
        Me.txtReportName1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtReportName1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtReportName1.Location = New System.Drawing.Point(442, 46)
        Me.txtReportName1.MaxLength = 250
        Me.txtReportName1.Name = "txtReportName1"
        Me.txtReportName1.ReadOnly = True
        Me.txtReportName1.Size = New System.Drawing.Size(246, 20)
        Me.txtReportName1.TabIndex = 71
        Me.txtReportName1.TabStop = False
        '
        'lblReportCode
        '
        Me.lblReportCode.AutoSize = True
        Me.lblReportCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReportCode.Location = New System.Drawing.Point(232, 50)
        Me.lblReportCode.Name = "lblReportCode"
        Me.lblReportCode.Size = New System.Drawing.Size(47, 13)
        Me.lblReportCode.TabIndex = 72
        Me.lblReportCode.Text = "Báo cáo"
        Me.lblReportCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCommon
        '
        Me.lblCommon.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCommon.AutoSize = True
        Me.lblCommon.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCommon.Location = New System.Drawing.Point(859, 23)
        Me.lblCommon.Name = "lblCommon"
        Me.lblCommon.Size = New System.Drawing.Size(19, 13)
        Me.lblCommon.TabIndex = 66
        Me.lblCommon.Text = "..."
        Me.lblCommon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDivisionName
        '
        Me.txtDivisionName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDivisionName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtDivisionName.Location = New System.Drawing.Point(232, 18)
        Me.txtDivisionName.Name = "txtDivisionName"
        Me.txtDivisionName.ReadOnly = True
        Me.txtDivisionName.Size = New System.Drawing.Size(456, 20)
        Me.txtDivisionName.TabIndex = 6
        Me.txtDivisionName.TabStop = False
        '
        'tdbcPeriodFrom
        '
        Me.tdbcPeriodFrom.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.tdbcPeriodFrom.AllowColMove = False
        Me.tdbcPeriodFrom.AllowSort = False
        Me.tdbcPeriodFrom.AlternatingRows = True
        Me.tdbcPeriodFrom.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tdbcPeriodFrom.AutoCompletion = True
        Me.tdbcPeriodFrom.AutoDropDown = True
        Me.tdbcPeriodFrom.AutoSelect = True
        Me.tdbcPeriodFrom.Caption = ""
        Me.tdbcPeriodFrom.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.tdbcPeriodFrom.ColumnHeaders = False
        Me.tdbcPeriodFrom.ColumnWidth = 100
        Me.tdbcPeriodFrom.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.tdbcPeriodFrom.DisplayMember = "Period"
        Me.tdbcPeriodFrom.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown
        Me.tdbcPeriodFrom.DropDownWidth = 128
        Me.tdbcPeriodFrom.EditorBackColor = System.Drawing.SystemColors.Window
        Me.tdbcPeriodFrom.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcPeriodFrom.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.tdbcPeriodFrom.EmptyRows = True
        Me.tdbcPeriodFrom.ExtendRightColumn = True
        Me.tdbcPeriodFrom.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcPeriodFrom.Images.Add(CType(resources.GetObject("tdbcPeriodFrom.Images"), System.Drawing.Image))
        Me.tdbcPeriodFrom.Location = New System.Drawing.Point(741, 18)
        Me.tdbcPeriodFrom.MatchEntryTimeout = CType(2000, Long)
        Me.tdbcPeriodFrom.MaxDropDownItems = CType(8, Short)
        Me.tdbcPeriodFrom.MaxLength = 32767
        Me.tdbcPeriodFrom.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.tdbcPeriodFrom.Name = "tdbcPeriodFrom"
        Me.tdbcPeriodFrom.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.tdbcPeriodFrom.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.tdbcPeriodFrom.Size = New System.Drawing.Size(109, 21)
        Me.tdbcPeriodFrom.TabIndex = 64
        Me.tdbcPeriodFrom.ValueMember = "Period"
        Me.tdbcPeriodFrom.PropBag = resources.GetString("tdbcPeriodFrom.PropBag")
        '
        'lblDivisionID
        '
        Me.lblDivisionID.AutoSize = True
        Me.lblDivisionID.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblDivisionID.Location = New System.Drawing.Point(10, 21)
        Me.lblDivisionID.Name = "lblDivisionID"
        Me.lblDivisionID.Size = New System.Drawing.Size(38, 13)
        Me.lblDivisionID.TabIndex = 5
        Me.lblDivisionID.Text = "Đơn vị"
        Me.lblDivisionID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tdbcPeriodTo
        '
        Me.tdbcPeriodTo.AddItemSeparator = Global.Microsoft.VisualBasic.ChrW(59)
        Me.tdbcPeriodTo.AllowColMove = False
        Me.tdbcPeriodTo.AllowSort = False
        Me.tdbcPeriodTo.AlternatingRows = True
        Me.tdbcPeriodTo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tdbcPeriodTo.AutoCompletion = True
        Me.tdbcPeriodTo.AutoDropDown = True
        Me.tdbcPeriodTo.AutoSelect = True
        Me.tdbcPeriodTo.Caption = ""
        Me.tdbcPeriodTo.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.tdbcPeriodTo.ColumnHeaders = False
        Me.tdbcPeriodTo.ColumnWidth = 100
        Me.tdbcPeriodTo.DeadAreaBackColor = System.Drawing.Color.Empty
        Me.tdbcPeriodTo.DisplayMember = "Period"
        Me.tdbcPeriodTo.DropdownPosition = C1.Win.C1List.DropdownPositionEnum.LeftDown
        Me.tdbcPeriodTo.DropDownWidth = 128
        Me.tdbcPeriodTo.EditorBackColor = System.Drawing.SystemColors.Window
        Me.tdbcPeriodTo.EditorFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcPeriodTo.EditorForeColor = System.Drawing.SystemColors.WindowText
        Me.tdbcPeriodTo.EmptyRows = True
        Me.tdbcPeriodTo.ExtendRightColumn = True
        Me.tdbcPeriodTo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbcPeriodTo.Images.Add(CType(resources.GetObject("tdbcPeriodTo.Images"), System.Drawing.Image))
        Me.tdbcPeriodTo.Location = New System.Drawing.Point(884, 18)
        Me.tdbcPeriodTo.MatchEntryTimeout = CType(2000, Long)
        Me.tdbcPeriodTo.MaxDropDownItems = CType(8, Short)
        Me.tdbcPeriodTo.MaxLength = 32767
        Me.tdbcPeriodTo.MouseCursor = System.Windows.Forms.Cursors.Default
        Me.tdbcPeriodTo.Name = "tdbcPeriodTo"
        Me.tdbcPeriodTo.RowDivider.Style = C1.Win.C1List.LineStyleEnum.None
        Me.tdbcPeriodTo.RowSubDividerColor = System.Drawing.Color.DarkGray
        Me.tdbcPeriodTo.Size = New System.Drawing.Size(109, 21)
        Me.tdbcPeriodTo.TabIndex = 65
        Me.tdbcPeriodTo.ValueMember = "Period"
        Me.tdbcPeriodTo.PropBag = resources.GetString("tdbcPeriodTo.PropBag")
        '
        'lblReportType
        '
        Me.lblReportType.AutoSize = True
        Me.lblReportType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblReportType.Location = New System.Drawing.Point(10, 50)
        Me.lblReportType.Name = "lblReportType"
        Me.lblReportType.Size = New System.Drawing.Size(69, 13)
        Me.lblReportType.TabIndex = 69
        Me.lblReportType.Text = "Loại báo cáo"
        Me.lblReportType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'grpDetail
        '
        Me.grpDetail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpDetail.Controls.Add(Me.tdbg)
        Me.grpDetail.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.grpDetail.Location = New System.Drawing.Point(3, 83)
        Me.grpDetail.Name = "grpDetail"
        Me.grpDetail.Size = New System.Drawing.Size(1002, 557)
        Me.grpDetail.TabIndex = 1
        Me.grpDetail.TabStop = False
        Me.grpDetail.Text = "Thuyết minh tăng giảm tài sản"
        '
        'tdbg
        '
        Me.tdbg.AllowColMove = False
        Me.tdbg.AllowColSelect = False
        Me.tdbg.AllowFilter = False
        Me.tdbg.AllowRowSizing = C1.Win.C1TrueDBGrid.RowSizingEnum.None
        Me.tdbg.AllowUpdate = False
        Me.tdbg.AlternatingRows = True
        Me.tdbg.ColumnFooters = True
        Me.tdbg.ContextMenuStrip = Me.ContextMenuStrip1
        Me.tdbg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tdbg.EmptyRows = True
        Me.tdbg.ExtendRightColumn = True
        Me.tdbg.FilterBar = True
        Me.tdbg.FlatStyle = C1.Win.C1TrueDBGrid.FlatModeEnum.Standard
        Me.tdbg.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.tdbg.Images.Add(CType(resources.GetObject("tdbg.Images"), System.Drawing.Image))
        Me.tdbg.Location = New System.Drawing.Point(3, 16)
        Me.tdbg.MultiSelect = C1.Win.C1TrueDBGrid.MultiSelectEnum.None
        Me.tdbg.Name = "tdbg"
        Me.tdbg.PreviewInfo.Location = New System.Drawing.Point(0, 0)
        Me.tdbg.PreviewInfo.Size = New System.Drawing.Size(0, 0)
        Me.tdbg.PreviewInfo.ZoomFactor = 75.0R
        Me.tdbg.PrintInfo.PageSettings = CType(resources.GetObject("tdbg.PrintInfo.PageSettings"), System.Drawing.Printing.PageSettings)
        Me.tdbg.Size = New System.Drawing.Size(996, 538)
        Me.tdbg.SplitDividerSize = New System.Drawing.Size(1, 1)
        Me.tdbg.TabAcrossSplits = True
        Me.tdbg.TabAction = C1.Win.C1TrueDBGrid.TabActionEnum.ColumnNavigation
        Me.tdbg.TabIndex = 0
        Me.tdbg.Tag = "COL"
        Me.tdbg.WrapCellPointer = True
        Me.tdbg.PropBag = resources.GetString("tdbg.PropBag")
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnsPrint, Me.mnsExportToExcel})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(153, 70)
        '
        'mnsPrint
        '
        Me.mnsPrint.Name = "mnsPrint"
        Me.mnsPrint.Size = New System.Drawing.Size(152, 22)
        Me.mnsPrint.Text = "&In"
        '
        'mnsExportToExcel
        '
        Me.mnsExportToExcel.Name = "mnsExportToExcel"
        Me.mnsExportToExcel.Size = New System.Drawing.Size(152, 22)
        Me.mnsExportToExcel.Text = "&Xuất Excel"
        '
        'D02F3070
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 645)
        Me.Controls.Add(Me.grpDetail)
        Me.Controls.Add(Me.grpFilter)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "D02F3070"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Truy vÊn thuyÕt minh tŸng gi¶m tªi s¶n - D02F3070"
        Me.grpFilter.ResumeLayout(False)
        Me.grpFilter.PerformLayout()
        CType(Me.tdbcReportType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tdbcReportCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tdbcDivisionID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tdbcPeriodFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tdbcPeriodTo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpDetail.ResumeLayout(False)
        CType(Me.tdbg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents grpFilter As System.Windows.Forms.GroupBox
    Private WithEvents tdbcDivisionID As C1.Win.C1List.C1Combo
    Private WithEvents txtDivisionName As System.Windows.Forms.TextBox
    Private WithEvents lblDivisionID As System.Windows.Forms.Label
    Private WithEvents lblCommon As System.Windows.Forms.Label
    Private WithEvents tdbcPeriodFrom As C1.Win.C1List.C1Combo
    Private WithEvents tdbcPeriodTo As C1.Win.C1List.C1Combo
    Private WithEvents lblPeriod As System.Windows.Forms.Label
    Private WithEvents btnFilter As System.Windows.Forms.Button
    Private WithEvents tdbcReportType As C1.Win.C1List.C1Combo
    Private WithEvents tdbcReportCode As C1.Win.C1List.C1Combo
    Private WithEvents txtReportName1 As System.Windows.Forms.TextBox
    Private WithEvents lblReportCode As System.Windows.Forms.Label
    Private WithEvents lblReportType As System.Windows.Forms.Label
    Private WithEvents grpDetail As System.Windows.Forms.GroupBox
    Private WithEvents tdbg As C1.Win.C1TrueDBGrid.C1TrueDBGrid
    Private WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Private WithEvents mnsExportToExcel As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents mnsPrint As System.Windows.Forms.ToolStripMenuItem
End Class