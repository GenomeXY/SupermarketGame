using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleUp : MonoBehaviour
{
    public float duration = 1.0f; // Длительность анимации в секундах

    private Vector3 targetScale;
    private float timeElapsed = 0f;
    private bool scaling = true;

    void Start()
    {
        // Сохраните целевой масштаб (должен быть масштабом в 1, если объект изначально был в полном размере)
        targetScale = transform.localScale;

        // Установите начальный масштаб в 0
        transform.localScale = Vector3.zero;
    }

    void Update()
    {
        if (scaling)
        {
            // Рассчитываем прошедшее время
            timeElapsed += Time.deltaTime;

            // Вычисляем процент завершения анимации
            float progress = Mathf.Clamp01(timeElapsed / duration);

            // Устанавливаем текущий масштаб объекта
            transform.localScale = Vector3.Lerp(Vector3.zero, targetScale, progress);

            // Останавливаем анимацию, когда достигли полного масштаба
            if (progress >= 1.0f)
            {
                scaling = false;
            }
        }
    }
}
