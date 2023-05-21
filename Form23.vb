Imports MySql.Data.MySqlClient
Imports Mysqlx.XDevAPI.Relational

'startposition = centerscreen 으로 해 놓음
Public Class Form23
    Dim conn_record23 As New MySqlConnection("server=localhost; Port=3306; database= ssfile; username=root; Password=*****; Convert Zero Datetime=True")
    Dim conn_record_sub23 As New MySqlConnection("server=localhost; Port=3306; database= jigfile; username=root; Password=*****; Convert Zero Datetime=True")


    Public Sub loadSSRecord()
        Dim record As String = "SELECT s.ss_serial as 'Silk Screen #', s.cust as 'Customer Code', c.des as 'Customer Name', s.tool as 'Tool #', s.model as 'Model', s.date as 'DATE(제작일자)', s.type as 'Material Type', s.thk as 'Thickness(T)', s.cutting as 'Method Type', s.memo as 'Memo' FROM ssfile.ss_detail s LEFT OUTER JOIN jigfile.cust_info_table c ON s.cust = c.nic WHERE (s.cust LIKE '%" & ComboBox1.Text & "%') AND (s.tool COLLATE utf8_general_ci LIKE '%" & TextBox1.Text & "%') AND (s.model COLLATE utf8_general_ci LIKE '%" & TextBox2.Text & "%')"
        Dim recordCmd As New MySqlCommand(record, conn_record23)
        Dim recordAdpt As New MySqlDataAdapter(recordCmd)
        Dim recordTable As New DataTable()

        recordTable.Clear()
        recordAdpt.Fill(recordTable)
        DataGridView1.DataSource = recordTable

        Label4.Text = "총 " & DataGridView1.RowCount & "  건이 조회되었습니다(There are  " & DataGridView1.RowCount & "  result(s) that match your search)."

        '<DataGridView 설정> ..... 데이터테이블 채운 후에야 컬럼 사이즈 조절 가능
        'RowHeaderVisible = False / EditMode = EditProgrammatically / Selection Mode = FullRowSelect
        '마지막줄에 빈칸이 나오는게 싫으면 allowusertoadd = false. 난 allowuser.. 부분은 다 false 함
        DataGridView1.Columns(0).Width = 70
        DataGridView1.Columns(1).Width = 70
        DataGridView1.Columns(2).Width = 100
        DataGridView1.Columns(3).Width = 150
        DataGridView1.Columns(4).Width = 150
        DataGridView1.Columns(5).Width = 200
        DataGridView1.Columns(6).Width = 150
    End Sub

    Private Sub loadCbbx_SS()
        Dim recordApt2 As New MySqlDataAdapter("SELECT nic FROM cust_info_table", conn_record_sub23)
        Dim recordTable2 As DataTable = New DataTable()
        recordApt2.Fill(recordTable2)

        Dim cbbxVal = (From dr In recordTable2 Select nic = dr(0).ToString).ToArray
        ComboBox1.DataSource = Nothing
        ComboBox1.DataSource = cbbxVal
        ComboBox1.SelectedIndex = -1
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        loadSSRecord()
    End Sub

    Private Sub Form23_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadCbbx_SS()
    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        If DataGridView1.SelectedRows.Count > 0 Then
            Form4.ComboBox1.Text = DataGridView1.CurrentRow.Cells(1).Value.ToString()
            Form4.TextBox2.Text = DataGridView1.CurrentRow.Cells(3).Value.ToString()
            Form4.TextBox1.Text = DataGridView1.CurrentRow.Cells(4).Value.ToString()
            Me.Close()
        End If
    End Sub
End Class
