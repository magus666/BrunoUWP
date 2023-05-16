Imports System.Globalization

Public Class Cl_DateTime
    Implements In_FechaHora
    Public Function ObtenerHoraActual() As String Implements In_FechaHora.ObtenerHoraActual
        Try
            Dim FechaHoraActual As Date = Date.Now
            Dim HoraActual As String = FechaHoraActual.ToString("hh:mm tt", CultureInfo.InvariantCulture)
            Return HoraActual
        Catch ex As Exception
            Throw New Exception
        End Try
    End Function
End Class
