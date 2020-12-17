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
    bool alt = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fanTimer += Time.deltaTime;
        if (fanTimer > fanSpacing)
        {
            if (alt)
            {
                shootFan(num, spread, Ammo);
            }
            else
            {
                shootFan(num-1, spread, Ammo);
            }
            fanTimer = 0f;
            alt = !alt;
        }
    }

    void shootFan(int n, int s, GameObject a)
    {
        if (n <= 1) { n = 2; }

        
        int offset1 = s / n;
        if (n % 2 == 0)
        {
            for (int k = 1; k <= n; k++)
            {
                Vector3 euler1 = new Vector3(0, 0, (offset1 * k) - (s/2 + offset1/2));
                Instantiate(a, this.transform.position, Quaternion.Euler(euler1));
            }
        }
        else
        {
            for (int k = 0; k < n; k++)
            {
                Vector3 euler1 = new Vector3(0, 0, (offset1 * k) - (s/2 - offset1/2));
                Instantiate(a, this.transform.position, Quaternion.Euler(euler1));
            }
        }
    }
}
