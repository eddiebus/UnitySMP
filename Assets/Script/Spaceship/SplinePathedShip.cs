using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;



public class SplinePathedShip : Ship
{
    public Enemy _EnemyComponent;
    public SplineContainer splineContainer;
    public bool Loop;
    public bool RotateInDirection;
    public int TargetSplineIndex;
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

    protected float GetNormalisedPos()
    {
        if (TargetSpline == null)
        {
            return 0.0f;
        }
        else
        {

            var normalizedPos = SplineUtility.ConvertIndexUnit(
                TargetSpline,
                _DistanceCovered,
                PathIndexUnit.Distance,
                PathIndexUnit.Normalized
                );

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

    protected void _SplineShipMove()
    {
        if (TargetSpline == null) return;
        else
        {
            _DistanceCovered += Time.deltaTime * MoveSpeed;
            Vector3 truePos = SplineUtility.EvaluatePosition(
                TargetSpline,
                GetNormalisedPos()
            );
            //truePos += splineContainer.transform.position;

            _ShipRigidbody.transform.position = truePos;


            if (RotateInDirection)
            {
                Vector3 LookDir = GetCurrentTangent().normalized;
                _ShipRigidbody.transform.rotation = Quaternion.LookRotation(
                    LookDir,
                    Vector3.back
                );
            }

            if (GetNormalisedPos() >= 1.0f)
            {
                if (Loop)
                {
                    _DistanceCovered = 0;
                }
                else{
                    if (_EnemyComponent){
                        GameObject.Destroy(_EnemyComponent.GetCharacterRootObj());
                    }
                }
            }



        }
    }
}
