' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

Imports System.Net
Imports Microsoft.Identity.Client
Imports Newtonsoft.Json.Linq
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
            Dim result = Await app.AcquireTokenInteractive(scopes).ExecuteAsync()
            Dim request As HttpWebRequest = CType(WebRequest.Create("https://graph.microsoft.com/v1.0/me"), HttpWebRequest)
            request.Headers.Add(HttpRequestHeader.Authorization, "Bearer " & result.AccessToken)
            Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
            Dim reader As StreamReader = New StreamReader(response.GetResponseStream())
            Dim userInfo As String = reader.ReadToEnd()
            Dim JsonUsuario As JObject = JObject.Parse(userInfo)
            Dim GetUsuario As New UsuarioModel With {
                .Nombre_Usuario = JsonUsuario("givenName"),
                .Apellido_Usuario = JsonUsuario("surname"),
                .CorreoElectronico_Usuario = JsonUsuario("mail")
            }
            TxtNombres.Text = GetUsuario.Nombre_Usuario
            TxtApellidos.Text = GetUsuario.Apellido_Usuario
            TxtCorreoElectronico.Text = GetUsuario.CorreoElectronico_Usuario
        Catch ex As MsalException
            Dim Mensaje = ex.Message
        End Try
    End Function

    Private Async Sub BtnClick_Click(sender As Object, e As RoutedEventArgs)
        Await SignInWithMicrosoftAsync()
    End Sub
End Class
