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

    Public Async Function ActualizarMascota(IdMascota As Integer,
                                            NombreMascota As String,
                                            EdadMascota As Integer,
                                            EstadoMascota As Boolean,
                                            ObservacionesMascota As String) As Task(Of Boolean)
        Try
            Await ConfiguraSqlite()
            Dim GetMascota = Await ConexionDB.Table(Of MascotaModel)().ToListAsync()
            Dim Mascota = (From x In GetMascota
                           Where x.Id_Mascota = IdMascota
                           Select x).FirstOrDefault
            If Mascota IsNot Nothing Then
                Mascota.Nombre_Mascota = NombreMascota
                Mascota.Edad_Mascota = EdadMascota
                Mascota.Observaciones_Mascota = ObservacionesMascota
                Mascota.Estado_Mascota = EstadoMascota
                Await ConexionDB.UpdateAsync(Mascota)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


End Class
