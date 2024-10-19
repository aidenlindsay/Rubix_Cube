using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pieces : MonoBehaviour
    //perhaps use the centre piece as the access points and do something with the points around as the
    //centre doesnt move
{
    //public GameObject Cube_Parent = GameObject.Find("Rubik's_Cube");
    //private GameObject[] array_GameObjects;
    public Transform Front_Parent;

    private void Update()
    {
        /*if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                //print("Success3");
                //this.gameObject.transform.SetParent(Front_Parent);
                //StartCoroutine(Front.Front_Reverse_Move());
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                //print("Success3");
                //StartCoroutine(Front.Front_Move());
                //this.gameObject.transform.SetParent(Cube_Parent);
            }
        }*/
    }

    
    /*public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Front") && (hasCoroutineStarted == false))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    print("Success2");
                    //this.gameObject.transform.SetParent(Front_Parent);
                    //StartCoroutine(Front.Front_Reverse_Move());
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    Front_Parent = GameObject.Find("Turning_Parents/Front").transform;
                    this.gameObject.transform.SetParent(Front_Parent);
                    counter++;
                    print("Success3");
                    hasCoroutineStarted = true;
                    StartCoroutine(Front_Move());
                    //this.gameObject.transform.SetParent(Cube_Parent);
                    //other.enabled = false;

                }
            }
        }


    }*/

    public void Method()
    {
        
    }

    public IEnumerator Front_Move()
    {
        //hasCoroutineStarted = true;


        Transform Front_Parent = GameObject.Find("Turning_Parents/Front").transform;

        for (int x = 0; x < 10; x++)
        {
            Front_Parent.Rotate(Vector3.forward * Time.deltaTime, -9);
            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }



    public class Centre : Pieces
    {
        public int colour1;
        public string arrangement;

        public void colours(int c1, string s1)
        {
            colour1 = c1;
            arrangement = s1;
        }
    }

    public class Edge : Pieces
    {
        int colour1;
        int colour2;
        string arrangement;

        public void colours(int c1, int c2, string s1)
        {
            colour1 = c1;
            colour2 = c2;
            arrangement = s1;
        }
    }

    public class Corner : Pieces
    {
        int colour1;
        int colour2;
        int colour3;
        string arrangement;

        public void colours(int c1, int c2, int c3, string s1)
        {
            colour1 = c1;
            colour2 = c2;
            colour3 = c3;
            arrangement = s1;
        }
    }
}


//hasCoroutineStarted = true;
//print("Success");
//Front Front = gameObject.AddComponent<Front>();
//Transform Front_Parent = GameObject.Find("Turning_Parents/Front").transform;
//Transform Cube_Parent = GameObject.Find("Rubik's_Cube").transform;

//this.gameObject.transform.SetParent(Front_Parent);
//Transform Front_Parent = GameObject.Find("Turning_Parents/Front").transform;
//this.gameObject.transform.SetParent(Front_Parent);

