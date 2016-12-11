using UnityEngine;
using System.Collections;

public class Pizza : MonoBehaviour {

    public float turningSpeed;
    public float scaleTime;
    public AnimationCurve scaleCurve;

    Vector3 initialScale;
    
    void Start()
    {
        initialScale = transform.localScale;
        StartCoroutine(turn());
    }

    void OnEnable()
    {
        Score.onGetPoints += Score_onGetPoints;
    }

    void OnDisable()
    {
        Score.onGetPoints -= Score_onGetPoints;
    }

    private void Score_onGetPoints()
    {
        StartCoroutine(Scale());
    }

    IEnumerator turn() {
        while (true)
        {
            transform.rotation *= Quaternion.Euler(0, 0, 10f * turningSpeed * (Time.timeScale * Time.timeScale));
            yield return 0;
        }
    }

    IEnumerator Scale()
    {
        float elapsed = 0f;
        while (elapsed < scaleTime)
        {
            transform.localScale = Vector3.LerpUnclamped(Vector3.zero, initialScale, scaleCurve.Evaluate(elapsed / scaleTime));
            elapsed += Time.deltaTime;
            yield return 0;
        }
        transform.localScale = initialScale;
    }
}
