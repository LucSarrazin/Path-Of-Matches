using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UI;

public class Screamer : MonoBehaviour
{
    public List<Texture2D> listScreamers = new List<Texture2D>();

    private RawImage screamer;
    private Insanity player;
    private bool wait;

    void Start()
    {
        screamer = gameObject.GetComponent<RawImage>();
        player = FindAnyObjectByType<Insanity>();
        wait = false;
        screamer.color = new Color(1, 1, 1, 0);
    }

    void Update()
    {
        if (player.InsanityLvl > 0 && Random.Range(0, 2000 - 250 * (player.InsanityLvl - 1)) == 0 && wait == false)
        {
            screamer.color = new Color(1, 1, 1, 1);
            screamer.texture = listScreamers[Random.Range(0, listScreamers.Count)];
            wait = true;
            StartCoroutine(ActiveScreamer());
        }
    }

    IEnumerator ActiveScreamer()
    {
        yield return new WaitForSeconds(5);
        screamer.color = new Color(1, 1, 1, 0);
        wait = false;
    }
}
