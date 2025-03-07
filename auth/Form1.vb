Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Drawing
Imports System.IO
Imports System.Net
Imports System.Runtime.CompilerServices
Imports System.Security.Cryptography
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Web
Imports System.Windows.Forms
Imports auth.My
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports Microsoft.Win32
Imports Newtonsoft.Json.Linq

Namespace auth
	' Token: 0x02000008 RID: 8
	<DesignerGenerated()>
	Public Partial Class Form1
		Inherits Form

		' Token: 0x06000014 RID: 20 RVA: 0x00002320 File Offset: 0x00000520
		Public Sub New()
			AddHandler MyBase.Load, AddressOf Me.Form1_Load
			Me.keyCustom = Encoding.UTF8.GetBytes("EA0fPST98s4qIOmqr1nlxOPr5fWfDmUE")
			Me.ivCustom = Encoding.UTF8.GetBytes("relylukeaaaadddd")
			Me.BgEDL = New BackgroundWorker()
			Me.username = ""
			Me.password = ""
			Me.credit = Conversions.ToString(0)
			Me.servername = "https://api.miazetool.com"
			Me.manualmode = True
			Me.serviceid = Conversions.ToString(3)
			Me.blobConfig = ""
			Me.CpuType = "02 00 00 00"
			Me.CpuCode = "01 34 02 20"
			Me.succesAuth = False
			Me.InitializeComponent()
		End Sub

		' Token: 0x06000015 RID: 21 RVA: 0x000023F8 File Offset: 0x000005F8
		Private Sub Form1_Load(sender As Object, e As EventArgs)
			Me.Text = "Mi-AzeTool SPFT v6 "
			Dim flag As Boolean = Not File.Exists("encrpyt")
			If flag Then
				Interaction.MsgBox("Blob key not exist", MsgBoxStyle.OkOnly, Nothing)
				MyBase.Invoke(New Action(Sub()
					MyBase.Close()
				End Sub))
			Else
				Me.blobkeyory = File.ReadAllText("encrpyt")
				MyBase.Size = New Size(420, 500)
				Me.Panel1.Visible = False
				Me.Panel2.Visible = True
				Me.Panel2.Location = New Point(13, 22)
				Me.CheckServer("flash")
				Me.CheckServer("frp")
				Me.CheckServer("fdl")
				Me.TextBox1.Text = "username"
				Me.TextBox2.Text = "Password"
				Me.regKey = MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\AYANG_MBEB_TOOL", True)
				Dim flag2 As Boolean = Me.regKey Is Nothing
				If flag2 Then
					MyProject.Computer.Registry.CurrentUser.CreateSubKey("Software\AYANG_MBEB_TOOL", True).SetValue("username", "username")
					MyProject.Computer.Registry.CurrentUser.CreateSubKey("Software\AYANG_MBEB_TOOL", True).SetValue("password", "Password")
					MyProject.Computer.Registry.CurrentUser.CreateSubKey("Software\AYANG_MBEB_TOOL", True).SetValue("remember", False)
				Else
					Dim us As Object = RuntimeHelpers.GetObjectValue(Me.regKey.GetValue("username", "username"))
					Dim passe As Object = RuntimeHelpers.GetObjectValue(Me.regKey.GetValue("password", "Password"))
					Dim flag3 As Boolean = Operators.ConditionalCompareObjectEqual(us, "", False)
					If flag3 Then
						Me.TextBox1.Text = "username"
					Else
						Dim flag4 As Boolean = Operators.ConditionalCompareObjectEqual(us, "username", False)
						If flag4 Then
							Me.TextBox1.Text = "username"
						Else
							Me.TextBox1.Text = Conversions.ToString(us)
						End If
					End If
					Dim flag5 As Boolean = Operators.ConditionalCompareObjectEqual(passe, "", False)
					If flag5 Then
						Me.TextBox2.Text = "Password"
					Else
						Dim flag6 As Boolean = Operators.ConditionalCompareObjectEqual(passe, "Password", False)
						If flag6 Then
							Me.TextBox2.Text = "Password"
						Else
							Me.TextBox2.PasswordChar = "*"c
							Me.TextBox2.Text = Conversions.ToString(passe)
						End If
					End If
					Me.CheckBox1.Checked = Conversions.ToBoolean(Me.regKey.GetValue("remember", False))
				End If
				Me.blobkey = Me.blobkeyory
				Dim ll As Byte() = Convert.FromBase64String(Me.blobkey)
				Dim nn As String = Me.AESDecrypt(ll, CType(Me.keyCustom, Byte()), CType(Me.ivCustom, Byte()))
				Dim pp As String = Encoding.UTF8.GetString(Convert.FromBase64String(nn.Substring(0, nn.Length - 16)))
				Dim qq As String = pp.Substring(0, pp.Length - 32)
				Dim rr As String = pp.Substring(pp.Length - 32)
				Dim ss As String = Me.reversebyte(rr)
				Me.blobkey = Convert.ToBase64String(Me.HexStringToBytes(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Me.CpuType, Me.CpuCode), qq), "0310"), ss))))
				Me.blobConfig = Me.blobkey
				Me.serviceid = Conversions.ToString(1)
				Me.RichTextBox1.Invoke(New Action(Sub()
					Me.RichTextBox1.Text = Me.blobkey
				End Sub))
				Me.Label1.Text = "Welcome Dear User"
				Me.CheckPricing()
			End If
		End Sub

		' Token: 0x06000016 RID: 22 RVA: 0x000027DC File Offset: 0x000009DC
		Public Sub CheckPricing()
			Me.logs("Checking  Price  ", True)
			Dim res As String = ""
			Try
				ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
				Dim PostData As String = ""
				Dim request As WebRequest = WebRequest.Create(Me.servername + "/price.php")
				request.Method = "POST"
				Dim byteArray As Byte() = Encoding.UTF8.GetBytes(PostData)
				request.ContentType = "application/x-www-form-urlencoded"
				request.ContentLength = CLng(byteArray.Length)
				request.Timeout = 60000
				Dim dataStream As Stream = request.GetRequestStream()
				dataStream.Write(byteArray, 0, byteArray.Length)
				dataStream.Close()
				Dim response As WebResponse = request.GetResponse()
				dataStream = response.GetResponseStream()
				Dim reader As StreamReader = New StreamReader(dataStream)
				Dim responseFromServer As String = reader.ReadToEnd()
				res = responseFromServer
				Dim read As JObject = JObject.Parse(responseFromServer)
				Dim flag As Boolean = read("priceflash") Is Nothing
				If flag Then
					Me.logs("Error " + responseFromServer, True)
				Else
					Dim flag2 As Boolean = read("pricefrp").ToString() = Nothing
					If flag2 Then
						Me.logs("Error " + responseFromServer, True)
					Else
						Dim flag3 As Boolean = read("pricefdl").ToString() = Nothing
						If flag3 Then
							Me.logs("Error " + responseFromServer, True)
						Else
							Dim flag4 As Boolean = read("pricemtk6").ToString() = Nothing
							If flag4 Then
								Me.logs("Error " + responseFromServer, True)
							Else
								Dim flag5 As Boolean = read("pricemtk5").ToString() = Nothing
								If flag5 Then
									Me.logs("Error " + responseFromServer, True)
								Else
									Dim priceflash As String = read("priceflash").ToString()
									Dim pricefrp As String = read("pricefrp").ToString()
									Dim pricefdl As String = read("pricefdl").ToString()
									Dim pricemtk5 As String = read("pricemtk5").ToString()
									Dim pricemtk6 As String = read("pricemtk6").ToString()
									Me.logs("Flash Qcom " & vbTab & vbTab + priceflash, True)
									Me.logs("Flash FRP " & vbTab & vbTab + pricefrp, True)
									Me.logs("Flash FDL " & vbTab & vbTab + pricefdl, True)
									Me.logs("Flash MTK v5 " & vbTab & vbTab + pricemtk5, True)
									Me.logs("Flash MTK v6 " & vbTab & vbTab + pricemtk6, True)
								End If
							End If
						End If
					End If
				End If
			Catch ex As Exception
				Me.logs(res, True)
				Me.logs(ex.ToString(), True)
			End Try
		End Sub

		' Token: 0x06000017 RID: 23 RVA: 0x00002ABC File Offset: 0x00000CBC
		Private Function reversebyte(rr As String) As String
			Dim ret As String = ""
			rr = rr.Replace(" ", "")
			Dim cfh As Integer = 0
			Dim num As Double = CDbl(rr.Length) / 4.0 - 1.0
			Dim i As Double = 0.0
			While i <= num
				Dim sh As String = rr.Substring(cfh, 4)
				cfh += 4
				Dim gttt As String = Me.rever(sh)
				ret += gttt
				i += 1.0
			End While
			Return ret
		End Function

		' Token: 0x06000018 RID: 24 RVA: 0x00002B48 File Offset: 0x00000D48
		Private Function rever(sh As String) As String
			Dim a As String = sh.Substring(0, 2)
			Dim b As String = sh.Substring(2, 2)
			Return b + a
		End Function

		' Token: 0x06000019 RID: 25 RVA: 0x00002B7C File Offset: 0x00000D7C
		Public Sub rtbx1(text As String, newline As Boolean)
			If newline Then
				Me.RichTextBox1.Invoke(New Action(Sub()
					Dim richTextBox As RichTextBox = Me.RichTextBox1
					Dim richTextBox2 As RichTextBox = richTextBox
					richTextBox.Text = richTextBox2.Text + text + vbCrLf
				End Sub))
			Else
				Me.RichTextBox1.Invoke(New Action(Sub()
					Dim richTextBox As RichTextBox = Me.RichTextBox1
					Dim richTextBox2 As RichTextBox = richTextBox
					richTextBox.Text = richTextBox2.Text + text
				End Sub))
			End If
		End Sub

		' Token: 0x0600001A RID: 26 RVA: 0x00002BD8 File Offset: 0x00000DD8
		Public Sub rtbx2(text As String, newline As Boolean)
			If newline Then
				Me.RichTextBox2.Invoke(New Action(Sub()
					Dim richTextBox As RichTextBox = Me.RichTextBox2
					Dim richTextBox2 As RichTextBox = richTextBox
					richTextBox.Text = richTextBox2.Text + text + vbCrLf
				End Sub))
			Else
				Me.RichTextBox2.Invoke(New Action(Sub()
					Dim richTextBox As RichTextBox = Me.RichTextBox2
					Dim richTextBox2 As RichTextBox = richTextBox
					richTextBox.Text = richTextBox2.Text + text
				End Sub))
			End If
		End Sub

		' Token: 0x0600001B RID: 27 RVA: 0x00002C34 File Offset: 0x00000E34
		Public Sub CheckServer(suport As String)
			Me.logs("Checking  Server [ " + suport + " ] : ", False)
			Try
				ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
				Dim PostData As String = "cek=" + suport + "&password=" + Me.password
				Dim request As WebRequest = WebRequest.Create("https://vegito-auth.com/apibridge/cekserver.php")
				request.Method = "POST"
				Dim byteArray As Byte() = Encoding.UTF8.GetBytes(PostData)
				request.ContentType = "application/x-www-form-urlencoded"
				request.ContentLength = CLng(byteArray.Length)
				request.Timeout = 60000
				Dim dataStream As Stream = request.GetRequestStream()
				dataStream.Write(byteArray, 0, byteArray.Length)
				dataStream.Close()
				Dim response As WebResponse = request.GetResponse()
				dataStream = response.GetResponseStream()
				Dim reader As StreamReader = New StreamReader(dataStream)
				Dim responseFromServer As String = reader.ReadToEnd()
				Me.logs(responseFromServer + " Online ", True)
			Catch ex As Exception
				Interaction.MsgBox(ex.ToString(), MsgBoxStyle.OkOnly, Nothing)
			End Try
		End Sub

		' Token: 0x0600001C RID: 28 RVA: 0x00002D4C File Offset: 0x00000F4C
		Public Sub rtbx3(text As String, newline As Boolean)
			If newline Then
				Me.RichTextBox3.Invoke(New Action(Sub()
					Dim richTextBox As RichTextBox = Me.RichTextBox3
					Dim richTextBox2 As RichTextBox = richTextBox
					richTextBox.Text = richTextBox2.Text + text + vbCrLf
				End Sub))
			Else
				Me.RichTextBox3.Invoke(New Action(Sub()
					Dim richTextBox As RichTextBox = Me.RichTextBox3
					Dim richTextBox2 As RichTextBox = richTextBox
					richTextBox.Text = richTextBox2.Text + text
				End Sub))
			End If
		End Sub

		' Token: 0x0600001D RID: 29 RVA: 0x00002DA8 File Offset: 0x00000FA8
		Private Function AESDecrypt(cipherText As Byte(), Key As Byte(), IV As Byte()) As String
			Dim finalkey As Byte() = New Byte(31) {}
			Buffer.BlockCopy(Key, 0, finalkey, 0, 32)
			Key = finalkey
			Dim finalIv As Byte() = New Byte(15) {}
			Buffer.BlockCopy(IV, 0, finalIv, 0, 16)
			IV = finalIv
			Dim AESDecrypt As String
			Try
				Dim Algo As Rijndael = Rijndael.Create()
				Dim rijndael As Rijndael = Algo
				rijndael.BlockSize = 128
				rijndael.FeedbackSize = 128
				rijndael.KeySize = 128
				rijndael.Mode = CipherMode.CBC
				rijndael.IV = IV
				rijndael.Key = Key
				rijndael.Padding = PaddingMode.None
				Dim buffout As Byte() = New Byte(cipherText.Length - 1 + 1 - 1) {}
				Dim s As MemoryStream = New MemoryStream(buffout)
				Using Decryptor As ICryptoTransform = Algo.CreateDecryptor()
					Using StreamInput As MemoryStream = New MemoryStream(cipherText)
						Using crypto_stream As CryptoStream = New CryptoStream(s, Decryptor, CryptoStreamMode.Write)
							Dim buffer As Byte() = New Byte(1024) {}
							Dim flag As Boolean
							Do
								Dim bytes_read As Integer = StreamInput.Read(buffer, 0, 1024)
								crypto_stream.Write(buffer, 0, bytes_read)
								flag = (bytes_read = 0)
							Loop While Not flag
						End Using
					End Using
				End Using
				AESDecrypt = Encoding.UTF8.GetString(buffout)
			Catch ex As Exception
				Interaction.MsgBox(ex.ToString(), MsgBoxStyle.OkOnly, Nothing)
			End Try
			Return AESDecrypt
		End Function

		' Token: 0x0600001E RID: 30 RVA: 0x00002F78 File Offset: 0x00001178
		Private Shared Function AESEncrypt(cipherText As Byte(), Key As Byte(), IV As Byte()) As String
			Dim finalkey As Byte() = New Byte(31) {}
			Buffer.BlockCopy(Key, 0, finalkey, 0, 32)
			Key = finalkey
			Dim finalIv As Byte() = New Byte(15) {}
			Buffer.BlockCopy(IV, 0, finalIv, 0, 16)
			IV = finalIv
			Dim AESEncrypt As String
			Try
				Dim Algo As RijndaelManaged = CType(Rijndael.Create(), RijndaelManaged)
				Dim rijndaelManaged As RijndaelManaged = Algo
				rijndaelManaged.BlockSize = 128
				rijndaelManaged.FeedbackSize = 128
				rijndaelManaged.KeySize = 128
				rijndaelManaged.Mode = CipherMode.CBC
				rijndaelManaged.IV = IV
				rijndaelManaged.Key = Key
				rijndaelManaged.Padding = PaddingMode.None
				Dim buffout As Byte() = New Byte(cipherText.Length - 1 + 1 - 1) {}
				Dim s As MemoryStream = New MemoryStream(buffout)
				Using Decryptor As ICryptoTransform = Algo.CreateEncryptor()
					Using StreamInput As MemoryStream = New MemoryStream(cipherText)
						Using crypto_stream As CryptoStream = New CryptoStream(s, Decryptor, CryptoStreamMode.Write)
							Dim buffer As Byte() = New Byte(128) {}
							Dim flag As Boolean
							Do
								Dim bytes_read As Integer = StreamInput.Read(buffer, 0, 128)
								crypto_stream.Write(buffer, 0, bytes_read)
								flag = (bytes_read = 0)
							Loop While Not flag
						End Using
					End Using
				End Using
				AESEncrypt = Convert.ToBase64String(buffout)
			Catch ex As Exception
				Interaction.MsgBox(ex.ToString(), MsgBoxStyle.OkOnly, Nothing)
			End Try
			Return AESEncrypt
		End Function

		' Token: 0x0600001F RID: 31 RVA: 0x00003148 File Offset: 0x00001348
		Private Function HexStringToBytes(s As String) As Byte()
			' The following expression was wrapped in a checked-statement
			Dim HexStringToBytes As Byte()
			Try
				s = s.Replace(" ", "")
				Dim nBytes As Integer = s.Length / 2
				Dim a As Byte() = New Byte(nBytes - 1 + 1 - 1) {}
				Dim num As Integer = nBytes - 1
				For i As Integer = 0 To num
					a(i) = Convert.ToByte(s.Substring(i * 2, 2), 16)
				Next
				HexStringToBytes = a
			Catch ex As Exception
			End Try
			Return HexStringToBytes
		End Function

		' Token: 0x06000020 RID: 32 RVA: 0x000031D0 File Offset: 0x000013D0
		Public Function ByteToHex(input As Byte()) As String
			Return BitConverter.ToString(input).Replace("-", "").ToLower()
		End Function

		' Token: 0x06000021 RID: 33 RVA: 0x000031FC File Offset: 0x000013FC
		Private Sub Button1_Click(sender As Object, e As EventArgs)
			Dim flag As Boolean = Conversions.ToDouble(Me.serviceid) = 7.0
			If flag Then
				Dim gtryui As String = Me.dataAuth + "0808080808080808"
				Dim jhy As String = Form1.AESEncrypt(Encoding.UTF8.GetBytes(gtryui), CType(Me.keyCustom, Byte()), CType(Me.ivCustom, Byte()))
				File.WriteAllText("decode", jhy)
			Else
				Dim flag2 As Boolean = Conversions.ToDouble(Me.serviceid) = 6.0
				If flag2 Then
					File.WriteAllText("decode", Me.dataAuth)
				End If
			End If
			MyBase.Close()
		End Sub

		' Token: 0x06000022 RID: 34 RVA: 0x000032A0 File Offset: 0x000014A0
		Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs)
			Me.regKey = MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\AYANG_MBEB_TOOL", True)
			Dim flag As Boolean = Me.regKey Is Nothing
			If flag Then
				MyProject.Computer.Registry.CurrentUser.CreateSubKey("Software\AYANG_MBEB_TOOL", False).SetValue("username", "username")
				MyProject.Computer.Registry.CurrentUser.CreateSubKey("Software\AYANG_MBEB_TOOL", False).SetValue("password", "Password")
				MyProject.Computer.Registry.CurrentUser.CreateSubKey("Software\AYANG_MBEB_TOOL", False).SetValue("remember", "false")
			End If
			Dim checked As Boolean = Me.CheckBox1.Checked
			If checked Then
				Me.regKey.SetValue("username", Me.TextBox1.Text)
				Me.regKey.SetValue("password", Me.TextBox2.Text)
				Me.regKey.SetValue("remember", True)
			Else
				Dim flag2 As Boolean = Not Me.CheckBox1.Checked
				If flag2 Then
					Me.regKey.SetValue("username", "username")
					Me.regKey.SetValue("password", "Password")
					Me.regKey.SetValue("remember", False)
				End If
			End If
		End Sub

		' Token: 0x06000023 RID: 35 RVA: 0x00003414 File Offset: 0x00001614
		Private Sub Button5_Click(sender As Object, e As EventArgs)
			Dim isBusy As Boolean = Me.BgEDL.IsBusy
			If Not isBusy Then
				Me.BgEDL = New BackgroundWorker()
				Me.BgEDL.WorkerSupportsCancellation = True
				AddHandler Me.BgEDL.DoWork, Sub(a0 As Object, a1 As DoWorkEventArgs)
					Me.cekCredit(RuntimeHelpers.GetObjectValue(a0), a1)
				End Sub
				Me.BgEDL.RunWorkerAsync()
				Me.BgEDL.Dispose()
			End If
		End Sub

		' Token: 0x06000024 RID: 36 RVA: 0x00003480 File Offset: 0x00001680
		Public Function cekCredit(sender As Object, e As DoWorkEventArgs) As Boolean
			Me.username = Me.TextBox1.Text
			Me.password = Me.TextBox2.Text
			Dim flag As Boolean = Operators.CompareString(Me.username, "username", False) = 0
			Dim cekCredit As Boolean
			If flag Then
				Me.rtbx3("Please Username And Password", True)
				cekCredit = False
			Else
				Dim flag2 As Boolean = Operators.CompareString(Me.password, "Password", False) = 0
				If flag2 Then
					Me.rtbx3("Please Username And Password", True)
					cekCredit = False
				Else
					Me.rtbx3("Your Balance : ", False)
					Try
						ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
						Dim PostData As String = "username=" + Me.username + "&password=" + Me.password
						Dim request As WebRequest = WebRequest.Create(Me.servername + "/cekcredit.php")
						request.Method = "POST"
						Dim byteArray As Byte() = Encoding.UTF8.GetBytes(PostData)
						request.ContentType = "application/x-www-form-urlencoded"
						request.ContentLength = CLng(byteArray.Length)
						request.Timeout = 30000
						Dim dataStream As Stream = request.GetRequestStream()
						dataStream.Write(byteArray, 0, byteArray.Length)
						dataStream.Close()
						Dim response As WebResponse = request.GetResponse()
						dataStream = response.GetResponseStream()
						Dim reader As StreamReader = New StreamReader(dataStream)
						Dim responseFromServer As String = reader.ReadToEnd()
						Thread.Sleep(1000)
						Dim readers As StringReader = New StringReader(responseFromServer)
						Dim resp As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
						Me.rtbx3(responseFromServer, True)
					Catch ex As Exception
						Interaction.MsgBox(ex.ToString(), MsgBoxStyle.OkOnly, Nothing)
						cekCredit = False
					End Try
				End If
			End If
			Return cekCredit
		End Function

		' Token: 0x06000025 RID: 37 RVA: 0x00003640 File Offset: 0x00001840
		Private Sub TextBox1_Enter(sender As Object, e As EventArgs)
			Dim flag As Boolean = Operators.CompareString(Me.TextBox1.Text, "username", False) = 0
			If flag Then
				Me.TextBox1.Text = ""
			End If
		End Sub

		' Token: 0x06000026 RID: 38 RVA: 0x00003680 File Offset: 0x00001880
		Private Sub TextBox1_Leave(sender As Object, e As EventArgs)
			Dim flag As Boolean = Operators.CompareString(Me.TextBox1.Text, "", False) = 0
			If flag Then
				Me.TextBox1.Text = "username"
			End If
		End Sub

		' Token: 0x06000027 RID: 39 RVA: 0x000036C0 File Offset: 0x000018C0
		Private Sub TextBox2_Enter(sender As Object, e As EventArgs)
			Me.TextBox2.PasswordChar = "*"c
			Dim flag As Boolean = Operators.CompareString(Me.TextBox2.Text, "Password", False) = 0
			If flag Then
				Me.TextBox2.Text = ""
			End If
		End Sub

		' Token: 0x06000028 RID: 40 RVA: 0x0000370C File Offset: 0x0000190C
		Private Sub TextBox2_Leave(sender As Object, e As EventArgs)
			Dim flag As Boolean = Operators.CompareString(Me.TextBox2.Text, "", False) = 0
			If flag Then
				Me.TextBox2.Text = "Password"
				Me.TextBox2.PasswordChar = vbNullChar
			End If
		End Sub

		' Token: 0x06000029 RID: 41 RVA: 0x0000375C File Offset: 0x0000195C
		Private Sub Button2_Click(sender As Object, e As EventArgs)
			Dim xxxxx As String = "pte6BjmU9Quvju279XMR2bmlZNZHguJwMoXBG3T4vH3mqvw/vR/NRHg1N62hiXwE9LKtetAcU5uz9q/IwyltQgVCK9pIyCCI86tH8xmT76jo2Uin+aOLxsZvPaaWfbcndApFJcppQ9ICN3VOQy6w7wwcpTW+6QlTleId4W/Zn+GrK/raQHcsrj6UsyQk86zzIJXlfJ1ernImiizJbPNnxRaR1LXqirXpcblAi+RSPh0C4MPQA/1bCWcPihGgS3Zwr96LZKV8VwTd77WJe3qCQUG2s4QQnU/wdFxtwHy6/6AQBrtI11ww3FwHn/SQyp8tRow+jDiAMqaCfLZs3ejmRYgRS2+2zQWuf02TO59SDZgylSjY7GsvtWwLKCrHlkHlewAaciVxDe2QnOk5fKapy+VFkqT9/vD6tBTCI0lhvyBgsKEcr5QbR+/SRELtJQPHkIvDfcPXuCmwUHFMxTc5RRMZHsZ71LcwzB+Dtv22jNgVAxUVtcP7ebt9oxfNKx/7gdCiWXtM9DcayJx3JnD9Yg3hc2iekBh0wtpbBgI5Ii2Sr3yS9Kgu93/p78bd+Q0tFwaWW7m+nzsOtBmwAXyYUdPrcQzyuQ8e+ZXJvOq1NDi3Q36LwPesNFfmf7OTok9dHq0C094tk4xxDVOPbFbfwZ+faXojcQcmasXlC27QMhbdY/GT9R9fL+OhOkdAFPlX"
			Dim blobkey As String = Me.AESDecrypt(Convert.FromBase64String(xxxxx), CType(Me.keyCustom, Byte()), CType(Me.ivCustom, Byte()))
			Interaction.MsgBox(blobkey, MsgBoxStyle.OkOnly, Nothing)
			Interaction.MsgBox("config Copied to ClipBoard", MsgBoxStyle.OkOnly, Nothing)
			MyProject.Computer.Clipboard.SetText(Me.RichTextBox1.Text)
		End Sub

		' Token: 0x0600002A RID: 42 RVA: 0x000037C4 File Offset: 0x000019C4
		Private Sub Button3_Click(sender As Object, e As EventArgs)
			Dim flag As Boolean = MyProject.Computer.Clipboard.ContainsText()
			If flag Then
				Me.RichTextBox2.Text = MyProject.Computer.Clipboard.GetText()
			Else
				Interaction.MsgBox("pasted data is not text", MsgBoxStyle.OkOnly, Nothing)
			End If
		End Sub

		' Token: 0x0600002B RID: 43 RVA: 0x00003814 File Offset: 0x00001A14
		Private Sub Button4_Click(sender As Object, e As EventArgs)
			Me.RichTextBox1.Text = ""
			Dim isBusy As Boolean = Me.BgEDL.IsBusy
			If Not isBusy Then
				Me.BgEDL = New BackgroundWorker()
				Me.BgEDL.WorkerSupportsCancellation = True
				AddHandler Me.BgEDL.DoWork, AddressOf Me.sendauth
				AddHandler Me.BgEDL.RunWorkerCompleted, AddressOf Me.alldone
				Me.BgEDL.RunWorkerAsync()
				Me.BgEDL.Dispose()
			End If
		End Sub

		' Token: 0x0600002C RID: 44 RVA: 0x000038A8 File Offset: 0x00001AA8
		Private Sub sendauth(sender As Object, e As DoWorkEventArgs)
			Me.succesAuth = False
			Me.username = Me.TextBox1.Text
			Me.password = Me.TextBox2.Text
			Me.logs("Sending Auth ...", False)
			Me.serviceid = Conversions.ToString(3)
			Try
				ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
				Dim PostData As String = String.Concat(New String() { "username=", Me.username, "&password=", Me.password, "&serviceId=", Me.serviceid, "&configblob=", Me.encodeURL(Me.blobConfig) })
				Dim request As WebRequest = WebRequest.Create(Me.servername + "/getauth.php")
				request.Method = "POST"
				Dim byteArray As Byte() = Encoding.UTF8.GetBytes(PostData)
				request.ContentType = "application/x-www-form-urlencoded"
				request.ContentLength = CLng(byteArray.Length)
				request.Timeout = 30000
				Dim dataStream As Stream = request.GetRequestStream()
				dataStream.Write(byteArray, 0, byteArray.Length)
				dataStream.Close()
				Dim response As WebResponse = request.GetResponse()
				dataStream = response.GetResponseStream()
				Dim reader As StreamReader = New StreamReader(dataStream)
				Dim responseFromServer As String = reader.ReadToEnd()
				Dim flag As Boolean = responseFromServer.Contains("sukses")
				If flag Then
					Me.logs("OK", True)
					Me.GetAuth(RuntimeHelpers.GetObjectValue(sender), e)
				Else
					Me.logs(responseFromServer, True)
				End If
			Catch ex As Exception
				Interaction.MsgBox(ex.ToString(), MsgBoxStyle.OkOnly, Nothing)
			End Try
		End Sub

		' Token: 0x0600002D RID: 45 RVA: 0x00003A6C File Offset: 0x00001C6C
		Private Sub GetAuth(sender As Object, e As DoWorkEventArgs)
			Me.succesAuth = False
			Me.serviceid = Conversions.ToString(3)
			Me.username = Me.TextBox1.Text
			Me.password = Me.TextBox2.Text
			Me.logs("Get Auth Response ...", False)
			Try
				ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
				Dim PostData As String = String.Concat(New String() { "username=", Me.username, "&password=", Me.password, "&serviceId=", Me.serviceid, "&configblob=", Me.encodeURL(Me.blobConfig) })
				Dim request As WebRequest = WebRequest.Create(Me.servername + "/grabauth.php")
				request.Method = "POST"
				Dim byteArray As Byte() = Encoding.UTF8.GetBytes(PostData)
				request.ContentType = "application/x-www-form-urlencoded"
				request.ContentLength = CLng(byteArray.Length)
				request.Timeout = 30000
				Dim dataStream As Stream = request.GetRequestStream()
				dataStream.Write(byteArray, 0, byteArray.Length)
				dataStream.Close()
				Dim response As WebResponse = request.GetResponse()
				dataStream = response.GetResponseStream()
				Dim reader As StreamReader = New StreamReader(dataStream)
				Dim responseFromServer As String = reader.ReadToEnd()
				Dim flag As Boolean = responseFromServer.Contains("sukses")
				If flag Then
					Me.dataAuth = responseFromServer.Replace("sukses", "")
					Dim nnnn As String = Me.reversebyte(Me.dataAuth)
					Dim hexadd As String = "10101010101010101010101010101010"
					Me.dataAuth = Form1.AESEncrypt(Encoding.UTF8.GetBytes(nnnn + Encoding.UTF8.GetString(Me.HexStringToBytes(hexadd))), CType(Me.keyCustom, Byte()), CType(Me.ivCustom, Byte()))
					File.WriteAllText("decode", Me.dataAuth)
					Me.logs("OK ", True)
					Thread.Sleep(2000)
					MyBase.Invoke(New Action(Sub()
						MyBase.Close()
					End Sub))
				Else
					Me.logs(responseFromServer, True)
				End If
			Catch ex As Exception
				Interaction.MsgBox(ex.ToString(), MsgBoxStyle.OkOnly, Nothing)
			End Try
		End Sub

		' Token: 0x0600002E RID: 46 RVA: 0x00003CC4 File Offset: 0x00001EC4
		Private Sub alldone(sender As Object, e As RunWorkerCompletedEventArgs)
		End Sub

		' Token: 0x0600002F RID: 47 RVA: 0x00003CC8 File Offset: 0x00001EC8
		Private Function encodeURL(input As String) As String
			Dim lower As String = HttpUtility.UrlEncode(input)
			Dim reg As Regex = New Regex("%[a-f0-9]{2}")
			Dim regex As Regex = reg
			Dim input2 As String = lower
			Dim evaluator As MatchEvaluator
			If Form1._Closure$__.$I47-0 IsNot Nothing Then
				evaluator = Form1._Closure$__.$I47-0
			Else
				Dim matchEvaluator As MatchEvaluator = Function(m As Match) m.Value.ToUpperInvariant()
				evaluator = matchEvaluator
				Form1._Closure$__.$I47-0 = matchEvaluator
			End If
			Return regex.Replace(input2, evaluator)
		End Function

		' Token: 0x06000030 RID: 48 RVA: 0x00003D1C File Offset: 0x00001F1C
		Public Sub updateProgressbar(value As Integer)
			Me.ProgressBar1.Invoke(New Action(Sub()
				Me.ProgressBar1.Value = value
			End Sub))
		End Sub

		' Token: 0x06000031 RID: 49 RVA: 0x00003D58 File Offset: 0x00001F58
		Public Sub logs(text As String, newline As Boolean)
			If newline Then
				Me.RichTextBox3.Invoke(New Action(Sub()
					Dim richTextBox As RichTextBox = Me.RichTextBox3
					Dim richTextBox2 As RichTextBox = richTextBox
					richTextBox.Text = richTextBox2.Text + text + vbCrLf
				End Sub))
			Else
				Me.RichTextBox3.Invoke(New Action(Sub()
					Dim richTextBox As RichTextBox = Me.RichTextBox3
					Dim richTextBox2 As RichTextBox = richTextBox
					richTextBox.Text = richTextBox2.Text + text
				End Sub))
			End If
		End Sub

		' Token: 0x06000032 RID: 50 RVA: 0x00003DB4 File Offset: 0x00001FB4
		Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs)
			MyBase.Size = New Size(420, 500)
			Me.Panel1.Visible = False
			Me.Panel2.Visible = True
			Me.Panel2.Location = New Point(13, 22)
			Me.TextBox1.Text = "username"
			Me.TextBox2.Text = "Passoword"
			Me.regKey = MyProject.Computer.Registry.CurrentUser.OpenSubKey("Software\AYANG_MBEB_TOOL", True)
			Dim flag As Boolean = Me.regKey Is Nothing
			If flag Then
				MyProject.Computer.Registry.CurrentUser.CreateSubKey("Software\AYANG_MBEB_TOOL", False).SetValue("username", "username")
				MyProject.Computer.Registry.CurrentUser.CreateSubKey("Software\AYANG_MBEB_TOOL", False).SetValue("password", "Password")
				MyProject.Computer.Registry.CurrentUser.CreateSubKey("Software\AYANG_MBEB_TOOL", False).SetValue("remember", "false")
				MyProject.Computer.Registry.CurrentUser.CreateSubKey("Software\AYANG_MBEB_TOOL", False).SetValue("servername", "false")
			Else
				Dim us As Object = RuntimeHelpers.GetObjectValue(Me.regKey.GetValue("username", "Default Value"))
				Dim passe As Object = RuntimeHelpers.GetObjectValue(Me.regKey.GetValue("password", "Default Value"))
				Me.TextBox3.Text = Conversions.ToString(Me.regKey.GetValue("servername", "Default Value"))
				Dim flag2 As Boolean = Operators.ConditionalCompareObjectEqual(us, "", False)
				If flag2 Then
					Me.TextBox1.Text = "username"
				Else
					Dim flag3 As Boolean = Operators.ConditionalCompareObjectEqual(us, "username", False)
					If flag3 Then
						Me.TextBox1.Text = "username"
					Else
						Me.TextBox1.Text = Conversions.ToString(us)
					End If
				End If
				Dim flag4 As Boolean = Operators.ConditionalCompareObjectEqual(passe, "", False)
				If flag4 Then
					Me.TextBox2.Text = "Password"
				Else
					Dim flag5 As Boolean = Operators.ConditionalCompareObjectEqual(passe, "Password", False)
					If flag5 Then
						Me.TextBox2.Text = "Password"
					Else
						Me.TextBox2.PasswordChar = "*"c
						Me.TextBox2.Text = Conversions.ToString(passe)
					End If
				End If
				Me.CheckBox1.Checked = Conversions.ToBoolean(Me.regKey.GetValue("remember", "Default Value"))
			End If
		End Sub

		' Token: 0x06000033 RID: 51 RVA: 0x00004057 File Offset: 0x00002257
		Private Sub Label1_Click(sender As Object, e As EventArgs)
		End Sub

		' Token: 0x06000034 RID: 52 RVA: 0x0000405A File Offset: 0x0000225A
		Private Sub PictureBox3_Click(sender As Object, e As EventArgs)
		End Sub

		' Token: 0x06000035 RID: 53 RVA: 0x0000405D File Offset: 0x0000225D
		Private Sub label2_Click(sender As Object, e As EventArgs)
			Process.Start(New ProcessStartInfo("https://wa.me/+994554680101") With { .UseShellExecute = True })
		End Sub

		' Token: 0x06000036 RID: 54 RVA: 0x00004078 File Offset: 0x00002278
		Private Sub pictureBox1_Click(sender As Object, e As EventArgs)
			Process.Start(New ProcessStartInfo("https://t.me/g2s7mx") With { .UseShellExecute = True })
		End Sub

		' Token: 0x06000037 RID: 55 RVA: 0x00004093 File Offset: 0x00002293
		Private Sub Button6_Click(sender As Object, e As EventArgs)
		End Sub

		' Token: 0x1700000A RID: 10
		' (get) Token: 0x0600003A RID: 58 RVA: 0x00004DA4 File Offset: 0x00002FA4
		' (set) Token: 0x0600003B RID: 59 RVA: 0x00004DAE File Offset: 0x00002FAE
		Friend Overridable Property RichTextBox1 As RichTextBox

		' Token: 0x1700000B RID: 11
		' (get) Token: 0x0600003C RID: 60 RVA: 0x00004DB7 File Offset: 0x00002FB7
		' (set) Token: 0x0600003D RID: 61 RVA: 0x00004DC1 File Offset: 0x00002FC1
		Friend Overridable Property RichTextBox2 As RichTextBox

		' Token: 0x1700000C RID: 12
		' (get) Token: 0x0600003E RID: 62 RVA: 0x00004DCA File Offset: 0x00002FCA
		' (set) Token: 0x0600003F RID: 63 RVA: 0x00004DD4 File Offset: 0x00002FD4
		Friend Overridable Property Button1 As Button
			<CompilerGenerated()>
			Get
				Return Me._Button1
			End Get
			<CompilerGenerated()>
			<MethodImpl(MethodImplOptions.Synchronized)>
			Set(value As Button)
				Dim value2 As EventHandler = AddressOf Me.Button1_Click
				Dim button As Button = Me._Button1
				If button IsNot Nothing Then
					RemoveHandler button.Click, value2
				End If
				Me._Button1 = value
				button = Me._Button1
				If button IsNot Nothing Then
					AddHandler button.Click, value2
				End If
			End Set
		End Property

		' Token: 0x1700000D RID: 13
		' (get) Token: 0x06000040 RID: 64 RVA: 0x00004E17 File Offset: 0x00003017
		' (set) Token: 0x06000041 RID: 65 RVA: 0x00004E24 File Offset: 0x00003024
		Friend Overridable Property Button2 As Button
			<CompilerGenerated()>
			Get
				Return Me._Button2
			End Get
			<CompilerGenerated()>
			<MethodImpl(MethodImplOptions.Synchronized)>
			Set(value As Button)
				Dim value2 As EventHandler = AddressOf Me.Button2_Click
				Dim button As Button = Me._Button2
				If button IsNot Nothing Then
					RemoveHandler button.Click, value2
				End If
				Me._Button2 = value
				button = Me._Button2
				If button IsNot Nothing Then
					AddHandler button.Click, value2
				End If
			End Set
		End Property

		' Token: 0x1700000E RID: 14
		' (get) Token: 0x06000042 RID: 66 RVA: 0x00004E67 File Offset: 0x00003067
		' (set) Token: 0x06000043 RID: 67 RVA: 0x00004E74 File Offset: 0x00003074
		Friend Overridable Property Button3 As Button
			<CompilerGenerated()>
			Get
				Return Me._Button3
			End Get
			<CompilerGenerated()>
			<MethodImpl(MethodImplOptions.Synchronized)>
			Set(value As Button)
				Dim value2 As EventHandler = AddressOf Me.Button3_Click
				Dim button As Button = Me._Button3
				If button IsNot Nothing Then
					RemoveHandler button.Click, value2
				End If
				Me._Button3 = value
				button = Me._Button3
				If button IsNot Nothing Then
					AddHandler button.Click, value2
				End If
			End Set
		End Property

		' Token: 0x1700000F RID: 15
		' (get) Token: 0x06000044 RID: 68 RVA: 0x00004EB7 File Offset: 0x000030B7
		' (set) Token: 0x06000045 RID: 69 RVA: 0x00004EC4 File Offset: 0x000030C4
		Friend Overridable Property Button4 As Button
			<CompilerGenerated()>
			Get
				Return Me._Button4
			End Get
			<CompilerGenerated()>
			<MethodImpl(MethodImplOptions.Synchronized)>
			Set(value As Button)
				Dim value2 As EventHandler = AddressOf Me.Button4_Click
				Dim button As Button = Me._Button4
				If button IsNot Nothing Then
					RemoveHandler button.Click, value2
				End If
				Me._Button4 = value
				button = Me._Button4
				If button IsNot Nothing Then
					AddHandler button.Click, value2
				End If
			End Set
		End Property

		' Token: 0x17000010 RID: 16
		' (get) Token: 0x06000046 RID: 70 RVA: 0x00004F07 File Offset: 0x00003107
		' (set) Token: 0x06000047 RID: 71 RVA: 0x00004F14 File Offset: 0x00003114
		Friend Overridable Property TextBox1 As TextBox
			<CompilerGenerated()>
			Get
				Return Me._TextBox1
			End Get
			<CompilerGenerated()>
			<MethodImpl(MethodImplOptions.Synchronized)>
			Set(value As TextBox)
				Dim value2 As EventHandler = AddressOf Me.TextBox1_Enter
				Dim value3 As EventHandler = AddressOf Me.TextBox1_Leave
				Dim textBox As TextBox = Me._TextBox1
				If textBox IsNot Nothing Then
					RemoveHandler textBox.Enter, value2
					RemoveHandler textBox.Leave, value3
				End If
				Me._TextBox1 = value
				textBox = Me._TextBox1
				If textBox IsNot Nothing Then
					AddHandler textBox.Enter, value2
					AddHandler textBox.Leave, value3
				End If
			End Set
		End Property

		' Token: 0x17000011 RID: 17
		' (get) Token: 0x06000048 RID: 72 RVA: 0x00004F72 File Offset: 0x00003172
		' (set) Token: 0x06000049 RID: 73 RVA: 0x00004F7C File Offset: 0x0000317C
		Friend Overridable Property TextBox2 As TextBox
			<CompilerGenerated()>
			Get
				Return Me._TextBox2
			End Get
			<CompilerGenerated()>
			<MethodImpl(MethodImplOptions.Synchronized)>
			Set(value As TextBox)
				Dim value2 As EventHandler = AddressOf Me.TextBox2_Enter
				Dim value3 As EventHandler = AddressOf Me.TextBox2_Leave
				Dim textBox As TextBox = Me._TextBox2
				If textBox IsNot Nothing Then
					RemoveHandler textBox.Enter, value2
					RemoveHandler textBox.Leave, value3
				End If
				Me._TextBox2 = value
				textBox = Me._TextBox2
				If textBox IsNot Nothing Then
					AddHandler textBox.Enter, value2
					AddHandler textBox.Leave, value3
				End If
			End Set
		End Property

		' Token: 0x17000012 RID: 18
		' (get) Token: 0x0600004A RID: 74 RVA: 0x00004FDA File Offset: 0x000031DA
		' (set) Token: 0x0600004B RID: 75 RVA: 0x00004FE4 File Offset: 0x000031E4
		Friend Overridable Property Button5 As Button
			<CompilerGenerated()>
			Get
				Return Me._Button5
			End Get
			<CompilerGenerated()>
			<MethodImpl(MethodImplOptions.Synchronized)>
			Set(value As Button)
				Dim value2 As EventHandler = AddressOf Me.Button5_Click
				Dim button As Button = Me._Button5
				If button IsNot Nothing Then
					RemoveHandler button.Click, value2
				End If
				Me._Button5 = value
				button = Me._Button5
				If button IsNot Nothing Then
					AddHandler button.Click, value2
				End If
			End Set
		End Property

		' Token: 0x17000013 RID: 19
		' (get) Token: 0x0600004C RID: 76 RVA: 0x00005027 File Offset: 0x00003227
		' (set) Token: 0x0600004D RID: 77 RVA: 0x00005034 File Offset: 0x00003234
		Friend Overridable Property CheckBox1 As CheckBox
			<CompilerGenerated()>
			Get
				Return Me._CheckBox1
			End Get
			<CompilerGenerated()>
			<MethodImpl(MethodImplOptions.Synchronized)>
			Set(value As CheckBox)
				Dim value2 As EventHandler = AddressOf Me.CheckBox1_CheckedChanged
				Dim checkBox As CheckBox = Me._CheckBox1
				If checkBox IsNot Nothing Then
					RemoveHandler checkBox.CheckedChanged, value2
				End If
				Me._CheckBox1 = value
				checkBox = Me._CheckBox1
				If checkBox IsNot Nothing Then
					AddHandler checkBox.CheckedChanged, value2
				End If
			End Set
		End Property

		' Token: 0x17000014 RID: 20
		' (get) Token: 0x0600004E RID: 78 RVA: 0x00005077 File Offset: 0x00003277
		' (set) Token: 0x0600004F RID: 79 RVA: 0x00005081 File Offset: 0x00003281
		Friend Overridable Property RichTextBox3 As RichTextBox

		' Token: 0x17000015 RID: 21
		' (get) Token: 0x06000050 RID: 80 RVA: 0x0000508A File Offset: 0x0000328A
		' (set) Token: 0x06000051 RID: 81 RVA: 0x00005094 File Offset: 0x00003294
		Friend Overridable Property Panel1 As Panel

		' Token: 0x17000016 RID: 22
		' (get) Token: 0x06000052 RID: 82 RVA: 0x0000509D File Offset: 0x0000329D
		' (set) Token: 0x06000053 RID: 83 RVA: 0x000050A7 File Offset: 0x000032A7
		Friend Overridable Property ProgressBar1 As ProgressBar

		' Token: 0x17000017 RID: 23
		' (get) Token: 0x06000054 RID: 84 RVA: 0x000050B0 File Offset: 0x000032B0
		' (set) Token: 0x06000055 RID: 85 RVA: 0x000050BA File Offset: 0x000032BA
		Friend Overridable Property Panel2 As Panel

		' Token: 0x17000018 RID: 24
		' (get) Token: 0x06000056 RID: 86 RVA: 0x000050C3 File Offset: 0x000032C3
		' (set) Token: 0x06000057 RID: 87 RVA: 0x000050D0 File Offset: 0x000032D0
		Friend Overridable Property CheckBox2 As CheckBox
			<CompilerGenerated()>
			Get
				Return Me._CheckBox2
			End Get
			<CompilerGenerated()>
			<MethodImpl(MethodImplOptions.Synchronized)>
			Set(value As CheckBox)
				Dim value2 As EventHandler = AddressOf Me.CheckBox2_CheckedChanged
				Dim checkBox As CheckBox = Me._CheckBox2
				If checkBox IsNot Nothing Then
					RemoveHandler checkBox.CheckedChanged, value2
				End If
				Me._CheckBox2 = value
				checkBox = Me._CheckBox2
				If checkBox IsNot Nothing Then
					AddHandler checkBox.CheckedChanged, value2
				End If
			End Set
		End Property

		' Token: 0x17000019 RID: 25
		' (get) Token: 0x06000058 RID: 88 RVA: 0x00005113 File Offset: 0x00003313
		' (set) Token: 0x06000059 RID: 89 RVA: 0x0000511D File Offset: 0x0000331D
		Friend Overridable Property CheckBox3 As CheckBox

		' Token: 0x1700001A RID: 26
		' (get) Token: 0x0600005A RID: 90 RVA: 0x00005126 File Offset: 0x00003326
		' (set) Token: 0x0600005B RID: 91 RVA: 0x00005130 File Offset: 0x00003330
		Friend Overridable Property TextBox3 As TextBox

		' Token: 0x1700001B RID: 27
		' (get) Token: 0x0600005C RID: 92 RVA: 0x00005139 File Offset: 0x00003339
		' (set) Token: 0x0600005D RID: 93 RVA: 0x00005144 File Offset: 0x00003344
		Friend Overridable Property Label1 As Label
			<CompilerGenerated()>
			Get
				Return Me._Label1
			End Get
			<CompilerGenerated()>
			<MethodImpl(MethodImplOptions.Synchronized)>
			Set(value As Label)
				Dim value2 As EventHandler = AddressOf Me.Label1_Click
				Dim label As Label = Me._Label1
				If label IsNot Nothing Then
					RemoveHandler label.Click, value2
				End If
				Me._Label1 = value
				label = Me._Label1
				If label IsNot Nothing Then
					AddHandler label.Click, value2
				End If
			End Set
		End Property

		' Token: 0x1700001C RID: 28
		' (get) Token: 0x0600005E RID: 94 RVA: 0x00005187 File Offset: 0x00003387
		' (set) Token: 0x0600005F RID: 95 RVA: 0x00005194 File Offset: 0x00003394
		Friend Overridable Property PictureBox3 As PictureBox
			<CompilerGenerated()>
			Get
				Return Me._PictureBox3
			End Get
			<CompilerGenerated()>
			<MethodImpl(MethodImplOptions.Synchronized)>
			Set(value As PictureBox)
				Dim value2 As EventHandler = AddressOf Me.PictureBox3_Click
				Dim pictureBox As PictureBox = Me._PictureBox3
				If pictureBox IsNot Nothing Then
					RemoveHandler pictureBox.Click, value2
				End If
				Me._PictureBox3 = value
				pictureBox = Me._PictureBox3
				If pictureBox IsNot Nothing Then
					AddHandler pictureBox.Click, value2
				End If
			End Set
		End Property

		' Token: 0x1700001D RID: 29
		' (get) Token: 0x06000060 RID: 96 RVA: 0x000051D7 File Offset: 0x000033D7
		' (set) Token: 0x06000061 RID: 97 RVA: 0x000051E4 File Offset: 0x000033E4
		Private Overridable Property label2 As Label
			<CompilerGenerated()>
			Get
				Return Me._label2
			End Get
			<CompilerGenerated()>
			<MethodImpl(MethodImplOptions.Synchronized)>
			Set(value As Label)
				Dim value2 As EventHandler = AddressOf Me.label2_Click
				Dim label As Label = Me._label2
				If label IsNot Nothing Then
					RemoveHandler label.Click, value2
				End If
				Me._label2 = value
				label = Me._label2
				If label IsNot Nothing Then
					AddHandler label.Click, value2
				End If
			End Set
		End Property

		' Token: 0x1700001E RID: 30
		' (get) Token: 0x06000062 RID: 98 RVA: 0x00005227 File Offset: 0x00003427
		' (set) Token: 0x06000063 RID: 99 RVA: 0x00005234 File Offset: 0x00003434
		Private Overridable Property pictureBox1 As PictureBox
			<CompilerGenerated()>
			Get
				Return Me._pictureBox1
			End Get
			<CompilerGenerated()>
			<MethodImpl(MethodImplOptions.Synchronized)>
			Set(value As PictureBox)
				Dim value2 As EventHandler = AddressOf Me.pictureBox1_Click
				Dim pictureBox As PictureBox = Me._pictureBox1
				If pictureBox IsNot Nothing Then
					RemoveHandler pictureBox.Click, value2
				End If
				Me._pictureBox1 = value
				pictureBox = Me._pictureBox1
				If pictureBox IsNot Nothing Then
					AddHandler pictureBox.Click, value2
				End If
			End Set
		End Property

		' Token: 0x0400000B RID: 11
		Private regKey As RegistryKey

		' Token: 0x0400000C RID: 12
		Private Value As Object

		' Token: 0x0400000D RID: 13
		Private keyCustom As Object

		' Token: 0x0400000E RID: 14
		Private ivCustom As Object

		' Token: 0x0400000F RID: 15
		Private blobkey As String

		' Token: 0x04000010 RID: 16
		Private BgEDL As BackgroundWorker

		' Token: 0x04000011 RID: 17
		Private username As String

		' Token: 0x04000012 RID: 18
		Private password As String

		' Token: 0x04000013 RID: 19
		Private suksesgen As Boolean

		' Token: 0x04000014 RID: 20
		Private credit As String

		' Token: 0x04000015 RID: 21
		Private servername As String

		' Token: 0x04000016 RID: 22
		Private manualmode As Object

		' Token: 0x04000017 RID: 23
		Private serveryangon As String()

		' Token: 0x04000018 RID: 24
		Private dataAuth As String

		' Token: 0x04000019 RID: 25
		Private serviceid As String

		' Token: 0x0400001A RID: 26
		Private blobkeyory As String

		' Token: 0x0400001B RID: 27
		Private blobConfig As String

		' Token: 0x0400001C RID: 28
		Private CpuType As Object

		' Token: 0x0400001D RID: 29
		Private CpuCode As Object

		' Token: 0x0400001E RID: 30
		Private succesAuth As Object
	End Class
End Namespace
