using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class multiSplineAnimator : MonoBehaviour
{
	public float Speed = 10.0f;
	public bool OrientToPath = true;
	public Spline[] Spline;
    [Range(0, 20)]
	public int mySpline = 0;
	public float randomTime = 0;


	double distanceTraveled;

	void Start()
	{
		distanceTraveled = 0;
	}

	void Update()
	{
		randomTime += Time.deltaTime;
		if (randomTime >= 4)
        {
			mySpline =  Random.Range(0, Spline.Length);
			randomTime = 0;
        }
		distanceTraveled += Time.deltaTime * Speed;

		SplineData data = Spline[mySpline].NextDataPoint(distanceTraveled);

		transform.position = data.Position;

		if (OrientToPath)
		{
			// For some reason calling SetLookRotation directly on transform.localRotation doesn't update anything
			Quaternion rot = Quaternion.LookRotation(data.Tangent, data.Normal);
			transform.localRotation = rot;
		}

	}
}
