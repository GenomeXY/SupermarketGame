using System.Collections;
using System.Collections.Generic;
using TouchScript.Gestures.TransformGestures;
using TouchScript.Gestures;
using UnityEngine;

public class PlayerControllerWithGestures : MonoBehaviour
{
    [SerializeField] private PressGesture _pressGesture;
    [SerializeField] private ReleaseGesture _releaseGesture;
    [SerializeField] private TransformGesture _transformGesture;
    [SerializeField] private Transform _playerTransform;
    private bool _isPressed = false;

    // ���������� ��� ����������� �������� ������ �����-������
    private float _xPosition;
    [SerializeField] private float _maxXposition = 2.5f;

    private void Start()
    {
        _playerTransform = transform;

        // ��������� �����, ���� �� ��������� ����� ���������
        _pressGesture ??= GetComponent<PressGesture>();
        _releaseGesture ??= GetComponent<ReleaseGesture>();
        _transformGesture ??= GetComponent<TransformGesture>();

        // �������� �� ������� ������
        if (_pressGesture != null)
        {
            _pressGesture.Pressed += OnPress;
        }

        if (_releaseGesture != null)
        {
            _releaseGesture.Released += OnRelease;
        }

        if (_transformGesture != null)
        {
            _transformGesture.Transformed += OnTransform;
        }
    }

    private void OnPress(object sender, System.EventArgs e)
    {
        _isPressed = true;
    }

    private void OnRelease(object sender, System.EventArgs e)
    {
        _isPressed = false;
    }
    private void OnTransform(object sender, System.EventArgs e)
    {
        if (!_isPressed) return;

        if (sender is TransformGesture gesture)
        {
            // ���������� ������ �����-������ � ����������� �� ��������� ������� �����
            float deltaX = gesture.DeltaPosition.x;
            _xPosition = Mathf.Clamp(_playerTransform.position.x + deltaX, -_maxXposition, _maxXposition); // ������������ ��� �����������
            _playerTransform.position = new Vector3(_xPosition, _playerTransform.position.y, _playerTransform.position.z);
        }
    }

    private void OnDestroy()
    {
        // ������� �� ������� ������
        if (_pressGesture != null)
        {
            _pressGesture.Pressed -= OnPress;
        }

        if (_releaseGesture != null)
        {
            _releaseGesture.Released -= OnRelease;
        }

        if (_transformGesture != null)
        {
            _transformGesture.Transformed -= OnTransform;
        }
    }
}
