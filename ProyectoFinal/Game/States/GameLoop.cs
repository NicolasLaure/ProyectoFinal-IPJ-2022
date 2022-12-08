using System;
using SFML.System;
using SFML.Graphics;
using SFML.Window;
class GameLoop : LoopState
{

    Entity backGround;
    Player player;
    Hud hud;
    AsteroidManager asteroidManager;

    const int MaxEnemies = 20;

    public event Action OnMenuCall;
    public event Action OnLevelReset;
    public GameLoop(RenderWindow window) : base(window) { }


    protected override void Start()
    {
        base.Start();

        string playerImageFilePath = "Assets/Sprites/Ship.png";
        string backgroundImageFilePath = "Assets/Sprites/SpaceBackground.png";
        string FontPath = "Assets/Fonts/ZenDots-Regular.ttf";

        backGround = new Entity(backgroundImageFilePath, "BackGround");

        Vector2f playerPos = new Vector2f(window.Size.X / 2, window.Size.Y / 2);
        Vector2i playerSize = new Vector2i(64, 64);
        float playerSpeed = 200f;
        player = new Player(playerImageFilePath, playerSize, playerSpeed, window);
        asteroidManager = new AsteroidManager(MaxEnemies, player);
        player.position = playerPos;

        hud = new Hud(window, FontPath, player);
        CollisionsHandler.AddEntity(player);
        OnPause += hud.OnPaused;
        OnGameOverPause += hud.OnGameOver;
        hud.OnUnpause += OnReturn;
        hud.OnResetPressed += OnRestart;

        player.OnGameOver += OnGameOverVoid;
        hud.OnMenuPressed += OnPressedMenu;
    }
    protected override void Update(float deltaTime)
    {
        hud.Update(deltaTime);
        player.Update(deltaTime);
        asteroidManager.Update(deltaTime);
        foreach (Bullet bullet in player.GetActiveBullets())
        {
            bullet.Update(deltaTime);
        }
        foreach (Asteroid asteroid in asteroidManager.GetActiveAsteroids())
        {
            asteroid.Update(deltaTime);
        }
        foreach (Asteroid asteroid in asteroidManager.GetActiveSmallAsteroids1())
        {
            asteroid.Update(deltaTime);
        }
        foreach (Asteroid asteroid in asteroidManager.GetActiveSmallAsteroids2())
        {
            asteroid.Update(deltaTime);
        }
        CollisionsHandler.Update();

        window.KeyReleased += OnKeyPressed;
    }

    private void OnGameOverVoid()
    {
        GameOver();
    }
    private void OnKeyPressed(object sender, KeyEventArgs args)
    {
        if (args.Code == Keyboard.Key.Escape)
        {
            if (!isPaused)
                Pause();
        }
    }
    private void OnPressedMenu() => OnMenuCall?.Invoke();
    private void OnRestart() => OnLevelReset?.Invoke();
    protected override void Draw()
    {


        window.Draw(backGround.Graphic);
        window.Draw(player.Graphic);
        foreach (Bullet bullet in player.GetActiveBullets())
        {
            window.Draw(bullet.Graphic);
        }
        foreach (Asteroid asteroid in asteroidManager.GetActiveAsteroids())
        {
            window.Draw(asteroid.Graphic);
        }
        foreach (Asteroid asteroid in asteroidManager.GetActiveSmallAsteroids1())
        {
            window.Draw(asteroid.Graphic);
        }
        foreach (Asteroid asteroid in asteroidManager.GetActiveSmallAsteroids2())
        {
            window.Draw(asteroid.Graphic);
        }
        hud.Draw();
    }

    public void OnReturn()
    {
        UnPause();
    }
    protected override void Finish()
    {
        base.Finish();
        CollisionsHandler.RemoveEntity(player);

    }
}
