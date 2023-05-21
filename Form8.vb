Imports MySql.Data.MySqlClient


Public Class Form8

    Dim connection_srch2 As New MySqlConnection("server=localhost; Port=3306; database= jigfile; username=root; Password=erer17*71R; Convert Zero Datetime=True")

    Private Sub search_jig2()

        Try
            '입고일자(x) 폐기일자(x)
            If DateTimePicker1.Text = " " And DateTimePicker2.Text = " " And DateTimePicker3.Text = " " And DateTimePicker4.Text = " " Then
                Dim dsp1 As String = "SELECT j.jig_serial as 'jig #', j.cust, c.des as 'Cust Name', j.tool as 'Tool #', j.model, j.date as 'Date(제작일자)', j.type as 'Material Type', j.thk as 'Thickness(T)', j.cutting as 'Method Type', j.hc as 'Hole Count', j.memo, j.ddate as 'Disposal Date(폐기일자)' FROM jigfile.disposal_table j LEFT OUTER JOIN jigfile.cust_info_table c ON j.cust = c.nic WHERE (j.cust LIKE '%" & ComboBox1.Text & "%') AND (j.tool COLLATE utf8_general_ci LIKE '%" & TextBox1.Text & "%') AND (j.model COLLATE utf8_general_ci LIKE '%" & TextBox2.Text & "%')"
                Dim srch_dsp1 As New MySqlCommand(dsp1, connection_srch2)
                Dim adpt1 As New MySqlDataAdapter(srch_dsp1)
                Dim dsptable1 As New DataTable()
                dsptable1.Clear()

                adpt1.Fill(dsptable1)
                DataGridView1.DataSource = dsptable1

                Label6.Text = "총 " & DataGridView1.RowCount & "  건이 조회되었습니다(There are  " & DataGridView1.RowCount & "  result(s) that match your search"



                '입고일자(x) 폐기일자(o)
            ElseIf DateTimePicker1.Text = " " And DateTimePicker2.Text = " " And DateTimePicker3.Text <> " " And DateTimePicker4.Text <> " " Then
                'datetimepicker format
                Dim dtp1 As DateTime = DateTime.Parse(DateTimePicker3.Text)
                Dim dtp As DateTime = DateTime.Parse(DateTimePicker4.Text)
                Dim dtp2 As DateTime = dtp.Date.AddHours(23).AddMinutes(59).AddSeconds(59)

                Dim dsp2 As String = "SELECT j.jig_serial as 'jig #', j.cust, c.des as 'Cust Name', j.tool as 'Tool #', j.model, j.date as 'Date(제작일자)', j.type as 'Material Type', j.thk as 'Thickness(T)', j.cutting as 'Method Type', j.hc as 'Hole Count', j.memo, j.ddate as 'Disposal Date(폐기일자)' FROM jigfile.disposal_table j LEFT OUTER JOIN jigfile.cust_info_table c ON j.cust = c.nic WHERE (j.cust LIKE '%" & ComboBox1.Text & "%') AND (j.tool COLLATE utf8_general_ci LIKE '%" & TextBox1.Text & "%' ) AND (j.model COLLATE utf8_general_ci LIKE '%" & TextBox2.Text & "%') and (j.ddate BETWEEN '" & dtp1.ToString("yyyy-MM-dd HH:mm:dd") & "' and '" & dtp2.ToString("yyyy-MM-dd HH:mm:dd") & "')"
                Dim srch_dsp2 As New MySqlCommand(dsp2, connection_srch2)
                Dim adt2 As New MySqlDataAdapter(srch_dsp2)
                Dim dsptable2 As New DataTable()
                dsptable2.Clear()

                adt2.Fill(dsptable2)
                DataGridView1.DataSource = dsptable2

                Label6.Text = "총 " & DataGridView1.RowCount & "  건이 조회되었습니다(There are  " & DataGridView1.RowCount & "  result(s) that match your search"



                '입고일자(o) 폐기일자(x)
            ElseIf DateTimePicker1.Text <> " " And DateTimePicker2.Text <> " " And DateTimePicker3.Text = " " And DateTimePicker4.Text = " " Then

                Dim dtp1 As DateTime = DateTime.Parse(DateTimePicker1.Text)
                Dim dtp As DateTime = DateTime.Parse(DateTimePicker2.Text)
                Dim dtp2 As DateTime = dtp.Date.AddHours(23).AddMinutes(59).AddSeconds(59)

                Dim dsp2 As String = "SELECT j.jig_serial as 'jig #', j.cust, c.des as 'Cust Name', j.tool as 'Tool #', j.model, j.date as 'Date(제작일자)', j.type as 'Material Type', j.thk as 'Thickness(T)', j.cutting as 'Method Type', j.hc as 'Hole Count', j.memo, j.ddate as 'Disposal Date(폐기일자)' FROM jigfile.disposal_table j LEFT OUTER JOIN jigfile.cust_info_table c ON j.cust = c.nic WHERE (j.cust LIKE '%" & ComboBox1.Text & "%') AND (j.tool COLLATE utf8_general_ci LIKE '%" & TextBox1.Text & "%' ) AND (j.model COLLATE utf8_general_ci LIKE '%" & TextBox2.Text & "%') and (j.date BETWEEN '" & dtp1.ToString("yyyy-MM-dd HH:mm:ss") & "' and '" & dtp2.ToString("yyyy-MM-dd HH:mm:ss") & "')"
                Dim srch_dsp2 As New MySqlCommand(dsp2, connection_srch2)
                Dim adt2 As New MySqlDataAdapter(srch_dsp2)
                Dim dsptable2 As New DataTable()
                dsptable2.Clear()

                adt2.Fill(dsptable2)
                DataGridView1.DataSource = dsptable2
               
                Label6.Text = "총 " & DataGridView1.RowCount & "  건이 조회되었습니다(There are  " & DataGridView1.RowCount & "  result(s) that match your search"


                '입고일자(o) 폐기일자(o)
            ElseIf DateTimePicker1.Text <> " " And DateTimePicker2.Text <> " " And DateTimePicker3.Text <> " " And DateTimePicker4.Text <> " " Then
                Dim dtp7 As DateTime = DateTime.Parse(DateTimePicker1.Text)
                Dim dtp6 As DateTime = DateTime.Parse(DateTimePicker2.Text)
                Dim dtp8 As DateTime = dtp6.Date.AddHours(23).AddMinutes(59).AddSeconds(59)

                Dim dtp10 As DateTime = DateTime.Parse(DateTimePicker3.Text)
                Dim dtp9 As DateTime = DateTime.Parse(DateTimePicker4.Text)
                Dim dtp11 As DateTime = dtp9.Date.AddHours(23).AddMinutes(59).AddSeconds(59)

                Dim dsp4 As String = "SELECT j.jig_serial as 'jig #', j.cust, c.des as 'Cust Name', j.tool as 'Tool #', j.model, j.date as 'Date(제작일자)', j.type as 'Material Type', j.thk as 'Thickness(T)', j.cutting as 'Method Type', j.hc as 'Hole Count', j.memo, j.ddate as 'Disposal Date(폐기일자)' FROM jigfile.disposal_table j LEFT OUTER JOIN jigfile.cust_info_table c ON j.cust = c.nic WHERE (j.cust LIKE '%" & ComboBox1.Text & "%') AND (j.tool COLLATE utf8_general_ci LIKE '%" & TextBox1.Text & "%' ) AND (j.model COLLATE utf8_general_ci LIKE '%" & TextBox2.Text & "%') and (j.date BETWEEN '" & dtp7.ToString("yyyy-MM-dd HH:mm:ss") & "' and '" & dtp8.ToString("yyyy-MM-dd HH:mm:ss") & "') AND (j.ddate BETWEEN '" & dtp10.ToString("yyyy-MM-dd HH:mm:ss") & "' and '" & dtp11.ToString("yyyy-MM-dd HH:mm:ss") & "')"

                'Dim dsp4 As String = "SELECT j.jig_serial as 'jig #', j.cust, c.des as 'Cust Name', j.tool as 'Tool #', j.model, j.date as 'Date(제작일자)', j.type as 'Material Type', j.thk as 'Thickness(T)', j.cutting as 'Method Type', j.hc as 'Hole Count', j.memo, j.ddate as 'Disposal Date(폐기일자)' FROM jigfile.disposal_table j LEFT OUTER JOIN jigfile.cust_info_table c ON j.cust = c.nic WHERE (cust LIKE '%" & ComboBox1.Text & "%') AND (tool COLLATE utf8_general_ci LIKE '%" & TextBox1.Text & "%' ) AND (model COLLATE utf8_general_ci LIKE '%" & TextBox2.Text & "%') and (date BETWEEN '" & dtp7.ToString("yyyy-MM-dd HH:mm:dd") & "' and '" & dtp8.ToString("yyyy-MM-dd HH:mm:dd") & "') AND (ddate BETWEEN '" & dtp10.ToString("yyyy-MM-dd HH:mm:dd") & "' and '" & dtp11.ToString("yyyy-MM-dd HH:mm:dd") & "')"
                Dim srch_dsp4 As New MySqlCommand(dsp4, connection_srch2)
                Dim adt4 As New MySqlDataAdapter(srch_dsp4)
                Dim dsptable4 As New DataTable()
                dsptable4.Clear()

                adt4.Fill(dsptable4)
                DataGridView1.DataSource = dsptable4

                Label6.Text = "총 " & DataGridView1.RowCount & "  건이 조회되었습니다(There are  " & DataGridView1.RowCount & "  result(s) that match your search"
            Else
                MsgBox("error1")

            End If

        Catch ex As Exception
            MsgBox("오류 발생: " & "'" & ex.Message & "'", 0 - vbOKOnly, "error")
        Finally
            connection_srch2.Close()
            '<DataGridView 설정> ..... 데이터테이블 채운 후에야 컬럼 사이즈 조절 가능
            'RowHeaderVisible = False / EditMode = EditProgrammatically / Selection Mode = FullRowSelect 
            DataGridView1.Columns(0).Width = 70
            DataGridView1.Columns(2).Width = 150
            DataGridView1.Columns(4).Width = 200
            DataGridView1.Columns(5).Width = 150
            DataGridView1.Columns(8).Width = 150
            DataGridView1.Columns(11).Width = 200


        End Try
    End Sub

    Private Sub LoadCbbx()
        DateTimePicker1.CustomFormat = " "
        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker2.CustomFormat = " "
        DateTimePicker2.Format = DateTimePickerFormat.Custom
        DateTimePicker3.CustomFormat = " "
        DateTimePicker3.Format = DateTimePickerFormat.Custom
        DateTimePicker4.CustomFormat = " "
        DateTimePicker4.Format = DateTimePickerFormat.Custom

        Dim adt5 As New MySqlDataAdapter("SELECT nic FROM cust_info_table", connection_srch2)
        Dim dsptable5 As DataTable = New DataTable()
        adt5.Fill(dsptable5)

        Dim custval = (From dr In dsptable5 Select nic = dr(0).ToString).ToArray
        ComboBox1.DataSource = Nothing
        ComboBox1.DataSource = custval
        ComboBox1.SelectedIndex = -1

    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        search_jig2()
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        DateTimePicker1.CustomFormat = "yyyy-MM-dd"
        DateTimePicker2.CustomFormat = "yyyy-MM-dd"
    End Sub

    Private Sub DateTimePicker3_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker3.ValueChanged
        DateTimePicker3.CustomFormat = "yyyy-MM-dd"
        DateTimePicker4.CustomFormat = "yyyy-MM-dd"
    End Sub

    Private Sub Form8_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        connection_srch2.Open()
        LoadCbbx()

    End Sub
End Class
