using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius1;
    [Range(0, 360)]
    public float viewAngle1;

    public SphereCollider spColl;

    public float roundViewRadius1;
    float roundViewAngle1 = 360;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    //見えているターゲットを保存するリスト
    [HideInInspector]
    public List<Transform> newVisibleTargets = new List<Transform>();

    [HideInInspector]
    private List<Transform> oldVisibleTargets = new List<Transform>();

    //解像度
    public float meshResolution;
    public int edgeResolveIterations;
    public float edgeDstThreshold;

    public MeshFilter viewMeshFilter;
    Mesh viewMesh;

    public MeshFilter viewRoundMeshFilter;
    Mesh viewRoundMesh;

    void Start()
    {
        //Meshを用意
        viewMesh = new Mesh();
        //Meshを用意
        viewRoundMesh = new Mesh();

        //名前をつけて識別しやすくする
        //viewMesh.name = "View Mesh";

        //sharedMeshは設定しなくてもよい　設定すると同じMeshを持つもの同士でMeshを共有し、メモリデータを削減できる
        viewMeshFilter.sharedMesh = viewMesh;
        viewRoundMeshFilter.sharedMesh = viewRoundMesh;

        //MeshFilter filter = GetComponentInChildren<MeshFilter>();
        //filter.sharedMesh = viewMesh;

        //敵にOnTriggerExitを発火させ、meshRendererのオンオフをさせるためのCollider　結構力技なので心苦しい
        spColl.radius = viewRadius1;
        StartCoroutine("FindTargetsWithDelay", 0.2f);
    }

    //メインスレッドで作業しないことで他の処理がスムーズ
    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            //Debug.Log("は？oldCount : " + oldVisibleTargets.Count);
            //Debug.Log("は？？？" + string.Join(",", oldVisibleTargets));
            newVisibleTargets = FindVisibleTargets(viewAngle1, roundViewAngle1, viewRadius1, roundViewRadius1);
            //Debug.Log("更新後newCount : " + newVisibleTargets.Count);
            //Debug.Log("更新後oldCount : " + oldVisibleTargets.Count);
            //Debug.Log("更新しないでほしい" + string.Join(",", oldVisibleTargets));

            //newVisibleTargetを描画
            DisplayVisibleTargets(newVisibleTargets);

            UnDisplayInvisibleTargets(newVisibleTargets, oldVisibleTargets);

            oldVisibleTargets = newVisibleTargets;
            //Debug.Log("更新前newCount : " + newVisibleTargets.Count);
            //Debug.Log("更新前oldCount : " + oldVisibleTargets.Count);
            yield return new WaitForSeconds(delay);
        }
    }

    private void Update()
    {

    }

    void LateUpdate()
    {
        DrawFieldOfView(viewAngle1, viewRadius1, viewMesh);
        DrawFieldOfView(roundViewAngle1, roundViewRadius1, viewRoundMesh);
    }

    void DisplayVisibleTargets(List<Transform> newVisibleTargets)
    {
        foreach(Transform target in newVisibleTargets)
        {
            target.GetComponent<MeshRenderer>().enabled = true;
        }
    }

    void UnDisplayInvisibleTargets(List<Transform> newVisibleTargets, List<Transform> oldVisibleTargets)
    {
        foreach (Transform oldTarget in oldVisibleTargets)
        {
            bool isInclude = false;

            foreach (Transform newTarget in newVisibleTargets)
            {
                //newにoldが含まれていればok
                if (oldTarget == newTarget)
                {
                    isInclude = true;
                    break;
                }
            }

            //含まれていないならオフ
            if (isInclude == false)
            {
                oldTarget.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }

    List<Transform> FindVisibleTargets(float viewAngle, float roundViewAngle, float viewRadius, float roundViewRadius)
    {
        //newVisibleTargets.Clear();
        List<Transform> newVisibleTargets = new List<Transform>();

        //第一引数が中心座標、第二引数が球の半径、引数で指定した球が触れた敵を全て配列で返す
        //でかいほう
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        //ちいさいほう
        Collider[] targetsInRoundViewRadius = Physics.OverlapSphere(transform.position, roundViewRadius, targetMask);

        //でかいほう
        Calc(targetsInViewRadius, viewAngle);
        //ちいさいほう
        Calc(targetsInRoundViewRadius, roundViewAngle);

        return newVisibleTargets;
        
        void Calc(Collider[] targetsInRadiusArray, float angle)
        {
            for (int i = 0; i < targetsInRadiusArray.Length; i++)
            {
                //敵のtransform
                Transform target = targetsInRadiusArray[i].transform;
                //MeshRenderer enemyMeshRenderer = target.GetComponent<MeshRenderer>();

                //敵の方向のベクトル（正規化）
                Vector3 dirToTarget = (target.position - transform.position).normalized;

                //敵の方向がviewAngle内だったら
                if (Vector3.Angle(transform.forward, dirToTarget) < angle / 2)
                {
                    //敵のdistance
                    float dstToTarget = Vector3.Distance(transform.position, target.position);

                    //敵までのrayを飛ばして間に障害物がなければ
                    if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                    {
                        //targetはvisible
                        //enemyMeshRenderer.enabled = true;
                        newVisibleTargets.Add(target);
                    }
                }
            }
        }
    }

    void DrawFieldOfView(float viewAngle, float viewRadius, Mesh mesh)
    {
        //stepCount = 角度に解像度を掛けたもの
        int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);

        //stepCountを角度で割る = 1°をどれぐらいの密度で描画するか
        float stepAngleSize = viewAngle / stepCount;

        List<Vector3> viewPoints = new List<Vector3>();
        ViewCastInfo oldViewCast = new ViewCastInfo();
        //Debug.Log("OldViewCast = hit : " + oldViewCast.hit + ", point : " + oldViewCast.point + ", dst : " + oldViewCast.dst + ", angle : " + oldViewCast.angle);
        //Debug.Log(oldViewCast.dst);
        //度数の分だけ行われる
        for (int i = 0; i <= stepCount; i++)
        {
            float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;

            //角度に対してRayを飛ばし、障害物を考慮した各頂点の値を格納する
            ViewCastInfo newViewCast = ViewCast(angle, viewRadius);
            //Debug.Log("NewViewCast = hit : " + newViewCast.hit + ", point : " + newViewCast.point + ", dst : " + newViewCast.dst + ", angle : " + newViewCast.angle);

            if (i > 0)
            {
                //隣の度数線における距離から現在の度数線における距離を引いた値が閾値より大きい＝隣の度数線における距離よりも現在の距離が短い＝衝突
                bool edgeDstThresholdExceeded = Mathf.Abs(oldViewCast.dst - newViewCast.dst) > edgeDstThreshold;
                //Debug.Log(oldViewCast.dst);
                //Debug.Log(Mathf.Abs(oldViewCast.dst - newViewCast.dst) + " : " + oldViewCast.dst + " , " + newViewCast.dst);
                //Debug.Log(newViewCast.angle + " : " + edgeDstThresholdExceeded);

                //隣の度数におけるhitと現在のhitが異なる＝衝突の差
                //両方ぶつかっているが、その距離に差がある＝衝突の差
                if (oldViewCast.hit != newViewCast.hit || (oldViewCast.hit && newViewCast.hit && edgeDstThresholdExceeded))
                {
                    //Debug.Log("突破");
                    //Debug.Log(oldViewCast.hit + ", " + newViewCast.hit);
                    //Debug.Log("前者 : " + (oldViewCast.hit != newViewCast.hit));
                    //Debug.Log("後者 : " + ((oldViewCast.hit && newViewCast.hit && edgeDstThresholdExceeded)));

                    //中間を取得、補完
                    EdgeInfo edge = FindEdge(oldViewCast, newViewCast, viewRadius);
                    if (edge.pointA != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointA);
                    }
                    if (edge.pointB != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointB);
                    }
                }
            }

            viewPoints.Add(newViewCast.point);
            oldViewCast = newViewCast;
        }

        //Debug.Log(string.Join("," , viewPoints));

        //扇の先＋原点
        int vertexCount = viewPoints.Count + 1;
        //頂点メッシュ
        Vector3[] vertices = new Vector3[vertexCount];
        //triangles 配列はメッシュの三角形を定義します。三角形の数は (vertexCount - 2) で、各三角形は3つの頂点を持ちます。
        int[] triangles = new int[(vertexCount - 2) * 3];

        vertices[0] = Vector3.zero;
        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }


    EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast, float radius)
    {
        float minAngle = minViewCast.angle;
        float maxAngle = maxViewCast.angle;
        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoint = Vector3.zero;

        for (int i = 0; i < edgeResolveIterations; i++)
        {
            //前と後の角度の中間の角度取得
            float angle = (minAngle + maxAngle) / 2;

            //その角度でRayを飛ばして情報を取得
            ViewCastInfo newViewCast = ViewCast(angle, radius);

            //oldViewCastのAngleと中間角度におけるdstの差分を確認
            bool edgeDstThresholdExceeded = Mathf.Abs(minViewCast.dst - newViewCast.dst) > edgeDstThreshold;

            //差がない
            if (newViewCast.hit == minViewCast.hit && !edgeDstThresholdExceeded)
            {
                minAngle = angle;
                minPoint = newViewCast.point;
                //差がない場合は(newViewCast.point, Vector3.zero)
            }
            //差がある
            else
            {
                maxAngle = angle;
                maxPoint = newViewCast.point;
                //差がある場合は(Vector3.zero, newViewCast.point)
            }
        }

        return new EdgeInfo(minPoint, maxPoint);
    }

    //Rayを飛ばして、当たればその位置を、当たらなければ距離と半径に従って位置を返す
    ViewCastInfo ViewCast(float globalAngle, float viewRadius)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, dir, out hit, viewRadius, obstacleMask))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, globalAngle);
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float dst;
        public float angle;

        public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle)
        {
            hit = _hit;
            point = _point;
            dst = _dst;
            angle = _angle;
        }
    }

    public struct EdgeInfo
    {
        public Vector3 pointA;
        public Vector3 pointB;

        public EdgeInfo(Vector3 _pointA, Vector3 _pointB)
        {
            pointA = _pointA;
            pointB = _pointB;
        }
    }

}
