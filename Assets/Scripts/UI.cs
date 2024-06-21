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

    public Shaker iconShake1;
    public Shaker iconShake2;
    public Shaker iconShake3;

    public Image ImageProduct1;
    public Image ImageProduct2;
    public Image ImageProduct3;

    public Image CheckerProduct1;
    public Image CheckerProduct2;
    public Image CheckerProduct3;

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
    private void InitializeUI()
    {
        Point1 = _fallingObjectsSpawner.selectedProducts[0].Amount;
        Point2 = _fallingObjectsSpawner.selectedProducts[1].Amount;
        Point3 = _fallingObjectsSpawner.selectedProducts[2].Amount;

        textproduct1.gameObject.SetActive(true);
        textproduct2.gameObject.SetActive(true);
        textproduct3.gameObject.SetActive(true);

        CheckerProduct1.gameObject.SetActive(false);
        CheckerProduct2.gameObject.SetActive(false);
        CheckerProduct3.gameObject.SetActive(false);

        ImageProduct1.sprite = _fallingObjectsSpawner.selectedProducts[0].Sprite;
        ImageProduct2.sprite = _fallingObjectsSpawner.selectedProducts[1].Sprite;
        ImageProduct3.sprite = _fallingObjectsSpawner.selectedProducts[2].Sprite;

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
                    iconShake1.Shake();
                    //StartCoroutine(AddScoreAnimation(product, ImageProduct1.transform.position));
                    break;
                case 1:
                    Point2 = Mathf.Max(product.Amount, 0);
                    textproduct2.text = Point2.ToString();
                    iconShake2.Shake();
                    //StartCoroutine(AddScoreAnimation(product, ImageProduct2.transform.position));
                    break;
                case 2:
                    Point3 = Mathf.Max(product.Amount, 0);
                    iconShake3.Shake();
                    //StartCoroutine(AddScoreAnimation(product, ImageProduct3.transform.position));
                    textproduct3.text = Point3.ToString();
                    break;
            }

            if (Point1 == 0)
            {
                textproduct1.gameObject.SetActive(false);
                CheckerProduct1.gameObject.SetActive(true);
            }
            if (Point2 == 0)
            {
                textproduct2.gameObject.SetActive(false);
                CheckerProduct2.gameObject.SetActive(true);
            }
            if (Point3 == 0)
            {
                textproduct3.gameObject.SetActive(false);
                CheckerProduct3.gameObject.SetActive(true);
            }
            // Проверяем, все ли продукты собраны
            StartCoroutine(CheckProducts());            
        }
    }

    private IEnumerator CheckProducts()
    {
        yield return new WaitForSeconds(0.5f);
        _scoreManager.CheckIfAllProductsCollected();
    }    
}
