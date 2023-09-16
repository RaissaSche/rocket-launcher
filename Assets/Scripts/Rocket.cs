using UnityEngine;
using UnityEngine.UI;

public class Rocket : MonoBehaviour {
    public float thrust = 10f, drag = 10f;
    public Text currentVelocity, currentPosition;
    private Rigidbody _firstCompartment, _secondCompartment;
    private MeshRenderer _parachute;
    private bool _addForceDirection, _isParachuteOpen;

    private void Start() {
        _firstCompartment = transform.Find("PrimeiroEstagio").GetComponent<Rigidbody>();
        _secondCompartment = transform.Find("Corpo_Nariz").GetComponent<Rigidbody>();
        _parachute = transform.Find("Paraquedas").GetComponent<MeshRenderer>();
        _addForceDirection = true;
        _isParachuteOpen = false;
        Timer.TimerEnded += UpdateAddForceStatus;

        Debug.Log(_secondCompartment);
    }

    private void FixedUpdate() {
        if (_addForceDirection) {
            _secondCompartment.AddForce(transform.up * thrust); // TODO: inclination
        }

        if (_secondCompartment.velocity.y < 0) {
            //opens parachute
            if (_isParachuteOpen == false) {
                Destroy(_secondCompartment.GetComponent<FixedJoint>());
                _firstCompartment.drag = drag;
                _parachute.enabled = true;
                Debug.Log("Open Parachute");
                _isParachuteOpen = true;
            }
        }
    }

    private void Update() {
        currentVelocity.text = "Velocidade: " + _firstCompartment.velocity.magnitude;
        currentPosition.text = "Posição: " + _firstCompartment.transform.position;
    }

    private void UpdateAddForceStatus() {
        _addForceDirection = false;
        Debug.Log("Timer has ended, _addForceDirection == " + _addForceDirection);
    }
}