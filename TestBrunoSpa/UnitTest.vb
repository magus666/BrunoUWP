Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports BrunoUWP

<TestClass>
Public Class UnitTest

    <TestMethod>
    Public Async Function TestInsertarCliente() As Task
        Dim cliente As New Cl_Cliente()
        Dim resultado As Boolean = Await cliente.InsertarCliente("123456", "Juan", "Pérez", "Calle 123", "555-1234", "juan@example.com", 30, 1, "C123", True)
        Assert.IsTrue(resultado, "El cliente no se insertó correctamente.")
    End Function

End Class
