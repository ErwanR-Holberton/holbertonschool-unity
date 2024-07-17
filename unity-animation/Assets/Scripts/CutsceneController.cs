using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    private Animator animator;
    public GameObject mainCamera;
    public MonoBehaviour playerController;
    public GameObject timerCanvas;


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f && !animator.IsInTransition(0))
        {
            mainCamera.SetActive(true);
            timerCanvas.SetActive(true);
            playerController.enabled = true;
            gameObject.SetActive(false);
        }
    }
}
