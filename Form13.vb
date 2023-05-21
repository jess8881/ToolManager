Imports System.Diagnostics.Eventing.Reader
Imports System.Globalization
Imports MySql.Data.MySqlClient
Imports Mysqlx.XDevAPI.Relational

Public Class Form13
    Dim connection_reg As New MySqlConnection("server=localhost; Port=3306; database= ssfile; username=root; Password=*****; Convert Zero Datetime=True")
    Dim connection_reg2 As New MySqlConnection("server=localhost; Port=3306; database= jigfile; username=root; Password=*****; Convert Zero Datetime=True")


    Public Sub EmptyCek()

        connection_reg2.Open()

        '<콤보박스에 insert>
        Dim adapter_cmd As New MySqlDataAdapter("SELECT nic FROM cust_info_table", connection_reg2)
        Dim dt_cmd As DataTable = New DataTable()
        adapter_cmd.Fill(dt_cmd)

        'LINQ를 이용해 CONVERT TO ARRAY
        Dim custValues = (From dr In dt_cmd Select nic = dr(0).ToString).ToArray

        ComboBox1.DataSource = Nothing   'datasource 속성을 설정하면 items.clear()가 불가능해서 null로 clear함
        'ComboBox1.Items.Clear()
        ComboBox1.DataSource = custValues
        ComboBox1.SelectedIndex = -1    '콤보박스 첫 칸을 빈칸으로
        connection_reg2.Close()


    End Sub


    Public Sub EmptyCase(ByRef NewSerial As Double)

        '<1>ss_detail의 jig_serial# 중 '가장 큰 #' +1 한 '값' 구하기
        Dim maxnum As String = "SELECT MAX(ss_serial) FROM ss_detail"
        Dim max_cmd As New MySqlCommand(maxnum, connection_reg)
        Dim MaxSerial As Double = max_cmd.ExecuteScalar()

        NewSerial = MaxSerial + 1

    End Sub

    Public Sub NotEmptyCase(ByRef MinSerial As Double)

        '<2>ss_no_table에서 가장 작은 시리얼# SELECT
        Dim minnum As String = "SELECT MIN(ss_serial) FROM ss_no_table"
        Dim min_cmd As New MySqlCommand(minnum, connection_reg)

        MinSerial = min_cmd.ExecuteScalar()

    End Sub


    Private Sub RegToDB()

        connection_reg.Open()

        Dim TableCount As String = "SELECT COUNT(ss_serial) FROM ss_no_table"
        Dim TableCount_cmd As New MySqlCommand(TableCount, connection_reg)
        Dim RowNum As Double = TableCount_cmd.ExecuteScalar()    '카운트한 결과값 받기


        Dim DetailCount As String = "SELECT COUNT(ss_serial) FROM ss_detail"
        Dim DC_cmd As New MySqlCommand(DetailCount, connection_reg)
        Dim DC As Double = DC_cmd.ExecuteScalar()


        Try
            Dim MsgRes1 As String
            MsgRes1 = MsgBox("정말로 등록하시겠습니까?", vbOKCancel, "등록")

            If MsgRes1 = vbOK Then



                '필수필드 모두 충족 x
                If String.IsNullOrEmpty(ComboBox1.Text) Or String.IsNullOrEmpty(ComboBox2.Text) Or String.IsNullOrEmpty(TextBox2.Text) Or String.IsNullOrEmpty(TextBox1.Text) Or String.IsNullOrEmpty(ComboBox2.Text) Or String.IsNullOrEmpty(ComboBox3.Text) Or String.IsNullOrEmpty(ComboBox4.Text) Then
                    MsgBox("빈 칸을 모두 채워주세요.", 0 - vbOKOnly, "등록 정보 부족")
                Else

                    '입고일자 x
                    If String.IsNullOrEmpty(TextBox3.Text) Then

                        'ss_detail이 비어있지 않은 경우
                        If DC <> 0 Then



                            'ss_no_table이 비어있지 않은 경우...(1)
                            If RowNum <> 0 Then
                                '이 호출을 RegToDB 처음 라인에 넣으면 dbnull을 double로 변환할 수 없다고 에러 뜸

                                Dim MinSerial As Double
                                Call NotEmptyCase(MinSerial)

                                Dim nempty_regDate1 As DateTime = DateTime.Now
                                Dim nempty_regDate2 As String = nempty_regDate1.ToString("yyyy-MM-dd HH:mm:ss")
                                Dim reg_sql2 As String
                                reg_sql2 = "INSERT INTO ss_detail(ss_serial, cust, tool, model, date, type, thk, cutting, memo) VALUES (" & "'" & MinSerial & "'" & "," & "'" & ComboBox1.Text & "'" & "," & "'" & TextBox2.Text & "'" & "," & "'" & TextBox1.Text & "'" & "," & "'" & nempty_regDate2 & "'" & "," & "'" & ComboBox2.Text & "'" & "," & "'" & ComboBox3.Text & "'" & "," & "'" & ComboBox4.Text & "'" & "," & "'" & TextBox4.Text & "'" & ")"
                                Dim reg_cmd2 As New MySqlCommand(reg_sql2, connection_reg)
                                Console.WriteLine(reg_sql2)
                                reg_cmd2.ExecuteNonQuery()

                                Dim del_sql2 As String = "DELETE FROM ss_no_table WHERE ss_serial =" & "'" & MinSerial & "'"           'double quote to string -> "chr(34)
                                Dim reg_cmd2_2 As New MySqlCommand(del_sql2, connection_reg)
                                reg_cmd2_2.ExecuteNonQuery()


                                MsgBox("시리얼 넘버: " & "'" & MinSerial & "'" & " 고객명: " & "'" & ComboBox1.Text & "'" & "  TOOL: " & "'" & TextBox2.Text & "'" & "  MODEL#:  " & "'" & TextBox1.Text & "'" & "  지그자재 종류:  " & "'" & ComboBox2.Text & "'" & "  지그자재 두께:  " & "'" & ComboBox3.Text & "'" & "  지그가공타입:  " & "'" & ComboBox4.Text & "'" & "  지그 홀 수:  " & "  메모:  " & "'" & TextBox4.Text & "'" & "  (으)로 등록되었습니다", 0 - vbOKOnly, "등록 완료")
                                TextBox1.Clear()
                                TextBox2.Clear()
                                TextBox3.Clear()
                                TextBox4.Clear()
                                ComboBox2.SelectedIndex = 0
                                ComboBox3.SelectedIndex = 2
                                ComboBox4.SelectedIndex = 0

                            Else    'ss_no_table이 비어있는 경우...(2)
                                Dim NewSerial As Double
                                Call EmptyCase(NewSerial)

                                Dim empty_regDate1 As DateTime = DateTime.Now
                                Dim empty_regDate2 As String = empty_regDate1.ToString("yyyy-MM-dd HH:mm:ss")
                                Dim reg_sql1 As String


                                reg_sql1 = "INSERT INTO ss_detail(ss_serial, cust, tool, model, date, type, thk, cutting, memo) VALUES (" & "'" & NewSerial & "'" & "," & "'" & ComboBox1.Text & "'" & "," & "'" & TextBox2.Text & "'" & "," & "'" & TextBox1.Text & "'" & "," & "'" & empty_regDate2 & "'" & "," & "'" & ComboBox2.Text & "'" & "," & "'" & ComboBox3.Text & "'" & "," & "'" & ComboBox4.Text & "'" & "," & "'" & TextBox4.Text & "'" & ")"


                                Dim reg_cmd1 As New MySqlCommand(reg_sql1, connection_reg)
                                reg_cmd1.ExecuteNonQuery()

                                MsgBox("시리얼 넘버: " & "'" & NewSerial & "'" & " 고객명: " & "'" & ComboBox1.Text & "'" & "  TOOL: " & "'" & TextBox2.Text & "'" & "'" & "  MODEL#:  " & "'" & TextBox1.Text & "'" & "  지그자재 종류:  " & "'" & ComboBox2.Text & "'" & "  지그자재 두께:  " & "'" & ComboBox3.Text & "'" & "  지그가공타입:  " & "'" & ComboBox4.Text & "'" & "  메모:  " & "'" & TextBox4.Text & "'" & "  (으)로 등록되었습니다.", 0 - vbOKOnly, "등록 완료")
                                TextBox1.Clear()
                                TextBox2.Clear()
                                TextBox3.Clear()
                                TextBox4.Clear()
                                ComboBox2.SelectedIndex = 0
                                ComboBox3.SelectedIndex = 2
                                ComboBox4.SelectedIndex = 0

                            End If
                        Else    'ss_detail이 비어있는 경우


                            If RowNum <> 0 Then  'ss_no_table이 비어있지 않은 경우(3)
                                Dim nempty_regdate3 As DateTime = DateTime.Now
                                Dim nempty_regdate4 As String = nempty_regdate3.ToString("yyyy-MM-dd HH:mm:ss")
                                Dim reg_sql3 As String
                                reg_sql3 = "INSERT INTO ss_detail(ss_serial, cust, tool, model, date, type, thk, cutting, memo) VALUES (1," & "'" & ComboBox1.Text & "'" & "," & "'" & TextBox2.Text & "'" & "," & "'" & TextBox1.Text & "'" & "," & "'" & nempty_regdate4 & "'" & "," & "'" & ComboBox2.Text & "'" & "," & "'" & ComboBox3.Text & "'" & "," & "'" & ComboBox4.Text & "'" & "," & "'" & TextBox4.Text & "'" & ")"
                                Dim reg_cmd3 As New MySqlCommand(reg_sql3, connection_reg)
                                reg_cmd3.ExecuteNonQuery()
                                MsgBox("시리얼 넘버: 1" & " 고객명: " & "'" & ComboBox1.Text & "'" & "  TOOL: " & "'" & TextBox2.Text & "'" & "'" & "  MODEL#:  " & "'" & TextBox1.Text & "'" & "  지그자재 종류:  " & "'" & ComboBox2.Text & "'" & "  지그자재 두께:  " & "'" & ComboBox3.Text & "'" & "  지그가공타입:  " & "'" & ComboBox4.Text & "'" & "  메모:  " & "'" & TextBox4.Text & "'" & "  (으)로 등록되었습니다.", 0 - vbOKOnly, "등록 완료")
                                TextBox1.Clear()
                                TextBox2.Clear()
                                TextBox3.Clear()
                                TextBox4.Clear()
                                ComboBox2.SelectedIndex = 0
                                ComboBox3.SelectedIndex = 2
                                ComboBox4.SelectedIndex = 0

                            Else    'ss_no_table이 비어있는 경우(4)
                                Dim empty_regdate3 As DateTime = DateTime.Now
                                Dim empty_regdate4 As String = empty_regdate3.ToString("yyyy-MM-dd HH:mm:ss")
                                Dim reg_sql3 As String
                                reg_sql3 = "INSERT INTO ss_detail(ss_serial, cust, tool, model, date, type, thk, cutting, memo) VALUES (1," & "'" & ComboBox1.Text & "'" & "," & "'" & TextBox2.Text & "'" & "," & "'" & TextBox1.Text & "'" & "," & "'" & empty_regdate4 & "'" & "," & "'" & ComboBox2.Text & "'" & "," & "'" & ComboBox3.Text & "'" & "," & "'" & ComboBox4.Text & "'" & "," & "'" & TextBox4.Text & "'" & ")"
                                Dim reg_cmd3 As New MySqlCommand(reg_sql3, connection_reg)
                                reg_cmd3.ExecuteNonQuery()
                                MsgBox("시리얼 넘버: 1" & " 고객명: " & "'" & ComboBox1.Text & "'" & "  TOOL: " & "'" & TextBox2.Text & "'" & "'" & "  MODEL#:  " & "'" & TextBox1.Text & "'" & "  지그자재 종류:  " & "'" & ComboBox2.Text & "'" & "  지그자재 두께:  " & "'" & ComboBox3.Text & "'" & "  지그가공타입:  " & "'" & ComboBox4.Text & "'" & "  메모:  " & "'" & TextBox4.Text & "'" & "  (으)로 등록되었습니다.", 0 - vbOKOnly, "등록 완료")
                                TextBox1.Clear()
                                TextBox2.Clear()
                                TextBox3.Clear()
                                TextBox4.Clear()
                                ComboBox2.SelectedIndex = 0
                                ComboBox3.SelectedIndex = 2
                                ComboBox4.SelectedIndex = 0
                            End If
                        End If
                    Else    '입고일자 o
                        'ss_detail이 비어있지 않은 경우
                        If DC <> 0 Then

                            'ss_no_table이 비어있지 않은 경우...(5)
                            If RowNum <> 0 Then
                                Dim MinSerial As Double
                                Call NotEmptyCase(MinSerial)

                                Dim whdate5 As DateTime
                                If DateTime.TryParseExact(TextBox3.Text, "yyyy-MM-dd", Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, whdate5) Then
                                    Dim whdate6 As String = whdate5.ToString("yyyy-MM-dd HH:mm:ss")

                                    Dim regsql3 As String
                                    regsql3 = "INSERT INTO ss_detail(ss_serial, cust, tool, model, date, type, thk, cutting, memo) VALUES (" & "'" & MinSerial & "'" & "," & "'" & ComboBox1.Text & "'" & "," & "'" & TextBox2.Text & "'" & "," & "'" & TextBox1.Text & "'" & "," & "'" & whdate6 & "'" & "," & "'" & ComboBox2.Text & "'" & "," & "'" & ComboBox3.Text & "'" & "," & "'" & ComboBox4.Text & "'" & "," & "'" & TextBox4.Text & "'" & ")"
                                    Dim regcmd3 As New MySqlCommand(regsql3, connection_reg)
                                    regcmd3.ExecuteNonQuery()


                                    Dim del_sql3 As String = "DELETE FROM ss_no_table WHERE ss_serial =" & "'" & MinSerial & "'"
                                    Dim reg_cmd3_2 As New MySqlCommand(del_sql3, connection_reg)
                                    reg_cmd3_2.ExecuteNonQuery()

                                    MsgBox("시리얼 넘버: " & "'" & MinSerial & "'" & " 고객명: " & "'" & ComboBox1.Text & "'" & "  TOOL: " & "'" & TextBox2.Text & "'" & "  MODEL#:  " & "'" & TextBox1.Text & "'" & "  입고일자:  " & "'" & whdate6 & "'" & "  지그자재 종류:  " & "'" & ComboBox2.Text & "'" & "  지그자재 두께:  " & "'" & ComboBox3.Text & "'" & "  지그가공타입:  " & "'" & ComboBox4.Text & "'" & "  지그 홀 수:  " & "  메모:  " & "'" & TextBox4.Text & "'" & "  (으)로 등록되었습니다", 0 - vbOKOnly, "등록 완료")
                                    TextBox1.Clear()
                                    TextBox2.Clear()
                                    TextBox3.Clear()
                                    TextBox4.Clear()
                                    ComboBox2.SelectedIndex = 0
                                    ComboBox3.SelectedIndex = 2
                                    ComboBox4.SelectedIndex = 0



                                Else
                                    MsgBox("입고일자(Warehousing Date)는 0000-00-00 형식으로 입력해주세요", 0 - vbOKOnly, "입고일자 형식 오류")
                                End If

                            Else    'ss_no_table이 비어있는 경우...(6)

                                Dim NewSerial As Double
                                Call EmptyCase(NewSerial)

                                Dim whdate1 As DateTime
                                If DateTime.TryParseExact(TextBox3.Text, "yyyy-MM-dd", Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, whdate1) Then
                                    Dim whdate2 As String = whdate1.ToString("yyyy-MM-dd HH:mm:ss")
                                    Dim regsql1 As String
                                    regsql1 = "INSERT INTO ss_detail(ss_serial, cust, tool, model, date, type, thk, cutting, memo) VALUES (" & "'" & NewSerial & "'" & "," & "'" & ComboBox1.Text & "'" & "," & "'" & TextBox2.Text & "'" & "," & "'" & TextBox1.Text & "'" & "," & "'" & whdate2 & "'" & "," & "'" & ComboBox2.Text & "'" & "," & "'" & ComboBox3.Text & "'" & "," & "'" & ComboBox4.Text & "'" & "," & "'" & TextBox4.Text & "'" & ")"

                                    Dim regcmd1 As New MySqlCommand(regsql1, connection_reg)
                                    regcmd1.ExecuteNonQuery()

                                    MsgBox("시리얼 넘버: " & "'" & NewSerial & "'" & " 고객명: " & "'" & ComboBox1.Text & "'" & "  TOOL: " & "'" & TextBox2.Text & "'" & "'" & "  MODEL#:  " & "'" & TextBox1.Text & "'" & "  입고일자:  " & "'" & whdate2 & "'" & "  지그자재 종류:  " & "'" & ComboBox2.Text & "'" & "  지그자재 두께:  " & "'" & ComboBox3.Text & "'" & "  지그가공타입:  " & "'" & ComboBox4.Text & "'" & "  메모:  " & "'" & TextBox4.Text & "'" & "  (으)로 등록되었습니다.", 0 - vbOKOnly, "등록 완료")
                                    TextBox1.Clear()
                                    TextBox2.Clear()
                                    TextBox3.Clear()
                                    TextBox4.Clear()
                                    ComboBox2.SelectedIndex = 0
                                    ComboBox3.SelectedIndex = 2
                                    ComboBox4.SelectedIndex = 0
                                Else
                                    MsgBox("입고일자(Warehousing Date)는 0000-00-00 형식으로 입력해주세요", 0 - vbOKOnly, "입고일자 형식 오류")

                                End If
                            End If
                        Else    'ss_detail이 비어있는 경우

                            If RowNum <> 0 Then  'ss_no_table이 비어있지 않은 경우(7)
                                Dim whdate7 As DateTime
                                If DateTime.TryParseExact(TextBox3.Text, "yyyy-MM-dd", Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, whdate7) Then
                                    Dim whdate8 As String = whdate7.ToString("yyyy-MM-dd HH:mm:ss")

                                    Dim regsql4 As String
                                    regsql4 = "INSERT INTO ss_detail(ss_serial, cust, tool, model, date, type, thk, cutting, memo) VALUES (1," & "'" & ComboBox1.Text & "'" & "," & "'" & TextBox2.Text & "'" & "," & "'" & TextBox1.Text & "'" & "," & "'" & whdate8 & "'" & "," & "'" & ComboBox2.Text & "'" & "," & "'" & ComboBox3.Text & "'" & "," & "'" & ComboBox4.Text & "'" & "," & "'" & TextBox4.Text & "'" & ")"
                                    Dim regcmd4 As New MySqlCommand(regsql4, connection_reg)
                                    regcmd4.ExecuteNonQuery()

                                    MsgBox("시리얼 넘버: 1" & " 고객명: " & "'" & ComboBox1.Text & "'" & "  TOOL: " & "'" & TextBox2.Text & "'" & "'" & "  MODEL#:  " & "'" & TextBox1.Text & "'" & "  입고일자:  " & "'" & whdate8 & "'" & "  지그자재 종류:  " & "'" & ComboBox2.Text & "'" & "  지그자재 두께:  " & "'" & ComboBox3.Text & "'" & "  지그가공타입:  " & "'" & ComboBox4.Text & "'" & "  메모:  " & "'" & TextBox4.Text & "'" & "  (으)로 등록되었습니다.", 0 - vbOKOnly, "등록 완료")
                                    TextBox1.Clear()
                                    TextBox2.Clear()
                                    TextBox3.Clear()
                                    TextBox4.Clear()
                                    ComboBox2.SelectedIndex = 0
                                    ComboBox3.SelectedIndex = 2
                                    ComboBox4.SelectedIndex = 0
                                Else
                                    MsgBox("입고일자(Warehousing Date)는 0000-00-00 형식으로 입력해주세요", 0 - vbOKOnly, "입고일자 형식 오류")
                                End If


                            Else    'ss_no_table이 비어있는 경우(8)
                                Dim whdate3 As DateTime
                                If DateTime.TryParseExact(TextBox3.Text, "yyyy-MM-dd", Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, whdate3) Then
                                    Dim whdate4 As String = whdate3.ToString("yyyy-MM-dd HH:mm:ss")
                                    Dim regsql2 As String
                                    regsql2 = "INSERT INTO ss_detail(ss_serial, cust, tool, model, date, type, thk, cutting, memo) VALUES (1," & "'" & ComboBox1.Text & "'" & "," & "'" & TextBox2.Text & "'" & "," & "'" & TextBox1.Text & "'" & "," & "'" & whdate4 & "'" & "," & "'" & ComboBox2.Text & "'" & "," & "'" & ComboBox3.Text & "'" & "," & "'" & ComboBox4.Text & "'" & "," & "'" & TextBox4.Text & "'" & ")"
                                    Dim regcmd2 As New MySqlCommand(regsql2, connection_reg)
                                    regcmd2.ExecuteNonQuery()

                                    MsgBox("시리얼 넘버: 1" & " 고객명: " & "'" & ComboBox1.Text & "'" & "  TOOL: " & "'" & TextBox2.Text & "'" & "'" & "  MODEL#:  " & "'" & TextBox1.Text & "  입고일자:  " & "'" & whdate4 & "'" & "  지그자재 종류:  " & "'" & ComboBox2.Text & "'" & "  지그자재 두께:  " & "'" & ComboBox3.Text & "'" & "  지그가공타입:  " & "'" & ComboBox4.Text & "'" & "  메모:  " & "'" & TextBox4.Text & "'" & "  (으)로 등록되었습니다.", 0 - vbOKOnly, "등록 완료")
                                    TextBox1.Clear()
                                    TextBox2.Clear()
                                    TextBox3.Clear()
                                    TextBox4.Clear()
                                    ComboBox2.SelectedIndex = 0
                                    ComboBox3.SelectedIndex = 2
                                    ComboBox4.SelectedIndex = 0
                                Else
                                    MsgBox("입고일자(Warehousing Date)는 0000-00-00 형식으로 입력해주세요", 0 - vbOKOnly, "입고일자 형식 오류")
                                End If
                            End If
                        End If
                    End If
                End If


            ElseIf MsgRes1 = vbCancel Then
            End If
            'Catch ex As Exception
            'MsgBox("등록 오류 발생: " & "'" & ex.Message & "'", 0 - vbOKOnly, "등록 오류")

        Finally
            connection_reg.Close()
            EmptyCek()

        End Try
    End Sub



    Private Sub Form13_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        EmptyCek()

        ComboBox2.SelectedIndex = 0
        ComboBox3.SelectedIndex = 2
        ComboBox4.SelectedIndex = 0
        '이거 써야 첫번째 칸 빈칸 되는건데 어째선지 주석처리해도 빈칸이 되네...
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        RegToDB()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Form15.Show()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Form16.Show()
    End Sub
End Class
