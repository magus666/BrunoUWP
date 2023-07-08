' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

Imports Windows.UI.Xaml.Controls.Maps
''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_CreaArticulo
    Inherits Page
    Dim GetUtilitarios As New Cl_Utilitarios
    Dim GetArticulo As New Cl_Articulo

    Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        If String.IsNullOrEmpty(TxtValorArticulo.Text) Then
            TxtValorArticulo.Text = "$"
            TxtValorArticulo.SelectionStart = TxtValorArticulo.Text.Length
        End If
    End Sub

    Private Sub TxtValorArticulo_BeforeTextChanging(sender As TextBox, args As TextBoxBeforeTextChangingEventArgs)
        args.Cancel = args.NewText.Any(Function(c) Not Char.IsDigit(c))
    End Sub

    Private Async Sub BtnGuardar_Click(sender As Object, e As RoutedEventArgs)
        Try
            Dim CodigoArticulo As String = GetUtilitarios.GeneraCodigoArticulo()
            If Await GetArticulo.InsertarArticulo(CodigoArticulo, TxtNombreArticulo.Text, TxtMarcaArticulo.Text,
                                                            TxtDescripcionArticulo.Text, TxtValorArticulo.Text,
                                                            NmbCantidadExistenciasArticulo.Text, Date.Now) = True Then
                Dim Retorno = "Buena la Rata"
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
