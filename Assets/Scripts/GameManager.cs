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
        _fallingObjectsSpawner.InitializeDropChances(); // �������������� ����������� ���������
        _fallingObjectsSpawner.Select3RandomProducts(); // �������� 3 ��������� �������� ��� �����

        _uiCanvas.SetActive(false);
        TapHandler.SetActive(false);       // ������������ ���������� �������
        LootSpawner.SetActive(false);      // ������������ ���-�������
        _countdownCanvas.SetActive(false); // ������������ ������ � �������� ��������
        _menuCanvas.SetActive(true);       // ���������� ������ � ������� ����        
    }

    public void GameActivate() // ���������� ���-������� � ���������� �������
    {
        TapHandler.SetActive(true);
        LootSpawner.SetActive(true);
    }
}
