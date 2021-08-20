using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _template;

    private void Awake()
    {
        Instantiate(_template, transform.position, Quaternion.identity);
    }
}
