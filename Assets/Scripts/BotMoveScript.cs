using UnityEngine;
using UnityEngine.Splines;

public class BotMoveScript : MonoBehaviour
{
    public bool _isStarted = false;
    [SerializeField] private SplineContainer spline;
    public float _maxSpeed = 1f;
    [SerializeField] private float _timeToMaxSpeed = 2f;

    private float splineLength;
    private float distancePercentage = 0f;
    private float speed = 0f;
    private float curTime = 0f;
    private bool _isInEnd = false;

    private void Start()
    {
        splineLength = spline.CalculateLength();
    }

    private void Update()
    {
        if (!_isInEnd && _isStarted)
        {
            curTime += Time.deltaTime;
            speed = _maxSpeed * Mathf.Clamp(curTime / _timeToMaxSpeed, 0f, 1f);
            distancePercentage += speed * Time.deltaTime / splineLength;
            Debug.Log(distancePercentage);
            if (distancePercentage >= 0.99) {goto f1; }
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

}
