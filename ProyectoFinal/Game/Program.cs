using System;
using SFML.System;
using SFML.Graphics;
using SFML.Window;
class Program
{
    static void Main()
    {
        VideoMode videoMode = new VideoMode(1280, 720);
        string title = "Asteroides";
        RenderWindow renderWindow = new RenderWindow(videoMode, title);

        GameLoop game = new GameLoop(renderWindow);
        game.Play();
    }
}

