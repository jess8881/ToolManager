Imports System.Data.SqlTypes
Imports System.IO
Imports MySql.Data.MySqlClient
Imports Microsoft.Office.Interop.Excel
Imports System.Drawing.Text
Imports System.Security.Cryptography.X509Certificates
Imports System.Runtime.CompilerServices

Public Class Form10

    Dim connection_srch As New MySqlConnection("server=localhost; Port=3306; database= ssfile; username=root; Password=*****; Convert Zero Datetime=True")
    Dim connection_srch2 As New MySqlConnection("server=localhost; Port=3306; database= jigfile; username=root; Password=*****; Convert Zero Datetime=True")


    Private Sub search_jig()

        'connection_srch.Open()
        Try

            If DateTimePicker1.Text = " " And DateTimePicker2.Text = " " Then
                Dim jig_search2 As String = "SELECT s.ss_serial as 'Silk Screen #', s.cust, c.des as 'Cust Name', s.tool as 'Tool #', s.model, s.date as 'Date(제작일자)', s.type as 'Material Type', s.thk as 'Thickness(T)', s.cutting as 'Method Type', s.hc as 'Hole Count', s.memo FROM ssfile.ss_detail s LEFT OUTER JOIN jigfile.cust_info_table c ON s.cust = c.nic WHERE (s.cust LIKE '%" & ComboBox1.Text & "%') AND ( s.tool COLLATE utf8_general_ci LIKE '%" & TextBox2.Text & "%' ) AND ( s.model COLLATE utf8_general_ci LIKE '%" & TextBox1.Text & "%' )"
                '컬럼 이름(필드 이름, 헤더명)에 띄어쓰기 하고 싶으면 single quotes 가 아니라 bachtick(`) 삽입
                Dim jig_cmd2 As New MySqlCommand(jig_search2, connection_srch)

                Dim adapter4 As New MySqlDataAdapter(jig_cmd2)
                Dim srchtable2 As New System.Data.DataTable()
                srchtable2.Clear()

                adapter4.Fill(srchtable2)
                DataGridView1.DataSource = srchtable2




                Label6.Text = "총 " & DataGridView1.RowCount & "  건이 조회되었습니다(There are  " & DataGridView1.RowCount & "  result(s) that match your search"


            Else



                'jig_detail의 datetime 

                'Datetimepicker Format
                Dim dtpdate1 As DateTime = DateTime.Parse(DateTimePicker1.Text)
                Dim dtpdate As DateTime = DateTime.Parse(DateTimePicker2.Text)
                Dim dtpdate2 As DateTime = dtpdate.Date.AddHours(23).AddMinutes(59).AddSeconds(59)
        
                Console.WriteLine(dtpdate1)


                Dim jig_search As String = "SELECT s.ss_serial as 'Silk Screen #', s.cust, c.des as 'Cust Name', s.tool as 'Tool #', s.model, s.date as 'Date(제작일자)', s.type as 'Material Type', s.thk as 'Thickness(T)', s.cutting as 'Method Type', s.hc as 'Hole Count', s.memo FROM ssfile.ss_detail s LEFT OUTER JOIN jigfile.cust_info_table c ON s.cust = c.nic WHERE (s.cust LIKE '%" & ComboBox1.Text & "%') AND ( s.tool COLLATE utf8_general_ci LIKE '%" & TextBox2.Text & "%' ) AND ( s.model COLLATE utf8_general_ci LIKE '%" & TextBox1.Text & "%' ) AND (s.date BETWEEN '" & dtpdate1.ToString("yyyy-MM-dd HH:mm:dd") & "' and '" & dtpdate2.ToString("yyyy-MM-dd HH:mm:ss") & "')"    '#를 싱글쿼트로 바꾸고 'ORDER BY DESCENDING' 없애니까 된다..

                '#를 싱글쿼트로 바꾸고 'ORDER BY DESCENDING' 없애니까 된다..
                'WHERE LOWER(table_name) LIKE LOWER('%name%') 로 하면 전부 소문자로 바꿔서 검색 -> 하지만 결과도 전부 소문자로 나온다.
                '그래서 filed_name COLLATE utf8_general_ci LIKE '%var%'로 하면 대소문자 구별없이 나옴. collate가 condition 앞에 나와야 오류가 안 남!
                Dim jig_cmd As New MySqlCommand(jig_search, connection_srch)

                Dim adapter3 As New MySqlDataAdapter(jig_cmd)
                Dim srchtable As New System.Data.DataTable()
                srchtable.Clear()

                adapter3.Fill(srchtable)
                DataGridView1.DataSource = srchtable


                Label6.Text = "총 " & DataGridView1.RowCount & "  건이 조회되었습니다(There are  " & DataGridView1.RowCount & "  result(s) that match your search"

                Console.WriteLine(dtpdate1)
                Console.WriteLine(dtpdate2)
                'connection_srch.Close()
            End If

        Catch ex As Exception
            MsgBox("오류 발생: " & "'" & ex.Message & "'", 0 - vbOKOnly, "오류")
        Finally
            '<DataGridView 설정> ..... 데이터테이블 채운 후에야 컬럼 사이즈 조절 가능
            'RowHeaderVisible = False / EditMode = EditProgrammatically / Selection Mode = FullRowSelect 
            DataGridView1.Columns(0).Width = 70
            DataGridView1.Columns(1).Width = 60
            DataGridView1.Columns(2).Width = 150
            DataGridView1.Columns(3).Width = 200
            DataGridView1.Columns(4).Width = 200
            DataGridView1.Columns(5).Width = 200
        End Try

    End Sub


    Private Sub LoadCombobox_srch()
        'connection_srch.Open()


        'datetimepicker default blank 기본값을 빈 칸으로
        DateTimePicker1.CustomFormat = " "
        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker2.CustomFormat = " "
        DateTimePicker2.Format = DateTimePickerFormat.Custom


        '<콤보박스에 insert>
        Dim adapter_srch As New MySqlDataAdapter("SELECT nic FROM cust_info_table", connection_srch2)
        Dim dt_srch As System.Data.DataTable = New System.Data.DataTable()
        dt_srch.Clear()
        adapter_srch.Fill(dt_srch)

        'LINQ를 이용해 CONVERT TO ARRAY
        Dim custValues = (From dr In dt_srch Select nic = dr(0).ToString).ToArray

        ComboBox1.DataSource = Nothing   'datasource 속성을 설정하면 items.clear()가 불가능해서 null로 clear함
        'ComboBox1.Items.Clear()
        ComboBox1.DataSource = custValues
        ComboBox1.SelectedIndex = -1    '콤보박스 첫 칸을 빈칸으로

        'connection_srch.Close()


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        search_jig()
    End Sub

    Private Sub Form10_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCombobox_srch()
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        DateTimePicker1.CustomFormat = "yyyy-MM-dd"
        DateTimePicker2.CustomFormat = "yyyy-MM-dd"
        'datetimepicker2_valuchanged에 따로 넣으면 datetimepicker 둘 중 하나만 선택 가능함. 근데 그럴 필요 x같아서 from, to 둘 다 선택해야 검색 가능하도록함.
    End Sub
End Class
