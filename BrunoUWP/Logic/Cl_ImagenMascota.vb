Public Class Cl_ImagenMascota

    Public Async Function InsertImagenMascota(DescripcionImagen As String,
                                              UrlImagen As String,
                                              IdMascota As Integer) As Task(Of Boolean)
        Try
            Await ConfiguraSqlite()
            Dim ImagenMascota = New ImagenMascotaModel With {
                .Descripcion_ImagenMascotaModel = DescripcionImagen,
                .Url_ImagenMascotaModel = UrlImagen,
                .Id_Mascota = IdMascota
            }
            Dim Id = Await ConexionDB.InsertAsync(ImagenMascota)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function ConsultaImagenMascota(IdMascota As Integer) As Task(Of List(Of ImagenMascotaModel))
        Try
            Await ConfiguraSqlite()
            Dim GetImagenMascota = Await ConexionDB.Table(Of ImagenMascotaModel)().ToListAsync()
            Dim ListaImagenMascota = (From x In GetImagenMascota
                                      Where x.Id_Mascota = IdMascota
                                      Select x).ToList
            Return ListaImagenMascota
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class
