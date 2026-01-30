using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseOfMadness : MonoBehaviour
{
    public int madnessLvl;
    public List<AudioClip> listSound = new List<AudioClip>();

    private Insanity player;
    private AudioSource sound;
    private bool wait;

    private void Start()
    {
        player = FindAnyObjectByType<Insanity>();
        sound = GetComponent<AudioSource>();
        wait = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && madnessLvl <= player.InsanityLvl && wait == false)
        {
            wait = true;
            StartCoroutine(PlaySound());
        }
    }

    IEnumerator PlaySound()
    {
        yield return new WaitForSeconds(Random.Range(25, 60));
        sound.generator = listSound[Random.Range(0, listSound.Count)];
        sound.Play();
        wait = false;
    }
}
