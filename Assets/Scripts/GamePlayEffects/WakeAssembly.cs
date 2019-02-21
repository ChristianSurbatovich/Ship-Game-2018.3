using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShipGame
{


    public class WakeAssembly : MonoBehaviour
    {
        public float volumeFactor, soundLockout, minIntensity;
        public bool playSound = false;
        public AudioClip splashSound;
        public AudioSource splashPlayer;
        [SerializeField]
        private float wakeSystemHeight;
        [SerializeField]
        private IShipControl ship;
        private Vector3 newPosition;
        private Vector3 newRotation;
        [SerializeField]
        private ParticleSystem[] splashParticles;
        [SerializeField]
        private ParticleSystem[] renderParticles;
        [SerializeField]
        private SplashData[] splashParticlesData;
        // {newDifference, lastDifference, lastPosition, totalChange}
        private Vector4[] seaHeightCounter;
        private float[] seaHeights;
        private float rateSpeedMultiplier, rateWaveMultiplier, rateDistanceMultiplier,
                      forceSpeedMultiplier, forceWaveMultiplier, forceDistanceMultiplier,
                      strengthSpeedMultiplier, strengthWaveMultiplier, strengthDistanceMultiplier, speed, soundPlayedTime, soundIntensity;
        
        // Use this for initialization
        void Start()
        {
            // wakeSystemHeight = transform.position.y;
            ship = GetComponentInParent<IShipControl>();
            seaHeightCounter = new Vector4[5];
            seaHeights = new float[5];
        }


        // Update is called once per frame
        void Update()
        {
            ship.SeaHeight(ref seaHeights);
            /*
            newPosition = ship.Position();
            newPosition.y = wakeSystemHeight;
            transform.position = newPosition;
            newRotation = ship.LocalRotation();
            newRotation.x = -newRotation.x;
            newRotation.y = 0;
            newRotation.z = -newRotation.z;
            transform.localRotation = Quaternion.Euler(newRotation);
            */
            for(int i = 0; i < 5; i++)
            {
                if(Mathf.Sign(seaHeightCounter[i].x) != Mathf.Sign(seaHeightCounter[i].y))
                {
                    seaHeightCounter[i].w = 0.0f;
                }
                seaHeightCounter[i].y = seaHeightCounter[i].x;
                seaHeightCounter[i].x = seaHeights[i] - seaHeightCounter[i].z;
                seaHeightCounter[i].z = seaHeights[i];
                seaHeightCounter[i].w += seaHeightCounter[i].y;
            }


            for(int j = 0; j < splashParticles.Length; j++)
            {
                SplashData splash = splashParticlesData[j];
                Vector4 seaHeight = seaHeightCounter[splash.seaIndex];

                rateSpeedMultiplier = splash.rateSpeedSplashMultiplier;
                forceSpeedMultiplier = splash.forceSpeedMultiplier;
                strengthSpeedMultiplier = splash.strengthSpeedMultiplier;
                if (Mathf.Sign(seaHeight.w) < 0)
                {
                    rateDistanceMultiplier = splash.rateDistanceSplashMultiplier;
                    forceDistanceMultiplier = splash.forceDistanceMultiplier;
                    strengthDistanceMultiplier = splash.strengthDistanceMultiplier;

                    if(Mathf.Sign(seaHeight.x) > 0)
                    {
                        rateWaveMultiplier = splash.rateWaveSplashMultiplier;
                        forceWaveMultiplier = splash.forceWaveMultiplier;
                        strengthWaveMultiplier = splash.strengthWaveMultiplier;
                        playSound = true;
                    }
                    else
                    {
                        rateWaveMultiplier = 0;
                        forceWaveMultiplier = 0;
                        strengthWaveMultiplier = 0;
                    }
                }
                else
                {
                    rateWaveMultiplier = 0;
                    forceWaveMultiplier = 0;
                    strengthWaveMultiplier = 0;
                    rateDistanceMultiplier = 0;
                    forceDistanceMultiplier = 0;
                    strengthDistanceMultiplier = 0;

                }
                ParticleSystem.EmissionModule em = splashParticles[j].emission;
                speed = ship.GetStat(Stats.CURRENT_SPEED);
                float rate = (rateWaveMultiplier + rateDistanceMultiplier) * (speed / splash.rateSpeedDampen) * Mathf.Abs(seaHeight.w) + rateSpeedMultiplier * speed;
                em.rateOverTime = rate;
                if (rate > soundIntensity)
                {
                    soundIntensity = rate;
                }
                ParticleSystem.MainModule mm = splashParticles[j].main;
                float multiplier = (forceWaveMultiplier + forceDistanceMultiplier) * (speed / splash.forceSpeedDampen) * Mathf.Abs(seaHeight.w) + forceSpeedMultiplier * speed;
                mm.startSpeed = new ParticleSystem.MinMaxCurve(multiplier * splash.minSpeed, multiplier * splash.maxSpeed);
                float h, s, v, a;
                a = splash.startColor.a;
                v = (strengthWaveMultiplier + strengthDistanceMultiplier) * (speed / splash.strengthSpeedDampen) * Mathf.Abs(seaHeight.w) + strengthSpeedMultiplier * speed;
                Color.RGBToHSV(splash.startColor, out h, out s, out v);
                Color newColor = Color.HSVToRGB(h, s, v);
                newColor.a = a;
                mm.startColor = newColor;


                

            }

            if (playSound && Time.time - soundPlayedTime > soundLockout && soundIntensity > minIntensity)
            {
                splashPlayer.PlayOneShot(splashSound, soundIntensity / volumeFactor);
                soundIntensity = 0;
                soundPlayedTime = Time.time;
            }
            playSound = false;


        }
    }

}
