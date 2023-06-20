using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BackButtonController : MonoBehaviour
{
    [SerializeField] GameObject normalView;
    [SerializeField] GameObject inventoryView;
    [SerializeField] Transform graphic;

    bool active = false;
    bool done = true;
    public void Open_CloseInventory()
    {
        active = !active;

        if(active)
        {
            if (done == true)
            {
                done = false;
                StartCoroutine(LerpRotation(Quaternion.Euler(new Vector3(90,transform.eulerAngles.y,transform.eulerAngles.z)), 0.15f));
                normalView.SetActive(false);
                inventoryView.SetActive(true);
                graphic.transform.rotation = Quaternion.Euler(Vector3.forward);
            }
        }
        else
        {
            if (done == true)
            {
                done = false;
                StartCoroutine(LerpRotation(Quaternion.Euler(new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z)), 0.15f));
                normalView.SetActive(true);
                inventoryView.SetActive(false);
                graphic.transform.rotation = Quaternion.Euler(Vector3.forward * 180);
            }
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
