using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using UnityEngine.UI;

public class MyAttack : Action
{
    public SharedTransform player;
    private Animator anim;
    public GameObject[] ground;
    private Slider []slider;
    private Text text;
    public SharedGameObject canvas;
    public AudioClip playerdie;
    public override void OnAwake()
    {
        slider = player.Value.GetComponentsInChildren<Slider>();
        text = player.Value.GetComponentInChildren<Text>();
        anim = transform.GetComponent<Animator>();
    }
   
    public override TaskStatus OnUpdate()
    {
        for (int i = 0; i < ground.Length; i++) //判断是否在边缘位置
            if ((ground[i].transform.position - player.Value.position).magnitude < 2)
            {
                return TaskStatus.Failure;
                anim.SetBool("iswalk", true);
                anim.SetBool("isseek", false);
                anim.SetBool("isAttack", false);
            }

            anim.SetBool("isAttack", true);
            anim.SetBool("iswalk", false);
            anim.SetBool("isseek", false);
        if (slider[0].value <= 0)
        {
            player.Value.GetComponent<Animator>().SetBool("isdeath", true);
            player.Value.GetComponent<Animator>().SetBool("isrun", false);
            player.Value.GetComponent<Animator>().SetBool("iswalk", false);
            player.Value.GetComponent<Animator>().SetBool("isAttack", false);
            player.Value.GetComponent<Animator>().SetBool("isIdle", false);
            transform.GetComponent<AudioSource>().clip = playerdie;
            if (!transform.GetComponent<AudioSource>().isPlaying)
            {
                transform.GetComponent<AudioSource>().Play();
                StartCoroutine(WaitAndPrint(0.01F));

            }
            canvas.Value.SetActive(true);
            canvas.Value.transform.GetComponentInChildren<Text>().text = "YOU LOSE";
        }
        else
        {
            //扣减玩家hp值
            slider[0].value -= 0.6f;
            slider[1].value -= 0.6f;
           
        }
            return TaskStatus.Success;
       
    }
    IEnumerator WaitAndPrint(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Time.timeScale = 0;
    }

}
