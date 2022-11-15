using SFML.System;
using SFML.Graphics;
using SFML.Window;
using System;
class Player : AnimatedEntity
{
    float speed;
    Window window;

    private const string Idle = "Idle";
    private const string Burn = "Burn";

    public Player(string imageFilePath, Vector2i size, float playerSpeed, Window window) : base(imageFilePath, size)
    {
        speed = playerSpeed;
        this.window = window;

        AnimationData idle = new AnimationData()
        {
            frameRate = 25,
            rowIndex = 0,
            columnsCount = 1,
            isLoopeable = true
        };
        AnimationData burn = new AnimationData()
        {
            frameRate = 25,
            rowIndex = 1,
            columnsCount = 2,
            isLoopeable = true
        };
        AddAnimation(Idle, idle);
        AddAnimation(Burn, burn);

        SetCurrentAnimation(Idle);
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        //playerPos += new Vector2f(1 * speed * deltaTime, 0);

        Vector2f mousePos = (Vector2f)Mouse.GetPosition(window);
        Vector2f dir = this.position - mousePos;
        float angle = MathF.Atan2(dir.Y, dir.X) * /*rad2deg const*/ 57.29578f - 90f;
        graphic.Rotation = angle;

        if (Mouse.IsButtonPressed(Mouse.Button.Right))
        {
            float x = MathF.Cos(angle);
            float y = MathF.Sin(angle);
            Translate(new Vector2f(x * speed, y * speed) * deltaTime);
            SetCurrentAnimation(Burn);
        }
        else
            SetCurrentAnimation(Idle);
    }
}

