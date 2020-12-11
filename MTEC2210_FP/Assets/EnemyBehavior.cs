using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{

    bool MarkForDestruction = false;
    public BasicEnemyMovement basic;
    GameObject thisColumn;
    ColumnScript sc;

    // Start is called before the first frame update
    void Start()
    {
        basic = GameObject.Find("EnemyRack").GetComponent<BasicEnemyMovement>();
        thisColumn = this.transform.parent.gameObject;
        sc = thisColumn.GetComponent<ColumnScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MarkForDestruction)
        {
            basic.reduceEnemyCount();
            sc.childObjects.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == ("PlayerBullet"))
        {
            basic.playExplosionSound();
            MarkForDestruction = true;
            Destroy(col.gameObject);
        }
    }

}
