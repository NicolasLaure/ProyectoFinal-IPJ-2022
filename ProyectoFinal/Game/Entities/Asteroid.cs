using System;
using System.Collections.Generic;
using SFML.System;
using SFML.Graphics;
using SFML.Window;
class Asteroid : AnimatedEntity
{

    Asteroid small1;
    Asteroid small2;
    Vector2f direction;
    Vector2f pos;
    Player player;
    float speed = 15;
    public bool isActive = false;
    public bool isTaken = false;
    public bool isSon = false;

    float timer;
    float spawnCd = 0.5f;
    public Asteroid(string fileNamePath, Vector2i size, Player player) : base(fileNamePath, size)
    {
        this.player = player;
    }

    public void Launch(Asteroid small1, Asteroid small2)
    {
        this.small1 = small1;
        this.small2 = small2;
        small1.isTaken = true;
        small2.isTaken = true;
        isActive = true;
        Random rand = new Random();
        int cardinalSpawn = rand.Next(0, 4);
        switch (cardinalSpawn)
        {
            case 0:
                pos = new Vector2f(rand.Next(-15, 0), rand.Next(0, 735)); //Izq
                break;
            case 1:
                pos = new Vector2f(rand.Next(0, 1095), rand.Next(-15, 0)); //arriba
                break;
            case 2:
                pos = new Vector2f(rand.Next(0, 1095), rand.Next(720, 735)); //abajo
                break;
            case 3:
                pos = new Vector2f(rand.Next(1080, 1095), rand.Next(0, 735)); //Izq
                break;
        }
        Graphic.Position = pos;

        //CollisionsHandler.AddEntity(this);


        Vector2f dir = this.position - player.position;
        float angle2 = MathF.Atan2(dir.Y, dir.X);

        float x = MathF.Cos(angle2);
        float y = MathF.Sin(angle2);
        direction = new Vector2f(x * speed * -1, y * speed * -1);

    }
    public void Split()
    {
        Disable();
        small1.isActive = true;
        small2.isActive = true;

        float deg2Rad = 35 * MathF.PI / 180;
        if (direction.X > 0 && direction.Y > 0)
        {
            small1.direction = new Vector2f(direction.X - speed * MathF.Sin(deg2Rad) * -1, direction.Y);
            small2.direction = new Vector2f(direction.X, direction.Y - speed * MathF.Sin(deg2Rad) * -1);
        }
        else if (direction.X > 0 && direction.Y < 0)
        {
            small1.direction = new Vector2f(direction.Y, direction.Y - speed * MathF.Sin(deg2Rad));
            small2.direction = new Vector2f(direction.X - speed * MathF.Sin(deg2Rad) * -1, direction.Y);
        }
        else if (direction.X < 0 && direction.Y > 0)
        {
            small1.direction = new Vector2f(direction.Y, direction.Y - speed * MathF.Sin(deg2Rad) * -1);
            small2.direction = new Vector2f(direction.X - speed * MathF.Sin(deg2Rad), direction.Y);
        }
        else
        {
            small1.direction = new Vector2f(direction.X - speed * MathF.Sin(deg2Rad), direction.Y);
            small2.direction = new Vector2f(direction.X, direction.Y - speed * MathF.Sin(deg2Rad));
        }

        small1.position = position;
        small2.position = position;

    }
    public void Disable()
    {
        if (!isSon)
        {
            isActive = false;
            small1.isTaken = false;
            small2.isTaken = false;
            CollisionsHandler.RemoveEntity(this);
        }
        else
        {
            isActive = false;
            CollisionsHandler.RemoveEntity(this);
        }
    }

    public override void Update(float deltaTime)
    {
        Translate(direction * speed * deltaTime);
        if (Graphic.Position.X < -10 || Graphic.Position.X > 1290)
            Disable();
        if (Graphic.Position.Y < -10 || Graphic.Position.Y > 730)
            Disable();

        timer += deltaTime;
        if (timer >= spawnCd)
        {
            CollisionsHandler.AddEntity(this);
            timer = 0;
        }
    }
}
