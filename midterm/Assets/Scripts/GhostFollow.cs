using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GhostFollow : MonoBehaviour
{
    public float speed,baseSpeed;
    public bool isSuper;
    public Texture albedo;
    public bool fade;
    public float trans, dis;
    Vector3 playerPos;
    GameObject[] ghosts;
    float fov;
    Light li;
    Renderer r;
    Color col;
    public bool isColliding;
    public Quaternion playerRot;
    GameObject player;
    Vector3 startVector;
    // Start is called before the first frame update
    void Start()
    {
        ghosts = GameObject.FindGameObjectsWithTag("ghost");
        li =gameObject.GetComponent<Light>();
        r = GetComponent<Renderer>();
        col = r.material.GetColor("_Color");
        baseSpeed = Mathf.Pow((Random.value),2)*4f+4;
        speed = baseSpeed;
        
        //check if special
        trans = 0;
        player = GameObject.Find("Player");
        if (ghosts.Length >= 4)
        {
            if (Random.Range(0f, 1f) >= .8f) isSuper = true;
            else isSuper = false;
        }
        
        //set up materials
        if (isSuper)
        {
            baseSpeed *= 1.5f;
            albedo = Resources.Load("superghostface") as Texture;
            
            r.material.SetTexture("_MainTex", albedo);
            r.material.SetTexture("_EmissionMap", albedo);
            //Debug.Log(r.material.GetTexture("_EmissionMap"));
            li.color = new Vector4(.6f,.6f,1f);
        }
        else
        {
            albedo = Resources.Load("ghostface") as Texture;
            r.material.SetTexture("_MainTex", albedo);
            r.material.SetTexture("_EmissionMap", albedo);
        }
        fade = true;
    }

    // Update is called once per frame
    void Update()
    {
        speed = baseSpeed;
        startVector = transform.position;
        if (player != null)
        {
            fov = Quaternion.Angle(transform.rotation, Globals.playerRotation);
            if (fade)
            {

                dis = Vector3.Distance(gameObject.transform.position, playerPos) / 1.5f;
                if (dis < 8) trans = 1.26f - .28f * dis + .015f * Mathf.Pow(dis, 2);
                else trans = 0;
                //Debug.Log(trans + ", " + fov);
                if (fov < 100&&Globals.lightOn)
                {
                    trans += .2f;
                    fov = 100 - fov;
                    trans *= fov / 2;
                    trans *= Globals.percentCharge;
                    speed = baseSpeed-baseSpeed*(Globals.percentCharge/2);
                    //trans += .5f;

                }
                else trans -= .15f;
                //Debug.Log(trans + ", " + fov);
                trans = Mathf.Min(trans, 1f);
                trans += Mathf.Sin(Time.time * speed/5f) / 5f;
                col.a = trans;
                r.material.SetColor("_Color", col);
                li.intensity = Mathf.Min(trans * .8f + .2f, 1f);
                

            }
            playerPos = player.transform.position;
            this.transform.position = Vector3.MoveTowards(this.transform.position, playerPos, speed * Time.deltaTime);
            Vector3 endVector = transform.position;
            this.transform.rotation = Quaternion.LookRotation((startVector - endVector), Vector3.back);
            transform.Rotate(Vector3.right * -90);
            transform.Rotate(Vector3.forward * 180);
            isColliding = false;
        }
        
        
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
                baseSpeed/=1.5f;
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
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        // Code to execute after the delay
    }
}
