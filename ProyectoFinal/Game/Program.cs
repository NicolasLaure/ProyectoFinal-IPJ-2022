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

        StateController stateController = new StateController(renderWindow);
        //MainMenuState mainMenu = new MainMenuState(renderWindow);
        //GameLoop game = new GameLoop(renderWindow);
        //mainMenu.Play();
        //game.Play();
    }
}

