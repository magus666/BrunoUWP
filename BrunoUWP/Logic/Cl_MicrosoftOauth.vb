Imports Microsoft.Identity.Client

Public Class Cl_MicrosoftOauth
    Private Const ClientId As String = "37785fff-78a8-4d39-84a6-2431e9b15773"
    Private Const TenantId As String = "0e4200c3-2ad7-48f2-adfa-27c70e734cc1"
    Private Const RedirectUri As String = "http://localhost"

    Public Async Function GetUserDetailsAsync() As Task(Of (userEmail As String, userName As String))
        Dim publicClientApp = PublicClientApplicationBuilder.Create(ClientId) _
            .WithAuthority(AzureCloudInstance.AzurePublic, TenantId) _
            .WithRedirectUri(RedirectUri) _
            .Build()
        Dim scopes = New String() {"user.read"}

        Try
            Dim result = Await publicClientApp.AcquireTokenInteractive(scopes).ExecuteAsync()
            Dim userName = result.Account.Username
            Dim userEmail = $"{result.Account.Username}@miempresa.com"

            Return (userEmail, userName)
        Catch ex As MsalException
            ' Maneja excepciones específicas aquí (por ejemplo, registra, muestra mensaje de error, etc.)
            Throw New Exception($"Error de autenticación ({ex.ErrorCode}): {ex.Message}")
        End Try
    End Function

End Class
