using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameTimer : MonoBehaviour{
    public delegate void returnObject();

    // Use this for initialization



    public void RunAfter(float time, returnObject func)
    {
        StartCoroutine(CountDown(time, func));
    } 
    public static IEnumerator CountDown(float t, returnObject func)
    {
        yield return new WaitForSeconds(t);
        func();
    }

    public static IEnumerator DeactivateAfter(float t, GameObject obj)
    {
        yield return new WaitForSeconds(t);
        obj.SetActive(false);
    }

    public static IEnumerator DeactivateAndReturn(float t, GameObject obj, Transform parent)
    {
        yield return new WaitForSeconds(t);
        obj.transform.SetParent(parent);
        obj.SetActive(false);
    }
}
