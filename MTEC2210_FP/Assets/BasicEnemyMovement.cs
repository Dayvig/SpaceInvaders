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
    BoxCollider2D rackCollider;
    int direction = 1;
    bool moveDown = false;
    Vector3 nextPos;
    public AudioClip[] moveSounds = new AudioClip[3];
    public AudioClip explosion;
    int currentClip = 0;
    AudioSource moveSource;

    int enemyCount = 40;

    public GameObject[] columnList = new GameObject[10];
    public ColumnScript[] scripts = new ColumnScript[10];
    int leftColumn = 0;
    int rightColumn = 9;
    private ColumnScript sc;

    Vector2 enemyOffset = new Vector2(1.5f, 0);

    public int shootSpacing = 8;
    int shootTimer = 0;
    public AudioClip shootSound;

    public GameObject Ammo;

    // Start is called before the first frame update
    void Start()
    {
        rightCollider = RightEdge.GetComponent<BoxCollider2D>();
        leftCollider = LeftEdge.GetComponent<BoxCollider2D>();
        bottomCollider = BottomEdge.GetComponent<BoxCollider2D>();
        topCollider = TopEdge.GetComponent<BoxCollider2D>();
        moveSource = GetComponent<AudioSource>();
        transform.position = InitialPosition;
        rackCollider = GetComponent<BoxCollider2D>();

        for(int i = 0; i < columnList.Length; i++)
        {
            scripts[i] = columnList[i].GetComponent<ColumnScript>();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        stepTimer += Time.deltaTime;
        if (stepTimer >= stepSpacing)
        {
            if (scripts[leftColumn].checkIfEmpty())
            {
                leftColumn++;
                shiftHitBoxRight();
            }
            if (scripts[rightColumn].checkIfEmpty())
            {
                rightColumn--;
                shiftHitBoxLeft();
            }
            takeStep();
        }

        if (shootTimer >= shootSpacing)
        {
            shoot();
            shootTimer = 0;
        }
    }

    void takeStep()
    {
        shootTimer += Random.Range(1, 4);
        playMoveSound();
        if (moveDown)
        {
            nextPos = transform.position + (Vector3.down * movementSpeed);
        }
        else
        {
            nextPos = transform.position + (Vector3.right * movementSpeed * direction);
        }
        moveDown = false;
        transform.position = nextPos;
        stepTimer = 0;
    }

    void shoot()
    {
        int randomColumn = (int)Random.Range(leftColumn, rightColumn+1);
        while (scripts[randomColumn].checkIfEmpty())
        {
            randomColumn = (int)Random.Range(leftColumn, rightColumn+1);
        }
        scripts[randomColumn].shoot(Ammo);
        moveSource.PlayOneShot(shootSound, 0.2f);
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

    public void shiftHitBoxRight()
    {
        rackCollider.size -= enemyOffset;
        rackCollider.offset += (enemyOffset / 2);
    }
    public void shiftHitBoxLeft()
    {
        rackCollider.size -= enemyOffset;
        rackCollider.offset -= (enemyOffset / 2);
    }
}
