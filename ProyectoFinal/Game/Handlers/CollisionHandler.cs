using System;
using System.Collections.Generic;
using System.Text;
using SFML.System;
using SFML.Graphics;
using SFML.Window;
public static class CollisionsHandler
{

    private static readonly List<Entity> entities = new List<Entity>();

    const float asteroidPoints = 10;
    const float smallAsteroidPoints = 15;

    private static void SolveCollision(Entity first, Entity second)
    {
        if (first is Player || second is Player)
        {
            if (first is Asteroid)
                Player.PlayerOnCollision(second as Player);
            else if (second is Asteroid)
                Player.PlayerOnCollision(first as Player);
        }

        if (first is Asteroid || second is Asteroid)
        {
            Asteroid asteroid;
            Bullet bullet;
            if (first is Bullet)
            {
                asteroid = second as Asteroid;
                bullet = first as Bullet;
                if (!asteroid.isSon)
                {
                    asteroid.Split();
                    bullet.GetPlayer().AddScore(asteroidPoints);
                }
                else
                {
                    asteroid.Disable();
                    bullet.GetPlayer().AddScore(asteroidPoints);
                }
            }
            else if (second is Bullet)
            {
                asteroid = first as Asteroid;
                bullet = second as Bullet;
                if (!asteroid.isSon)
                {
                    asteroid.Split();
                    bullet.GetPlayer().AddScore(asteroidPoints);
                }
                else
                {
                    asteroid.Disable();
                    bullet.GetPlayer().AddScore(asteroidPoints);
                }
            }

        }
    }

    public static void AddEntity(Entity entity)
    {
        if (entities.Contains(entity))
        {
            return;
        }

        entities.Add(entity);
    }

    public static void RemoveEntity(Entity entity)
    {
        if (!entities.Contains(entity))
        {
            return;
        }
        entities.Remove(entity);
    }

    public static void Update()
    {
        for (int i = 0; i < entities.Count; i++)
        {
            for (int j = i + 1; j < entities.Count; j++)
            {
                if (entities[i].IsColliding(entities[j]))
                    SolveCollision(entities[i], entities[j]);
            }
        }
    }
}
