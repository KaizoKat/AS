using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform posit;
    [SerializeField] private float lerpSpeed;

    void Update()
    {
        if(Vector3.Distance(transform.position,posit.position) <= 0.01f)
            transform.position = posit.position;
        else
            transform.position = Vector3.Lerp(transform.position, posit.position, Time.deltaTime * lerpSpeed);

        transform.eulerAngles = posit.eulerAngles;
    }
}
