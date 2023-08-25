Public Interface IOpereacionesContables
    Function CalculaValorServicio(TipoServicio As Integer, Optional DimensionoPerro As Integer = Nothing) As Integer
    Function ConvertirPesosAEuros(valor As Double) As Double
End Interface
