using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Torch : MonoBehaviour
{
    [Header("[SETTINGS]")]
    //* If it's a fire camp = TRUE
    //* Because it create a new SAVE on light ON
    //* Maybe change this name, to be more clear and avoir mistakes ? 
    [Tooltip("Change in inspector ONLY if it's a fire camp -> TRUE, it allows a new save")]
    [SerializeField] private bool _allowSave = false;

    [Header("[REFERENCES]")]
    private bool oneTime = false;
    [SerializeField] private Material _colorOff;
    [SerializeField] private Material _colorOn;
    [SerializeField] private GameObject _particle;
    [SerializeField] private GameObject _pointLight;
    [SerializeField] private bool _safeZoneTorch;
    [SerializeField] private bool _destroyObjectAfter;
    [SerializeField] private bool _torchDestroyFog;
    [SerializeField] private float _timeForDisapearing;
    [SerializeField] private FogZone _fogZone;
    [SerializeField] private Renderer _meshRenderer;
    public UnityEvent onDestroyed;
    private float timer;
    //private SkinnedMeshRenderer _skinnedMeshRenderer;

    void Start()
    {
        //_skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        _meshRenderer = GetComponent<MeshRenderer>();
        if (_meshRenderer == null)
        {
            _meshRenderer = GetComponent<SkinnedMeshRenderer>();
        }
        if (_colorOff != null)
        {
            _meshRenderer.material = _colorOff;
        }
        _pointLight.SetActive(false);
        if (_particle != null)
        {
            _particle.SetActive(false);
        }
    }

    void Update()
    {
        if (_colorOn != null)
        {
            timer += Time.deltaTime;
            _meshRenderer.material.SetFloat("_EffectTime", timer);
        }
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

                /* Save here : allow re-save on another fireCamp */
                /* try by sending fire camp position */

                if (_allowSave) { GameEvents.OnAutoSaveRequested?.Invoke(this.transform, false); }

                if (_particle != null)
                {
                    _particle.SetActive(true);
                }
                if (_colorOn != null)
                {
                    _meshRenderer.material = _colorOn;
                    _meshRenderer.material.SetFloat("_EffectTime", 0);
                }

                if (_safeZoneTorch == false)
                {
                    StartCoroutine(waitBeforeTurningOff());
                }
                
                if (_torchDestroyFog == true)
                {
                    _fogZone.disableFog();
                }
            }
        }
    }

    IEnumerator waitBeforeTurningOff()
    {
        yield return new WaitForSeconds(_timeForDisapearing);
        _pointLight.SetActive(false);
        if (_particle != null)
        {
            _particle.SetActive(false);
        }
        if (_colorOff != null)
        {
            _meshRenderer.material = _colorOff;
        }
        oneTime = false;
        if (_destroyObjectAfter == true)
        {
            onDestroyed?.Invoke();
            Destroy(gameObject);
        }
    }
}
