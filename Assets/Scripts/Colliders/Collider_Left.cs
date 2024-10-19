using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider_Left : MonoBehaviour
{
    public Transform Left_Parent;
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
        bool shuffle = random.compLeft;
        return shuffle;
    }

    public int CheckChildNum()
    {
        Transform Left_Parent = GameObject.Find("Turning_Parents/Left").transform;
        int children = Left_Parent.GetComponentInChildren<Transform>().childCount;
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
                if (Input.GetKeyDown(KeyCode.L))
                {
                    if (counter < 9 && run == true)
                    {
                        if (other.CompareTag("Player"))
                        {
                            Left_Parent = GameObject.Find("Turning_Parents/Left").transform;
                            other.gameObject.transform.SetParent(Left_Parent);
                        }
                    }
                    else if (counter >= 9 && run == true)
                    {
                        //print("Success");
                        other.enabled = false;
                        StartCoroutine(Left_Reverse_Move());
                        run = false;
                        other.enabled = true;
                    }
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.L) || CheckForShuffle() == true)
                {
                    if (counter < 9 && run == true)
                    {
                        if (other.CompareTag("Player"))
                        {
                            Left_Parent = GameObject.Find("Turning_Parents/Left").transform;
                            other.gameObject.transform.SetParent(Left_Parent);
                        }
                    }
                    else if (counter >= 9 && run == true)
                    {
                        //print("Success");
                        other.enabled = false;
                        StartCoroutine(Left_Move());
                        run = false;
                        other.enabled = true;
                        RandomScript random = GameObject.Find("Rubik's_Cube")
                        .GetComponent<RandomScript>();
                        random.compLeft = false;
                    }
                }
            }
    }

    public IEnumerator Left_Move()
    {
        for (int x = 0; x < 9; x++)
        {
            Left_Parent.Rotate(-10, 0, 0);
            yield return null;
        }
        MainCube = GameObject.Find("Rubik's_Cube").transform;
        for (int i = Left_Parent.childCount - 1; i > -1; i--)
        {
            Transform child = Left_Parent.GetChild(i);
            child.SetParent(MainCube);
        }
        run = true;
    }

    public IEnumerator Left_Reverse_Move()
    {
        for (int x = 0; x < 9; x++)
        {
            Left_Parent.Rotate(10, 0, 0);
            yield return null;
        }
        MainCube = GameObject.Find("Rubik's_Cube").transform;
        for (int i = Left_Parent.childCount - 1; i > -1; i--)
        {
            Transform child = Left_Parent.GetChild(i);
            child.SetParent(MainCube);
        }
        run = true;
    }
}
