Public Class Background

    Public image As Image

    Public width As Integer = 1
    Public height As Integer = 50
    Public quantity As Integer = 1

    Public dust As New List(Of Dust)

    Public Sub New(backgroundImage As Image, random As Random, display As VBGame)
        image = backgroundImage
    End Sub

    Private Sub addDust(random As Random, display As VBGame)
        dust.Add(New Dust(random.Next(0, display.width), -height, width, height))

    End Sub

    Public Sub handle(random As Random, display As VBGame)
        For x As Integer = 1 To quantity
            addDust(random, display)
        Next

        display.blitUnscaled(image, New Point(0, 0))

        For Each partical As Dust In dust.ToList()
            If partical.y > display.height Then
                dust.Remove(partical)
            Else
                'partical = New Rectangle(partical.X, partical.Y + 25, partical.Width, partical.Height)
                partical.y += 10
                display.drawRect(partical.toRect(), Color.FromArgb(50, 255, 255, 255))
            End If
        Next

    End Sub

End Class

Public Class Dust
    Public x As Integer
    Public y As Integer
    Public width As Integer
    Public height As Integer
    Public Sub New(rx As Integer, ry As Integer, rwidth As Integer, rheight As Integer)
        x = rx
        y = ry
        width = rwidth
        height = rheight
    End Sub
    Public Function toRect() As Rectangle
        Return New Rectangle(x, y, width, height)
    End Function
End Class