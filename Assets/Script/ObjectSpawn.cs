using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawn : MonoBehaviour
{
    public GameObject obj;

    public void SpawnObject()
    {
        GameObject newobj =  GameObject.Instantiate(obj);
        newobj.transform.position = this.transform.position;

    }
}
