using System;
using UnityEngine;

public class SimplePatrol : MonoBehaviour
{
  public float speed = 5.0f;
  private bool movingForward = true;
  private float switchDirectionTime = 5.0f;
  private float timer = 0.0f; 
  
  void Update()
  {
    timer += Time.deltaTime;

    if (timer >= switchDirectionTime)
    {
      movingForward = !movingForward;
      timer = 0.0f;
    }

    if (movingForward)
    {
      transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    else
    {
      transform.Translate(Vector3.back * speed * Time.deltaTime);
    }
  }
}
