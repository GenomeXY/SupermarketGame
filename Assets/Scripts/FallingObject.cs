using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class FallingObject : MonoBehaviour
{
    public static Action<ProductsTypes> OnCollected; // ����������� � ����� ��������
    
    [SerializeField] private float _fallSpeed = 1.0f;
    [SerializeField] private GameObject _collectEffect;
    [SerializeField] private GameObject _dieEffect;

    [SerializeField] private ProductsTypes _productTypes;
    private void Start()
    {
        _productTypes = GetComponent<ProductsTypes>();
    }
    private void Update()
    {
        // ���������� Translate ��� �������� �������
        transform.Translate(Vector3.down * _fallSpeed * Time.deltaTime, Space.World);
    }
    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponentInParent<Player>();
        if (player)
        {
            Instantiate(_collectEffect, transform.position, Quaternion.identity);
            MyAudioManager.Instance.FoodColectSound.Play();
            OnCollected?.Invoke(_productTypes);
            Destroy(gameObject);
        }
        else
        {
            Instantiate(_dieEffect, transform.position, Quaternion.identity);
            MyAudioManager.Instance.SmashSound.Play();
            Destroy(gameObject);
        }
    }
}
