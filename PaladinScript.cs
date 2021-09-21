using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinScript : MonoBehaviour
{
    public static bool portal = false;
    private float dirX, dirZ;
    private float speed = 4;
    private float rotationSpeed = 80;
    private float rotate = 0f;
    private float gravity = 10f;
    public Transform paladin;
    public AudioClip att1Audio;
    public AudioClip att2Audio;
    public AudioClip att3Audio;
    public AudioClip runAudio;
    public AudioClip impactAudio;
    public AudioClip deathAudio;
    public AudioClip startAudio;
    AudioSource audio;

    Rigidbody rb;
    double checkTime = 0f, limit = 1f;
    bool isJumping = false;

    Vector3 palMove = Vector3.zero;

    //CharacterController charController;
    Animator anim;


    public GameObject wings;
    public static bool gotWing = false;
    private bool fire = false;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        //charController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        audio.PlayOneShot(startAudio);
        wings.SetActive(false);
    }

    private void Update()
    {
        GotWing();
        //Debug.Log(checkTime);
        if (portal)
        {
            anim.SetFloat("DirectionX", 0);
            anim.SetFloat("DirectionZ", 0);
        }
        GetInput();
        movement();
        GetJump();
        //Movement();
        if(anim.GetInteger("Condition") == 0 && dirX == 0f && dirZ == 0f)
        {
            checkTime += Time.deltaTime;
        }
        else
        {
            checkTime = 0;
        }

        if(checkTime >= 5f)
        {
            checkTime = 0;
            StartCoroutine(idleRoutine());
        }
    }

    public void GotWing()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (gotWing)
            {
                if (fire)
                {
                    wings.SetActive(false);
                    fire = false;
                    anim.SetFloat("Speed", 1.0f);
                }
                else
                {
                    wings.SetActive(true);
                    fire = true;
                    anim.SetFloat("Speed", 1.5f);
                }
            }

        }
    }

    private void movement()
    {
        //if (charController.isGrounded)
        //{
            if (anim.GetBool("Jumping") == true || anim.GetBool("Attacking") == true)
            {
                return;
            }
            if (Pause.stop == true || anim.GetBool("Impact") == true)
            {
                palMove = new Vector3(0, 0, 0);
                //charController.Move(palMove * Time.deltaTime);
                anim.SetFloat("DirectionX", 0);
                anim.SetFloat("DirectionZ", 0);
                return;
            }
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A))
            {
                anim.SetInteger("Condition", 0);
            }
            dirX = Input.GetAxis("Horizontal");
            dirZ = Input.GetAxis("Vertical");
            anim.SetFloat("DirectionX", dirX);
            anim.SetFloat("DirectionZ", dirZ);
        //}
        //    palMove.y -= gravity * Time.deltaTime;
        //    charController.Move(palMove * Time.deltaTime);
    }

    //void Movement()
    //{
    //    if (charController.isGrounded)
    //    {

    //        palMove.y -= gravity * Time.deltaTime;
    //        charController.Move(palMove * Time.deltaTime);
    //        if (Pause.stop == true || anim.GetBool("Impact") == true)
    //        {
    //            return;
    //        }
    //        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
    //        {
    //            if(anim.GetBool("Attacking") == true)
    //            {
    //                return;
    //            }else if(anim.GetBool("Attacking") == false)
    //            {
    //                Running();
    //                anim.SetBool("Running", true);
    //                palMove = new Vector3(0, 0, 1);
    //                palMove *= speed;
    //                palMove = transform.TransformDirection(palMove);
    //            }
    //        }
    //        else if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
    //        {
    //            if (anim.GetBool("Attacking") == true)
    //            {
    //                return;
    //            }
    //            else if (anim.GetBool("Attacking") == false)
    //            {
    //                RunningLeftOrBack();
    //                anim.SetBool("Running", true);
    //                palMove = new Vector3(-1, 0, 0);
    //                palMove *= speed;
    //                palMove = transform.TransformDirection(palMove);
    //            }
    //        }
    //        else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
    //        {
    //            if (anim.GetBool("Attacking") == true)
    //            {
    //                return;
    //            }
    //            else if (anim.GetBool("Attacking") == false)
    //            {
    //                RunningRightOrForward();
    //                anim.SetBool("Running", true);
    //                palMove = new Vector3(1, 0, 0);
    //                palMove *= speed;
    //                palMove = transform.TransformDirection(palMove);
    //            }
    //        }
    //        else if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
    //        {
    //            if (anim.GetBool("Attacking") == true)
    //            {
    //                return;
    //            }
    //            else if (anim.GetBool("Attacking") == false)
    //            {
    //                RunningLeftOrBack();
    //                anim.SetBool("Running", true);
    //                palMove = new Vector3(0, 0, -1);
    //                palMove *= speed;
    //                palMove = transform.TransformDirection(palMove);
    //            }
    //        }
    //        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
    //        {
    //            if (anim.GetBool("Attacking") == true)
    //            {
    //                return;
    //            }
    //            else if (anim.GetBool("Attacking") == false)
    //            {
    //                RunningRightOrForward();
    //                anim.SetBool("Running", true);
    //                palMove = new Vector3(-0.5f, 0, 0.5f);
    //                palMove *= speed;
    //                palMove = transform.TransformDirection(palMove);
    //            }
    //        }
    //        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
    //        {
    //            if (anim.GetBool("Attacking") == true)
    //            {
    //                return;
    //            }
    //            else if (anim.GetBool("Attacking") == false)
    //            {
    //                RunningLeftOrBack();
    //                anim.SetBool("Running", true);
    //                palMove = new Vector3(-0.5f, 0, -0.5f);
    //                palMove *= speed;
    //                palMove = transform.TransformDirection(palMove);
    //            }
    //        }
    //        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
    //        {
    //            if (anim.GetBool("Attacking") == true)
    //            {
    //                return;
    //            }
    //            else if (anim.GetBool("Attacking") == false)
    //            {
    //                RunningRightOrForward();
    //                anim.SetBool("Running", true);
    //                palMove = new Vector3(0.5f, 0, 0.5f);
    //                palMove *= speed;
    //                palMove = transform.TransformDirection(palMove);
    //            }
    //        }
    //        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
    //        {
    //            if (anim.GetBool("Attacking") == true)
    //            {
    //                return;
    //            }
    //            else if (anim.GetBool("Attacking") == false)
    //            {
    //                RunningLeftOrBack();
    //                anim.SetBool("Running", true);
    //                palMove = new Vector3(0.5f, 0, -0.5f);
    //                palMove *= speed;
    //                palMove = transform.TransformDirection(palMove);
    //            }
    //        }
    //        if (Input.GetKey(KeyCode.Space))
    //        {
    //            if(isJumping == false)
    //            {
    //                if (anim.GetBool("Attacking") == true)
    //                {
    //                    return;
    //                }
    //                else if (anim.GetBool("Attacking") == false)
    //                {
    //                    Jumping();
    //                    if (anim.GetBool("Running") == true)
    //                    {
    //                        palMove = new Vector3(0, 1, 0.5f);
    //                    }
    //                    else
    //                    {
    //                        palMove = new Vector3(0, 1, 0);
    //                    }
    //                    palMove *= speed;
    //                    palMove = transform.TransformDirection(palMove);
    //                }
    //            }
    //        }
    //        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || 
    //            Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.S))
    //        {
    //            Idle();
    //            anim.SetBool("Running", false);
    //            palMove = new Vector3(0, 0, 0);
    //        }
    //    }
    //    //rotate += Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
    //    //transform.eulerAngles = new Vector3(0, rotate, 0);

    //    palMove.y -= gravity * Time.deltaTime;
    //    charController.Move(palMove * Time.deltaTime); 
    //}

    //void GetJump()
    //{
    //    if (charController.isGrounded)
    //    {
    //        if (Input.GetKeyDown(KeyCode.Space))
    //        {
    //            if (anim.GetBool("Attacking") == true)
    //            {
    //                return;
    //            }
    //            else if (anim.GetBool("Attacking") == false)
    //            {
    //                Debug.Log("jump");
    //                Jumping();
    //            }
    //        }

    //        palMove.y -= gravity * Time.deltaTime;
    //        charController.Move(palMove * Time.deltaTime);
    //    }
    //}

    void GetInput()
    {
        if(Pause.stop == true || anim.GetBool("Impact") == true)
        {
            palMove = new Vector3(0, 0, 0);
            //charController.Move(palMove * Time.deltaTime);
            return;
        }
        //if (charController.isGrounded)
        //{
            if (Input.GetMouseButtonDown(0))
            {
                anim.SetBool("Attacking", true);
                Debug.Log(anim.GetBool("Attacking"));
                palMove = new Vector3(0, 0, 0);
                //charController.Move(palMove * Time.deltaTime);
                anim.SetBool("Running", false);
                Attack1();
            }
            if (Input.GetMouseButtonUp(0))
            {
                //if (anim.GetBool("Running") == false)
                //{
                //    //Idle();
                //}
                //else
                //{
                //    Running();
                //}
                anim.SetInteger("Condition", 0);
                anim.SetBool("Attacking", false);
            }
        //}
    }
    //IEnumerator AttackRoutine1()
    //{
    //    anim.SetBool("Attacking", true);
    //    anim.SetInteger("Condition", 2);
    //    palMove = new Vector3(0, 0, 0);
    //    charController.Move(palMove * Time.deltaTime);
    //    yield return new WaitForSeconds (1f);
    //    if (Input.GetKey(KeyCode.Mouse0))
    //    {
    //        anim.SetInteger("Condition", 6);
    //        palMove = new Vector3(0, 0, 0);
    //        charController.Move(palMove * Time.deltaTime);
    //        yield return new WaitForSeconds(1f);
    //        if (Input.GetKey(KeyCode.Mouse0))
    //        {
    //            anim.SetInteger("Condition", 7);
    //            palMove = new Vector3(0, 0, 0);
    //            charController.Move(palMove * Time.deltaTime);
    //            yield return new WaitForSeconds(1f);
    //        }
    //        else
    //        {
    //            Idle();
    //        }
    //    }
    //    else
    //    {
    //        Idle();
    //    }
    //    palMove = new Vector3(0, 0, 0);
    //    charController.Move(palMove * Time.deltaTime);
    //    anim.SetBool("Attacking", false);
    //}

    //IEnumerator AttackRoutine2()
    //{
    //    anim.SetBool("Attacking", true);
    //    anim.SetInteger("Condition", 6);
    //    //palMove = new Vector3(0, 0, 0);
    //    yield return new WaitForSeconds(1f);
    //    Idle();
    //    anim.SetBool("Attacking", false);
    //}

    void GetJump()
    {
        //if (charController.isGrounded)
        //{
            if (Pause.stop || portal)
            {
                return;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                palMove = new Vector3(0, 1, 0);
                palMove *= speed;
                palMove = transform.TransformDirection(palMove);
                StartCoroutine(JumpRoutine());
            }
        //}
    }

    IEnumerator JumpRoutine()
    {
        //anim.SetInteger("Condition", 5);
        //isJumping = true;
        anim.SetInteger("Condition", 5);
        anim.SetBool("Jumping", true);
        yield return new WaitForSeconds(0.55f);
        anim.SetInteger("Condition", 0);
        anim.SetBool("Jumping", false);
        palMove = new Vector3(0, 0, 0);
        //charController.Move(palMove * Time.deltaTime);
    }

    void Attack1()
    {
        anim.SetInteger("Condition", 2);
        palMove = new Vector3(0, 0, 0);
        //charController.Move(palMove * Time.deltaTime);
    }

    IEnumerator idleRoutine()
    {
        anim.SetInteger("Condition", 1);
        yield return new WaitForSeconds(7.7f);
        if(anim.GetInteger("Condition") == 1)
        {
            anim.SetInteger("Condition", 0);
        }
    }

    IEnumerator attackAdvance()
    {
        PaladinLifeManager.attackDmg *= 2;
        yield return new WaitForSeconds(1);
        PaladinLifeManager.attackDmg /= 2;
    }

    //audio
    public void Step()
    {
        audio.PlayOneShot(runAudio);
    }

    public void ImpactAudio()
    {
        audio.PlayOneShot(impactAudio);
    }

    public void DeathAudio()
    {
        audio.PlayOneShot(deathAudio);
    }

    public void Att1()
    {
        audio.PlayOneShot(att1Audio);
    }

    public void Att2()
    {
        audio.PlayOneShot(att2Audio);
    }

    public void Att3()
    {
        audio.PlayOneShot(att3Audio);
    }

    public void Att3Adv()
    {
        StartCoroutine(attackAdvance());
    }
}
