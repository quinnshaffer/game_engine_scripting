using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostFollow : MonoBehaviour
{
    public float speed;
    public bool isSuper;
    public Texture albedo;
    public bool fade;
    public float trans, dis;
    GameObject[] ghosts;
    float fov;
    Light li;
    Renderer r;
    Color col;
    public bool isColliding;
    public Quaternion playerRot;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        ghosts = GameObject.FindGameObjectsWithTag("ghost");
        li =gameObject.GetComponent<Light>();
        r = GetComponent<Renderer>();
        col = r.material.GetColor("_Color");
        speed = Mathf.Pow((Random.value),2)*4+4;
        //speed = .1f;
        
        //check if special
        trans = 0;
        player = GameObject.Find("Player");
        if (ghosts.Length >= 4)
        {
            if (Random.Range(0f, 1f) >= .8f)
            {
                isSuper = true;
            }
            else if(Random.Range(0f, 1f) >= .6f)
            { 
               fade = true; 
            }
            else
            {
                isSuper = false;
                fade = false;
            }
        }
        
        //set up materials
        if (isSuper)
        {
            speed *= 1.5f;
            albedo = Resources.Load("superghostface") as Texture;
            
            r.material.SetTexture("_MainTex", albedo);
            r.material.SetTexture("_EmissionMap", albedo);
            Debug.Log(r.material.GetTexture("_EmissionMap"));
            li.color = new Vector4(.6f,.6f,1f);
        }
        else
        {
            albedo = Resources.Load("ghostface") as Texture;
            r.material.SetTexture("_MainTex", albedo);
            r.material.SetTexture("_EmissionMap", albedo);
        }

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 startVector = transform.position;
        this.transform.position= Vector3.MoveTowards(this.transform.position,player.transform.position,speed*Time.deltaTime);
        Vector3 endVector = transform.position;
        this.transform.rotation = Quaternion.LookRotation((startVector-endVector), Vector3.back);
        transform.Rotate(Vector3.right*-90);
        transform.Rotate(Vector3.forward * 180);
        isColliding = false;
        fov = Quaternion.Angle(transform.rotation, Globals.playerRotation);
        if (fade)
        {
            trans = (Mathf.Sin(Time.time)+1)/2;
            //trans = 1f/Mathf.Pow(Vector3.Distance(gameObject.transform.position, GameObject.Find("Player").transform.position),1f)-.5f;
            dis = Vector3.Distance(gameObject.transform.position, GameObject.Find("Player").transform.position);
            //trans =1f/Vector3.Distance(gameObject.transform.position, GameObject.Find("Player").transform.position)-1f;
            //1.26f - .28f * dis + .015f*Mathf.Pow(dis, 2)
            trans = 1.1f - .28f * dis + .015f*Mathf.Pow(dis, 2);
            if (fov < 100)
            {
                trans *= 10;
                trans += .5f;
            }
            trans = Mathf.Min(trans, 1f);
            trans +=Mathf.Sin(Time.time)/5;
            col.a = trans;
            r.material.SetColor("_Color", col);
            li.intensity = Mathf.Min(trans * .8f + .2f, 1f);
        }
        
        //Debug.Log(fov);
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
            audioControl.playPlayerDead();
            GameObject.Destroy(collision.gameObject);
            StartCoroutine(ExecuteAfterTime(.4f));
        }
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "ghost"){
            if (isColliding) return;
            isColliding = true;
            if (!isSuper)
            {
                audioControl.playGhostKill();
                //Destroy(other.gameObject);
                Destroy(this.gameObject);

                Globals.changeScore(1);
                //Debug.Log("not super");
            }
            else
            {
                //Debug.Log("super");
                audioControl.playGhostDown();
                isSuper = false;
                speed/=1.5f;
                Globals.changeScore(1);
                albedo = Resources.Load("ghostface") as Texture;
                r.material.SetTexture("_MainTex", albedo);
                r.material.SetTexture("_EmissionMap", albedo);
                li.color = Color.white;
            }
        }
    }
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Application.LoadLevel(Application.loadedLevel);
        // Code to execute after the delay
    }
}
