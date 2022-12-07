using System;
using SFML.System;
using SFML.Graphics;
using SFML.Window;

public class Bullet : AnimatedEntity
{
    float angle;
    float speed = 25;
    bool isEnabled;
    private Player player;
    public bool IsEnabled => isEnabled;
    public Bullet(string fileNamePath, Vector2i size, Vector2f position, Player player) : base(fileNamePath, size)
    {
        this.player = player;
        Graphic.Position = position;
        isEnabled = false;
    }

    public Player GetPlayer()
    {
        return player;
    }
    public void Shoot(Vector2f playerPos,  float playerRotation, float playerRotRadians)
    {
        CollisionsHandler.AddEntity(this);

        angle = playerRotRadians;
        Graphic.Rotation = playerRotation;
        Graphic.Position = playerPos;
        isEnabled = true;
    }
    public void Disable()
    {
        CollisionsHandler.RemoveEntity(this);
        isEnabled = false;
    }
    public override void Update(float deltaTime)
    {
        if (!isEnabled)
            return;
        float x = MathF.Cos(angle);
        float y = MathF.Sin(angle);
        Translate(new Vector2f(x * speed * -1, y * speed * -1) * speed * deltaTime);

        if (Graphic.Position.X < -10 || Graphic.Position.X > 1290)
            Disable();
        if (Graphic.Position.Y < -10 || Graphic.Position.Y > 730)
            Disable();
    }

}
