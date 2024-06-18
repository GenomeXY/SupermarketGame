using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;
using System;

public class ObjectInterAction : MonoBehaviour
{
    // Создаем событие для нажатия
    public static Action OnStartTapped;
    [SerializeField] private TapGesture _tapGesture;

    private void Start()
    {
        // Подписываемся на событие нажатия
        _tapGesture.Tapped += OnTapped;
    }

    private void OnDisable()
    {
        // Отписываемся от события нажатия
        _tapGesture.Tapped -= OnTapped;
    }

    private void OnTapped(object sender, System.EventArgs e)
    {
        // Вызываем событие нажатия
        OnStartTapped?.Invoke();
    }
}
