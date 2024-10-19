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
        /*if (Input.GetKey(KeyCode.LeftShift))
        { 
            if (Input.GetKeyDown(KeyCode.F))
            {
                StartCoroutine(Front_Reverse_Move());
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                StartCoroutine(Front_Move());
            }
        }*/

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

    /*public IEnumerator Front_Reverse_Move()
    {

        Front_Parent = this.gameObject.transform;
        //FrontC = GameObject.Find("Colliders/Front_Collider");
        //Collider Front_Collision = FrontC.GetComponent<Collider>();

        //GetComponent<Collider_Front>().OnCollisionStay();

        Corner = GameObject.Find("Rubik's_Cube/Corner_1_owg");
        Corner.transform.SetParent(Front_Parent);

        Edge2 = GameObject.Find("Rubik's_Cube/Edge_2_gw");
        Edge2.transform.SetParent(Front_Parent);

        Corner3 = GameObject.Find("Rubik's_Cube/Corner_3_gwr");
        Corner3.transform.SetParent(Front_Parent);

        Edge4 = GameObject.Find("Rubik's_Cube/Edge_4_og");
        Edge4.transform.SetParent(Front_Parent);

        Centre5 = GameObject.Find("Rubik's_Cube/Centre_5_g");
        Centre5.transform.SetParent(Front_Parent);

        Edge6 = GameObject.Find("Rubik's_Cube/Edge_6_gr");
        Edge6.transform.SetParent(Front_Parent);

        Corner7 = GameObject.Find("Rubik's_Cube/Corner_7_yog");
        Corner7.transform.SetParent(Front_Parent);

        Edge8 = GameObject.Find("Rubik's_Cube/Edge_8_yg");
        Edge8.transform.SetParent(Front_Parent);

        Corner9 = GameObject.Find("Rubik's_Cube/Corner_9_ygr");
        Corner9.transform.SetParent(Front_Parent);

        for (int x = 0; x < 9; x++)
        {
            Front_Parent.transform.Rotate(Vector3.forward * Time.deltaTime, 10);
            yield return new WaitForEndOfFrame();
        }

        Cube_Parent = GameObject.Find("Rubik's_Cube").transform;

        Corner.transform.SetParent(Cube_Parent);
        Edge2.transform.SetParent(Cube_Parent);
        Corner3.transform.SetParent(Cube_Parent);
        Edge4.transform.SetParent(Cube_Parent);
        Centre5.transform.SetParent(Cube_Parent);
        Edge6.transform.SetParent(Cube_Parent);
        Corner7.transform.SetParent(Cube_Parent);
        Edge8.transform.SetParent(Cube_Parent);
        Corner9.transform.SetParent(Cube_Parent);

        yield return null;
    }*/
}