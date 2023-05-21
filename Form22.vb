Imports MySql.Data.MySqlClient
Imports System.Globalization
Imports System.Diagnostics.Eventing.Reader

'startposition = centerscreen 해놓음
Public Class Form22
  Dim conn_cust As New MySqlConnection("server=localhost; Port=3306; database= jigfile; username=root; Password=*****; Convert Zero Datetime=True")

    Private Sub updateCust()
        conn_cust.Open()


        Try
            Dim subMsg As String
            subMsg = MsgBox("정말로 등록하시겠습니까?", vbOKCancel, "등록")


            If subMsg = vbOK Then

                '필수필드가 부족한 경우
                If String.IsNullOrEmpty(ComboBox1.Text) Or String.IsNullOrEmpty(TextBox1.Text) Or String.IsNullOrEmpty(TextBox2.Text) Or String.IsNullOrEmpty(TextBox3.Text) Or String.IsNullOrEmpty(TextBox4.Text) Or String.IsNullOrEmpty(TextBox5.Text) Or String.IsNullOrEmpty(TextBox6.Text) Or String.IsNullOrEmpty(TextBox8.Text) Or String.IsNullOrEmpty(TextBox10.Text) Then
                    MsgBox("필수 입력 항목을 모두 채워주세요.", 0 - vbOKOnly, "필수 정보 부족")

                Else    '필수필드가 모두 충족된 경우

                    Dim updateCust As String = "UPDATE cust_info_table SET len_wei='" & ComboBox1.Text & "', des='" & TextBox1.Text & "', nic='" & TextBox2.Text & "', resp='" & TextBox3.Text & "', volume='" & TextBox4.Text & "', price='" & TextBox5.Text & "', phone1='" & TextBox6.Text & "', phone2='" & TextBox7.Text & "', email='" & TextBox8.Text & "', website='" & TextBox9.Text & "', pdate='" & TextBox10.Text & "' WHERE serial ='" & TextBox11.Text & "'"

                    Dim custCmd1 As New MySqlCommand(updateCust, conn_cust)
                    custCmd1.ExecuteNonQuery()

                    MsgBox(" 고객명: " & "'" & TextBox1.Text & "'" & "  고객코드: " & "'" & TextBox2.Text & "'" & "  담당자:  " & "'" & TextBox3.Text & "'" & "  단위:  " & "'" & ComboBox1.Text & "'" & "  수량:  " & "'" & TextBox4.Text & "'" & "  단가:  " & "'" & TextBox5.Text & "'" & "  연락처1:  " & "'" & TextBox6.Text & "'" & "  연락처2:  " & "'" & TextBox7.Text & "'" & "  이메일:  " & "'" & TextBox8.Text & "'" & "  웹사이트:  " & "'" & TextBox9.Text & "  결제일:  " & "'" & TextBox10.Text & "'" & "  (으)로 수정되었습니다", 0 - vbOKOnly, "수정 완료")

                End If

            ElseIf subMsg = vbCancel Then
            End If


        Catch ex As Exception
            MsgBox("수정 오류 발생: " & "'" & ex.Message & "'", 0 - vbOKOnly, "수정 오류")
        Finally
            conn_cust.Close()
        End Try

    End Sub



    Private Sub Form22_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'cbbx 채우기
        ComboBox1.Items.Add("hole")
        ComboBox1.Items.Add("m2(제곱미터, Square meter)")
        ComboBox1.Items.Add("매")
        ComboBox1.Items.Add("mm")
        ComboBox1.Items.Add("기타")

        'serial select는 하지만 굳이 보여줄필요 없음
        TextBox11.Visible = False
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        updateCust()
        Me.Close()
    End Sub
End Class
