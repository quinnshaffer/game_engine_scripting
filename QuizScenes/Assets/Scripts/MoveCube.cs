using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCube : MonoBehaviour
{
    public float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }
  

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Floor")
            
            return;
        if (collision.gameObject.name == "LeftWall" || collision.gameObject.name == "RightWall") {
            speed *= -1;
        }

    }
}
