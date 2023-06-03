Public Class Cl_Sexo

    Public Async Function InsertaActualizaSexo() As Task(Of Boolean)
        Try
            Dim GetSexo = Await ConexionDB.Table(Of SexoModel)().ToListAsync()
            Dim MasculinoExiste = (From x In GetSexo
                                   Where x.Nombre_Sexo = "Masculino"
                                   Select x).Count
            Dim FemeninoExiste = (From x In GetSexo
                                  Where x.Nombre_Sexo = "Femenino"
                                  Select x).Count

            ' Verificar si ya existen los registros de género "masculino" y "femenino"
            'Dim MasculinoExistente = Await ConexionDB.Table(Of SexoModel)().Where(Function(x) x.Nombre_Sexo = "masculino").FirstOrDefaultAsync()
            'Dim FemeninoExistente = Await ConexionDB.Table(Of SexoModel)().Where(Function(x) x.Nombre_Sexo = "femenino").FirstOrDefaultAsync()

            ' Si no existe el registro "masculino", crearlo
            If MasculinoExiste = 0 Then
                Dim Masculino = New SexoModel With {
                    .Nombre_Sexo = "Masculino"
                }
                Dim Id = Await ConexionDB.InsertAsync(Masculino)
            End If

            ' Si no existe el registro "femenino", crearlo
            If FemeninoExiste = 0 Then
                Dim Femenino = New SexoModel With {
                    .Nombre_Sexo = "Femenino"
                }
                Dim Id = Await ConexionDB.InsertAsync(Femenino)
            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function ConsultaSexo() As Task(Of List(Of SexoModel))
        Try
            Await ConfiguraSqlite()
            Dim GetSexo = Await ConexionDB.Table(Of SexoModel)().ToListAsync()
            Dim ListaSexo = (From x In GetSexo
                             Select x).ToList
            Return ListaSexo
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class
