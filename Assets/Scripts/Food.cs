using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D gridArea;


    private void Start()
    {
        RandomizePosition();
    }


    void RandomizePosition()
    {
        Bounds bounds = gridArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        // Ensure that apple won't spawn into snake's body

        while (Snake.Instance._bodiesOfSnake.Exists(t=>t.position==new Vector3(x,y))){
            x = Random.Range(bounds.min.x, bounds.max.x);
            y = Random.Range(bounds.min.y, bounds.max.y);
        }
            
        transform.position = new Vector3(Mathf.RoundToInt(x), Mathf.RoundToInt(y));
            
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            RandomizePosition();
        }
    }

}
