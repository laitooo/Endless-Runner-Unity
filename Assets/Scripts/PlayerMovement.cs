using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    public Animator animator;
    public GameObject fireEffect;
    public float runningSpeed = 20f;
    public float jumpPower = 4f;
    public float minHorizontalSpeed = 5;
    public float maxHorizontalSpeed = 20f;
    public float horizontalAccelration = 0.5f;
    public float jumpDuration = 0.6f;
    public float distanceToGround = 1f;

    private Rigidbody rigidbody;
    public float horizontalSpeed;
    public float speed;
    private float jumpCounter;
    private float previousHorizontal;
    private bool isJumpIncreasing;

    void Start() {
        rigidbody = GetComponent<Rigidbody>();
        horizontalSpeed = minHorizontalSpeed;
        previousHorizontal = 0.0f;
        isJumpIncreasing = false;
        speed = runningSpeed;
        jumpCounter = 0.0f;
    }

    void Update() {
        if (GameManager.instance.isDead) {
            rigidbody.velocity = Vector3.zero;
            return;
        }
        
        RaycastHit raycastHit;
        if (Physics.Raycast(transform.position, Vector3.down, out raycastHit, distanceToGround)) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                rigidbody.AddForce(Vector3.up * jumpPower);
                animator.SetBool("isJumping", true);
            }    
        } else {
            if (animator.GetBool("isJumping")) {
                animator.SetBool("isJumping", false);
            }
        }

        float horMovement = ((Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow)) ? -1 : ((Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow)) ? 1 : 0));
        if (horMovement == 0) {
            horizontalSpeed = minHorizontalSpeed;
        } else {
            if ((horMovement > 0 && previousHorizontal < 0)
             || (horMovement < 0 && previousHorizontal > 0)) {
                horizontalSpeed = minHorizontalSpeed;
            }
            previousHorizontal = horMovement;

            if (horizontalSpeed < maxHorizontalSpeed) {
                horizontalSpeed += horizontalAccelration * Time.deltaTime;
            }
        }

        Vector3 direction = new Vector3(horMovement * horizontalSpeed * Time.deltaTime, rigidbody.velocity.y, speed * Time.deltaTime);
        rigidbody.velocity = direction;
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Rock")) {
            animator.SetBool("hasFallen", true);
            AudioManager.instance.play("Death");
            GameManager.instance.loosedGame();
            return;
        }

        if (collision.gameObject.CompareTag("Lava")) {
            GameManager.instance.isDead = true;
            GameObject gameObject = (GameObject) Instantiate(fireEffect, transform.position, Quaternion.identity);
            animator.SetBool("isBurning", true);
            AudioManager.instance.play("Burning");
            StartCoroutine(getBurned());
            return;
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Coin")) {
            Destroy(other.gameObject);
            CoinsManager.instance.collectCoin();
            return;
        }
    }

    IEnumerator getBurned() {
        yield return new WaitForSeconds(3);
        GameManager.instance.loosedGame();
    }

    public void increaseSpeed(float amount) {
        speed = runningSpeed + amount;
    }
}
