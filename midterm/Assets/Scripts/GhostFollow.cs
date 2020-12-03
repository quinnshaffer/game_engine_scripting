using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostFollow : MonoBehaviour
{
    public float speed;
    public bool isSuper;
    public Texture albedo;
    Renderer r;
    public bool isColliding;
    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Renderer>();
        speed = Mathf.Pow((Random.value),2)*4+4;
        //speed = .1f;
        if (Random.Range(0f, 1f) >= .8f) isSuper = true;
        else isSuper = false;
        if (isSuper)
        {
            speed *= 1.5f;
            albedo = Resources.Load("superghostface") as Texture;
            r.material.SetTexture("_MainTex", albedo);
            r.material.SetTexture("_EmissionMap", albedo);
        }
        else {
            albedo = Resources.Load("ghostface") as Texture;
            r.material.SetTexture("_MainTex", albedo);
            r.material.SetTexture("_EmissionMap", albedo);
        }


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
        isColliding = false;

    }
    void OnCollisionEnter(Collision collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        //Debug.Log("collision detected");
        

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
            if (isColliding) return;
            isColliding = true;
            if (!isSuper)
            {
                //Destroy(other.gameObject);
                Destroy(this.gameObject);

                Globals.changeScore(1);
                //Debug.Log("not super");
            }
            else
            {
                //Debug.Log("super");
                isSuper = false;
                speed/=1.5f;
                Globals.changeScore(1);
                albedo = Resources.Load("ghostface") as Texture;
                r.material.SetTexture("_MainTex", albedo);
                r.material.SetTexture("_EmissionMap", albedo);
            }
        }
    }
}
