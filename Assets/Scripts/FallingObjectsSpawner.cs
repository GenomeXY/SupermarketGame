using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FallingObjectsSpawner : MonoBehaviour
{
    public static Action<Products> OnProductAmountChanged;

    [SerializeField] private List<Products> _fallingProducts; // Список падающих продуктов
    [SerializeField] public List<Products> selectedProducts = new List<Products>(); // Список продуктов для сбора
    [SerializeField] private float _spawnInterval = 2.0f; // Интервал между спавнами предметов
    [SerializeField] private float _spawnRange = 2.5f; // Диапазон, в котором предметы могут появляться (соответствует диапазону передвижения игрока)

    private float _timeSinceLastSpawn = 0.0f;
    public int TotalScore = 0;

    private void Start()
    {
        //selectedProducts.Clear();
        FallingObject.OnCollected += HandleProductCollected; // Подписываемся на событие сбора        
    }
    private void OnDestroy()
    {
        FallingObject.OnCollected -= HandleProductCollected; // Отписываемся от события при разрушении
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
            Debug.LogWarning("Нет данных для падающих предметов.");
            return;
        }

        float randomX = UnityEngine.Random.Range(-_spawnRange, _spawnRange);
        Vector3 spawnPosition = new Vector3(randomX, transform.position.y, transform.position.z);

        // Выбираем случайный префаб из списка
        int randomIndex = UnityEngine.Random.Range(0, _fallingProducts.Count);
        GameObject selectedPrefab = _fallingProducts[randomIndex].Prefab;

        // Создаем падающий объект 
        GameObject fallingObject = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);
    }

    public void Select3RandomProducts()
    {
        // Проверяем, достаточно ли продуктов для выбора 3 случайных
        if (_fallingProducts.Count < 3)
        {
            Debug.LogWarning("Недостаточно продуктов для отображения.");
            return;
        }

        // Создаем временный список для работы с случайным выбором
        List<Products> tempFallingProducts = new List<Products>(_fallingProducts);
        //selectedProducts.Clear();

        // Выбираем 3 случайных продукта
        for (int i = 0; i < 3; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, tempFallingProducts.Count);
            Products selectedProduct = tempFallingProducts[randomIndex];
            selectedProducts.Add(selectedProduct);
            selectedProduct.Initialize();
            tempFallingProducts.RemoveAt(randomIndex); // Удаляем выбранный продукт из временного списка
        }
    }

    private void HandleProductCollected(ProductsTypes productType)
    {
        // Находим продукт в списке selectedProducts с соответствующим типом
        Products collectedProduct = selectedProducts.FirstOrDefault(product => product.ProductTypes == productType);

        // Проверяем, что продукт существует и его количество больше нуля
        if (collectedProduct != null && collectedProduct.Amount > 0)
        {
            // Уменьшаем количество (Amount)
            collectedProduct.Amount--;

            // Вызываем событие об изменении количества продукта
            OnProductAmountChanged?.Invoke(collectedProduct);

            // Увеличиваем общий счет
            TotalScore++;
            Debug.Log($"Collected a selected product! Total Score: {TotalScore}, {collectedProduct.ProductTypes} Amount left: {collectedProduct.Amount}");

        }
        else
        {
            Debug.Log("Collected a product that is not selected or already depleted.");
        }
    }
}
