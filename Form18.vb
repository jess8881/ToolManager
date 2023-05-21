Imports MySql.Data.MySqlClient
Imports System.Globalization
Imports System.Diagnostics.Eventing.Reader

'startposition=centerscreen 해 놓음
Public Class Form18
    Dim conn_ud As New MySqlConnection("server=localhost; Port=3306; database= jigfile; username=root; Password=*****; Convert Zero Datetime=True")


    Private Sub loadCBBX()

        conn_ud.Open()

        Dim dgvval1 As String

        dgvval1 = form17.DataGridView1.CurrentRow.Cells(1).Value.ToString()



        '<콤보박스에 insert>
        Dim load_cmd1 As New MySqlDataAdapter("SELECT des, nic FROM cust_info_table", conn_ud)
        Dim table3 As DataTable = New DataTable()

        'fill the datatable with records from table
        load_cmd1.Fill(table3)

        'insert the default item to datatable
        Dim row1 As DataRow = table3.NewRow()
        row1("des") = 0
        row1("nic") = dgvval1
        table3.Rows.InsertAt(row1, 0)

        'assign datatable as datasource
        ComboBox1.DataSource = Nothing
        ComboBox1.DataSource = table3
        ComboBox1.ValueMember = "des"
        ComboBox1.DisplayMember = "nic" '->없으면 콤보박스에 systems.data.datarowview로 뜸





        ComboBox2.Items.Add("아크릴(acryl)")
        ComboBox2.Items.Add("원판(CCL)")
        ComboBox2.Items.Add("수지(resin)")
        ComboBox2.Items.Add("에폭시(epoxy)")
        ComboBox2.Items.Add("PE")

        ComboBox3.Items.Add("0.8T")
        ComboBox3.Items.Add("1T")
        ComboBox3.Items.Add("2T")
        ComboBox3.Items.Add("3T")
        ComboBox3.Items.Add("4T")
        ComboBox3.Items.Add("5T")

        ComboBox4.Items.Add("드릴(Drill)")
        ComboBox4.Items.Add("라우터(Router)")
        ComboBox4.Items.Add("드릴+라우터(Drill&Router)")


        conn_ud.Close()
    End Sub

    Private Sub UpdateJIG()

        conn_ud.Open()

        Try
            Dim msg1 As String
            msg1 = MsgBox("정말로 등록하시겠습니까?", vbOKCancel, "등록")

            If msg1 = vbOK Then

                '필수필드 모두 충족x
                If String.IsNullOrEmpty(ComboBox1.Text) Or String.IsNullOrEmpty(ComboBox2.Text) Or String.IsNullOrEmpty(TextBox1.Text) Or String.IsNullOrEmpty(TextBox2.Text) Or String.IsNullOrEmpty(ComboBox3.Text) Or String.IsNullOrEmpty(ComboBox4.Text) Then
                    MsgBox("빈 칸을 모두 채워주세요.", 0 - vbOKOnly, "등록 정보 부족")

                Else    '필수필드 모두 충족o


                    Dim update1 As String = "UPDATE jig_detail SET cust='" & ComboBox1.Text & "', tool='" & TextBox1.Text & "', model='" & TextBox2.Text & "', type='" & ComboBox2.Text & "', thk='" & ComboBox3.Text & "', cutting='" & ComboBox4.Text & "', hc='" & TextBox4.Text & "', memo='" & TextBox5.Text & "' WHERE jig_serial ='" & TextBox6.Text & "'"

                    Dim ud_cmd1 As New MySqlCommand(update1, conn_ud)
                    ud_cmd1.ExecuteNonQuery()

                    MsgBox(" 고객명: " & "'" & ComboBox1.Text & "'" & "  TOOL: " & "'" & TextBox1.Text & "'" & "  MODEL#:  " & "'" & TextBox2.Text & "'" & "  지그자재 종류:  " & "'" & ComboBox2.Text & "'" & "  지그자재 두께:  " & "'" & ComboBox3.Text & "'" & "  지그가공타입:  " & "'" & ComboBox4.Text & "'" & "  입고일자:  " & "'" & TextBox3.Text & "'" & "  지그 홀 수:  " & "'" & TextBox4.Text & "'" & "  메모:  " & "'" & TextBox5.Text & "'" & "  (으)로 수정되었습니다", 0 - vbOKOnly, "수정 완료")

                End If

            ElseIf msg1 = vbCancel Then
            End If

        Catch ex As Exception
            MsgBox("수정 오류 발생: " & "'" & ex.Message & "'", 0 - vbOKOnly, "수정 오류")

        Finally
            conn_ud.Close()
        End Try

    End Sub




    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        UpdateJIG()
        Me.Close()
    End Sub



    Private Sub Form18_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadCBBX()
    End Sub
End Class
