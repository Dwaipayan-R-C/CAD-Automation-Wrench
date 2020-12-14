Public Class Form1

    Dim swApp As SldWorks.SldWorks
    Dim swModel As SldWorks.ModelDoc2
    Dim swModel1 As SldWorks.ModelDoc2
    Dim swCompModel As SldWorks.ModelDoc2
    Dim swSelMgr As SldWorks.SelectionMgr
    Dim swComp As SldWorks.Component2
    Dim vMatProp As Object
    Dim Part As Object
    Dim bRet As Boolean
    Dim booleanpi As Boolean
    Dim errors As Long
    Dim warnings As Long
    Dim var As String
    Dim red As Integer
    Dim grn As Integer
    Dim bl As Integer
    Dim Directory As String
    Dim Matvalue1 As String
    'Dim Matvalue2 As String
    'Dim Visible As Boolean
    Dim swModelDocExt As SldWorks.ModelDocExtension
    Dim swAppearance As SldWorks.RenderMaterial
    Dim bool2 As Boolean
    Dim strName As String
    Dim nDecalID As Long
    Dim bool As Boolean
    Dim boolea As Boolean
    Dim err As Long
    Dim warn As Long
    Dim Boolst As Boolean
    Dim longstatus As Long, longwarnings As Long
    Dim Longi As Long
    Dim Directory1 As String

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ComboBox1.Items.Add("Red")
        Me.ComboBox1.Items.Add("Green")
        Me.ComboBox1.Items.Add("Blue")

        Me.ComboBox2.Items.Add("Brushed Aluminium")
        Me.ComboBox2.Items.Add("Brushed Brass")
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


        If (ofd.ShowDialog() = DialogResult.OK) Then
            Directory = ofd.FileName
            Dim FolderPath As String = Directory
            Directory1 = (FolderPath.Substring(0, FolderPath.LastIndexOf("\")))
            'Debug.Print(Directory1 + "\text")
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        swApp = New SldWorks.SldWorks()
        swModel1 = swApp.OpenDoc6(Directory1 + "\GeneratedModel.SLDASM", SwConst.swDocumentTypes_e.swDocASSEMBLY, SwConst.swOpenDocOptions_e.swOpenDocOptions_Silent, "", err, warn)
        swModel1.Visible = True

        'Dim swErrors As Long
        'Dim swWarnings As Long
        'boolea = swModel.Save3(1, swErrors, swWarnings)
        'swModel = swApp.OpenDoc6("E:\Tandemloop\Project\WIP\Model_Specifications\GRIP-TEXTURE.sldprt", SwConst.swDocumentTypes_e.swDocPART, SwConst.swOpenDocOptions_e.swOpenDocOptions_Silent, "", err, warn)
        'swModelDocExt = swModel.Extension
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim myProcess() As Process = System.Diagnostics.Process.GetProcessesByName("SLDWORKS")

        For Each myKill As Process In myProcess
            myKill.Kill()

        Next
    End Sub

    Public Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        swApp = New SldWorks.SldWorks()


        'swModel = swApp.OpenDoc6("C:\Stanley\Final\GRIP-TEXTURE.sldprt", SwConst.swDocumentTypes_e.swDocPART, SwConst.swOpenDocOptions_e.swOpenDocOptions_Silent, "", err, warn)
        swModel = swApp.OpenDoc6(Directory1 + "\GRIP-TEXTURE.sldprt", SwConst.swDocumentTypes_e.swDocPART, SwConst.swOpenDocOptions_e.swOpenDocOptions_Silent, "", err, warn)
        swModelDocExt = swModel.Extension

        swModel.ClearSelection2(True)

        If Me.ComboBox2.SelectedIndex = 0 Then
            Matvalue1 = "C:\Program Files\SOLIDWORKS Corp\SOLIDWORKS\IRay\mdl\solidworks\materials\metal\aluminum\brushed_aluminum.mdl"
            strName = Matvalue1
            swAppearance = swModelDocExt.CreateRenderMaterial(strName)
            bool2 = swAppearance.AddEntity(swModel)
            bool2 = swModelDocExt.AddRenderMaterial(swAppearance, nDecalID)
            Dim swErrors As Long
            Dim swWarnings As Long

            'bool = swModel.EditRebuild3()
            swModel.Save3(1, swErrors, swWarnings)

        ElseIf Me.ComboBox2.SelectedIndex = 0 Then
            Matvalue1 = "C:\Program Files\SOLIDWORKS Corp\SOLIDWORKS\IRay\mdl\solidworks\materials\metal\brass\brushed_brass.mdl"
            strName = Matvalue1
            swAppearance = swModelDocExt.CreateRenderMaterial(strName)
            bool2 = swAppearance.AddEntity(swModel)
            bool2 = swModelDocExt.AddRenderMaterial(swAppearance, nDecalID)
            Dim swErrors As Long
            Dim swWarnings As Long

            bool = swModel.Save3(1, swErrors, swWarnings)
            'bool = swModel.EditRebuild3()
        End If
        Boolst = swModel.EditRebuild3()

        swModel.ClearSelection2(True)
        ' Apply the specified appearance to add to the model


        'Debug.Print(bool)

        swModel = swApp.OpenDoc6(Directory, SwConst.swDocumentTypes_e.swDocASSEMBLY, SwConst.swOpenDocOptions_e.swOpenDocOptions_Silent, "", errors, warnings)

        If (swModel Is Nothing) Then
            MessageBox.Show("End Previously Running Solidworks File")

        Else

            swSelMgr = swModel.SelectionManager

            booleanpi = swModel.Extension.SelectByID2("GRIP-TEXTURE-1@Spanner", "COMPONENT", 0, 0, 0, False, 0, Nothing, 0)

            swComp = swSelMgr.GetSelectedObjectsComponent2(1)
            vMatProp = swComp.MaterialPropertyValues

            'Debug.Print("sdvdsv")

            If (vMatProp Is Nothing) Then
                ' Empty if no component-level colors specified,
                ' so get from underlying model
                swCompModel = swComp.GetModelDoc
                If swCompModel Is Nothing Then
                    ' Component is lightweight
                    Debug.Print("Selected component is lightweight; exiting macro.")
                    Exit Sub
                End If

                vMatProp = swCompModel.MaterialPropertyValues
            End If

            If Me.ComboBox1.SelectedIndex = 0 Then
                vMatProp(0) = 1
                vMatProp(1) = 0
                vMatProp(2) = 0

            ElseIf Me.ComboBox1.SelectedIndex = 1 Then
                vMatProp(0) = 0
                vMatProp(1) = 1
                vMatProp(2) = 0

            Else
                vMatProp(0) = 0
                vMatProp(1) = 0
                vMatProp(2) = 1
            End If
            swComp.MaterialPropertyValues = vMatProp

            ' Deselect component to see new color
            swModel.ClearSelection2(True)
        End If


        'Part = swApp.OpenDoc6("C:\Stanley\Final\Spanner.SLDASM", SwConst.swDocumentTypes_e.swDocASSEMBLY, SwConst.swOpenDocOptions_e.swOpenDocOptions_Silent, "", errors, warnings)
        Part = swApp.OpenDoc6(Directory, SwConst.swDocumentTypes_e.swDocASSEMBLY, SwConst.swOpenDocOptions_e.swOpenDocOptions_Silent, "", errors, warnings)
        Dim myDimension As Object
        If TrackBar1.Value.ToString() = 0 Then
            myDimension = Part.Parameter("D1@LimitDistance1")
            myDimension.SystemValue = 0.001

        ElseIf TrackBar1.Value.ToString() = 2 Then
            myDimension = Part.Parameter("D1@LimitDistance1")
            myDimension.SystemValue = 0.03479

        Else
            myDimension = Part.Parameter("D1@LimitDistance1")
            myDimension.SystemValue = 0.014

        End If


        Boolst = Part.EditRebuild3()
        Part.ClearSelection2(True)
        'Longi = swModel.SaveAs3("C:\Stanley\Final\Output\Stanley_Output\GeneratedModel.SLDASM", 0, 2)
        Longi = swModel.SaveAs3(Directory1 + "\GeneratedModel.SLDASM", 0, 2)
    End Sub

End Class