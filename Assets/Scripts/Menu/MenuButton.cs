using UnityEngine;

public class MenuButton : MonoBehaviour
{
    private bool creditsOpen = false;
    [SerializeField] private GameObject creditsGameobject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Credits()
    {
        if (!creditsOpen)
        {
            creditsOpen = true;
            creditsGameobject.SetActive(true);
        }
        else
        {
            creditsOpen = false;
            creditsGameobject.SetActive(false);
        }
    }
}
