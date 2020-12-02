using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTest : MonoBehaviour
{
    // Start is called before the first frame update
    public float zchange = .1f;
    public float xchange = 0f;
    private bool Collided = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(xchange, 0, zchange);
        if (Collided) 
            foreach (GameObject sphere in GameObject.FindGameObjectsWithTag("sphere")) 
                GameObject.Destroy(sphere);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name != "Plane")
        {
            
            Collided = true;
        }
    }
}
