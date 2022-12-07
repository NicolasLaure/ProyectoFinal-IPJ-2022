using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
class Button
{
    RenderWindow window;
    private Font font;
    private RectangleShape background;
    private Text text;

    FloatRect backGroundRect;

    public event Action OnClicked;
    public Button(RenderWindow window, string fontPath, string label)
    {
        this.window = window;
        font = new Font(fontPath);

        background = new RectangleShape(new Vector2f(250, 80));
        text = new Text(label, font);
        text.CharacterSize = 40;
        text.OutlineColor = Color.Black;
        text.OutlineThickness = 5;

        backGroundRect = background.GetGlobalBounds();
        background.Origin = new Vector2f(backGroundRect.Width / 2f, backGroundRect.Height / 2f);

        FloatRect textRect = text.GetGlobalBounds();
        text.Origin = new Vector2f(textRect.Width / 2f, textRect.Height / 2f);

        window.MouseButtonReleased += OnReleaseMouseButton;
    }
    ~Button()
    {
        window.MouseButtonReleased -= OnReleaseMouseButton;
    }

    private void OnReleaseMouseButton(object sender, MouseButtonEventArgs eventArgs)
    {
        FloatRect bounds = background.GetGlobalBounds();

        if (eventArgs.Button == Mouse.Button.Left && bounds.Contains(eventArgs.X, eventArgs.Y))
            OnClicked?.Invoke();
    }
    public void SetPosition(Vector2f position)
    {
        text.Position = position;
        background.Position = position;
    }
    public FloatRect GetBackGroundRect()
    {
        return backGroundRect;
    }
    public void Draw()
    {
        window.Draw(background);
        window.Draw(text);
    }
}
