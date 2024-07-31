using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public string surface = "";
    private Animator anim;
    private bool IsMoving = false, IsGrounded = false, IsFalling = false;
    public bool IsLanding = false;
    private GameObject running_grass, running_rock, landing_grass, landing_rock;
    AudioSource grass_fall, rock_fall;

    void Start()
    {
        anim = GameObject.Find("ty").GetComponent<Animator>();
        running_grass = MyFind(this.gameObject, "footsteps_running_grass");
        running_rock = MyFind(this.gameObject, "footsteps_running_rock");
        landing_grass = MyFind(this.gameObject, "footsteps_landing_grass");
        landing_rock = MyFind(this.gameObject, "footsteps_landing_rock");
        grass_fall = landing_grass.GetComponent<AudioSource>();
        rock_fall = landing_rock.GetComponent<AudioSource>();
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
        if (IsLanding)
        {
            if (surface == "Grass")
                grass_fall.Play();
            else
                rock_fall.Play();
            IsLanding = false;
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