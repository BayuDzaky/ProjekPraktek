using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamraPivot : MonoBehaviour
{
    


    [Range(0.001f, 0.01f)]
    public float Amount = 0.002f;
    [Range(1f, 30f)]

    public float Frequency = 10.0f;

    [Range(10f, 100f)]
    public float Smooth = 10.0f;

    Vector3 StartPos;
    // Start is called before the first frame update
    void Start()
    {
        StartPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForHeadBob();
        StopHeadBob();
    }

    private void CheckForHeadBob()
    {
        float inputMagnitude = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).magnitude;
        if (inputMagnitude > 0)
        {
            StartHeadBob();
        }


    }

    private Vector3 StartHeadBob()
    {
        Vector3 Pos = Vector3.zero;
        Pos.y += Mathf.Lerp(Pos.y, Mathf.Sin(Time.time * Frequency) * Amount * 1.4f, Smooth * Time.deltaTime);
        Pos.x += Mathf.Lerp(Pos.x, Mathf.Cos(Time.time * Frequency / 2f) * Amount * 1.6f, Smooth * Time.deltaTime);
        transform.position += Pos;

        return Pos;
    }

    private void StopHeadBob()
    {
        if (transform.localPosition == StartPos) return;
        transform.localPosition = Vector3.Lerp(transform.localPosition, StartPos, 1 * Time.deltaTime);
    }
}
