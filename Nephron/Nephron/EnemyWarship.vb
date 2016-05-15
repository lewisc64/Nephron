Public Class EnemyWarship
    Inherits EnemyShipBase

    Public Sub New(random As Random, display As VBGame)
        minSpeed = 1
        maxSpeed = 2
        speed = random.Next(minSpeed, maxSpeed + 1)
        animations.addAnim("fly", New Animation(Assets.images("EnemyShip2"), New Size(1, 77), 64))
        animations.playActive()

        width = 172
        height = 154

        reset(random, display)

    End Sub
End Class
