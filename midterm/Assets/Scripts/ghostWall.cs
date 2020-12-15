using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostWall : MonoBehaviour
{
    // Start is called before the first frame update
    bool isColliding;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isColliding = false;
    }
    void OnCollisionEnter(Collision collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
       // Debug.Log("collision detected");
        

        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "player")
        {
            //If the GameObject has the same tag as specified, output this message in the console
            Globals.setScore(0);
            StartCoroutine(ExecuteAfterTime(.4f));
            audioControl.playPlayerDead();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ghost")
        {
           
            if (isColliding) return;
            isColliding = true;
            //Destroy(other.gameObject);
           
            Destroy(this.gameObject);

            Globals.changeScore(1);
            //Debug.Log("hit ghost");

        }
    }
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Application.LoadLevel(Application.loadedLevel);
        // Code to execute after the delay
    }
}
