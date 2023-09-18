using UnityEngine;
using UnityEngine.UI;

public class Rocket : MonoBehaviour {
    public float thrust = 10f, drag = 10f, parachuteDrag = 15f, parachuteAngularDrag = 5f;
    public Text currentVelocity, currentPosition;
    private Rigidbody _firstCompartment, _secondCompartment, _parachute;
    private bool _addForce, _isParachuteOpen;

    private void Start() {
        _firstCompartment = transform.Find("PrimeiroEstagio").GetComponent<Rigidbody>();
        _secondCompartment = transform.Find("Corpo_Nariz").GetComponent<Rigidbody>();
        _parachute = transform.Find("Paraquedas").GetComponent<Rigidbody>();
        
        _addForce = true;
        _isParachuteOpen = false;
        
        _parachute.drag = 0;
        _parachute.angularDrag = 0;
        
        Timer.TimerEnded += UpdateAddForceStatus;
    }

    private void FixedUpdate() {
        if (_addForce) {
            _secondCompartment.AddForce(transform.up * thrust); // TODO: inclination
        }

        if (_secondCompartment.velocity.y < 0) { //opens parachute
            if (_isParachuteOpen == false) {
                _parachute.drag = parachuteDrag;
                _parachute.angularDrag = parachuteAngularDrag;
                Destroy(_secondCompartment.GetComponent<FixedJoint>());
                _firstCompartment.drag = drag;
                _parachute.GetComponent<MeshRenderer>().enabled = true;
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
        _addForce = false;
        Debug.Log("Timer has ended, _addForceDirection == " + _addForce);
    }
}