using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private FallingObjectsSpawner _spawner;
    [SerializeField] private GameManager _gameManager;


    public static Action<Products> OnProductAmountChanged; 
    private void OnEnable()
    {
        // Подписываемся на событие сбора
        FallingObject.OnCollected += HandleProductCollected;
    }

    private void OnDestroy()
    {
        // Отписываемся от события сбора
        FallingObject.OnCollected -= HandleProductCollected;
    }
    private void HandleProductCollected(ProductsTypes productType)
    {
        // Находим продукт в списке selectedProducts с соответствующим типом
        Products collectedProduct = _spawner.selectedProducts.FirstOrDefault(product => product.ProductTypes == productType);

        // Проверяем, что продукт существует и его количество больше нуля
        if (collectedProduct != null && collectedProduct.Amount > 0)
        {
            // Уменьшаем количество (Amount)
            collectedProduct.Amount--;

            // Вызываем событие об изменении количества продукта
            OnProductAmountChanged?.Invoke(collectedProduct);

            if (collectedProduct.Amount <= 0)
            {
                // Удаляем продукт из списка падающих продуктов
                //_spawner.fallingProducts.Remove(collectedProduct);
                //Debug.Log($"Product {collectedProduct.ProductTypes} has been removed from falling products.");
            }
        }
    }

    public void CheckIfAllProductsCollected()
    {
        if (_spawner.selectedProducts.All(product => product.Amount <= 0))
        {
            MyAudioManager.Instance.WinSound.Play();
            _gameManager.WinGameState();
        }
    }

    public void Restart()
    {
        // Ничего не нужно сбрасывать в ScoreManager
        // Просто перезапустим отслеживание сбора продуктов
        //FallingObject.OnCollected += HandleProductCollected;
    }


}
