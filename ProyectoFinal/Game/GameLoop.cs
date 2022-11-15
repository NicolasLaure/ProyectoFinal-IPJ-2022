using System;
using SFML.System;
using SFML.Graphics;
using SFML.Window;
class GameLoop
{
    private RenderWindow window;
    Color windowColor = Color.White;
    private bool isRunning;

    private Color playerColor = Color.Red;
    Player player;
    public GameLoop(RenderWindow window)
    {
        this.window = window;
    }
    private void Start()
    {
        window.Closed += OnWindowClose;

        string playerImageFilePath = "Assets/Sprites/Ship.png";
        Vector2f playerPos = new Vector2f(window.Size.X / 2, window.Size.Y / 2);
        Vector2i playerSize = new Vector2i(64, 64);
        float playerSpeed = 200f;
        player = new Player(playerImageFilePath, playerSize, playerSpeed, window);
        player.position = playerPos;
    }
    public void OnWindowClose(object sender, EventArgs eventArgs)
    {
        window.Close();
    }
    private void ProcessInputs()
    {
        window.DispatchEvents();
    }
    private void Update(float deltaTime)
    {
        player.Update(deltaTime);
    }

    private void Draw()
    {
        window.Clear(windowColor);

        window.Draw(player.graphic);
        window.Display();
    }

    private void Finish()
    {
        window.Closed -= OnWindowClose;
    }
    public void Play()
    {
        Clock clock = new Clock();

        isRunning = true;

        Start();
        while (isRunning)
        {
            Time deltaTime = clock.Restart();
            ProcessInputs();
            Update(deltaTime.AsSeconds());
            Draw();
        }
        Finish();
    }
}
