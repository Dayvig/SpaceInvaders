using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyMovement : MonoBehaviour
{
    private Vector3 InitialPosition = new Vector3(-11f, 7f, 0);
    float stepTimer = 0;
    public float stepSpacing;
    public GameObject LeftEdge;
    public GameObject RightEdge;
    public GameObject TopEdge;
    public GameObject BottomEdge;
    public float movementSpeed;
    BoxCollider2D rightCollider;
    BoxCollider2D leftCollider;
    BoxCollider2D bottomCollider;
    BoxCollider2D topCollider;
    int direction = 1;
    bool moveDown = false;
    Vector3 nextPos;
    public AudioClip[] moveSounds = new AudioClip[3];
    public AudioClip explosion;
    int currentClip = 0;
    AudioSource moveSource;

    int enemyCount = 40;

    // Start is called before the first frame update
    void Start()
    {
        rightCollider = RightEdge.GetComponent<BoxCollider2D>();
        leftCollider = LeftEdge.GetComponent<BoxCollider2D>();
        bottomCollider = BottomEdge.GetComponent<BoxCollider2D>();
        topCollider = TopEdge.GetComponent<BoxCollider2D>();
        moveSource = GetComponent<AudioSource>();
        transform.position = InitialPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        stepTimer += Time.deltaTime;
        if (stepTimer >= stepSpacing)
        {
            takeStep();
        }
    }

    void takeStep()
    {
        playMoveSound();
        if (moveDown)
        {
            nextPos = transform.position + (Vector3.down * movementSpeed);
        }
        else
        {
            nextPos = transform.position + (Vector3.right * movementSpeed * direction);
        }
        Debug.Log(nextPos);
        moveDown = false;
        transform.position = nextPos;
        stepTimer = 0;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col == (rightCollider))
        {
            direction = -1;
            moveDown = true;
        }
        else if (col == (leftCollider))
        {
            direction = 1;
            moveDown = true;
        }
        else if (col == (bottomCollider))
        {
            //lose state
        }
        else if (col == topCollider)
        {
            moveDown = true;
        }
    }

    void playMoveSound()
    {
        moveSource.PlayOneShot(moveSounds[currentClip]);
        currentClip = Random.Range(0, 4);
    }
    public void playExplosionSound()
    {
        moveSource.PlayOneShot(explosion, 0.3f);
    }

    public void reduceEnemyCount()
    {
        enemyCount--;
        if (enemyCount == 19) 
        {
            stepSpacing /= 2;
        }
        else if (enemyCount == 4){
            stepSpacing /= 2;
        }
    }
}
