using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;
using System;

public class ObjectInterAction : MonoBehaviour
{
    // ������� ������� ��� �������
    public static Action OnStartTapped;
    [SerializeField] private TapGesture _tapGesture;

    private void Start()
    {
        // ������������� �� ������� �������
        _tapGesture.Tapped += OnTapped;
    }

    private void OnDisable()
    {
        // ������������ �� ������� �������
        _tapGesture.Tapped -= OnTapped;
    }

    private void OnTapped(object sender, System.EventArgs e)
    {
        // �������� ������� �������
        OnStartTapped?.Invoke();
    }
}
