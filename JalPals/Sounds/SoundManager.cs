using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;


/* This class acts as an accessable list of all the sound effects in the game. 
 * If you want something to use the sound effect, you have to create a SoundManager item, then use 
 * the Play() method to play the sound effect.
 * 
 * You can also choose to add sound effects on the fly with the add method.
 */

namespace JalPals.Sounds
{
    public class SoundManager
    {
        //This is the dictionary that will keep track of the string and the sound effect that it's associated with.
        public Dictionary<string, Sound> mySounds;

        public SoundManager(Dictionary<string, Sound> sounds, ContentManager content)
        {

            // These are all the sounds that are in the game. We initialize them all directly. 
            Sound sound1 = new Sound("LOZ_Arrow_Boomerang", content);
            Sound sound2 = new Sound("LOZ_Bomb_Blow", content); //Bombs can't be used yet.
            Sound sound3 = new Sound("LOZ_Boss_Hit", content); //Bosses don't have a hit box.
            Sound sound4 = new Sound("LOZ_Boss_Scream1", content); //The Aguamantis doesn't have acces too the game object. This needs to be changed.
            Sound sound5 = new Sound("LOZ_Boss_Scream2", content);
            Sound sound6 = new Sound("LOZ_Boss_Scream3", content);
            Sound sound7 = new Sound("LOZ_Candle", content); //Candle... I don't think will be in our implementation.
            Sound sound8 = new Sound("LOZ_Door_Unlock", content); //Doors cannot unlock yet.
            Sound sound9 = new Sound("LOZ_Enemy_Die", content); //Enemies cannot die yet.
            Sound sound10 = new Sound("LOZ_Enemy_Hit", content); //Enemies have no collision detection.
            Sound sound11 = new Sound("LOZ_Fanfare", content); //Items are using this, but don't have collision.
            Sound sound12 = new Sound("LOZ_Get_Heart", content);
            Sound sound13 = new Sound("LOZ_Get_Item", content);
            Sound sound14 = new Sound("LOZ_Get_Rupee", content);
            Sound sound15 = new Sound("LOZ_Key_Appear", content); //Keys don't spawn in yet.
            Sound sound16 = new Sound("LOZ_Link_Die", content); //Link can't die yet.
            Sound sound17 = new Sound("LOZ_Link_Hurt", content);
            Sound sound18 = new Sound("LOZ_LowHealth", content);
            Sound sound19 = new Sound("LOZ_MagicalRod", content);
            Sound sound20 = new Sound("LOZ_Recorder", content); //I'm not exactly sure when this ones plays, but I don't think we need to use it.
            Sound sound21 = new Sound("LOZ_Refill_Loop", content); //This is... not even a sound... idk
            Sound sound22 = new Sound("LOZ_Secret", content); //This needs to be implemented elsewhere.
            Sound sound23 = new Sound("LOZ_Shield", content);
            Sound sound24 = new Sound("LOZ_Shore", content);
            Sound sound25 = new Sound("LOZ_Stairs", content); //We don't currently have stairs
            Sound sound26 = new Sound("LOZ_Sword_Combined", content);
            Sound sound27 = new Sound("LOZ_Sword_Shoot", content);
            Sound sound28 = new Sound("LOZ_Sword_Slash", content);
            Sound sound29 = new Sound("LOZ_Text", content);
            Sound sound30 = new Sound("LOZ_Text_Slow", content);

            //The dictionary is filled with all of it's values.
            mySounds = new Dictionary<string, Sound>()
            {
                { "Arrow_Boomerang", sound1},
                { "Bomb_Blow", sound2},
                { "Boss_Hit" ,sound3},
                { "Boss_Scream1" ,sound4},
                { "Boss_Scream2" ,sound5},
                { "Boss_Scream3" ,sound6},
                { "Candle" ,sound7},
                { "Door_Unlock" ,sound8},
                { "Enemy_Die" ,sound9},
                { "Enemy_Hit" ,sound10},
                { "Fanfare" ,sound11},
                { "Get_Heart" ,sound12},
                { "Get_Item" ,sound13},
                { "Get_Rupee" ,sound14},
                { "Key_Appear" ,sound15},
                { "Link_Die" ,sound16},
                { "Link_Hurt" ,sound17},
                { "LowHealth" ,sound18},
                { "MagicalRod" ,sound19},
                { "Recorder" ,sound20},
                { "Refill_Loop" ,sound21},
                { "Secret" ,sound22},
                { "Shield" ,sound23},
                { "Shore" ,sound24},
                { "Stairs" ,sound25},
                { "Sword_Combined" ,sound26},
                { "Sword_Shoot" ,sound27},
                { "Sword_Slash" ,sound28},
                { "Text" ,sound29},
                { "Text_Slow" ,sound30 }
            };

        }

        //Add is used to add a sound effect to the list.
        public void add(Sound sound)
        {
            mySounds.Add(sound.name, sound);
        }
        public void remove(Sound sound)
        {
            mySounds.Remove(sound.name);
        }

        //Play() will play a sound item that is linked to the soundName string.
        public void Play(string soundName)
        {
            if (mySounds.TryGetValue(soundName, out Sound sound))
            {
                sound.Play();
            }
        }
    }
}
