Imports SQLite

Public Class PagosModel
    <PrimaryKey, AutoIncrement>
    Public Property Id_Pagos As Integer
    Public Property Codigo_Pagos As String
    Public Property Monto_Pagos As Double
    Public Property Id_Cita As Integer
    Public Property Id_MetodoPago As Integer

End Class
