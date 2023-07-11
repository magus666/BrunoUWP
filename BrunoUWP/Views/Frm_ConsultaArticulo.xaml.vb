' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_ConsultaArticulo
    Inherits Page
    Dim GetMaestroArticulo As New Cl_MaestroArticulo
    Dim GetArticulos As New Cl_Articulo
    Dim GetNotificaciones As New Cl_Notificaciones
    Dim IdMaestroArticulo As Integer

    Private Async Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            Dim CargaArticulos = Await GetArticulos.ConsultaArticulos()
            If CargaArticulos.Count = 0 Then
                LblTituloCreacionArticulo.Visibility = Visibility.Visible
                DtgArticulos.Visibility = Visibility.Collapsed
            Else
                DtgArticulos.ItemsSource = CargaArticulos
                LblTituloCreacionArticulo.Visibility = Visibility.Collapsed
                DtgArticulos.Visibility = Visibility.Visible
                CmbMaestroArticulo.ItemsSource = Await GetMaestroArticulo.ConsultaMaestroArticulos
                CmbMaestroArticulo.DisplayMemberPath = "Nombre_MaestroArticulo"
            End If
        Catch ex As Exception
            GetNotificaciones.AlertaErrorInfoBar(InfAlerta, "Error", ex.Message)
        End Try
    End Sub

    Private Async Sub AppBarButton_Click(sender As Object, e As RoutedEventArgs)
        Try
            PgrGeneraExcel.IsActive = True
            If Await GetArticulos.CreaExcelArticulo = True Then
                PgrGeneraExcel.IsActive = False
                GetNotificaciones.AlertaExitoInfoBar(InfAlerta, "Exito", "La informacion Se exportó con exito a Excel")
            Else
                GetNotificaciones.AlertaAdvertenciaInfoBar(InfAlerta, "Advertencia", "Se canceló la Exportacion a Excel")
                PgrGeneraExcel.IsActive = False
            End If

        Catch ex As Exception
            GetNotificaciones.AlertaErrorInfoBar(InfAlerta, "Error", ex.Message)
        End Try
    End Sub

    Private Async Sub CmbMaestroArticulo_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        Try
            Dim comboBox As ComboBox = CType(sender, ComboBox)
            Dim selectedItem As MaestroArticuloModel = CType(comboBox.SelectedItem, MaestroArticuloModel)
            If CmbMaestroArticulo.SelectedIndex = -1 Then
                IdMaestroArticulo = 0
            Else
                IdMaestroArticulo = selectedItem.Id_MaestroArticulo
                Dim Articulo = Await GetArticulos.ConsultaArticulos
                Dim ListaRetornoArticulo = (From Mar In Articulo
                                            Where Mar.Id_MaestroArticulo = IdMaestroArticulo
                                            Select Mar).ToList
                DtgArticulos.ItemsSource = ListaRetornoArticulo
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
