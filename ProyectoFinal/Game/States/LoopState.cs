using System;
using SFML.System;
using SFML.Graphics;
public abstract class LoopState
{
    protected RenderWindow window;
    private bool isRunning;
    protected bool isPaused = false;

    public event Action OnPause;
    public event Action OnGameOverPause;
    public LoopState(RenderWindow window)
    {
        this.window = window;
        isRunning = false;
    }
    protected virtual void Start()
    {
        window.Closed += OnWindowClose;
    }
    public void OnWindowClose(object sender, EventArgs eventArgs)
    {
        window.Close();
    }
    private void ProcessInputs()
    {
        window.DispatchEvents();
    }
    protected abstract void Update(float deltaTime);

    protected abstract void Draw();

    protected virtual void Finish()
    {
        window.Closed -= OnWindowClose;
    }
    public void Play()
    {
        Clock clock = new Clock();

        isRunning = true;
        isPaused = false;
        Start();

        while (isRunning)
        {
            Time deltaTime = clock.Restart();
            ProcessInputs();

            if (!isPaused)
                Update(deltaTime.AsSeconds());

            window.Clear();
            Draw();
            window.Display();
        }
    }
    public virtual void Stop()
    {
        if (!isRunning)
        {
            Console.WriteLine("Can't stop what is already stopped");
            return;
        }
        isRunning = false;

        Finish();
    }

    public void Pause()
    {
        OnPause?.Invoke();
        isPaused = true;
    }
    public void GameOver()
    {
        OnGameOverPause?.Invoke();
        isPaused = true;
    }
    public void UnPause()
    {
        isPaused = false;
    }
}
