<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows Form 디자이너에 필요합니다.
    Private components As System.ComponentModel.IContainer

    '참고: 다음 프로시저는 Windows Form 디자이너에 필요합니다.
    '수정하려면 Windows Form 디자이너를 사용하십시오.  
    '코드 편집기에서는 수정하지 마세요.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.지그관리ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.지그조회ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.지그조회jigRetrieval폐기지그ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.지그조회ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.지그등록ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.지그폐기ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.지그정보수정ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.재판관리ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.재판조회SilkScreenRetrievalToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.재판조회SilkScreenRetrieval폐기재판DiscardedSilkScreenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.재판조회SilkScreenRettrieval재판넘버SilkScreenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.재판등록SilkScreenRegisterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.재판폐기SilkScreenDisposalToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.제판수정SilkScreenModifyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.고객정보관리ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.고객정보조회ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.고객정보등록ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.고객정보수정CurstomerInformationModifyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.고객정보삭제CustomerInoformationDisposalToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.도움말ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.지그관리ToolStripMenuItem, Me.재판관리ToolStripMenuItem, Me.고객정보관리ToolStripMenuItem, Me.도움말ToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1004, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        '지그관리ToolStripMenuItem
        '
        Me.지그관리ToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.지그조회ToolStripMenuItem, Me.지그조회jigRetrieval폐기지그ToolStripMenuItem, Me.지그조회ToolStripMenuItem1, Me.ToolStripSeparator1, Me.지그등록ToolStripMenuItem, Me.지그폐기ToolStripMenuItem, Me.지그정보수정ToolStripMenuItem})
        Me.지그관리ToolStripMenuItem.Name = "지그관리ToolStripMenuItem"
        Me.지그관리ToolStripMenuItem.Size = New System.Drawing.Size(169, 20)
        Me.지그관리ToolStripMenuItem.Text = "지그 관리(JIG Management)"
        '
        '지그조회ToolStripMenuItem
        '
        Me.지그조회ToolStripMenuItem.Name = "지그조회ToolStripMenuItem"
        Me.지그조회ToolStripMenuItem.Size = New System.Drawing.Size(343, 22)
        Me.지그조회ToolStripMenuItem.Text = "지그 조회(JIG Retrieval) - 전체 지그(Whole JIG)"
        '
        '지그조회jigRetrieval폐기지그ToolStripMenuItem
        '
        Me.지그조회jigRetrieval폐기지그ToolStripMenuItem.Name = "지그조회jigRetrieval폐기지그ToolStripMenuItem"
        Me.지그조회jigRetrieval폐기지그ToolStripMenuItem.Size = New System.Drawing.Size(343, 22)
        Me.지그조회jigRetrieval폐기지그ToolStripMenuItem.Text = "지그 조회(JIG Retrieval) - 폐기 지그(Discarded JIG)"
        '
        '지그조회ToolStripMenuItem1
        '
        Me.지그조회ToolStripMenuItem1.Name = "지그조회ToolStripMenuItem1"
        Me.지그조회ToolStripMenuItem1.Size = New System.Drawing.Size(343, 22)
        Me.지그조회ToolStripMenuItem1.Text = "지그 조회(JIG Retrieval) - 지그 넘버(JIG #)"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(340, 6)
        '
        '지그등록ToolStripMenuItem
        '
        Me.지그등록ToolStripMenuItem.Name = "지그등록ToolStripMenuItem"
        Me.지그등록ToolStripMenuItem.Size = New System.Drawing.Size(343, 22)
        Me.지그등록ToolStripMenuItem.Text = "지그 등록(JIG Register)"
        '
        '지그폐기ToolStripMenuItem
        '
        Me.지그폐기ToolStripMenuItem.Name = "지그폐기ToolStripMenuItem"
        Me.지그폐기ToolStripMenuItem.Size = New System.Drawing.Size(343, 22)
        Me.지그폐기ToolStripMenuItem.Text = "지그 폐기(JIG Disposal)"
        '
        '지그정보수정ToolStripMenuItem
        '
        Me.지그정보수정ToolStripMenuItem.Name = "지그정보수정ToolStripMenuItem"
        Me.지그정보수정ToolStripMenuItem.Size = New System.Drawing.Size(343, 22)
        Me.지그정보수정ToolStripMenuItem.Text = "지그 정보 수정(jIG Modify)"
        '
        '재판관리ToolStripMenuItem
        '
        Me.재판관리ToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.재판조회SilkScreenRetrievalToolStripMenuItem, Me.재판조회SilkScreenRetrieval폐기재판DiscardedSilkScreenToolStripMenuItem, Me.재판조회SilkScreenRettrieval재판넘버SilkScreenToolStripMenuItem, Me.ToolStripSeparator2, Me.재판등록SilkScreenRegisterToolStripMenuItem, Me.재판폐기SilkScreenDisposalToolStripMenuItem, Me.제판수정SilkScreenModifyToolStripMenuItem})
        Me.재판관리ToolStripMenuItem.Name = "재판관리ToolStripMenuItem"
        Me.재판관리ToolStripMenuItem.Size = New System.Drawing.Size(209, 20)
        Me.재판관리ToolStripMenuItem.Text = "제판관리(Silk Screen Management)"
        '
        '재판조회SilkScreenRetrievalToolStripMenuItem
        '
        Me.재판조회SilkScreenRetrievalToolStripMenuItem.Name = "재판조회SilkScreenRetrievalToolStripMenuItem"
        Me.재판조회SilkScreenRetrievalToolStripMenuItem.Size = New System.Drawing.Size(427, 22)
        Me.재판조회SilkScreenRetrievalToolStripMenuItem.Text = "제판조회(Silk Screen Retrieval) - 전체 제판(Whole Silk Screen) "
        '
        '재판조회SilkScreenRetrieval폐기재판DiscardedSilkScreenToolStripMenuItem
        '
        Me.재판조회SilkScreenRetrieval폐기재판DiscardedSilkScreenToolStripMenuItem.Name = "재판조회SilkScreenRetrieval폐기재판DiscardedSilkScreenToolStripMenuItem"
        Me.재판조회SilkScreenRetrieval폐기재판DiscardedSilkScreenToolStripMenuItem.Size = New System.Drawing.Size(427, 22)
        Me.재판조회SilkScreenRetrieval폐기재판DiscardedSilkScreenToolStripMenuItem.Text = "제판조회(Silk Screen Retrieval) - 폐기 제판(Discarded Silk Screen)"
        '
        '재판조회SilkScreenRettrieval재판넘버SilkScreenToolStripMenuItem
        '
        Me.재판조회SilkScreenRettrieval재판넘버SilkScreenToolStripMenuItem.Name = "재판조회SilkScreenRettrieval재판넘버SilkScreenToolStripMenuItem"
        Me.재판조회SilkScreenRettrieval재판넘버SilkScreenToolStripMenuItem.Size = New System.Drawing.Size(427, 22)
        Me.재판조회SilkScreenRettrieval재판넘버SilkScreenToolStripMenuItem.Text = "제판조회(Silk Screen Rettrieval)  - 제판 넘버(Silk Screen #)"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(424, 6)
        '
        '재판등록SilkScreenRegisterToolStripMenuItem
        '
        Me.재판등록SilkScreenRegisterToolStripMenuItem.Name = "재판등록SilkScreenRegisterToolStripMenuItem"
        Me.재판등록SilkScreenRegisterToolStripMenuItem.Size = New System.Drawing.Size(427, 22)
        Me.재판등록SilkScreenRegisterToolStripMenuItem.Text = "제판 등록(Silk Screen Register)"
        '
        '재판폐기SilkScreenDisposalToolStripMenuItem
        '
        Me.재판폐기SilkScreenDisposalToolStripMenuItem.Name = "재판폐기SilkScreenDisposalToolStripMenuItem"
        Me.재판폐기SilkScreenDisposalToolStripMenuItem.Size = New System.Drawing.Size(427, 22)
        Me.재판폐기SilkScreenDisposalToolStripMenuItem.Text = "제판 폐기(Silk Screen Disposal)"
        '
        '제판수정SilkScreenModifyToolStripMenuItem
        '
        Me.제판수정SilkScreenModifyToolStripMenuItem.Name = "제판수정SilkScreenModifyToolStripMenuItem"
        Me.제판수정SilkScreenModifyToolStripMenuItem.Size = New System.Drawing.Size(427, 22)
        Me.제판수정SilkScreenModifyToolStripMenuItem.Text = "제판 수정(Silk Screen Modify)"
        '
        '고객정보관리ToolStripMenuItem
        '
        Me.고객정보관리ToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.고객정보조회ToolStripMenuItem, Me.고객정보등록ToolStripMenuItem, Me.고객정보수정CurstomerInformationModifyToolStripMenuItem, Me.고객정보삭제CustomerInoformationDisposalToolStripMenuItem})
        Me.고객정보관리ToolStripMenuItem.Name = "고객정보관리ToolStripMenuItem"
        Me.고객정보관리ToolStripMenuItem.Size = New System.Drawing.Size(293, 20)
        Me.고객정보관리ToolStripMenuItem.Text = "고객정보관리(Customer Information Management)"
        '
        '고객정보조회ToolStripMenuItem
        '
        Me.고객정보조회ToolStripMenuItem.Name = "고객정보조회ToolStripMenuItem"
        Me.고객정보조회ToolStripMenuItem.Size = New System.Drawing.Size(329, 22)
        Me.고객정보조회ToolStripMenuItem.Text = "고객정보조회(Customer Information Retrieval)"
        '
        '고객정보등록ToolStripMenuItem
        '
        Me.고객정보등록ToolStripMenuItem.Name = "고객정보등록ToolStripMenuItem"
        Me.고객정보등록ToolStripMenuItem.Size = New System.Drawing.Size(329, 22)
        Me.고객정보등록ToolStripMenuItem.Text = "고객정보등록(Customer Information Register)"
        '
        '고객정보수정CurstomerInformationModifyToolStripMenuItem
        '
        Me.고객정보수정CurstomerInformationModifyToolStripMenuItem.Name = "고객정보수정CurstomerInformationModifyToolStripMenuItem"
        Me.고객정보수정CurstomerInformationModifyToolStripMenuItem.Size = New System.Drawing.Size(329, 22)
        Me.고객정보수정CurstomerInformationModifyToolStripMenuItem.Text = "고객정보수정(Curstomer Information Modify)"
        '
        '고객정보삭제CustomerInoformationDisposalToolStripMenuItem
        '
        Me.고객정보삭제CustomerInoformationDisposalToolStripMenuItem.Name = "고객정보삭제CustomerInoformationDisposalToolStripMenuItem"
        Me.고객정보삭제CustomerInoformationDisposalToolStripMenuItem.Size = New System.Drawing.Size(329, 22)
        Me.고객정보삭제CustomerInoformationDisposalToolStripMenuItem.Text = "고객정보폐기(Customer Inoformation Disposal)"
        '
        '도움말ToolStripMenuItem
        '
        Me.도움말ToolStripMenuItem.Name = "도움말ToolStripMenuItem"
        Me.도움말ToolStripMenuItem.Size = New System.Drawing.Size(88, 20)
        Me.도움말ToolStripMenuItem.Text = "도움말(Help)"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1004, 628)
        Me.Controls.Add(Me.MenuStrip1)
        Me.IsMdiContainer = True
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(1020, 667)
        Me.MinimumSize = New System.Drawing.Size(1020, 667)
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "지그관리"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents 지그관리ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 지그조회ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 지그등록ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 지그폐기ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 지그조회jigRetrieval폐기지그ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 지그조회ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents 재판관리ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 재판조회SilkScreenRetrievalToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 재판조회SilkScreenRetrieval폐기재판DiscardedSilkScreenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 재판조회SilkScreenRettrieval재판넘버SilkScreenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents 재판등록SilkScreenRegisterToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 재판폐기SilkScreenDisposalToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 도움말ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 고객정보관리ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 고객정보조회ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 고객정보등록ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 지그정보수정ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 제판수정SilkScreenModifyToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 고객정보수정CurstomerInformationModifyToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 고객정보삭제CustomerInoformationDisposalToolStripMenuItem As ToolStripMenuItem
End Class
