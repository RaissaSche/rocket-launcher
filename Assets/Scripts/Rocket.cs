using UnityEngine;
using UnityEngine.UI;

public class Rocket : MonoBehaviour {
    public float thrust = 10f, drag = 10f, parachuteDrag = 15f, parachuteAngularDrag = 5f, audioVolume = 0.25f;
    public Text currentVelocity, currentPosition;
    public AudioSource audioSource;
    [SerializeField] private ParticleSystem smokeParticle, fireParticle;
    [SerializeField] private Rigidbody firstCompartment, secondCompartment, parachute;
    private bool _engineOn, _ballisticMovement, _isParachuteOpen;
    private Vector3 _forceTarget;


    private void Awake() {
        smokeParticle.Play();
        fireParticle.Play();
    }

    private void Start() {
        parachute.drag = 0.5f;
        parachute.angularDrag = 0;
        _engineOn = _ballisticMovement = true;
        _isParachuteOpen = false;

        audioSource.PlayOneShot(audioSource.clip, audioVolume);

        Timer.TimerEnded += SignalToStopApplyingForce;
        Timer.TimerHalf += AddBallisticMovement;
    }

    private void FixedUpdate() {
        SimulateWind();

        if (_engineOn) {
            secondCompartment.AddForce(transform.up * thrust);
            secondCompartment.AddTorque(new Vector3(0, 0, -1));
            _ballisticMovement = false;
        }

        if (!(secondCompartment.velocity.y < 0)) return;
        if (_isParachuteOpen) return;

        OpenParachute(); //only opens if it's velocity is zero or below and if the parachute hasn't been opened yet
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

    public void AddBallisticMovement() {
        //if (!_ballisticMovement) return;
         /*_forceTarget = Quaternion.Euler(0, 2, 0) * transform.up;
        _ballisticMovement = true;*/
        //Debug.Log("Add Torque");
    }

    public void Reset() {
        Timer.TimerEnded -= SignalToStopApplyingForce;
        //Timer.TimerHalf -= AddBallisticMovement;
    }
}