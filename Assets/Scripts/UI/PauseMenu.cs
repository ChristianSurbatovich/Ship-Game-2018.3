using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShipGame.UI
{
    public class PauseMenu : MonoBehaviour, IKeyHandler
    {
        public GameObject pauseMenu;
        private bool focus, menuOpen;
        private IInputManager input;

        private void Awake()
        {
            input = GameObject.FindGameObjectWithTag("Input").GetComponent(typeof(IInputManager)) as IInputManager;
        }

        private void Start()
        {
            input.Register(this, InputFocus.SHARED);
        }

        private void Update()
        {
            if (focus && Input.GetButtonDown("Cancel"))
            {
                if (menuOpen)
                {
                    Close();
                }
                else
                {
                    Open();
                }
            }
        }

        public void Quit()
        {
            Application.Quit();
        }

        public void Open()
        {
            menuOpen = true;
            pauseMenu.SetActive(true);
            input.Register(this, InputFocus.EXCLUSIVE);
        }

        public void Close()
        {
            menuOpen = false;
            pauseMenu.SetActive(false);
            input.Unregister(this, InputFocus.EXCLUSIVE);
            input.Register(this, InputFocus.SHARED);
        }

        public void SetFocus(bool newFocus)
        {
            focus = newFocus;
        }
    }

}
