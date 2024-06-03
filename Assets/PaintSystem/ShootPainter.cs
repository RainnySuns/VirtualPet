using UnityEngine;
using Votanic.vXR.vGear;


public class ShootPainter : MonoBehaviour{

    private Camera cam;
    [Space]
    public bool mouseSingleClick;
    [Space]
    public Color paintColor;

    public float radius = 1;
    public float strength = 1;
    public float hardness = 1;

    private Vector3 position;
    private bool click;
    LayerMask groundLayer = 1 << 6;

    //public GameObject shootpoint;

    public void Start()
    {
        cam = GameObject.Find("vGear/Frame/User/Head/MainCamera").GetComponentInChildren<Camera>();
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
            Debug.Log("a");

            if (Physics.Raycast(ray, out hit, 100.0f, groundLayer))
            {                
                Debug.DrawRay(ray.origin, hit.point - ray.origin, Color.red);
                Paintable p = hit.collider.GetComponent<Paintable>();
                string c = hit.collider.name;
                Debug.Log(c);
                if (p != null)
                {
                    Debug.Log("b" + hit.point);
                    PaintManager.instance.paint(p, hit.point, radius, hardness, strength, paintColor);
                }
            }

        }
    }

}

