using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class FallingObjectsSpawner : MonoBehaviour
{   
    public List<Products> fallingProducts;                         // —писок падающих продуктов
    //public List<Products> tempfallingProducts;                     // ¬ременный список падающих продуктов
    public List<Products> selectedProducts = new List<Products>(); // —писок продуктов дл€ сбора

    [SerializeField] private float _spawnInterval = 2.0f;                           // »нтервал между спавнами предметов
    [SerializeField] private float _spawnRange = 2.6f;                              // ƒиапазон, в котором предметы могут по€вл€тьс€ 
    [Header("Drop Chances")]
    [SerializeField] private float defaultDropChance = 1.0f;                        // Ѕазова€ веро€тность выпадени€ дл€ обычных продуктов
    [SerializeField] private float selectedDropChance = 5.0f;                       // ¬еро€тность выпадени€ дл€ выбранных продуктов

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

        // ¬ыбираем случайный префаб из списка с учетом веро€тности выпадени€
        Products selectedProduct = GetRandomProductByChance(fallingProducts);
        GameObject selectedPrefab = selectedProduct.Prefab;

        // —оздаем падающий объект 
        GameObject fallingObject = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);
    }

    public void Select3RandomProducts()
    {
        // ѕровер€ем, достаточно ли продуктов дл€ выбора 3 случайных
        if (fallingProducts.Count < 3)
        {
            Debug.LogWarning("Ќедостаточно продуктов дл€ отображени€.");
            return;
        }

        // —оздаем временный список дл€ работы с случайным выбором
        List<Products> tempFallingProducts = new List<Products>(fallingProducts);
        selectedProducts.Clear();

        // ¬ыбираем 3 случайных продукта с учетом веро€тности выпадени€
        for (int i = 0; i < 3; i++)
        {
            Products selectedProduct = GetRandomProductByChance(tempFallingProducts);
            selectedProducts.Add(selectedProduct);
            selectedProduct.Initialize();
            selectedProduct.SetDropChance(selectedDropChance); // ”станавливаем повышенный шанс выпадени€
            tempFallingProducts.Remove(selectedProduct); // ”дал€ем выбранный продукт из временного списка
        }

        // ќстальным продуктам устанавливаем базовый шанс выпадени€
        foreach (var product in tempFallingProducts)
        {
            product.SetDropChance(defaultDropChance);
        }
    }
    public void InitializeDropChances()
    {
        // ”станавливаем базовый шанс выпадени€ дл€ всех продуктов
        foreach (var product in fallingProducts)
        {
            product.Initialize(); // ”бедитесь, что продукты инициализированы
            product.SetDropChance(defaultDropChance);
        }
    }

    // ћетод дл€ выбора продукта с учетом веро€тности выпадени€
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
        return products[products.Count - 1]; // Ќа случай, если что-то пошло не так, возвращаем последний продукт
    }    
}

