using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShipGame{
    public class InputFocusManager : MonoBehaviour, IInputManager{

        // input elements that stop other elements from receiving inputs
        private Stack<IKeyHandler> exclusiveFocus;
        // input elements that can receive inputs at the same time
        private List<IKeyHandler> sharedFocus;
        // input elements that always receive inputs
        private List<IKeyHandler> noFocus;

        private void Awake()
        {
            exclusiveFocus = new Stack<IKeyHandler>();
            sharedFocus = new List<IKeyHandler>();
            noFocus = new List<IKeyHandler>();
        }
        public void Register(IKeyHandler menu, InputFocus focusType)
        {
            Remove(menu);
            switch (focusType)
            {
                case InputFocus.EXCLUSIVE:
                    try
                    {
                        exclusiveFocus.Peek().SetFocus(false);
                    }catch(InvalidOperationException ie)
                    {

                    }

                    exclusiveFocus.Push(menu);
                    menu.SetFocus(true);
                    for (int i = 0; i < sharedFocus.Count; i++)
                    {
                        sharedFocus[i].SetFocus(false);
                    }
                    break;
                case InputFocus.SHARED:
                    sharedFocus.Add(menu);
                    if (exclusiveFocus.Count > 0)
                    {
                        menu.SetFocus(false);
                    }
                    else
                    {
                        menu.SetFocus(true);
                    }
                    break;
                case InputFocus.NONE:
                    break;
            }
        }

        public void Unregister(IKeyHandler menu, InputFocus focusType)
        {
            switch (focusType)
            {
                case InputFocus.EXCLUSIVE:
                    exclusiveFocus.Pop().SetFocus(false);
                    try
                    {
                        exclusiveFocus.Peek().SetFocus(true);
                    }
                    catch (InvalidOperationException)
                    {
                        for (int i = 0; i < sharedFocus.Count; i++)
                        {
                            sharedFocus[i].SetFocus(true);
                        }
                    }

                    break;
                case InputFocus.SHARED:
                    sharedFocus.Remove(menu);
                    break;
                case InputFocus.NONE:
                    noFocus.Remove(menu);
                    break;
            }
        }

        private void Remove(IKeyHandler menu)
        {
            sharedFocus.Remove(menu);
            noFocus.Remove(menu);
        }

        public void ChangeFocus(IKeyHandler menu, InputFocus focusType)
        {

        }
    }
}
