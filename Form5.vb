Imports MySql.Data.MySqlClient
Imports Mysqlx.XDevAPI.Relational

Public Class Form5
Dim connection_dsp As New MySqlConnection("server=localhost; Port=3306; database= jigfile; username=root; Password=*****; Convert Zero Datetime=True")

    Private Sub LoadCombobox_dsp()
        connection_dsp.Open()

        'datetimepicker default blank 기본값을 빈 칸null으로(사실 빈 칸은 아니고 " "space bar)
        DateTimePicker1.CustomFormat = " "
        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker2.CustomFormat = " "
        DateTimePicker2.Format = DateTimePickerFormat.Custom


        '<콤보박스에 insert>
        Dim adapter_dsp As New MySqlDataAdapter("SELECT nic FROM cust_info_table", connection_dsp)
        Dim dt_dsp As DataTable = New DataTable()
        adapter_dsp.Fill(dt_dsp)

        'LINQ를 이용해 CONVERT TO ARRAY
        Dim cmbbxValues = (From dr In dt_dsp Select nic = dr(0).ToString).ToArray

        ComboBox1.DataSource = Nothing   'datasource 속성을 설정하면 items.clear()가 불가능해서 null로 clear함
        'ComboBox1.Items.Clear()
        ComboBox1.DataSource = cmbbxValues
        ComboBox1.SelectedIndex = -1    '콤보박스 첫 칸을 빈칸으로


    End Sub

    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCombobox_dsp()
    End Sub

    Private Sub search_dsp()

        '!connection_dsp.Open()

        '<datagridview 기본 설정>
        Dim columnCheckbox As New DataGridViewCheckBoxColumn
        columnCheckbox.HeaderText = ""
        columnCheckbox.Name = "checkcell"

        'DataGridView1.Columns.Add(columnCheckbox) -> 이거 쓰는 대신 datagridview 디자인에서 미리 체크박스 추가해놔야함/그대로 쓰면 조회누를 때마다 체크박스 컬럼이 계속 생김
        DataGridView1.Columns(0).Width = 20




        'serial(x), dtp(notnull)
        'serial(X), dtp(null)
        'serial(o), dtp(notnull)
        'serial(o), dtp(null)

        If String.IsNullOrEmpty(TextBox1.Text) Then

            If DateTimePicker1.Text <> " " Then
                'Datetimepicker Format
                Dim dtpdate3 As DateTime = DateTime.Parse(DateTimePicker1.Text)
                Dim dtpdate As DateTime = DateTime.Parse(DateTimePicker2.Text)
                Dim dtpdate4 As DateTime = dtpdate.Date.AddHours(23).AddMinutes(59).AddSeconds(59)

                Dim dsp_search1 As String = "SELECT j.jig_serial as 'jig #', j.cust, c.des as 'Cust Name', j.tool as 'Tool #', j.model, j.date as 'Date(제작일자)', j.type as 'Material Type', j.thk as 'Thickness(T)', j.cutting as 'Method Type', j.hc as 'Hole Count', j.memo FROM jigfile.jig_detail j LEFT OUTER JOIN jigfile.cust_info_table c ON j.cust = c.nic WHERE (cust LIKE '%" & ComboBox1.Text & "%') AND (tool COLLATE utf8_general_ci LIKE '%" & TextBox2.Text & "%') AND (model COLLATE utf8_general_ci LIKE '%" & TextBox3.Text & "%') AND date BETWEEN '" & dtpdate3.ToString("yyyy-MM-dd HH:mm:dd") & "' and '" & dtpdate4.ToString("yyyy-MM-dd HH:mm:dd") & "'"
                Dim dsp_cmd3 As New MySqlCommand(dsp_search1, connection_dsp)

                Dim adapter2 As New MySqlDataAdapter(dsp_cmd3)
                Dim dsptable1 As New DataTable()
                dsptable1.Clear()

                adapter2.Fill(dsptable1)
                DataGridView1.DataSource = dsptable1

            ElseIf DateTimePicker1.Text = " " And DateTimePicker2.Text = " " Then
                Dim dsp_search2 As String = "SELECT j.jig_serial as 'jig #', j.cust, c.des as 'Cust Name', j.tool as 'Tool #', j.model, j.date as 'Date(제작일자)', j.type as 'Material Type', j.thk as 'Thickness(T)', j.cutting as 'Method Type', j.hc as 'Hole Count', j.memo FROM jigfile.jig_detail j LEFT OUTER JOIN jigfile.cust_info_table c ON j.cust = c.nic WHERE (cust LIKE '%" & ComboBox1.Text & "%') AND (tool COLLATE utf8_general_ci LIKE '%" & TextBox2.Text & "%') AND (model COLLATE utf8_general_ci LIKE '%" & TextBox3.Text & "%')"
                Dim dsp_cmd4 As New MySqlCommand(dsp_search2, connection_dsp)

                Dim adapter3 As New MySqlDataAdapter(dsp_cmd4)
                Dim dsptable2 As New DataTable()
                dsptable2.Clear()

                adapter3.Fill(dsptable2)
                DataGridView1.DataSource = dsptable2
            End If

        Else
            If DateTimePicker1.Text <> " " Then
                'Datetimepicker Format - 이걸 datetimepicker1.text = " " 인 경우에 두면 유효한 datetime어쩌구가 아니라는 에러 남. 애초에 if 문 나눌 때 isnullorempty가 아니라 dtp= " " 기준으로 나눴으면 두 번 쓸 필요 없었을 듯 ,,ㅋ
                Dim dtpdate3 As DateTime = DateTime.Parse(DateTimePicker1.Text)
                Dim dtpdate As DateTime = DateTime.Parse(DateTimePicker2.Text)
                Dim dtpdate4 As DateTime = dtpdate.Date.AddHours(23).AddMinutes(59).AddSeconds(59)


                Dim dsp_search3 As String = "SELECT j.jig_serial as 'jig #', j.cust, c.des as 'Cust Name', j.tool as 'Tool #', j.model, j.date as 'Date(제작일자)', j.type 'Material Type', j.thk as 'Thickness(T)', j.cutting as 'Method Type', j.hc as 'Hole Count', j.memo FROM jigfile.jig_detail j LEFT OUTER JOIN jigfile.cust_info_table c ON j.cust = nic WHERE (j.jig_serial = " & TextBox1.Text & ") AND (j.cust LIKE '%" & ComboBox1.Text & "%') AND (j.tool COLLATE utf8_general_ci LIKE '%" & TextBox2.Text & "%') AND (model COLLATE utf8_general_ci LIKE '%" & TextBox3.Text & "%') AND j.date BETWEEN '" & dtpdate3.ToString("yyyy-MM-dd") & "' and '" & dtpdate4.ToString("yyyy-MM-dd") & "'"  '#를 싱글쿼트로 바꾸고 'ORDER BY DESCENDING' 없애니까 된다..
               
                'WHERE LOWER(table_name) LIKE LOWER('%name%') 로 하면 전부 소문자로 바꿔서 검색
                Dim dsp_cmd5 As New MySqlCommand(dsp_search3, connection_dsp)

                Dim adapter4 As New MySqlDataAdapter(dsp_cmd5)
                Dim dsptable3 As New DataTable()
                dsptable3.Clear()

                adapter4.Fill(dsptable3)
                DataGridView1.DataSource = dsptable3

            ElseIf DateTimePicker1.Text = " " And DateTimePicker2.Text = " " Then
                Dim dsp_search4 As String = "SELECT j.jig_serial as 'jig #', j.cust, c.des as 'Cust Name', j.tool as 'Tool #', j.model, j.date as 'Date(제작일자)', j.type as 'Material Type', j.thk as 'Thickness(T)', j.cutting as 'Method Type', j.hc as 'Hole Count', j.memo FROM jigfile.jig_detail j LEFT OUTER JOIN jigfile.cust_info_table c ON j.cust = nic WHERE (j.jig_serial = " & TextBox1.Text & ") AND (j.cust LIKE '%" & ComboBox1.Text & "%') AND (j.tool COLLATE utf8_general_ci LIKE '%" & TextBox2.Text & "%') AND (model COLLATE utf8_general_ci LIKE '%" & TextBox3.Text & "%')"
                Dim dsp_cmd5 As New MySqlCommand(dsp_search4, connection_dsp)

                Dim adapter5 As New MySqlDataAdapter(dsp_cmd5)
                Dim dsptable4 As New DataTable()
                dsptable4.Clear()

                adapter5.Fill(dsptable4)
                DataGridView1.DataSource = dsptable4
            End If

        End If

        '<DataGridView 설정> ..... 데이터테이블 채운 후에야 컬럼 사이즈 조절 가능
        'RowHeaderVisible = False / EditMode = EditProgrammatically / Selection Mode = FullRowSelect
        '마지막줄에 빈칸이 나오는게 싫으면 allowusertoadd = false. 난 allowuser.. 부분은 다 false 함
        DataGridView1.Columns(0).Width = 40
        DataGridView1.Columns(1).Width = 60
        DataGridView1.Columns(2).Width = 100
        DataGridView1.Columns(3).Width = 200
        DataGridView1.Columns(4).Width = 200

        Label7.Text = "총 " & DataGridView1.RowCount & "  건이 조회되었습니다(There are  " & DataGridView1.RowCount & "  results that match your search"

    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim ask_dsp As MsgBoxResult
        ask_dsp = MsgBox("정말로 폐기하시겠습니까?", vbOKCancel, "폐기")

        If ask_dsp = vbOK Then

            For Each row As DataGridViewRow In DataGridView1.Rows
                Dim select1 As Boolean = Convert.ToBoolean(row.Cells("checkcell").Value)

                If select1 Then

                    '!connection_dsp.Open()

                    '<1> 선택된 줄들을 현재 날짜, 시간과 함께 disposal_table에 INSERT
                    Dim date1 As DateTime = DateTime.Now
                    Dim date2 As String = date1.ToString("yyyy-MM-dd HH:mm:ss")

                    Dim dsp_insert1 As String = "INSERT INTO disposal_table(jig_serial, cust, tool, model, date, type, thk, cutting, hc, memo, ddate) VALUES ( @jig_serial, @cust, @tool, @model, @date, @type, @thk, @cutting, @hc, @memo, '" & date2 & "')"
                    Dim dsp_cmd1 As New MySqlCommand(dsp_insert1, connection_dsp)


                    'addwithvalue 메소드로 데이터타입 지정 없이 값 넣기
                    dsp_cmd1.Parameters.AddWithValue("@jig_serial", row.Cells("jig #").Value)
                    dsp_cmd1.Parameters.AddWithValue("@cust", row.Cells("cust").Value)
                    dsp_cmd1.Parameters.AddWithValue("@tool", row.Cells("Tool #").Value)
                    dsp_cmd1.Parameters.AddWithValue("@model", row.Cells("model").Value)
                    dsp_cmd1.Parameters.AddWithValue("@date", row.Cells("Date(제작일자)").Value)
                    dsp_cmd1.Parameters.AddWithValue("@type", row.Cells("Material Type").Value)
                    dsp_cmd1.Parameters.AddWithValue("@thk", row.Cells("Thickness(T)").Value)
                    dsp_cmd1.Parameters.AddWithValue("@cutting", row.Cells("Method Type").Value)
                    dsp_cmd1.Parameters.AddWithValue("@hc", row.Cells("Hole Count").Value)
                    dsp_cmd1.Parameters.AddWithValue("@memo", row.Cells("memo").Value)
                    dsp_cmd1.ExecuteNonQuery()


                    '<2> 선택된 줄의 jig serial 넘버를 jig_no_table에 INSERT
                    Dim dsp_insert2 As String = "INSERT INTO jig_no_table(jig_serial) VALUES (@jig_serial)"
                    Dim dsp_cmd2 As New MySqlCommand(dsp_insert2, connection_dsp)

                    dsp_cmd2.Parameters.AddWithValue("@jig_serial", row.Cells("jig #").Value)
                    dsp_cmd2.ExecuteNonQuery()



                    '<3> 선택된 줄을 jig_detail 테이블에서 DELETE
                    Dim dsp_delete As New MySqlCommand("DELETE FROM jig_detail WHERE jig_serial=@jig_serial", connection_dsp)

                    dsp_delete.Parameters.AddWithValue("@jig_serial", row.Cells("jig #").Value)
                    dsp_delete.ExecuteNonQuery()   'sql update, delete, insert 할 때 사용


                End If

            Next
            MsgBox("폐기되었습니다", 0 - vbOKOnly, "폐기 완료")

            '시도1
            'DataGridView1.DataSource = Nothing
            'DataGridView1.Refresh()
            '이렇게 하면 datasource가 지정이 안 되어서 그런지 예상했던 거와 다르게 텅텅 빈 뷰가 나옴

            search_dsp()
            '일케 하면 간단하게 되네.. 도대체 왜임? search 했을 때의 상태가 그대로 저장되는 이유가 머임?


        ElseIf ask_dsp = vbCancel Then

        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        search_dsp()
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        DateTimePicker1.CustomFormat = "yyyy-MM-dd"
        DateTimePicker2.CustomFormat = "yyyy-MM-dd"
    End Sub


End Class
