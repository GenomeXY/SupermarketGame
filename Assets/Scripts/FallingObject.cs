using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FallingObject : MonoBehaviour
{
    [SerializeField] private float _fallSpeed = 1.0f;
    [SerializeField] private GameObject _objectExplosion;
    private void Update()
    {
        // ���������� Translate ��� �������� �������
        transform.Translate(Vector3.down * _fallSpeed * Time.deltaTime, Space.World);

        // ������� ������, ����� �� ������� �� ������������ �������
        if (transform.position.y < -2f)
        {
            Destroy(gameObject);
        }
    }    

    public void Die()
    {
        Instantiate(_objectExplosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponentInParent<Player>();
        if (player)
        {
            Die();
        }
    }
}
