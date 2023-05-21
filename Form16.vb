Imports MySql.Data.MySqlClient
Imports Mysqlx.XDevAPI.Relational

'datagridview 설정
'RowHeaderVisible = False / EditMode = EditProgrammatically / Selection Mode = FullRowSelect / allowuserto... 전부 = false / startposition = centerscreen

Public Class Form15
    Dim conn_record As New MySqlConnection("server=localhost; Port=3306; database= ssfile; username=root; Password=*****; Convert Zero Datetime=True")
    Dim conn_record2 As New MySqlConnection("server=localhost; Port=3306; database= jigfile; username=root; Password=*****; Convert Zero Datetime=True")

    Public Sub loadrecord()
        Dim recd As String = "SELECT s.ss_serial as 'Silk Screen #', s.cust as 'Customer Code', c.des as 'Customer Name', s.tool as 'Tool #', s.model as 'Model', s.date as 'DATE(제작일자)', s.type as 'Material Type', s.thk as 'Thickness(T)', s.cutting as 'Method Type', s.hc as 'Hole Count', s.memo as 'Memo' FROM ssfile.ss_disposal_table s LEFT OUTER JOIN jigfile.cust_info_table c ON s.cust = c.nic WHERE (s.cust LIKE '%" & ComboBox1.Text & "%') AND (s.tool COLLATE utf8_general_ci LIKE '%" & TextBox1.Text & "%') AND (s.model COLLATE utf8_general_ci LIKE '%" & TextBox2.Text & "%')"
        Dim recd_cmd As New MySqlCommand(recd, conn_record)
        Dim recd_adpt1 As New MySqlDataAdapter(recd_cmd)
        Dim recdtable As New DataTable()

        recdtable.Clear()
        recd_adpt1.Fill(recdtable)
        DataGridView1.DataSource = recdtable

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

    Public Sub loadcbbox()
        Dim recd_adpt2 As New MySqlDataAdapter("SELECT nic FROM cust_info_table", conn_record2)
        Dim recdtable2 As DataTable = New DataTable()
        recd_adpt2.Fill(recdtable2)

        Dim cbval = (From dr In recdtable2 Select nic = dr(0).ToString).ToArray
        ComboBox1.DataSource = Nothing
        ComboBox1.DataSource = cbval
        ComboBox1.SelectedIndex = -1
    End Sub

    Private Sub Form15_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadcbbox()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        loadrecord()
    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick

        If DataGridView1.SelectedRows.Count > 0 Then
            'Dim newForm As New Form13
            'newForm.SelectedRows = DataGridView1.SelectedRows
            Form13.ComboBox1.Text = DataGridView1.CurrentRow.Cells(1).Value.ToString()
            Form13.TextBox1.Text = DataGridView1.CurrentRow.Cells(4).Value.ToString()
            Form13.TextBox2.Text = DataGridView1.CurrentRow.Cells(3).Value.ToString()
            Form13.ComboBox2.Text = DataGridView1.CurrentRow.Cells(6).Value.ToString()
            Form13.ComboBox3.Text = DataGridView1.CurrentRow.Cells(7).Value.ToString()
            Form13.ComboBox4.Text = DataGridView1.CurrentRow.Cells(8).Value.ToString()
            Me.Close()
        End If
    End Sub
End Class
