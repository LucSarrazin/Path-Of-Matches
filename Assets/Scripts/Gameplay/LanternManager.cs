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
        if (int.Parse(actualCode) >= 100)
        {
            if (oneTime == false)
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
            for (int i = 0; i < lantern.Length; i++)
            {
                lantern[i].GetComponent<Lantern>().TurnOff();
            }
            oneTime = false;
            actualCode = "";
        }
    }

    public void AddCode(string code)
    {
        actualCode += code;
        Debug.Log(actualCode);
    }
}
