using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBehaviour : MonoBehaviour
{
    public GameObject blood;
    public enum entityType { damage, checkpoint, extralife }
    public entityType entity;
    private bool hasActivated;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            switch (entity)
            {
                case entityType.damage:
                    {

                        var pC = other.GetComponent<PlayerControl>();
                        if(pC.pCON)
                        {
                            StartCoroutine(pC.PlayerDeath());
                        }
                        GameObject bl = Instantiate(blood) as GameObject;
                        bl.transform.position = transform.position;
                        StartCoroutine(DestroyBlood(bl));
                        break;
                    }
                case entityType.checkpoint:
                    {
                        if(!hasActivated)
                        {
                            var pC = other.GetComponent<PlayerControl>();
                            GetComponent<Animator>().SetBool("isActive", true);
                            GetComponent<AudioSource>().Play();
                            pC.spawnPos = transform.position;
                            pC.lives = 3;
                            pC.ResetBodies();
                            hasActivated = true;

                        }
                        break;
                    }
                case entityType.extralife:
                    {
                        var pL = other.GetComponent<PlayerControl>();
                        GetComponent<AudioSource>().Play();
                        if (pL.lives < 3)
                        {
                            pL.lives += 1;
                        }
                        GetComponent<SpriteRenderer>().enabled = false;
                        GetComponent<Collider2D>().enabled = false;
                        StartCoroutine(DestroyBlood(gameObject));
                        break;
                    }
            }
        }
            
        
    }
    private IEnumerator DestroyBlood(GameObject gO)
    {
        yield return new WaitForSeconds(1f);
        Destroy(gO);
    }
}
