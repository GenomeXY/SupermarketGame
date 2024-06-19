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
    [SerializeField] private GameObject _conf3Effect;

    private void OnEnable()
    {
        StartCoroutine(RestartMenu());
    }

    private IEnumerator RestartMenu()
    {
        _conf3Effect.SetActive(true);
        yield return new WaitForSeconds(0.7f);
        _conf1Effect.SetActive(true);
        yield return new WaitForSeconds(0.7f);
        _conf2Effect.SetActive(true);

        yield return new WaitForSeconds(3.9f);        
        MyAudioManager.Instance.BackGameMusic.Stop();

        _gameManager.RestartGame();
        gameObject.SetActive(false);
    }
}
