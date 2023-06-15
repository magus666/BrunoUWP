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
            If Efectivo = 0 Then
                Dim DimPequenio = New MetodoPagoModel With {
                    .Nombre_MetodoPago = "Efectivo"
                }
                Dim Id = Await ConexionDB.InsertAsync(DimPequenio)
            End If
            If Nequi = 0 Then
                Dim DimMediano = New MetodoPagoModel With {
                    .Nombre_MetodoPago = "Nequi"
                }
                Dim Id = Await ConexionDB.InsertAsync(DimMediano)
            End If
            If Daviplata = 0 Then
                Dim DimGrande = New MetodoPagoModel With {
                    .Nombre_MetodoPago = "DaviPlata"
                }
                Dim Id = Await ConexionDB.InsertAsync(DimGrande)
            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class
