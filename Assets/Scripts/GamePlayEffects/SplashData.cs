using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ShipGame
{
    [CreateAssetMenu(menuName ="SplashData")]
    public class SplashData : ScriptableObject
    {
        public int seaIndex;
        public float minRate, maxRate, minSpeed, maxSpeed;
        public Color startColor;
        public float rateSpeedDampen, forceSpeedDampen, strengthSpeedDampen;
        public float rateSpeedSplashMultiplier, rateWaveSplashMultiplier, rateDistanceSplashMultiplier, forceSpeedMultiplier, forceWaveMultiplier, forceDistanceMultiplier, strengthSpeedMultiplier, strengthWaveMultiplier, strengthDistanceMultiplier;
    }
}

