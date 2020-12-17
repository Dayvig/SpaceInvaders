using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRackScript : MonoBehaviour
{

    public float speed;
    public GameObject Ammo;
    public float rackSpacing;
    public float rackTimer = 0f;
    int direction = 1;
    Vector3 nextPos;
    BoxCollider2D rightCollider;
    BoxCollider2D leftCollider;
    public GameObject LeftEdge;
    public GameObject RightEdge;
    int buildup = 0;
    public float lifeTime = 30f;

    // Start is called before the first frame update
    void Start()
    {
        LeftEdge = GameObject.Find("LeftEdge");
        RightEdge = GameObject.Find("RightEdge");
        rightCollider = RightEdge.GetComponent<BoxCollider2D>();
        leftCollider = LeftEdge.GetComponent<BoxCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        rackTimer += Time.deltaTime;
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0f)
        {
            Destroy(this.gameObject);
        }
        if (rackTimer > rackSpacing)
        {
            buildup++;
            if (buildup > 1)
            {
                shoot(Ammo);
                buildup = 0;
            }
            nextPos = transform.position + (Vector3.right * speed * direction);
            transform.position = nextPos;
            rackTimer = 0f;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("test1");
        if (col == (rightCollider))
        {
            Debug.Log("test2");
            direction = -1;
        }
        else if (col == (leftCollider))
        {
            direction = 1;
        }
    }

        void shoot(GameObject a)
    {
        Vector3 proxy = new Vector3 (1.5f, 0, 0);
        for (int i = 0;i<10; i++)
        {
            Instantiate(a, this.transform.position - (proxy * 5) + (proxy * i), this.transform.rotation);
        }
    }
}

