Imports Microsoft.UI.Xaml.Controls

Public Class Cl_Utilitarios

    Public Function GenerarCodigoCliente() As String
        Dim Iniciales As String = "CLI-"
        Dim RetornoCodigo As String
        Dim Longitud As Integer = 6
        Dim caracteres As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
        Dim codigo As String = ""
        Dim rnd As New Random()
        For i As Integer = 1 To Longitud
            Dim index As Integer = rnd.Next(0, caracteres.Length)
            codigo &= caracteres.Substring(index, 1)
        Next
        RetornoCodigo = Iniciales & codigo
        Return RetornoCodigo
    End Function

    Public Function GenerarCodigoMascota() As String
        Dim Iniciales As String = "MSC-"
        Dim RetornoCodigo As String
        Dim Longitud As Integer = 6
        Dim caracteres As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
        Dim codigo As String = ""
        Dim rnd As New Random()
        For i As Integer = 1 To Longitud
            Dim index As Integer = rnd.Next(0, caracteres.Length)
            codigo &= caracteres.Substring(index, 1)
        Next
        RetornoCodigo = Iniciales & codigo
        Return RetornoCodigo
    End Function

    Public Function GenearCodigoCita() As String
        Dim Iniciales As String = "CIT-"
        Dim RetornoCodigo As String
        Dim Longitud As Integer = 6
        Dim caracteres As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
        Dim codigo As String = ""
        Dim rnd As New Random()
        For i As Integer = 1 To Longitud
            Dim index As Integer = rnd.Next(0, caracteres.Length)
            codigo &= caracteres.Substring(index, 1)
        Next
        RetornoCodigo = Iniciales & codigo
        Return RetornoCodigo
    End Function

    Public Function GenearCodigoVenta() As String
        Dim Iniciales As String = "VNT-"
        Dim RetornoCodigo As String
        Dim Longitud As Integer = 6
        Dim caracteres As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
        Dim codigo As String = ""
        Dim rnd As New Random()
        For i As Integer = 1 To Longitud
            Dim index As Integer = rnd.Next(0, caracteres.Length)
            codigo &= caracteres.Substring(index, 1)
        Next
        RetornoCodigo = Iniciales & codigo
        Return RetornoCodigo
    End Function
    Public Function GeneraCodigoArticulo() As String
        Dim Iniciales As String = "ART-"
        Dim RetornoCodigo As String
        Dim Longitud As Integer = 6
        Dim caracteres As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
        Dim codigo As String = ""
        Dim rnd As New Random()
        For i As Integer = 1 To Longitud
            Dim index As Integer = rnd.Next(0, caracteres.Length)
            codigo &= caracteres.Substring(index, 1)
        Next
        RetornoCodigo = Iniciales & codigo
        Return RetornoCodigo
    End Function

    Public Sub LimpiarControles(StpDatos As StackPanel)
        Try
            For Each element As UIElement In StpDatos.Children
                If (TypeOf element Is TextBox) Then
                    DirectCast(element, TextBox).Text = String.Empty
                End If
                If (TypeOf element Is ComboBox) Then
                    DirectCast(element, ComboBox).SelectedIndex = -1
                End If
                If (TypeOf element Is NumberBox) Then
                    DirectCast(element, NumberBox).Text = 1
                End If
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

End Class
