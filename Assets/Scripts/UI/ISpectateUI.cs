using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShipGame.UI
{
    public interface ISpectateUI : IUIObject
    {
        void StartSpawnCounter(float t);

        void Spectate(string s);
    }

}
