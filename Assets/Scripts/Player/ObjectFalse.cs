using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFalse : MonoBehaviour
{
    public int atk;

    public GameObject effect;

    float timer;

    public int destroyTime = 0;

    public Transform instantiatePos;
    // trueならPlayerの位置に、falseならこのオブジェクトの位置に配置する

    bool playEffect = false;

    public float destroyEffextTime = 5f;

    private void Update()
    {
        if(destroyTime > 0)
        {
            timer += Time.deltaTime;
            if (timer >= destroyTime) DestroySphere();
        }
    }

    public void DestroySphere()
    {
        Destroy(gameObject);
    }

    public void PlayingEffect(Transform parents)
    {
        if (playEffect) return;
        if (effect == null) return;

        if (instantiatePos == null) instantiatePos = transform;

        GameObject e = Instantiate(effect, instantiatePos.position, Quaternion.Euler(parents.localEulerAngles),parents);

        e.AddComponent<ObjectFalse>().Invoke("DestroySphere", destroyEffextTime);
        Debug.Log("エフェクトやで");

        playEffect = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyStatus>().GetDamage(atk);
                
            collision.gameObject.GetComponent<Rigidbody>().velocity = (collision.transform.up * atk) + (collision.transform.forward * atk * -1);
        }

        if (!playEffect && effect != null) PlayingEffect(transform);
        
        //DestroySphere();
    }
}
