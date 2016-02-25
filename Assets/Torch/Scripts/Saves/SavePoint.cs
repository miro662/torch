using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class SavePoint : MonoBehaviour {
    public Vector3 spawnOffset;
    bool wasActive = false;
    Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Deactivate()
    {
        _animator.SetBool("Lit", false);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player" && !wasActive)
        {
            //Activate save point
            Torch.GameController.RegisterSavePoint(this);
            _animator.SetBool("Lit", true);
            wasActive = true;
        }
    }
}
