using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFly : MonoBehaviour
{
    public float speed = 700f; // Скорость отлета
    private RectTransform rectTransform;
    private Vector2 originalPosition;
    private bool _isFlying = false;
    [SerializeField] private bool _isText;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition; // Сохраните исходную позицию
    }

    void Update()
    {
        if (_isFlying && _isText)
        {
            // Перемещение элемента вниз
            rectTransform.anchoredPosition -= new Vector2(0, speed * Time.deltaTime);
        }
        else if (_isFlying)
        {
            // Перемещение элемента влево            
            rectTransform.anchoredPosition -= new Vector2(speed * Time.deltaTime, 0);
        }
    }

    // Метод для запуска анимации отлета вниз
    public void StartFlying()
    {
        _isFlying = true;
    }

    // Метод для остановки анимации и возврата элемента в исходную позицию
    public void StopFlying()
    {
        _isFlying = false;
        rectTransform.anchoredPosition = originalPosition; // Вернуть в исходную позицию
    }
}
