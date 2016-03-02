using UnityEngine;

[RequireComponent(typeof(PlatformerMotor))]
[RequireComponent(typeof(Animator))]
public class PlatformerController : MonoBehaviour
{
    Animator animator;

    //Komponenty
    PlatformerMotor _platformerMotor;
    void Awake()
    {
        _platformerMotor = GetComponent<PlatformerMotor>();
        animator = GetComponent<Animator>();
    }

    //Parametry
    public Vector2 gravity;
    public Vector2 jumpForce;
    public float horizontalVelocity;

    //Dane
    [HideInInspector]
    public float horizontalDirection;

    bool jump = false;
    public void Jump()
    {
        if (_platformerMotor.collisions.down) jump = true;
    }

    void FixedUpdate()
    {
            //Przemieść
            horizontalDirection = Mathf.Clamp(horizontalDirection, -1f, 1f);
            _platformerMotor.velocity.x = horizontalVelocity * horizontalDirection;

            //Skacz
            if (jump)
            {
                jump = false;
            animator.SetTrigger("Jump");
                _platformerMotor.velocity += jumpForce;
            }
            else
            {
                //Dodaj grawitację
                _platformerMotor.velocity += gravity;
            }
    }

    void Update()
    {
        animator.SetFloat("Horizontal Velocity", Mathf.Abs(_platformerMotor.velocity.x));
        animator.SetFloat("Vertical Velocity", _platformerMotor.velocity.y);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Lethal") Torch.GameController.Die();
    }
}
