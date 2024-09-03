Public Class RickMelo
    ' Root myDeserializedClass = JsonConvert.DeserializeObject(Of Root)(myJsonResponse)
    Public Class Info
        Public Property count As Integer
        Public Property pages As Integer
        Public Property prev As Object
    End Class

    Public Class Location
        Public Property name As String
        Public Property url As String
    End Class

    Public Class Origin
        Public Property name As String
        Public Property url As String
    End Class

    Public Class Result
        Public Property id As Integer
        Public Property name As String
        Public Property status As String
        Public Property species As String
        Public Property type As String
        Public Property gender As String
        Public Property origin As Origin
        Public Property location As Location
        Public Property image As String
        Public Property episode As List(Of String)
        Public Property url As String
        Public Property created As DateTime
    End Class

    Public Class Root
        Public Property info As Info
        Public Property results As List(Of Result)
    End Class

End Class
