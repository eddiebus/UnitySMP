using UnityEngine;

public class RigidBody2DBounds
{
    private Rigidbody2D _body;
    private Bounds _ResultBounds;
    public Bounds Bounds => _ResultBounds;


    public static Bounds Get(Rigidbody2D Body)
    {
        Bounds totalBounds = new Bounds(Body.position, Vector3.zero);
        if (Body != null)
        {
            var colliders = Body.gameObject.GetComponentsInChildren<Collider2D>();

            foreach (var c in colliders)
            {
                totalBounds.Encapsulate(c.bounds);
            }
        }
        return totalBounds;
    }
    public RigidBody2DBounds(Rigidbody2D ownerBody)
    {
        _body = ownerBody;
        _CalculateBounds();
    }

    private void _CalculateBounds()
    {
        _ResultBounds = new Bounds(Vector3.zero, Vector3.zero);
        if (!_body) return;
        var gameobject = _body.gameObject;
        if (!gameobject) return;
        Collider2D[] allColliders = gameobject.GetComponentsInChildren<Collider2D>();
        for (int i = 0; i < allColliders.Length; i++)
        {
            if (i == 0)
            {
                _ResultBounds = allColliders[i].bounds;
            }
            else
            {
                _ResultBounds.Encapsulate(allColliders[i].bounds);
            }
        }
    }
    public Bounds GetBounds()
    {
        return _ResultBounds;
    }

    public void DrawDebugGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_ResultBounds.center, _ResultBounds.size);
    }
}
