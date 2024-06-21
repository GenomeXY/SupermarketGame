using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseButton : MonoBehaviour
{
    public float pulseSpeed = 10.0f;  // Скорость пульсации
    public float pulseAmount = 0.1f; // Максимальное изменение масштаба (размер пульсации)

    private RectTransform rectTransform;
    private Vector3 originalScale;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalScale = rectTransform.localScale;
    }

    void Update()
    {
        // рассчет нового масштаба
        float scale = 1.0f + Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;

        rectTransform.localScale = originalScale * scale;
    }
}
