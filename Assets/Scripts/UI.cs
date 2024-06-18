using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject _uiCanvas;
    [SerializeField] private FallingObjectsSpawner _fallingObjectsSpawner;
    [SerializeField] private GameObject _winCanvas; // Канвас для окна победы

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
        FallingObjectsSpawner.OnProductAmountChanged += UpdateProductAmount;
    }

    private void OnDisable()
    {
        FallingObjectsSpawner.OnProductAmountChanged -= UpdateProductAmount;
    }

    void Start()
    {
        InitializeUI();
    }

    private void InitializeUI()
    {
        if (_fallingObjectsSpawner.selectedProducts.Count > 0)
        {
            Point1 = _fallingObjectsSpawner.selectedProducts[0].Amount;
            rawImageProduct1.texture = _fallingObjectsSpawner.selectedProducts[0].Sprite.texture;
            textproduct1.text = Point1.ToString();
        }

        if (_fallingObjectsSpawner.selectedProducts.Count > 1)
        {
            Point2 = _fallingObjectsSpawner.selectedProducts[1].Amount;
            rawImageProduct2.texture = _fallingObjectsSpawner.selectedProducts[1].Sprite.texture;
            textproduct2.text = Point2.ToString();
        }

        if (_fallingObjectsSpawner.selectedProducts.Count > 2)
        {
            Point3 = _fallingObjectsSpawner.selectedProducts[2].Amount;
            rawImageProduct3.texture = _fallingObjectsSpawner.selectedProducts[2].Sprite.texture;
            textproduct3.text = Point3.ToString();
        }
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
            CheckIfAllProductsCollected();
        }
    }

    private void CheckIfAllProductsCollected()
    {
        if (_fallingObjectsSpawner.selectedProducts.All(product => product.Amount <= 0))
        {
            // Останавливаем игру и открываем канвас победы
            Time.timeScale = 0f; // Останавливаем время
            _uiCanvas.SetActive(false); // Деактивируем основной UI
            _winCanvas.SetActive(true); // Активируем канвас победы
            MyAudioManager.Instance.WinSound.Play();
        }
    }
}
