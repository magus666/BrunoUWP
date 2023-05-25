Imports Windows.Perception

Public Class Cl_Mascota

    Public Async Function InsertarMascota(TipoMascota As Integer,
                                          RazaMascota As Integer,
                                          NombreMascota As String,
                                          EdadMascota As Integer,
                                          PropietarioMascota As Integer,
                                          ObservacionesMenscota As String) As Task(Of Boolean)
        Try
            Await ConfiguraSqlite()
            Dim Cliente = New MascotaModel With {
                .Tipo_Mascota = TipoMascota,
                .Raza_Mascota = RazaMascota,
                .Nombre_Mascota = NombreMascota,
                .Edad_Mascota = EdadMascota,
                .Propietario_Mascota = PropietarioMascota,
                .Observaciones_Mascota = ObservacionesMenscota
            }
            Dim Id = Await ConexionDB.InsertAsync(Cliente)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function ConsultaMascotas() As Task(Of List(Of MascotaModel))
        Try
            Await ConfiguraSqlite()
            Dim GetMascota = Await ConexionDB.Table(Of MascotaModel)().ToListAsync()
            Dim ListaMascota = (From x In GetMascota
                                Select x).ToList
            Return ListaMascota
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class
