using System.Collections;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private int _health;
    private float _timehealth;

    private float _maxTimeHealth = 3f;
    private int _maxHealth = 100;
    private bool _isHealing = false;


    private void Start()
    {
        if (_isHealing)
        {
            return;
        }
        else
        {
            ReceiveHealing();
        }
        
    }
    public void ReceiveHealing()
    {
        StartCoroutine(Healing());
    }
    private IEnumerator Healing()
    {
        while (_health < _maxHealth && _timehealth < _maxTimeHealth)
        {
            _isHealing = true;
            _health += 5;
            MaxHp();
            _timehealth += 0.5f;
            yield return new WaitForSeconds(0.5f);
        }
        Debug.Log(_health);
        _maxHealth = 0;
    }
    private void MaxHp()
    {
        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }
    }
}
