using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;



public class SplinePathedShip : Ship
{
    public SplineContainer splineContainer;
    public bool Loop;
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
                int selectIndex = UnityEngine.Random.Range(
                    0,
                    splineContainer.Splines.Count
                );
            }
        }
    }

    protected void SplineShipMove()
    {

    }
}
