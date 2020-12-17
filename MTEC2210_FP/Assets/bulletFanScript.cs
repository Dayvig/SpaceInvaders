using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletFanScript : MonoBehaviour
{

    public int num;
    public int spread;
    public GameObject Ammo;
    public float fanSpacing;
    float fanTimer = 0f;
    bool alt = true;
    public bool aimAtPlayer;
    public bool temporary = false;
    public GameObject player;
    public float lifeTime;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerCannon");
    }

    // Update is called once per frame
    void Update()
    {
        if (aimAtPlayer)
        {
            aimTowardsPlayer();
        }
        fanTimer += Time.deltaTime;
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0f && temporary)
        {
            Destroy(this.gameObject);
        }

        if (fanTimer > fanSpacing)
        {
            shootFan(num, spread, Ammo, alt);
            fanTimer = 0f;
            alt = !alt;
        }
    }

    void shootFan(int n, int s, GameObject a, bool rev)
    {
        if (n <= 1) { n = 2; }

        
        int offset1 = s / n;
        if (rev)
        {
            for (int k = 0; k < n; k++)
            {
                Vector3 euler1 = new Vector3(0, 0, (offset1 * k) - (s/2) + (offset1 / 2));
                Instantiate(a, this.transform.position, Quaternion.Euler(euler1) * this.transform.rotation);
            }
        }
        else
        {
            for (int k = 0; k <= n; k++)
            {
                Vector3 euler1 = new Vector3(0, 0, (offset1 * k) - (s/2));
                Instantiate(a, this.transform.position, Quaternion.Euler(euler1) * this.transform.rotation);
            }
        }
    }

    void aimTowardsPlayer()
    {
        float angle = Mathf.Tan((this.transform.position.x - player.transform.position.x) / (this.transform.position.y - player.transform.position.y)) * (180 / Mathf.PI);
        Vector3 euler = new Vector3 (0f, 0f, -angle);
        transform.rotation = Quaternion.Euler(euler);
    }

}
