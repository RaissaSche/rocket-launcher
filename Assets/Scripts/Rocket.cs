using UnityEngine;
using UnityEngine.UI;

public class Rocket : MonoBehaviour {
    public float thrust = 10f, drag = 10f, parachuteDrag = 15f, parachuteAngularDrag = 5f, audioVolume = 0.25f;
    public Text currentVelocity, currentPosition;
    public AudioSource audioSource;
    [SerializeField] private ParticleSystem smokeParticle, fireParticle;
    [SerializeField] private Rigidbody firstCompartment, secondCompartment, parachute;
    private bool _addForce, _addTorque, _isParachuteOpen;

    private void Start() {
        parachute.drag = parachute.angularDrag = 0;
        _addForce = _addTorque = true;
        _isParachuteOpen = false;

        audioSource.PlayOneShot(audioSource.clip, audioVolume);

        Timer.TimerEnded += SignalToStopApplyingForce;
        Timer.TimerHalf += AddBallisticMovement;
    }

    private void FixedUpdate() {
        if (_addForce) {
            secondCompartment.AddForce(transform.up * thrust);
            secondCompartment.AddForce(transform.right); //wind-simulating extra lateral force
        }

        if (!(secondCompartment.velocity.y < 0)) return;
        if (_isParachuteOpen) return;

        OpenParachute(); //only opens if it's velocity is zero or below and if the parachute hasn't been opened yet
    }

    private void Update() {
        currentVelocity.text = "Velocidade: " + firstCompartment.velocity.magnitude;
        currentPosition.text = "Posição: " + firstCompartment.transform.position;
    }

    private void SignalToStopApplyingForce() {
        _addForce = false;
    }

    private void OpenParachute() {
        parachute.drag = parachuteDrag;
        parachute.angularDrag = parachuteAngularDrag;
        firstCompartment.drag = drag;

        Destroy(secondCompartment.GetComponent<FixedJoint>());
        smokeParticle.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        fireParticle.Stop(true, ParticleSystemStopBehavior.StopEmitting);

        parachute.GetComponent<MeshRenderer>().enabled = true;
        _isParachuteOpen = true;
    }

    private void AddBallisticMovement() {
        if (!_addTorque) return;
        secondCompartment.AddTorque(transform.up * 10000);
        _addTorque = false;
        Debug.Log("Add Torque");
    }
}