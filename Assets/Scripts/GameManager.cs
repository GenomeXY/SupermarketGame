using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject TapHandler;
    [SerializeField] private GameObject LootSpawner;
    [SerializeField] private GameObject _menuCanvas;
    [SerializeField] private GameObject _uiCanvas;
    [SerializeField] private GameObject _winCanvas;
    [SerializeField] private GameObject _countdownCanvas;
    
    [SerializeField] private FallingObjectsSpawner _fallingObjectsSpawner;
    private void Awake()
    {       
        StartMenuGameState();                      
    }

    public void StartMenuGameState()
    {       
        _fallingObjectsSpawner.InitializeDropChances(); // Инициализируем вероятности выпадения
        _fallingObjectsSpawner.Select3RandomProducts(); // Выбираем 3 случайных продукта для сбора

        _menuCanvas.SetActive(true);       // активируем канвас с главным меню
        _countdownCanvas.SetActive(false); // деактивируем канвас с обратным отсчетом
        _uiCanvas.SetActive(false);
        TapHandler.SetActive(false);       // деактивируем обработчик нажатий
        LootSpawner.SetActive(false);      // деактивируем лут-спаунер
    }

    public void UIGameState()
    {

    }

    public void PlayNewGameState()
    {

    }

    public void GameActivate() // активируем лут-спаунер и обработчик нажатий
    {
        TapHandler.SetActive(true);
        LootSpawner.SetActive(true);
    }    
    public void WinGameState()
    {

    }


    
}
