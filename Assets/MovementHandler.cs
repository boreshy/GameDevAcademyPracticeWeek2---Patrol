using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHandler : MonoBehaviour
{

    [SerializeField] private Transform _target;
    [SerializeField] private float _speed; 
    
    [SerializeField] private List<Transform> _patrolPoints;
    private int _currentPointIndex = 0;
    
    [SerializeField] private float _waitTime = 2f;
    private float _waitTimer = 0f;


    // Update is called once per frame
    private void Update()
    {
        // Проверяем, есть ли точки патрулирования
        if (_patrolPoints.Count == 0)
        {
            Debug.LogWarning("No patrol points assigned!");
            return;
        }

        // Если достигнута текущая целевая точка, ожидаем некоторое время
        if (Vector3.Distance(transform.position, _patrolPoints[_currentPointIndex].position) < 0.1f)
        {
            _waitTimer += Time.deltaTime;
            if (_waitTimer >= _waitTime)
            {
                // Выбираем следующую точку патрулирования
                _currentPointIndex = (_currentPointIndex + 1) % _patrolPoints.Count;
                _waitTimer = 0f;
            }
        }
        else
        {
            // Перемещаемся к текущей целевой точке
            var travelDistance = _speed * Time.deltaTime;
            var newPosition = Vector3.MoveTowards(transform.position, _patrolPoints[_currentPointIndex].position, travelDistance);
            transform.position = newPosition;
        }
    }

    
}
