using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewProduct", menuName = "Products/Product")]
public class Product : Products
{  
    public override void Initialize()
    {
        Amount = Random.Range(1, 6); // Устанавливаем случайное значение (кол-во продукта для сбора)
    }

    public override void SetDropChance(float chance)
    {
        DropChance = chance;
    }
}
