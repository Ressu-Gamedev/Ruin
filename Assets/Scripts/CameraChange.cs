using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CameraChange : MonoBehaviour
{
    public float[] cRange = new float[4];
    public int level = 1;
    public GameObject gameEnd;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var cF = FindObjectOfType<CameraFollow>();
            var lM = cF.gameObject.GetComponent<LevelManagement>();
            lM.NextLevel(level);
            for (int i = 0; i < 4; i++)
                cF.posLimit[i] = cRange[i];
        }

        if (level == 2)
        {
            StartCoroutine(GameEnd());
        }
    }
    private IEnumerator GameEnd()
    {
        gameEnd.SetActive(true);
        yield return new WaitForSeconds(7f);
        var t = gameEnd.GetComponent<TextMeshProUGUI>();
        t.alpha = 0f;
        while (t.alpha < 1f)
        {
            t.alpha += 0.01f;
            yield return null;
        }
        yield return new WaitForSeconds(20f);
        SceneManager.LoadScene("menu");

    }

}
