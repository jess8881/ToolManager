Imports System.Data.SqlTypes
Imports System.IO
Imports MySql.Data.MySqlClient
Imports Microsoft.Office.Interop.Excel
Imports System.Drawing.Text
Imports System.Security.Cryptography.X509Certificates

Public Class Form3
    Dim connection_srch As New MySqlConnection("server=localhost; Port=3306; database= jigfile; username=root; Password=*****; Convert Zero Datetime=True")


    'connection open을 안했는데 어케 db에 연결되고 있는거임?????

    Private Sub search_jig()

        'connection_srch.Open()

        If DateTimePicker1.Text = " " And DateTimePicker2.Text = " " Then
            Dim jig_search2 As String = "SELECT j.jig_serial as 'jig #', j.cust, c.des as 'Cust Name', j.tool as 'Tool #', j.model, j.date as 'Date(제작일자)', j.type as 'Material Type', j.thk as 'Thickness(T)', j.cutting as 'Method Type', j.hc as 'Hole Count', j.memo FROM jigfile.jig_detail j LEFT OUTER JOIN jigfile.cust_info_table c ON j.cust = c.nic WHERE (j.cust LIKE '%" & ComboBox1.Text & "%') AND ( j.tool COLLATE utf8_general_ci LIKE '%" & TextBox2.Text & "%' ) AND ( j.model COLLATE utf8_general_ci LIKE '%" & TextBox1.Text & "%' )"
            '컬럼 이름(필드 이름, 헤더명)에 띄어쓰기 하고 싶으면 single quotes 가 아니라 bachtick(`) 삽입
            Dim jig_cmd2 As New MySqlCommand(jig_search2, connection_srch)

            Dim adapter4 As New MySqlDataAdapter(jig_cmd2)
            Dim srchtable2 As New System.Data.DataTable()
            srchtable2.Clear()

            adapter4.Fill(srchtable2)
            DataGridView1.DataSource = srchtable2

            '<DataGridView 설정> ..... 데이터테이블 채운 후에야 컬럼 사이즈 조절 가능
            'RowHeaderVisible = False / EditMode = EditProgrammatically / Selection Mode = FullRowSelect 
            DataGridView1.Columns(0).Width = 40
            DataGridView1.Columns(1).Width = 60
            DataGridView1.Columns(2).Width = 100
            DataGridView1.Columns(3).Width = 200
            DataGridView1.Columns(4).Width = 200


            Label6.Text = "총 " & DataGridView1.RowCount & "  건이 조회되었습니다(There are  " & DataGridView1.RowCount & "  result(s) that match your search"


        Else


            'DateTimePicker1.CustomFormat = "yyyy-MM-dd"
            'DateTimePicker2.CustomFormat = "yyyy-MM-dd"


            'jig_detail의 datetime 

            'DateTimePicker2.MaxDate = DateTime.Today
            'Datetimepicker Format
            Dim dtpdate1 As DateTime = DateTime.Parse(DateTimePicker1.Text)
            Dim dtpdate As DateTime = DateTime.Parse(DateTimePicker2.Text)
            Dim dtpdate2 As DateTime = dtpdate.Date.AddHours(23).AddMinutes(59).AddSeconds(59)
            'dim dtpdate1 as dateime = datetime.parse(datetimepicker1.text + "23:59:59")

            Console.WriteLine(dtpdate1)
            'Dim dtpdate1 As String = DateTimePicker1.Value.Date.ToString("yyyy-MM-dd")
            'Dim dtpdate2 As String = DateTimePicker2.Value.Date.ToString("yyyy-MM-dd")


            Dim jig_search As String = "SELECT j.jig_serial as 'jig #', j.cust, c.des as 'Cust Name', j.tool as 'Tool #', j.model, j.date as 'Date(제작일자)', j.type as 'Material Type', j.thk as 'Thickness(T)', j.cutting as 'Method Type', j.hc as 'Hole Count', j.memo FROM jigfile.jig_detail j LEFT OUTER JOIN jigfile.cust_info_table c ON j.cust = c.nic WHERE (j.cust LIKE '%" & ComboBox1.Text & "%') AND ( j.tool COLLATE utf8_general_ci LIKE '%" & TextBox2.Text & "%' ) AND ( j.model COLLATE utf8_general_ci LIKE '%" & TextBox1.Text & "%' ) AND (j.date BETWEEN '" & dtpdate1.ToString("yyyy-MM-dd HH:mm:dd") & "' and '" & dtpdate2.ToString("yyyy-MM-dd HH:mm:ss") & "')"    '#를 싱글쿼트로 바꾸고 'ORDER BY DESCENDING' 없애니까 된다..

            '#를 싱글쿼트로 바꾸고 'ORDER BY DESCENDING' 없애니까 된다..
            'WHERE LOWER(table_name) LIKE LOWER('%name%') 로 하면 전부 소문자로 바꿔서 검색 -> 하지만 결과도 전부 소문자로 나온다.
            '그래서 filed_name COLLATE utf8_general_ci LIKE '%var%'로 하면 대소문자 구별없이 나옴. collate가 condition 앞에 나와야 오류가 안 남!
            Dim jig_cmd As New MySqlCommand(jig_search, connection_srch)

            Dim adapter3 As New MySqlDataAdapter(jig_cmd)
            Dim srchtable As New System.Data.DataTable()
            srchtable.Clear()

            adapter3.Fill(srchtable)
            DataGridView1.DataSource = srchtable


            '<DataGridView 설정> ..... 데이터테이블 채운 후에야 컬럼 사이즈 조절 가능
            'RowHeaderVisible = False / EditMode = EditProgrammatically / Selection Mode = FullRowSelect 
            DataGridView1.Columns(0).Width = 40
            DataGridView1.Columns(1).Width = 60
            DataGridView1.Columns(2).Width = 100
            DataGridView1.Columns(3).Width = 300
            DataGridView1.Columns(4).Width = 200

            Label6.Text = "총 " & DataGridView1.RowCount & "  건이 조회되었습니다(There are  " & DataGridView1.RowCount & "  result(s) that match your search"

            'connection_srch.Close()
        End If



    End Sub


    Private Sub LoadCombobox_srch()
        'connection_srch.Open()


        'datetimepicker default blank 기본값을 빈 칸으로
        DateTimePicker1.CustomFormat = " "
        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker2.CustomFormat = " "
        DateTimePicker2.Format = DateTimePickerFormat.Custom


        '<콤보박스에 insert>
        Dim adapter_srch As New MySqlDataAdapter("SELECT nic FROM cust_info_table", connection_srch)
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

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCombobox_srch()
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        DateTimePicker1.CustomFormat = "yyyy-MM-dd"
        DateTimePicker2.CustomFormat = "yyyy-MM-dd"
        'datetimepicker2_valuchanged에 따로 넣으면 datetimepicker 둘 중 하나만 선택 가능함. 근데 그럴 필요 x같아서 from, to 둘 다 선택해야 검색 가능하도록함.
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) 
        Dim xlapp As Microsoft.Office.Interop.Excel.Application
        Dim xlwb As Microsoft.Office.Interop.Excel.Workbook
        Dim xlopen As Microsoft.Office.Interop.Excel.Workbook
        Dim xlws As Microsoft.Office.Interop.Excel.Worksheet
        Dim misvalue As Object = System.Reflection.Missing.Value

        Dim i As Integer
        Dim j As Integer
        xlapp = New Microsoft.Office.Interop.Excel.ApplicationClass
        '#에러나서 microsoft.office.interop.excel 참조의 interop 형식을 false로 바꿈
        xlwb = xlapp.Workbooks.Add(misvalue)

        xlws = xlwb.Sheets("sheet1")

        Try


            For i = 0 To DataGridView1.RowCount - 2
                For j = 0 To DataGridView1.ColumnCount - 1
                    For k As Integer = 1 To DataGridView1.Columns.Count
                        xlws.Cells(1, k) = DataGridView1.Columns(k - 1).HeaderText
                        xlws.Cells(i + 2, j + 1) = DataGridView1(j, i).Value.ToString()
                    Next
                Next
            Next
            xlws.Columns.AutoFit() '열 너비 자동 맞춤
            xlopen = xlapp.Workbooks.Open("xlws.xlsx")
            'MsgBox("완료")

        Catch ex As Exception
            MsgBox("Error: " & "'" & ex.Message & "'", 0 - vbOKOnly, "error")


        Finally
        End Try
    End Sub

End Class



