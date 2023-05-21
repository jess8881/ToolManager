Imports MySql.Data.MySqlClient

Public Class Form21
    Dim conn_srchCust As New MySqlConnection("server=localhost; Port=3306; database= jigfile; username=root; Password=*****; Convert Zero Datetime=True")

    Private Sub srchCust()

        conn_srchCust.Open()
        Dim srchCust1 As String = "SELECT serial as 'No.', des as 'Customer Name', nic as 'Customer Code', resp as 'PIC(담당자)', len_wei as 'Unit(단위)', volume as 'Volume(수량)', price as 'Price(단가)', phone1 as 'Contact1(연락처1)', phone2 as 'Contact2(연락처2)', email as 'E-Mail', website as 'Website', pdate as 'Purchase Date(결제일자)' FROM cust_info_Table WHERE (des COLLATE utf8_general_ci LIKE '%" & TextBox1.Text & "%' OR des IS NULL) AND (nic LIKE '%" & ComboBox1.Text & "%') AND (resp LIKE '%" & TextBox2.Text & "%' OR nic IS NULL) "
        
        Dim custCmd1 As New MySqlCommand(srchCust1, conn_srchCust)

        Dim adptCust1 As New MySqlDataAdapter(custCmd1)
        Dim custTable1 As New DataTable()
        custTable1.Clear()

        adptCust1.Fill(custTable1)
        DataGridView1.DataSource = custTable1

        '<DataGridView 설정> ..... 데이터테이블 채운 후에야 컬럼 사이즈 조절 가능
        'RowHeaderVisible = False / EditMode = EditProgrammatically / Selection Mode = FullRowSelect 
        '0번째 column은 어차피 가릴 거니까 안 씀
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

        conn_srchCust.Close()

    End Sub

    Private Sub loadCbbx_cust()

        conn_srchCust.Open()

        '<콤보박스에 insert>
        Dim adptCust2 As New MySqlDataAdapter("SELECT nic FROM cust_info_table", conn_srchCust)
        Dim custTable2 As DataTable = New DataTable()

        adptCust2.Fill(custTable2)

        'LINQ를 이용해 CONVERT TO ARRAY
        Dim custVal = (From dr In custTable2 Select nic = dr(0).ToString).ToArray

        ComboBox1.DataSource = Nothing
        ComboBox1.DataSource = custVal
        ComboBox1.SelectedIndex = -1    '콤보박스 첫 칸을 빈칸으로


        conn_srchCust.Close()

    End Sub


    Public Sub DatagridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick

        Form22.TextBox11.Text = DataGridView1.CurrentRow.Cells(0).Value.ToString()
        Form22.TextBox1.Text = DataGridView1.CurrentRow.Cells(1).Value.ToString()
        Form22.TextBox2.Text = DataGridView1.CurrentRow.Cells(2).Value.ToString()
        Form22.TextBox3.Text = DataGridView1.CurrentRow.Cells(3).Value.ToString()
        Form22.ComboBox1.Text = DataGridView1.CurrentRow.Cells(4).Value.ToString()
        Form22.TextBox4.Text = DataGridView1.CurrentRow.Cells(5).Value.ToString()
        Form22.TextBox5.Text = DataGridView1.CurrentRow.Cells(6).Value.ToString()
        Form22.TextBox6.Text = DataGridView1.CurrentRow.Cells(7).Value.ToString()
        Form22.TextBox7.Text = DataGridView1.CurrentRow.Cells(8).Value.ToString()
        Form22.TextBox8.Text = DataGridView1.CurrentRow.Cells(9).Value.ToString()
        Form22.TextBox9.Text = DataGridView1.CurrentRow.Cells(10).Value.ToString()
        Form22.TextBox10.Text = DataGridView1.CurrentRow.Cells(11).Value.ToString()

        Form22.Show()

    End Sub

    Private Sub Form21_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadCbbx_cust()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        srchCust()
    End Sub
End Class
