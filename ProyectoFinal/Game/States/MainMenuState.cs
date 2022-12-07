using System;
using SFML.System;
using SFML.Graphics;
class MainMenuState : LoopState
{
    Font titleFont;
    Text titleText;
    Entity backGround;

    Text creditsTitle;
    Text fontCreditsText;
    Text musicCreditsText;
    Text explosionSfxCreditsText;
    Text blasterSfxCreditsText;
    Text engineSfxCreditsText;
    Text woobSfxCreditsText;

    public event Action OnPlayPressed;
    public event Action OnQuitPressed;

    // font zenDots by yoshimichi Ohira https://fonts.google.com/specimen/Zen+Dots/about?preview.text=0123456789&preview.text_type=custom
    // music by RomarioGrande https://freesound.org/people/Romariogrande/sounds/396231/
    // asteroidExplosionSfx by derplayer https://freesound.org/people/derplayer/sounds/587196/
    // blaster shoot by newlocknew https://freesound.org/people/newlocknew/sounds/514033/
    // spaceship_Woob_woob by Ideacraft https://freesound.org/people/Ideacraft/sounds/345110/
    // Afterburner sound by TiesWijnen https://freesound.org/people/TiesWijnen/sounds/413312/

    private Button playButton;
    private Button creditsButton;
    private Button returnButton;
    private Button quitButton;

    bool credits = false;
    public MainMenuState(RenderWindow window) : base(window)
    {

    }
    protected override void Start()
    {
        base.Start();

        string font = "Assets/Fonts/ZenDots-Regular.ttf";
        titleFont = new Font(font);
        titleText = new Text("Asteroids", titleFont, 100);

        creditsTitle = new Text("Credits", titleFont, 80);

        fontCreditsText = new Text("Font zenDots by yoshimichi Ohira \n https://fonts.google.com/specimen/Zen+Dots/ \n about?preview.text=0123456789&preview.text_type=custom", titleFont, 20);
        fontCreditsText.Position = new Vector2f(40, window.Size.Y / 2 - fontCreditsText.GetGlobalBounds().Height / 2 - 200);
        musicCreditsText = new Text("music by RomarioGrande \n https://freesound.org/people/Romariogrande/sounds/396231/", titleFont, 20);
        musicCreditsText.Position = new Vector2f(40, window.Size.Y / 2 - fontCreditsText.GetGlobalBounds().Height / 2 - 100);

        explosionSfxCreditsText = new Text("ExplosionSfx by derplayer \n https://freesound.org/people/derplayer/sounds/587196/", titleFont, 20);
        explosionSfxCreditsText.Position = new Vector2f(40, window.Size.Y / 2 - fontCreditsText.GetGlobalBounds().Height / 2);
        
        blasterSfxCreditsText = new Text("blasterSfx by newlocknew \n https://freesound.org/people/newlocknew/sounds/514033/", titleFont, 20);
        blasterSfxCreditsText.Position = new Vector2f(40, window.Size.Y / 2 - fontCreditsText.GetGlobalBounds().Height / 2 + 100);
        
        engineSfxCreditsText = new Text("spaceship_Woob_woob by Ideacraft \n https://freesound.org/people/Ideacraft/sounds/345110/", titleFont, 20);
        engineSfxCreditsText.Position = new Vector2f(40, window.Size.Y / 2 - fontCreditsText.GetGlobalBounds().Height / 2 + 200);
        
        woobSfxCreditsText = new Text("Afterburner sound by TiesWijnen \n https://freesound.org/people/TiesWijnen/sounds/413312/", titleFont, 20);
        woobSfxCreditsText.Position = new Vector2f(40, window.Size.Y / 2 - fontCreditsText.GetGlobalBounds().Height / 2 + 300);

        backGround = new Entity("Assets/Sprites/SpaceBackground.png", "BackGround");

        FloatRect titleBounds = titleText.GetGlobalBounds();
        titleText.Position = new Vector2f(window.Size.X / 2 - titleBounds.Width / 2, 0);

        playButton = new Button(window, font, "Play");
        FloatRect buttonsRect = playButton.GetBackGroundRect();
        playButton.SetPosition(new Vector2f(window.Size.X / 2, window.Size.Y / 2 - 40));

        creditsButton = new Button(window, font, "Credits");
        creditsButton.SetPosition(new Vector2f(window.Size.X / 2, window.Size.Y / 2 + 70));

        quitButton = new Button(window, font, "Quit");
        quitButton.SetPosition(new Vector2f(window.Size.X / 2, window.Size.Y / 2 + 180));

        returnButton = new Button(window, font, "Return");
        returnButton.SetPosition(new Vector2f(window.Size.X - buttonsRect.Width / 2, buttonsRect.Height / 2));

        playButton.OnClicked += OnPressPlay;
        creditsButton.OnClicked += OnPressCredits;
        returnButton.OnClicked += OnPressReturn;
        quitButton.OnClicked += OnPressQuit;
    }
    protected override void Update(float deltaTime)
    {

    }
    public override void Stop()
    {
        playButton.OnClicked -= OnPressPlay;
        creditsButton.OnClicked -= OnPressCredits;
        returnButton.OnClicked -= OnPressReturn;
        quitButton.OnClicked -= OnPressQuit;
    }
    private void OnPressPlay() => OnPlayPressed?.Invoke();
    private void OnPressQuit() => OnQuitPressed?.Invoke();
    private void OnPressCredits()
    {
        credits = true;
    }
    private void OnPressReturn()
    {
        credits = false;
    }

    protected override void Draw()
    {
        if (!credits)
        {
            window.Draw(backGround.Graphic);
            playButton.Draw();
            quitButton.Draw();
            creditsButton.Draw();
            window.Draw(titleText);
        }
        else
        {
            window.Draw(backGround.Graphic);
            returnButton.Draw();

            window.Draw(creditsTitle);
            window.Draw(fontCreditsText);
            window.Draw(musicCreditsText);
            window.Draw(engineSfxCreditsText);
            window.Draw(blasterSfxCreditsText);
            window.Draw(woobSfxCreditsText);
            window.Draw(explosionSfxCreditsText);
            
        }
    }
    protected override void Finish()
    {
        playButton.OnClicked -= OnPressPlay;
        creditsButton.OnClicked -= OnPressCredits;
        returnButton.OnClicked -= OnPressReturn;
        quitButton.OnClicked -= OnPressQuit;

        base.Finish();
    }
}
