using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteBullets : MonoBehaviour
{
    BoxCollider2D thisCollider;

    // Start is called before the first frame update
    void Start()
    {
        thisCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == ("PlayerBullet"))
        {
            Destroy(col.gameObject);
        }
    }
}
