Public Class Cl_TipoServicio

    Public Async Function InsertarActualizarTipoServicio() As Task(Of Boolean)
        Try
            Dim GetTipoServicio = Await ConexionDB.Table(Of TipoServicioModel)().ToListAsync()
            Dim CorteUnas = (From x In GetTipoServicio
                             Where x.Nombre_TipoServicio = "Corte Uñas"
                             Select x).Count
            Dim Bano = (From x In GetTipoServicio
                        Where x.Nombre_TipoServicio = "Baño"
                        Select x).Count
            Dim ServicioCompleto = (From x In GetTipoServicio
                                    Where x.Nombre_TipoServicio = "Servicio Completo"
                                    Select x).Count
            Dim BanoMedicado = (From x In GetTipoServicio
                                Where x.Nombre_TipoServicio = "Baño Medicado"
                                Select x).Count
            If CorteUnas = 0 Then
                Dim DimPequenio = New TipoServicioModel With {
                    .Nombre_TipoServicio = "Corte Uñas"
                }
                Dim Id = Await ConexionDB.InsertAsync(DimPequenio)
            End If
            If Bano = 0 Then
                Dim DimMediano = New TipoServicioModel With {
                    .Nombre_TipoServicio = "Baño"
                }
                Dim Id = Await ConexionDB.InsertAsync(DimMediano)
            End If
            If ServicioCompleto = 0 Then
                Dim DimGrande = New TipoServicioModel With {
                    .Nombre_TipoServicio = "Servicio Completo"
                }
                Dim Id = Await ConexionDB.InsertAsync(DimGrande)
            End If
            If BanoMedicado = 0 Then
                Dim DimGrande = New TipoServicioModel With {
                    .Nombre_TipoServicio = "Baño Medicado"
                }
                Dim Id = Await ConexionDB.InsertAsync(DimGrande)
            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function




    Public Async Function ConsultaTipoServicio() As Task(Of List(Of TipoServicioModel))
        Try
            Await ConfiguraSqlite()
            Dim GetTipoServicio = Await ConexionDB.Table(Of TipoServicioModel)().ToListAsync()
            Dim ListaTipoServicio = (From x In GetTipoServicio
                                     Select x).ToList
            Return ListaTipoServicio
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class
