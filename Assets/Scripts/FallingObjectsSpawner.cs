using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FallingObjectsSpawner : MonoBehaviour
{
    public static Action<Products> OnProductAmountChanged;

    [SerializeField] private List<Products> _fallingProducts; // ������ �������� ���������
    [SerializeField] public List<Products> selectedProducts = new List<Products>(); // ������ ��������� ��� �����
    [SerializeField] private float _spawnInterval = 2.0f; // �������� ����� �������� ���������
    [SerializeField] private float _spawnRange = 2.5f; // ��������, � ������� �������� ����� ���������� (������������� ��������� ������������ ������)

    private float _timeSinceLastSpawn = 0.0f;
    public int TotalScore = 0;

    private void Start()
    {
        //selectedProducts.Clear();
        FallingObject.OnCollected += HandleProductCollected; // ������������� �� ������� �����        
    }
    private void OnDestroy()
    {
        FallingObject.OnCollected -= HandleProductCollected; // ������������ �� ������� ��� ����������
    }
    private void Update()
    {
        _timeSinceLastSpawn += Time.deltaTime;

        if (_timeSinceLastSpawn >= _spawnInterval)
        {
            SpawnFallingObject();
            _timeSinceLastSpawn = 0.0f;
        }
    }

    private void SpawnFallingObject()
    {
        if (_fallingProducts.Count == 0)
        {
            Debug.LogWarning("��� ������ ��� �������� ���������.");
            return;
        }

        float randomX = UnityEngine.Random.Range(-_spawnRange, _spawnRange);
        Vector3 spawnPosition = new Vector3(randomX, transform.position.y, transform.position.z);

        // �������� ��������� ������ �� ������
        int randomIndex = UnityEngine.Random.Range(0, _fallingProducts.Count);
        GameObject selectedPrefab = _fallingProducts[randomIndex].Prefab;

        // ������� �������� ������ 
        GameObject fallingObject = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);
    }

    public void Select3RandomProducts()
    {
        // ���������, ���������� �� ��������� ��� ������ 3 ���������
        if (_fallingProducts.Count < 3)
        {
            Debug.LogWarning("������������ ��������� ��� �����������.");
            return;
        }

        // ������� ��������� ������ ��� ������ � ��������� �������
        List<Products> tempFallingProducts = new List<Products>(_fallingProducts);
        //selectedProducts.Clear();

        // �������� 3 ��������� ��������
        for (int i = 0; i < 3; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, tempFallingProducts.Count);
            Products selectedProduct = tempFallingProducts[randomIndex];
            selectedProducts.Add(selectedProduct);
            selectedProduct.Initialize();
            tempFallingProducts.RemoveAt(randomIndex); // ������� ��������� ������� �� ���������� ������
        }
    }

    private void HandleProductCollected(ProductsTypes productType)
    {
        // ������� ������� � ������ selectedProducts � ��������������� �����
        Products collectedProduct = selectedProducts.FirstOrDefault(product => product.ProductTypes == productType);

        // ���������, ��� ������� ���������� � ��� ���������� ������ ����
        if (collectedProduct != null && collectedProduct.Amount > 0)
        {
            // ��������� ���������� (Amount)
            collectedProduct.Amount--;

            // �������� ������� �� ��������� ���������� ��������
            OnProductAmountChanged?.Invoke(collectedProduct);

            // ����������� ����� ����
            TotalScore++;
            Debug.Log($"Collected a selected product! Total Score: {TotalScore}, {collectedProduct.ProductTypes} Amount left: {collectedProduct.Amount}");

        }
        else
        {
            Debug.Log("Collected a product that is not selected or already depleted.");
        }
    }
}
