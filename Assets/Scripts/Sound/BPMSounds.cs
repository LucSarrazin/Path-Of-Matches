using UnityEngine;

public class BPMSounds : MonoBehaviour
{
    [Header("[REFERENCES]")]
    [SerializeField] AudioSource _heartBeat;
    [SerializeField] PlayerReferences _playerReferences;

    [Header("[Audio Settings]")]
    [SerializeField] private float _smoothBPMSpeed = 3f;
    [SerializeField] private float _minBpmPitch = 0.8f;
    [SerializeField] private float _maxBpmPitch = 2.4f;

    private Insanity _insanity;
    private float _targetPitch;
    private void OnEnable()
    {
        _insanity.OnInsanityChange += UpdateHeartBeatPitch;
    }

    private void OnDisable()
    {
        _insanity.OnInsanityChange -= UpdateHeartBeatPitch;       
    }

    private void Awake()
    {
        if (_heartBeat == null)
        {
            _heartBeat = GetComponent<AudioSource>();
        }
        if (_playerReferences == null)
        {
            _playerReferences = FindAnyObjectByType<PlayerReferences>();
            Debug.Log("Please Add PlayerReferences in inspector to avoid use of FindAnyObject");
        }

        _insanity = _playerReferences.PlayerInsanity;
    }

    private void Start()
    {
        _heartBeat.pitch = _minBpmPitch;
    }

    private void Update()
    {
        _heartBeat.pitch = Mathf.Lerp(_heartBeat.pitch, _targetPitch, Time.deltaTime * _smoothBPMSpeed);
    }

    // WARNING : Insanity lvls are hardcoded ! If min/max values are changed, the inverseLerp below must be Updated
    // insanity Lvl -> 70 to 150  (int) 
  
    private void UpdateHeartBeatPitch(int insanityLvl) 
    {
        float normalizedlInsanity = Mathf.InverseLerp(70f, 150f, insanityLvl);
        _targetPitch = Mathf.Lerp(_minBpmPitch, _maxBpmPitch, normalizedlInsanity);
    }

}
