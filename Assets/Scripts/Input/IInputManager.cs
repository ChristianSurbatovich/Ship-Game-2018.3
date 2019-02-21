using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ShipGame
{
    public interface IInputManager
    {
        void Register(IKeyHandler menu, InputFocus focusType);

        void Unregister(IKeyHandler menu, InputFocus focusType);

        void ChangeFocus(IKeyHandler menu, InputFocus focusType);
    }
}

