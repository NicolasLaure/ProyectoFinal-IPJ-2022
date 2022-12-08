using System;
using System.Collections.Generic;
using System.Text;
using SFML.Graphics;
class StateController : IDisposable
{
    private RenderWindow window;
    private MainMenuState mainMenu;
    private GameLoop gameLoop;

    public StateController(RenderWindow window)
    {
        this.window = window;
        mainMenu = new MainMenuState(window);
        gameLoop = new GameLoop(window);

        gameLoop.OnMenuCall += OnPressMenu;
        gameLoop.OnLevelReset += OnRestart;
        mainMenu.OnPlayPressed += OnPressPlay;
        mainMenu.OnQuitPressed += OnPressQuit;
        mainMenu.Play();


        

    }

    public void Dispose()
    {
        mainMenu.OnPlayPressed -= OnPressPlay;
        mainMenu.OnQuitPressed -= OnPressQuit;
    }

    private void OnRestart()
    {
        gameLoop.Stop();
        gameLoop.Play();
    }
    private void OnPressPlay()
    {
        mainMenu.Stop();
        gameLoop.Play();
    }

    private void OnPressMenu()
    {
        gameLoop.Stop();
        mainMenu.music.Stop();
        mainMenu.Play();
    }
    private void OnPressQuit()
    {
        window.Close();
        System.Environment.Exit(1);
    }
}
