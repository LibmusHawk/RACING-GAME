using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player2Movement : MonoBehaviour {

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
    public Text player2Lap;

    private void Start() {
    // Get a reference to the attached RigidBody2D.
    shipRigidbody = GetComponent<Rigidbody2D>();
  }

    private void Update() {
      HandleShipAcceleration();
      HandleShipDecceleration();
      HandleShipRotation();
      boosttrigger();

      player2Lap.text ="P2 Laps:" + laps.ToString();
  }

    private void OnTriggerEnter2D (Collider2D collision) {
        if (collision.CompareTag("FinishLine") && checkChecked) {
        laps++;
      Boost = 1.1f;
      shipMaxVelocity = 8f;
        Debug.Log("Player2 Laps: " + laps);
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
    isAccelerating = Input.GetKey(KeyCode.UpArrow);
  }
    private void HandleShipDecceleration(){
    isDeccelerating =Input.GetKey(KeyCode.DownArrow);
    }
    private void boosttrigger () {
        if (Input.GetKeyDown(KeyCode.Mouse0)){
            isboosting = true;
        }
        if (Input.GetKeyUp(KeyCode.Mouse0)){
            isboosting = false;
        }

        if (isboosting){
            shipRigidbody.AddForce(transform.up * Boost);
        }
    }
    

  private void HandleShipRotation() {
    // Ship rotation.
    if (Input.GetKey(KeyCode.LeftArrow)) {
      transform.Rotate(shipRotationSpeed * Time.deltaTime * transform.forward);
    } else if (Input.GetKey(KeyCode.RightArrow)) {
      transform.Rotate(-shipRotationSpeed * Time.deltaTime * transform.forward);
    }
  }
    }
