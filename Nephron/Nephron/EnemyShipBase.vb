Public Class EnemyShipBase
    Inherits Sprite

    Public minSpeed = 4
    Public maxSpeed = 7

    Public waitFrames As Integer = 0

    Public Sub draw(display As VBGame)
        If waitFrames = 0 Then
            display.blit(animations.handle(), getRect())
        End If
    End Sub

    Public Sub reset(random As Random, display As VBGame)
        x = random.Next(0, display.width - width)
        y = -height
    End Sub

    Public Function handle(random As Random, display As VBGame) As Boolean

        If waitFrames = 0 Then
            y += speed
        Else
            waitFrames -= 1
        End If

        If y > display.height Then
            reset(random, display)
            waitFrames = random.Next(60, 600)
            Return True
        End If

        Return False
    End Function

End Class
