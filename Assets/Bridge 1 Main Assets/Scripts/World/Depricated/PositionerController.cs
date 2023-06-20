
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class PositionerController : MonoBehaviour
{
    [SerializeField] private LayerMask positioner;
    [SerializeField] private float speed = 5;

    bool activated;

    Camera cam;
    RaycastHit hit;
    Vector2 bounds1 = new Vector3(173, 1080);
    Vector2 bounds2 = new Vector3(1745, 135);

    private void Start()
    {
        cam = GetComponent<Camera>();
    }
    void Update()
    {
        Vector3 mPos = Input.mousePosition;
        if (mPos.x > bounds1.x && mPos.x < bounds2.x && mPos.y <= bounds1.y && mPos.y >= bounds2.y)
        {
            if(activated == false)
            if (Input.GetMouseButtonDown((int)MouseButton.LeftMouse) && Physics.Raycast(cam.ScreenPointToRay(mPos).origin,
                cam.ScreenPointToRay(Input.mousePosition).direction, out hit, 20.0f, positioner, QueryTriggerInteraction.Collide))
                    StartCoroutine(LerpPosition(hit.transform.position, speed));
        }  
    }

    IEnumerator LerpPosition(Vector3 pos, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.position;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, pos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.position = pos;
        activated = false;
    }
}



