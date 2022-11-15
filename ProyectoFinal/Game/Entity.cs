using SFML.Graphics;
using SFML.System;

class Entity
{
    private Texture texture;
    private Sprite sprite;


    public Vector2f position { get => sprite.Position; set => sprite.Position = value; }
    public float rotation { get => sprite.Rotation; set => sprite.Rotation = value; }
    public Vector2f scale { get => sprite.Scale; set => sprite.Scale = value; }

    public Sprite graphic => sprite;

    public Entity(string imageFilePath)
    {
        texture = new Texture(imageFilePath);
        sprite = new Sprite(texture);
        sprite.Origin = new Vector2f(32, 32);
    }

    public void Translate(Vector2f movement) => position += movement;
    public void Rotate(float angle) => rotation += angle;
    public void Draw()
    {

    }
}

