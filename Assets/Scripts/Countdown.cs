using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countdownText;
    [SerializeField] private GameManager _gameManager;
    public void StartCountdown()
    {           
        StartCoroutine(CountdownUIText());          
    }

    private IEnumerator CountdownUIText()
    {
        _countdownText.text = "3";
        MyAudioManager.Instance.CountdownSound.Play();
        yield return new WaitForSeconds(1f);
        _countdownText.text = "2";
        MyAudioManager.Instance.CountdownSound.Play();
        yield return new WaitForSeconds(1f);
        _countdownText.text = "1";
        MyAudioManager.Instance.CountdownSound.Play();
        yield return new WaitForSeconds(1f);
        _countdownText.text = "GO!";
        MyAudioManager.Instance.StartSound.Play();
        yield return new WaitForSeconds(1f);
        _gameManager.GameActivate();
        gameObject.SetActive(false);
    }
}
