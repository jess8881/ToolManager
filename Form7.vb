Imports MySql.Data.MySqlClient

Public Class Form7
  Dim connection_regcust As New MySqlConnection("server=localhost; Port=3306; database= jigfile; username=root; Password=*****; Convert Zero Datetime=True")


    'chr(13)은 줄바꿈(엔터키)

    Private Sub RegCust()
        connection_regcust.Open()
        'connection_regcust2.Open()

        Try

            Dim MsgRes2 As String
            MsgRes2 = MsgBox("정말로 등록하시겠습니까?", vbOKCancel, "등록")

            If MsgRes2 = vbOK Then


                If String.IsNullOrEmpty(TextBox1.Text) Or String.IsNullOrEmpty(TextBox2.Text) Or String.IsNullOrEmpty(TextBox3.Text) Or String.IsNullOrEmpty(TextBox4.Text) Or String.IsNullOrEmpty(TextBox5.Text) Or String.IsNullOrEmpty(TextBox6.Text) Or String.IsNullOrEmpty(TextBox8.Text) Or String.IsNullOrEmpty(TextBox10.Text) Or String.IsNullOrEmpty(ComboBox1.Text) Then
                    MsgBox("필수 항목을 입력해주세요(Please fill in all the required fileds)", 0 - vbOKOnly, "필수 항목 입력 필요")

                Else


                    Dim reg_sql3 As String
                    reg_sql3 = "INSERT INTO cust_info_table(des, nic, resp, len_wei, volume, price, phone1, phone2, email, website, pdate) VALUES (" & "'" & TextBox1.Text & "'" & "," & "'" & TextBox2.Text & "'" & "," & "'" & TextBox3.Text & "'" & "," & "'" & ComboBox1.Text & "'" & "," & "'" & TextBox4.Text & "'" & "," & "'" & TextBox5.Text & "'" & "," & "'" & TextBox6.Text & "'" & "," & "'" & TextBox7.Text & "'" & "," & "'" & TextBox8.Text & "'" & "," & "'" & TextBox9.Text & "'" & "," & "'" & TextBox10.Text & "'" & ")"
                    Dim reg_cmd3 As New MySqlCommand(reg_sql3, connection_regcust)
                    reg_cmd3.ExecuteNonQuery()
                    
                    MsgBox("고객명: " & TextBox1.Text & " 고객코드: " & TextBox2.Text & " 담당자: " & TextBox3.Text & " 단위: " & ComboBox1.Text & " 수량: " & TextBox4.Text & " 단가: " & TextBox5.Text & " 연락처1: " & TextBox6.Text & " 연락처2: " & TextBox7.Text & " 이메일: " & TextBox8.Text & " 웹사이트: " & TextBox9.Text & " 결제일: " & TextBox10.Text & " (으)로 등록되었습니다.")
    
                    TextBox1.Clear()
                    TextBox2.Clear()
                    TextBox3.Clear()
                    TextBox4.Clear()
                    TextBox5.Clear()
                    TextBox6.Clear()
                    TextBox7.Clear()
                    TextBox8.Clear()
                    TextBox9.Clear()
                    TextBox10.Clear()
                    ComboBox1.SelectedIndex = -1

                End If


            ElseIf MsgRes2 = vbCancel Then

            End If



        Catch ex As Exception
            MsgBox("등록 오류 발생: " & "'" & ex.Message & "'", 0 - vbOKOnly, "등록 오류")

        Finally
            connection_regcust.Close()
        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        RegCust()
    End Sub
End Class

