Imports System.Net.Http
Imports System.Text.Json

Public Class Lenovotote
    Public Async Function GetRickAndMorty() As Task(Of RickMelo)
        Dim Url As String = "https://rickandmortyapi.com/api/character"
        Dim httpClient As New HttpClient()
        Dim response As HttpResponseMessage = Await httpClient.GetAsync(Url)
        Dim content = Await response.Content.ReadAsStringAsync()
        Dim rickAndMorty = JsonSerializer.Deserialize(Of RickMelo)(content)
        Return rickAndMorty
    End Function
End Class
