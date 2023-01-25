using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeEnemy : MonoBehaviour
{
    public MonoBehaviour monoBehaviourScript; 

    public void FreezeObject(float duration)
    {
        monoBehaviourScript?.StartCoroutine("Freeze", duration);
    }
}

//public IEnumerator Freeze(float duration)
//{
//    if (!isFreezed)
//    {
//        isFreezed = true;
//        anim.speed = 0;
//        yield return new WaitForSeconds(duration);
//        anim.speed = 1;
//        isFreezed = false;
//    }
//}
