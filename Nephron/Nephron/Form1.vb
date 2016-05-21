Imports System.Threading
Imports System.IO

Public Class Form1

    Public display As New VBGame
    Public thread As New Thread(AddressOf mainloop)
    Public random As New Random

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        display.setDisplay(Me, New Size(800, 600), "Nephron", True)

        Assets.images.Add("bg1", VBGame.loadImage("bg.png"))
        Assets.images.Add("PlayerShip", VBGame.loadImage("ship_anim.png"))
        Assets.images.Add("PlayerShipExplode", VBGame.loadImage("ship_anim_explode.png"))
        Assets.images.Add("EnemyShip", VBGame.loadImage("enemy1_anim.png"))
        Assets.images.Add("EnemyShip2", VBGame.loadImage("enemy2_anim.png"))

        Assets.sounds.Add("EpicLaser", New Sound("EpicLaser.mp3"))
        Assets.sounds.Add("Explosion", New Sound("Explosion.mp3"))

        thread.Start()
    End Sub

    Public Sub mainloop()

        Dim start As New Button(display, "START", New Rectangle(150, display.height - 150, 100, 50), "Arial", 12)
        start.setColor(Color.FromArgb(0, 128, 0), VBGame.green)
        start.setTextColor(VBGame.white, VBGame.white)

        Dim quit As New Button(display, "QUIT", New Rectangle(display.width - 250, display.height - 150, 100, 50), "Arial", 12)
        quit.setColor(Color.FromArgb(128, 0, 0), VBGame.red)
        quit.setTextColor(VBGame.white, VBGame.white)

        Dim test As New EnemyWarship(random, display)

        While True

            For Each e As MouseEvent In display.getMouseEvents()
                If start.handle(e) = MouseEvent.buttons.left Then
                    While gameloop()
                    End While
                ElseIf quit.handle(e) = MouseEvent.buttons.left Then
                    End
                End If
            Next

            display.blitUnscaled(Assets.images("bg1"), New Point(0, 0))

            test.handle(random, display)
            test.draw(display)

            start.draw()
            quit.draw()

            display.drawCenteredText(display.getRect(), "NEPHRON", VBGame.white, New Font("Arial", 50))

            drawHighscore()

            display.drawText(New Point(0, 20), "MUSIC: CEPHALOPOD BY KEVIN MACLEOD", VBGame.white, New Font("Arial", 8))

            display.update()
            Console.WriteLine(display.getTime())
            display.clockTick(60)

        End While

    End Sub

    Public Sub drawScore(score As Integer)
        display.drawText(New Point(0, 0), "SCORE: " & score, VBGame.white)
    End Sub

    Public Sub drawHighscore()
        display.drawText(New Point(0, 0), "HIGHSCORE: " & getHighscore(), VBGame.white)
    End Sub

    Public Function getHighscore() As Integer
        If Not File.Exists("highscore.dat") Then
            File.WriteAllLines("highscore.dat", {"0"})
        End If
        Return CInt(File.ReadLines("highscore.dat")(0))
    End Function

    Public Sub setHighscore(score As Integer)
        File.WriteAllLines("highscore.dat", {CStr(score)})
    End Sub

    Public Function gameloop() As Boolean

        Dim highscore As Integer = getHighscore()
        Dim highscoreSet As Boolean = False

        Dim deathTimer As New Stopwatch

        Dim music As New Sound("Cephalopod.mp3")
        music.play(True)

        Dim playerShip As New PlayerShip(display)
        Dim score As Integer = 0

        Dim ships As New List(Of Object)
        Dim background As New Background(Assets.images("bg1"), random, display)

        ships.Add(New EnemyShip(random, display))

        While True

            background.handle(random, display)

            For Each e As KeyEventArgs In display.getKeyDownEvents()
                playerShip.controls(e, True)
            Next

            For Each e As KeyEventArgs In display.getKeyUpEvents()
                playerShip.controls(e, False)
                If e.KeyCode = Keys.R Then
                    Return True
                ElseIf e.KeyCode = Keys.Escape Then
                    music.halt()
                    Return False
                End If
            Next

            playerShip.handleControls()
            playerShip.move()
            playerShip.keepInBounds(display.getRect())

            For Each ship In ships.ToList()

                ship.draw(display)

                If ship.handle(random, display) And deathTimer.ElapsedMilliseconds = 0 Then
                    score += 1

                    Select Case score
                        Case 1
                            For x = 1 To 20
                                ships.Add(New EnemyShip(random, display))
                            Next
                        Case 30
                            For x = 1 To 20
                                ships.Add(New EnemyShip(random, display))
                            Next
                        Case 200
                            ships.Add(New EnemyWarship(random, display))
                        Case 300
                            ships.Add(New EnemyWarship(random, display))
                        Case 500
                            For x = 1 To 5
                                ships.Add(New EnemyLaser(random, display))
                            Next
                    End Select

                End If

                If ship.collides(playerShip) And deathTimer.ElapsedMilliseconds = 0 Then
                    playerShip.animations.setActive("explode")
                    playerShip.animations.playActive()
                    deathTimer.Start()
                    music.halt()
                    Assets.sounds("Explosion").play()
                End If

            Next

            If deathTimer.ElapsedMilliseconds <> 0 Then
                display.drawCenteredText(display.getRect(), "GAME OVER", VBGame.white, New Font("Arial", 50))
                If deathTimer.ElapsedMilliseconds >= 4000 Then
                    display.drawCenteredText(New Rectangle(0, display.height / 2, display.width, display.height / 2), "PRESS 'R' TO RESTART" & vbCrLf & "PRESS 'ESC' TO QUIT", VBGame.white)
                    If highscore < score Then
                        display.drawCenteredText(New Rectangle(0, 0, display.width, display.height / 2), "SCORE: " & score & vbCrLf & "NEW HIGHSCORE", VBGame.white)
                        If Not highscoreSet Then
                            setHighscore(score)
                            highscoreSet = True
                        End If
                    Else
                        display.drawCenteredText(New Rectangle(0, 0, display.width, display.height / 2), "SCORE: " & score, VBGame.white)
                    End If

                End If
            End If

            playerShip.draw(display)

            If deathTimer.ElapsedMilliseconds = 0 Then
                drawScore(score)
            End If

            display.update()
            Console.WriteLine(display.getTime())
            display.clockTick(60)

        End While

        Return False
    End Function

End Class
