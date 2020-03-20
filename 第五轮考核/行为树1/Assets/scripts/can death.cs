using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.UI;
using BehaviorDesigner.Runtime;

public class candeath : Conditional
{   private Animator anim;
    private Slider slider;
    public  SharedGameObject canvas;
    public AudioClip monsterdie;
    public override void OnAwake()
    {
        anim = GetComponent<Animator>();
        slider = transform.GetComponentInChildren<Slider>();
    }
   
    public override TaskStatus OnUpdate()
    {
       if(slider.value<=0)
        {
            anim.SetBool("isdead", true);
            anim.SetBool("iswalk", false);
            anim.SetBool("isseek", false);
            anim.SetBool("isAttack", false);
            transform.GetComponent<AudioSource>().clip = monsterdie;
            if (!transform.GetComponent<AudioSource>().isPlaying&& transform.GetComponent<AudioSource>().enabled==true)
            {
                
                transform.GetComponent<AudioSource>().Play();
                StartCoroutine(WaitAndPrint(1F));
            }
            canvas.Value.SetActive(true);
            canvas.Value.transform.GetComponentInChildren<Text>().text = "YOU WIN";
            return TaskStatus.Running;
        }
        return TaskStatus.Failure;
    }
    IEnumerator WaitAndPrint(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        transform.GetComponent<AudioSource>().enabled = false;
        Time.timeScale = 0;
    }
}
