using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baton : MonoBehaviour
{

    public GameObject Baton;

    // We are going to rotate on the z-axis. We'll rename this as a velocity;
    public Vector3 aVelocity;

    // Start is called before the first frame update
    void Start()
    {
        Baton = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        Renderer renderer = Baton.GetComponent<Renderer>();
        renderer.material = new Material(Shader.Find("Diffuse"));

        renderer.material.color = Color.white;
        aVelocity = new Vector3(0f, 0f, 6f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Baton.transform.Rotate(aVelocity, Space.World);
        //Baton.transform.rotation += aVelocity;
    }
}