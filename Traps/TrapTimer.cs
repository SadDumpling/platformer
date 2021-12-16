using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTimer : MonoBehaviour
{
    [SerializeField] private float startTimerTime = 0;
    [SerializeField] private float fireTime = 3;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(Fire());
    }
    IEnumerator Fire()
    {
        yield return new WaitForSeconds(startTimerTime);
        while (true)
        {
            anim.SetBool("fire", false);
            this.transform.GetChild(0).gameObject.SetActive(false);
            yield return new WaitForSeconds(fireTime);
            anim.SetBool("fire", true);
            this.transform.GetChild(0).gameObject.SetActive(true);
            yield return new WaitForSeconds(fireTime);
        }
    }
}
