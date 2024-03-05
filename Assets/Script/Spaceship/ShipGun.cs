using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShipGun : MonoBehaviour
{
    public int GroupID;
    public GameObject BulletPrefab;

    public float FireDelay = 0.2f;
    public float _CurrentFireDelay;

    public UnityEvent OnFire;
    // Start is called before the first frame update
    void Start()
    {
        _CurrentFireDelay = FireDelay;
    }

    // Update is called once per frame
    void Update()
    {
        _GunTick();
    }

    protected void _GunTick()
    {
        if (_CurrentFireDelay > 0)
        {
            _CurrentFireDelay -= Time.deltaTime;
        }
    }

    public void Fire()
    {
        if (_CheckPrefab() && _CurrentFireDelay <= 0)
        {
            var newBullet = GameObject.Instantiate(BulletPrefab);
            newBullet.transform.position = transform.position;
            _CurrentFireDelay = FireDelay;
            OnFire.Invoke();
        }
    }

    private bool _CheckPrefab()
    {
        var Bulletcomp = BulletPrefab.GetComponent<Bullet>();
        return Bulletcomp;
    }
}
