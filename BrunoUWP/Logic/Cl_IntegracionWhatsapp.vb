Imports System.Net
Imports System.Text
Imports RestSharp

Public Class Cl_IntegracionWhatsapp


    Public Async Function EnviaMensajeWhatsapp(NumeroDestino As String, MensajeWhatsapp As String) As Task
        Try
            Dim IndicativoColombia As String = "+57"
            Dim url As String = "https://api.ultramsg.com/instance50025/messages/chat"
            Dim client As New RestClient(url)
            Dim request As New RestRequest(url, Method.Post)
            request.AddHeader("content-type", "application/x-www-form-urlencoded")
            request.AddParameter("token", "o9ozgxpn8klbvvh1")
            request.AddParameter("to", IndicativoColombia & NumeroDestino)
            request.AddParameter("body", MensajeWhatsapp)
            Dim response As RestResponse = Await client.ExecuteAsync(request)
            Dim output As String = response.Content
            Console.WriteLine(output)
        Catch ex As Exception

        End Try

    End Function

End Class
