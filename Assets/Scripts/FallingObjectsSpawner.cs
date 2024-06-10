using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjectsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _fallingObjectPrefabs; // Массив префабов падающих предметов
    [SerializeField] private float _spawnInterval = 1.0f; // Интервал между спавнами предметов
    [SerializeField] private float _spawnRange = 2.5f; // Диапазон, в котором предметы могут появляться (соответствует диапазону передвижения игрока)

    private float _timeSinceLastSpawn = 0.0f;

    private void Update()
    {
        _timeSinceLastSpawn += Time.deltaTime;

        if (_timeSinceLastSpawn >= _spawnInterval)
        {
            SpawnFallingObject();
            _timeSinceLastSpawn = 0.0f;
        }
    }

    private void SpawnFallingObject()
    {
        if (_fallingObjectPrefabs.Length == 0)
        {
            Debug.LogWarning("Для падающих объектов не назначены префабы.");
            return;
        }

        float randomX = Random.Range(-_spawnRange, _spawnRange);
        Vector3 spawnPosition = new Vector3(randomX, transform.position.y, transform.position.z);

        // Выбираем случайный префаб из массива
        int randomIndex = Random.Range(0, _fallingObjectPrefabs.Length);
        GameObject selectedPrefab = _fallingObjectPrefabs[randomIndex];

        // Создаем падающий объект 
        GameObject fallingObject = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);
    }
}
