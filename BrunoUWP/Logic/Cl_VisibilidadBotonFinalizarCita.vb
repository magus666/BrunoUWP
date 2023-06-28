Public Class Cl_VisibilidadBotonFinalizarCita
    Implements IValueConverter

    Public Function Convert(value As Object, targetType As Type, parameter As Object, language As String) As Object Implements IValueConverter.Convert
        Dim estado As String = CType(value, String)
        If estado = "Pendiente" Then
            Return Visibility.Visible
        Else
            Return Visibility.Collapsed
        End If
    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, language As String) As Object Implements IValueConverter.ConvertBack
        Throw New NotImplementedException()
    End Function
End Class
