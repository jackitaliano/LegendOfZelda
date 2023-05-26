using System;

namespace JalPals.Player;

public interface ILinkHealth
{
    public int LinkHealthVal { get; set; }
    public void RemoveHealth();
    public void AddHealth();
    public void ResetHealth();

}

