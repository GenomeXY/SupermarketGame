using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class FallingObjectsSpawner : MonoBehaviour
{   
    public List<Products> fallingProducts;                         // ������ �������� ���������
    //public List<Products> tempfallingProducts;                     // ��������� ������ �������� ���������
    public List<Products> selectedProducts = new List<Products>(); // ������ ��������� ��� �����

    [SerializeField] private float _spawnInterval = 2.0f;                           // �������� ����� �������� ���������
    [SerializeField] private float _spawnRange = 2.6f;                              // ��������, � ������� �������� ����� ���������� 
    [Header("Drop Chances")]
    [SerializeField] private float defaultDropChance = 1.0f;                        // ������� ����������� ��������� ��� ������� ���������
    [SerializeField] private float selectedDropChance = 5.0f;                       // ����������� ��������� ��� ��������� ���������

    private float _timeSinceLastSpawn = 0.0f;

    private void Start()
    {
        //tempfallingProducts = new List<Products>(fallingProducts);
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
        float randomX = UnityEngine.Random.Range(-_spawnRange, _spawnRange);
        Vector3 spawnPosition = new Vector3(randomX, transform.position.y, transform.position.z);

        // �������� ��������� ������ �� ������ � ������ ����������� ���������
        Products selectedProduct = GetRandomProductByChance(fallingProducts);
        GameObject selectedPrefab = selectedProduct.Prefab;

        // ������� �������� ������ 
        GameObject fallingObject = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);
    }

    public void Select3RandomProducts()
    {
        // ���������, ���������� �� ��������� ��� ������ 3 ���������
        if (fallingProducts.Count < 3)
        {
            Debug.LogWarning("������������ ��������� ��� �����������.");
            return;
        }

        // ������� ��������� ������ ��� ������ � ��������� �������
        List<Products> tempFallingProducts = new List<Products>(fallingProducts);
        selectedProducts.Clear();

        // �������� 3 ��������� �������� � ������ ����������� ���������
        for (int i = 0; i < 3; i++)
        {
            Products selectedProduct = GetRandomProductByChance(tempFallingProducts);
            selectedProducts.Add(selectedProduct);
            selectedProduct.Initialize();
            selectedProduct.SetDropChance(selectedDropChance); // ������������� ���������� ���� ���������
            tempFallingProducts.Remove(selectedProduct); // ������� ��������� ������� �� ���������� ������
        }

        // ��������� ��������� ������������� ������� ���� ���������
        foreach (var product in tempFallingProducts)
        {
            product.SetDropChance(defaultDropChance);
        }
    }
    public void InitializeDropChances()
    {
        // ������������� ������� ���� ��������� ��� ���� ���������
        foreach (var product in fallingProducts)
        {
            product.Initialize(); // ���������, ��� �������� ����������������
            product.SetDropChance(defaultDropChance);
        }
    }

    // ����� ��� ������ �������� � ������ ����������� ���������
    private Products GetRandomProductByChance(List<Products> products)
    {
        float totalChance = products.Sum(p => p.DropChance);
        float randomPoint = UnityEngine.Random.value * totalChance;

        float cumulativeChance = 0f;
        foreach (var product in products)
        {
            cumulativeChance += product.DropChance;
            if (randomPoint <= cumulativeChance)
            {
                return product;
            }
        }
        return products[products.Count - 1]; // �� ������, ���� ���-�� ����� �� ���, ���������� ��������� �������
    }    
}

