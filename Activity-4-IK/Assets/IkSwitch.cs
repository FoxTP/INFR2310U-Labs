using System.Collections;
using System.Collections.Generic;
using DitzelGames.FastIK;
using UnityEngine;

public class IkSwitch : MonoBehaviour
{
    FastIKFabric ik;
    Transform t;
    GameObject raycastPoint;
    void Start()
    {
        ik = GetComponent<FastIKFabric>();
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        float maxDist = 1.0f;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, maxDist))
        {
            Debug.Log(hit.collider.name);
            ik.enabled = true;
            Vector3 go = hit.point;
            if (go != null)
            {
                ik.Target.transform.position = go;

            }
        }
    }
}