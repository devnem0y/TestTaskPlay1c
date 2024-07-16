using System;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public event Action<int> Enter;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Enemy")) return;
        
        var enemy = other.gameObject.GetComponent<Enemy>();
        Enter?.Invoke(enemy.Damage);
    }
}