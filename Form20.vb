Imports MySql.Data.MySqlClient
Imports System.Globalization
Imports System.Diagnostics.Eventing.Reader


'startposition = centerscreen 해 놓음
Public Class Form20

    Dim conn_ss As New MySqlConnection("server=localhost; Port=3306; database= ssfile; username=root; Password=*****; Convert Zero Datetime=True")
    Dim conn_ss_sub As New MySqlConnection("server=localhost; Port=3306; database= jigfile; username=root; Password=*****; Convert Zero Datetime=True")

    Private Sub loadCbbx()

        conn_ss.Open()

        Dim custnameVal As String
        custnameVal = Form19.DataGridView1.CurrentRow.Cells(1).Value.ToString()

        '<콤보박스에 insert>
        Dim loadCmd As New MySqlDataAdapter("SELECT des, nic FROM cust_info_table", conn_ss_sub)
        Dim SStable As DataTable = New DataTable()

        'datatable을 DB 내용으로 채우기
        loadCmd.Fill(SStable)

        'insert the default item to datatable
        Dim row As DataRow = SStable.NewRow()
        row("des") = 0
        row("nic") = custnameVal
        SStable.Rows.InsertAt(row, 0)

        'assign datatable as datasource
        ComboBox1.DataSource = Nothing
        ComboBox1.DataSource = SStable
        ComboBox1.ValueMember = "des"
        ComboBox1.DisplayMember = "nic" '->없으면 콤보박스에 systems.data.datarowview로 뜸

        '콤보박스 항목 추가(바인딩 안 한 콤보박스)

        ComboBox2.Items.Add("홀 플러깅(Hole Plugging)")
        ComboBox2.Items.Add("PSR")
        ComboBox2.Items.Add("마킹(Marking)")
        ComboBox2.Items.Add("TC")
        ComboBox2.Items.Add("기타")

        ComboBox3.Items.Add("100")
        ComboBox3.Items.Add("120")
        ComboBox3.Items.Add("150")
        ComboBox3.Items.Add("180")
        ComboBox3.Items.Add("200")
        ComboBox3.Items.Add("250")
        ComboBox3.Items.Add("300")
        ComboBox3.Items.Add("305")
        ComboBox3.Items.Add("350")

        ComboBox4.Items.Add("일반 유제")
        ComboBox4.Items.Add("필름 유제")
        ComboBox4.Items.Add("서스(SUS) 유제")
        ComboBox4.Items.Add("기타 유제")


        conn_ss.Close()

    End Sub

    Private Sub updateSS()
        conn_ss.Open()

        Try
            Dim submitMsg As String
            submitMsg = MsgBox("정말로 등록하시겠습니까?", vbOKCancel, "등록")


            If submitMsg = vbOK Then

                '필수필드가 부족한 경우
                If String.IsNullOrEmpty(ComboBox1.Text) Or String.IsNullOrEmpty(ComboBox2.Text) Or String.IsNullOrEmpty(TextBox1.Text) Or String.IsNullOrEmpty(TextBox2.Text) Or String.IsNullOrEmpty(ComboBox3.Text) Or String.IsNullOrEmpty(ComboBox4.Text) Then
                    MsgBox("필수 입력 항목을 모두 채워주세요.", 0 - vbOKOnly, "필수 정보 부족")

                Else    '필수필드가 모두 충족된 경우

                    Dim update1 As String = "UPDATE ss_detail SET cust='" & ComboBox1.Text & "', tool='" & TextBox1.Text & "', model='" & TextBox2.Text & "', type='" & ComboBox2.Text & "', thk='" & ComboBox3.Text & "', cutting='" & ComboBox4.Text & "', memo='" & TextBox5.Text & "' WHERE ss_serial ='" & TextBox6.Text & "'"

                    Dim ud_cmd1 As New MySqlCommand(update1, conn_ss)
                    ud_cmd1.ExecuteNonQuery()

                    MsgBox(" 고객명: " & "'" & ComboBox1.Text & "'" & "  TOOL: " & "'" & TextBox1.Text & "'" & "  MODEL#:  " & "'" & TextBox2.Text & "'" & "  지그자재 종류:  " & "'" & ComboBox2.Text & "'" & "  지그자재 두께:  " & "'" & ComboBox3.Text & "'" & "  지그가공타입:  " & "'" & ComboBox4.Text & "'" & "  입고일자:  " & "'" & TextBox3.Text & "'" & "  지그 홀 수:  " & "'" & "  메모:  " & "'" & TextBox5.Text & "'" & "  (으)로 수정되었습니다", 0 - vbOKOnly, "수정 완료")

                End If

            ElseIf submitMsg = vbCancel Then
            End If





        Catch ex As Exception
            MsgBox("수정 오류 발생: " & "'" & ex.Message & "'", 0 - vbOKOnly, "수정 오류")
        Finally
            conn_ss.Close()
        End Try
    End Sub


    Private Sub Form20_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadCbbx()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        updateSS()
    End Sub
End Class
