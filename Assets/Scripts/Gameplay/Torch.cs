using System;
using UnityEngine;

public class Torch : MonoBehaviour
{
    private bool oneTime = false;
    [SerializeField] private Material _colorOff;
    [SerializeField] private Material _colorOn;
    [SerializeField] private GameObject _pointLight;
    private MeshRenderer _meshRenderer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.material = _colorOff;
        _pointLight.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Matches"))
        {
            Debug.Log("Matches touch the torch");

            if (!oneTime)
            {
                _pointLight.SetActive(true);
                _meshRenderer.material = _colorOn;
                oneTime = true;
            }
        }
    }
}
