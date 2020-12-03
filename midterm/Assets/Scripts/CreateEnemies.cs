using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Globals
{
    public static int score;
    public static int highScore;
    public static void changeScore(int s) {
        score += s;
        if (score>highScore) {
            highScore = score;
            PlayerPrefs.SetInt("highscore", highScore);
            PlayerPrefs.Save();
        }
        //Debug.Log("score: "+score/2);
    }
    public static void setScore(int s) {
        score = s;
    }
}
public class CreateEnemies : MonoBehaviour
{
    // Start is called before the first frame update
    public int level;
    public GameObject[] getCount;
    public bool ghostsLeft;
    void Start()
    {
        level = 0;
        Globals.score = 0;
    }

    // Update is called once per frame
    void Update()
    {
       
        getCount = GameObject.FindGameObjectsWithTag("ghost");

        if (getCount.Length <= 1) {
            level++;
            for (int i = 0; i < level * 2; i++)
            {
               // Debug.Log("i:"+i);
                if (i >= 4 && i % 2 == 1)
                {
                   
                    if (Random.Range(0f, 1f) >= .8f) { 
                        makeGhostwall(); 
                    }
                }
                else makeGhost();
            }
            makeObstacle();
        }
        
        
    }
    void makeGhost() {
        Vector3 loc;
        do {
            loc = new Vector3(Random.Range(25, -25), Random.Range(-12, 12), -1);
            }
        while (Vector3.Distance(loc, GameObject.Find("Player").transform.position) < 10);
        GameObject ghost = Instantiate(Resources.Load("ghost"), loc,
            Quaternion.identity) as GameObject;
    }
    void makeObstacle() {
        Vector3 loc;
        do
        {
            loc = new Vector3(Random.Range(25, -25), Random.Range(-12, 12), 1/3);
        }
        while (Vector3.Distance(loc, GameObject.Find("Player").transform.position) < 4);
        GameObject obst = Instantiate(Resources.Load("obstacle"), loc,
           Quaternion.identity) as GameObject;
    }
    void makeGhostwall()
    {
        Vector3 loc;
        do
        {
            loc = new Vector3(Random.Range(23, -23), Random.Range(-10, 10), 1 / 3);
        }
        while (Vector3.Distance(loc, GameObject.Find("Player").transform.position) < 4);
        GameObject obst = Instantiate(Resources.Load("ghostWall"), loc,
           Quaternion.identity) as GameObject;
    }
    
}
