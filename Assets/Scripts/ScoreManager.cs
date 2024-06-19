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
        // ������������� �� ������� �����
        FallingObject.OnCollected += HandleProductCollected;
    }

    private void OnDestroy()
    {
        // ������������ �� ������� �����
        FallingObject.OnCollected -= HandleProductCollected;
    }
    private void HandleProductCollected(ProductsTypes productType)
    {
        // ������� ������� � ������ selectedProducts � ��������������� �����
        Products collectedProduct = _spawner.selectedProducts.FirstOrDefault(product => product.ProductTypes == productType);

        // ���������, ��� ������� ���������� � ��� ���������� ������ ����
        if (collectedProduct != null && collectedProduct.Amount > 0)
        {
            // ��������� ���������� (Amount)
            collectedProduct.Amount--;

            // �������� ������� �� ��������� ���������� ��������
            OnProductAmountChanged?.Invoke(collectedProduct);

            if (collectedProduct.Amount <= 0)
            {
                // ������� ������� �� ������ �������� ���������
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
        // ������ �� ����� ���������� � ScoreManager
        // ������ ������������ ������������ ����� ���������
        //FallingObject.OnCollected += HandleProductCollected;
    }


}
