using UnityEngine;

public class RandomDestroy : MonoBehaviour
{
    [Range(0, 1)]
    public float Chance = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        if (UnityEngine.Random.Range(0.0f, 1) < Chance)
            GameObject.Destroy(this.gameObject);
    }
}
