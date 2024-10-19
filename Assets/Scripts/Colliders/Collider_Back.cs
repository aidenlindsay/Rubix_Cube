 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider_Back : MonoBehaviour
{
    public Transform Back_Parent;
    public Transform MainCube;
    public Transform[] counter;
    public bool run = true;
    public int startingCount;
    [SerializeField]
    public float speed = 1f;

    public RandomScript randomScript;

    public bool CheckForUserBlock()
    {
        RandomScript random = GameObject.Find("Rubik's_Cube").GetComponent<RandomScript>();
        bool block = random.userBlock;
        return block;
    }

    public bool CheckForShuffle()
    {
        RandomScript random = GameObject.Find("Rubik's_Cube").GetComponent<RandomScript>();
        bool shuffle = random.compBack;
        return shuffle;
    }

    public int CheckChildNum()
    {
        Transform Back_Parent = GameObject.Find("Turning_Parents/Back").transform;
        int children = Back_Parent.GetComponentInChildren<Transform>().childCount;
        //print("Children = " + children);
        return children;
    }

    public void ChangeParent(Transform parent)
    {
        Transform oldParent = parent;
        MainCube = GameObject.Find("Rubik's_Cube").transform;
        for (int x = oldParent.childCount; x > 0; x--)
        {
            Transform child = oldParent.GetChild(x);
            child.SetParent(MainCube);
        }

    }

    public void OnTriggerStay(Collider other)
    {
        int counter = CheckChildNum();

            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (Input.GetKeyDown(KeyCode.B))
                {
                    if (counter < 9 && run == true)
                    {
                        if (other.CompareTag("Player"))
                        {
                            Back_Parent = GameObject.Find("Turning_Parents/Back").transform;
                            other.gameObject.transform.SetParent(Back_Parent);
                        }
                    }
                    else if (counter >= 9 && run == true)
                    {
                        //print("Success");
                        other.enabled = false;
                        StartCoroutine(Back_Reverse_Move());
                        run = false;
                        other.enabled = true;
                    }
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.B) || CheckForShuffle() == true)
                {
                    if (counter < 9 && run == true)
                    {
                        if (other.CompareTag("Player"))
                        {
                            Back_Parent = GameObject.Find("Turning_Parents/Back").transform;
                            other.gameObject.transform.SetParent(Back_Parent);
                        }
                    }
                    else if (counter >= 9 && run == true)
                    {
                        //print("Success");
                        other.enabled = false;
                        StartCoroutine(Back_Move());
                        run = false;
                        other.enabled = true;
                        RandomScript random = GameObject.Find("Rubik's_Cube").
                        GetComponent<RandomScript>();
                        random.compBack = false;
                    }
                }
            }
    }

    public IEnumerator Back_Move()
    {

        for (int x = 0; x < 9; x++)
        {
            Back_Parent.Rotate(0, 0, 10);
            yield return null;
        }
        MainCube = GameObject.Find("Rubik's_Cube").transform;
        for (int i = Back_Parent.childCount - 1; i > -1; i--)
        {
            Transform child = Back_Parent.GetChild(i);
            child.SetParent(MainCube);
        }
        run = true;
    }

    public IEnumerator Back_Reverse_Move()
    {

        for (int x = 0; x < 9; x++)
        {
            Back_Parent.Rotate(0, 0, -10);
            yield return null;
        }
        MainCube = GameObject.Find("Rubik's_Cube").transform;
        for (int i = Back_Parent.childCount - 1; i > -1; i--)
        {
            Transform child = Back_Parent.GetChild(i);
            child.SetParent(MainCube);
        }
        run = true;
    }
}
