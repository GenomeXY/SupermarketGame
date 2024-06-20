using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    // ��������� ������������
    public float duration = 0.2f; // ����������������� ������������
    public float magnitude = 15f; // ��������� ������������

    // ������������ �������
    private Vector3 originalPos;

    void OnEnable()
    {
        // ��������� ������������ ������� ������
        originalPos = transform.localPosition;
    }

    public void Shake()
    {
        // ��������� �������� ��� ������������
        StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            // ���������� ��������� �������� ��� ������
            float offsetX = Random.Range(-1f, 1f) * magnitude;
            float offsetY = Random.Range(-1f, 1f) * magnitude;

            // ��������� �������� � �������
            transform.localPosition = new Vector3(originalPos.x + offsetX, originalPos.y + offsetY, originalPos.z);

            // ����������� �����
            elapsed += Time.deltaTime;

            // ���� �� ���������� �����
            yield return null;
        }

        // ���������� ������ �� ������������ �������
        transform.localPosition = originalPos;
    }
}
