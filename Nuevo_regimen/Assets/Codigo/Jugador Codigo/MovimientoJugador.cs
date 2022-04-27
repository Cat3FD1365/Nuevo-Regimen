using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    [SerializeField] CharacterController characterControler;

    float movementSpeed = 6f;
    Vector3 moveDirection;

    private Animator anim;
    [SerializeField] float velocity = 0.0f;
    [SerializeField] float acceleration;

    [SerializeField] Transform playerLastPosition;
    [SerializeField] Transform playerActualPosition;
    public bool playerOnSight;
    public Vector3 playerStaticPosition = new Vector3(0, 0, 0);
    float folloPlayerTimer = 1.0f;

    private AudioSource audioSource;
    [SerializeField] private AudioClip[] stepClips;
    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        PlayerMovement();
        Gravity();
        PlayerAnimaton();
        PlayerPositionReference();
    }

    private void PlayerPositionReference()
    {
        if (playerOnSight == true)
        {
            playerLastPosition.transform.position = new Vector3(playerActualPosition.position.x, 0, playerActualPosition.position.z);
            playerStaticPosition = playerLastPosition.position;
            folloPlayerTimer = 1.0f;
        }
        else if (playerOnSight == false)
        {
            folloPlayerTimer -= Time.deltaTime;
            if (folloPlayerTimer > 0.0f)
            {
                playerLastPosition.transform.position = new Vector3(playerActualPosition.position.x, 0, playerActualPosition.position.z);
                playerStaticPosition = playerLastPosition.position;
            }
            else if (folloPlayerTimer <= 0.0f)
                playerLastPosition.position = playerStaticPosition;
        }
    }

    private void PlayerMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        moveDirection = transform.right * moveX + transform.forward * moveZ;

        characterControler.Move(moveDirection * movementSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl))
        {
            movementSpeed = 12;
            audioSource.volume = 0.9f;
        }
        else if (Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = 3;
            audioSource.volume = 0.4f;
        }
        else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = 4.5f;
            audioSource.volume = 0.5f;
        }
        else
        {
            movementSpeed = 6f;
            audioSource.volume = 0.6f;
        }
    }

    private void Gravity()
    {
       Vector3 moveVector = Vector3.zero;

        if (characterControler.isGrounded == false)
            moveVector += Physics.gravity;

        characterControler.Move(moveVector * Time.deltaTime);
    }

    private void PlayerAnimaton()
    {
        if (moveDirection == Vector3.zero && velocity >= 0.0f && !Input.GetKey(KeyCode.LeftControl)
            || moveDirection != Vector3.zero && velocity > 0.5f && !Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl)
            || moveDirection != Vector3.zero && velocity > 0.5f && !Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.LeftControl)
            || moveDirection == Vector3.zero && velocity >= 0.0f && Input.GetKey(KeyCode.LeftControl))
        {
            velocity -= Time.deltaTime * acceleration;
            anim.SetFloat("Speed", velocity);
            if (velocity <= 0.0f)
            {
                velocity = 0.0f;
            }
        }
        else if (moveDirection != Vector3.zero && velocity <= 0.5f && !Input.GetKey(KeyCode.LeftShift))
        {
            velocity += Time.deltaTime * acceleration;
            anim.SetFloat("Speed", velocity);
            if (velocity >= 0.5f)
            {
                velocity = 0.5f;
            }
        }
        else if (moveDirection != Vector3.zero && velocity <= 1.0f && Input.GetKey(KeyCode.LeftShift))
        {
            velocity += Time.deltaTime * acceleration;
            anim.SetFloat("Speed", velocity);
            if (velocity >= 1.0f)
            {
                velocity = 1.0f;
            }
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            anim.SetBool("Crouch", true);
        }
        else if (!Input.GetKey(KeyCode.LeftControl))
        {
            anim.SetBool("Crouch", false);
        }
    }

    private void Step_Sound()
    {
        AudioClip clip = StepClip();
        audioSource.PlayOneShot(clip);
    }

    private AudioClip StepClip()
    {
        return stepClips[UnityEngine.Random.Range(0, stepClips.Length)];
    }
}
