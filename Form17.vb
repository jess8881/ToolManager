Imports MySql.Data.MySqlClient


'datagridview 설정
'RowHeaderVisible = False / EditMode = EditProgrammatically / Selection Mode = FullRowSelect / allowuserto... 전부 = false
'jig serial textbox 설정(user input x)
'enabled = false
Public Class form17
    Dim conn_record As New MySqlConnection("server=localhost; Port=3306; database= jigfile; username=root; Password=*****; Convert Zero Datetime=True")


    Public Sub loadJIGinfo()


        If DateTimePicker1.Text <> " " Then

            Dim dtpdate2 As DateTime = DateTime.Parse(DateTimePicker1.Text)
            Dim dtpdate1 As DateTime = DateTime.Parse(DateTimePicker2.Text)
            Dim dtpdate3 As DateTime = dtpdate1.Date.AddHours(23).AddMinutes(59).AddSeconds(59)

            Dim JIGinfo1 As String = "SELECT j.jig_serial as 'JIG #', j.cust as 'Customer Code', c.des as 'Customer Name', j.tool as 'Tool#', j.model 'Model', j.date as'DATE(제작일자)', j.type as 'Material Type', j.thk as 'Thickness(T)', j.cutting as 'Method Type', j.hc as 'Hole Count', j.memo as 'Memo' FROM jigfile.jig_detail j LEFT OUTER JOIN jigfile.cust_info_table c ON j.cust = c.nic WHERE (j.cust LIKE '%" & ComboBox1.Text & "%') AND (j.tool COLLATE utf8_general_ci LIKE '%" & TextBox1.Text & "%') AND (j.model COLLATE utf8_general_ci LIKE '%" & TextBox2.Text & "%') AND j.date BETWEEN '" & dtpdate2.ToString("yyyy-MM-dd HH:mm:ss") & "' and '" & dtpdate3.ToString("yyyy-MM-dd HH:mm:ss") & "'"

            Dim JIGinfo1_cmd As New MySqlCommand(JIGinfo1, conn_record)
            Dim JIGinfo1_adt1 As New MySqlDataAdapter(JIGinfo1_cmd)
            Dim table1 As New DataTable()
            table1.Clear()

            JIGinfo1_adt1.Fill(table1)
            DataGridView1.DataSource = table1

        ElseIf DateTimePicker1.Text = " " And DateTimePicker2.Text = " " Then
            Dim JIGinfo2 As String = "SELECT j.jig_serial as 'JIG #', j.cust as 'Customer Code', c.des as 'Customer Name', j.tool as 'Tool#', j.model 'Model', j.date as'DATE(제작일자)', j.type as 'Material Type', j.thk as 'Thickness(T)', j.cutting as 'Method Type', j.hc as 'Hole Count', j.memo as 'Memo' FROM jigfile.jig_detail j LEFT OUTER JOIN jigfile.cust_info_table c ON j.cust = c.nic WHERE (j.cust LIKE '%" & ComboBox1.Text & "%') AND (j.tool COLLATE utf8_general_ci LIKE '%" & TextBox1.Text & "%') AND (j.model COLLATE utf8_general_ci LIKE '%" & TextBox2.Text & "%')"

            Dim JIGinfo2_cmd As New MySqlCommand(JIGinfo2, conn_record)
            Dim JIGinfo2_adt1 As New MySqlDataAdapter(JIGinfo2_cmd)
            Dim table2 As New DataTable()

            table2.Clear()
            JIGinfo2_adt1.Fill(table2)
            DataGridView1.DataSource = table2
        End If


        Label5.Text = "총 " & DataGridView1.RowCount & "  건이 조회되었습니다(There are  " & DataGridView1.RowCount & "   result(s) that match your search)."

        DataGridView1.Columns(0).Width = 70
        DataGridView1.Columns(1).Width = 70
        DataGridView1.Columns(2).Width = 100
        DataGridView1.Columns(3).Width = 150
        DataGridView1.Columns(4).Width = 150
        DataGridView1.Columns(5).Width = 200
        DataGridView1.Columns(6).Width = 150
    End Sub

    Public Sub loadcbbx()

        'DATETIMEPICKER DEFAULT BLANK(actually it is null, not blank^_^;)
        DateTimePicker1.CustomFormat = " "
        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker2.CustomFormat = " "
        DateTimePicker2.Format = DateTimePickerFormat.Custom


        Dim adpt1 As New MySqlDataAdapter("SELECT nic FROM cust_info_table", conn_record)
        Dim table1 As DataTable = New DataTable()
        adpt1.Fill(table1)

        Dim cbval1 = (From dr In table1 Select nic = dr(0).ToString).ToArray
        ComboBox1.DataSource = Nothing
        ComboBox1.DataSource = cbval1
        ComboBox1.SelectedIndex = -1
    End Sub




    Public Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick

        If DataGridView1.SelectedRows.Count > 0 Then


            Form18.TextBox6.Text = DataGridView1.CurrentRow.Cells(0).Value.ToString()
            Form18.ComboBox1.Text = DataGridView1.CurrentRow.Cells(1).Value.ToString()
            Form18.TextBox1.Text = DataGridView1.CurrentRow.Cells(3).Value.ToString()
            Form18.TextBox2.Text = DataGridView1.CurrentRow.Cells(4).Value.ToString()
            Form18.ComboBox2.Text = DataGridView1.CurrentRow.Cells(6).Value.ToString()
            Form18.ComboBox3.Text = DataGridView1.CurrentRow.Cells(7).Value.ToString()
            Form18.ComboBox4.Text = DataGridView1.CurrentRow.Cells(8).Value.ToString()
            Form18.TextBox3.Text = DataGridView1.CurrentRow.Cells(5).Value.ToString()
            Form18.TextBox4.Text = DataGridView1.CurrentRow.Cells(9).Value.ToString()
            Form18.TextBox5.Text = DataGridView1.CurrentRow.Cells(10).Value.ToString()
            Form18.Show()


        End If

    End Sub

    Private Sub form17_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadcbbx()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        loadJIGinfo()
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        DateTimePicker1.CustomFormat = "yyyy-MM-dd"
        DateTimePicker2.CustomFormat = "yyyy-MM-dd"
    End Sub
End Class
