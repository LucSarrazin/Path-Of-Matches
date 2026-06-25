using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class LanternManager : MonoBehaviour
{
    public GameObject[] lantern;
    [SerializeField] private string goodCode;
    [SerializeField] private string actualCode;
    [SerializeField] private bool oneTime = false;
    [SerializeField] private Door door;
    public static LanternManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int code;

        if (int.TryParse(actualCode, out code) && code >= 100)
        {
            if (!oneTime)
            {
                oneTime = true;
                LookForCode();
            }
        }
    }

    public void LookForCode()
    {
        if (actualCode == goodCode)
        {
            Debug.Log("BOn code");
            door.OpenDoor();
        }
        else
        {
            Debug.Log("Mauvais code");
            StartCoroutine(IncorrectCode());
            oneTime = false;
            actualCode = "";
        }
    }

    public void AddCode(string code)
    {
        actualCode += code;
        Debug.Log(actualCode);
    }

    IEnumerator IncorrectCode()
    {
        yield return new WaitForSeconds(2);

        for (int i = 0; i < lantern.Length; i++)
        {
            lantern[i].GetComponent<Lantern>().TurnOff();
        }
    }
}
