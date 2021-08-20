using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _template;

    private Transform _pointPatrol1;
    private Transform _pointPatrol2;

    private void Awake()
    {
        if (transform.childCount != 2)
        {
            Debug.LogError("Должно быть 2 точки патруля");
        }
        else
        {
            _pointPatrol1 = transform.GetChild(0);
            _pointPatrol2 = transform.GetChild(1);
        }
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        var createdEnemy = Instantiate(_template, transform.position, Quaternion.identity);
        createdEnemy.gameObject.SetActive(false);
        createdEnemy.Init(_pointPatrol1, _pointPatrol2);
    }
}
