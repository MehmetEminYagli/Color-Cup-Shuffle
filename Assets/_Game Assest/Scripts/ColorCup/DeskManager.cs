using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class DeskManager : MonoBehaviour
{
    [SerializeField] private GameObject desk;

    private void Start()
    {
        TutorialRotateDesk();
    }

    public void TutorialRotateDesk()
    {
        desk.transform.DOLocalRotate(new Vector3(0, 180, 180), 0.00001f, RotateMode.FastBeyond360);
    }
    public void TutorialRotateDeskEnd()
    {
        desk.transform.DOLocalRotate(new Vector3(0, 0, 180), 0.00001f, RotateMode.FastBeyond360);
    }
    public void WinRotateDesk()
    {
        desk.transform.DOLocalRotate(new Vector3(0, 180, 180), 1f, RotateMode.FastBeyond360);
    }

    public void FailRotateDesk()
    {
        desk.transform.DOShakeRotation(1f, new Vector3(0, 15, 0), 6, 90, false);
    }
}
