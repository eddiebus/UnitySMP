using UnityEngine;


public class Player : Character
{
    public const int _DefaultLives = 3;
    public static int Lives = _DefaultLives;
    public static int Score;
    public float InvinTimeOnHit = 3.0f;
    public float InvinsibilityTime => _InvisibilityTime;
    protected float _InvisibilityTime = 0;

    void Start()
    {
        // Level Quit. Reset Lives and Score
        Level.OnLevelQuit += () =>
        {
            Lives = _DefaultLives;
            Score = 0;
        };

        this.OnDamage.AddListener(() =>
        {
            this._InvisibilityTime += InvinTimeOnHit;
        });
    }

    // Update is called once per frame
    void Update()
    {
        CharacterTag = CharacterNames.Player;
        if (_InvisibilityTime > 0)
        {
            _InvisibilityTime -= Time.deltaTime;
            this.Invincible = true;
        }
        else
        {
            this.Invincible = false;
        }
    }

    public static Player Get()
    {
        return FindFirstObjectByType<Player>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        var otherBody = col.rigidbody;
        var enemyComp = otherBody.gameObject.GetComponent<Enemy>();

        if (enemyComp)
        {
            this.Damage(0.2f);
            Debug.Log($"Player has hit Enemy {enemyComp.gameObject.name}");
        }
    }

}


