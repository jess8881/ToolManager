Imports System.Diagnostics.Eventing.Reader
Imports MySql.Data.MySqlClient
Imports System.Globalization

'.ExecuteNonQuery: INSERT, UPSQTE, DELETE에 사용됨. 영향받은 rows 반환


'[1]jig_no_table이 비어있는 경우
'   <1>jig_detail의 jig_serial# 중 '가장 큰 #' +1 한 '값' 찾기 (O)
'   <2>찾은 값을 jig_serial#로 지정해서 jig_detail 테이블에 다른 셀값과 함께 insert (O)
'[2]jig_no_table이 비어있지 않은 경우
'   <1>jig_no_table에서 가장 작은 시리얼# 가져옴(O)
'   <2>가져온 #을 jig_serial#로 지정해서 jig_detail 테이블에 다른 셀값과 함께 insert(O)
'   <3>jig_no_table에서 가장 작은 시리얼# 삭제(X)


Public Class Form4
    Dim connection_reg As New MySqlConnection("server=localhost; Port=3306; database= jigfile; username=root; Password=*****; Convert Zero Datetime=True")

    Public Sub EmptyCek()

        connection_reg.Open()

        '<콤보박스에 insert>
        Dim adapter_cmd As New MySqlDataAdapter("SELECT nic FROM cust_info_table", connection_reg)
        Dim dt_cmd As DataTable = New DataTable()
        adapter_cmd.Fill(dt_cmd)

        'LINQ를 이용해 CONVERT TO ARRAY
        Dim custValues = (From dr In dt_cmd Select nic = dr(0).ToString).ToArray

        ComboBox1.DataSource = Nothing   'datasource 속성을 설정하면 items.clear()가 불가능해서 null로 clear함
        'ComboBox1.Items.Clear()
        ComboBox1.DataSource = custValues
        ComboBox1.SelectedIndex = -1    '콤보박스 첫 칸을 빈칸으로
        connection_reg.Close()


    End Sub


    Public Sub EmptyCase(ByRef NewSerial As Double)

        '<1>jig_detail의 jig_serial# 중 '가장 큰 #' +1 한 '값' 구하기
        Dim maxnum As String = "SELECT MAX(jig_serial) FROM jig_detail"
        Dim max_cmd As New MySqlCommand(maxnum, connection_reg)
        Dim MaxSerial As Double = max_cmd.ExecuteScalar()

        NewSerial = MaxSerial + 1

    End Sub

    Public Sub NotEmptyCase(ByRef MinSerial As Double)

        '<1>jig_no_table에서 가장 작은 시리얼# SELECT
        Dim minnum As String = "SELECT MIN(jig_serial) FROM jig_no_table"
        Dim min_cmd As New MySqlCommand(minnum, connection_reg)

        MinSerial = min_cmd.ExecuteScalar()

    End Sub

    Private Sub RegToDB()

        connection_reg.Open()

        Dim TableCount As String = "SELECT COUNT(jig_serial) FROM jig_no_table"
        Dim TableCount_cmd As New MySqlCommand(TableCount, connection_reg)
        Dim RowNum2 As Double = TableCount_cmd.ExecuteScalar()    '카운트한 결과값 받기

        Dim DetailCount2 As String = "SELECT COUNT(jig_serial) FROM jig_detail"
        Dim DC_cmd2 As New MySqlCommand(DetailCount2, connection_reg)
        Dim DC2 As Double = DC_cmd2.ExecuteScalar()


        Try

            Dim MsgRes2 As String
            MsgRes2 = MsgBox("정말로 등록하시겠습니까?", vbOKCancel, "등록")

            If MsgRes2 = vbOK Then

                '필수필드 모두 충족x
                If String.IsNullOrEmpty(ComboBox1.Text) Or String.IsNullOrEmpty(ComboBox2.Text) Or String.IsNullOrEmpty(TextBox2.Text) Or String.IsNullOrEmpty(TextBox1.Text) Or String.IsNullOrEmpty(ComboBox3.Text) Or String.IsNullOrEmpty(ComboBox4.Text) Then
                    MsgBox("빈 칸을 모두 채워주세요.", 0 - vbOKOnly, "등록 정보 부족")
                Else    '필수필드 모두 충족 o

                    '입고일자 x
                    If String.IsNullOrEmpty(TextBox5.Text) Then

                        'ss_detail이 비어있지 않은 경우
                        If DC2 <> 0 Then

                            'ss_no_table이 비어있지 않은 경우 ...(1)
                            If RowNum2 <> 0 Then
                                Dim MinSerial As Double
                                Call NotEmptyCase(MinSerial)

                                Dim nempty_regDate1 As DateTime = DateTime.Now
                                Dim nempty_regDate2 As String = nempty_regDate1.ToString("yyyy-MM-dd HH:mm:ss")
                                Dim reg_sql2 As String
                                reg_sql2 = "INSERT INTO jig_detail(jig_serial, cust, tool, model, date, type, thk, cutting, hc, memo) VALUES (" & "'" & MinSerial & "'" & "," & "'" & ComboBox1.Text & "'" & "," & "'" & TextBox2.Text & "'" & "," & "'" & TextBox1.Text & "'" & "," & "'" & nempty_regDate2 & "'" & "," & "'" & ComboBox2.Text & "'" & "," & "'" & ComboBox3.Text & "'" & "," & "'" & ComboBox4.Text & "'" & "," & "'" & TextBox3.Text & "'" & "," & "'" & TextBox4.Text & "'" & ")"
                                Dim reg_cmd2 As New MySqlCommand(reg_sql2, connection_reg)
                                Console.WriteLine(reg_sql2)
                                reg_cmd2.ExecuteNonQuery()

                                Dim del_sql2 As String = "DELETE FROM jig_no_table WHERE jig_serial =" & "'" & MinSerial & "'"           'double quote to string -> "chr(34)
                                Dim reg_cmd2_2 As New MySqlCommand(del_sql2, connection_reg)
                                reg_cmd2_2.ExecuteNonQuery()


                                MsgBox("시리얼 넘버: " & "'" & MinSerial & "'" & " 고객명: " & "'" & ComboBox1.Text & "'" & "  TOOL: " & "'" & TextBox2.Text & "'" & "  MODEL#:  " & "'" & TextBox1.Text & "'" & "  지그자재 종류:  " & "'" & ComboBox2.Text & "'" & "  지그자재 두께:  " & "'" & ComboBox3.Text & "'" & "  지그가공타입:  " & "'" & ComboBox4.Text & "'" & "  지그 홀 수:  " & "'" & TextBox3.Text & "'" & "  메모:  " & "'" & TextBox4.Text & "'" & "  (으)로 등록되었습니다", 0 - vbOKOnly, "등록 완료")
                                TextBox1.Clear()
                                TextBox2.Clear()
                                TextBox3.Clear()
                                TextBox4.Clear()
                                TextBox5.Clear()
                                ComboBox2.SelectedIndex = 0
                                ComboBox3.SelectedIndex = 3
                                ComboBox4.SelectedIndex = 0

                            Else    'ss_no_table이 비어있는 경우 ...(2)
                                Dim NewSerial As Double
                                Call EmptyCase(NewSerial)


                                Dim empty_regDate1 As DateTime = DateTime.Now
                                Dim empty_regDate2 As String = empty_regDate1.ToString("yyyy-MM-dd HH:mm:ss")
                                Dim reg_sql1 As String

                                reg_sql1 = "INSERT INTO jig_detail(jig_serial, cust, tool, model, date, type, thk, cutting, hc, memo) VALUES (" & "'" & NewSerial & "'" & "," & "'" & ComboBox1.Text & "'" & "," & "'" & TextBox2.Text & "'" & "," & "'" & TextBox1.Text & "'" & "," & "'" & empty_regDate2 & "'" & "," & "'" & ComboBox2.Text & "'" & "," & "'" & ComboBox3.Text & "'" & "," & "'" & ComboBox4.Text & "'" & "," & "'" & TextBox3.Text & "'" & "," & "'" & TextBox4.Text & "'" & ")"

                                Dim reg_cmd1 As New MySqlCommand(reg_sql1, connection_reg)
                                Console.WriteLine(reg_sql1)
                                reg_cmd1.ExecuteNonQuery()



                                MsgBox("시리얼 넘버: " & "'" & NewSerial & "'" & " 고객명: " & "'" & ComboBox1.Text & "'" & "  TOOL: " & "'" & TextBox2.Text & "'" & "'" & "  MODEL#:  " & "'" & TextBox1.Text & "'" & "  지그자재 종류:  " & "'" & ComboBox2.Text & "'" & "  지그자재 두께:  " & "'" & ComboBox3.Text & "'" & "  지그가공타입:  " & "'" & ComboBox4.Text & "'" & "  지그 홀 수:  " & "'" & TextBox3.Text & "'" & "  메모:  " & "'" & TextBox4.Text & "'" & "  (으)로 등록되었습니다.", 0 - vbOKOnly, "등록 완료")
                                TextBox1.Clear()
                                TextBox2.Clear()
                                TextBox3.Clear()
                                TextBox4.Clear()
                                TextBox5.Clear()
                                ComboBox2.SelectedIndex = 0
                                ComboBox3.SelectedIndex = 3
                                ComboBox4.SelectedIndex = 0
                            End If


                        Else    'ss_detail이 비어있는 경우

                            'ss_no_table이 비어있지 않은 경우 ...(3)
                            If RowNum2 <> 0 Then
                                Dim nempty_regdate3 As DateTime = DateTime.Now
                                Dim nempty_regdate4 As String = nempty_regdate3.ToString("yyyy-MM-dd HH:mm:ss")
                                Dim reg_sql3 = "INSERT INTO jig_detail(jig_serial, cust, tool, model, date, type, thk, cutting, hc, memo) VALUES (1," & "'" & ComboBox1.Text & "'" & "," & "'" & TextBox2.Text & "'" & "," & "'" & TextBox1.Text & "'" & "," & "'" & nempty_regdate4 & "'" & "," & "'" & ComboBox2.Text & "'" & "," & "'" & ComboBox3.Text & "'" & "," & "'" & ComboBox4.Text & "'" & "," & "'" & TextBox3.Text & "'" & "," & "'" & TextBox4.Text & "'" & ")"
                                Dim reg_cmd3 As New MySqlCommand(reg_sql3, connection_reg)
                                reg_cmd3.ExecuteNonQuery()
                                MsgBox("시리얼 넘버: 1" & "'" & " 고객명: " & "'" & ComboBox1.Text & "'" & "  TOOL: " & "'" & TextBox2.Text & "'" & "  MODEL#:  " & "'" & TextBox1.Text & "'" & "  지그자재 종류:  " & "'" & ComboBox2.Text & "'" & "  지그자재 두께:  " & "'" & ComboBox3.Text & "'" & "  지그가공타입:  " & "'" & ComboBox4.Text & "'" & "  지그 홀 수:  " & "'" & TextBox3.Text & "'" & "  메모:  " & "'" & TextBox4.Text & "'" & "  (으)로 등록되었습니다", 0 - vbOKOnly, "등록 완료")
                                TextBox1.Clear()
                                TextBox2.Clear()
                                TextBox3.Clear()
                                TextBox4.Clear()
                                TextBox5.Clear()
                                ComboBox2.SelectedIndex = 0
                                ComboBox3.SelectedIndex = 3
                                ComboBox4.SelectedIndex = 0

                            Else    'ss_no_table이 비어있는 경우 ...(4)

                                Dim empty_regdate3 As DateTime = DateTime.Now
                                Dim empty_regdate4 As String = empty_regdate3.ToString("yyyy-MM-dd HH:mm:ss")
                                Dim reg_sql3 As String
                                reg_sql3 = "INSERT INTO jig_detail(jig_serial, cust, tool, model, date, type, thk, cutting, hc, memo) VALUES (1," & "'" & ComboBox1.Text & "'" & "," & "'" & TextBox2.Text & "'" & "," & "'" & TextBox1.Text & "'" & "," & "'" & empty_regdate4 & "'" & "," & "'" & ComboBox2.Text & "'" & "," & "'" & ComboBox3.Text & "'" & "," & "'" & ComboBox4.Text & "'" & "," & "'" & TextBox3.Text & "'" & "," & "'" & TextBox4.Text & "'" & ")"
                                Dim reg_cmd3 As New MySqlCommand(reg_sql3, connection_reg)
                                reg_cmd3.ExecuteNonQuery()
                                MsgBox("시리얼 넘버: 1" & "'" & " 고객명: " & "'" & ComboBox1.Text & "'" & "  TOOL: " & "'" & TextBox2.Text & "'" & "'" & "  MODEL#:  " & "'" & TextBox1.Text & "'" & "  지그자재 종류:  " & "'" & ComboBox2.Text & "'" & "  지그자재 두께:  " & "'" & ComboBox3.Text & "'" & "  지그가공타입:  " & "'" & ComboBox4.Text & "'" & "  지그 홀 수:  " & "'" & TextBox3.Text & "'" & "  메모:  " & "'" & TextBox4.Text & "'" & "  (으)로 등록되었습니다.", 0 - vbOKOnly, "등록 완료")
                                TextBox1.Clear()
                                TextBox2.Clear()
                                TextBox4.Clear()
                                TextBox3.Clear()
                                TextBox5.Clear()
                                ComboBox2.SelectedIndex = 0
                                ComboBox3.SelectedIndex = 3
                                ComboBox4.SelectedIndex = 0
                            End If
                        End If

                    Else    '입고일자 o
                        If DC2 <> 0 Then    'ss_detail이 비어있지 않은 경우
                            If RowNum2 <> 0 Then    'ss_no_table이 비어있지 않은 경우 ...(5)

                                Dim MinSerial As Double
                                Call NotEmptyCase(MinSerial)

                                Dim whdate5 As DateTime
                                If DateTime.TryParseExact(TextBox5.Text, "yyyy-MM-dd", Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, whdate5) Then
                                    Dim whdate5_1 As String = whdate5.ToString("yyyy-MM-dd HH:mm:ss")
                                    Dim reg_sql2 As String
                                    reg_sql2 = "INSERT INTO jig_detail(jig_serial, cust, tool, model, date, type, thk, cutting, hc, memo) VALUES (" & "'" & MinSerial & "'" & "," & "'" & ComboBox1.Text & "'" & "," & "'" & TextBox2.Text & "'" & "," & "'" & TextBox1.Text & "'" & "," & "'" & whdate5_1 & "'" & "," & "'" & ComboBox2.Text & "'" & "," & "'" & ComboBox3.Text & "'" & "," & "'" & ComboBox4.Text & "'" & "," & "'" & TextBox3.Text & "'" & "," & "'" & TextBox4.Text & "'" & ")"
                                    Dim reg_cmd2 As New MySqlCommand(reg_sql2, connection_reg)
                                    Console.WriteLine(reg_sql2)
                                    reg_cmd2.ExecuteNonQuery()

                                    Dim del_sql2 As String = "DELETE FROM jig_no_table WHERE jig_serial =" & "'" & MinSerial & "'"           'double quote to string -> "chr(34)
                                    Dim reg_cmd2_2 As New MySqlCommand(del_sql2, connection_reg)
                                    reg_cmd2_2.ExecuteNonQuery()


                                    MsgBox("시리얼 넘버: " & "'" & MinSerial & "'" & " 고객명: " & "'" & ComboBox1.Text & "'" & "  TOOL: " & "'" & TextBox2.Text & "'" & "  MODEL#:  " & "'" & TextBox1.Text & "'" & "  지그자재 종류:  " & "'" & ComboBox2.Text & "'" & "  지그자재 두께:  " & "'" & ComboBox3.Text & "'" & "  지그가공타입:  " & "'" & ComboBox4.Text & "'" & "  지그 홀 수:  " & "'" & TextBox3.Text & "'" & "  메모:  " & "'" & TextBox4.Text & "'" & "  (으)로 등록되었습니다", 0 - vbOKOnly, "등록 완료")
                                    TextBox1.Clear()
                                    TextBox2.Clear()
                                    TextBox3.Clear()
                                    TextBox4.Clear()
                                    TextBox5.Clear()
                                    ComboBox2.SelectedIndex = 0
                                    ComboBox3.SelectedIndex = 3
                                    ComboBox4.SelectedIndex = 0
                                Else
                                    MsgBox("입고일자(Warehousing Date)는 0000-00-00 형식으로 입력해주세요", 0 - vbOKOnly, "입고일자 형식 오류")
                                End If



                            Else    'ss_no_table이 비어있는 경우 ...(6)

                                Dim NewSerial As Double
                                Call EmptyCase(NewSerial)

                                Dim whdate6 As DateTime

                                If DateTime.TryParseExact(TextBox5.Text, "yyyy-MM-dd", Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, whdate6) Then
                                    Dim whdate6_1 As String = whdate6.ToString("yyyy-MM-dd HH:mm:ss")
                                    Dim reg_sql1 As String
                                    reg_sql1 = "INSERT INTO jig_detail(jig_serial, cust, tool, model, date, type, thk, cutting, hc, memo) VALUES (" & "'" & NewSerial & "'" & "," & "'" & ComboBox1.Text & "'" & "," & "'" & TextBox2.Text & "'" & "," & "'" & TextBox1.Text & "'" & "," & "'" & whdate6_1 & "'" & "," & "'" & ComboBox2.Text & "'" & "," & "'" & ComboBox3.Text & "'" & "," & "'" & ComboBox4.Text & "'" & "," & "'" & TextBox3.Text & "'" & "," & "'" & TextBox4.Text & "'" & ")"
                                    Dim reg_cmd1 As New MySqlCommand(reg_sql1, connection_reg)
                                    Console.WriteLine(reg_sql1)
                                    reg_cmd1.ExecuteNonQuery()



                                    MsgBox("시리얼 넘버: " & "'" & NewSerial & "'" & " 고객명: " & "'" & ComboBox1.Text & "'" & "  TOOL: " & "'" & TextBox2.Text & "'" & "'" & "  MODEL#:  " & "'" & TextBox1.Text & "'" & "  지그자재 종류:  " & "'" & ComboBox2.Text & "'" & "  지그자재 두께:  " & "'" & ComboBox3.Text & "'" & "  지그가공타입:  " & "'" & ComboBox4.Text & "'" & "  지그 홀 수:  " & "'" & TextBox3.Text & "'" & "  메모:  " & "'" & TextBox4.Text & "'" & "  (으)로 등록되었습니다.", 0 - vbOKOnly, "등록 완료")
                                    TextBox1.Clear()
                                    TextBox2.Clear()
                                    TextBox3.Clear()
                                    TextBox4.Clear()
                                    TextBox5.Clear()
                                    ComboBox2.SelectedIndex = 0
                                    ComboBox3.SelectedIndex = 3
                                    ComboBox4.SelectedIndex = 0
                                Else
                                    MsgBox("입고일자(Warehousing Date)는 0000-00-00 형식으로 입력해주세요", 0 - vbOKOnly, "입고일자 형식 오류")
                                End If

                            End If
                        Else    'ss_detail이 비어있는 경우
                            If RowNum2 <> 0 Then    'ss_no_table이 비어있는 경우..(7)

                                Dim whdate7 As DateTime
                                If DateTime.TryParseExact(TextBox5.Text, "yyyy-MM-dd", Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, whdate7) Then
                                    Dim whdate7_1 As String = whdate7.ToString("yyyy-MM-dd HH:mm:ss")
                                    Dim reg_sql3 As String
                                    reg_sql3 = "INSERT INTO jig_detail(jig_serial, cust, tool, model, date, type, thk, cutting, hc, memo) VALUES (1," & "'" & ComboBox1.Text & "'" & "," & "'" & TextBox2.Text & "'" & "," & "'" & TextBox1.Text & "'" & "," & "'" & whdate7_1 & "'" & "," & "'" & ComboBox2.Text & "'" & "," & "'" & ComboBox3.Text & "'" & "," & "'" & ComboBox4.Text & "'" & "," & "'" & TextBox3.Text & "'" & "," & "'" & TextBox4.Text & "'" & ")"
                                    Dim reg_cmd3 As New MySqlCommand(reg_sql3, connection_reg)
                                    reg_cmd3.ExecuteNonQuery()
                                    MsgBox("시리얼 넘버: 1" & "'" & " 고객명: " & "'" & ComboBox1.Text & "'" & "  TOOL: " & "'" & TextBox2.Text & "'" & "  MODEL#:  " & "'" & TextBox1.Text & "'" & "  지그자재 종류:  " & "'" & ComboBox2.Text & "'" & "  지그자재 두께:  " & "'" & ComboBox3.Text & "'" & "  지그가공타입:  " & "'" & ComboBox4.Text & "'" & "  지그 홀 수:  " & "'" & TextBox3.Text & "'" & "  메모:  " & "'" & TextBox4.Text & "'" & "  (으)로 등록되었습니다", 0 - vbOKOnly, "등록 완료")
                                    TextBox1.Clear()
                                    TextBox2.Clear()
                                    TextBox3.Clear()
                                    TextBox4.Clear()
                                    TextBox5.Clear()
                                    ComboBox2.SelectedIndex = 0
                                    ComboBox3.SelectedIndex = 3
                                    ComboBox4.SelectedIndex = 0

                                Else
                                    MsgBox("입고일자(Warehousing Date)는 0000-00-00 형식으로 입력해주세요", 0 - vbOKOnly, "입고일자 형식 오류")
                                End If


                            Else    'ss_no_table이 비어있지 않은 경우(8)

                                Dim whdate8 As DateTime
                                If DateTime.TryParseExact(TextBox5.Text, "yyyy-MM-dd", Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, whdate8) Then
                                    Dim whdate8_1 As String = whdate8.ToString("yyyy-MM-dd HH:mm:ss")
                                    Dim reg_sql3 As String
                                    reg_sql3 = "INSERT INTO jig_detail(jig_serial, cust, tool, model, date, type, thk, cutting, hc, memo) VALUES (1," & "'" & ComboBox1.Text & "'" & "," & "'" & TextBox2.Text & "'" & "," & "'" & TextBox1.Text & "'" & "," & "'" & whdate8_1 & "'" & "," & "'" & ComboBox2.Text & "'" & "," & "'" & ComboBox3.Text & "'" & "," & "'" & ComboBox4.Text & "'" & "," & "'" & TextBox3.Text & "'" & "," & "'" & TextBox4.Text & "'" & ")"
                                    Dim reg_cmd3 As New MySqlCommand(reg_sql3, connection_reg)
                                    reg_cmd3.ExecuteNonQuery()
                                    MsgBox("시리얼 넘버: 1" & "'" & " 고객명: " & "'" & ComboBox1.Text & "'" & "  TOOL: " & "'" & TextBox2.Text & "'" & "'" & "  MODEL#:  " & "'" & TextBox1.Text & "'" & "  지그자재 종류:  " & "'" & ComboBox2.Text & "'" & "  지그자재 두께:  " & "'" & ComboBox3.Text & "'" & "  지그가공타입:  " & "'" & ComboBox4.Text & "'" & "  지그 홀 수:  " & "'" & TextBox3.Text & "'" & "  메모:  " & "'" & TextBox4.Text & "'" & "  (으)로 등록되었습니다.", 0 - vbOKOnly, "등록 완료")
                                    TextBox1.Clear()
                                    TextBox2.Clear()
                                    TextBox4.Clear()
                                    TextBox3.Clear()
                                    TextBox5.Clear()
                                    ComboBox2.SelectedIndex = 0
                                    ComboBox3.SelectedIndex = 3
                                    ComboBox4.SelectedIndex = 0
                                Else
                                    MsgBox("입고일자(Warehousing Date)는 0000-00-00 형식으로 입력해주세요", 0 - vbOKOnly, "입고일자 형식 오류")
                                End If

                            End If
                        End If
                    End If

                End If








            ElseIf MsgRes2 = vbCancel Then

            End If


        Catch ex As Exception
            MsgBox("등록 오류 발생: " & "'" & ex.Message & "'", 0 - vbOKOnly, "등록 오류")

        Finally
            connection_reg.Close()
            EmptyCek()

        End Try
    End Sub


    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        EmptyCek()
        ComboBox2.SelectedIndex = 0
        ComboBox3.SelectedIndex = 3
        ComboBox4.SelectedIndex = 0
        '이거 써야 첫번째 칸 빈칸 되는건데 어째선지 주석처리해도 빈칸이 되네...
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        RegToDB()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Form23.Show()
    End Sub
End Class
