' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

Imports Microsoft.Graph
Imports Microsoft.Identity.Client
Imports Windows.System
Imports Windows.UI.Composition
''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_MicrosoftLogin
    Inherits Page
    Private Shared ClientId As String = "edcddeac-b608-4c15-9ebd-c64101845274"
    Private Shared RedirectUri As String = "https://login.microsoftonline.com/common/oauth2/nativeclient"

    Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)

    End Sub

    Public Async Function SignInWithMicrosoftAsync() As Task(Of String)
        Try
            Dim app = PublicClientApplicationBuilder.Create(ClientId) _
            .WithRedirectUri(RedirectUri) _
            .Build()
            Dim scopes = New String() {"User.Read"}

            Dim result = Await app.AcquireTokenInteractive(scopes) _
                .ExecuteAsync()
        Catch ex As MsalException
            Dim Mensaje = ex.Message
        End Try
    End Function

    Private Async Sub BtnClick_Click(sender As Object, e As RoutedEventArgs)
        Await SignInWithMicrosoftAsync()
    End Sub
End Class
