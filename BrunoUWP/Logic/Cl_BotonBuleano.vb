Public Class Cl_BotonBuleano
    Implements IValueConverter

    Public Function Convert(value As Object, targetType As Type, parameter As Object, language As String) As Object Implements IValueConverter.Convert
        If CType(value, Boolean) Then
            Return Visibility.Visible
        Else
            Return Visibility.Collapsed
        End If
    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, language As String) As Object Implements IValueConverter.ConvertBack
        If CType(value, Visibility) = Visibility.Visible Then
            Return True
        Else
            Return False
        End If
    End Function
End Class
