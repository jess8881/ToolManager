Imports MySql.Data.MySqlClient

Public Class Form6
    Dim connection_srch_cust As New MySqlConnection("server=localhost; Port=3306; database= jigfile; username=root; Password=*****; Convert Zero Datetime=True")

    Private Sub search_cust()

        connection_srch_cust.Open()
        '"SELECT j.jig_serial, j.cust, c.des, j.tool, j.date FROM jigfile.jig_detail j LEFT OUTER JOIN jigfile.cust_info_table c ON j.cust = c.nic WHERE (j.cust LIKE '%" & ComboBox1.Text & "%') AND ( j.tool COLLATE utf8_general_ci LIKE '%" & TextBox2.Text & "%' ) AND (j.date BETWEEN '" & dtpdate1.ToString("yyyy-MM-dd HH:mm:dd") & "' and '" & dtpdate2.ToString("yyyy-MM-dd HH:mm:ss") & "')"    '#를 싱글쿼트로 바꾸고 'ORDER BY DESCENDING' 없애니까 된다..

        Dim cust_search As String = "SELECT serial as 'No.', des as 'Customer Name', nic as 'Customer Code', resp as 'PIC(담당자)', len_wei as 'Unit(단위)', volume as 'Volume(수량)', price as 'Price(단가)', phone1 as 'Contact1(연락처1)', phone2 as 'Contact2(연락처2)', email as 'E-Mail', website as 'Website', pdate as 'Purchase Date(결제일자)' FROM cust_info_Table WHERE (des COLLATE utf8_general_ci LIKE '%" & TextBox1.Text & "%' OR des IS NULL) AND (nic LIKE '%" & ComboBox1.Text & "%') AND (resp LIKE '%" & TextBox2.Text & "%' OR nic IS NULL) "
        'OR IS NULL : cust_info_table의 데이터 중 nic만 있고 des, resp는 null인 row를 검색할 수가 없어서 찾았는데..안 먹힘..
        '근데 생각해보니 des, nic, resp 전부 고객정보등록 할 때 필수 입력 항목이라 null일 수 없으니까 넘어가기로

        Dim cust_cmd As New MySqlCommand(cust_search, connection_srch_cust)

        Dim adapter_cust1 As New MySqlDataAdapter(cust_cmd)
        Dim custtable1 As New DataTable()
        custtable1.Clear()

        adapter_cust1.Fill(custtable1)
        DataGridView1.DataSource = custtable1

        '<DataGridView 설정> ..... 데이터테이블 채운 후에야 컬럼 사이즈 조절 가능
        'RowHeaderVisible = False / EditMode = EditProgrammatically / Selection Mode = FullRowSelect 
        'DataGridView1.Columns(0).Width = 100 -> 어차피 invisible이라 필요x
        DataGridView1.Columns(1).Width = 150
        DataGridView1.Columns(2).Width = 100
        DataGridView1.Columns(3).Width = 150
        DataGridView1.Columns(4).Width = 150
        DataGridView1.Columns(5).Width = 100
        DataGridView1.Columns(6).Width = 100
        DataGridView1.Columns(7).Width = 150
        DataGridView1.Columns(8).Width = 150
        DataGridView1.Columns(9).Width = 150
        DataGridView1.Columns(10).Width = 150
        DataGridView1.Columns(11).Width = 200
        DataGridView1.Columns("No.").Visible = False
        'serial number 보여줄 필요 없으니 select 하기만 하고 가리기

        Label4.Text = "총" & DataGridView1.RowCount & " 건이 조회되었습니다(There are  " & DataGridView1.RowCount & "  results that match your search."

        connection_srch_cust.Close()
    End Sub

    Private Sub loadCombobox_srch_cust()


        connection_srch_cust.Open()

        '<콤보박스에 insert>
        Dim adapter_cust2 As New MySqlDataAdapter("SELECT nic FROM cust_info_table", connection_srch_cust)
        Dim custtable2 As DataTable = New DataTable()


        'custtable2.Columns.Add(New DataColumn(""))
        adapter_cust2.Fill(custtable2)

        'LINQ를 이용해 CONVERT TO ARRAY
        Dim custval = (From dr In custtable2 Select nic = dr(0).ToString).ToArray

        ComboBox1.DataSource = Nothing
        ComboBox1.DataSource = custval
        ComboBox1.SelectedIndex = -1    '콤보박스 첫 칸을 빈칸으로


        connection_srch_cust.Close()

    End Sub

    Private Sub Form6_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadCombobox_srch_cust()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        search_cust()
    End Sub
End Class

