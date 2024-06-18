using System.Collections;
using System.Collections.Generic;
using TMPro;
using TouchScript.Gestures;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerWithTapGesture : MonoBehaviour
{
    [SerializeField] private TapGesture _tapGesture;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _maxXposition = 2.6f;
    [SerializeField] private float _moveSpeed = 0.3f;
    [SerializeField] private float _tapCooldown = 0f;

    private Vector3 _targetPosition;
    private bool _canTap = true;

    private void Start()
    {
        // �������� �� ������� �����
        _tapGesture.Tapped += OnTap;

        // ��������� ��������� ������� �������
        _targetPosition = _playerTransform.position;
    }    

    private void OnTap(object sender, System.EventArgs e)
    {
        if (_canTap && sender is TapGesture gesture)
        {
            StartCoroutine(TapCooldownRoutine());

            // ����������� ������ �����-������ � ����������� �� ����� �������
            Vector3 tapPosition = Camera.main.ScreenToWorldPoint(new Vector3(gesture.ScreenPosition.x, gesture.ScreenPosition.y, -Camera.main.transform.position.z));
            float tapPositionX = tapPosition.x;

            // ����������� ������� �������
            Vector3 targetPosition = new Vector3(Mathf.Clamp(tapPositionX, -_maxXposition, _maxXposition), _playerTransform.position.y, _playerTransform.position.z);

            // ������ �������� �����������
            StartCoroutine(MovePlayer(targetPosition, _moveSpeed));
        }
    }

    private void OnDestroy()
    {
        // ������� �� ������� �����
        if (_tapGesture != null)
        {
            _tapGesture.Tapped -= OnTap;
        }
    }

    private IEnumerator MovePlayer(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = _playerTransform.position;

        while (time < duration)
        {
            _playerTransform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        // ����������, ��� ����� ����� ������ ������� �������
        _playerTransform.position = targetPosition;
    }

    private IEnumerator TapCooldownRoutine()
    {
        _canTap = false;
        yield return new WaitForSeconds(_tapCooldown);
        _canTap = true;
    }
}
