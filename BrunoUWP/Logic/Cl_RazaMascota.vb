Public Class Cl_RazaMascota
    Public Async Function InsertarRaza(NombreRaza As String,
                                       DescripcionRaza As String,
                                       IdTipoMascota As Integer) As Task(Of Boolean)
        Try
            Await ConfiguraSqlite()
            Dim RazaMascota = New RazaModel With {
                .Nombre_Raza = NombreRaza,
                .Descripcion_Raza = DescripcionRaza,
                .Id_TipoMascota = IdTipoMascota
            }
            Dim Id = Await ConexionDB.InsertAsync(RazaMascota)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function ConsultarRazaMascota() As Task(Of List(Of RazaModel))
        Try
            Await ConfiguraSqlite()
            Dim GetRaza = Await ConexionDB.Table(Of RazaModel)().ToListAsync()
            Dim ListaRaza = (From x In GetRaza
                             Select x).ToList
            Return ListaRaza
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function ConsultaRazaMascotaId(IdTipoMascota As Integer) As Task(Of List(Of RazaModel))
        Try
            Await ConfiguraSqlite()
            Dim GetRazaId = Await ConexionDB.Table(Of RazaModel)().ToListAsync()
            Dim ListaRazaId = (From x In GetRazaId
                               Where x.Id_TipoMascota = IdTipoMascota
                               Select x).ToList
            Return ListaRazaId
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class
