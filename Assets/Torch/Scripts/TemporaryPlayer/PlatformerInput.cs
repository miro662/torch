using UnityEngine;

[RequireComponent(typeof(PlatformerController))]
public class PlatformerInput : MonoBehaviour
{
    Vector3 oldScale;
    //Komponenty
    PlatformerController _platformerController;
    void Awake()
    {
        _platformerController = GetComponent<PlatformerController>();
        oldScale = transform.localScale;
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump")) _platformerController.Jump();
        _platformerController.horizontalDirection = Input.GetAxisRaw("Horizontal");

        if (_platformerController.horizontalDirection > 0) transform.localScale = oldScale;
        if (_platformerController.horizontalDirection < 0) transform.localScale = new Vector3(-oldScale.x, oldScale.y, oldScale.z);
    }
}
