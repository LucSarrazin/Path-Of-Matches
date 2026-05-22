using System;
using System.Collections;
using UnityEngine;

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
    [SerializeField] private GameObject _pointLight;
    [SerializeField] private bool _safeZoneTorch;
    private MeshRenderer _meshRenderer;

    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.material = _colorOff;
        _pointLight.SetActive(false);
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Matches"))
        {
            Debug.Log("Matches touch the torch");
            /* Save here : allow re-save on another fireCamp */
            /* try by sending fire camp position */
            if (_allowSave) { GameEvents.OnAutoSaveRequested?.Invoke(this.transform); }

            if (!oneTime)
            {
                oneTime = true;
                _pointLight.SetActive(true);
                _meshRenderer.material = _colorOn;


                if (_safeZoneTorch == false)
                {
                    StartCoroutine(waitBeforeTurningOff());
                }
            }
        }
    }

    IEnumerator waitBeforeTurningOff()
    {
        yield return new WaitForSeconds(5f);
        _pointLight.SetActive(false);
        _meshRenderer.material = _colorOff;
        oneTime = false;
        Destroy(gameObject);
    }
}
