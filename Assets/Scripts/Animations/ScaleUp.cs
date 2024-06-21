using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleUp : MonoBehaviour
{
    public float duration = 1.0f; // Длительность анимации в секундах

    private Vector3 targetScale;
    private float timeElapsed = 0f;
    private bool scaling = false;

    void Start()
    {
        // Сохраняем целевой масштаб 
        targetScale = transform.localScale;

        // Установливаем начальный масштаб в 0
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
    public void StartScaling()
    {
        scaling = true;
        timeElapsed = 0f;
        transform.localScale = Vector3.zero;
    }
}
