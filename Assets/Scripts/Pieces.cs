using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pieces : MonoBehaviour{
    public Transform Front_Parent;

    public IEnumerator Front_Move()
    {
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

