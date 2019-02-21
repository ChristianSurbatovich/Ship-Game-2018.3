using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShipGame.Network;

namespace ShipGame
{
    

    public class waves : MonoBehaviour
    {
        public float testOffset = 0;
        public float networkTimeOffset;
        [SerializeField]
        private Vector4 LargeSwells, Swells, SmallWaves, Ripples,
            LargeSwells1, LargeSwells2, LargeSwells3, LargeSwells4, LargeSwells5, LargeSwells6,
            Swells1, Swells2, Swells3, Swells4, Swells5, Swells6,
            SmallWaves1, SmallWaves2, SmallWaves3, SmallWaves4, SmallWaves5, SmallWaves6,
            Ripples1, Ripples2, Ripples3, Ripples4, Ripples5, Ripples6,
            DepthMapping;
        private Vector4 scaledLargeSwells1, scaledLargeSwells2, scaledLargeSwells3, scaledLargeSwells4, scaledLargeSwells5, scaledLargeSwells6,
            scaledSwells1, scaledSwells2, scaledSwells3, scaledSwells4, scaledSwells5, scaledSwells6,
            scaledSmallWaves1, scaledSmallWaves2, scaledSmallWaves3, scaledSmallWaves4, scaledSmallWaves5, scaledSmallWaves6,
           scaledRipples1, scaledRipples2, scaledRipples3, scaledRipples4, scaledRipples5, scaledRipples6;


        private int timeOffsetID,lsID,ls1,ls2,ls3,ls4,ls5,ls6,sID,s1,s2,s3,s4,s5,s6,swID,sw1,sw2,sw3,sw4,sw5,sw6,rID,r1,r2,r3,r4,r5,r6,dpm;
        private Vector3 newPosition;
        [ExecuteInEditMode]
        private void Awake()
        {
            print("Getting PropertyIDs");
            timeOffsetID = Shader.PropertyToID("timeOffset");
            ls1 = Shader.PropertyToID("LargeSwells1");
            ls2 = Shader.PropertyToID("LargeSwells2");
            ls3 = Shader.PropertyToID("LargeSwells3");
            ls4 = Shader.PropertyToID("LargeSwells4");
            ls5 = Shader.PropertyToID("LargeSwells5");
            ls6 = Shader.PropertyToID("LargeSwells6");
            s1 = Shader.PropertyToID("Swells1");
            s2 = Shader.PropertyToID("Swells2");
            s3 = Shader.PropertyToID("Swells3");
            s4 = Shader.PropertyToID("Swells4");
            s5 = Shader.PropertyToID("Swells5");
            s6 = Shader.PropertyToID("Swells6");
            sw1 = Shader.PropertyToID("SmallWaves1");
            sw2 = Shader.PropertyToID("SmallWaves2");
            sw3 = Shader.PropertyToID("SmallWaves3");
            sw4 = Shader.PropertyToID("SmallWaves4");
            sw5 = Shader.PropertyToID("SmallWaves5");
            sw6 = Shader.PropertyToID("SmallWaves6");
            r1 = Shader.PropertyToID("Ripples1");
            r2 = Shader.PropertyToID("Ripples2");
            r3 = Shader.PropertyToID("Ripples3");
            r4 = Shader.PropertyToID("Ripples4");
            r5 = Shader.PropertyToID("Ripples5");
            r6 = Shader.PropertyToID("Ripples6");
            dpm = Shader.PropertyToID("DepthMapping");

        }
        private void Start()
        {
            StartCoroutine("WaveSync");
        }

        private void Update()
        {
            if (CameraControl.target)
            {
                newPosition = CameraControl.target.transform.position;
                newPosition.y = transform.position.y;
                transform.position = newPosition;
            }

        }


        private IEnumerator WaveSync()
        {
            while (true)
            {
                Shader.SetGlobalFloat(timeOffsetID, NetworkManager.netTimeOffset);
                networkTimeOffset = NetworkManager.netTimeOffset;
                yield return new WaitForSeconds(10);
            }

        }

        private void OnValidate()
        {
            print("Getting PropertyIDs");
            timeOffsetID = Shader.PropertyToID("timeOffset");
            ls1 = Shader.PropertyToID("LargeSwells1");
            ls2 = Shader.PropertyToID("LargeSwells2");
            ls3 = Shader.PropertyToID("LargeSwells3");
            ls4 = Shader.PropertyToID("LargeSwells4");
            ls5 = Shader.PropertyToID("LargeSwells5");
            ls6 = Shader.PropertyToID("LargeSwells6");
            s1 = Shader.PropertyToID("Swells1");
            s2 = Shader.PropertyToID("Swells2");
            s3 = Shader.PropertyToID("Swells3");
            s4 = Shader.PropertyToID("Swells4");
            s5 = Shader.PropertyToID("Swells5");
            s6 = Shader.PropertyToID("Swells6");
            sw1 = Shader.PropertyToID("SmallWaves1");
            sw2 = Shader.PropertyToID("SmallWaves2");
            sw3 = Shader.PropertyToID("SmallWaves3");
            sw4 = Shader.PropertyToID("SmallWaves4");
            sw5 = Shader.PropertyToID("SmallWaves5");
            sw6 = Shader.PropertyToID("SmallWaves6");
            r1 = Shader.PropertyToID("Ripples1");
            r2 = Shader.PropertyToID("Ripples2");
            r3 = Shader.PropertyToID("Ripples3");
            r4 = Shader.PropertyToID("Ripples4");
            r5 = Shader.PropertyToID("Ripples5");
            r6 = Shader.PropertyToID("Ripples6");
            dpm = Shader.PropertyToID("DepthMapping");
            print("Setting Shader Globals");
            Vector2 normalized;
            scaledLargeSwells1 = Vector4.Scale(LargeSwells1, LargeSwells);
            normalized.x = scaledLargeSwells1.x;
            normalized.y = scaledLargeSwells1.y;
            normalized.Normalize();
            scaledLargeSwells1.x = normalized.x;
            scaledLargeSwells1.y = normalized.y;

            scaledLargeSwells2 = Vector4.Scale(LargeSwells2, LargeSwells);
            normalized.x = scaledLargeSwells2.x;
            normalized.y = scaledLargeSwells2.y;
            normalized.Normalize();
            scaledLargeSwells2.x = normalized.x;
            scaledLargeSwells2.y = normalized.y;

            scaledLargeSwells3 = Vector4.Scale(LargeSwells3, LargeSwells);
            normalized.x = scaledLargeSwells3.x;
            normalized.y = scaledLargeSwells3.y;
            normalized.Normalize();
            scaledLargeSwells3.x = normalized.x;
            scaledLargeSwells3.y = normalized.y;

            scaledLargeSwells4 = Vector4.Scale(LargeSwells4, LargeSwells);
            normalized.x = scaledLargeSwells4.x;
            normalized.y = scaledLargeSwells4.y;
            normalized.Normalize();
            scaledLargeSwells4.x = normalized.x;
            scaledLargeSwells4.y = normalized.y;

            scaledLargeSwells5 = Vector4.Scale(LargeSwells5, LargeSwells);
            normalized.x = scaledLargeSwells5.x;
            normalized.y = scaledLargeSwells5.y;
            normalized.Normalize();
            scaledLargeSwells5.x = normalized.x;
            scaledLargeSwells5.y = normalized.y;

            scaledLargeSwells6 = Vector4.Scale(LargeSwells6, LargeSwells);
            normalized.x = scaledLargeSwells6.x;
            normalized.y = scaledLargeSwells6.y;
            normalized.Normalize();
            scaledLargeSwells6.x = normalized.x;
            scaledLargeSwells6.y = normalized.y;


            scaledSwells1 = Vector4.Scale(Swells1, Swells);
            normalized.x = scaledSwells1.x;
            normalized.y = scaledSwells1.y;
            normalized.Normalize();
            scaledSwells1.x = normalized.x;
            scaledSwells1.y = normalized.y;

            scaledSwells2 = Vector4.Scale(Swells2, Swells);
            normalized.x = scaledSwells2.x;
            normalized.y = scaledSwells2.y;
            normalized.Normalize();
            scaledSwells2.x = normalized.x;
            scaledSwells2.y = normalized.y;

            scaledSwells3 = Vector4.Scale(Swells3, Swells);
            normalized.x = scaledSwells3.x;
            normalized.y = scaledSwells3.y;
            normalized.Normalize();
            scaledSwells3.x = normalized.x;
            scaledSwells3.y = normalized.y;

            scaledSwells4 = Vector4.Scale(Swells4, Swells);
            normalized.x = scaledSwells4.x;
            normalized.y = scaledSwells4.y;
            normalized.Normalize();
            scaledSwells4.x = normalized.x;
            scaledSwells4.y = normalized.y;

            scaledSwells5 = Vector4.Scale(Swells5, Swells);
            normalized.x = scaledSwells5.x;
            normalized.y = scaledSwells5.y;
            normalized.Normalize();
            scaledSwells5.x = normalized.x;
            scaledSwells5.y = normalized.y;

            scaledSwells6 = Vector4.Scale(Swells6, Swells);
            normalized.x = scaledSwells6.x;
            normalized.y = scaledSwells6.y;
            normalized.Normalize();
            scaledSwells6.x = normalized.x;
            scaledSwells6.y = normalized.y;



            scaledSmallWaves1 = Vector4.Scale(SmallWaves1, SmallWaves);
            normalized.x = scaledSmallWaves1.x;
            normalized.y = scaledSmallWaves1.y;
            normalized.Normalize();
            scaledSmallWaves1.x = normalized.x;
            scaledSmallWaves1.y = normalized.y;

            scaledSmallWaves2 = Vector4.Scale(SmallWaves2, SmallWaves);
            normalized.x = scaledSmallWaves2.x;
            normalized.y = scaledSmallWaves2.y;
            normalized.Normalize();
            scaledSmallWaves2.x = normalized.x;
            scaledSmallWaves2.y = normalized.y;

            scaledSmallWaves3 = Vector4.Scale(SmallWaves3, SmallWaves);
            normalized.x = scaledSmallWaves3.x;
            normalized.y = scaledSmallWaves3.y;
            normalized.Normalize();
            scaledSmallWaves3.x = normalized.x;
            scaledSmallWaves3.y = normalized.y;

            scaledSmallWaves4 = Vector4.Scale(SmallWaves4, SmallWaves);
            normalized.x = scaledSmallWaves4.x;
            normalized.y = scaledSmallWaves4.y;
            normalized.Normalize();
            scaledSmallWaves4.x = normalized.x;
            scaledSmallWaves4.y = normalized.y;

            scaledSmallWaves5 = Vector4.Scale(SmallWaves5, SmallWaves);
            normalized.x = scaledSmallWaves5.x;
            normalized.y = scaledSmallWaves5.y;
            normalized.Normalize();
            scaledSmallWaves5.x = normalized.x;
            scaledSmallWaves5.y = normalized.y;

            scaledSmallWaves6 = Vector4.Scale(SmallWaves6, SmallWaves);
            normalized.x = scaledSmallWaves6.x;
            normalized.y = scaledSmallWaves6.y;
            normalized.Normalize();
            scaledSmallWaves6.x = normalized.x;
            scaledSmallWaves6.y = normalized.y;



            scaledRipples1 = Vector4.Scale(Ripples1, Ripples);
            normalized.x = scaledRipples1.x;
            normalized.y = scaledRipples1.y;
            normalized.Normalize();
            scaledRipples1.x = normalized.x;
            scaledRipples1.y = normalized.y;

            scaledRipples2 = Vector4.Scale(Ripples2, Ripples);
            normalized.x = scaledRipples2.x;
            normalized.y = scaledRipples2.y;
            normalized.Normalize();
            scaledRipples2.x = normalized.x;
            scaledRipples2.y = normalized.y;

            scaledRipples3 = Vector4.Scale(Ripples3, Ripples);
            normalized.x = scaledRipples3.x;
            normalized.y = scaledRipples3.y;
            normalized.Normalize();
            scaledRipples3.x = normalized.x;
            scaledRipples3.y = normalized.y;

            scaledRipples4 = Vector4.Scale(Ripples4, Ripples);
            normalized.x = scaledRipples4.x;
            normalized.y = scaledRipples4.y;
            normalized.Normalize();
            scaledRipples4.x = normalized.x;
            scaledRipples4.y = normalized.y;

            scaledRipples5 = Vector4.Scale(Ripples5, Ripples);
            normalized.x = scaledRipples5.x;
            normalized.y = scaledRipples5.y;
            normalized.Normalize();
            scaledRipples5.x = normalized.x;
            scaledRipples5.y = normalized.y;

            scaledRipples6 = Vector4.Scale(Ripples6, Ripples);
            normalized.x = scaledRipples6.x;
            normalized.y = scaledRipples6.y;
            normalized.Normalize();
            scaledRipples6.x = normalized.x;
            scaledRipples6.y = normalized.y;


            Shader.SetGlobalVector(ls1, scaledLargeSwells1);
            Shader.SetGlobalVector(ls2, scaledLargeSwells2);
            Shader.SetGlobalVector(ls3, scaledLargeSwells3);
            Shader.SetGlobalVector(ls4, scaledLargeSwells4);
            Shader.SetGlobalVector(ls5, scaledLargeSwells5);
            Shader.SetGlobalVector(ls6, scaledLargeSwells6);
            Shader.SetGlobalVector(s1, scaledSwells1);
            Shader.SetGlobalVector(s2, scaledSwells2);
            Shader.SetGlobalVector(s3, scaledSwells3);
            Shader.SetGlobalVector(s4, scaledSwells4);
            Shader.SetGlobalVector(s5, scaledSwells5);
            Shader.SetGlobalVector(s6, scaledSwells6);
            Shader.SetGlobalVector(sw1, scaledSmallWaves1);
            Shader.SetGlobalVector(sw2, scaledSmallWaves2);
            Shader.SetGlobalVector(sw3, scaledSmallWaves3);
            Shader.SetGlobalVector(sw4, scaledSmallWaves4);
            Shader.SetGlobalVector(sw5, scaledSmallWaves5);
            Shader.SetGlobalVector(sw6, scaledSmallWaves6);
            Shader.SetGlobalVector(r1, scaledRipples1);
            Shader.SetGlobalVector(r2, scaledRipples2);
            Shader.SetGlobalVector(r3, scaledRipples3);
            Shader.SetGlobalVector(r4, scaledRipples4);
            Shader.SetGlobalVector(r5, scaledRipples5);
            Shader.SetGlobalVector(r6, scaledRipples6);
            Shader.SetGlobalFloat(timeOffsetID, networkTimeOffset);
            Shader.SetGlobalVector(dpm, DepthMapping);
        }
        public float waveHeight(Vector3 position)
        {
            Vector3 vertexShift;
            float height;
            position.y = transform.position.y;
            vertexShift = position +
                GerstnerWave(scaledLargeSwells1, position) +
                GerstnerWave(scaledLargeSwells2, position) +
                GerstnerWave(scaledLargeSwells3, position) +
                GerstnerWave(scaledLargeSwells4, position) +
                GerstnerWave(scaledLargeSwells5, position) +
                GerstnerWave(scaledLargeSwells6, position) +
                GerstnerWave(scaledSwells1, position) +
                GerstnerWave(scaledSwells2, position) +
                GerstnerWave(scaledSwells3, position) +
                GerstnerWave(scaledSwells4, position) +
                GerstnerWave(scaledSwells5, position) +
                GerstnerWave(scaledSwells6, position);
            position.x += position.x - vertexShift.x;
            position.z += position.z - vertexShift.z;

            height = transform.position.y +
                GerstnerWaveHeight(scaledLargeSwells1, position) +
                GerstnerWaveHeight(scaledLargeSwells2, position) +
                GerstnerWaveHeight(scaledLargeSwells3, position) +
                GerstnerWaveHeight(scaledLargeSwells4, position) +
                GerstnerWaveHeight(scaledLargeSwells5, position) +
                GerstnerWaveHeight(scaledLargeSwells6, position) +
                GerstnerWaveHeight(scaledSwells1, position) +
                GerstnerWaveHeight(scaledSwells2, position) +
                GerstnerWaveHeight(scaledSwells3, position) +
                GerstnerWaveHeight(scaledSwells4, position) +
                GerstnerWaveHeight(scaledSwells5, position) +
                GerstnerWaveHeight(scaledSwells6, position);
            /*
                GerstnerWaveHeight(Vector4.Scale(SmallWaves1, SmallWaves), position) +
                GerstnerWaveHeight(Vector4.Scale(SmallWaves2, SmallWaves), position) +
                GerstnerWaveHeight(Vector4.Scale(SmallWaves3, SmallWaves), position) +
                GerstnerWaveHeight(Vector4.Scale(SmallWaves4, SmallWaves), position) +
                GerstnerWaveHeight(Vector4.Scale(SmallWaves5, SmallWaves), position) +
                GerstnerWaveHeight(Vector4.Scale(SmallWaves6, SmallWaves), position);
                */
            return height;
        }


        public float fineWaveHeight(Vector3 position)
        {
            return waveHeight(position);
        }

        private float GerstnerWaveHeight(Vector4 wave, Vector3 position)
        {
            float steepness = wave.z;
            float wavelength = wave.w;
            float k = 2.0f * Mathf.PI / wavelength;
            float c = Mathf.Sqrt(9.8f / k);
            Vector2 d = new Vector2(wave.x, wave.y);
            float f = k * (Vector2.Dot(d, new Vector2(position.x, position.z)) - c * (Time.time + networkTimeOffset));
            float a = steepness / k;
            return a * Mathf.Sin(f);
        }

        private Vector3 GerstnerWave(Vector4 wave, Vector3 position)
        {
            float steepness = wave.z;   
            float wavelength = wave.w;
            float k = 2.0f * Mathf.PI / wavelength;
            float c = Mathf.Sqrt(9.8f / k);
            Vector2 d = new Vector2(wave.x, wave.y); 
            float f = k * (Vector2.Dot(d, new Vector2(position.x, position.z)) - c * (Time.time + networkTimeOffset));
            float a = steepness / k;
            return new Vector3(
                d.x * a * Mathf.Cos(f),
                a * Mathf.Sin(f),
                d.y * a * Mathf.Cos(f)
                );
        }
        

        private float ValueFromTexture(Vector2Int texCoord, Texture2D tex)
        {
            //return tex.GetPixelBilinear(texCoord.x, texCoord.y).r;
            return tex.GetPixel(texCoord.x, texCoord.y).r;
        }

        private float Remap(float value, float oldMin, float oldMax, float newMin, float newMax)
        {
            return ((value - oldMin)/(oldMax - oldMin) * (newMax - newMin)) + newMin;
        }


    }

}
