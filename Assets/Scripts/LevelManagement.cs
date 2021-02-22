using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagement : MonoBehaviour
{
    private int level = 0;
    private AudioSource aS;
    public AudioClip[] aC = new AudioClip[3];

    
    // Start is called before the first frame update
    void Start()
    {
        aS = GetComponent<AudioSource>();
        StartCoroutine(LoopMusic());
    }

    private IEnumerator LoopMusic()
    {
        aS.clip = aC[level];
        aS.Play();

        yield return new WaitForSecondsRealtime(aC[level].length);
        StartCoroutine(LoopMusic());
    }
    // Update is called once per frame
    public void NextLevel(int lvl)
    {
        level = lvl;
        StopAllCoroutines();
        aS.Stop();
        StartCoroutine(LoopMusic());
    }
}
