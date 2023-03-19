using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GranadeThrow : MonoBehaviour
{
    public float throwForce = 40f;
    public GameObject grenadePrefab;
    public Transform grenadePoint;
    public float flightTime = 1f;

    public LineRenderer lineVisual;
    public int lineSegment;
    public Camera mainCamera;

    public AudioClip GranadeThrowClip;
    public AudioSource GranadeSound;
    public int grenadesNumber = 5;
    public Text GrenadeText;
    public GameObject granadeParent;
    public bool nades_enabled = false;
    public GamePauseScript GamePauseScript;
    // Start is called before the first frame update
    void Start()
    {
        AnimationCurve curve = new AnimationCurve();
        curve.AddKey(0, 0.4f);
        curve.AddKey(1, 0.4f);

        lineVisual.widthCurve = curve;
        lineVisual.positionCount = lineSegment;
        Material whiteDiffuseMat = new Material(Shader.Find("Unlit/Texture"));
        lineVisual.material = whiteDiffuseMat;
    }

    // Update is called once per frame
    void Update()
    {
       if (!Input.GetKey(KeyCode.G) && !GamePauseScript.ispaused)
        {
            lineVisual.enabled = false;
        }

        if (grenadesNumber > 0 && nades_enabled)
        {
            if (Input.GetKey(KeyCode.G) && !GamePauseScript.ispaused)
            {
                lineVisual.enabled = true;
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                Physics.Raycast(ray, out RaycastHit hit);

                Vector3 vo = CalculateVelocty(hit.point, grenadePoint.position, flightTime);
                Visualize(vo);
            }
            if (Input.GetKeyUp(KeyCode.G) && !GamePauseScript.ispaused)
            {
                ThrowGrenade();
                grenadesNumber--;
            }
            GrenadeText.text = grenadesNumber.ToString();
        }
        else
        {
            GrenadeText.text = "0";
            lineVisual.enabled = false;
        }
    }

    void ThrowGrenade()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hit);

        Vector3 vo = CalculateVelocty(hit.point, grenadePoint.position, flightTime);

        GameObject grenade = Instantiate(grenadePrefab, grenadePoint.position, grenadePoint.rotation);
        grenade.transform.SetParent(granadeParent.transform);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(vo * throwForce, ForceMode.VelocityChange);

        for (int i = 0; i < lineSegment; i++)
        {
            lineVisual.SetPosition(i, grenade.transform.position);
        }
        GranadeSound.PlayOneShot(GranadeThrowClip);
   }

    void Visualize(Vector3 vo)
    {
        for(int i = 0; i < lineSegment; i++)
        {
            Vector3 pos = CalculatePosInTime(vo, i / (float)lineSegment);
            lineVisual.SetPosition(i, pos);
                
        }
    }

    Vector3 CalculateVelocty(Vector3 target, Vector3 origin, float time)
    {
        Vector3 distance = target - origin;
        Vector3 distanceXz = distance;
        distanceXz.y = 0f;

        float sY = distance.y;
        float sXz = distanceXz.magnitude;

        float Vxz = sXz / time;
        float Vy = (sY / time) + (0.5f * Mathf.Abs(Physics.gravity.y) * time);

        Vector3 result = distanceXz.normalized;
        result *= Vxz;
        result.y = Vy;

        return result;
    }

    Vector3 CalculatePosInTime(Vector3 vo, float time)
    {
        Vector3 Vxz = vo;
        Vxz.y = 0f;

        Vector3 result = grenadePoint.position + vo * time;
        float sY = (-0.5f * Mathf.Abs(Physics.gravity.y) * (time*time)) +(vo.y * time) + grenadePoint.position.y;

        result.y = sY;

        return result;
    }
}
