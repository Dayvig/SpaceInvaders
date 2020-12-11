using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    int hp;
    public int startingHP;
    SpriteRenderer sr;
    public AudioSource shootSound;
    public AudioClip explosionSound;

    // Start is called before the first frame update
    void Start()
    {
        hp = startingHP;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == ("EnemyBullet") || col.tag == ("PlayerBullet"))
        {
            shootSound.PlayOneShot(explosionSound, 0.1f);
            hp--;
            Color newColor = sr.color;
            newColor.a -= 0.2f;
            sr.color = newColor;
            Destroy(col.gameObject);
        }
    }

}
