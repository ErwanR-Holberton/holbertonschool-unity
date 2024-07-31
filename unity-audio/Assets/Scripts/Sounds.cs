using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public string surface = "";
    private Animator anim;
    private bool IsMoving = false, IsGrounded = false, IsFalling = false;
    private GameObject running_grass, running_rock, landing_grass, landing_rock;

    void Start()
    {
        anim = GameObject.Find("ty").GetComponent<Animator>();
        running_grass = MyFind(this.gameObject, "footsteps_running_grass");
        running_rock = MyFind(this.gameObject, "footsteps_running_rock");
        landing_grass = MyFind(this.gameObject, "footsteps_landing_grass");
        landing_rock = MyFind(this.gameObject, "footsteps_landing_rock");
    }

    void Update()
    {
        IsMoving = anim.GetBool("IsMoving");
        IsGrounded = anim.GetBool("IsGrounded");
        IsFalling = anim.GetBool("IsFalling");

        if (IsMoving && IsGrounded)
            if (surface == "Grass")
                running_grass.SetActive(true);
            else
                running_rock.SetActive(true);
        else
        {
            running_rock.SetActive(false);
            running_grass.SetActive(false);
        }

    }

    private GameObject MyFind(GameObject parent, string name)
{
    foreach (Transform child in parent.transform)
    {
        if (child.gameObject.name == name)
            return child.gameObject;
    }
    return null; // Return null if no matching child is found
}
}
