using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class CameraControll : MonoBehaviour
{
    [SerializeField] float speed;

    int angle = 0;
    bool rotate = false;
    bool done = false;
    Vector3 prevPos;
    EventSystem eve;

    Vector3 lerpedP;

    private void Start()
    {
        eve = FindObjectOfType<EventSystem>();
    }

    private void Update()
    {
        eve.SetSelectedGameObject(null);

        if (rotate)
        {
            StartCoroutine(LerpRotation(Quaternion.Euler(new Vector3(transform.eulerAngles.x,angle,transform.eulerAngles.z)), speed));

            if (done)
            {
                float reangle = transform.eulerAngles.y;
                if (reangle < 0) reangle = -reangle;

                if ((reangle < 10 && reangle >= 350) || (reangle < 100 && reangle >= 80) ||
                    (reangle < 190 && reangle >= 170) || (reangle < 280 && reangle >= 260))
                    rotate = false;
            }
        }
    }

    public void RotateLeft()
    {
        if (rotate == false)
        {
            angle -= 90;
            rotate = true;
        }
    }

    public void RotateRight()
    {
        if (rotate == false)
        {
            angle += 90;
            rotate = true;
        }
    }

    IEnumerator LerpRotation(Quaternion rot, float duration)
    {
        float time = 0;
        Quaternion iVal = transform.rotation;
        while (time < duration)
        {
            transform.rotation = Quaternion.Lerp(iVal, rot, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.rotation = rot;
        done = true;
    }
}
