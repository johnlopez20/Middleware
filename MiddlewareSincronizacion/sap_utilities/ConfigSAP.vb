Public Class ConfigSAP
    Public Property systemSAP
    Public Property systemNumberSAP
    Public Property applicationServer
    Public Property client
    Public Property user
    Public Property password
    Public Property language
    Public Property SAProuter
    Private Property ConnectionSAP As Object

    Public Sub New()
        Try
            ConnectionSAP = CreateObject("SAP.Functions")

        Catch ex As Exception
            MsgBox("" + ex.Message, MsgBoxStyle.Exclamation)
        End Try
        systemSAP = String.Empty '"AreConsulting"
        systemNumberSAP = String.Empty '"00"
        applicationServer = String.Empty '""
        client = String.Empty '"800"
        user = String.Empty '"JCRUZ"
        password = String.Empty '"JUANSAP"
        language = String.Empty '"ES"
        SAProuter = String.Empty '"192.168.25.25"
    End Sub
    Public Function TestConection() As Boolean
        Dim respuesta As Boolean = False
        Try
            ConnectionSAP = CreateObject("SAP.Functions")

        Catch ex As Exception
            MsgBox("" + ex.Message, MsgBoxStyle.Exclamation)
        End Try

        Try
            ConnectionSAP.Connection.System = systemSAP
            ConnectionSAP.Connection.SystemNumber = systemNumberSAP
            ConnectionSAP.Connection.Applicationserver = applicationServer
            ConnectionSAP.Connection.Client = client
            ConnectionSAP.Connection.User = user
            ConnectionSAP.Connection.Password = password
            ConnectionSAP.Connection.language = language
            ConnectionSAP.Connection.Saprouter = SAProuter
            respuesta = ConnectionSAP.Connection.Logon(0, True)
        Catch ex As Exception
            MsgBox("" + ex.Message, MsgBoxStyle.Exclamation)
        End Try
        Return respuesta
    End Function
End Class
