using UnityEngine;
using UnityEngine.Playables;

public class StartingEvent1 : MonoBehaviour
{
    [SerializeField] private PlayableDirector _timelinePlayable;
    [SerializeField] private PlayerReferences _playerReferences;
    [SerializeField] private GameObject timelineObject;
    [SerializeField] private Transform cam;
    [SerializeField] private ResetCamera resetCamera;
    [SerializeField] private bool onetime;

    [SerializeField] private Animator _animatorEye;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _timelinePlayable.stopped += TimelinePlayableOnstopped;
        
        if (onetime != true)
        {
            _animatorEye.SetBool("Start", true);
            //resetCamera.ResetCameraPosition();
            //cam.rotation = new Quaternion(0, 0, 0, 1);
            _playerReferences.PlayerMovements.CanMove(false);
            _playerReferences.PlayerMovements.CanLook(false);
            timelineObject.SetActive(true);
            onetime = true;
            _timelinePlayable.Play();
        }
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
}
