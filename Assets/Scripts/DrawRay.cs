using UnityEngine;
using System.Collections;

public class DrawRay : MonoBehaviour {
    public GameObject reticule;
    private RaycastHit aimpoint;
    private GameObject indicator;

    void Start()
    {
        
    }

    void Update()
    {
        Ray myray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(myray, out aimpoint))
        {
            if (indicator == null)
            {
                indicator = Instantiate(reticule, aimpoint.point, Quaternion.identity) as GameObject;
            }
            else
            {
                indicator.transform.position = aimpoint.point;
            }
        }
        else
        {
            Destroy(indicator);
        }
       
        Debug.DrawRay(myray.origin, myray.direction, Color.yellow);
    }
}
