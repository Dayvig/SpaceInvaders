using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletMovement : MonoBehaviour
{

    public float movementSpeed;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.down * movementSpeed);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == ("Shield"))
        {
            Destroy(this.gameObject);
        }
    }

}
