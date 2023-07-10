Public Class Cl_AgregaSignoPesos
    Implements IValueConverter

    Public Function Convert(value As Object, targetType As Type, parameter As Object, language As String) As Object Implements IValueConverter.Convert
        If TypeOf value Is Double Then
            Dim ValorEnPesos As Double = CType(value, Double)
            Return ValorEnPesos.ToString("C")
        End If
        Return value
    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, language As String) As Object Implements IValueConverter.ConvertBack
        Throw New NotImplementedException()
    End Function
End Class
