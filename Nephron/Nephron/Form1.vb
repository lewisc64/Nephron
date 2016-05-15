Imports System.Threading

Public Class Form1

    Public display As New VBGame
    Public thread As New Thread(AddressOf mainloop)
    Public random As New Random

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        display.setDisplay(Me, New Size(800, 600), "Nephron", True)

        Assets.images.Add("bg1", VBGame.loadImage("bg.png"))
        Assets.images.Add("PlayerShip", VBGame.loadImage("ship_anim.png"))
        Assets.images.Add("EnemyShip", VBGame.loadImage("enemy1_anim.png"))
        Assets.images.Add("EnemyShip2", VBGame.loadImage("enemy2_anim.png"))
        thread.Start()
    End Sub

    Public Sub mainloop()

        Dim music As New Sound("Cephalopod.mp3")
        music.play(True)

        Dim start As New Button(display, "Start", New Rectangle(150, display.height - 150, 100, 50), "Arial", 12)
        start.setColor(Color.FromArgb(0, 128, 0), VBGame.green)
        start.setTextColor(VBGame.white, VBGame.white)

        Dim quit As New Button(display, "Quit", New Rectangle(display.width - 250, display.height - 150, 100, 50), "Arial", 12)
        quit.setColor(Color.FromArgb(128, 0, 0), VBGame.red)
        quit.setTextColor(VBGame.white, VBGame.white)

        Dim test As New EnemyWarship(random, display)

        While True

            For Each e As MouseEvent In display.getMouseEvents()
                If start.handle(e) = MouseEvent.buttons.left Then
                    gameloop()
                ElseIf quit.handle(e) = MouseEvent.buttons.left Then
                    End
                End If
            Next

            display.blitUnscaled(Assets.images("bg1"), New Point(0, 0))

            test.handle(random, display)
            test.draw(display)

            start.draw()
            quit.draw()

            display.drawCenteredText(display.getRect(), "Nephron", VBGame.white, New Font("Arial", 50))

            display.update()
            Console.WriteLine(display.getTime())
            display.clockTick(60)

        End While

    End Sub

    Public Function gameloop() As Boolean

        Dim ships As New List(Of Object)

        For x As Integer = 1 To 30
            ships.Add(New EnemyShip(random, display))
        Next

        For x As Integer = 1 To 4
            ships.Add(New EnemyWarship(random, display))
        Next

        While True

            display.blitUnscaled(Assets.images("bg1"), New Point(0, 0))

            For Each ship In ships

                ship.draw(display)
                ship.handle(random, display)

            Next

            display.update()
            Console.WriteLine(display.getTime())
            display.clockTick(60)

        End While

        Return False
    End Function

End Class
