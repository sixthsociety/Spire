using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _RenderTracer : MonoBehaviour {


    public LineRenderer tracer;
    public Transform barrel;
    public Gun _weaponScript;
    
    void Start () {
        _weaponScript = GetComponent<Gun>();
        tracer = GetComponent<LineRenderer>();
    }
	
	// Update is called once per frame
	void Update () {

        Ray ray = new Ray(barrel.position, barrel.forward);

        if (tracer)
        {
           // StartCoroutine("RenderTracer", ray.direction * _weaponScript.range);
        }
    }

    IEnumerator RenderTracer(Vector3 hitPoint)
    {
        tracer.enabled = true;
        tracer.SetPosition(0, barrel.position);
        tracer.SetPosition(1, barrel.position + hitPoint);
        yield return null;
        tracer.enabled = false;
    }

}
