using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinMenu : MonoBehaviour
{
    private void OnEnable()
    {
        MyAudioManager.Instance.WinSound.Play();
        StartCoroutine(RestartMenu());
    }

    private IEnumerator RestartMenu()
    {
        Debug.Log("RestartMenu coroutine started");

        yield return new WaitForSeconds(3f);

        Debug.Log("After waiting 3 seconds");

        Debug.Log("!!!"); // Проверяем, что этот лог выводится

        // Перезагрузка сцены
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
