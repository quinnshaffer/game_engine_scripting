using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizVerticalMotion : MonoBehaviour
{
    private float lastTime = 0f;
    public float timeInterval = .5f;
    public float ScaleMotion = .1f;
    public GameObject Plane = null;
    private MeshCollider meshCollider = null;
    private Vector3 lastPosition = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        meshCollider =
        Plane.transform.GetComponent<MeshCollider>();
        lastPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        
        lastTime += Time.deltaTime;
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");

        if (Horizontal == 0 && Vertical == 0)
            return;

        if (lastTime > timeInterval)
        {
           

            lastTime = 0;
        }
    }
}
