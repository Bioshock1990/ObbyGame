using Unity.Collections;
using UnityEngine;
using UnityEngine.Splines;

public class MoveAlongSpline : MonoBehaviour
{
    [SerializeField] private SplineContainer spline;
    public float speed = 1f;

    private float distancePercentage = 0f;
    private float splineLength;
    private bool _isInEnd = false;

    private void Start()
    {
        splineLength = spline.CalculateLength();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!_isInEnd)
        {
            distancePercentage += speed * Time.deltaTime / splineLength;
            Debug.Log(distancePercentage);
            if (distancePercentage >= 0.99) { endAnim(); goto f1; }
            Vector3 currentPosition = spline.EvaluatePosition(distancePercentage);
            transform.position = currentPosition;

            if (distancePercentage > 1f)
            {
                distancePercentage = 0f;
            }

            Vector3 nextPosition = spline.EvaluatePosition(distancePercentage + 0.05f);
            Vector3 direction = nextPosition - currentPosition;
            transform.rotation = Quaternion.LookRotation(direction, transform.up);
            f1:;
        }
    }
    private void endAnim()
    {
        _isInEnd = true;
        transform.position = spline.EvaluatePosition(1);
    }
}