using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace ShipGame
{
    public class LevelSegmentManager : MonoBehaviour
    {
        public GameSettings settings;
        private Transform player;
        [SerializeField]
        private Vector3[] minBounds, maxBounds;
        [SerializeField]
        private string[] sceneNames;
        [SerializeField]
        private bool[] sceneLoaded;
        // Use this for initialization
        void Start()
        {
            settings.Subscribe(OnPlayerChanged, Settings.playerShip);
            // player = ((GameObject)settings.GetValue(Settings.playerShip)).transform;
            Application.backgroundLoadingPriority = ThreadPriority.Low;
        }

        IEnumerator AsyncLevelLoad(int sceneIndex)
        {
            if (!sceneLoaded[sceneIndex])
            {
                sceneLoaded[sceneIndex] = true;
                AsyncOperation loading = SceneManager.LoadSceneAsync(sceneNames[sceneIndex], LoadSceneMode.Additive);
                while (!loading.isDone)
                {
                    yield return null;
                }
            }


        }


        // Update is called once per frame
        void Update()
        {
            if (player)
            {
                for(int i = 0; i < sceneNames.Length; i++)
                {
                    if(!sceneLoaded[i] && minBounds[i].x < player.position.x && maxBounds[i].x > player.position.x &&
                        minBounds[i].y < player.position.y && maxBounds[i].y > player.position.y &&
                        minBounds[i].z < player.position.z && maxBounds[i].z > player.position.z)
                    {
                        StartCoroutine(AsyncLevelLoad(i));
                    }
                }
            }
        }

        public void OnPlayerChanged(object playerObject)
        {
            player = ((GameObject)playerObject).transform;
        }
    }

}
