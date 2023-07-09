using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] Animator monsterAnimator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            monsterAnimator.enabled = true;
            monsterAnimator.SetBool("die", true);
            StartCoroutine(DelayDie());
        }

        
    }
    private IEnumerator DelayDie()
    {
        yield return new WaitForSeconds(monsterAnimator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }
}
