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

    Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)

    End Sub

    Public Async Function SignInWithMicrosoftAsync() As Task(Of String)
        Dim clientId As String = "e25cb291-29f9-4841-8bae-a9269cfad602" ' Reemplaza con tu Client ID
        Dim scopes() As String = {"User.Read"} ' Los permisos que necesitas

        Dim app = PublicClientApplicationBuilder.Create(clientId) _
            .WithRedirectUri("msale25cb291-29f9-4841-8bae-a9269cfad602://auth").Build()

        Dim accounts = Await app.GetAccountsAsync()

        Try
            Dim result = Await app.AcquireTokenInteractive(scopes).WithUseEmbeddedWebView(True).WithAccount(accounts.FirstOrDefault()).ExecuteAsync()

            Return result.AccessToken
        Catch ex As MsalException
            ' Maneja errores de inicio de sesión
            Return Nothing
        End Try
    End Function

    Private Async Sub BtnClick_Click(sender As Object, e As RoutedEventArgs)
        Await SignInWithMicrosoftAsync()
    End Sub
End Class
