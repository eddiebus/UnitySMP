using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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

    public Vector2 GetDirection(){
        return transform.rotation * Vector3.up;
    }

    public void Fire(GameObject bulletPrefab){
        if (bulletPrefab == null) return;
        if (_CheckPrefab() && _CurrentFireDelay <= 0)
        {
            var newBullet = GameObject.Instantiate(bulletPrefab);
            var Bulletcomp = newBullet.GetComponent<Bullet>();

            //Spawn Bullet
            if (Bulletcomp)
            {
                Bulletcomp.Direction = GetDirection();
                newBullet.transform.position = transform.position;
                _CurrentFireDelay = FireDelay;
                OnFire.Invoke();
            }
            //Not valid bullet prefab. Destroy
            else{
                GameObject.Destroy(newBullet);
            }
        }
    }

    public void Fire()
    {
        if (_CheckPrefab() && _CurrentFireDelay <= 0)
        {
            var newBullet = GameObject.Instantiate(BulletPrefab);
            var Bulletcomp = newBullet.GetComponent<Bullet>();

            //Spawn Bullet
            if (Bulletcomp)
            {
                Bulletcomp.Direction = GetDirection();
                newBullet.transform.position = transform.position;
                _CurrentFireDelay = FireDelay;
                OnFire.Invoke();
            }
            //Not valid bullet prefab. Destroy
            else{
                GameObject.Destroy(newBullet);
            }
        }
    }

    private bool _CheckPrefab()
    {
        var Bulletcomp = BulletPrefab.GetComponent<Bullet>();
        return Bulletcomp;
    }

    void OnDrawGizmos()
    {
        if (Selection.activeGameObject == this.gameObject)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(
                transform.position,
                transform.position + (transform.rotation * Vector2.up));
            Gizmos.DrawWireSphere(transform.position, 0.3f);
        }
    }
}
