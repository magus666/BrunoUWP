' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

Imports System.Security
Imports System.Security.Principal
''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_Login
    Inherits Page
    Public Gonorrita As New MicrosoftPassportHelper
    Dim _account As New Usuario
    Dim Timer As DispatcherTimer = New DispatcherTimer

    Protected Overrides Async Sub OnNavigatedTo(ByVal e As NavigationEventArgs)
        Try

            If Await Gonorrita.MicrosoftPassportAvailableCheckAsync() Then
                Dim RespuestaLogueo = Await SignInPassport()
                If RespuestaLogueo = "Clave Correcta" Then
                    LblMensaje.Visibility = Visibility.Visible
                    TimerLogin()
                    Exit Sub
                End If

                If RespuestaLogueo = "Cancelo el Logueo" Then
                    Application.Current.Exit()
                    Exit Sub
                End If
                If RespuestaLogueo = "Usuario No Valido" Then
                    LblMensaje.Visibility = Visibility.Visible
                    LblMensaje.Text = "Usuario No Registrado"
                    Exit Sub
                End If

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub TimerLogin()
        Try
            Timer.Interval = New TimeSpan(0, 0, 2)
            Timer.Start()
            AddHandler Timer.Tick, AddressOf Timer_Tick
        Catch ex As Exception
        End Try
    End Sub

    Private Function Timer_Tick(sender As Object, e As Object)
        Try
            Timer.Stop()
            Frame.Navigate(GetType(MainPage))
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Private Async Function SignInPassport() As Task(Of String)
        If AccountHelper.ValidateAccountCredentials("sampleUsername") Then
            ' Create and add a new local account
            _account = AccountHelper.AddAccount("sampleUsername")
            Debug.WriteLine("Successfully signed in with traditional credentials and created local account instance!")
            If Await MicrosoftPassportHelper.CreatePassportKeyAsync("sampleUsername") Then
                Return "Clave Correcta"
            Else
                Return "Cancelo el Logueo"
            End If
        Else
            Return "Usuario No Valido"
        End If
    End Function
End Class
