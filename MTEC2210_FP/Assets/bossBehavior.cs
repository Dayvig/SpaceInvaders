using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossBehavior : MonoBehaviour
{

    private Vector2 InitialPosition = new Vector3(-1f, 11f, 0);
    bool floatIn = true;
    Vector2 nextPos;
    int phase = 3;
    public GameObject mainFan;
    float phaseTimer = 10f;
    public GameObject rackPattern;
    public GameObject randomBurstPattern;

    int halfSpawns = 0;

    float startingHp = 2000f;
    float hp;
    public GameObject healthBar;

    GameObject gameManager; 
    GameManager manager;
    float miniTimer = 0f;

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
            if (phase != 0)
            {
                Reprieve();
                phase = 0;
            }
            else
            {
                phase = Random.Range(1, 2);
            }
            switch (phase)
            {
                case 1:
                    RackPhaseSpawn();
                    break;
                case 2:
                    RandomBurstSpawn();
                    break;
                default:
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
                break;
            case 2:
                miniTimer -= Time.deltaTime;
                if (miniTimer <= 0f)
                {
                    int randomPos = Random.Range(-16, 16);
                    GameObject tmp = Instantiate(randomBurstPattern, new Vector3(randomPos, this.transform.position.y, this.transform.position.z), this.transform.rotation);
                    tmp.GetComponent<bulletFanScript>().lifeTime = 1f;
                    tmp.GetComponent<bulletFanScript>().temporary = true;
                    miniTimer = 0.5f;
                }
                break;
            default:
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

    void Reprieve()
    {
        phaseTimer = 5f;
    }

    void RandomBurstSpawn()
    {
        int randomPos = Random.Range(-16, 16);
        GameObject tmp = Instantiate(randomBurstPattern, new Vector3 (randomPos, this.transform.position.y, this.transform.position.z), this.transform.rotation);
        tmp.GetComponent<bulletFanScript>().lifeTime = 1f;
        tmp.GetComponent<bulletFanScript>().temporary = true;
        phaseTimer = 40f;
        miniTimer = 0.5f;
    }

}
