using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObjectMover : MonoBehaviour
{
    private GameObject selectedObject;
    [SerializeField] private List<PositionManager> positionManagers; // Hedef pozisyonların listesi
    [SerializeField] private PositionManager currentPositionManager;
    [SerializeField] private bool objectSelected = false;
    [SerializeField] private bool objectIsUp = false;
    [SerializeField] List<CupCompare> cupComparer;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!objectSelected)
            {
                SelectObject();
            }
            else if (selectedObject != null)
            {
                MoveObject();
            }
        }

        /*mobile*/
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (!objectSelected)
                {
                    SelectObject(touch.position);
                }
                else if (selectedObject != null)
                {
                    MoveObject(touch.position);
                }
            }
        }
    }

    /*mobile*/


    private void SelectObject(Vector2 touchPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent<PositionManager>(out PositionManager pos))
            {
                selectedObject = pos.GetComponentInChildren<Cup>().gameObject;

                currentPositionManager = selectedObject.GetComponentInParent<PositionManager>();
                selectedObject.transform.DOMoveY(selectedObject.transform.position.y + .5f, 0.5f).OnComplete(() =>
                {
                    objectIsUp = true;
                    selectedObject.GetComponent<Rigidbody>().isKinematic = true;
                });
                currentPositionManager.SetIsFull(false);
                objectSelected = true; // Nesne seçildi
            }
        }
    }

    private void MoveObject(Vector2 touchPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent<PositionManager>(out PositionManager targetPosition) && objectIsUp)
            {
                Collider selectedObjectCollider = selectedObject.GetComponent<Collider>();

                if (!targetPosition.IsFull)
                {
                    // Hedef pozisyon boşsa, nesneyi bu konuma taşı
                    selectedObjectCollider.enabled = false;
                    selectedObject.transform.DOMove(targetPosition.transform.position, 0.5f).OnComplete(() =>
                    {
                        targetPosition.SetIsFull(true);
                        selectedObjectCollider.enabled = true;
                        selectedObject.transform.SetParent(targetPosition.transform); // Parent değişikliği
                        selectedObject.GetComponent<Rigidbody>().isKinematic = false;
                        selectedObject = null;
                        objectSelected = false;
                        objectIsUp = false;

                        CompareFunctionUpdate();
                    });
                }
                else
                {
                    // Hedef pozisyon doluysa, yer değiştirme işlemi
                    GameObject otherObject = targetPosition.GetComponentInChildren<Cup>().gameObject;
                    Collider otherObjectCollider = otherObject.GetComponent<Collider>();
                    Vector3 originalPosition = selectedObject.transform.position;

                    selectedObjectCollider.enabled = false;
                    otherObjectCollider.enabled = false;

                    // Diğer nesneyi mevcut pozisyona taşı
                    otherObject.transform.DOMove(currentPositionManager.transform.position, 0.5f).OnComplete(() =>
                    {
                        currentPositionManager.SetIsFull(true);
                        otherObjectCollider.enabled = true;
                        otherObject.transform.SetParent(currentPositionManager.transform); // Parent değişikliği
                    });

                    // Seçilen nesneyi hedef pozisyona taşı
                    selectedObject.transform.DOMove(targetPosition.transform.position, 0.5f).OnComplete(() =>
                    {
                        targetPosition.SetIsFull(true);
                        selectedObjectCollider.enabled = true;
                        selectedObject.transform.SetParent(targetPosition.transform); // Parent değişikliği
                        selectedObject.GetComponent<Rigidbody>().isKinematic = false;
                        selectedObject = null;
                        objectSelected = false;
                        objectIsUp = false;

                        CompareFunctionUpdate();
                    });
                }

                currentPositionManager.SetIsFull(false);
            }
        }
    }



    /*mobile*/

    private void SelectObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent<PositionManager>(out PositionManager pos))
            {
                selectedObject = pos.GetComponentInChildren<Cup>().gameObject;

                currentPositionManager = selectedObject.GetComponentInParent<PositionManager>();
                selectedObject.transform.DOMoveY(selectedObject.transform.position.y + .5f, 0.5f).OnComplete(() =>
                {
                    objectIsUp = true;
                    selectedObject.GetComponent<Rigidbody>().isKinematic = true;
                });
                currentPositionManager.SetIsFull(false);
                objectSelected = true; // Nesne seçildi
            }
        }
    }

    private void MoveObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent<PositionManager>(out PositionManager targetPosition) && objectIsUp)
            {
                Collider selectedObjectCollider = selectedObject.GetComponent<Collider>();

                if (!targetPosition.IsFull)
                {
                    // Hedef pozisyon boşsa, nesneyi bu konuma taşı
                    selectedObjectCollider.enabled = false;
                    selectedObject.transform.DOMove(targetPosition.transform.position, 0.5f).OnComplete(() =>
                    {
                        targetPosition.SetIsFull(true);
                        selectedObjectCollider.enabled = true;
                        selectedObject.transform.SetParent(targetPosition.transform); // Parent değişikliği
                        selectedObject.GetComponent<Rigidbody>().isKinematic = false;
                        selectedObject = null;
                        objectSelected = false;
                        objectIsUp = false;

                        CompareFunctionUpdate();

                    });
                }
                else
                {
                    // Hedef pozisyon doluysa, yer değiştirme işlemi
                    GameObject otherObject = targetPosition.GetComponentInChildren<Cup>().gameObject;
                    Collider otherObjectCollider = otherObject.GetComponent<Collider>();
                    Vector3 originalPosition = selectedObject.transform.position;

                    selectedObjectCollider.enabled = false;
                    otherObjectCollider.enabled = false;

                    // Diğer nesneyi mevcut pozisyona taşı
                    otherObject.transform.DOMove(currentPositionManager.transform.position, 0.5f).OnComplete(() =>
                    {
                        currentPositionManager.SetIsFull(true);
                        otherObjectCollider.enabled = true;
                        otherObject.transform.SetParent(currentPositionManager.transform); // Parent değişikliği
                    });

                    // Seçilen nesneyi hedef pozisyona taşı
                    selectedObject.transform.DOMove(targetPosition.transform.position, 0.5f).OnComplete(() =>
                    {
                        targetPosition.SetIsFull(true);
                        selectedObjectCollider.enabled = true;
                        selectedObject.transform.SetParent(targetPosition.transform); // Parent değişikliği
                        selectedObject.GetComponent<Rigidbody>().isKinematic = false;
                        selectedObject = null;
                        objectSelected = false; 
                        objectIsUp = false; 

                        CompareFunctionUpdate();
                    });
                }

                currentPositionManager.SetIsFull(false);
            }
        }
    }
    void CompareFunctionUpdate()
    {
        for (int i = 0; i < cupComparer.Count; i++)
        {

            cupComparer[i].UpdateCupPairs();
        }
    }
}
