using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnScript : MonoBehaviour
{

    public List<GameObject> childObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        BoxCollider2D[] allChildren = GetComponentsInChildren<BoxCollider2D>();
        foreach (BoxCollider2D child in allChildren)
        {
            childObjects.Add(child.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool checkIfEmpty()
    {
        foreach (GameObject g in childObjects)
        {
            if (g == null)
            {
                childObjects.Remove(g);
            }
        }
        return childObjects.Count <= 0;
    }
}
