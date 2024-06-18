using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProductsTypes
{
    Empty,
    Watermelon,
    Apple,
    Mushroom,
    Cheese,
    Donut,
    Tomato,
    CornFlakes,
    Eggs,
    Fish,
    Chicken,
    Milk,
    Baguette
}
public abstract class Products : ScriptableObject
{
    public ProductsTypes ProductTypes;
    public GameObject Prefab;
    public Sprite Sprite;
    public int Amount;

    public abstract void Initialize();
}
