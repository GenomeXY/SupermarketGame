using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinMenu : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Menu _menu;
    [SerializeField] private GameObject _conf1Effect;
    [SerializeField] private GameObject _conf2Effect;

    private void OnEnable()
    {
        StartCoroutine(RestartMenu());
        _conf1Effect.SetActive(true);
        _conf2Effect.SetActive(true);
    }

    private IEnumerator RestartMenu()
    {
        yield return new WaitForSeconds(3f);

        MyAudioManager.Instance.BackGameMusic.Stop();

        _gameManager.RestartGame();
        gameObject.SetActive(false);
    }
}
