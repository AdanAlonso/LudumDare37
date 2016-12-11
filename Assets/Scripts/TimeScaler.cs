using UnityEngine;
using System.Collections;

public class TimeScaler : MonoBehaviour {

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
        Time.timeScale += 0.1f;
    }
}
