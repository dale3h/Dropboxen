Imports System.Diagnostics
Imports System.Security
Imports System.Xml
Imports System.Collections.Specialized

Public Module Dropboxen
    Public Accounts As New NameValueCollection

    Public Const CONFIG_FILE = "Dropboxen.xml"

    Sub Main()
        'If Not SupportedOS() Then
        'If MsgBox("You appear to be running a Windows version (" & GetOS() & ") that is not supported at this time." & vbCrLf & vbCrLf & _
        '          "To continue, please press OK. To exit, please press Cancel", MsgBoxStyle.Critical Or MsgBoxStyle.OkCancel, "Unsupported Windows Version") = MsgBoxResult.Cancel Then
        'End
        'End If
        'End If

        Main(Environment.GetCommandLineArgs())
    End Sub

    Private Sub Main(ByVal args() As String)
        Call LoadConfig()

        Try
            For Each acctItem In Accounts
                Call LaunchDropbox(acctItem, Accounts(acctItem))
            Next
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Dropboxen Error")
        End Try
    End Sub

    Public Sub LoadConfig()
        Try
            Dim xDoc As New XmlDocument()

            xDoc.Load(CONFIG_FILE)

            Dim acctNodes As XmlNodeList = xDoc.GetElementsByTagName("account")
            For Each acctNode As XmlNode In acctNodes
                Accounts(acctNode.Attributes("username").InnerText) = acctNode.Attributes("password").InnerText
            Next
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Dropboxen Error")
        End Try
    End Sub

    Public Function LaunchDropbox(ByVal strUsername As String, ByVal strPassword As String) As Boolean
        Try
            Dim startInfo As New ProcessStartInfo

            startInfo.EnvironmentVariables("APPDATA") = Environment.GetEnvironmentVariable("APPDATA").Replace(Environment.UserName, strUsername)
            startInfo.EnvironmentVariables("HOMEPATH") = Environment.GetEnvironmentVariable("HOMEPATH").Replace(Environment.UserName, strUsername)
            startInfo.EnvironmentVariables("USERNAME") = Environment.GetEnvironmentVariable("USERNAME").Replace(Environment.UserName, strUsername)
            startInfo.EnvironmentVariables("USERPROFILE") = Environment.GetEnvironmentVariable("USERPROFILE").Replace(Environment.UserName, strUsername)

            startInfo.FileName = "Dropbox.exe"
            startInfo.UserName = strUsername
            startInfo.Password = ConvertToSecureString(strPassword)
            startInfo.UseShellExecute = False

            Process.Start(startInfo)
            Return True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Dropboxen Error")
            Return False
        End Try
    End Function

    Function ConvertToSecureString(ByVal strString As String)
        Dim strSecure As New SecureString
        For Each c As Char In strString.ToCharArray
            strSecure.AppendChar(c)
        Next
        Return strSecure
    End Function

    Public Function SupportedOS() As Boolean
        Dim strOS As String = GetOS()
        Return strOS = "XP" Or strOS = "VISTA"
    End Function

    Public Function GetOS() As String
        Dim osInfo As OperatingSystem = System.Environment.OSVersion

        Select Case osInfo.Platform
            Case PlatformID.Win32Windows
                Select Case (osInfo.Version.Minor)
                    Case 0
                        Return "95"
                    Case 10
                        If osInfo.Version.Revision.ToString() = "2222A" Then
                            Return "98SE"
                        Else
                            Return "98"
                        End If
                    Case 90
                        Return "ME"
                End Select
            Case PlatformID.Win32NT
                Select Case (osInfo.Version.Major)
                    Case 3
                        Return "NT3.51"
                    Case 4
                        Return "NT4.0"
                    Case 5
                        If osInfo.Version.Minor = 0 Then
                            Return "2000"
                        ElseIf osInfo.Version.Minor = 1 Then
                            Return "XP"
                        ElseIf osInfo.Version.Minor = 2 Then
                            Return "Server 2003"
                        End If
                    Case 6
                        Return "VISTA"
                End Select
        End Select

        Return "Unknown (" & osInfo.Version.ToString() & ")"
    End Function
End Module
