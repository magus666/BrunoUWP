Imports System.Text.RegularExpressions

Public Class Cl_Validaciones
    Public Function ValidaCorreo(CorreoElectronico As String) As Boolean
        Try
            Dim emailRegex As New Regex("\b[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}\b", RegexOptions.IgnoreCase)
            If emailRegex.IsMatch(CorreoElectronico) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function ValidaTextBoxVacio(CajaTextBox As TextBox) As Boolean
        If CajaTextBox.Text = String.Empty Then
            Return False
        Else
            Return True
        End If
    End Function
End Class
