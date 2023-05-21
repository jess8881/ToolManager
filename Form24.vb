Imports MySql.Data.MySqlClient
Imports Mysqlx.XDevAPI.Relational
Public Class Form24
  Dim conn_custInfo As New MySqlConnection("server=localhost; Port=3306; database= jigfile; username=root; Password=*****; Convert Zero Datetime=True")

    Private Sub LoadCbbxCustInfo()
        conn_custInfo.Open()

        '<콤보박스에 insert>
        Dim adapter_cust2 As New MySqlDataAdapter("SELECT nic FROM cust_info_table", conn_custInfo)
        Dim custtable2 As DataTable = New DataTable()


        'custtable2.Columns.Add(New DataColumn(""))
        adapter_cust2.Fill(custtable2)

        'LINQ를 이용해 CONVERT TO ARRAY
        Dim custval = (From dr In custtable2 Select nic = dr(0).ToString).ToArray

        ComboBox1.DataSource = Nothing
        ComboBox1.DataSource = custval
        ComboBox1.SelectedIndex = -1    '콤보박스 첫 칸을 빈칸으로

        conn_custInfo.Close()
    End Sub

    Private Sub LoadCustInfo()
        conn_custInfo.Open()

        '<datagridview 기본 설정>
        Dim colCheckbox As New DataGridViewCheckBoxColumn
        colCheckbox.HeaderText = ""
        colCheckbox.Name = "checkcell"



        Dim cust_search As String = "SELECT serial as 'No.', des as 'Customer Name', nic as 'Customer Code', resp as 'PIC(담당자)', len_wei as 'Unit(단위)', volume as 'Volume(수량)', price as 'Price(단가)', phone1 as 'Contact1(연락처1)', phone2 as 'Contact2(연락처2)', email as 'E-Mail', website as 'Website', pdate as 'Purchase Date(결제일자)' FROM cust_info_Table WHERE (des COLLATE utf8_general_ci LIKE '%" & TextBox1.Text & "%' OR des IS NULL) AND (nic LIKE '%" & ComboBox1.Text & "%') AND (resp LIKE '%" & TextBox2.Text & "%' OR nic IS NULL) "
        
        Dim cust_cmd As New MySqlCommand(cust_search, conn_custInfo)

        Dim adapter_cust1 As New MySqlDataAdapter(cust_cmd)
        Dim custtable1 As New DataTable()
        custtable1.Clear()

        adapter_cust1.Fill(custtable1)
        DataGridView1.DataSource = custtable1

        '<DataGridView 설정> ..... 데이터테이블 채운 후에야 컬럼 사이즈 조절 가능
        'RowHeaderVisible = False / EditMode = EditProgrammatically / Selection Mode = FullRowSelect 
        DataGridView1.Columns(0).Width = 40
        DataGridView1.Columns(1).Width = 60
        DataGridView1.Columns(2).Width = 100
        DataGridView1.Columns(3).Width = 150
        DataGridView1.Columns(4).Width = 150
        DataGridView1.Columns(5).Width = 150
        DataGridView1.Columns(6).Width = 90
        DataGridView1.Columns(7).Width = 90
        DataGridView1.Columns(8).Width = 150
        DataGridView1.Columns(9).Width = 150
        DataGridView1.Columns(10).Width = 150
        DataGridView1.Columns(11).Width = 200
        DataGridView1.Columns(12).Width = 200
        DataGridView1.Columns("No.").Visible = False
        'serial number 보여줄 필요 없으니 select 하기만 하고 가리기

        Label4.Text = "총" & DataGridView1.RowCount & " 건이 조회되었습니다(There are  " & DataGridView1.RowCount & "  results that match your search."

        conn_custInfo.Close()


    End Sub

    Private Sub DelCustinfo()

        conn_custInfo.Open()

        Dim ask_del As MsgBoxResult
        ask_del = MsgBox("한 번 삭제하면 복구할 수 없습니다." & Chr(13) & "정말로 폐기하시겠습니까?", vbOKCancel, "폐기")

        If ask_del = vbOK Then

            For Each row As DataGridViewRow In DataGridView1.Rows
                Dim checked As Boolean = Convert.ToBoolean(row.Cells("checkcell").Value)

                If checked Then

                    '선택된 줄을 jig_detail 테이블에서 DELETE
                    Dim dsp_delete As New MySqlCommand("DELETE FROM cust_info_table WHERE serial=@serial", conn_custInfo)

                    dsp_delete.Parameters.AddWithValue("@serial", row.Cells("No.").Value)
                    dsp_delete.ExecuteNonQuery()   'sql update, delete, insert 할 때 사용

                End If

            Next
            MsgBox("폐기되었습니다", 0 - vbOKOnly, "폐기 완료")

            conn_custInfo.Close()
            '커넥션을 이따구로 열고닫으면 안될 것 같은데,,,,

            LoadCustInfo()



        ElseIf ask_del = vbCancel Then

        End If


        conn_custInfo.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        LoadCustInfo()
    End Sub


    Private Sub Form24_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCbbxCustInfo()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        DelCustinfo()
    End Sub
End Class
