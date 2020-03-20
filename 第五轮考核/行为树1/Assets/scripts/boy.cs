using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class boy : MonoBehaviour
{
    private Rigidbody rg;
    private Animator anim;
    public GameObject target;
    private Slider slider;
    private Text text;
    private MeshRenderer mesh;
    private SkinnedMeshRenderer[] skin;
    private Animator anim2;
    private AudioSource audio;
    public Text text1;
   
    public void Awake()
    {
        rg = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        slider = target.GetComponentInChildren<Slider>();
        text = target.GetComponentInChildren<Text>();
        mesh = target.GetComponentInChildren<MeshRenderer>();
        skin = target.GetComponentsInChildren<SkinnedMeshRenderer>();
        anim2 = target.GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * 0.1f);
            anim.SetBool("isrun", true);
            anim.SetBool("iswalk", false);
            anim.SetBool("isAttack",false);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * 0.1f);
           
        }
        if(Input.GetKeyDown(KeyCode.W))
        {
            anim.SetBool("isrun", false);
            anim.SetBool("iswalk", true);
            anim.SetBool("isAttack",false);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.Rotate(0,-45, 0);
            
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.Rotate(0, 45, 0);
            
        }
        if (Input.GetKeyUp(KeyCode.W))
        { 
            anim.SetBool("iswalk", false);
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("isAttack", true);
            anim.SetBool("isrun", false);
            anim.SetBool("iswalk", false);
            if ((transform.position - target.transform.position).magnitude < 1f)
            {
                if (slider.value < 50&&slider.value>20)
                {   //怪物血量大于20小于50则隐身
                    mesh.enabled = false;
                    skin[0].enabled = false;
                    skin[1].enabled = false;
                    slider.value -= 1;
                    if (slider.value == 48)
                    {
                        text1.enabled = true;
                        StartCoroutine(WaitAndPrint(3F));
                    }
                }
                else if((slider.value>0&&slider.value<=20)||(slider.value>=50))
                { //怪物血量在0-20或者>50时显形
                    mesh.enabled = true;
                    skin[0].enabled = true;
                    skin[1].enabled = true;
                    slider.value -= 1;
                    
                }
              
            }
            audio.Play();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            anim.SetBool("isAttack", false);

        }

    }
    void Rotate(float hor, float ver)
    {
        //获取方向        
        Vector3 dir = new Vector3(hor,0,ver);
        //将方向转换为四元数        
        Quaternion quaDir = Quaternion.LookRotation(dir, Vector3.up);
        //缓慢转动到目标点        
        transform.rotation = Quaternion.Lerp(transform.rotation, quaDir,0.5f);
    }
    IEnumerator WaitAndPrint(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        text1.enabled = false;
    }
}
