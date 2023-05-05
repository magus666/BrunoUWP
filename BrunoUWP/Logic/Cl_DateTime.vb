Imports System.Globalization

Public Class Cl_DateTime

    Public Function ObtenerHoraActual() As String
        Dim FechaHoraActual As Date = Date.Now
        Dim HoraActual As String = FechaHoraActual.ToString("hh:mm tt", CultureInfo.InvariantCulture)
        Return HoraActual
    End Function

End Class
