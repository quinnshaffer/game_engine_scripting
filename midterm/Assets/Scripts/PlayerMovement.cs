using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public Light flashLight;
    // Start is called before the first frame update
    void Start()
    {
        Globals.highScore = PlayerPrefs.GetInt("highscore", 0);
        flashLight = gameObject.GetComponent<Light>();
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
            Globals.playerRotation = transform.rotation;
            transform.Rotate(Vector3.up * 90);
        }
        //flashLight.transform.rotation = Quaternion.AngleAxis(90f,new Vector3(0f, 0f, 1f));
        if (Input.GetKeyDown("r")) Application.LoadLevel(Application.loadedLevel);
    }
}
