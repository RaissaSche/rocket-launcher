using System;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public static event Action TimerEnded;
    public Text currentTime;
    [SerializeField] private float timer = 5.0f;
    private bool _timeHasEnded;

    private void Start() {
        _timeHasEnded = false;
    }

    private void Update() {
        if (_timeHasEnded) {
            currentTime.text = "Timer: " + 0;
            enabled = false;
            return;
        }

        if (timer > 0) {
            timer -= Time.deltaTime;
            currentTime.text = "Timer: " + timer;
        }

        if (!(timer <= 0)) {
            return;
        }

        TimerEnded?.Invoke();
        _timeHasEnded = true;
    }
}