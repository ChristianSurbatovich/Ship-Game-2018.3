using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShipGame
{
    public interface IUIObject : IKeyHandler
    {

        void Open();

        void Close();
    }
}

