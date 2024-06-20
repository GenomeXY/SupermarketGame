using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.Linq;

public class FallingObject : MonoBehaviour
{
    public static Action<ProductsTypes> OnCollected; // ����������� � ����� ��������
    
    [SerializeField] private float _fallSpeed = 1.0f;

    [SerializeField] private GameObject _collectEffectSelected;
    [SerializeField] private GameObject _collectEffectOther;

    [SerializeField] private GameObject _dieEffect;

    [SerializeField] private Product _product; // ������ �� ScriptableObject
    [SerializeField] private ProductsTypes _productTypes;

    private FallingObjectsSpawner _spawner;

    private void Awake()
    {
        _spawner = FindObjectOfType<FallingObjectsSpawner>();
    }
    private void Start()
    {
        if (_product != null)
        {
            _productTypes = _product.ProductTypes;
        }
        else
        {
            Debug.LogError("������� �� �������� �� ������.");
        }
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
            GameObject effectToInstantiate;
            if (_spawner.selectedProducts.Any(product => product.ProductTypes == _productTypes))
            {
                effectToInstantiate = _collectEffectSelected;
                MyAudioManager.Instance.PlaySoundWithRandomPitch(MyAudioManager.Instance.FoodColectSound);
            }
            else
            {
                effectToInstantiate = _collectEffectOther;
                MyAudioManager.Instance.PlaySoundWithRandomPitch(MyAudioManager.Instance.ErrorSound, 1.0f, 1.2f);
            }

            // ������� ������ � ����������� ��� � �������
            GameObject effectInstance = Instantiate(effectToInstantiate, transform.position, Quaternion.identity);
            effectInstance.transform.SetParent(other.transform);

            
            OnCollected?.Invoke(_productTypes);

            Destroy(gameObject);
        }
        else
        {
            // ������� ������ � ����������� ��� � �������
            GameObject effectInstance = Instantiate(_dieEffect, transform.position, Quaternion.identity);

            MyAudioManager.Instance.PlaySoundWithRandomPitch(MyAudioManager.Instance.SmashSound);
            Destroy(gameObject);
        }
    }
}
