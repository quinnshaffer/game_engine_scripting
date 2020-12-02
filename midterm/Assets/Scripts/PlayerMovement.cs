using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;    
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        gameObject.transform.position = new Vector2(transform.position.x + (h * step),
           transform.position.y + (v * step));
        if (v != 0 || h != 0)
        {
            this.transform.rotation = Quaternion.LookRotation(new Vector2(h, v), Vector3.forward);
            transform.Rotate(Vector3.up * 90);
        }

        if (Input.GetKeyDown("r")) Application.LoadLevel(Application.loadedLevel);
        
    }
}
