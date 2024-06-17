using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using UnityEngine;

public class PathMove : MonoBehaviour
{
    public float v;//km/h  1000/3600s=10/36  得到每秒的速度
    private List<Vector3> path = new List<Vector3>();


    public LineRenderer lineRenderer;
 

    private float totalLength;//路的总长度

    private float currentS;//当前已经走过的路程

    private int index = 0;

    private Vector3 dir;
    private Vector3 pos;
    private float s;

    void Start()
    {
        Once();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            v = 0;
        } //前

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            v = 3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            v = 6;

        }


        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            v = 9;

        }

      
    }

    private void FixedUpdate()
    {
        if (player==null) {
            return;
        }

        s += (v * 10 / 36) * 0.02f;// 

        if (currentS < totalLength)
        {
            for (int i = index; i < path.Count - 1; i++)
            {
                currentS += (path[i + 1] - path[i]).magnitude;//计算下一个点的路程
                if (currentS > s)
                {
                    index = i;
                    currentS -= (path[i + 1] - path[i]).magnitude;
                    dir = (path[i + 1] - path[i]).normalized;
                    pos = path[i] + dir * (s - currentS);
                    break;

                }
            }
            //player.transform.position = pos;

            player.transform.position = new Vector3(pos.x, pos.y + 0.1f, pos.z);

            //player.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir, transform.up), Time.deltaTime * 5);
            //player.transform.rotation = Quaternion.Lerp(player.transform.rotation, Quaternion.LookRotation(dir, player.transform.up), Time.deltaTime * 5);
            //player.transform.rotation = Quaternion.Euler(30, 0, 0);

            player.transform.rotation= Quaternion.Lerp(player.transform.rotation, Quaternion.LookRotation(dir, player.transform.up), Time.deltaTime * 0.2f);
        }
        else
        {
            Debug.LogError("抵达终点");

           
        }
    }

    private Transform player;
    public void SetPath(List<Vector3> pathPoint,Transform player) {
        path = pathPoint;
        this.player = player;
        Once();   //重置并进行总路长计算
    }

    private void Once() {
        currentS = 0;
        totalLength = 0;
        index = 0;
        s = 0;

       // lineRenderer.positionCount = path.Count;
       // lineRenderer.SetPositions(path.ToArray());
        for (int i = 1; i < path.Count; i++)
        {
            totalLength += (path[i] - path[i - 1]).magnitude;
        }
    }
    
}
