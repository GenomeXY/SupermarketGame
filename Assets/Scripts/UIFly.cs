using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFly : MonoBehaviour
{
    public float speed = 700f; // �������� ������
    private RectTransform rectTransform;
    private Vector2 originalPosition;
    private bool _isFlying = false;
    [SerializeField] private bool _isText;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition; // ��������� �������� �������
    }

    void Update()
    {
        if (_isFlying && _isText)
        {
            // ����������� �������� ����
            rectTransform.anchoredPosition -= new Vector2(0, speed * Time.deltaTime);
        }
        else if (_isFlying)
        {
            // ����������� �������� �����            
            rectTransform.anchoredPosition -= new Vector2(speed * Time.deltaTime, 0);
        }
    }

    // ����� ��� ������� �������� ������ ����
    public void StartFlying()
    {
        _isFlying = true;
    }

    // ����� ��� ��������� �������� � �������� �������� � �������� �������
    public void StopFlying()
    {
        _isFlying = false;
        rectTransform.anchoredPosition = originalPosition; // ������� � �������� �������
    }
}
