using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossBehavior : MonoBehaviour
{

    private Vector2 InitialPosition = new Vector3(-1f, 11f, 0);
    bool floatIn = true;
    Vector2 nextPos;
    int phase = 1;
    public GameObject mainFan;
    public float phaseTimer = 20f;
    public GameObject rackPattern;

    int halfSpawns = 0;

    public float startingHp = 3000f;
    float hp;
    public GameObject healthBar;

    GameObject gameManager; 
    GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        hp = startingHp;
        transform.position = InitialPosition;
        manager = GameObject.Find("Background").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            manager.gameState = 3;
        }
        phaseTimer -= Time.deltaTime;
        updateGraphics();
        updatePhase(phase);
        if (phaseTimer <= 0f)
        {
            switch (phase)
            {
                case 1:
                    RackPhaseSpawn();
                    return;
            }
        }
    }

    void updateGraphics()
    {
        healthBar.transform.localScale = new Vector3 (3f * (hp / startingHp), 0.15f, 1);
        if (floatIn)
        {
            nextPos = transform.position + (Vector3.down * 0.55f * Time.deltaTime);
            transform.position = nextPos;
            if (transform.position.y <= 7f)
            {
                floatIn = false;
                Instantiate(mainFan, this.transform.position, this.transform.rotation);
            }
        }
    }
    
    void updatePhase(int p)
    {
        switch (p)
        {
            case 1:
                if (phaseTimer <= 30f && halfSpawns > 0)
                {
                    GameObject tmp = Instantiate(rackPattern, this.transform.position, this.transform.rotation);
                    tmp.GetComponent<BulletRackScript>().rackTimer += 0.1f;
                    tmp.transform.position += Vector3.right * 0.5f;
                    halfSpawns--;
                }
                return;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == ("PlayerBullet"))
        {
            hp -= 1;
            Destroy(col.gameObject);
        }
    }

    void RackPhaseSpawn()
    {
        GameObject tmp = Instantiate(rackPattern, this.transform.position, this.transform.rotation);
        tmp.GetComponent<BulletRackScript>().lifeTime = 45f;
        phaseTimer = 45f;
        halfSpawns = 1;
    }



}
