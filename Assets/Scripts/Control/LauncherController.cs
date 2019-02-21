using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ShipGame
{
    public class LauncherController : MonoBehaviour
    {
        public float spread;
        public GameObject[] lObjects;
        private ProjectileLauncher[] launchers;
        private Vector3[] offset;
        private Vector3[] targetPoints;
        private bool ready = true;
        private float timesincefiring;
        public float recovery, volleyTime, volleyMinTime, volleyMaxTime;
        public Vector2 accuracyX, accuracyY, accuracyZ;
        public float accuracyRange;
        public Vector3 accuracy;
        // Use this for initialization

        public void SetAccuracy(float aSpread, float aRange, float aGrouping)
        {
            spread = aSpread;
          
        }
        private void Awake()
        {
            offset = new Vector3[lObjects.Length];
            targetPoints = new Vector3[lObjects.Length];
            launchers = new ProjectileLauncher[lObjects.Length];
            for (int i = 0; i < lObjects.Length; i++)
            {
                offset[i] = transform.position - lObjects[i].transform.position;
                launchers[i] = lObjects[i].GetComponent<ProjectileLauncher>();
            }
        }
        private void Update()
        {

            if (!ready)
            {
                if (timesincefiring > recovery)
                {
                    ready = true;
                    for (int i = 0; i < launchers.Length; i++)
                    {
                        launchers[i].LoadProjectile();
                    }
                }
                else
                {
                    timesincefiring += Time.deltaTime;
                }
            }
            else
            {

            }
        }

        // Update is called once per frame
        public void AimAtPoint(Vector3 point)
        {
            for (int i = 0; i < launchers.Length; i++)
            {
                accuracy.Set(Random.Range(accuracyX.x, accuracyX.y), Random.Range(accuracyY.x, accuracyY.y), Random.Range(accuracyZ.x, accuracyZ.y));
                Vector3 target = point + (launchers[i].transform.position - transform.position) * spread + accuracy * Vector3.Distance(launchers[i].transform.position, point) / accuracyRange;
                launchers[i].AimAtPoint(target);
            }
        }

        public Vector3[] Fire(Vector3 aimPoint, short id)
        {
            for (int i = 0; i < launchers.Length; i++)
            {
                accuracy.Set(Random.Range(accuracyX.x, accuracyX.y), Random.Range(accuracyY.x, accuracyY.y), Random.Range(accuracyZ.x, accuracyZ.y));
                targetPoints[i] = aimPoint + (launchers[i].transform.position - transform.position) * spread + accuracy * Vector3.Distance(launchers[i].transform.position, aimPoint) / accuracyRange;
                //Debug.DrawLine(launchers[i].transform.position, targetPoints[i], Color.green, 5f);
            }
            ready = false;
            StartCoroutine(Volley(id));
            return targetPoints;
        }

        public void Fire(Vector3[] aimPoints, short id)
        {
            targetPoints = aimPoints;
            ready = false;
            StartCoroutine(Volley(id));
        }
        IEnumerator Volley(short id)
        {
            for (int i = 0; i < launchers.Length; i++)
            {
                launchers[i].Fire(id, targetPoints[i]);
                yield return new WaitForSeconds(Random.Range(volleyMinTime, volleyMaxTime));
            }
        }
    }

}
