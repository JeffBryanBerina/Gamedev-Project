using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZoneScript : MonoBehaviour
{
    public List<Collider2D> detectedColliders = new List<Collider2D>();

    Collider2D colider;
    public void Awake()
    {
        colider = GetComponent<Collider2D>();
     
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        detectedColliders.Add(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        detectedColliders.Remove(collision);    
    }

}
