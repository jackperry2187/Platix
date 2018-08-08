using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour {

    public float RotateSpeed = 2.5f;
    public Transform Center;
    private Vector3 zAxis = new Vector3(0, 0, 1);

    private void Update()
    {
        if(Time.timeScale != 0)
        {
            transform.RotateAround(Center.position, zAxis, RotateSpeed);
        }   
    }
}
