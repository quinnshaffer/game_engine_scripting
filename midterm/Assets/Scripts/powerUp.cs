using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        //Debug.Log("collision detected");


        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "player")
        {
            //If the GameObject has the same tag as specified, output this message in the console
            audioControl.playGhostKill();
            GameObject[] ghosts = GameObject.FindGameObjectsWithTag("ghost");
            foreach (GameObject ghost in ghosts)
            {
                GameObject.Destroy(ghost);
                Globals.score++;
            }
            GameObject.Destroy(this.gameObject);
        }
    }
}
