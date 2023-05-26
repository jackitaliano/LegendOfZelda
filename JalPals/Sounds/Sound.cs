using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;


namespace JalPals.Sounds
{
    public class Sound
    {
        //The sound effect object that will be played.
        SoundEffect sound;
        //The name of the sound.
        public string name;
        //volume is the volume of the sound effect.
        public float volume = 0.3f;
        public Sound(string soundName, ContentManager content)
        {
            sound = content.Load<SoundEffect>(soundName);
            name = soundName;
        }

        //Play() sets the volume to a certain state, and 
        public void Play()
        {
            SoundEffect.MasterVolume = volume;
            sound.Play();
        }

        //setVolume() sets the voulme of the Sound object.
        public void setVolume(float num)
        {
            volume = num;
        }
    }
}
