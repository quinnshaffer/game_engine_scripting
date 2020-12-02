using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroid : MonoBehaviour
{
    spaceship mover;
    //Mouse coordinates
    Vector2 inWorldMousePosition;

    float turnSpeed = 1f;
    float moveSpeed = 0f;
    float topSpeed = 15f;
    float angle = 0f;
    float acceleration = .05f;

    // Start is called before the first frame update
    void Start()
    {
        mover = new spaceship();
        inWorldMousePosition = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {

        //inWorldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //float gotoX = inWorldMousePosition.x - mover.location.x;
        //float gotoY = inWorldMousePosition.y - mover.location.y;

        //float angle = Mathf.Atan2(gotoX, gotoY);
        //angle += Mathf.Deg2Rad*turnSpeed;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            angle -= Mathf.Deg2Rad * turnSpeed;
            //Debug.Log("left");
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            angle += Mathf.Deg2Rad * turnSpeed;

            //Debug.Log("right");
        }
        if (Input.GetKey(KeyCode.Z))
        {
            moveSpeed += acceleration;
        }
        else {
            moveSpeed -= acceleration*2;
        }
        moveSpeed = Mathf.Clamp(moveSpeed, 0, topSpeed);
        //angle += angle;
        //angle = angle%(2*Mathf.PI);
        //angle = 3.15f;
        Debug.Log(angle);
        if (angle > Mathf.PI)
        {
            angle %= Mathf.PI;
            angle = Mathf.PI * -1 + angle;
        }
        else if (angle < -Mathf.PI)
        {
            angle %= Mathf.PI;
            angle = Mathf.PI + angle;
        }
        //angle -= angle;
        //angle=Mathf.Clamp(angle,-1*Mathf.PI+.01f, Mathf.PI-.01f);
        mover.Translate(moveSpeed);
        mover.Rotate(angle, turnSpeed);
        //Debug.Log(angle);
        mover.Update();
    }
}

public class spaceship
{
    // The basic properties of a mover class
    public Vector2 location, velocity, acceleration;
    public float mass;

    private Vector2 minimumPos, maximumPos;

    private GameObject mover;

    public spaceship()
    {
        mover = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Renderer renderer = mover.GetComponent<Renderer>();
        renderer.material = new Material(Shader.Find("Diffuse"));
        renderer.material.color = Color.black;

        mover.transform.localScale = new Vector3(0.5f, 1, 0.5f);

        mass = 1;
        location = Vector2.zero;
        velocity = Vector2.zero;
        acceleration = Vector2.zero;
        findWindowLimits();
    }

    public void Translate(float speed)
    {
        velocity = (Vector2)mover.transform.up * speed;
    }

    public void Rotate(float radiansAngle, float turnSpeed)
    {
        float eularAngle = (-radiansAngle * Mathf.Rad2Deg) + 180;
        float toSpin = eularAngle - ((mover.transform.eulerAngles.z + 180) % 360);
        if (toSpin > 180 || toSpin < -180)
        {
            toSpin %= 180;
            toSpin *= -1;
        }

        toSpin = Mathf.Clamp(toSpin, -turnSpeed, turnSpeed);
        mover.transform.Rotate(new Vector3(0, 0, toSpin));
    }

    public void Update()
    {
        velocity += acceleration * Time.deltaTime;
        location += velocity * Time.deltaTime;

        acceleration = Vector2.zero;


        mover.transform.position = location;
        CheckEdges();
    }

    public void CheckEdges()
    {
        if (location.x > maximumPos.x)
        {
            location.x = minimumPos.x;
        }
        else if (location.x < minimumPos.x)
        {
            location.x = maximumPos.x;
        }
        if (location.y > maximumPos.y)
        {
            location.y = minimumPos.y;
        }
        else if (location.y < minimumPos.y)
        {
            location.y = maximumPos.y;
        }
    }

    private void findWindowLimits()
    {
        Camera.main.orthographic = true;
        Camera.main.orthographicSize = 8;
        minimumPos = Camera.main.ScreenToWorldPoint(Vector2.zero);
        maximumPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }
}