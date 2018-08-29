Public Class zfactura
    Public Property numeroFactura As String
    Public Property centro As String
    Public Property year As String
    Public Property fileDir As String
    Public Property poliza As String
    Public Property status As Boolean
    Public Property serie As String
    Public Property folio As String
    Public Property mensaje As String
    Public Property tipoDocumento As String
    Public Property folioFactura As String
    Public Property fechaFactura As String

    Public Property Subtotal As String
    Public Property IVA As String
    Public Property Total As String

    Private Property ConnectionSAP As Object

    Public Sub New(ByVal configSAP As ConfigSAP, ByVal VBELN As String)
        'Me.ConnectionSAP = ConnectionSAP
        ConnectionSAP = CreateObject("SAP.Functions")
        ConnectionSAP.Connection.System = configSAP.systemSAP
        ConnectionSAP.Connection.SystemNumber = configSAP.systemNumberSAP
        ConnectionSAP.Connection.Applicationserver = configSAP.applicationServer
        ConnectionSAP.Connection.Client = configSAP.client
        ConnectionSAP.Connection.User = configSAP.user
        ConnectionSAP.Connection.Password = configSAP.password
        ConnectionSAP.Connection.language = configSAP.language
        ConnectionSAP.Connection.Saprouter = configSAP.SAProuter
        ConnectionSAP.Connection.Logon(0, True)

        ObtenerPropiedades(VBELN)
    End Sub
    Public Function Actualizar_FacturaSAP() As Boolean
        Dim respuesta As Boolean = False
        Dim funcionSAP As Object
        Dim datosSAP As Object
        funcionSAP = ConnectionSAP.Add("ZRECIBE_FACTURA")
        datosSAP = funcionSAP.Tables("ZFACTURA")
        datosSAP.rows.Add()
        datosSAP.Value(1, "VBELN") = Me.numeroFactura 'Factura //numero factura
        datosSAP.Value(1, "BUKRS") = Me.centro 'Centro 
        datosSAP.VALUE(1, "GJAHR") = Me.year 'Año
        datosSAP.VALUE(1, "EFILE") = Me.fileDir 'Direccion Archivo
        datosSAP.VALUE(1, "FI") = Me.poliza 'Poliza FI  //---
        If Me.status Then
            datosSAP.VALUE(1, "STATUS") = "E"
        Else
            datosSAP.VALUE(1, "STATUS") = "X"
        End If
        datosSAP.Value(1, "SERIE") = Me.serie 'Serie
        datosSAP.Value(1, "FOLIO") = Me.folio 'Folio
        datosSAP.Value(1, "DESCRIP") = Me.mensaje 'Mensaje //Mensaje de MySuite
        datosSAP.Value(1, "FKART") = Me.tipoDocumento 'Tipo de Factura //Tipo de documento
        datosSAP.VALUE(1, "FOLIOF") = Me.folioFactura ' Folio de la factura
        datosSAP.VALUE(1, "FECHAF") = Me.fechaFactura

        datosSAP.VALUE(1, "SUBTOTAL") = Me.Subtotal
        datosSAP.VALUE(1, "IVA") = Me.IVA
        datosSAP.VALUE(1, "TOTAL") = Me.Total

        respuesta = funcionSAP.CALL

        Return respuesta
    End Function

    Private Sub ObtenerPropiedades(ByVal VBELN As String)
        Dim respuesta As Boolean = False
        Dim funcionSAP As Object
        Dim datosSAP As Object
        Dim TablaZFactura As Object

        funcionSAP = ConnectionSAP.Add("ZENVIA_FACTURA")
        datosSAP = funcionSAP.Tables("ZFACT")
        datosSAP.rows.Add()

        datosSAP.Value(1, "VBELN") = VBELN

        If funcionSAP.CALL Then
            'Console.WriteLine("Factura (s) obtenida (s) correctamente.")
            TablaZFactura = funcionSAP.Tables("ZFACTURA")
            'Console.WriteLine("Facturas a timbrar:" + TablaZFactura.rowcount.ToString)

            If TablaZFactura.rowcount > 0 Then
                Me.numeroFactura = TablaZFactura(1, "VBELN")
                Me.centro = TablaZFactura(1, "BUKRS")
                Me.year = TablaZFactura(1, "GJAHR")
                Me.fileDir = TablaZFactura(1, "EFILE")
                Me.poliza = TablaZFactura(1, "FI")
                Me.status = False
                Me.serie = ""
                Me.folio = ""
                Me.mensaje = ""
                Me.tipoDocumento = TablaZFactura(1, "FKART")
                Me.folioFactura = ""
                Me.fechaFactura = ""
            End If
        End If
    End Sub

End Class
