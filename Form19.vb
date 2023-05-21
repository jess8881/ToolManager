Imports MySql.Data.MySqlClient


'datagridview 설정
'RowHeaderVisible = False / EditMode = EditProgrammatically / Selection Mode = FullRowSelect / allowuserto... 전부 = false
'jig serial textbox 설정(user input x)
'enabled = false

Public Class Form19
    '지그정보수정(조회)에서 복붙해서 함수변수 이름들이 좀 엉망진창

    Dim conn_record19 As New MySqlConnection("server=localhost; Port=3306; database= ssfile; username=root; Password=*****; Convert Zero Datetime=True")
    Dim conn_record_sub19 As New MySqlConnection("server=localhost; Port=3306; database= jigfile; username=root; Password=*****; Convert Zero Datetime=True")



    '제판 정보 조회
    Public Sub loadSSinfo()

        'connection_srch.Open()
        Try

            If DateTimePicker1.Text = " " And DateTimePicker2.Text = " " Then
                Dim loadSS_19 As String = "SELECT s.ss_serial as 'Silk Screen #', s.cust, c.des as 'Cust Name', s.tool as 'Tool #', s.model, s.date as 'Date(제작일자)', s.type as 'Material Type', s.thk as 'Thickness(T)', s.cutting as 'Method Type', s.hc as 'Hole Count', s.memo FROM ssfile.ss_detail s LEFT OUTER JOIN jigfile.cust_info_table c ON s.cust = c.nic WHERE (s.cust LIKE '%" & ComboBox1.Text & "%') AND ( s.tool COLLATE utf8_general_ci LIKE '%" & TextBox1.Text & "%' ) AND ( s.model COLLATE utf8_general_ci LIKE '%" & TextBox2.Text & "%' )"
                '컬럼 이름(필드 이름, 헤더명)에 띄어쓰기 하고 싶으면 single quotes 가 아니라 bachtick(`) 삽입
                Dim ss_cmd As New MySqlCommand(loadSS_19, conn_record19)

                Dim SSadapter As New MySqlDataAdapter(ss_cmd)
                Dim loadTable As New System.Data.DataTable()
                loadTable.Clear()

                SSadapter.Fill(loadTable)
                DataGridView1.DataSource = loadTable




                Label5.Text = "총 " & DataGridView1.RowCount & "  건이 조회되었습니다(There are  " & DataGridView1.RowCount & "  result(s) that match your search"


            Else
                'jig_detail의 datetime 

                'Datetimepicker Format
                Dim dtp1 As DateTime = DateTime.Parse(DateTimePicker1.Text)
                Dim dtp As DateTime = DateTime.Parse(DateTimePicker2.Text)
                Dim dtp2 As DateTime = dtp.Date.AddHours(23).AddMinutes(59).AddSeconds(59)
                'dim dtpdate1 as dateime = datetime.parse(datetimepicker1.text + "23:59:59")

                Console.WriteLine(dtp1)


                Dim loadSS_19 As String = "SELECT s.ss_serial as 'Silk Screen #', s.cust, c.des as 'Cust Name', s.tool as 'Tool #', s.model, s.date as 'Date(제작일자)', s.type as 'Material Type', s.thk as 'Thickness(T)', s.cutting as 'Method Type', s.hc as 'Hole Count', s.memo FROM ssfile.ss_detail s LEFT OUTER JOIN jigfile.cust_info_table c ON s.cust = c.nic WHERE (s.cust LIKE '%" & ComboBox1.Text & "%') AND ( s.tool COLLATE utf8_general_ci LIKE '%" & TextBox2.Text & "%' ) AND ( s.model COLLATE utf8_general_ci LIKE '%" & TextBox1.Text & "%' ) AND (s.date BETWEEN '" & dtp1.ToString("yyyy-MM-dd HH:mm:dd") & "' and '" & dtp2.ToString("yyyy-MM-dd HH:mm:ss") & "')"    '#를 싱글쿼트로 바꾸고 'ORDER BY DESCENDING' 없애니까 된다..

                '#를 싱글쿼트로 바꾸고 'ORDER BY DESCENDING' 없애니까 된다..
                'WHERE LOWER(table_name) LIKE LOWER('%name%') 로 하면 전부 소문자로 바꿔서 검색 -> 하지만 결과도 전부 소문자로 나온다.
                '그래서 filed_name COLLATE utf8_general_ci LIKE '%var%'로 하면 대소문자 구별없이 나옴. collate가 condition 앞에 나와야 오류가 안 남!
                Dim ss_cmd2 As New MySqlCommand(loadSS_19, conn_record19)

                Dim SSadapter2 As New MySqlDataAdapter(ss_cmd2)
                Dim loadTable2 As New System.Data.DataTable()
                loadTable2.Clear()

                SSadapter2.Fill(loadTable2)
                DataGridView1.DataSource = loadTable2


                Label5.Text = "총 " & DataGridView1.RowCount & "  건이 조회되었습니다(There are  " & DataGridView1.RowCount & "  result(s) that match your search"

                Console.WriteLine(dtp1)
                Console.WriteLine(dtp2)
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

    '고객코드 콤보박스와 DB 연결
    Public Sub loadcbbx()
        'connection_srch.Open()


        'datetimepicker default blank 기본값을 빈 칸으로
        DateTimePicker1.CustomFormat = " "
        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker2.CustomFormat = " "
        DateTimePicker2.Format = DateTimePickerFormat.Custom


        '<콤보박스에 insert>
        Dim adapter_srch As New MySqlDataAdapter("SELECT nic FROM cust_info_table", conn_record_sub19)
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

    'datagridview 더블클릭하여 수정
    Public Sub DatagridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick

        Form20.TextBox6.Text = DataGridView1.CurrentRow.Cells(0).Value.ToString()
        Form20.ComboBox1.Text = DataGridView1.CurrentRow.Cells(1).Value.ToString()
        Form20.TextBox1.Text = DataGridView1.CurrentRow.Cells(3).Value.ToString()
        Form20.TextBox2.Text = DataGridView1.CurrentRow.Cells(4).Value.ToString()
        Form20.ComboBox2.Text = DataGridView1.CurrentRow.Cells(6).Value.ToString()
        Form20.ComboBox3.Text = DataGridView1.CurrentRow.Cells(7).Value.ToString()
        Form20.ComboBox4.Text = DataGridView1.CurrentRow.Cells(8).Value.ToString()
        Form20.TextBox3.Text = DataGridView1.CurrentRow.Cells(5).Value.ToString()
        Form20.TextBox5.Text = DataGridView1.CurrentRow.Cells(10).Value.ToString()
        Form20.Show()


    End Sub


    Private Sub Form10_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadcbbx()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        loadSSinfo()
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        DateTimePicker1.CustomFormat = "yyyy-MM-dd"
        DateTimePicker2.CustomFormat = "yyyy-MM-dd"
        'datetimepicker2_valuchanged에 따로 넣으면 datetimepicker 둘 중 하나만 선택 가능함. 근데 그럴 필요 x같아서 from, to 둘 다 선택해야 검색 가능하도록함.
    End Sub
End Class
