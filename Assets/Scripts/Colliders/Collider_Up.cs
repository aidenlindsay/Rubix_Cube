using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider_Up : MonoBehaviour
{
    public Transform Up_Parent;
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
        bool shuffle = random.compUp;
        return shuffle;
    }

    public int CheckChildNum()
    {
        Transform Up_Parent = GameObject.Find("Turning_Parents/Up").transform;
        int children = Up_Parent.GetComponentInChildren<Transform>().childCount;
        //print("Children = " + children);
        return children;
    }

    public bool CheckIfMoving()
    {
        Transform Front_Parent = GameObject.Find("Turning_Parents/Front").transform;
        bool ifMoving = Front_Parent.GetComponent<Collider_Front>().isMoving;
        return ifMoving;
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
                if (Input.GetKeyDown(KeyCode.U))
                {
                    if (counter < 9 && run == true)
                    {
                        if (other.CompareTag("Player"))
                        {
                            Up_Parent = GameObject.Find("Turning_Parents/Up").transform;
                            other.gameObject.transform.SetParent(Up_Parent);
                        }
                    }
                    else if (counter >= 9 && run == true)
                    {
                        other.enabled = false;
                        StartCoroutine(Up_Reverse_Move());
                        run = false;
                        other.enabled = true;
                    }
                }
            }
            else
            {
                if ((Input.GetKeyDown(KeyCode.U) || CheckForShuffle() == true))
                {
                    if (counter < 9 && run == true)
                    {
                        if (other.CompareTag("Player"))
                        {
                            Up_Parent = GameObject.Find("Turning_Parents/Up").transform;
                            other.gameObject.transform.SetParent(Up_Parent);
                        }
                    }
                    else if (counter >= 9 && run == true)
                    {
                        other.enabled = false;
                        StartCoroutine(Up_Move());
                        run = false;
                        other.enabled = true;
                        RandomScript random = GameObject.Find("Rubik's_Cube").
                        GetComponent<RandomScript>();
                        random.compUp = false;
                    }
                }
            }
    }

    public IEnumerator Up_Move()
    {
        for (int x = 0; x < 9; x++)
        {
            Up_Parent.Rotate(0, 10, 0);
            yield return null;
        }
        MainCube = GameObject.Find("Rubik's_Cube").transform;
        for (int i = Up_Parent.childCount - 1; i > -1; i--)
        {
            Transform child = Up_Parent.GetChild(i);
            child.SetParent(MainCube);
        }
        run = true;
    }

    public IEnumerator Up_Reverse_Move()
    {
        for (int x = 0; x < 9; x++)
        {
            Up_Parent.Rotate(0, -10, 0);
            yield return null;
        }
        MainCube = GameObject.Find("Rubik's_Cube").transform;
        for (int i = Up_Parent.childCount - 1; i > -1; i--)
        {
            Transform child = Up_Parent.GetChild(i);
            child.SetParent(MainCube);
        }
        run = true;
    }
}
