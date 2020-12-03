using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{

    bool MarkForDestruction = false;
    public BasicEnemyMovement basic;

    // Start is called before the first frame update
    void Start()
    {
        basic = GameObject.Find("EnemyRack").GetComponent<BasicEnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MarkForDestruction)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == ("PlayerBullet"))
        {
            basic.playExplosionSound();
            MarkForDestruction = true;
            basic.reduceEnemyCount();
            Destroy(col.gameObject);
        }
    }

}
