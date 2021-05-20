using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform desiredPos;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(desiredPos.transform.position.x, desiredPos.transform.position.y, -10);
    }
}
