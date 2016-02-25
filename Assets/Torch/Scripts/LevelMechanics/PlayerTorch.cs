using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
public class PlayerTorch : MonoBehaviour
{
    //Is player torch currently lit?
    bool isLit;
    public bool IsLit
    {
        get { return isLit; }

        set
        {
            isLit = value;
            GetComponent<Animator>().SetBool("Lit", value);
        }
    }

    void Awake()
    {
        IsLit = false;
    }

    //Current torch
    TorchController torch;
    void OnTriggerEnter2D(Collider2D collider)
    {
        //Check if entered torch
        if (collider.tag == "Torch")
        {
            //Set current torch
            torch = collider.GetComponent<TorchController>();
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Torch")
        {
            torch = null;
        }
    }

    void Update()
    {
        if (torch != null)
        {
            if (Input.GetButtonDown("Lit"))
            {
                //If playertorch lit and torch unlit
                if (IsLit && !torch.Status)
                {
                    //Lit torch
                    torch.Status = true;
                    //Unlit playertorch
                    IsLit = false;
                }
                //If playertorch unlit and torch lit
                else if (!IsLit && torch.Status)
                {
                    //Lit torch
                    torch.Status = false;
                    //Unlit playertorch
                    IsLit = true;
                }
            }
        }
    }
}
