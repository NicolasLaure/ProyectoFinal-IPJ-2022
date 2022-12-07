using SFML.System;
using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
public class Player : AnimatedEntity
{
    int hp = 3;
    float speed;
    RenderWindow window;
    List<Bullet> bullets = new List<Bullet>();
    float shootCD = 1f;
    float shootTimer;
    private const string Idle = "Idle";
    private const string Burn = "Burn";

    static bool canTakeDamage = true;
    float damageTimer = 0;
    float damageCD = 1.5f;

    private float score;

    public event Action OnGameOver;

    public Player(string imageFilePath, Vector2i size, float playerSpeed, RenderWindow window) : base(imageFilePath, size)
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
        shootTimer = shootCD;
        string bulletImageFilePath = "Assets/Sprites/SpaceBullet.png";
        Vector2i bulletSize = new Vector2i(64, 64);

        for (int i = 0; i < 50; i++)
        {
            bullets.Add(new Bullet(bulletImageFilePath, bulletSize, Graphic.Position, this));
        }
    }

    public List<Bullet> GetActiveBullets()
    {
        return bullets.FindAll(b => b.IsEnabled);
    }
    public void TakeDamage()
    {
        hp--;
    }
    public void AddScore(float points)
    {
        score += points;
    }
    public int GetScore()
    {
        return (int)score;
    }
    void GameOver()
    {
        OnGameOver?.Invoke();
    }
    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);


        score += deltaTime * 1.35f;

        Hud.SetLivesText(hp);
        //playerPos += new Vector2f(1 * speed * deltaTime, 0);
        Vector2f mousePos = (Vector2f)Mouse.GetPosition(window);
        Vector2f dir = this.position - mousePos;
        float angle2 = MathF.Atan2(dir.Y, dir.X);
        float angle = MathF.Atan2(dir.Y, dir.X) * 180 / MathF.PI - 90f;
        Graphic.Rotation = angle;

        if (Mouse.IsButtonPressed(Mouse.Button.Right))
        {
            float x = MathF.Cos(angle2);
            float y = MathF.Sin(angle2);
            Translate(new Vector2f(x * speed * -1, y * speed * -1) * deltaTime);

            //Vector2f forward = new Vector2f(VectorUtility.Up.X * x - VectorUtility.Up.y * y, 
            //                                VectorUtility.Up.X * y - VectorUtility.Up.y * x);
            SetCurrentAnimation(Burn);
        }
        else
            SetCurrentAnimation(Idle);


        shootTimer += deltaTime;

        if (Mouse.IsButtonPressed(Mouse.Button.Left) && shootTimer >= shootCD)
        {
            Bullet bullet = bullets.Find(b => !b.IsEnabled);
            if (bullet != null)
            {
                bullet.Shoot(Graphic.Position, Graphic.Rotation, angle2);
            }
            shootTimer = 0;
        }

        if (hp == 0)
            GameOver();

        if (!canTakeDamage)
            damageTimer += deltaTime;

        if(damageTimer >= damageCD)
        {
            damageTimer = 0;
            canTakeDamage = true;
        }

    }
    public static void PlayerOnCollision(Player player)
    {
        if (canTakeDamage)
        {
            player.TakeDamage();
            canTakeDamage = false;
        }
    }
}

