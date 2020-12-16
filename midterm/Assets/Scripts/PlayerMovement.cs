using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public Light flashLight;
    bool lightOn, useCharge;
    public float charge=5;
    public float percentCharge;
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        Globals.highScore = PlayerPrefs.GetInt("highscore", 0);
        flashLight = GameObject.FindGameObjectWithTag("flashLight").GetComponent<Light>();
        cam = GameObject.FindObjectOfType<Camera>();
        Globals.lightOn = true;
        Globals.charge = charge;
        useCharge = true;
       
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;    
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        percentCharge = Globals.charge/charge;
        percentCharge = Mathf.Pow(percentCharge,2);
        if (Globals.lightOn)
        {
            if (useCharge)
            {
                if (Globals.charge > 1)
                {
                    Globals.charge -= Time.deltaTime;
                    flashLight.intensity = percentCharge * 5;
                    flashLight.range = percentCharge * 20;
                }
                else
                {
                    Globals.charge = 1;
                    Globals.lightOn = false;
                    audioControl.playSound("bleep");
                }
            }
            else 
            {
                flashLight.intensity = 5;
                flashLight.range = 20;
                Globals.percentCharge = .2f;
            }
        }
        else 
        {
            flashLight.intensity = 0;
            if(Globals.charge<charge) Globals.charge += Time.deltaTime*2f;
        }
       
        if (v != 0 || h != 0) {
            gameObject.transform.position = new Vector2(transform.position.x + (h * step),
               transform.position.y + (v * step));
        }
        
            //this.transform.rotation = Quaternion.LookRotation(new Vector2(h, v), Vector3.forward);
        this.transform.rotation = Quaternion.LookRotation(Input.mousePosition - cam.WorldToScreenPoint(transform.position), Vector3.forward);
        Globals.percentCharge = percentCharge;
        Globals.playerRotation = transform.rotation;
        transform.Rotate(Vector3.up * 90);



        //flashLight.transform.rotation = Quaternion.AngleAxis(90f,new Vector3(0f, 0f, 1f));
        if (Input.GetKeyDown("r")) {
            Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        }
        if (Input.GetKeyDown("0")) Globals.setHighScore(0);
        if (Input.GetKeyDown("l")) useCharge=!useCharge;
        if (Input.GetMouseButtonDown(0)) toggleLight();
    }
    void toggleLight() {
        
        Globals.lightOn = !Globals.lightOn;
        audioControl.playSound("bleep");
    }
}
