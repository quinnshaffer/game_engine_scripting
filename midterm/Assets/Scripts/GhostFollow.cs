using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostFollow : MonoBehaviour
{
    public float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        speed = Mathf.Pow((Random.value),2)*4+4;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.Find("Player");
        Vector3 startVector = transform.position;
        this.transform.position= Vector3.MoveTowards(this.transform.position,player.transform.position,speed*Time.deltaTime);
        Vector3 endVector = transform.position;
        this.transform.rotation = Quaternion.LookRotation((startVector-endVector), Vector3.back);
        transform.Rotate(Vector3.right*-90);
        transform.Rotate(Vector3.forward * 180);

        
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
            Globals.setScore(0);
            Application.LoadLevel(Application.loadedLevel);
        }
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "ghost"){ 
        Destroy(other.gameObject);
        Destroy(this);

            Globals.changeScore(1);
        }
    }
}
