using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterBehviour : MonoBehaviour
{
    public GameObject arrow;
    public float direc = 1;
    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(Shoot());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(1f);
        GameObject gO = Instantiate(arrow) as GameObject;
        gO.transform.position = transform.position + new Vector3(direc, 0f, 0f);
        gO.GetComponent<ArrowBehviour>().direc = direc;
        GetComponent<AudioSource>().Play();
        StartCoroutine(Shoot());
    }
}
