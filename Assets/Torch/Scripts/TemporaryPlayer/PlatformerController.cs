using UnityEngine;

[RequireComponent(typeof(PlatformerMotor))]
public class PlatformerController : MonoBehaviour
{
    //Komponenty
    PlatformerMotor _platformerMotor;
    void Awake()
    {
        _platformerMotor = GetComponent<PlatformerMotor>();
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
                _platformerMotor.velocity += jumpForce;
            }
            else
            {
                //Dodaj grawitację
                _platformerMotor.velocity += gravity;
            }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Lethal") Torch.GameController.Die();
    }
}
