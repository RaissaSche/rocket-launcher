using UnityEngine;

public class Rocket : MonoBehaviour {
    public float thrust = 10f, decelerationMultiplier = 10f;
    private Rigidbody _rigidbody;
    private bool _addForceDirection;

    private void Start() {
        _rigidbody = GetComponent<Rigidbody>();
        _addForceDirection = true;
        Timer.TimerEnded += UpdateAddForceStatus;
    }

    private void FixedUpdate() {
        if (_rigidbody.velocity.y < 0) {
            _rigidbody.drag = 20;
           // _rigidbody.AddForce(-_rigidbody.velocity * decelerationMultiplier, ForceMode.Acceleration);
        }

        if (_addForceDirection) {
            _rigidbody.AddForce(transform.up * thrust);
        }
    }

    private void UpdateAddForceStatus() {
        _addForceDirection = false;
        Debug.Log("Timer has ended, _addForceDirection! " + _addForceDirection);
    }
}