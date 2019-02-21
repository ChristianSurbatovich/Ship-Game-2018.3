using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShipGame
{
    public interface IGameAction
    {

        void Use();
        void OnLearn();
        void OnUnlearn();

    }
}

