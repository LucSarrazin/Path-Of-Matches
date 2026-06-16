using System.Collections;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    [SerializeField] private BoxCollider _collider;
    private float _delay = 2f; 

    private Coroutine _coroutine;

    private void Start()
    {
        _collider.GetComponent<BoxCollider>();         
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
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
