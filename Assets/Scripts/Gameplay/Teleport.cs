using System;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Vector3 teleportPosition;
    public Transform player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.name == "Player")
        {
            Debug.Log("tp joueur");
            player.localPosition = teleportPosition;
        }
    }
}
