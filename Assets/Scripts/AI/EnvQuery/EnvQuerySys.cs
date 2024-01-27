using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnvQuerySys : MonoBehaviour
{
    [SerializeField, Range(0.0f, 100.0f)] private float length = 50.0f;
    [SerializeField, Range(0, 20)] private int postCount = 5;

    [Header("Collection Of all Post")]
    [SerializeField] private List<Vector3> edgePosts = new List<Vector3>();
    [SerializeField] private List<Vector3> posts = new List<Vector3>();
    public bool reloadPosts = false;

    [Header("Collections of post Navigation")]
    [SerializeField] private Dictionary<Vector3, bool> navPost = new Dictionary<Vector3, bool>();
    [SerializeField, Range(0.0f, 5.0f)] private float samplePostRayLength = 2.0f;
    public bool updateNavPost = false;

    [Header("Debugger")]
    [SerializeField] private Color boundaryColour = Color.blue;
    [SerializeField] private Color edgePostColour = Color.yellow;
    [SerializeField] private Color postColour = Color.cyan;
    [SerializeField] private Color navPostColourWalkable = Color.cyan;
    [SerializeField] private Color navPostColourNotWalkable = Color.red;
    [SerializeField, Range(0.0f, 2.0f)] private float postSize = 0.5f;
    public bool drawEdgePost = false;
    public bool drawPost = false;
    public bool drawNavPostQuery = false;


    private void Start()
    {
        edgePosts.Clear();
        posts.Clear();
        navPost.Clear();

        reloadPosts = updateNavPost = true;
    }



    private void GeneratePoints()
    {
        edgePosts.Clear();
        posts.Clear();

        Vector3 initPos = transform.position - new Vector3(length * 0.5f, 0.0f, length * 0.5f);
        float lengthPerPost = length / postCount;

        for (int i = 0; i < postCount + 1; i++)
        {
            for (int j = 0; j < postCount + 1; j++)
            {
                Vector3 newEdge = initPos + new Vector3(i * lengthPerPost, 0.0f, j * lengthPerPost);
                edgePosts.Add(newEdge);
            }
        }

        for (float i = 0.5f; i < postCount; i++)
        {
            for (float j = 0.5f; j < postCount; j++)
            {
                Vector3 newPost = initPos + new Vector3(i * lengthPerPost, 0.0f, j * lengthPerPost);
                posts.Add(newPost);
            }
        }

        reloadPosts = false;
    }


    public Dictionary<Vector3, bool> HardQueryPosts()
    {
        if(navPost.Count > 0)
            return navPost;

        return navPost = IsPostNavAble(posts);
    }

    Dictionary<Vector3, bool> IsPostNavAble(List<Vector3> posts)
    {
        Dictionary<Vector3, bool> temp = new Dictionary<Vector3, bool>();

        bool valid;

        //print("Printing: " + posts.Count);

        if(posts.Count < 0)
            GeneratePoints();

        foreach (var post in posts)
        {

            //bool safePostRay = NavMesh.Raycast(node, target.position, out hit, NavMesh.AllAreas);

            NavMeshHit hit;
            valid = NavMesh.SamplePosition(post, out hit, samplePostRayLength, NavMesh.AllAreas);

            Debug.DrawRay(post, Vector3.down * samplePostRayLength, (valid) ? Color.red : Color.yellow, 5f);
            //print("Point " + post + " is " + valid);
            temp.Add(post, valid);
        }


        updateNavPost = false;
        return temp;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = boundaryColour;
        Gizmos.DrawWireCube(transform.position, new Vector3(length, 0.0f, length));


        if (reloadPosts)
            GeneratePoints();

        if(updateNavPost)
            navPost = IsPostNavAble(posts);

        Gizmos.color = edgePostColour;
        if (drawEdgePost)
            foreach (var e in edgePosts)
                Gizmos.DrawSphere(e, postSize);

        Gizmos.color = postColour;
        if (drawPost)
            foreach (var p in posts)
                Gizmos.DrawSphere(p, postSize);


        if (drawNavPostQuery)
        {
            foreach (var n in navPost)
            {
                Gizmos.color = (n.Value) ? navPostColourWalkable : navPostColourNotWalkable;
                Gizmos.DrawSphere(n.Key, postSize);
            }
        }

    }
}
