using UnityEngine;
using UnityEngine.UI;

public class Rocket : MonoBehaviour {

    public float thrust = 10f, drag = 10f, parachuteDrag = 15f, parachuteAngularDrag = 5f, audioVolume = 0.25f;
    public Text currentVelocity, currentPosition;
    public AudioSource audioSource;

    [SerializeField] private ParticleSystem smokeParticle, fireParticle;
    [SerializeField] private Rigidbody firstCompartment, secondCompartment, parachute;

    private bool _engineOn, _isParachuteOpen;

    private void Start() {
        parachute.drag = 0.5f;
        parachute.angularDrag = 0;
        _engineOn = true;
        _isParachuteOpen = false;

        audioSource.PlayOneShot(audioSource.clip, audioVolume);
        Timer.TimerEnded += SignalToStopApplyingForce;
    }

    private void FixedUpdate() {
        SimulateWind();

        if (_engineOn) {
            secondCompartment.AddForce(transform.up * thrust);
        }

        if (!(secondCompartment.velocity.y < 0)) return;
        if (_isParachuteOpen) return;

        //only opens if it's velocity is zero or below and if the parachute hasn't been opened yet
        OpenParachute();
    }

    private void Update() {
        currentVelocity.text = "Velocidade: " + firstCompartment.velocity;
        currentPosition.text = "Posição: " + firstCompartment.transform.position;
    }

    private void SignalToStopApplyingForce() {
        _engineOn = false;
        smokeParticle.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        fireParticle.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    }

    //wind-simulating extra lateral force
    private void SimulateWind() {
        Vector3 wind = new Vector3(1, 0, 0);
        firstCompartment.AddForce(wind);
        secondCompartment.AddForce(wind);
        parachute.AddForce(wind);
    }

    private void OpenParachute() {
        parachute.drag = parachuteDrag;
        parachute.angularDrag = parachuteAngularDrag;
        firstCompartment.drag = drag;

        Destroy(secondCompartment.GetComponent<FixedJoint>());
        parachute.GetComponent<MeshRenderer>().enabled = true;
        _isParachuteOpen = true;
    }

    public void Reset() {
        Timer.TimerEnded -= SignalToStopApplyingForce;
    }
}