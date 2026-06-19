using System.Collections;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    [SerializeField] private BoxCollider _collider;
    private float _delay = 2f; 

    private Coroutine _coroutine;

    private void Awake()
    {
        if (_collider == null)
        {
            _collider = GetComponent<BoxCollider>();         
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player is in Dead zone"); 
            if (_coroutine != null) StopCoroutine(_coroutine);
            _coroutine = StartCoroutine(WaitBeforeDie());
        }
    }

    private IEnumerator WaitBeforeDie()
    {
        float timer = 0f;

        while (timer < _delay)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        GameEvents.OnPlayerDeath?.Invoke();
    }
}
