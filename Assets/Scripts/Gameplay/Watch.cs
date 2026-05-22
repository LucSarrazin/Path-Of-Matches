using UnityEngine;

public class Watch : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private bool switched = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openWatch()
    {
        if (!switched)
        {
            switched = true;
            Debug.Log("Open Watch");
            animator.SetBool("Close", true);
        }
        else
        {
            switched = false;
            Debug.Log("Close Watch");
            animator.SetBool("Close", false);
        }
    }
}
