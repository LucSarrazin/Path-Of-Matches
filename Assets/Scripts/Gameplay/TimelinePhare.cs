using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimelinePhare : MonoBehaviour
{
    [SerializeField] private PlayableDirector _timelinePlayable;
    [SerializeField] private PlayerReferences _playerReferences;
    [SerializeField] private GameObject timelineObject;
    [SerializeField] private Transform cam;
    [SerializeField] private changeMaterial phare;
    [SerializeField] private ResetCamera resetCamera;
    [SerializeField] private bool onetime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _timelinePlayable.stopped += TimelinePlayableOnstopped;
    }

    private void TimelinePlayableOnstopped(PlayableDirector obj)
    {
        _playerReferences.PlayerMovements.CanMove(true);
        _playerReferences.PlayerMovements.CanLook(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (onetime != true)
            {
                resetCamera.ResetCameraPosition();
                cam.rotation = new Quaternion(0, 0, 0, 1);
                _playerReferences.PlayerMovements.CanMove(false);
                _playerReferences.PlayerMovements.CanLook(false);
                timelineObject.SetActive(true);
                onetime = true;
                _timelinePlayable.Play();
                StartCoroutine(playThat());
            }
        }
    }

    IEnumerator playThat()
    {
        yield return new WaitForSeconds(1.23f);
        phare.ChangeMaterial();
    }
}
