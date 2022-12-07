using System;
using System.Collections.Generic;
using SFML.System;
using SFML.Graphics;
using SFML.Window;
class AsteroidManager
{
    float spawnRate = 1f;
    float timer = 0f;
    List<Asteroid> asteroids = new List<Asteroid>();
    List<Asteroid> smallAsteroid1 = new List<Asteroid>();
    List<Asteroid> smallAsteroid2 = new List<Asteroid>();
    public AsteroidManager(int quantity, Player player)
    {
        Vector2i asteroidSize = new Vector2i(64, 64);
        string bigAsteroidImageFilePath = "Assets/Sprites/BigAsteroid.png";
        string smallAsteroidImageFilePath1 = "Assets/Sprites/SmallAsteroid1.png";
        string smallAsteroidImageFilePath2 = "Assets/Sprites/SmallAsteroid2.png";

        for (int i = 0; i < quantity; i++)
        {
            asteroids.Add(new Asteroid(bigAsteroidImageFilePath, asteroidSize, player));
            smallAsteroid1.Add(new Asteroid(smallAsteroidImageFilePath1, asteroidSize, player));
            smallAsteroid2.Add(new Asteroid(smallAsteroidImageFilePath2, asteroidSize, player));
        }

        foreach(Asteroid asteroid in smallAsteroid1)
        {
            asteroid.isSon = true;
        }
        foreach (Asteroid asteroid in smallAsteroid2)
        {
            asteroid.isSon = true;
        }
    }

    void spawnAsteroid()
    {
        Asteroid asteroid = asteroids.Find(b => !b.isActive);
        Asteroid small1 = smallAsteroid1.Find(b => !b.isActive && !b.isTaken);
        Asteroid small2 = smallAsteroid2.Find(b => !b.isActive && !b.isTaken);
        if (asteroid != null)
        {
            asteroid.Launch(small1, small2);
        }
    }
    public List<Asteroid> GetActiveAsteroids()
    {
        return asteroids.FindAll(a => a.isActive);
    }
    public List<Asteroid> GetActiveSmallAsteroids1()
    {
        return smallAsteroid1.FindAll(a => a.isActive);
    }
    public List<Asteroid> GetActiveSmallAsteroids2()
    {
        return smallAsteroid2.FindAll(a => a.isActive);
    }
   
    public void Update(float deltaTime)
    {
        timer += deltaTime;
        if (timer >= spawnRate)
        {
            spawnAsteroid();
            timer = 0;
        }
        List<Asteroid> small1 = smallAsteroid1.FindAll(b => !b.isActive && !b.isTaken);

        Console.WriteLine($"cantidad de small1 disponibles = {small1.Count}");
    }
}
