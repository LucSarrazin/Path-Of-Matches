using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Lantern : MonoBehaviour
{
    [Header("[REFERENCES]")]
    private bool oneTime = false;
    [SerializeField] private Material _materialOn;
    [SerializeField] private Material _materialOff;
    [SerializeField] private GameObject _particle;
    [SerializeField] private GameObject _pointLight;
    [SerializeField] public string code;

    [Header("[REFERENCES]")]
    [SerializeField] private AudioSource _audioSourceLoop;
    [SerializeField] private AudioClip _igniteClip; 

    public UnityEvent onDestroyed;
    private float timer;

    void Start()
    {
        _pointLight.SetActive(false);
        if (_particle != null)
        {
            _particle.SetActive(false);
        }

        if (_audioSourceLoop == null) _audioSourceLoop = GetComponentInChildren<AudioSource>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Matches"))
        {
            //Debug.Log("Matches touch the torch");

            if (!oneTime)
            {
                oneTime = true;
                _pointLight.SetActive(true);
                _audioSourceLoop.PlayOneShot(_igniteClip); 
                _audioSourceLoop.Play();
                LanternManager.instance.AddCode(code);
                //Debug.Log("Try Play sound");
                ChangeMaterial(_materialOn);

                if (_particle != null)
                {
                    _particle.SetActive(true);
                    //Debug.Log($"Try Play {_audioSourceLoop.gameObject.name}"); 
                }
            }
        }
    }

    public void TurnOff()
    {
        _pointLight.SetActive(false);
        if (_particle != null)
        {
            _particle.SetActive(false);
            _audioSourceLoop.Stop();

            Debug.Log($"Try Stop {_audioSourceLoop.gameObject.name}"); 
        }

        ChangeMaterial(_materialOff);
        oneTime = false;
    }
    
    

    public void ChangeMaterial(Material mat)
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();

        Outline outline = GetComponent<Outline>();
        bool wasEnabled = false;
        if (outline != null)
        {
            wasEnabled = outline.enabled;
            outline.enabled = false;
        }

        Material[] mats = renderer.materials;
        if (mats.Length > 0) mats[0] = mat;
        renderer.materials = mats;

        if (outline != null)
            outline.enabled = wasEnabled;
    }
}
