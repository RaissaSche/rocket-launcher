using System;
using UnityEngine;

public class Timer : MonoBehaviour {

    public static event Action TimerEnded;
    [SerializeField] private float timer = 5.0f;
    private bool _timeHasEnded;

    private void Start() {
        _timeHasEnded = false;
    }

    private void Update() {
        if (_timeHasEnded) {
            enabled = false;
            return;
        }

        if (timer > 0) {
            timer -= Time.deltaTime;
        }

        if (!(timer <= 0)) {
            return;
        }

        TimerEnded?.Invoke();
        _timeHasEnded = true;
    }
}