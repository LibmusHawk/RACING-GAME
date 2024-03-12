using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player1Movement : MonoBehaviour {

  [SerializeField] private float shipAcceleration = 10f;
    [SerializeField] private float shipDecceleration = -2f;
  [SerializeField] private float shipMaxVelocity = 10f;
  [SerializeField] private float shipRotationSpeed = 180f;
  [SerializeField] private float Boost = 1.1f;
  private Rigidbody2D shipRigidbody;
   private bool isAccelerating = false;
   private bool isboosting = false;
    private bool isDeccelerating = false;
    private bool checkChecked = false;

    private float laps = 0f;
    public Text player1Lap;

    private void Start() {
    // Get a reference to the attached RigidBody2D.
    shipRigidbody = GetComponent<Rigidbody2D>();
  }

    private void Update() {
      HandleShipAcceleration();
      HandleShipDecceleration();
      HandleShipRotation();
      boosttrigger();

      player1Lap.text ="P1 Laps:" + laps.ToString();
  }
    private void OnTriggerEnter2D (Collider2D collision) {
        if (collision.CompareTag("FinishLine") && checkChecked) {
        laps++;
      Boost = 1.1f;
      shipMaxVelocity = 8f;
        Debug.Log("Player1 Laps: " + laps);
        checkChecked = false;
    } else if(collision.CompareTag("Checkpoint")) {
        checkChecked = true;
    }
    if (collision.CompareTag("Oil")){
      Boost = 1.0f;
      shipMaxVelocity = 4f;
      collision.gameObject.SetActive(false);
    }
    }

    private void FixedUpdate() {
    if (isAccelerating) {
      // Increase velocity upto a maximum.
      shipRigidbody.AddForce(shipAcceleration * transform.up);
      shipRigidbody.velocity = Vector2.ClampMagnitude(shipRigidbody.velocity, shipMaxVelocity);
    }
        if (isDeccelerating) {
      // Increase velocity upto a maximum.
      shipRigidbody.AddForce(shipDecceleration * transform.up);
      shipRigidbody.velocity = Vector2.ClampMagnitude(shipRigidbody.velocity, shipMaxVelocity);
    }
    
  }

    private void HandleShipAcceleration() {
    // Are we accelerating?
    isAccelerating = Input.GetKey(KeyCode.W);
  }
    private void HandleShipDecceleration(){
    isDeccelerating =Input.GetKey(KeyCode.S);
    }
    private void boosttrigger () {
        if (Input.GetKeyDown(KeyCode.Space)){
            isboosting = true;
        }
        if (Input.GetKeyUp(KeyCode.Space)){
            isboosting = false;
        }

        if (isboosting){
            shipRigidbody.AddForce(transform.up * Boost);
        }
    }
    

  private void HandleShipRotation() {
    // Ship rotation.
    if (Input.GetKey(KeyCode.A)) {
      transform.Rotate(shipRotationSpeed * Time.deltaTime * transform.forward);
    } else if (Input.GetKey(KeyCode.D)) {
      transform.Rotate(-shipRotationSpeed * Time.deltaTime * transform.forward);
    }
  }
    }
