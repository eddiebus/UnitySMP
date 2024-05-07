using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class EnemySpawnSet
{
    public GameObject Prefab;
    public UnityEvent OnSpawn;
    public float SpawnDelay = 3.0f;
    public bool HaltSpawning = false;
    public void SpawnSet()
    {
        var newObj = GameObject.Instantiate(Prefab);
        newObj.transform.position = Vector3.zero;
        OnSpawn.Invoke();
    }
}

public class EnemySpawner : MonoBehaviour
{
    public float TimeDelay = 1.0f;
    protected float _TimeTillSpawn;
    public List<EnemySpawnSet> EnemySets;
    protected bool _WaitingForClear = false;

    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        _SpawnerTick();
        if (_TimeTillSpawn <= 0)
        {
            if (!_WaitingForClear)
            {
                if (EnemySets.Count > 0)
                {
                    // Spawn random set. Do not edit list
                    int selectIndex = UnityEngine.Random.Range(0, EnemySets.Count);
                    EnemySets[selectIndex].SpawnSet();
                    _TimeTillSpawn = EnemySets[selectIndex].SpawnDelay;
                    
                    // Set halts upcoming spawn till wave cleared
                    if (EnemySets[selectIndex].HaltSpawning) _WaitingForClear = true;
                }
            }
            else{
                if (Enemy.All.Length == 0){
                    _WaitingForClear = false;
                }
            }
        }
    }
    protected void _SpawnerTick()
    {
        if (_TimeTillSpawn > 0)
        {
            _TimeTillSpawn -= Time.deltaTime;
        }
    }

    public void SetTimeDelay(float newDelay)
    {
        TimeDelay = newDelay;
    }

    public void WaitForClear()
    {
        _WaitingForClear = true;
    }


}
