using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject TapHandler;
    [SerializeField] private GameObject LootSpawner;
    [SerializeField] private GameObject _menuCanvas;
    [SerializeField] private GameObject _countdownCanvas;
    [SerializeField] private GameObject _uiCanvas;
    [SerializeField] private FallingObjectsSpawner _fallingObjectsSpawner;
    private void Awake()
    {
        _fallingObjectsSpawner.Select3RandomProducts();
        _uiCanvas.SetActive(false);
        TapHandler.SetActive(false);       // деактивируем обработчик нажатий
        LootSpawner.SetActive(false);      // деактивируем лут-спаунер
        _countdownCanvas.SetActive(false); // деактивируем канвас с обратным отсчетом
        _menuCanvas.SetActive(true);       // активируем канвас с главным меню        
    }

    public void GameActivate() // активируем лут-спаунер и обработчик нажатий
    {
        TapHandler.SetActive(true);
        LootSpawner.SetActive(true);
    }
}
