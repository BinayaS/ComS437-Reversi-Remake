using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfBelowFloor : MonoBehaviour
{
    public float floorHeight;

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.position.y < floorHeight) {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, floorHeight + 1f, gameObject.transform.position.z);
        }
    }
}
