using System.Collections;
using System.Collections.Generic;
using DitzelGames.FastIK;
using UnityEngine;

public class IKLedgeGrab : MonoBehaviour
{
    FastIKFabric ik;
    private void Start()
    {
        ik = GetComponent<FastIKFabric>();
    }
    void FixedUpdate()
    {
        RaycastHit hit;
        float maxDist = 5.0f;
        float radius = 10;
        if (Physics.SphereCast(transform.position, radius, Vector3.up, out hit, maxDist))
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
