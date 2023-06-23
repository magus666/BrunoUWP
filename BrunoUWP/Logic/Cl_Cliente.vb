Imports System.Drawing
Imports System.Reflection
Imports OfficeOpenXml
Imports OfficeOpenXml.Style
Imports OfficeOpenXml.Table
Imports Windows.Storage
Imports Windows.Storage.Pickers

Public Class Cl_Cliente
    Dim GetPickers As New Cl_Pickers

    Public Async Function InsertarCliente(DocumentoCliente As String,
                                          NombreCliente As String,
                                          ApellidoCliente As String,
                                          DireccionCliente As String,
                                          TelefonoCliente As String,
                                          CorreoCliente As String,
                                          EdadCliente As Integer,
                                          SexoCliente As Integer,
                                          CodigoCliente As String,
                                          EstadoCliente As Boolean) As Task(Of Boolean)
        Try
            Await ConfiguraSqlite()
            Dim Cliente = New ClienteModel With {
                .Documento_Persona = DocumentoCliente,
                .Nombre_Persona = NombreCliente,
                .Apellido_Persona = ApellidoCliente,
                .NombreCompleto_Persona = NombreCliente & " " & ApellidoCliente,
                .Direccion_Persona = DireccionCliente,
                .Telefono_Persona = TelefonoCliente,
                .Correo_Persona = CorreoCliente,
                .Edad_Persona = EdadCliente,
                .Id_Sexo = SexoCliente,
                .Codigo_Cliente = CodigoCliente,
                .Estado_Cliente = EstadoCliente,
                .FechaCreacion_Persona = Date.Now
            }
            Dim Id = Await ConexionDB.InsertAsync(Cliente)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function ActualizarCliente(IdCliente As Integer,
                                            DireccionCLiente As String,
                                            TelefonoCliente As String,
                                            CorreoCliente As String,
                                            EstadoCliente As Boolean) As Task(Of Boolean)
        Try
            Await ConfiguraSqlite()
            Dim GetCliente = Await ConexionDB.Table(Of ClienteModel)().ToListAsync()
            Dim Cliente = (From x In GetCliente
                           Where x.Id_Persona = IdCliente
                           Select x).FirstOrDefault
            If Cliente IsNot Nothing Then
                Cliente.Direccion_Persona = DireccionCLiente
                Cliente.Telefono_Persona = TelefonoCliente
                Cliente.Correo_Persona = CorreoCliente
                Cliente.Estado_Cliente = EstadoCliente
                Cliente.NombreCompleto_Persona = Cliente.Nombre_Persona & " " & Cliente.Apellido_Persona
                Await ConexionDB.UpdateAsync(Cliente)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function ÉliminarCliente(IdCliente As Integer) As Task(Of Boolean)
        Try
            Await ConfiguraSqlite()
            Dim GetCliente = Await ConexionDB.Table(Of ClienteModel)().ToListAsync()
            Dim Cliente = (From x In GetCliente
                           Where x.Id_Persona = IdCliente
                           Select x).FirstOrDefault
            If Cliente IsNot Nothing Then
                Await ConexionDB.DeleteAsync(Cliente)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function ConsultaCliente() As Task(Of List(Of ClienteModel))
        Try
            Await ConfiguraSqlite()
            Dim GetCliente = Await ConexionDB.Table(Of ClienteModel)().ToListAsync()
            Dim ListaPersona = (From x In GetCliente
                                Select x).ToList()
            Return ListaPersona
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function CountClienteTotal() As Task(Of Integer)
        Try
            Await ConfiguraSqlite()
            Dim GetCliente = Await ConexionDB.Table(Of ClienteModel)().CountAsync()
            Return GetCliente
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function CountClienteUltimoDia() As Task(Of Integer)
        Try
            Await ConfiguraSqlite()
            Dim GetCliente = Await ConexionDB.Table(Of ClienteModel)().ToListAsync()
            Dim CoutCliente = (From x In GetCliente
                               Where x.FechaCreacion_Persona >= Date.Now.AddDays(-1)
                               Select x).Count
            Return CoutCliente
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function CountClienteUltimaSemana() As Task(Of Integer)
        Try
            Await ConfiguraSqlite()
            Dim GetCliente = Await ConexionDB.Table(Of ClienteModel)().ToListAsync()
            Dim CoutCliente = (From x In GetCliente
                               Where x.FechaCreacion_Persona >= Date.Now.AddDays(-7)
                               Select x).Count
            Return CoutCliente
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function CountClienteUltimoMes() As Task(Of Integer)
        Try
            Await ConfiguraSqlite()
            Dim GetCliente = Await ConexionDB.Table(Of ClienteModel)().ToListAsync()
            Dim CoutCliente = (From x In GetCliente
                               Where x.FechaCreacion_Persona >= Date.Now.AddMonths(-1)
                               Select x).Count
            Return CoutCliente
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function CreaExcelCliente() As Task(Of Boolean)
        Try
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial
            Dim xlPackage As New ExcelPackage()
            Dim oBook As ExcelWorkbook = xlPackage.Workbook
            Dim ws As ExcelWorksheet = oBook.Worksheets.Add("Clients")

            Dim GetCLiente As New List(Of ClienteModel)
            Dim ListaCliente = Await ConsultaCliente()
            Dim ListaFiltrada = (From x In ListaCliente
                                 Order By x.NombreCompleto_Persona
                                 Select x.Codigo_Cliente,
                                     x.Documento_Persona,
                                     x.NombreCompleto_Persona,
                                     x.Direccion_Persona,
                                     x.Telefono_Persona,
                                     x.Correo_Persona)
            ws.InsertRow(1, 1)
            ws.Cells("A1:F1").Merge = True
            ws.Cells("A1").Value = "Clientes Bruno Spa"
            With ws.Cells("A1")
                .Style.Font.Bold = True
                .Style.Font.Size = 22
                .Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
            End With

            ws.Cells("A2").LoadFromCollection(ListaFiltrada, True, TableStyles.Medium1)
            Dim GuardarArchvo = GetPickers.CrearFileSavePicker("ListadoClientes")

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
