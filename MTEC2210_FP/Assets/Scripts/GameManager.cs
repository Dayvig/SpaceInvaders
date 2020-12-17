using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject rack;
    BasicEnemyMovement enemyManager;
    public GameObject LifeTracker;

    public int gameState;
    public int startingLives = 3;
    int lives;
    public SpriteRenderer[] sr = new SpriteRenderer[3];

    public GameObject Boss;

    //0- Main gameplay (space invaders)
    //1- lose state
    //2- boss battle state
    //3- win state

    // Start is called before the first frame update
    void Start()
    {
        enemyManager = rack.GetComponent<BasicEnemyMovement>();
        gameState = 0;
        lives = startingLives;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyUp(KeyCode.G)){
            gameState = 2;
            Destroy(GameObject.Find("EnemyRack"));
            Instantiate(Boss, this.transform.position, this.transform.rotation);
        }
        if (enemyManager.enemyCount <= 0)
        {

        }
        sr[2].enabled = (lives >= 3);
        sr[1].enabled = (lives >= 2);
        sr[0].enabled = (lives >= 1);

    }

    public void reduceLives()
    {
        if (gameState != 1 && gameState != 3)
        {
            lives--;
            if (lives <= 0)
            {
                gameState = 1;
            }
        }
    }
}
