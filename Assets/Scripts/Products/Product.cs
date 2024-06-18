using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewProduct", menuName = "Products/Product")]
public class Product : Products
{  
    public override void Initialize()
    {
        Amount = Random.Range(1, 4); // Устанавливаем случайное значение от 1 до 4
    }
}
