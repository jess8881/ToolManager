Imports MySql.Data.MySqlClient

'[1]jig_no_table이 비어있는 경우
'   <1>jig_detail의 jig_serial# 중 '가장 큰 #' +1 한 '값' 찾기
'   <2>찾은 값을 jig #로 그 값 1개만 뿌림
'[2]jig_no_table이 비어있지 않은 경우
'   <1>jig_no_table 전체 데이터 select


Public Class Form9    Dim connection_jig As New MySqlConnection("server=localhost; Port=3306; database= jigfile; username=root; Password=*****; Convert Zero Datetime=True")


    Public Sub InsFrmDB()

        Try
            Dim TableCount2 As String = "SELECT COUNT(jig_serial) FROM jig_no_table"
            Dim TableCount_cmd2 As New MySqlCommand(TableCount2, connection_jig)
            Dim RowNum2 As Double = TableCount_cmd2.ExecuteScalar()

            '<datagridview 기본 설정>
            'RowHeaderVisible = False / EditMode = EditProgrammatically / Selection Mode = FullRowSelect 

            If RowNum2 = 0 Then '[1]jig_no_table이 비어있는 경우

                Dim ccc As New MySqlDataAdapter("SELECT MAX(jig_serial) + 1 as 'jig #' FROM jig_detail", connection_jig)
                Dim ddd As DataTable = New DataTable()
                ddd.Clear()
                ccc.Fill(ddd)
                DataGridView1.DataSource = ddd

                Label3.Text = "총 " & DataGridView1.RowCount & "  건이 조회되었습니다(There are  " & DataGridView1.RowCount & "  result(s) that match your search"



            Else    '[2]jig_no_table이 비어있지 않은 경우

                Dim aaa As New MySqlDataAdapter("SELECT jig_serial as 'jig #' FROM jig_no_table", connection_jig)
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

    Private Sub Form9_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        connection_jig.Open()
    End Sub

    Private Sub Form9_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        connection_jig.Close()
    End Sub
End Class
