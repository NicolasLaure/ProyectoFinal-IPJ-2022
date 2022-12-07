using SFML.System;
using SFML.Graphics;
using System;
public class Hud
{
    private RenderWindow window;
    Player player;

    private static Font font;

    Text scoreText;
    static Text livesText;

    bool isPaused;
    bool gameOver;
    private Text pauseTitle;
    private Text gameOverTitle;

    private Text gameOverScore;

    private Button UnpauseButton;
    private Button ResetButton;
    private Button MainMenuButton;
    private RectangleShape pausePanel;

    public event Action OnUnpause;
    public event Action OnMenuPressed;
    public event Action OnResetPressed;
    public Hud(RenderWindow window, string fontPath, Player player)
    {
        this.window = window;
        this.player = player;
        font = new Font(fontPath);
        scoreText = new Text($"Score: {player.GetScore()}", font);
        scoreText.FillColor = Color.White;
        scoreText.CharacterSize = 40;
        scoreText.OutlineColor = Color.Red;
        scoreText.OutlineThickness = 2;

        livesText = new Text("Lives: ", font);

        pauseTitle = new Text("Paused", font, 80);
        FloatRect titleBounds = pauseTitle.GetGlobalBounds();
        pauseTitle.Position = new Vector2f(window.Size.X / 2 - titleBounds.Width / 2, 0);
        gameOverTitle = new Text("GameOver", font, 80);
        FloatRect gameOverTitleRect = gameOverTitle.GetGlobalBounds();
        gameOverTitle.Position = new Vector2f(window.Size.X / 2 - gameOverTitleRect.Width / 2, 0);

        gameOverScore = new Text($"Your Score was: {player.GetScore()}", font, 40);
        FloatRect gameOverScoreRect = gameOverScore.GetGlobalBounds();
        gameOverScore.Position = new Vector2f(window.Size.X / 2 - gameOverScoreRect.Width / 2, window.Size.Y / 2 - 140);


        pausePanel = new RectangleShape(new Vector2f(window.Size.X, window.Size.Y));
        pausePanel.FillColor = new Color(0, 0, 0, 90);
        UnpauseButton = new Button(window, fontPath, "Return");
        FloatRect buttonsRect = UnpauseButton.GetBackGroundRect();
        UnpauseButton.SetPosition(new Vector2f(window.Size.X / 2, window.Size.Y / 2 - 40));
        OnUnpause += OnUnPaused;
        OnMenuPressed += MenuPressed;
        OnResetPressed += ResetPressed;
        MainMenuButton = new Button(window, fontPath, "Menu");
        MainMenuButton.SetPosition(new Vector2f(window.Size.X / 2, window.Size.Y / 2 + 150));

        ResetButton = new Button(window, fontPath, "Restart");
        ResetButton.SetPosition(new Vector2f(window.Size.X / 2, window.Size.Y / 2 + 50));


        gameOver = false;
    }


    public void Draw()
    {
        window.Draw(scoreText);
        window.Draw(livesText);


        if (isPaused)
        {
            window.Draw(pausePanel);
            window.Draw(pauseTitle);
            MainMenuButton.Draw();
            UnpauseButton.Draw();
        }

        if (gameOver)
        {
            gameOverScore.DisplayedString = $"Your Score was: {player.GetScore()}";
            window.Draw(pausePanel);
            window.Draw(gameOverTitle);
            window.Draw(gameOverScore);
            MainMenuButton.Draw();
            ResetButton.Draw();
        }
    }
    public void OnPaused()
    {
        UnpauseButton.OnClicked += OnUnpause;
        MainMenuButton.OnClicked += OnMenuPressed;
        isPaused = true;
    }
    public void OnGameOver()
    {
        ResetButton.OnClicked += OnResetPressed;
        MainMenuButton.OnClicked += OnMenuPressed;
        gameOver = true;
    }
    public void OnUnPaused()
    {
        UnpauseButton.OnClicked -= OnUnpause;
        MainMenuButton.OnClicked -= OnMenuPressed;
        isPaused = false;
    }
    private void MenuPressed()
    {
        ResetButton.OnClicked -= OnResetPressed;
        MainMenuButton.OnClicked -= OnMenuPressed;
    }
    void ResetPressed()
    {
        ResetButton.OnClicked -= OnResetPressed;
        MainMenuButton.OnClicked -= OnMenuPressed;
    }
    public static void SetLivesText(int hp)
    {
        livesText.DisplayedString = $"Lives: {hp}";
        livesText.FillColor = Color.White;
        livesText.CharacterSize = 40;
        livesText.OutlineColor = Color.Red;
        livesText.OutlineThickness = 2;
    }
    public void Update(float deltaTime)
    {
        scoreText.DisplayedString = $"Score: {player.GetScore()}";

        if (player.GetScore() <= 99)
            scoreText.Position = new Vector2f(1010, 0);
        else if (player.GetScore() <= 999)
            scoreText.Position = new Vector2f(980, 0);
        else
            scoreText.Position = new Vector2f(950, 0);
    }
}
