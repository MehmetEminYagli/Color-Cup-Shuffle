using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixEffectController : MonoBehaviour
{
    public List<GameObject> effect;
    public float effectWaitTime;
    public GameObject lastEffect;
    public DeskManager desk;

    public void StartMixEffect()
    {
        StartCoroutine(ShuffleEffect());
    }
    private IEnumerator ShuffleEffect()
    {
        for (int i = 0; i < effect.Count; i++)
        {
            effect[i].SetActive(true);
        }

        yield return new WaitForSeconds(effectWaitTime);

        for (int i = 0; i < effect.Count; i++)
        {
            effect[i].SetActive(false);
        }
        
    }
    public void EndMixEffect()
    {
        StartCoroutine(ActivateEffects());
    }

    private IEnumerator ActivateEffects()
    {
        lastEffect.transform.localScale = new Vector3(2, 2, 2);
        lastEffect.SetActive(true);
        desk.TutorialRotateDeskEnd();
        yield return new WaitForSeconds(1);
        lastEffect.SetActive(false);

    }
}
