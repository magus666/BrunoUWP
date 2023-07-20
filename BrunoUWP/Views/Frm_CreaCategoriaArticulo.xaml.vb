' La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

Imports Microsoft.Toolkit.Uwp.UI.Controls
''' <summary>
''' Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
''' </summary>
Public NotInheritable Class Frm_CreaCategoriaArticulo
    Inherits Page
    Dim GetUtilitarios As New Cl_Utilitarios
    Dim GetValidaciones As New Cl_Validaciones
    Dim GetNotificaciones As New Cl_Notificaciones
    Dim GetCategoriaArticulo As New Cl_CategoriaArticulo
    Dim IdMaestroArticulo As Integer

    Private Async Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            DtgMaestroArticulo.ItemsSource = Await GetCategoriaArticulo.ConsultaCategoriaArticulo
        Catch ex As Exception
            GetNotificaciones.AlertaErrorInfoBar(InfAlerta, "Error", ex.Message)
        End Try
    End Sub

    Private Async Sub BtnGuardar_Click(sender As Object, e As RoutedEventArgs)
        Try
            Dim CodigoMastroArticulo As String = GetUtilitarios.GeneraCodigoMaestroArticulo
            If ValidaDatos() = True Then
                If Await GetCategoriaArticulo.InsertarCategoriaArticulo(CodigoMastroArticulo, TxtNombreMaestroArticulo.Text,
                                                                TxtDescripcionMaestroArticulo.Text, Date.Now) = True Then
                    GetNotificaciones.AlertaExitoInfoBar(InfAlerta, "Exito", "Se guardo la Categoria Correctamente.")
                    GetUtilitarios.LimpiarControles(StpPrincipal)
                    DtgMaestroArticulo.ItemsSource = Await GetCategoriaArticulo.ConsultaCategoriaArticulo
                End If
            End If
        Catch ex As Exception
            GetNotificaciones.AlertaErrorInfoBar(InfAlerta, "Error", ex.Message)
        End Try
    End Sub

    Public Function ValidaDatos() As Boolean
        Try
            If GetValidaciones.ValidaTextBoxVacio(TxtNombreMaestroArticulo) = False Then
                GetNotificaciones.ValidacionControlesTeachingTip(TctAlerta, "Aviso", "El Nombre no puede Estar Vacio.", TxtNombreMaestroArticulo)
                Return False
                Exit Function
            End If
            If GetValidaciones.ValidaTextBoxVacio(TxtDescripcionMaestroArticulo) = False Then
                GetNotificaciones.ValidacionControlesTeachingTip(TctAlerta, "Aviso", "El Nombre no puede Estar Vacio.", TxtDescripcionMaestroArticulo)
                Return False
                Exit Function
            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Private Async Sub DtgMaestroArticulo_DoubleTapped(sender As Object, e As DoubleTappedRoutedEventArgs)
        Try
            Dim ObtenerMaestroArticulo As New CategoriaArticuloModel
            ObtenerMaestroArticulo = DtgMaestroArticulo.SelectedItem
            IdMaestroArticulo = ObtenerMaestroArticulo.Id_MaestroArticulo
            TxtNombreMaestroArticuloDialog.Text = ObtenerMaestroArticulo.Nombre_MaestroArticulo
            TxtDescripcionMaestroArticuloDialog.Text = ObtenerMaestroArticulo.Descripcion_MaestroArticulo
            Await CtdModificaArticuloMaestro.ShowAsync()
        Catch ex As Exception
            GetNotificaciones.AlertaErrorInfoBar(InfAlerta, "Error", ex.Message)
        End Try
    End Sub

    Private Async Sub CtdModificaArticuloMaestro_PrimaryButtonClick(sender As ContentDialog, args As ContentDialogButtonClickEventArgs)
        Try
            If Await GetCategoriaArticulo.ActualizarCategoriaArticulo(IdMaestroArticulo, TxtNombreMaestroArticuloDialog.Text,
                                                            TxtDescripcionMaestroArticuloDialog.Text) = True Then
                GetNotificaciones.AlertaExitoInfoBar(InfAlerta, "Exito", "El Articulo maestro fue Actualizado con Exito.")
                DtgMaestroArticulo.ItemsSource = Await GetCategoriaArticulo.ConsultaCategoriaArticulo
            End If
        Catch ex As Exception
            GetNotificaciones.AlertaErrorInfoBar(InfAlerta, "Error", ex.Message)
        End Try
    End Sub

    Private Async Sub DtgMaestroArticulo_Sorting(sender As Object, e As DataGridColumnEventArgs)
        Try
            Dim ObtenerDatos = Await GetCategoriaArticulo.ConsultaCategoriaArticulo
            If e.Column.Tag.ToString() = "NombreMaestroArticulo" Then
                'Implement sort on the column "Range" using LINQ
                If e.Column.SortDirection Is Nothing OrElse e.Column.SortDirection = DataGridSortDirection.Descending Then
                    DtgMaestroArticulo.ItemsSource = (From item In ObtenerDatos
                                                      Order By item.Nombre_MaestroArticulo Ascending)
                    e.Column.SortDirection = DataGridSortDirection.Ascending
                Else
                    DtgMaestroArticulo.ItemsSource = (From item In ObtenerDatos
                                                      Order By item.Nombre_MaestroArticulo Descending)
                    e.Column.SortDirection = DataGridSortDirection.Descending
                End If
            End If
        Catch ex As Exception
            GetNotificaciones.AlertaErrorInfoBar(InfAlerta, "Error", ex.Message)
        End Try
    End Sub
End Class
