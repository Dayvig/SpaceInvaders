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
    Vector3 nextPos;
    public KeyCode rightArrow;
    public KeyCode leftArrow;
    public KeyCode downArrow;
    public KeyCode upArrow;
    public KeyCode shootButton;
    bool cannotMoveRight = false;
    bool cannotMoveLeft = false;
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

    // Start is called before the first frame update
    void Start()
    {
        rightCollider = RightEdge.GetComponent<BoxCollider2D>();
        leftCollider = LeftEdge.GetComponent<BoxCollider2D>();
        bottomCollider = BottomEdge.GetComponent<BoxCollider2D>();
        transform.position = InitialPosition;
        nextPos = InitialPosition;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
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
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == ("EnemyBullet") && !flickerOn)
        {
            shootSound.PlayOneShot(explosionSound, 0.8f);
            flickerOn = true;
            //reduce player lives
            //if out of lives, lose state
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
