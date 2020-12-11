using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class loseText : MonoBehaviour
{

    public GameObject gameManager;
    GameManager manager;
    public TextMeshPro text;

    // Start is called before the first frame update
    void Start()
    {
        manager = gameManager.GetComponent<GameManager>();
        text.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.gameState == 2)
        {
            text.enabled = true;
        }
    }
}
