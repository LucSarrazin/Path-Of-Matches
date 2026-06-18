using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FootstepSurface
{
    public string LayerName;
    public AudioClip[] Clips;
}

public class PlayerFootStep : MonoBehaviour
{
    [Header("[SETTINGS]")]
    [SerializeField] private float _volume = 0.5f;
    [SerializeField] private float _footstepInterval = 0.5f;

    [Header("[SURFACES]")]
    [SerializeField] private FootstepSurface[] _surfaces;

    [Header("[REFERENCES]")]
    [SerializeField] private PlayerReferences _playerReferences;
    [SerializeField] private PlayerGroundChecker _groundChecker;

    public float FootStepInterval => _footstepInterval;


    private Dictionary<int, AudioClip[]> _surfaceMap;

    private int _lastSoundPlayedIndex = -1;
    private void Awake()
    {
        if (_playerReferences == null)
        {
            _playerReferences = GetComponentInParent<PlayerReferences>();
        }

        if (_groundChecker == null)
        {
            _groundChecker = GetComponent<PlayerGroundChecker>();
        }

        BuildSurfaceDictionary();
        PreloadAudioClips();
    }

    private void BuildSurfaceDictionary()
    {
        _surfaceMap = new Dictionary<int, AudioClip[]>();

        foreach (FootstepSurface surface in _surfaces)
        {
            int layer = LayerMask.NameToLayer(surface.LayerName);

            if (layer == -1)
            {
                Debug.LogWarning($"Layer '{surface.LayerName}' does not exist.");
                continue;
            }

            if (_surfaceMap.ContainsKey(layer))
            {
                Debug.LogWarning($"Layer '{surface.LayerName}' already added.");
                continue;
            }

            _surfaceMap.Add(layer, surface.Clips);
        }
    }

    private void PreloadAudioClips()
    {
        foreach (FootstepSurface surface in _surfaces)
        {
            if (surface.Clips == null)
                continue;

            foreach (AudioClip clip in surface.Clips)
            {
                if (clip != null)
                {
                    clip.LoadAudioData();
                }
            }
        }
    }

    private int GetRandomIndex(int length)
    {
        int index;
        do
        {
            index = Random.Range(0, length);
        }
        while (index == _lastSoundPlayedIndex);

        _lastSoundPlayedIndex = index;

        return index;
    }

    public void PlayFootstep()
    {
        int currentGroundLayer = _groundChecker.GetGroundLayer();

        if (currentGroundLayer == -1)
            return;

        if (_surfaceMap.TryGetValue(currentGroundLayer, out AudioClip[] clips))
        {
            if (clips == null || clips.Length == 0)
                return;

            AudioClip randomClip = clips[GetRandomIndex(clips.Length)];
            _playerReferences._footstepsAudioSource.pitch = Random.Range(0.95f, 1.05f); // Switch pitch to randomise footsteps

           _playerReferences._footstepsAudioSource.PlayOneShot(randomClip, _volume);
        }
    }
}