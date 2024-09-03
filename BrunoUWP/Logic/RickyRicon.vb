Imports System.Net.Http
Imports System.Text.Json

Public Class RickAndMortyResponse
    Public Property results As List(Of RickAndMortyModel.original)
End Class

Public Class RickyRicon
    Public Async Function GetRickAndMorty() As Task(Of List(Of RickAndMortyModel.original))
        Dim Url As String = "https://rickandmortyapi.com/api/character"
        Dim httpClient As New HttpClient()

        Try
            Dim response As HttpResponseMessage = Await httpClient.GetAsync(Url)

            ' Validar el código de estado de la respuesta
            If response.IsSuccessStatusCode Then
                Dim content = Await response.Content.ReadAsStringAsync()
                Dim rickAndMortyResponse = JsonSerializer.Deserialize(Of RickAndMortyResponse)(content)
                Return rickAndMortyResponse.results
            Else
                ' Manejar códigos de estado no exitosos
                Throw New HttpRequestException($"Error en la solicitud HTTP: {response.StatusCode}")
            End If

        Catch ex As HttpRequestException
            ' Manejar excepciones específicas de solicitudes HTTP
            Throw New Exception($"Error al obtener datos de la API: {ex.Message}")
        Catch ex As Exception
            ' Manejar otras excepciones
            Throw New Exception($"Ocurrió un error: {ex.Message}")
        End Try
    End Function

End Class
