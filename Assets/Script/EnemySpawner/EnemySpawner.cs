using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class EnemySpawnSet
{
    public GameObject Prefab;
    public UnityEvent OnSpawn;
    public void SpawnSet()
    {
        var newObj = GameObject.Instantiate(Prefab);
        newObj.transform.position = Vector3.zero;
        OnSpawn.Invoke();
    }
}

public enum EnemySpawnerState
{
    LevelMode,
    EndlessMode, // Plays from animation
    WaitForClear
}

public class EnemySpawner : MonoBehaviour
{
    public EnemySpawnerState State;
    public float TimeDelay = 1.0f;
    protected float _TimeTillSpawn;
    public List<EnemySpawnSet> EnemySets;

    // Update is called once per frame
    void Update()
    {
        _SpawnerTick();
        switch (State)
        {
            case EnemySpawnerState.EndlessMode:
                {
                    if (_TimeTillSpawn <= 0)
                    {
                        if (EnemySets.Count > 0)
                        {
                            // Spawn random set. Do not edit list
                            int selectIndex = UnityEngine.Random.Range(0,EnemySets.Count);
                            EnemySets[selectIndex].SpawnSet();
                            _TimeTillSpawn = TimeDelay;
                        }
                    }
                    break;
                }
            case EnemySpawnerState.LevelMode:
                {
                    if (_TimeTillSpawn <= 0)
                    {
                        if (EnemySets.Count > 0)
                        {
                            // Spawn next set and remove from list
                            EnemySets[0].SpawnSet();
                            EnemySets.RemoveAt(0);
                            _TimeTillSpawn = TimeDelay;
                        }
                    }
                    break;
                }
            case EnemySpawnerState.WaitForClear:
                {
                    if (Enemy.All.Length == 0)
                    {
                        State = EnemySpawnerState.LevelMode;
                    }
                    break;
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
        State = EnemySpawnerState.WaitForClear;
    }


}
