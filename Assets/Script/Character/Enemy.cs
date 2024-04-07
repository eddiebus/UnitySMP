using UnityEngine;


public class Enemy : Character
{
    public static Enemy[] All => FindObjectsByType<Enemy>(
        FindObjectsInactive.Include,
        FindObjectsSortMode.InstanceID
        );

    public int ScoreValue;
    // Start is called before the first frame update
    void Start()
    {
        this.CharacterTag = CharacterNames.Enemy;
        this.OnDestroy.AddListener(() =>
        {
            Player.Score += ScoreValue;
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (Health < 0)
        {
            this.Destroy();
        }
    }
}
