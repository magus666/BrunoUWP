Imports System.Globalization

Public Class Cl_CambioMoneda
    Implements ICambioMoneda

    Public Function CalculaValorServicio(TipoServicio As Integer, Optional DimensionoPerro As Integer = 0) As Integer Implements ICambioMoneda.CalculaValorServicio
        Try
            Dim RetornoValor As Double
            Select Case TipoServicio
                Case 1
                    RetornoValor = 4000
                Case 2
                    Select Case DimensionoPerro
                        Case 1
                            RetornoValor = ConvertirPesosAEuros(15000)
                        Case 2
                            RetornoValor = ConvertirPesosAEuros(22000)
                        Case 3
                            RetornoValor = ConvertirPesosAEuros(30000)
                    End Select
                Case 3
                    Select Case DimensionoPerro
                        Case 1
                            RetornoValor = ConvertirPesosAEuros(20000)
                        Case 2
                            RetornoValor = ConvertirPesosAEuros(35000)
                        Case 3
                            RetornoValor = ConvertirPesosAEuros(50000)
                    End Select
                Case 4
                    Select Case DimensionoPerro
                        Case 1
                            RetornoValor = ConvertirPesosAEuros(15000)
                        Case 2
                            RetornoValor = ConvertirPesosAEuros(22000)
                        Case 3
                            RetornoValor = ConvertirPesosAEuros(30000)
                    End Select
            End Select
            Return RetornoValor
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function ConvertirPesosAEuros(Cantidad As Double) As Double Implements ICambioMoneda.ConvertirPesosAEuros
        Try
            Dim regionInfo As New RegionInfo(Threading.Thread.CurrentThread.CurrentUICulture.LCID)
            Dim currencySymbol As String = regionInfo.ISOCurrencySymbol
            Dim ValorResultado As Double
            Select Case currencySymbol
                Case "COP"
                    ValorResultado = Cantidad
                Case "EUR"
                    ValorResultado = Cantidad * 0.000226
                Case "USD"
                    ValorResultado = Cantidad * 0.000244
            End Select
            Return ValorResultado
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class
