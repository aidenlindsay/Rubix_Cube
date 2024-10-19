using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider_Down : MonoBehaviour
{
    public Transform Down_Parent;
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
        bool shuffle = random.compDown;
        return shuffle;
    }

    public int CheckChildNum()
    {
        Transform Down_Parent = GameObject.Find("Turning_Parents/Down").transform;
        int children = Down_Parent.GetComponentInChildren<Transform>().childCount;
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
                if (Input.GetKeyDown(KeyCode.D))
                {
                    if (counter < 9 && run == true)
                    {
                        if (other.CompareTag("Player"))
                        {
                            Down_Parent = GameObject.Find("Turning_Parents/Down").transform;
                            other.gameObject.transform.SetParent(Down_Parent);
                        }
                    }
                    else if (counter >= 9 && run == true)
                    {
                        //print("Success");
                        other.enabled = false;
                        StartCoroutine(Down_Reverse_Move());
                        run = false;
                        other.enabled = true;
                    }
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.D) || CheckForShuffle() == true)
                {
                    if (counter < 9 && run == true)
                    {
                        if (other.CompareTag("Player"))
                        {
                            Down_Parent = GameObject.Find("Turning_Parents/Down").transform;
                            other.gameObject.transform.SetParent(Down_Parent);
                        }
                    }
                    else if (counter >= 9 && run == true)
                    {
                        //print("Success");
                        other.enabled = false;
                        StartCoroutine(Down_Move());
                        run = false;
                        other.enabled = true;
                        RandomScript random = GameObject.Find("Rubik's_Cube").GetComponent<RandomScript>();
                        random.compDown = false;
                    }
                }
            }
    }

    public IEnumerator Down_Move()
    {

        for (int x = 0; x < 9; x++)
        {
            Down_Parent.Rotate(0, -10, 0);
            yield return null;
        }
        MainCube = GameObject.Find("Rubik's_Cube").transform;
        for (int i = Down_Parent.childCount - 1; i > -1; i--)
        {
            Transform child = Down_Parent.GetChild(i);
            child.SetParent(MainCube);
        }
        run = true;

    }

    public IEnumerator Down_Reverse_Move()
    {

        for (int x = 0; x < 9; x++)
        {
            Down_Parent.Rotate(0, 10, 0);
            yield return null;
        }
        MainCube = GameObject.Find("Rubik's_Cube").transform;
        for (int i = Down_Parent.childCount - 1; i > -1; i--)
        {
            Transform child = Down_Parent.GetChild(i);
            child.SetParent(MainCube);
        }
        run = true;

    }
}
