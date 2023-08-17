Public Class Cl_CrudBrunoSpa
    Public Async Function InsertarModelo(Of T)(Modelo As T) As Task(Of Boolean)
        Try
            Await ConfiguraSqlite()
            Dim Id = Await ConexionDB.InsertAsync(Modelo)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function ConsultarModelo(Of T As {New})() As Task(Of List(Of T))
        Try
            Await ConfiguraSqlite()
            Dim Modelo = Await ConexionDB.Table(Of T)().ToListAsync()
            Return Modelo
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function ActualizarModelo(Of T)(Modelo As T) As Task(Of Boolean)
        Try
            Await ConfiguraSqlite()
            Dim Id = Await ConexionDB.UpdateAsync(Modelo)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class
