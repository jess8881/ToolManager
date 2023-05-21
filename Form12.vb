Imports MySql.Data.MySqlClient
Public Class Form12
    Dim connection_jig As New MySqlConnection("server=localhost; Port=3306; database= ssfile; username=root; Password=*****; Convert Zero Datetime=True")


    Public Sub InsFrmDB()

        Try
            Dim TableCount2 As String = "SELECT COUNT(ss_serial) FROM ss_no_table"
            Dim TableCount_cmd2 As New MySqlCommand(TableCount2, connection_jig)
            Dim RowNum2 As Double = TableCount_cmd2.ExecuteScalar()

            '<datagridview 기본 설정>
            'RowHeaderVisible = False / EditMode = EditProgrammatically / Selection Mode = FullRowSelect 

            'ss_detail도 비어있을 경우에는 아무것도 안 뜸 수정!!!!

            If RowNum2 = 0 Then '[1]jig_no_table이 비어있는 경우

                Dim ccc As New MySqlDataAdapter("SELECT MAX(ss_serial) + 1 as 'Silk Screen #' FROM ss_detail", connection_jig)
                Dim ddd As DataTable = New DataTable()
                ddd.Clear()
                ccc.Fill(ddd)
                DataGridView1.DataSource = ddd

                Label3.Text = "총 " & DataGridView1.RowCount & "  건이 조회되었습니다(There are  " & DataGridView1.RowCount & "  result(s) that match your search"



            Else    '[2]jig_no_table이 비어있지 않은 경우

                Dim aaa As New MySqlDataAdapter("SELECT ss_serial as 'Silk Screen #' FROM ss_no_table", connection_jig)
                Dim bbb As DataTable = New DataTable()
                bbb.Clear()
                aaa.Fill(bbb)
                DataGridView1.DataSource = bbb

                Label3.Text = "총 " & DataGridView1.RowCount & "  건이 조회되었습니다(There are  " & DataGridView1.RowCount & "  result(s) that match your search"

            End If


        Catch ex As Exception
            MsgBox("error: " & "'" & ex.Message & "'", 0 - vbOKOnly, "error")

        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        InsFrmDB()
    End Sub

    Private Sub Form12_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        connection_jig.Open()
    End Sub

    Private Sub Form12_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        connection_jig.Close()
    End Sub
End Class
