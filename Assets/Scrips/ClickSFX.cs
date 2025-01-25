using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ClickSFX : MonoBehaviour
{
    [SerializeField] private RandomSFX randomSFX;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
            randomSFX.PlayRandomSFX();
    }
}
