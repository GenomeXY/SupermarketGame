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
        _conf1Effect.SetActive(true);
        _conf2Effect.SetActive(true);
        StartCoroutine(RestartMenu());
    }

    private IEnumerator RestartMenu()
    {
        yield return new WaitForSeconds(3f);

        _gameManager.StartMenuGameState();
        gameObject.SetActive(false);
        MyAudioManager.Instance.BackGameMusic.Stop();
        _menu.Restart();
    }
}
