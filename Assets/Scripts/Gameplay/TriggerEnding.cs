using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerEnding : MonoBehaviour
{
    private bool oneTime = false;
    [SerializeField] private Animator blinkingEyes;
    [SerializeField] private PlayerReferences _playerReferences;
    [SerializeField] private GameObject _endingCanvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (_endingCanvas.activeSelf)
        {
            _endingCanvas.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (oneTime == false)
            {
                oneTime = true;
                blinkingEyes.SetBool("Close", true);
                _playerReferences.PlayerMovements.CanMove(false);
                _playerReferences.PlayerMovements.CanLook(false);

                StartCoroutine(PlayEndScene());

            }
        }
    }

    private IEnumerator PlayEndScene()
    {
        yield return new WaitForSeconds(2f);
        _endingCanvas.SetActive(true);
        yield return new WaitForSeconds(2f);
        GameEvents.OnDeleteSaveRequested?.Invoke();
        _playerReferences.PlayerAudioSource.PlayOneShot(_playerReferences.reviveSound);

        yield return new WaitForSeconds(7f);

        SceneManager.LoadScene(0);
    }
}