using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks; //引入命名空间
public class MySeek : Action
{
    public SharedFloat Speed = 0;  //按照Seek格式定义公开的变量，速度

    public SharedFloat ArriveDistance = 0.1f;  //距离，判定多少为到达目标  
    public SharedTransform tagert;  //目标位置
    private Animator anim;
    private Vector3 target;
    public GameObject[] ground;
    public AudioClip yelp;
    public override void OnStart()
    {
        transform.GetComponent<AudioSource>().clip = yelp;
        
}
    public override void OnAwake()
    {
        anim = transform.GetComponent<Animator>();
    }
    public override TaskStatus OnUpdate()
    {   
        for(int i=0;i<ground.Length;i++) //判断是否在边缘位置
        if((ground[i].transform.position-tagert.Value.position).magnitude<2)
        {
            return TaskStatus.Failure;
            anim.SetBool("iswalk", true);
            anim.SetBool("isseek", false);
            anim.SetBool("isAttack", false);
        }
       
        if (!transform.GetComponent<AudioSource>().isPlaying)
        {
            transform.GetComponent<AudioSource>().Play();   
        }
        anim.SetBool("iswalk", false);
        anim.SetBool("isseek", true);
        anim.SetBool("isAttack", false);
        target = new Vector3(tagert.Value.position.x,
            transform.position.y, tagert.Value.position.z);//让目标与怪物等高
        if (tagert == null || tagert.Value == null)  //如果目标为空
        {
            return TaskStatus.Failure;  //返回失败
            
        }
        transform.LookAt(target);  //看向目标
        //向目标移动
        transform.position = Vector3.MoveTowards(transform.position, target, Speed.Value * Time.deltaTime);
        
        //如果与目标距离的平方，小于设定值的距离
        if (Vector3.Distance(target, transform.position) < ArriveDistance.Value)
        {
            
            return TaskStatus.Success;
            //则到达目标，返回成功
        }
        return TaskStatus.Running;
        //否者反正运行，继续执行方法
    }
    
}