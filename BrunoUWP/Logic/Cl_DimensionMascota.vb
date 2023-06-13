Public Class Cl_DimensionMascota

    Public Async Function InsertarActualizarDimensionMascota() As Task(Of Boolean)
        Try
            Dim GetDimensionMascota = Await ConexionDB.Table(Of DimensionMascotaModel)().ToListAsync()
            Dim Pequenio = (From x In GetDimensionMascota
                            Where x.Nombre_DimensionMascota = "Pequeño"
                            Select x).Count
            Dim Mediano = (From x In GetDimensionMascota
                           Where x.Nombre_DimensionMascota = "Mediano"
                           Select x).Count
            Dim Grande = (From x In GetDimensionMascota
                          Where x.Nombre_DimensionMascota = "Grande"
                          Select x).Count
            If Pequenio = 0 Then
                Dim DimPequenio = New DimensionMascotaModel With {
                    .Nombre_DimensionMascota = "Pequeño"
                }
                Dim Id = Await ConexionDB.InsertAsync(DimPequenio)
            End If
            If Mediano = 0 Then
                Dim DimMediano = New DimensionMascotaModel With {
                    .Nombre_DimensionMascota = "Mediano"
                }
                Dim Id = Await ConexionDB.InsertAsync(DimMediano)
            End If
            If Grande = 0 Then
                Dim DimGrande = New DimensionMascotaModel With {
                    .Nombre_DimensionMascota = "Grande"
                }
                Dim Id = Await ConexionDB.InsertAsync(DimGrande)
            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function ConsultaDimensionMascota() As Task(Of List(Of DimensionMascotaModel))
        Try
            Await ConfiguraSqlite()
            Dim GetDimensionMascota = Await ConexionDB.Table(Of DimensionMascotaModel)().ToListAsync()
            Dim ListaDimensionMascota = (From x In GetDimensionMascota
                                         Select x).ToList
            Return ListaDimensionMascota
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class
