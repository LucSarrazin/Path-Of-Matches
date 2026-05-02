using System.Collections;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    public bool start = false;
    public AnimationCurve curve;
    public float duration = 1f;
    [SerializeField] private float minusStrength;
    private Vector3 startPos, currentPos;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            start = false;
            StartCoroutine("Shaking");
        }
    }

    public void ShakeScreenMatches()
    {
        StartCoroutine("ShakingMatches");
    }

    public void StopShakeMatches()
    {
        StopCoroutine("ShakingMatches");
        transform.localPosition = new Vector3(0,0,0);
    }

    IEnumerator ShakingMatches()
    {
        Vector3 startPosition = transform.localPosition;
        float elapsedTime = 0f;
        while (true)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            transform.localPosition = startPosition + Random.insideUnitSphere * (strength - minusStrength);
            yield return null;
        }
    }


    public void ShakeScreen()
    {
        StartCoroutine("Shaking");
    }

    public void StopShake()
    {
        StopCoroutine("Shaking");
        transform.position = startPos;
    }
    IEnumerator Shaking()
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            transform.position = startPosition + Random.insideUnitSphere * strength;
            yield return null;
        }
        transform.position = startPosition;
    }
}
