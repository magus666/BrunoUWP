﻿Imports OfficeOpenXml
Imports OfficeOpenXml.Style
Imports OfficeOpenXml.Table
Imports Windows.Storage

Public Class Cl_Articulo
    Implements IArticulo
    Dim GetPickers As New Cl_Pickers

    Public Async Function IArticulo_InsertarArticulo(CodigoArticulo As String, NombreArticulo As String, MarcaArticulo As String, DescripcionArticulo As String, ValorArticulo As Double, CantidadArticulo As Integer, CantidadTotalVentaArticulo As Integer, FechaCreacionArticulo As Date, IdMaestroArticulo As Integer) As Task(Of Boolean) Implements IArticulo.InsertarArticulo
        Try
            Await ConfiguraSqlite()
            Dim Articulo = New ArticuloModel With {
                .Codigo_Articulo = CodigoArticulo,
                .Nombre_Articulo = NombreArticulo,
                .Marca_Articulo = MarcaArticulo,
                .Descripcion_Articulo = DescripcionArticulo,
                .Valor_Articulo = ValorArticulo,
                .Cantidad_Articulo = CantidadArticulo,
                .CantidadTotalVenta_Articulo = CantidadTotalVentaArticulo,
                .FechaCreacion_Articulo = FechaCreacionArticulo,
                .Id_MaestroArticulo = IdMaestroArticulo
            }
            Dim Id = Await ConexionDB.InsertAsync(Articulo)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function IArticulo_ActualizarArticulo(IdArticulo As Integer, ValorArticulo As Double, CantidadArticulo As Integer, CantidadTotalVentaArticulo As Integer) As Task(Of Boolean) Implements IArticulo.ActualizarArticulo
        Try
            Await ConfiguraSqlite()
            Dim GetArticulo = Await ConexionDB.Table(Of ArticuloModel)().ToListAsync()
            Dim Articulo = (From x In GetArticulo
                            Where x.Id_Articulo = IdArticulo
                            Select x).FirstOrDefault
            If Articulo IsNot Nothing Then
                Articulo.Valor_Articulo = ValorArticulo
                Articulo.Cantidad_Articulo = CantidadArticulo
                Articulo.CantidadTotalVenta_Articulo = CantidadTotalVentaArticulo
                Await ConexionDB.UpdateAsync(Articulo)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function IArticulo_ConsultaArticulos() As Task(Of List(Of ArticuloModel)) Implements IArticulo.ConsultaArticulos
        Try
            Await ConfiguraSqlite()
            Dim GetArticulo = Await ConexionDB.Table(Of ArticuloModel)().ToListAsync()
            Dim ListaArticulo = (From x In GetArticulo
                                 Select x).ToList
            Return ListaArticulo
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function IArticulo_CountArticulosUltimoDia() As Task(Of Integer) Implements IArticulo.CountArticulosUltimoDia
        Try
            Await ConfiguraSqlite()
            Dim GetArticulo = Await ConexionDB.Table(Of ArticuloModel)().ToListAsync()
            Dim CountArticulo = (From x In GetArticulo
                                 Where x.FechaCreacion_Articulo >= Date.Now.AddDays(-1)
                                 Select x).Count
            Return CountArticulo
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function IArticulo_CountArticulosUltimaSemana() As Task(Of Integer) Implements IArticulo.CountArticulosUltimaSemana
        Try
            Await ConfiguraSqlite()
            Dim GetArticulo = Await ConexionDB.Table(Of ArticuloModel)().ToListAsync()
            Dim CountArticulo = (From x In GetArticulo
                                 Where x.FechaCreacion_Articulo >= Date.Now.AddDays(-7)
                                 Select x).Count
            Return CountArticulo
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function IArticulo_CountArticulosUltimoMes() As Task(Of Integer) Implements IArticulo.CountArticulosUltimoMes
        Try
            Await ConfiguraSqlite()
            Dim GetArticulo = Await ConexionDB.Table(Of ArticuloModel)().ToListAsync()
            Dim CountArticulo = (From x In GetArticulo
                                 Where x.FechaCreacion_Articulo >= Date.Now.AddMonths(-1)
                                 Select x).Count
            Return CountArticulo
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function IArticulo_CreaExcelArticulo() As Task(Of Boolean) Implements IArticulo.CreaExcelArticulo
        Try
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial
            Dim xlPackage As New ExcelPackage()
            Dim oBook As ExcelWorkbook = xlPackage.Workbook
            Dim ws As ExcelWorksheet = oBook.Worksheets.Add("Clientes")

            Dim GetCLiente As New List(Of ClienteModel)
            Dim ListaArticulos = Await IArticulo_ConsultaArticulos()
            Dim ListaFiltrada = (From x In ListaArticulos
                                 Order By x.Nombre_Articulo
                                 Select Codigo = x.Codigo_Articulo,
                                        Nombre = x.Nombre_Articulo,
                                        Marca = x.Marca_Articulo,
                                        Descripcion = x.Descripcion_Articulo,
                                        Valor_Unitario = x.Valor_Articulo,
                                        Existencias_Actuales = x.Cantidad_Articulo,
                                        Articulos_Vendidos = x.CantidadTotalVenta_Articulo)
            ws.InsertRow(1, 1)
            ws.Cells("A1:G1").Merge = True
            ws.Cells("A1").Value = "Articulos Bruno Spa"
            With ws.Cells("A1")
                .Style.Font.Bold = True
                .Style.Font.Size = 22
                .Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
            End With

            ws.Cells("A2").LoadFromCollection(ListaFiltrada, True, TableStyles.Medium1)
            Dim GuardarArchvo = GetPickers.CrearFileSavePicker("ListadoArticulos")

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
