using System;
using UnityEngine;
using UnityEngine.Splines;

public enum SplineShipDirection
{
    Forwards,
    Backward
}

public class SplinePathedShip : Ship
{
    public Enemy _EnemyComponent;
    public bool AutoFire; // To Constant Fire
    public SplineContainer splineContainer;
    public bool Loop;
    public SplineShipDirection PathDirection;
    public bool RotateInDirection;
    public int TargetSplineIndex;

    public UnityEngine.Events.UnityEvent OnSplineComplete;
    protected Spline TargetSpline = null;
    protected float _DistanceCovered = 0;
    // Start is called before the first frame update
    void Start()
    {
        SplineShipInit();
    }

    // Update is called once per frame
    void Update()
    {
        _SplineShipMove();
        _ShipSetRigidbody();

        Fire();
    }

    protected void SplineShipInit()
    {
        _ShipInit();
        if (splineContainer)
        {
            if (splineContainer.Splines.Count > 0)
            {
                if (TargetSplineIndex >= splineContainer.Splines.Count)
                {
                    TargetSplineIndex = splineContainer.Splines.Count - 1;
                }
                else if (TargetSplineIndex < 0)
                {
                    TargetSplineIndex = 0;
                }

                TargetSpline = splineContainer.Splines[
                    TargetSplineIndex
                ];
            }
        }
    }

    protected bool HasFinishedPath()
    {
        // No Spline  so never completed
        if (TargetSpline == null)
        {
            return false;
        }
        else
        {
            if (!Loop)
            {
                float splineLength = TargetSpline.GetLength();
                if (_DistanceCovered >= splineLength)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                // Looping mode, Can Never Finish
                return false;
            }
        }
    }

    protected float GetSplineLength()
    {
        if (TargetSpline == null)
        {
            return 0;
        }
        else
        {
            return TargetSpline.GetLength();
        }
    }

    protected float GetNormalisedPos()
    {
        float defaultPos = (float)Convert.ToDouble(
                PathDirection == SplineShipDirection.Backward
            );

        if (TargetSpline == null)
        {
            return defaultPos;
        }
        else
        {

            var normalizedPos = SplineUtility.ConvertIndexUnit(
                TargetSpline,
                _DistanceCovered,
                PathIndexUnit.Distance,
                PathIndexUnit.Normalized
                );

            if (
                PathDirection == SplineShipDirection.Backward
                )
            {
                normalizedPos = 1.0f - normalizedPos;
            }

            return normalizedPos;
        }
    }

    protected Vector3 GetCurrentTangent()
    {
        if (TargetSpline == null)
        {
            return Vector3.down;
        }
        else
        {
            return SplineUtility.EvaluateTangent(TargetSpline, GetNormalisedPos());
        }
    }

    // Move the ship
    protected void _SplineShipMove()
    {
        if (TargetSpline == null) return;
        else
        {

            // Before moving Ship could finish path
            bool toCompleteSpline = !this.HasFinishedPath();

            _DistanceCovered += Time.deltaTime * MoveSpeed;
            Vector3 truePos = SplineUtility.EvaluatePosition(
                TargetSpline,
                GetNormalisedPos()
            );
            // Set Ship Position
            truePos += splineContainer.transform.position;
            _ShipRigidbody.transform.position = truePos;


            // Rotate the object in direction of the spline
            if (RotateInDirection)
            {
                Vector3 LookDir = GetCurrentTangent().normalized;
                if (PathDirection == SplineShipDirection.Backward)
                {
                    LookDir *= -1;
                }
                _ShipRigidbody.transform.rotation = Quaternion.LookRotation(
                    Vector3.forward,
                    LookDir
                );
            }

            // Did the ship complete the spline after moving
            if (toCompleteSpline && HasFinishedPath())
            {
                OnSplineComplete.Invoke();
            }


            if (Loop)
            {
                if (_DistanceCovered >= GetSplineLength())
                {
                    _DistanceCovered = 0;
                }
            }


        }
    }
}
