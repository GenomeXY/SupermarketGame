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
    [SerializeField] private float defaultDropChance = 1.0f; // Базовая вероятность выпадения для обычных продуктов
    [SerializeField] private float selectedDropChance = 5.0f; // Вероятность выпадения для выбранных продуктов

    private float _timeSinceLastSpawn = 0.0f;
    public int TotalScore = 0;

    private void Start()
    {
        // Подписываемся на событие сбора
        FallingObject.OnCollected += HandleProductCollected;
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

        // Выбираем случайный префаб из списка с учетом вероятности выпадения
        Products selectedProduct = GetRandomProductByChance(_fallingProducts);
        GameObject selectedPrefab = selectedProduct.Prefab;

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
        selectedProducts.Clear();

        // Выбираем 3 случайных продукта с учетом вероятности выпадения
        for (int i = 0; i < 3; i++)
        {
            Products selectedProduct = GetRandomProductByChance(tempFallingProducts);
            selectedProducts.Add(selectedProduct);
            selectedProduct.Initialize();
            selectedProduct.SetDropChance(selectedDropChance); // Устанавливаем повышенный шанс выпадения
            tempFallingProducts.Remove(selectedProduct); // Удаляем выбранный продукт из временного списка
        }

        // Остальным продуктам устанавливаем базовый шанс выпадения
        foreach (var product in tempFallingProducts)
        {
            product.SetDropChance(defaultDropChance);
        }
    }

    public void InitializeDropChances()
    {
        // Устанавливаем базовый шанс выпадения для всех продуктов
        foreach (var product in _fallingProducts)
        {
            product.Initialize(); // Убедитесь, что продукты инициализированы
            product.SetDropChance(defaultDropChance);
        }
    }

    // Метод для выбора продукта с учетом вероятности выпадения
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
        return products[products.Count - 1]; // На случай, если что-то пошло не так, возвращаем последний продукт
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

            if (collectedProduct.Amount <= 0)
            {
                // Удаляем продукт из списка падающих продуктов
                _fallingProducts.Remove(collectedProduct);
                Debug.Log($"Product {collectedProduct.ProductTypes} has been removed from falling products.");
            }
        }
        else
        {
            Debug.Log("Collected a product that is not selected or already depleted.");
        }
    }
}
