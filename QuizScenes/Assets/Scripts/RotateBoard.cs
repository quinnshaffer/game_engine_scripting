using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBoard : MonoBehaviour
{

    public float delay;
    public float repeatRate;
    public int degrees;
    void Start()
    {
        InvokeRepeating("RotateTransform", delay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RotateTransform()
    {

    }

}
