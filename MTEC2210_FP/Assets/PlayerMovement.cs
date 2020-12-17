using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject LeftEdge;
    public GameObject RightEdge;
    public GameObject TopEdge;
    public GameObject BottomEdge;
    public float movementSpeed;
    BoxCollider2D rightCollider;
    BoxCollider2D leftCollider;
    BoxCollider2D bottomCollider;
    BoxCollider2D topCollider;
    Vector3 nextPos;
    public KeyCode rightArrow;
    public KeyCode leftArrow;
    public KeyCode downArrow;
    public KeyCode upArrow;
    public KeyCode shootButton;
    bool cannotMoveRight = false;
    bool cannotMoveLeft = false;
    bool cannotMoveUp = false;
    bool cannotMoveDown = false;
    private Vector3 InitialPosition = new Vector3(0, -9.45f, 0);
    float shootTimer = 0;
    public float shootSpacing;
    public GameObject Ammo;
    public AudioSource shootSound;
    public AudioClip explosionSound;

    public float flickerSpacing = 0.2f;
    float flickerTimer;
    public int baseFlickerAmount = 3;
    int flickerAmount = 0;
    bool flickerOn = false;

    SpriteRenderer sr;
    public GameManager manager;

    bool holdingUp = false;
    bool holdingRight = false;
    bool holdingDown = false;
    bool holdingLeft = false;

    int mode = 0;
    BoxCollider2D thisCollider;

    // Start is called before the first frame update
    void Start()
    {
        rightCollider = RightEdge.GetComponent<BoxCollider2D>();
        leftCollider = LeftEdge.GetComponent<BoxCollider2D>();
        bottomCollider = BottomEdge.GetComponent<BoxCollider2D>();
        topCollider = TopEdge.GetComponent<BoxCollider2D>();
        transform.position = InitialPosition;
        nextPos = InitialPosition;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (manager.gameState == 0)
        {
            updateGraphics();
            shootTimer += Time.deltaTime;
            if (Input.GetKey(rightArrow) && !cannotMoveRight)
            {
                nextPos = transform.position + (Vector3.right * movementSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(leftArrow) && !cannotMoveLeft)
            {
                nextPos = transform.position + (Vector3.right * -movementSpeed * Time.deltaTime);
            }
            if (Input.GetKey(shootButton) && !flickerOn)
            {
                if (shootTimer >= shootSpacing)
                {
                    shoot();
                    shootTimer = 0;
                }
            }
            transform.position = nextPos;
            cannotMoveRight = false;
            cannotMoveLeft = false;
        }

        else if (manager.gameState == 2)
        {
            if (mode != 1) {
                shootSpacing /= 20;
                thisCollider = GetComponent<BoxCollider2D>();
                thisCollider.size = new Vector2(0.2f, 0.2f);
                mode = 1; 
            }
            updateGraphics();
            shootTimer += Time.deltaTime;
            if (Input.GetKey(rightArrow) && !cannotMoveRight)
            {
                holdingRight = true;
            }
            if (Input.GetKey(leftArrow) && !cannotMoveLeft)
            {
                holdingLeft = true;
            }
            if (Input.GetKey(upArrow) && !cannotMoveUp)
            {
                holdingUp = true;
            }
            if (Input.GetKey(downArrow) && !cannotMoveDown)
            {
                holdingDown = true;
            }
            if (Input.GetKey(shootButton) && !flickerOn)
            {
                if (shootTimer >= shootSpacing)
                {
                    shoot();
                    shootTimer = 0;
                }
            }
            nextPos = transform.position;
            if (holdingRight)
            {
                nextPos += (Vector3.right * movementSpeed * Time.deltaTime);
            }
            if (holdingLeft)
            {
                nextPos += (Vector3.right * -movementSpeed * Time.deltaTime);
            }
            if (holdingUp)
            {
                nextPos += (Vector3.up * movementSpeed * Time.deltaTime);
            }
            if (holdingDown)
            {
                nextPos += (Vector3.up * -movementSpeed * Time.deltaTime);
            }
            holdingRight = false;
            holdingLeft = false;
            holdingUp = false;
            holdingDown = false;

            transform.position = nextPos;
            cannotMoveRight = false;
            cannotMoveLeft = false;
            cannotMoveDown = false;
            cannotMoveUp = false;
        }
    }

void OnTriggerStay2D(Collider2D col)
    {
        if (col == (rightCollider))
        {
            cannotMoveRight = true;
        }
        else if (col == (leftCollider))
        {
            cannotMoveLeft = true;
        }
    else if (col == (topCollider))
    {
        cannotMoveUp = true;
    }
        else if (col == (bottomCollider))
    {
        cannotMoveDown = true;
    }
}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == ("EnemyBullet") && !flickerOn)
        {
            shootSound.PlayOneShot(explosionSound, 0.8f);
            flickerOn = true;
            manager.reduceLives();
        }
    }


    void shoot()
    {
        shootSound.Play();
        Instantiate(Ammo, transform.position, transform.rotation);
    }

    void updateGraphics()
    {
        if (flickerOn)
        {
            flickerTimer += Time.deltaTime;
            if (flickerTimer >= flickerSpacing)
            {
                sr.enabled = !sr.enabled;
                flickerTimer = 0;
                flickerAmount++;
            }
            if (flickerAmount > baseFlickerAmount * 2)
            {
                sr.enabled = true;
                flickerOn = false;
                flickerTimer = 0;
                flickerAmount = 0;
            }
        }
    }
}
