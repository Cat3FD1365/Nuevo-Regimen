using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    [SerializeField] CharacterController characterControler;

    float movementSpeed;
    [SerializeField] float staticMovementSpeed = 6f;
    Vector3 moveDirection;

    Animator anim;
    float animationVelocity = 0.0f;
    [SerializeField] float animationAcceleration = 1;

    AudioSource audioSource;
    [SerializeField]  AudioClip[] stepClips;

    [HideInInspector] public short movementSound = 0;
    [SerializeField] AudioClip[] whistleClips;

    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        movementSpeed = staticMovementSpeed;
    }

    void Update()
    {
        PlayerMovement();
        Gravity();
        PlayerAnimaton();
    }

    private void PlayerMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        moveDirection = transform.right * moveX + transform.forward * moveZ;

        characterControler.Move(moveDirection * movementSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl))
        {
            movementSpeed = staticMovementSpeed * 2;
            audioSource.volume = 0.9f;
            movementSound = 3;
            characterControler.height = 1.95f;
            characterControler.center = new Vector3(0, 1, 0);
        }
        else if (Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = staticMovementSpeed / 2;
            audioSource.volume = 0.4f;
            movementSound = 1;
            characterControler.height = 0.95f;
            characterControler.center = new Vector3(0, 0.5f, 0);
        }
        else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = staticMovementSpeed / 1.3f;
            audioSource.volume = 0.5f;
            movementSound = 2;
            characterControler.height = 0.95f;
            characterControler.center = new Vector3(0, 0.5f, 0);
        }
        else
        {
            movementSpeed = staticMovementSpeed;
            audioSource.volume = 0.6f;
            movementSound = 0;
            characterControler.height = 1.95f;
            characterControler.center = new Vector3(0, 1, 0);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            movementSound = 4;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Whistle_Sound();
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
        if (moveDirection == Vector3.zero && animationVelocity >= 0.0f && !Input.GetKey(KeyCode.LeftControl)
            || moveDirection != Vector3.zero && animationVelocity > 0.5f && !Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl)
            || moveDirection != Vector3.zero && animationVelocity > 0.5f && !Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.LeftControl)
            || moveDirection == Vector3.zero && animationVelocity >= 0.0f && Input.GetKey(KeyCode.LeftControl))
        {
            animationVelocity -= Time.deltaTime * animationAcceleration;
            anim.SetFloat("Speed", animationVelocity);
            if (animationVelocity <= 0.0f)
            {
                animationVelocity = 0.0f;
            }
        }
        else if (moveDirection != Vector3.zero && animationVelocity <= 0.5f && !Input.GetKey(KeyCode.LeftShift))
        {
            animationVelocity += Time.deltaTime * animationAcceleration;
            anim.SetFloat("Speed", animationVelocity);
            if (animationVelocity >= 0.5f)
            {
                animationVelocity = 0.5f;
            }
        }
        else if (moveDirection != Vector3.zero && animationVelocity <= 1.0f && Input.GetKey(KeyCode.LeftShift))
        {
            animationVelocity += Time.deltaTime * animationAcceleration;
            anim.SetFloat("Speed", animationVelocity);
            if (animationVelocity >= 1.0f)
            {
                animationVelocity = 1.0f;
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

    private void Whistle_Sound()
    {
        AudioClip clip = WhistleClip();
        audioSource.PlayOneShot(clip);
    }

    private AudioClip WhistleClip()
    {
        return whistleClips[UnityEngine.Random.Range(0, whistleClips.Length)];
    }
}
