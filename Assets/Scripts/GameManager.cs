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
    private void Awake()
    {       
        StartMenuGameState();                      
    }

    public void StartMenuGameState()
    {       
        LootSpawner.GetComponent<FallingObjectsSpawner>().InitializeDropChances(); // �������������� ����������� ��������� ���������
        LootSpawner.GetComponent<FallingObjectsSpawner>().Select3RandomProducts(); // �������� 3 ��������� �������� ��� �����

        GameProcessDeactivate();

        _countdownCanvas.SetActive(false); // ������������ ������ � �������� ��������
        _uiCanvas.SetActive(false);        // ������������ ������ � UI        
        _menuCanvas.SetActive(true);       // ���������� ������ � ������� ����
    }    
    public void CountdownGameState()
    {
        _menuCanvas.SetActive(false);
        _countdownCanvas.SetActive(true);
        _uiCanvas.SetActive(true);
        _countdownCanvas.GetComponent<Countdown>().StartCountdown();
    }
    public void GameState()
    {
        GameProcessActivate();
        _countdownCanvas.SetActive(false);
    }
    public void WinGameState()
    {
        GameProcessDeactivate();
        _uiCanvas.SetActive(false);
        _winCanvas.SetActive(true);
    }

    public void GameProcessActivate() 
    {
        TapHandler.SetActive(true);
        LootSpawner.SetActive(true);
    }

    public void GameProcessDeactivate()
    {
        TapHandler.SetActive(false);
        LootSpawner.SetActive(false);
    }    
}
