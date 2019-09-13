Imports System.Text
Imports System.IO

Public Class Form1

	Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Try
			Dim strInput
			Dim strMode
			Dim strFullFileNameAndPath
			Dim strFileNameOnly

			strInput = Command$()

			If Len(strInput) < 3 Then
				My.Computer.Clipboard.SetText(strInput)
			Else
				'MODE IS HARDCODED, THUS:
				'mode 1: copy the filename
				'mode 2: copy the path
				'mode 3: copy the filename and path

				strMode = strInput.Substring(0, 1)
				'MsgBox(strMode)

				strFullFileNameAndPath = strInput.Substring(2, strInput.Length - 2)
				'MsgBox(strFullFileNameAndPath)

				Select Case strMode

					Case "1"
						strFileNameOnly = My.Computer.FileSystem.GetName(strFullFileNameAndPath)
						'MsgBox(strFileNameOnly)
						My.Computer.Clipboard.SetText(strFileNameOnly)

					Case "2"
						'MsgBox(strParentFolderPath)
						My.Computer.Clipboard.SetText(GetUNCPath(My.Computer.FileSystem.GetParentPath(strFullFileNameAndPath)))

					Case "3"
						My.Computer.Clipboard.SetText(GetUNCPath(strFullFileNameAndPath))

					Case Else
						My.Computer.Clipboard.SetText(strInput)

				End Select
			End If

		Catch ex As Exception
			'MsgBox("Run this with any text string," & vbCrLf & _
			'"and that string will get copied" & vbCrLf & _
			'"directly to your system clipboard." & vbCrLf & _
			'vbCrLf & _
			'"If you are feeling fancy," & vbCrLf & _
			'"You can use a prefix mode." & vbCrLf & _
			'"1 [text]  : copy only a filename" & vbCrLf & _
			'"2 [text]  : copy only a path" & vbCrLf & _
			'"3 [text]  : copy a filename and path")
		End Try



		Close()
	End Sub



	'This code was taken from:
	'http://stackoverflow.com/questions/2765015/get-unc-path-for-mapped-drive-vb-net

	Declare Function WNetGetConnection Lib "mpr.dll" Alias "WNetGetConnectionA" (ByVal lpszLocalName As String, _
		ByVal lpszRemoteName As String, ByRef cbRemoteName As Integer) As Integer

	Private Function GetUNCPath(ByVal sFilePath As String) As String

		Dim d As DriveInfo
		Dim intDriveType, Ctr As Integer
		Dim strDriveLtr, strUNCName As String
		Dim StrBldr As New StringBuilder

		'are we already a UNC path?  then go home.
		If sFilePath.StartsWith("\\") Then Return sFilePath

		'start by defining a big blank string.
		'i confess, i dont know why.
		strUNCName = Space(160)
		GetUNCPath = ""

		'the mapped drive letter is gonna be like c:\
		strDriveLtr = sFilePath.Substring(0, 3)

		'get a listing of all drives
		Dim allDrives() As DriveInfo = DriveInfo.GetDrives()

		'look for the one actual mapped drive info that matches the one we have to use
		For Each d In allDrives
			If d.Name = strDriveLtr Then
				intDriveType = d.DriveType
				Exit For
			End If
		Next

		'drive types are listed here:
		'http://msdn.microsoft.com/en-us/library/system.io.driveinfo.drivetype
		'http://msdn.microsoft.com/en-us/library/system.io.drivetype
		If intDriveType = 4 Then

			'not entirely sure what this does or how.
			'i think it, like, feeds info to mpr.dll, but i don't have any clue what that does.
			Ctr = WNetGetConnection(sFilePath.Substring(0, 2), strUNCName, strUNCName.Length)

			'then we go into a couple loops to get the UNC path.
			'this is the boring part where i stop commenting.
			If Ctr = 0 Then
				strUNCName = strUNCName.Trim
				For Ctr = 0 To strUNCName.Length - 1
					Dim SingleChar As Char = strUNCName(Ctr)
					Dim asciiValue As Integer = Asc(SingleChar)
					If asciiValue > 0 Then
						StrBldr.Append(SingleChar)
					Else
						Exit For
					End If
				Next
				StrBldr.Append(sFilePath.Substring(2))
				GetUNCPath = StrBldr.ToString


				'if we have one of a few different problems, just return the original input.
			Else
				GetUNCPath = sFilePath
			End If
		Else
			GetUNCPath = sFilePath
		End If

	End Function







End Class
