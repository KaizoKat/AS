
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class PointerController : MonoBehaviour
{
    [SerializeField] private LayerMask pointer;
    [SerializeField] private float speed = 5;
    [SerializeField] private Button backButton;
    [SerializeField] private GameObject normalUI;
    [SerializeField] private GameObject compactPuzzleUI;
    [SerializeField] private GameObject backButtonUI;
    [SerializeField] private GameObject backButtonPuzzleUI;

    Camera cam;
    RaycastHit hit;
    Vector3 prevPos;
    Quaternion prevRot;
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
            if (Input.GetMouseButtonDown((int)MouseButton.LeftMouse) && Physics.Raycast(cam.ScreenPointToRay(mPos).origin,
                cam.ScreenPointToRay(Input.mousePosition).direction, out hit, 5.0f, pointer, QueryTriggerInteraction.Collide))
            {
                prevPos = transform.position;
                prevRot = transform.rotation;
                StartCoroutine(LerpPosition(hit.transform.position, speed));
                StartCoroutine(LerpRotation(hit.transform.rotation, speed));
                normalUI.SetActive(false);
                compactPuzzleUI.SetActive(true);
                backButtonUI.SetActive(false);
                backButtonPuzzleUI.SetActive(true);
            }
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
    }

    public void Retrun()
    {
        StartCoroutine(LerpPosition(prevPos, speed));
        StartCoroutine(LerpRotation(prevRot, speed));
        normalUI.SetActive(true);
        compactPuzzleUI.SetActive(false);
        backButtonUI.SetActive(true);
        backButtonPuzzleUI.SetActive(false);
    }
}



