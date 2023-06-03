Public Class CL_TipoMascota
    Public Async Function InsertarActualizarMascota() As Task(Of Boolean)
        Try
            Dim GetTipoMascota = Await ConexionDB.Table(Of TipoMascotaModel)().ToListAsync()
            Dim PerroExiste = (From x In GetTipoMascota
                               Where x.Nombre_TipoMascota = "Perro"
                               Select x).Count
            Dim GatoExiste = (From x In GetTipoMascota
                              Where x.Nombre_TipoMascota = "Gato"
                              Select x).Count
            Dim ConejoExiste = (From x In GetTipoMascota
                                Where x.Nombre_TipoMascota = "Conejo"
                                Select x).Count
            If PerroExiste = 0 Then
                Dim Masculino = New TipoMascotaModel With {
                    .Nombre_TipoMascota = "Perro"
                }
                Dim Id = Await ConexionDB.InsertAsync(Masculino)
            End If
            If GatoExiste = 0 Then
                Dim Masculino = New TipoMascotaModel With {
                    .Nombre_TipoMascota = "Gato"
                }
                Dim Id = Await ConexionDB.InsertAsync(Masculino)
            End If
            If ConejoExiste = 0 Then
                Dim Femenino = New TipoMascotaModel With {
                    .Nombre_TipoMascota = "Conejo"
                }
                Dim Id = Await ConexionDB.InsertAsync(Femenino)
            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function ConsultarTipoMascota() As Task(Of List(Of TipoMascotaModel))
        Try
            Await ConfiguraSqlite()
            Dim GetTipoMascota = Await ConexionDB.Table(Of TipoMascotaModel)().ToListAsync()
            Dim ListaTipoMascota = (From x In GetTipoMascota
                                    Select x).ToList
            Return ListaTipoMascota
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class
