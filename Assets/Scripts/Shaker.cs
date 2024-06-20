using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    // Параметры подергивания
    public float duration = 0.2f; // Продолжительность подергивания
    public float magnitude = 15f; // Амплитуда подергивания

    // Оригинальная позиция
    private Vector3 originalPos;

    void OnEnable()
    {
        // Сохраняем оригинальную позицию иконки
        originalPos = transform.localPosition;
    }

    public void Shake()
    {
        // Запускаем корутину для подергивания
        StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            // Генерируем случайное смещение для иконки
            float offsetX = Random.Range(-1f, 1f) * magnitude;
            float offsetY = Random.Range(-1f, 1f) * magnitude;

            // Применяем смещение к позиции
            transform.localPosition = new Vector3(originalPos.x + offsetX, originalPos.y + offsetY, originalPos.z);

            // Увеличиваем время
            elapsed += Time.deltaTime;

            // Ждем до следующего кадра
            yield return null;
        }

        // Возвращаем иконку на оригинальную позицию
        transform.localPosition = originalPos;
    }
}
