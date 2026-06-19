using UnityEngine;

public class CubeInspectable : Inspectable
{
    [SerializeField] AudioSource _audioSrc;

    protected override void Awake()
    {
        base.Awake();
        if (_audioSrc == null) _audioSrc = GetComponent<AudioSource>();

    }

    public override void Interact()
    {
        base.Interact();
        _audioSrc.Play();
        
        /* Here : inspectable special methods for this object */
    }
}
