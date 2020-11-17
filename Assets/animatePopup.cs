using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animatePopup : MonoBehaviour
{
    public Animator foreignWordPopupAnimator;
    private float popupAnimationDelay = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playAnimation()
    {
        foreignWordPopupAnimator.SetTrigger("Play");
    }
}
