using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRoute : MonoBehaviour
{
    public List<Transform> wps;
    public List<Transform> route;
    public int routeNumber = 0;
    public int targetWP = 0;
    public Rigidbody rb;
    public bool go = false;
    public float initialDelay;
    public bool stopped = false;

    public TrafficLightController trafficController;
    // Start is called before the first frame update
    void Start()
    {
        wps = new List<Transform>();
        rb = GetComponent<Rigidbody>();
        GameObject wp;

        wp = GameObject.Find("CWP1");
        wps.Add(wp.transform);
        wp = GameObject.Find("CWP2");
        wps.Add(wp.transform);
        wp = GameObject.Find("CWP3");
        wps.Add(wp.transform);
        wp = GameObject.Find("CWP4");
        wps.Add(wp.transform);
        wp = GameObject.Find("CWP5");
        wps.Add(wp.transform);

        trafficController = GameObject.Find("TrafficLights").GetComponent<TrafficLightController>();

        SetRoute();
        initialDelay = Random.Range(2.0f, 8.0f);
        transform.position = new Vector3(0.0f, -5.0f, 0.0f);


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(trafficController.state);
        if (!go)
        {
            initialDelay -= Time.deltaTime;
            if (initialDelay <= 0.0f)
            {
                go = true;
                SetRoute();
            }
            else return;
        }
        Vector3 displacement = route[targetWP].position - transform.position;
        displacement.y = 0;
        float dist = displacement.magnitude;

        if (!stopped)
        {


            if (dist < 0.1f)
            {
                targetWP++;
                if (targetWP >= route.Count)
                {
                    SetRoute();
                    return;
                }
            }
            //calculate velocity for this frame
            Vector3 velocity = displacement;
            velocity.Normalize();
            velocity *= 5f;
            //apply velocity
            Vector3 newPosition = transform.position;
            newPosition += velocity * Time.deltaTime;
            rb.MovePosition(newPosition);
            //align to velocity
            Vector3 desiredForward = Vector3.RotateTowards(transform.forward, velocity,
            10.0f * Time.deltaTime, 0f);
            Quaternion rotation = Quaternion.LookRotation(desiredForward);
            rb.MoveRotation(rotation);
        }

        if (targetWP == 2)
        {
            // if (t1green.active)
            // {
            //     stopped = false;
            // }
            // if (t1red.active)
            // {
            //     stopped = true;
            // }
            Debug.Log("Epic");
        }
    }


    void SetRoute()
    {
        //randomise the next route
        routeNumber = Random.Range(0, 12);
        //set the route waypoints
        // if (routeNumber == 0) route = new List<Transform> { wps[0], wps[4],
        // wps[5], wps[6] };
        // else if (routeNumber == 1) route = new List<Transform> { wps[0], wps[4],
        // wps[5], wps[7] };
        // else if (routeNumber == 2) route = new List<Transform> { wps[2], wps[1],
        // wps[4], wps[5], wps[6] };
        // else if (routeNumber == 3) route = new List<Transform> { wps[2], wps[1],
        // wps[4], wps[5], wps[7] };
        // else if (routeNumber == 4) route = new List<Transform> { wps[3], wps[4],
        // wps[5], wps[6] };
        // else if (routeNumber == 5) route = new List<Transform> { wps[3], wps[4],
        // wps[5], wps[7] };
        // else if (routeNumber == 6) route = new List<Transform> { wps[6], wps[5],
        // wps[4], wps[0] };
        // else if (routeNumber == 7) route = new List<Transform> { wps[6], wps[5],
        // wps[4], wps[3] };
        // else if (routeNumber == 8) route = new List<Transform> { wps[6], wps[5],
        // wps[4], wps[1], wps[2] };
        // else if (routeNumber == 9) route = new List<Transform> { wps[7], wps[5],
        // wps[4], wps[0] };
        // else if (routeNumber == 10) route = new List<Transform> { wps[7],
        // wps[5], wps[4], wps[3] };
        // else if (routeNumber == 11) route = new List<Transform> { wps[7],
        // wps[5], wps[4], wps[1], wps[2] };
        route = new List<Transform> { wps[0], wps[1], wps[2], wps[3], wps[4] };
        //initialise position and waypoint counter
        transform.position = new Vector3(route[0].position.x, 0.5f,
        route[0].position.z);
        targetWP = 1;

    }
}
