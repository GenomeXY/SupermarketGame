using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS : MonoBehaviour
{
    [SerializeField] private int _targetFrameRate = 60; //reset ט -1 הכÿ סבנמסא
    void Start()
    {
        Application.targetFrameRate = _targetFrameRate;
    }
}
