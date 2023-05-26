using System;

namespace JalPals.Player
{
    public class LinkHealth : ILinkHealth
    {
        //LinkHealthVal is the number of hearts the player currently has left.
        public int LinkHealthVal { get; set; }

        private ILink link;

        public LinkHealth(ILink link)
        {
            this.link = link;
            LinkHealthVal = 12;
        }

        public void RemoveHealth()
        {
            LinkHealthVal--;
        }

        public void AddHealth()
        {
            LinkHealthVal++;
        }

        public void ResetHealth()
        {
            LinkHealthVal = 6;
        }
    }
}

