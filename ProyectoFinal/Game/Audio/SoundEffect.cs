using SFML.Audio;

public class SoundEffect
{
    private SoundBuffer soundBuffer;
    private Sound sound;
    public SoundEffect(string soundPath, bool isLoopable, float volume)
    {
        soundBuffer = new SoundBuffer(soundPath);
        sound = new Sound(soundBuffer);

        if (isLoopable)
            sound.Loop = true;

        sound.Volume = volume;
    }

    public void Play() => sound.Play();
    public void Stop() => sound.Stop();
    public SoundStatus IsActive() => sound.Status;

}
