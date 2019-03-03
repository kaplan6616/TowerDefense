using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nodeUIScript : MonoBehaviour
{
    private buildingOnNode target;
    public GameObject UI;
    public Vector3 offset;
    
    public void SetTarget(buildingOnNode _target)
    {
        target = _target;
        transform.position = target.transform.position + offset;
        UI.SetActive(true);
    }
    public void Hide()
    {
        UI.SetActive(false);
    }
}
