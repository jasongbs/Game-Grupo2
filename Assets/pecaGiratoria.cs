using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pecaGiratoria : MonoBehaviour
{
    float smooth = 0.0f;
    
    void Update()
    {
        smooth = smooth + 0.5f;

        // Dampen towards the target rotation
        this.transform.eulerAngles = new Vector3(0, 0, smooth);
    }
}
