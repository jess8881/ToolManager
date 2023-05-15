Imports System.Security.Cryptography
Imports MySql.Data.MySqlClient

Public Class Form1

    'Formn.Location = new Point(0,0)이 안 먹히면 폼 디자이너 화면에서 startpoint = manual로 되어있는지 확인해보자..

    'mdicontainer 사이즈 유저가 못 건들게 하려면
    '1) maximumbox. minimumox = false
    '2) maximumsize, minimumsize = (form) size


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Size = New System.Drawing.Size(1000, 600)

        'Me.StartPosition = FormStartPosition.CenterScreen  <-이거 안 먹혀서 디자이너로 centerscreen 설정함;

        'Form3.MdiParent = Me
        'Form4.MdiParent = Me
        'Form5.MdiParent = Me6
        'Form6.MdiParent = Me
        'Form7.MdiParent = Me
    End Sub


    'childform.windowstate = formwindowstate.maximized


    '[지그 조회]메뉴 클릭 시 지그 조회 폼 열기
    Private Sub 지그조회ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 지그조회ToolStripMenuItem.Click

        Form3.MdiParent = Me
        Form3.Location = New Point(0, 0)

        If Application.OpenForms().OfType(Of Form3).Any Then
            Form3.Activate()
        Else
            Form3.Show()
        End If
        'Form3.MdiParent = Me
        'Form3.Location = New Point(0, 0)
        'Form3.Show()
    End Sub

    Private Sub 지그조회jigRetrieval폐기지그ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 지그조회jigRetrieval폐기지그ToolStripMenuItem.Click
        Form8.MdiParent = Me
        Form8.Location = New Point(0, 0)

        If Application.OpenForms().OfType(Of Form8).Any Then
            Form8.Activate()
        Else
            Form8.Show()
        End If
    End Sub

    Private Sub 지그조회ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles 지그조회ToolStripMenuItem1.Click
        Form9.MdiParent = Me
        Form9.Location = New Point(0, 0)

        If Application.OpenForms().OfType(Of Form9).Any Then
            Form9.Activate()
        Else
            Form9.Show()
        End If
    End Sub

    Private Sub 지그등록ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 지그등록ToolStripMenuItem.Click
        Form4.MdiParent = Me
        Form4.Location = New Point(0, 0)

        If Application.OpenForms().OfType(Of Form4).Any Then
            Form4.Activate()
        Else
            Form4.Show()
        End If
    End Sub

    Private Sub 지그폐기ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 지그폐기ToolStripMenuItem.Click
        Form5.MdiParent = Me
        Form5.Location = New Point(0, 0)

        If Application.OpenForms().OfType(Of Form5).Any Then
            Form5.Activate()
        Else
            Form5.Show()
        End If
    End Sub

    Private Sub 재판조회SilkScreenRetrievalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 재판조회SilkScreenRetrievalToolStripMenuItem.Click
        Form10.MdiParent = Me
        Form10.Location = New Point(0, 0)

        If Application.OpenForms().OfType(Of Form10).Any Then
            Form10.Activate()
        Else
            Form10.Show()
        End If
    End Sub

    Private Sub 재판조회SilkScreenRetrieval폐기재판DiscardedSilkScreenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 재판조회SilkScreenRetrieval폐기재판DiscardedSilkScreenToolStripMenuItem.Click
        form11.MdiParent = Me
        form11.Location = New Point(0, 0)

        If Application.OpenForms().OfType(Of form11).Any Then
            form11.Activate()
        Else
            form11.Show()
        End If
    End Sub

    Private Sub 재판조회SilkScreenRettrieval재판넘버SilkScreenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 재판조회SilkScreenRettrieval재판넘버SilkScreenToolStripMenuItem.Click
        Form12.MdiParent = Me
        Form12.Location = New Point(0, 0)

        If Application.OpenForms().OfType(Of Form12).Any Then
            Form12.Activate()
        Else
            Form12.Show()
        End If
    End Sub

    Private Sub 재판등록SilkScreenRegisterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 재판등록SilkScreenRegisterToolStripMenuItem.Click
        Form13.MdiParent = Me
        Form13.Location = New Point(0, 0)

        If Application.OpenForms().OfType(Of Form13).Any Then
            Form13.Activate()
        Else
            Form13.Show()
        End If
    End Sub

    Private Sub 재판폐기SilkScreenDisposalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 재판폐기SilkScreenDisposalToolStripMenuItem.Click
        Form14.MdiParent = Me
        Form14.Location = New Point(0, 0)

        If Application.OpenForms().OfType(Of Form14).Any Then
            Form14.Activate()
        Else
            Form14.Show()
        End If
    End Sub

    Private Sub 고객정보조회ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 고객정보조회ToolStripMenuItem.Click
        Form6.MdiParent = Me
        Form6.Location = New Point(0, 0)

        If Application.OpenForms().OfType(Of Form6).Any Then
            Form6.Activate()
        Else
            Form6.Show()
        End If
    End Sub

    Private Sub 고객정보등록ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 고객정보등록ToolStripMenuItem.Click
        Form7.MdiParent = Me
        Form7.Location = New Point(0, 0)

        If Application.OpenForms().OfType(Of Form7).Any Then
            Form7.Activate()
        Else
            Form7.Show()
        End If
    End Sub

    Private Sub 도움말ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 도움말ToolStripMenuItem.Click
        Form2.MdiParent = Me

        If Application.OpenForms().OfType(Of Form2).Any Then
            Form2.Activate()
        Else
            Form2.Show()
        End If
    End Sub

    Private Sub 지그정보수정ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 지그정보수정ToolStripMenuItem.Click
        form17.MdiParent = Me
        form17.Location = New Point(0, 0)

        If Application.OpenForms().OfType(Of form17).Any Then
            form17.Activate()
        Else
            form17.Show()
        End If
    End Sub

    Private Sub 제판수정SilkScreenModifyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 제판수정SilkScreenModifyToolStripMenuItem.Click
        Form19.MdiParent = Me
        Form19.Location = New Point(0, 0)

        If Application.OpenForms().OfType(Of Form19).Any Then
            Form19.Activate()
        Else
            Form19.Show()
        End If
    End Sub

    Private Sub 고객정보수정CurstomerInformationModifyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 고객정보수정CurstomerInformationModifyToolStripMenuItem.Click
        Form21.MdiParent = Me
        Form21.Location = New Point(0, 0)

        If Application.OpenForms().OfType(Of Form21).Any Then
            Form21.Activate()
        Else
            Form21.Show()
        End If
    End Sub

    Private Sub 고객정보삭제CustomerInoformationDisposalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 고객정보삭제CustomerInoformationDisposalToolStripMenuItem.Click
        Form24.MdiParent = Me
        Form24.Location = New Point(0, 0)

        If Application.OpenForms().OfType(Of Form24).Any Then
            Form24.Activate()
        Else
            Form24.Show()
        End If
    End Sub
End Class
