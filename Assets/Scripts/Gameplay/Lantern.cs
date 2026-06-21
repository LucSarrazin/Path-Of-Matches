using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

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

        Material[] mats = renderer.materials;

        for (int i = 0; i < mats.Length; i++)
        {
            Debug.Log("Material " + i + " est : " + mats[i].name + " ou ça : " +  mats[i]);
            if (i == 0)
            {
                mats[i] = mat;
            }
        }

        renderer.materials = mats;
    }
}
