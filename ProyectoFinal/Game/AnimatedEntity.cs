using System;
using System.Collections.Generic;
using SFML.System;
using SFML.Graphics;

class AnimatedEntity : Entity
{
    private Dictionary<string, AnimationData> animations = new Dictionary<string, AnimationData>();
    private Vector2i frameSize;
    private Vector2i imagePosition;
    private string currentAnimationName;
    private float currentFrameTime;
    private float animationTimer;

    public AnimatedEntity(string imageFilePath, Vector2i frameSize) : base(imageFilePath)
    {
        this.frameSize = frameSize;
        graphic.TextureRect = new IntRect()
        {
            Left = 0,
            Top = 0,
            Width = frameSize.X,
            Height = frameSize.Y
        };
    }

    protected void AddAnimation(string animationName, AnimationData animationData)
    {
        if (animations.ContainsKey(animationName))
        {
            Console.WriteLine($"The {animationName} animation was already added");
            return;
        }
        animations.Add(animationName, animationData);
    }
    protected void RemoveAnimation(string animationName, AnimationData animationData)
    {
        if (!animations.ContainsKey(animationName))
        {
            Console.WriteLine($"The {animationName} animation was not found");
            return;
        }
        animations.Remove(animationName);
    }
    protected void SetCurrentAnimation(string animationName)
    {
        if (!animations.ContainsKey(animationName))
        {
            Console.WriteLine($"The {animationName} animation was not found ");
            return;
        }
        if(animationName != currentAnimationName)
        {
            currentAnimationName = animationName;
            currentFrameTime = 1f / animations[currentAnimationName].frameRate;

            imagePosition = new Vector2i(0, animations[currentAnimationName].rowIndex);
        }
    }

    public virtual void Update(float deltaTime)
    {
        if (currentAnimationName == null)
            return;

        animationTimer += deltaTime;

        if(animationTimer >= currentFrameTime)
        {
            animationTimer -= currentFrameTime;

            if (imagePosition.X < animations[currentAnimationName].columnsCount - 1)
                imagePosition.X++;
            else if (animations[currentAnimationName].isLoopeable)
                imagePosition.X = 0;

            graphic.TextureRect = new IntRect()
            {
                Left = imagePosition.X * frameSize.X,
                Top = imagePosition.Y * frameSize.Y,
                Width = frameSize.X,
                Height = frameSize.Y
            };
        }
    }
}
