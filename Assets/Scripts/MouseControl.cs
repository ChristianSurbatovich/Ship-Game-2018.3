using UnityEngine;
using System.Collections;

public class MouseControl : MonoBehaviour {
    public GameObject reticule;
    private RaycastHit aimpoint;
    private GameObject indicator;
    private int mask;
    private int waitTime;
    public int waitForFrames;
   

    // Use this for initialization
    void Start () {
        mask = (1 << 8) | (1 << 4) | (1 << 11);
	}

    // Update is called once per frame
    void Update()
    {
        if (waitTime > 0)
        {
            waitTime--;
        }
        else
        {

            Ray myray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Physics.Raycast(myray, out aimpoint, Mathf.Infinity, mask, QueryTriggerInteraction.Ignore)
            if (Physics.SphereCast(myray, 1.0f, out aimpoint, Mathf.Infinity, mask, QueryTriggerInteraction.Ignore) && !Input.GetMouseButton(0) && !Input.GetMouseButton(1))
            {

                if (indicator == null)
                {
                    indicator = Instantiate(reticule, aimpoint.point, Quaternion.identity) as GameObject;
                }
                else if (indicator.activeInHierarchy == false)
                {
                    indicator.SetActive(true);
                }
                else
                {
                    indicator.transform.position = aimpoint.point;
                }
            }
            else
            {
                if (indicator)
                {
                    indicator.SetActive(false);
                }
            }
        }
        
    }

    public RaycastHit getAimPoint()
    {
        return aimpoint;
    }

    public void wait()
    {
        waitTime = waitForFrames;
    }
}
