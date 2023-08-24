Public Interface ICambioMoneda
    Function CalculaValorServicio(TipoServicio As Integer, Optional DimensionoPerro As Integer = Nothing) As Integer
    Function ConvertirPesosAEuros(valor As Integer) As Double
End Interface
