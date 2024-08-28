using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObjectMover : MonoBehaviour
{
    private GameObject selectedObject;
    [SerializeField] private List<PositionManager> positionManagers;
    [SerializeField] private PositionManager currentPositionManager;
    [SerializeField] private bool objectSelected = false;
    [SerializeField] private bool objectIsUp = false;
    [SerializeField] List<CupCompare> cupComparer;
    private bool isAnimating = false;
    private bool isMoveActive = false;

    private void Update()
    {
        if (isMoveActive == true)
        {
            if (isAnimating) return;

            if (Input.GetMouseButtonDown(0))
            {
                HandleInput(Input.mousePosition);
            }

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    HandleInput(touch.position);
                }
            }
        }
    }

    public bool IsMoveActive(bool gameActiveStatus)
    {
        isMoveActive = gameActiveStatus;
        return isMoveActive;
    }

    private void HandleInput(Vector2 inputPosition)
    {
        if (!objectSelected)
        {
            SelectObject(inputPosition);
        }
        else if (selectedObject != null)
        {
            MoveObject(inputPosition);
        }
    }

    private void SelectObject(Vector2 inputPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(inputPosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent<PositionManager>(out PositionManager pos))
            {
                selectedObject = pos.GetComponentInChildren<Cup>().gameObject;
                currentPositionManager = selectedObject.GetComponentInParent<PositionManager>();
                isAnimating = true;
                selectedObject.transform.DOMoveY(selectedObject.transform.position.y + .5f, 0.5f).OnComplete(() =>
                {
                    objectIsUp = true;
                    selectedObject.GetComponent<Rigidbody>().isKinematic = true;
                    isAnimating = false;
                });
                currentPositionManager.SetIsFull(false);
                objectSelected = true;
            }
        }
    }

    private void MoveObject(Vector2 inputPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(inputPosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent<PositionManager>(out PositionManager targetPosition) && objectIsUp)
            {
                Collider selectedObjectCollider = selectedObject.GetComponent<Collider>();

                if (!targetPosition.IsFull)
                {
                    MoveToTargetPosition(targetPosition, selectedObjectCollider);
                }
                else
                {
                    SwapObjects(targetPosition, selectedObjectCollider);
                }

                currentPositionManager.SetIsFull(false);
            }
        }
    }

    private void MoveToTargetPosition(PositionManager targetPosition, Collider selectedObjectCollider)
    {
        selectedObjectCollider.enabled = false;
        isAnimating = true;
        selectedObject.transform.DOMove(targetPosition.transform.position, 0.5f).OnComplete(() =>
        {
            targetPosition.SetIsFull(true);
            selectedObjectCollider.enabled = true;
            selectedObject.transform.SetParent(targetPosition.transform);
            selectedObject.GetComponent<Rigidbody>().isKinematic = false;
            selectedObject = null;
            objectSelected = false;
            objectIsUp = false;
            isAnimating = false;
            CompareFunctionUpdate();
        });

    }

    private void SwapObjects(PositionManager targetPosition, Collider selectedObjectCollider)
    {
        GameObject otherObject = targetPosition.GetComponentInChildren<Cup>().gameObject;
        Collider otherObjectCollider = otherObject.GetComponent<Collider>();
        Vector3 originalPosition = selectedObject.transform.position;

        selectedObjectCollider.enabled = false;
        otherObjectCollider.enabled = false;
        isAnimating = true;

        otherObject.transform.DOMove(currentPositionManager.transform.position, 0.5f).OnComplete(() =>
        {
            currentPositionManager.SetIsFull(true);
            otherObjectCollider.enabled = true;
            otherObject.transform.SetParent(currentPositionManager.transform);
        });

        selectedObject.transform.DOMove(targetPosition.transform.position, 0.5f).OnComplete(() =>
        {
            targetPosition.SetIsFull(true);
            selectedObjectCollider.enabled = true;
            selectedObject.transform.SetParent(targetPosition.transform);
            selectedObject.GetComponent<Rigidbody>().isKinematic = false;
            selectedObject = null;
            objectSelected = false;
            objectIsUp = false;
            isAnimating = false;
            CompareFunctionUpdate();
        });
    }

    private void CompareFunctionUpdate()
    {
        for (int i = 0; i < cupComparer.Count; i++)
        {
            cupComparer[i].UpdateCupPairs();
        }
    }
}
