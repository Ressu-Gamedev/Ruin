using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehviour : MonoBehaviour
{
    public GameObject spark;
    private Rigidbody2D rb;
    public float direc;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if(direc <= -1)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2(5f*direc, 0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Shooter"))
        {
            GameObject sp = Instantiate(spark) as GameObject;
            sp.transform.position = transform.position;
            StartCoroutine(DestroySpark(sp));
            transform.position = new Vector2(0, 100);
        }
    }

    private IEnumerator DestroySpark(GameObject gO)
    {
        yield return new WaitForSeconds(1f);
        Destroy(gO);
        Destroy(gameObject);
    }
}
