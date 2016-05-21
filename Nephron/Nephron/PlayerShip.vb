Public Class PlayerShip
    Inherits Sprite

    Public maxSpeed As Double = 4
    Public acceleration As Double = 0.2
    Public heldKeys As New List(Of Keys)

    Public Sub New(display As VBGame)

        animations.addAnim("fly", New Animation(Assets.images("PlayerShip"), New Size(1, 3), 64))
        animations.playActive()
        animations.addAnim("explode", New Animation(Assets.images("PlayerShipExplode"), New Size(1, 4), 128, 4, False, False))

        width = 26
        height = 42

        x = (display.width / 2) - (width / 2)
        y = ((2 / 3) * display.height) - (height / 2)

    End Sub

    Public Sub draw(display As VBGame)
        display.blit(animations.handle(), getRect())
    End Sub

    Public Sub controls(e As KeyEventArgs, keydown As Boolean)

        If keydown Then
            If Not heldKeys.Contains(e.KeyCode) Then
                heldKeys.Add(e.KeyCode)
            End If
        Else
            If heldKeys.Contains(e.KeyCode) Then
                heldKeys.Remove(e.KeyCode)
            End If
        End If
    End Sub

    Public Sub handleControls()
        If heldKeys.Contains(Keys.W) Then
            If nyc < maxSpeed Then
                nyc += acceleration / 2
            End If
        Else
            If nyc > 0 Then
                nyc -= acceleration / 2
            Else
                nyc = 0
            End If
        End If

        If heldKeys.Contains(Keys.S) Then
            If pyc < maxSpeed Then
                pyc += acceleration / 2
            End If
        Else
            If pyc > 0 Then
                pyc -= acceleration / 2
            Else
                pyc = 0
            End If
        End If

        If heldKeys.Contains(Keys.A) Then
            If nxc < maxSpeed Then
                nxc += acceleration
            End If
        Else
            If nxc > 0 Then
                nxc -= acceleration
            Else
                nxc = 0
            End If
        End If

        If heldKeys.Contains(Keys.D) Then
            If pxc < maxSpeed Then
                pxc += acceleration
            End If
        Else
            If pxc > 0 Then
                pxc -= acceleration
            Else
                pxc = 0
            End If
        End If
    End Sub

End Class
