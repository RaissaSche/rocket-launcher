using UnityEngine;

public class ReduceDump : MonoBehaviour {
    private void OnCollisionEnter(Collision other) {
        other.rigidbody.drag = 0;
    }
}