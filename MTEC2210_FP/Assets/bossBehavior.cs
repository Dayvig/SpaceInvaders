using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossBehavior : MonoBehaviour
{

    private Vector2 InitialPosition = new Vector3(-1f, 11f, 0);
    bool floatIn = true;
    Vector2 nextPos;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = InitialPosition;
    }

    // Update is called once per frame
    void Update()
    {
        updateGraphics();
    }

    void updateGraphics()
    {
        if (floatIn)
        {
            nextPos = transform.position + (Vector3.down * 0.45f * Time.deltaTime);
            transform.position = nextPos;
            Debug.Log(transform.position.y);
            if (transform.position.y <= 7f)
            {
                floatIn = false;
            }
        }
    }

}
