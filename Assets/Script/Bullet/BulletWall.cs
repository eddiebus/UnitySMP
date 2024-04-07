using UnityEngine;

public class BulletWall : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col)
    {
        var rigidbody = col.rigidbody;
        if (rigidbody)
        {
            var gameObject = rigidbody.gameObject;
            var bulletComponent = gameObject.GetComponent<Bullet>();
            if (bulletComponent)
            {
                GameObject.Destroy(bulletComponent.gameObject);
            }
        }
    }
}
