using UnityEngine;
using System.Collections.Generic;

public class TailAnimation : MonoBehaviour
{
    public int length; // how many tail segments
    public LineRenderer lineRend; // the line renderer for the tail
    Vector3[] segmentPoses; // positions of each tail segment
    Vector3[] segmentV; // velocity for smoothdamp
    public Transform targetDir; // the object the tail follows
    public float targetDistance; // spacing between segments
    public float smoothSpeed; // how smooth the tail moves

    [Header("Segments")]
    public bool useSegments; // if true, spawn physical segment objects
    public GameObject segment; // prefab for each segment
    List<GameObject> segments = new List<GameObject>();
    public float segmentAngleOffset; // rotate segment by this if needed
    public GameObject segmentend;

    void Start()
    {
        // setup line renderer
        lineRend.positionCount = length;
        segmentPoses = new Vector3[length];
        segmentV = new Vector3[length];

        // spawn segment objects if needed
        if (useSegments)
        {
            for (int i = 0; i < length; i++)
            {
                if (i == length - 1)
                {
                    GameObject a = Instantiate(segmentend, transform.position, transform.rotation, transform);
                    segments.Add(a);
                }
                else
                {
                    GameObject a = Instantiate(segment, transform.position, transform.rotation, transform);
                    segments.Add(a);
                }
                    
            }
        }
    }

    void Update()
    {
        // first segment follows the target
        segmentPoses[0] = targetDir.position;

        // loop through tail segments and move them smoothly
        for (int i = 1; i < segmentPoses.Length; i++)
        {
            Vector3 targetPos = segmentPoses[i - 1] + (segmentPoses[i] - segmentPoses[i - 1]).normalized * targetDistance;
            segmentPoses[i] = Vector3.SmoothDamp(segmentPoses[i], targetPos, ref segmentV[i], smoothSpeed);
        }

        // update line renderer positions
        lineRend.SetPositions(segmentPoses);

        // move and rotate physical segments if using them
        if (useSegments)
        {
            for (int i = 1; i < segments.Count; i++)
            {
                segments[i].transform.position = segmentPoses[i];
                Vector3 dir = (segments[i - 1].transform.position - segments[i].transform.position).normalized;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                segments[i].transform.rotation = Quaternion.Euler(0, 0, angle + segmentAngleOffset);
            }

            // first segment follows target rotation too
            Vector3 d = (targetDir.position - segments[0].transform.position).normalized;
            segments[0].transform.localRotation = Quaternion.Euler(0, 0, Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg + segmentAngleOffset);
            segments[0].transform.position = targetDir.position;
        }
    }
}
