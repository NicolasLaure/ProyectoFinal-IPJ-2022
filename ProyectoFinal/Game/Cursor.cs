using SFML.System;
using SFML.Graphics;

class Cursor
{
    Vector2f cursorPos;
    public CircleShape graphic;

    public Cursor(Vector2f initialPos, Color cursorColor, float size, uint verticeCount)
    {
        graphic = new CircleShape(size / 2, verticeCount);
        graphic.Position = cursorPos;
        graphic.FillColor = cursorColor;
    }
    
}

