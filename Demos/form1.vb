Imports System.Windows.Forms

Public Class Form1
    Inherits Form

    Private Button1 As New Button()

    Public Sub New()
        ' إعدادات الزر
        Button1.Text = "Click Me"
        Button1.Location = New Drawing.Point(50, 50)
        AddHandler Button1.Click, AddressOf Button1_Click

        ' إضافة الزر إلى النموذج
        Controls.Add(Button1)

        ' إعدادات النموذج
        Me.Text = "My VB Form"
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.Size = New Drawing.Size(300, 200)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        MessageBox.Show("Hello, World!")
    End Sub
End Class
