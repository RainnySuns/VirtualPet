using UnityEngine;
using Votanic.vXR.vGear;


public class ShootPainter : MonoBehaviour{

    public Camera cam;
    [Space]
    public bool mouseSingleClick;
    [Space]
    public Color paintColor;

    public float radius = 1;
    public float strength = 1;
    public float hardness = 1;

    private Vector3 position;
    private bool click;

    //public GameObject shootpoint;

    public void Start()
    {

    }

    void Update()
    {

        //click = mouseSingleClick ? vGear.Cmd.Received("shoot") : vGear.Cmd.Received("isshoot");
        click = mouseSingleClick ? Input.GetMouseButtonDown(0) : Input.GetMouseButton(0);

        if (click)
        {
            position = Input.mousePosition;
            //position = shootpoint.transform.position;
            Ray ray = cam.ScreenPointToRay(position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                Debug.DrawRay(ray.origin, hit.point - ray.origin, Color.red);
                Paintable p = hit.collider.GetComponent<Paintable>();
                if (p != null)
                {
                    Debug.Log("b" + hit.point);
                    PaintManager.instance.paint(p, hit.point, radius, hardness, strength, paintColor);
                }                
            }

        }
    }

}

