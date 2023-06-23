Imports OfficeOpenXml
Imports OfficeOpenXml.Style
Imports OfficeOpenXml.Table
Imports Windows.Perception
Imports Windows.Storage

Public Class Cl_Mascota
    Dim GetCliente As New Cl_Cliente
    Dim GetTipoMascota As New CL_TipoMascota
    Dim GetRazaMascota As New Cl_RazaMascota
    Dim GetPickers As New Cl_Pickers

    Public Async Function InsertarMascota(IdTipoMascota As Integer,
                                          IdRazaMascota As Integer,
                                          NombreMascota As String,
                                          EdadMascota As Integer,
                                          IdPersona As Integer,
                                          ObservacionesMenscota As String) As Task(Of Boolean)
        Try
            Await ConfiguraSqlite()
            Dim Mascota = New MascotaModel With {
                .Id_TipoMascota = IdTipoMascota,
                .Id_Raza = IdRazaMascota,
                .Nombre_Mascota = NombreMascota,
                .Edad_Mascota = EdadMascota,
                .Id_Persona = IdPersona,
                .Observaciones_Mascota = ObservacionesMenscota,
                .FechaRegistro_Mascota = Date.Now
            }
            Dim Id = Await ConexionDB.InsertAsync(Mascota)
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

    Public Async Function CountMascotas() As Task(Of Integer)
        Try
            Await ConfiguraSqlite()
            Dim GetMascota = Await ConexionDB.Table(Of MascotaModel)().CountAsync()
            Return GetMascota
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function CountMascotaUltimoDia() As Task(Of Integer)
        Try
            Await ConfiguraSqlite()
            Dim GetMascota = Await ConexionDB.Table(Of MascotaModel)().ToListAsync()
            Dim CountMascota = (From x In GetMascota
                                Where x.FechaRegistro_Mascota >= Date.Now.AddDays(-1)
                                Select x).Count
            Return CountMascota
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function CountMascotaUltimaSemana() As Task(Of Integer)
        Try
            Await ConfiguraSqlite()
            Dim GetMascota = Await ConexionDB.Table(Of MascotaModel)().ToListAsync()
            Dim CountMascota = (From x In GetMascota
                                Where x.FechaRegistro_Mascota >= Date.Now.AddDays(-7)
                                Select x).Count
            Return CountMascota
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function CountMascotaUltimoMes() As Task(Of Integer)
        Try
            Await ConfiguraSqlite()
            Dim GetMascota = Await ConexionDB.Table(Of MascotaModel)().ToListAsync()
            Dim CountMascota = (From x In GetMascota
                                Where x.FechaRegistro_Mascota >= Date.Now.AddMonths(-1)
                                Select x).Count
            Return CountMascota
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function CrearExcelMascota() As Task(Of Boolean)
        Try
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial
            Dim Propietario = Await GetCliente.ConsultaCliente()
            Dim TipoMascota = Await GetTipoMascota.ConsultarTipoMascota()
            Dim RazaMascota = Await GetRazaMascota.ConsultarRazaMascota
            Dim xlPackage As New ExcelPackage()
            Dim oBook As ExcelWorkbook = xlPackage.Workbook
            Dim ws As ExcelWorksheet = oBook.Worksheets.Add("Mascotas")

            Dim GerMascota As New List(Of MascotaModel)
            Dim ListaMascota = Await ConsultaMascotas()
            Dim ListaFiltrada = (From Mas In ListaMascota
                                 Join Tmc In TipoMascota On
                                       Mas.Id_TipoMascota Equals Tmc.Id_TipoMascota
                                 Join Rza In RazaMascota On
                                       Mas.Id_Raza Equals Rza.Id_Raza
                                 Join Ppo In Propietario On
                                       Mas.Id_Persona Equals Ppo.Id_Persona
                                 Select Tipo = Tmc.Nombre_TipoMascota,
                                    Raza = Rza.Nombre_Raza,
                                    Nombre = Mas.Nombre_Mascota,
                                    Edad = Mas.Edad_Mascota,
                                    Dueño = Ppo.NombreCompleto_Persona,
                                    Observaciones = Mas.Observaciones_Mascota,
                                    Fecha_Creacion = Mas.FechaRegistro_Mascota)
            ws.InsertRow(1, 1)
            ws.Cells("A1:G1").Merge = True
            ws.Cells("A1").Value = "Mascotas Bruno Spa"
            With ws.Cells("A1")
                .Style.Font.Bold = True
                .Style.Font.Size = 22
                .Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
            End With

            ws.Cells("A2").LoadFromCollection(ListaFiltrada, True, TableStyles.Medium1)
            Dim GuardarArchvo = GetPickers.CrearFileSavePicker("ListadoMascotas")

            Dim file = Await GuardarArchvo.PickSaveFileAsync()
            If file IsNot Nothing Then
                CachedFileManager.DeferUpdates(file)
                Using stream = Await file.OpenAsync(FileAccessMode.ReadWrite)
                    Await xlPackage.SaveAsAsync(stream.AsStream())
                End Using
                Dim status = Await CachedFileManager.CompleteUpdatesAsync(file)
                Dim success = Await Windows.System.Launcher.LaunchFileAsync(file)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class
