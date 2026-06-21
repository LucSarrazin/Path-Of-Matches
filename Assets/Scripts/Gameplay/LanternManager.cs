using Unity.VisualScripting;
using UnityEngine;

public class LanternManager : MonoBehaviour
{
    public GameObject[] lantern;
    private string goodCode;
    private string actualCode;
    public string[] LanternCode;
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
        
    }

    public void LookForCode()
    {
        if (actualCode == goodCode)
        {
            Debug.Log("BOn code");
        }
        else
        {
            Debug.Log("Mauvais code");
            for (int i = 0; i < lantern.Length; i++)
            {
                lantern[i].GetComponent<Lantern>().TurnOff();
            }
        }
    }

    public void AddCode(string code)
    {
        LanternCode = new string[] { code };
        actualCode = LanternCode.ToString();
        Debug.Log(actualCode);
    }
}
