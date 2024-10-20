using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontScript : MonoBehaviour
{
    public Transform[] children;

    private void Start()
    {
        children = gameObject.GetComponentsInChildren<Transform>();
    }

    void Update()
    {

    }

    public IEnumerator FrontL_Move()
    {

        Transform Front_Parent = GameObject.Find("Turning_Parents/Front").transform;

        for (int x = 0; x < 1; x++)
        {
            Front_Parent.Rotate(Vector3.forward * Time.deltaTime, -90);
            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }
}