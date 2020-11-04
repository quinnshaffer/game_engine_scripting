using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostFollow : MonoBehaviour
{
    public float speed;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.Find("Player");
        this.transform.position= Vector3.MoveTowards(this.transform.position,player.transform.position,speed*Time.deltaTime);
        
    }
    void OnCollisionEnter(Collision collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        Debug.Log("collision detected");
        if (collision.gameObject.tag == "ghost")
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            
            Destroy(collision.gameObject);
            Destroy(this);
        }

        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "player")
        {
            //If the GameObject has the same tag as specified, output this message in the console
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}
