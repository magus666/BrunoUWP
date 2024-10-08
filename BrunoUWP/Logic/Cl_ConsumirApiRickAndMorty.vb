﻿Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Text
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class Cl_ConsumirApiRickAndMorty

    Private ReadOnly client As New HttpClient()

    Public Async Function GetDataRickAndMorty() As Task(Of List(Of RickAndMortyModel))
        Dim response = Await client.GetAsync("https://rickandmortyapi.com/api/character")
        If response.IsSuccessStatusCode Then
            Dim content = Await response.Content.ReadAsStringAsync()
            Dim json = JObject.Parse(content)
            Dim results = json("results")
            Dim characters As New List(Of RickAndMortyModel)
            For Each Resultado In results
                Dim character As New RickAndMortyModel With {
                        .Nombre_Personaje = Resultado("name").ToString(),
                        .Imagen_Personaje = Resultado("image").ToString(),
                        .origin = New RickAndMortyModel.original With {
                            .name = Resultado("origin")("name").ToString(),
                            .image = Resultado("origin")("image").ToString()
                        }
                }
                characters.Add(character)
            Next
            Return characters
        Else
            Throw New Exception(response.StatusCode)
        End If
    End Function

    'Funcion obtimizada para obtener a rick and morty
    'Public Async Function GetRickAndMorty() As Task(Of RickAndMortyModel)
    '    Dim Url As String = "https://rickandmortyapi.com/api/character"
    '    Dim httpClient As New HttpClient()
    '    Dim response As HttpResponseMessage = Await httpClient.GetAsync(Url)
    '    Dim content = Await response.Content.ReadAsStringAsync()
    '    Dim rickAndMorty = JsonSerializer.Deserialize(Of RickAndMortyModel)(content)
    '    Return rickAndMorty
    'End Function

    Public Async Function GetClienteAsync(Usuario As String, Clave As String) As Task(Of String)
        Try
            Dim NombreCliente As String = ""
            Dim User As String = Usuario
            Dim Pass As String = Clave
            Dim Cliente = IniciarCliente(User, Pass)

            ' Realizar una solicitud GET al recurso deseado
            Dim response As HttpResponseMessage = Await Cliente.GetAsync("api/Cliente")

            ' Procesar la respuesta
            If response.IsSuccessStatusCode Then
                Dim content As String = Await response.Content.ReadAsStringAsync()
                Dim json = JObject.Parse(content)
                Dim results = json("clientes")
                For Each Resultado In results
                    NombreCliente = Resultado("Nombre_Cliente").ToString()
                Next
                Return NombreCliente
            Else
                Dim Respuesta = response.StatusCode
                Return Respuesta
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function AddClienteAsync(Usuario As String, Clave As String, nuevoCliente As ClienteModel) As Task(Of HttpResponseMessage)
        Dim url As String = "https://apiirestbruno.azurewebsites.net/api/Cliente"
        Dim json As String = JsonConvert.SerializeObject(nuevoCliente)
        Dim content As New StringContent(json, Encoding.UTF8, "application/json")

        Using client As New HttpClient()
            Dim NombreCliente As String = ""
            Dim User As String = Usuario
            Dim Pass As String = Clave
            Dim Cliente = IniciarCliente(User, Pass)

            ' Realizar una solicitud GET al recurso deseado
            Dim response As HttpResponseMessage = Await Cliente.PostAsync(url, content)
            Return response
        End Using
    End Function

    Public Function IniciarCliente(Usuario As String, Clave As String) As HttpClient
        ' Crear una instancia de HttpClient y establecer la dirección base de la API
        Dim client As New HttpClient With {
            .BaseAddress = New Uri("https://apiirestbruno.azurewebsites.net/")
        }

        ' Agregar las credenciales de autenticación básica a la cabecera Authorization del cliente
        Dim authenticationString As String = $"{Usuario}:{Clave}"
        Dim base64EncodedAuthenticationString As String = Convert.ToBase64String(Encoding.ASCII.GetBytes(authenticationString))
        client.DefaultRequestHeaders.Authorization = New AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString)
        Return client
    End Function

    'Otra Forma de consumir Api
    Private ReadOnly _httpClient As HttpClient
    Public Sub New()
        _httpClient = New HttpClient()
    End Sub

    Public Async Function GetCharacterAsync(characterId As Integer) As Task(Of RickAndMortyModel)
        Dim apiUrl As String = $"https://rickandmortyapi.com/api/character/{characterId}"
        Dim response = Await _httpClient.GetAsync(apiUrl)

        If response.IsSuccessStatusCode Then
            Dim content = Await response.Content.ReadAsStringAsync()
            Dim Jason = JsonConvert.DeserializeObject(Of RickAndMortyModel)(content)
            Return Jason
        End If

        Return Nothing ' Maneja errores según tus necesidades
    End Function
End Class
