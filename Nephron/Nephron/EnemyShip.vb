Public Class EnemyShip
    Inherits EnemyShipBase

    Public Sub New(random As Random, display As VBGame)
        speed = random.Next(minSpeed, maxSpeed + 1)
        pyc = speed
        animations.addAnim("fly", New Animation(Assets.images("EnemyShip"), New Size(1, 3), 64))
        animations.playActive()

        width = 26
        height = 42

        reset(random, display)

    End Sub

End Class
