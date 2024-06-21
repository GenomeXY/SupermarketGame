using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleUp : MonoBehaviour
{
    public float duration = 1.0f; // ������������ �������� � ��������

    private Vector3 targetScale;
    private float timeElapsed = 0f;
    private bool scaling = false;

    void Start()
    {
        // ��������� ������� ������� 
        targetScale = transform.localScale;

        // ������������� ��������� ������� � 0
        transform.localScale = Vector3.zero;
    }

    void Update()
    {
        if (scaling)
        {
            // ������������ ��������� �����
            timeElapsed += Time.deltaTime;

            // ��������� ������� ���������� ��������
            float progress = Mathf.Clamp01(timeElapsed / duration);

            // ������������� ������� ������� �������
            transform.localScale = Vector3.Lerp(Vector3.zero, targetScale, progress);

            // ������������� ��������, ����� �������� ������� ��������
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
