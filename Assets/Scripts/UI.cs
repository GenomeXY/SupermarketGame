using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private FallingObjectsSpawner _fallingObjectsSpawner;

    public RawImage rawImageProduct1;
    public RawImage rawImageProduct2;
    public RawImage rawImageProduct3;

    public int Point1 = 0;
    public int Point2 = 0;
    public int Point3 = 0;

    public TextMeshProUGUI textproduct1;
    public TextMeshProUGUI textproduct2;
    public TextMeshProUGUI textproduct3;

    private void OnEnable()
    {
        ScoreManager.OnProductAmountChanged += UpdateProductAmount;
        InitializeUI();
    }

    private void OnDisable()
    {
        ScoreManager.OnProductAmountChanged -= UpdateProductAmount;
    }

    //void Start()
    //{
    //    InitializeUI();
    //}

    private void InitializeUI()
    {
        Point1 = _fallingObjectsSpawner.selectedProducts[0].Amount;
        Point2 = _fallingObjectsSpawner.selectedProducts[1].Amount;
        Point3 = _fallingObjectsSpawner.selectedProducts[2].Amount;

        rawImageProduct1.texture = _fallingObjectsSpawner.selectedProducts[0].Sprite.texture;
        rawImageProduct2.texture = _fallingObjectsSpawner.selectedProducts[1].Sprite.texture;
        rawImageProduct3.texture = _fallingObjectsSpawner.selectedProducts[2].Sprite.texture;

        textproduct1.text = Point1.ToString();
        textproduct2.text = Point2.ToString();
        textproduct3.text = Point3.ToString();
    }

    private void UpdateProductAmount(Products product)
    {
        // Обновляем количество и текст в UI в зависимости от того, какой продукт изменился
        if (_fallingObjectsSpawner.selectedProducts.Contains(product))
        {
            int index = _fallingObjectsSpawner.selectedProducts.IndexOf(product);
            switch (index)
            {
                case 0:
                    Point1 = Mathf.Max(product.Amount, 0);
                    textproduct1.text = Point1.ToString();
                    break;
                case 1:
                    Point2 = Mathf.Max(product.Amount, 0);
                    textproduct2.text = Point2.ToString();
                    break;
                case 2:
                    Point3 = Mathf.Max(product.Amount, 0);
                    textproduct3.text = Point3.ToString();
                    break;
            }

            // Проверяем, все ли продукты собраны
            _scoreManager.CheckIfAllProductsCollected();
        }
    }
}
