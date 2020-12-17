using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanBulletMovement : MonoBehaviour
{

    public float movementSpeed;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddRelativeForce(Vector2.down * movementSpeed);
    }
}

