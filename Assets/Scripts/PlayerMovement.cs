using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public Animator animator;
    public GameObject fireEffect;
    public float runningSpeed = 20f;
    public float jumpPower = 4f;
    public float maxHorizontalSpeed = 20f;
    public float horizontalAccelration = 0.5f;
    public float jumpDuration = 0.6f;
    public float distanceToGround = 1f;

    private float horizontalSpeed;
    private float speed;
    private float jumpCounter;
    private float previousHorizontal;
    private bool isJumpIncreasing;

    void Start() {
        horizontalSpeed = 0.0f;
        previousHorizontal = 0.0f;
        isJumpIncreasing = false;
        speed = runningSpeed;
        jumpCounter = 0.0f;
    }

    void Update() {
        if (GameManager.instance.isDead) {
            return;
        }
        
        RaycastHit raycastHit;
        float jumpMovement = 0f;
        if (Physics.Raycast(transform.position, Vector3.down, out raycastHit, distanceToGround)) {
            if (jumpCounter <= 0f) {
                if (Input.GetKeyDown(KeyCode.Space)) {
                    jumpMovement = jumpPower;
                    isJumpIncreasing = true;
                    jumpCounter += isJumpIncreasing ? Time.deltaTime : -Time.deltaTime;
                }
            }
        } else {
            if (jumpCounter >= jumpDuration) {
                isJumpIncreasing = false;
            }            
        }

        if (jumpCounter > 0f) {
            jumpMovement = jumpPower;
            jumpCounter += isJumpIncreasing ? Time.deltaTime : -Time.deltaTime;
        }
        
        animator.SetFloat("jump", jumpCounter);

        float horMovement = (Input.GetKey("a") ? -1 : (Input.GetKey("d") ? 1 : 0));
        if (horMovement == 0) {
            horizontalSpeed = 0;
        } else {
            if ((horMovement > 0 && previousHorizontal < 0)
             || (horMovement < 0 && previousHorizontal > 0)) {
                horizontalSpeed = 0;
            }
            previousHorizontal = horMovement;

            if (horizontalSpeed < maxHorizontalSpeed) {
                horizontalSpeed += horizontalAccelration * Time.deltaTime;
            }
        }

        Vector3 direction = (new Vector3(0, 0, 1f) * speed) + 
            (new Vector3(horMovement, 0, 0) * horizontalSpeed) +
            new Vector3(0, jumpMovement, 0);
        transform.Translate(direction * Time.deltaTime, Space.World);
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
            // TODO: delay this
            GameManager.instance.loosedGame();
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

    public void increaseSpeed(float amount) {
        speed = runningSpeed + amount;
    }
}
