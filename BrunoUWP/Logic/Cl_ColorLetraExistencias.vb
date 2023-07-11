Imports Windows.UI

Public Class Cl_ColorLetraExistencias
    Implements IValueConverter

    Public Function Convert(value As Object, targetType As Type, parameter As Object, language As String) As Object Implements IValueConverter.Convert
        Dim Existencias As Integer = CType(value, Integer)
        If Existencias >= 10 Then
            Return New SolidColorBrush(Colors.Green)
        ElseIf Existencias >= 6 Then
            Return New SolidColorBrush(Colors.Yellow)
        ElseIf Existencias >= 1 Then
            Return New SolidColorBrush(Colors.Red)
        End If
    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, language As String) As Object Implements IValueConverter.ConvertBack
        Throw New NotImplementedException()
    End Function
End Class
