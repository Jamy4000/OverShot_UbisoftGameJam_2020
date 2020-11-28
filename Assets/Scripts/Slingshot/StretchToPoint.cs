using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class StretchToPoint : MonoBehaviour
{
    public Transform targetpoint;
    public float lengthMultiplier = 1.0f;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(targetpoint);
        transform.localScale = new Vector3(1,1,Vector3.Distance(transform.position, targetpoint.position)* lengthMultiplier);
    }

}
