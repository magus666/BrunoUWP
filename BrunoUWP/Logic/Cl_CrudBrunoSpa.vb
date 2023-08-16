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

    Public Async Function ActualizarModelo(Of T As {New, Imodelo})(Id As Integer) As Task(Of Boolean)
        Try
            Await ConfiguraSqlite()
            Dim GetModelo = Await ConexionDB.Table(Of T)().ToListAsync()
            Dim Modelo = (From x In GetModelo
                          Where x.Id = Id
                          Select x).FirstOrDefault()
            If Modelo IsNot Nothing Then
                Await ConexionDB.UpdateAsync(Modelo)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class
