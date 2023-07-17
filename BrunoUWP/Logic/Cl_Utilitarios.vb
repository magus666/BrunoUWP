Imports System.Text
Imports Microsoft.UI.Xaml.Controls

Public Class Cl_Utilitarios

    Public Function GenerarNumeroDocumento() As String
        Dim Minimo As Integer = 6
        Dim Maximo As Integer = 10
        Dim Aleatorio As New Random()
        Dim length As Integer = Aleatorio.Next(Minimo, Maximo + 1)
        Dim Creador As New StringBuilder(length)
        Creador.Append(Aleatorio.Next(1, 10))
        For i = 1 To length - 1
            Creador.Append(Aleatorio.Next(0, 10))
        Next
        Return Creador.ToString()
    End Function

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

    Public Function GenerarCodigoRaza() As String
        Dim Iniciales As String = "RZA-"
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
    Public Function GeneraCodigoMaestroArticulo() As String
        Dim Iniciales As String = "MAR-"
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

    Public Async Function CrearContentDialogMetodoPago(ValorPago As Double) As Task
        Try
            Dim GetMetodosPago As New Cl_MetodoPago
            Dim container As New StackPanel()

            Dim LblDescripcionPago As New TextBlock()
            LblDescripcionPago.Text = "Valor total a Pagar: " & ValorPago.ToString("C")
            container.Children.Add(LblDescripcionPago)

            Dim CmbMetodoPago As New ComboBox()
            CmbMetodoPago.Header = "Por Favor Seleccione un Metodo de Pago"
            CmbMetodoPago.PlaceholderText = "Metodo de Pago"
            CmbMetodoPago.ItemsSource = Await GetMetodosPago.ConsultaMetodoPago
            CmbMetodoPago.DisplayMemberPath = "Nombre_MetodoPago"
            container.Children.Add(CmbMetodoPago)

            Dim contentDialog As New ContentDialog With {
                .Title = "Pagar",
                .Content = container,
                .PrimaryButtonText = "Pagar",
                .CloseButtonText = "Cancelar"
            }
            Await contentDialog.ShowAsync()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class
