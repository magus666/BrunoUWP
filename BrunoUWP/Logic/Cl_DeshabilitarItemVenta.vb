Imports Windows.UI

Public Class Cl_DeshabilitarItemVenta
    Implements IValueConverter

    Public Function Convert(value As Object, targetType As Type, parameter As Object, language As String) As Object Implements IValueConverter.Convert
        Dim Existencias As Integer = CType(value, Integer)
        If Existencias <= 0 Then
            Return False
        Else
            Return True
        End If
    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, language As String) As Object Implements IValueConverter.ConvertBack
        Throw New NotImplementedException()
    End Function
End Class
