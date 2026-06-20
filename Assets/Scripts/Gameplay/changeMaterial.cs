using UnityEngine;

public class changeMaterial : MonoBehaviour
{
    [SerializeField] private Material _material;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeMaterial()
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();

        Material[] mats = renderer.materials;

        for (int i = 0; i < mats.Length; i++)
        {
            Debug.Log("Material " + i + " est : " + mats[i].name + " ou ça : " +  mats[i]);
            if (i == 6)
            {
                mats[i] = _material;
            }
        }

        renderer.materials = mats;
    }
}
