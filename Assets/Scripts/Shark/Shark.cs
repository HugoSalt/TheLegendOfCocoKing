using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour
{
    
    public LineRenderer lineRenderer;
    public bool playMode;

    int numPoints;
    int resolution;
    float totalDistance;
    Vector3[] positions_raw;
    Vector3[] positions;

    public float health;
	private bool isSinking;
    public float speed;

    public float sinkingSpeed;
    
    public Shark() {
        numPoints = 1000;
        resolution = 1000;
        speed = 2f;
        positions_raw = new Vector3[numPoints];
        positions = new Vector3[resolution];
    }


    // Use this for initialization
    void Start()
    {
        lineRenderer.positionCount = numPoints;
        float baseTime = Time.realtimeSinceStartup;
        drawCurve();
        //print(Time.realtimeSinceStartup - baseTime);
        if (playMode) {
            transform.Find("BezierPoints").gameObject.SetActive(false);
            lineRenderer.enabled = false;
        }
        else {
            // edit mode : we want to show the bezier point and the curve
            transform.Find("BezierPoints").gameObject.SetActive(true);
            lineRenderer.enabled = true;
        }
        /*for (int i = 0; i < numPoints - 1; ++i)
        {
            GameObject cube_raw = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube_raw.GetComponent<MeshRenderer>().material.color = new Color(204f, 204f, 0f);
            Instantiate(cube_raw, positions_raw[i], Quaternion.identity);
        }
        for (int i = 0; i < resolution; ++i)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.GetComponent<MeshRenderer>().material.color = new Color(0f, 0f, 100f);
            Instantiate(cube, positions[i], Quaternion.identity);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if (playMode)
        {
            if (isSinking) {
                transform.Translate(- Vector3.up * sinkingSpeed);
            }
            else {
                updatePosition();
            }
        }
        else
        {
            drawCurve();
        }
    }

    public void TakeDamage() {
		health -= 1;
		if (health <= 0) DestroyShark();
		//foreach (Transform child in transform.Find("CollisionFX"))
		//	child.GetComponent<ParticleSystem>().Play();
		//GetComponents<AudioSource>()[1].Play();	
	}

	private void DestroyShark() {
		isSinking = true;
		//foreach (Transform child in transform.Find("SinkingFX"))
		//	child.GetComponent<ParticleSystem>().Play();		
	}

    private void drawCurve()
    {
        Transform bezierPoints = transform.Find("BezierPoints");
        int bezierPointsCount = bezierPoints.childCount;
        Vector3[] startingPoints = new Vector3[bezierPointsCount + 1];
        for (int i = 0; i < bezierPointsCount; ++i)
        {
            startingPoints[i] = bezierPoints.GetChild(i).position;
        }
        startingPoints[bezierPointsCount] = bezierPoints.GetChild(0).position;

        // For all t (resolution of the curve)
        for (int i = 0; i < numPoints; ++i)
        {
            Vector3[] points = (Vector3[])startingPoints.Clone();
            float t = (float)i / (float)(numPoints - 1);
            int iterationLeft = bezierPointsCount;

            while (iterationLeft > 0)
            {
                for (int j = 0; j < iterationLeft; ++j)
                {
                    points[j] = points[j] + t * (points[j + 1] - points[j]);
                }
                --iterationLeft;
            }
            positions_raw[i] = points[0];
        }

        // Here positions_raw is a list of Vector3 representing points on the 
        // bezier curve. The first and the last one are on the same spot.

        // The positions don't have equal distance, we have to compute new
        // points where the distance is the same from one to the next. 
        float distanceLoop = 0.0f;
        for (int i = 0; i < numPoints - 1; ++i)
        {
            distanceLoop += (positions_raw[i + 1] - positions_raw[i]).magnitude;
        }
        totalDistance = distanceLoop;

        // Now we go along the loop, and stop as soon we travel the correct amount
        positions[0] = positions_raw[0];
        int last = 0; // last raw point reached
        int next = last + 1; // next raw point to reach
        float distanceStep = distanceLoop / resolution;
        for (int i = 1; i < resolution; ++i)
        {
            // The current position is on a uniform point.
            Vector3 currentPosition = positions[i - 1];
            float nextRawPointDistance = (positions_raw[next] - currentPosition).magnitude;
            // If we reach directly another uniform point
            if (nextRawPointDistance >= distanceStep)
            {
                Vector3 nextStepVec = (distanceStep / nextRawPointDistance) * (positions_raw[next] - currentPosition);
                positions[i] = currentPosition + nextStepVec;
            }
            // If we reach a raw point
            else
            {
                // We move to the raw point;
                float distanceFromLastUniform = (positions_raw[next] - currentPosition).magnitude;
                currentPosition = positions_raw[next];
                last = next;
                next += 1;
                float distanceToNextRaw = (positions_raw[next] - currentPosition).magnitude;
                // We have to check if we don't hit some other raw points directly
                while (distanceFromLastUniform + distanceToNextRaw < distanceStep)
                {
                    distanceFromLastUniform += distanceToNextRaw;
                    currentPosition = positions_raw[next];
                    last = next;
                    next += 1;
                }
                // Now we know that the next point is a uniform
                nextRawPointDistance = (positions_raw[next] - currentPosition).magnitude;
                Vector3 nextStepVec = ((distanceStep - distanceFromLastUniform) / nextRawPointDistance) * (positions_raw[next] - currentPosition);
                positions[i] = currentPosition + nextStepVec;
            }
        }

        lineRenderer.SetPositions(positions_raw);
    }

    void updatePosition()
    {
        float time = Time.time;
        float timeLoop = totalDistance / speed;
        float index_float = ((time % timeLoop) * resolution / timeLoop);
        int index = (int)index_float;
        float coef = index_float - index;
        Vector3 pos1 = positions[index];
        Vector3 pos2 = positions[0];
        if (index + 1 < resolution)
        {
            pos2 = positions[index + 1];
        }
        transform.position = (1 - coef) * pos1 + coef * pos2;

        Vector3 direction = pos2 - pos1;
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
