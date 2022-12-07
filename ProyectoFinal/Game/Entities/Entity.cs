using SFML.Graphics;
using SFML.System;

public class Entity
{
    private Texture texture;
    private Sprite sprite;

    public Vector2f position { get => sprite.Position; set => sprite.Position = value; }
    public float rotation { get => sprite.Rotation; set => sprite.Rotation = value; }
    public Vector2f scale { get => sprite.Scale; set => sprite.Scale = value; }

    public Sprite Graphic => sprite;

    public Entity(string imageFilePath)
    {
        texture = new Texture(imageFilePath);
        sprite = new Sprite(texture);
        sprite.Origin = new Vector2f(32, 32);
    }

    public Entity(string imageFilePath, string name)
    {
        texture = new Texture(imageFilePath);
        sprite = new Sprite(texture);
    }
    public void Translate(Vector2f movement) => position += movement;
    public void Rotate(float angle) => rotation += angle;
    public void Draw()
    {

    }
    public bool IsColliding(Entity other)
    {
        FloatRect thisBounds = this.Graphic.GetGlobalBounds();
        thisBounds.Height = thisBounds.Height / 1.5f;
        thisBounds.Width = thisBounds.Width / 1.5f;
        FloatRect otherBounds = other.Graphic.GetGlobalBounds();
        otherBounds.Height = thisBounds.Height / 1.5f;
        otherBounds.Width = thisBounds.Width / 1.5f;

        return thisBounds.Intersects(otherBounds);
    }
}

