using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider_Front : MonoBehaviour
{
    public static Transform[] children;
    public Transform Front_Parent2;
    public Transform MainCube;
    public Transform[] counter;
    public bool run = true;
    public int startingCount;
    [SerializeField]
    public float speed = 1f;

    public bool isMoving = false;

    public RandomScript randomScript;

    public bool CheckForUserBlock()
    {
        RandomScript random = GameObject.Find("Rubik's_Cube").GetComponent<RandomScript>();
        bool block = random.userBlock;
        return block;
    }

    public bool CheckForShuffle()
    {
        //Finds the shuffle boolean variable from the Rubik's Cube object, checks state
        RandomScript random = GameObject.Find("Rubik's_Cube").GetComponent<RandomScript>();
        bool shuffle = random.compFront;
        return shuffle;
    }

    public int CheckChildNum()
    {
        //Identifies the number of child objects inside the Front Turning Parent
        Transform Front_Parent2 = GameObject.Find("Turning_Parents/Front").transform;
        int children = Front_Parent2.GetComponentInChildren<Transform>().childCount;
        //print("Children = " + children);
        return children;
    }


    /*public void ChangeParent(Transform parent)
    {
        Transform oldParent = parent;
        MainCube = GameObject.Find("Rubik's_Cube").transform;
        for (int x = oldParent.childCount; x > 0; x--)
        {
            Transform child = oldParent.GetChild(x);
            child.SetParent(MainCube);
        }

    }*/

    public void OnTriggerStay(Collider other)
    {
        int counter = CheckChildNum();
      
            if (Input.GetKey(KeyCode.LeftShift))
            {
            //isMoving = true;
                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (counter < 9 && run == true)
                    {
                        if (other.CompareTag("Player"))
                        {
                            Front_Parent2 = GameObject.Find("Turning_Parents/Front").transform;
                            other.gameObject.transform.SetParent(Front_Parent2);
                        }
                    }
                    else if (counter >= 9 && run == true)
                    {
                        //print("Success");
                        other.enabled = false;
                        StartCoroutine(Front_Reverse_Move());
                        run = false;
                        other.enabled = true;
                    }
                }
            }
            else
            {
                if ((Input.GetKeyDown(KeyCode.F) || CheckForShuffle() == true))
                {
                //isMoving = true;
                    if (counter < 9 && run == true)
                    {
                        if (other.CompareTag("Player"))
                        {
                            Front_Parent2 = GameObject.Find("Turning_Parents/Front").transform;
                            other.gameObject.transform.SetParent(Front_Parent2);
                        }
                    }
                    else if (counter >= 9 && run == true)
                    {
                        //print("Success");
                        other.enabled = false;
                        StartCoroutine(Front_Move());
                        run = false;
                        other.enabled = true;
                        RandomScript random = GameObject.Find("Rubik's_Cube").GetComponent<RandomScript>();
                        random.compFront = false;
                    }
                }
            }
    }

    public IEnumerator Front_Move()
    {
        for (int x = 0; x < 9; x++)
        {
            Front_Parent2.Rotate(0, 0, -10);
            yield return null;
        }
        MainCube = GameObject.Find("Rubik's_Cube").transform;
        for (int i = Front_Parent2.childCount - 1; i > -1; i--)
        {
            Transform child = Front_Parent2.GetChild(i);
            child.SetParent(MainCube);
        }
        run = true;
        //isMoving = false;
    }

    public IEnumerator Front_Reverse_Move()
    {
        for (int x=0; x<9; x++)
        {
            Front_Parent2.Rotate(0, 0, 10);
            yield return null;
        }
        MainCube = GameObject.Find("Rubik's_Cube").transform;
        for (int i = Front_Parent2.childCount -1; i > -1; i--)
        {
            Transform child = Front_Parent2.GetChild(i);
            child.SetParent(MainCube);
        }
        run = true;
        //isMoving = false;
    }

}
