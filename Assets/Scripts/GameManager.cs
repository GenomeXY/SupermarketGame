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
        _fallingObjectsSpawner.InitializeDropChances(); // �������������� ����������� ���������
        _fallingObjectsSpawner.Select3RandomProducts(); // �������� 3 ��������� �������� ��� �����

        _menuCanvas.SetActive(true);       // ���������� ������ � ������� ����
        _countdownCanvas.SetActive(false); // ������������ ������ � �������� ��������
        _uiCanvas.SetActive(false);
        TapHandler.SetActive(false);       // ������������ ���������� �������
        LootSpawner.SetActive(false);      // ������������ ���-�������
    }

    public void UIGameState()
    {

    }

    public void PlayNewGameState()
    {

    }

    public void GameActivate() // ���������� ���-������� � ���������� �������
    {
        TapHandler.SetActive(true);
        LootSpawner.SetActive(true);
    }    
    public void WinGameState()
    {

    }


    
}
