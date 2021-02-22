using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public Vector3[] destinations = new Vector3[2];
    //public Vector2 origin;
    private Vector2 dir;

    private int curDes;

    public float speed = 2;

    private Rigidbody2D rb;

    private bool moving = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        for (int i = 0; i < destinations.Length; i++)

        destinations[i] = transform.position + destinations[i];
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.collider.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.collider.transform.SetParent(null);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(moving)
        {
            if(transform.position == destinations[curDes])
            {
                curDes++;
                if(curDes > 1)
                {
                    curDes = 0;
                }
            }
                transform.position = Vector3.MoveTowards(transform.position, destinations[curDes], speed * Time.deltaTime);
            
        }
    }
}
