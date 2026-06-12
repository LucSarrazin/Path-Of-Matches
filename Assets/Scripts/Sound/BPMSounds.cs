using System.Collections;
using UnityEngine;

public class BPMSounds : MonoBehaviour
{
    [Header("[REFERENCES]")]
    [SerializeField] AudioSource _heartBeat;
    [SerializeField] PlayerReferences _playerReferences;

    [Header("[Audio Settings]")]
    [Header("Pitch")]
    [SerializeField] private float _smoothBPMSpeed = 3f;
    [SerializeField] private float _minBpmPitch = 0.8f;
    [SerializeField] private float _maxBpmPitch = 2.4f;

    [Header("Volume")]
    [SerializeField] private float _normalVolumeSpeed = 3f;
    [SerializeField, Range(0.4f, 1f)] private float _minVolume = 0.4f;
    [SerializeField/*, Range(0f, 1f)*/] private float _maxVolume = 1f;

    [Header("[Events Settings]")]
    [Tooltip("Delay before pause BPM sound when player enter in a safe Zone")]
    [SerializeField] private float _delay = 2f; 

    private Insanity _insanity;
    private float _targetPitch;
    private float _targetVolume;

    private bool _isInSafeZone = false;
    private Coroutine _fadeVolumeCoroutine;

    private void OnEnable()
    {
        _insanity.OnInsanityChange += UpdateHeartBeatSFX;

        GameEvents.OnDarkZoneEnter += PlayHeartBeat;
        GameEvents.OnSafeZoneEnter += StopHeartBeat; 

    }

    private void OnDisable()
    {
        _insanity.OnInsanityChange -= UpdateHeartBeatSFX;

        GameEvents.OnDarkZoneEnter -= PlayHeartBeat;
        GameEvents.OnSafeZoneEnter -= StopHeartBeat;
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
        _heartBeat.volume = _minVolume;
    }

    private void Update()
    {
        _heartBeat.pitch = Mathf.Lerp(_heartBeat.pitch, _targetPitch, Time.deltaTime * _smoothBPMSpeed);

        if (!_isInSafeZone)
        {
            _heartBeat.volume = Mathf.Lerp(_heartBeat.volume, _targetVolume, Time.deltaTime * _normalVolumeSpeed);
        }
    }

    // WARNING : Insanity lvls are hardcoded ! If min/max values are changed, the inverseLerp below must be Updated
    // insanity Lvl -> 70 to 150  (int) 
    // PALIERS : 70 | 90 | 125

    private void UpdateHeartBeatSFX(int insanityLvl) 
    {
        float normalizedlInsanity = Mathf.InverseLerp(70f, 150f, insanityLvl);
        _targetPitch = Mathf.Lerp(_minBpmPitch, _maxBpmPitch, normalizedlInsanity);
        _targetVolume = Mathf.Lerp(_minVolume, _maxVolume, normalizedlInsanity);

    }

    private void PlayHeartBeat()
    {
        _isInSafeZone = false;
        _heartBeat.Play();

        Debug.Log($"[BPM SFX] PLAY Heart Beat");
    }


    private void StopHeartBeat()
    {
        _isInSafeZone = true; 

        if(_fadeVolumeCoroutine != null) StopCoroutine(_fadeVolumeCoroutine); // SAFETY CHECK -> Stop coroutine to avoid double routines

        _fadeVolumeCoroutine = StartCoroutine(FadeOutVolumeAndPause()); 

        Debug.Log($"[BPM SFX] Start Coroutine to STOP Heart Beat after {_delay}");
    }

    private IEnumerator FadeOutVolumeAndPause()
    {
        float startVolume = _heartBeat.volume;
        float duration = _delay; 
        float timer = 0f; 

        while (timer < duration)
        {
            timer += Time.deltaTime;

            _heartBeat.volume = Mathf.Lerp(startVolume, 0f, timer/duration); 
            yield return null;
        }
        _heartBeat.volume = 0f;
        _heartBeat.Pause(); 

        Debug.Log($"[BPM SFX] Heart Beat STOPPED ");
    }

}
