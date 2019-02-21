using UnityEngine;
using System.Collections;

public interface Effect{


    IEnumerator effectLife();

    string getName();

    void onEffectStart();

    void onEffectEnd();

}
