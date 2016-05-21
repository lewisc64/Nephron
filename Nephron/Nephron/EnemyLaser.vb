Public Class EnemyLaser
    Inherits Sprite

    Public opacity As Integer
    Public active As Boolean
    Public timer As New Stopwatch
    Public waitTime As Integer
    Public activeTime As Integer

    Public Sub reset(random As Random, display As VBGame)
        x = random.Next(0, display.width - width)
        timer.Restart()
        opacity = 0
        active = False
    End Sub

    Public Sub New(random As Random, display As VBGame)

        width = 5
        height = display.height

        color = VBGame.cyan

        activeTime = 100
        waitTime = 10000

        reset(random, display)
    End Sub

    Public Function handle(random As Random, display As VBGame)
        If timer.ElapsedMilliseconds >= waitTime Then
            If timer.ElapsedMilliseconds < waitTime + activeTime And Not active Then
                opacity = 255
                Assets.sounds("EpicLaser").play()
                active = True
                Return True
            ElseIf timer.ElapsedMilliseconds > waitTime + activeTime Then
                reset(random, display)
            End If
        ElseIf timer.ElapsedMilliseconds > waitTime - (activeTime * 20) Then
            opacity = 20
        End If
        Return False
    End Function

    Public Function collides(playerShip As PlayerShip) As Boolean
        If active Then
            Return VBGame.collideRect(playerShip.getRect(), getRect())
        Else
            Return False
        End If
    End Function

    Public Sub draw(display As VBGame)
        If active Then
            display.fill(color.FromArgb(100, color.R, color.G, color.B))
        End If
        display.drawRect(getRect(), color.FromArgb(opacity, color.R, color.G, color.B))
    End Sub

End Class
