Public Class Cl_MetodoPago

    Public Async Function InsertarActualizarMetodoPago() As Task(Of Boolean)
        Try
            Dim GetMetodoPago = Await ConexionDB.Table(Of MetodoPagoModel)().ToListAsync()
            Dim Efectivo = (From x In GetMetodoPago
                            Where x.Nombre_MetodoPago = "Efectivo"
                            Select x).Count
            Dim Nequi = (From x In GetMetodoPago
                         Where x.Nombre_MetodoPago = "Nequi"
                         Select x).Count
            Dim Daviplata = (From x In GetMetodoPago
                             Where x.Nombre_MetodoPago = "DaviPlata"
                             Select x).Count
            Dim Tarjeta = (From x In GetMetodoPago
                           Where x.Nombre_MetodoPago = "Tarjeta"
                           Select x).Count
            If Efectivo = 0 Then
                Dim ValorEfectivo = New MetodoPagoModel With {
                    .Nombre_MetodoPago = "Efectivo"
                }
                Dim Id = Await ConexionDB.InsertAsync(ValorEfectivo)
            End If
            If Nequi = 0 Then
                Dim ValorNequi = New MetodoPagoModel With {
                    .Nombre_MetodoPago = "Nequi"
                }
                Dim Id = Await ConexionDB.InsertAsync(ValorNequi)
            End If
            If Daviplata = 0 Then
                Dim ValorDaviPlata = New MetodoPagoModel With {
                    .Nombre_MetodoPago = "DaviPlata"
                }
                Dim Id = Await ConexionDB.InsertAsync(ValorDaviPlata)
            End If
            If Tarjeta = 0 Then
                Dim ValorTarjeta = New MetodoPagoModel With {
                    .Nombre_MetodoPago = "Tarjeta"
                }
                Dim Id = Await ConexionDB.InsertAsync(ValorTarjeta)
            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Async Function ConsultaMetodoPago() As Task(Of List(Of MetodoPagoModel))
        Try
            Await ConfiguraSqlite()
            Dim GetMetodoPago = Await ConexionDB.Table(Of MetodoPagoModel)().ToListAsync()
            Dim ListaMetodoPago = (From x In GetMetodoPago
                                   Select x).ToList
            Return ListaMetodoPago
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class
