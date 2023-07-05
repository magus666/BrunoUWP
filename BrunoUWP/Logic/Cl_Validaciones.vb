Imports System.Text.RegularExpressions
Imports Windows.UI.Popups

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
        Try
            If CajaTextBox.Text = String.Empty Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function ValidaComboBoxVacio(Combo As ComboBox) As Boolean
        Try
            If Combo.SelectedIndex < 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Function ValidarApellidos(Apellido As String) As Boolean
        Dim Palabras() As String = Apellido.Split(" "c)
        Dim contador As Integer = 0
        For Each Palabra In Palabras
            If Char.IsUpper(Palabra(0)) Then
                contador += 1
            End If
        Next
        If contador = 2 Then
            Return True
        Else
            Return False
        End If
    End Function
    Function ValidarNumeroTelefonico(Numero As String) As Boolean
        Try
            If Numero.Length = 10 AndAlso Numero.StartsWith("3") Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function ValidacionDosFechas(FechaInicio As Date, FechaFin As Date) As Boolean
        Try
            If FechaFin < FechaInicio Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function ValidaHoraTimePicker(ElementoTimePiker As TimePicker, HoraPrevia As Date) As Boolean
        Try
            Dim selectedTime As TimeSpan = ElementoTimePiker.Time
            Dim dbTime As TimeSpan = HoraPrevia.TimeOfDay
            Dim twoHoursLater As TimeSpan = dbTime.Add(TimeSpan.FromHours(2))
            If selectedTime >= dbTime AndAlso selectedTime <= twoHoursLater Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class
