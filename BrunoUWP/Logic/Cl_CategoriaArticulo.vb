Public Class Cl_CategoriaArticulo
    Public Async Function InsertarCategoriaArticulo(CodigoMaestroArticulo As String,
                                                  NombreMaestroArticulo As String,
                                                  DescripcionMaestroArticulo As String,
                                                  FechaCreacionMaestroArticulo As Date) As Task(Of Boolean)
        Try
            Await ConfiguraSqlite()
            Dim Articulo = New CategoriaArticuloModel With {
                .Codigo_MaestroArticulo = CodigoMaestroArticulo,
                .Nombre_MaestroArticulo = NombreMaestroArticulo,
                .Descripcion_MaestroArticulo = DescripcionMaestroArticulo,
                .FechaCreacion_MaestroArticulo = FechaCreacionMaestroArticulo
            }
            Dim Id = Await ConexionDB.InsertAsync(Articulo)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function ConsultaCategoriaArticulo() As Task(Of List(Of CategoriaArticuloModel))
        Try
            Await ConfiguraSqlite()
            Dim GetMaestroArticulo = Await ConexionDB.Table(Of CategoriaArticuloModel)().ToListAsync()
            Dim ListaMaestroArticulo = (From x In GetMaestroArticulo
                                        Select x).ToList
            Return ListaMaestroArticulo
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function ActualizarCategoriaArticulo(IdMaestroArticulo As Integer,
                                                   NombreMaestroArticulo As String,
                                                   DescripcionMaestroArticulo As String) As Task(Of Boolean)
        Try
            Await ConfiguraSqlite()
            Dim GetMaestroArticulo = Await ConexionDB.Table(Of CategoriaArticuloModel)().ToListAsync()
            Dim MaestroArticulo = (From x In GetMaestroArticulo
                                   Where x.Id_MaestroArticulo = IdMaestroArticulo
                                   Select x).FirstOrDefault
            If MaestroArticulo IsNot Nothing Then
                MaestroArticulo.Nombre_MaestroArticulo = NombreMaestroArticulo
                MaestroArticulo.Descripcion_MaestroArticulo = DescripcionMaestroArticulo
                Await ConexionDB.UpdateAsync(MaestroArticulo)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class
