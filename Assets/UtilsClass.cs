using System.Net.NetworkInformation;
using UnityEngine;

public class UtilsClass : MonoBehaviour
{
    // Global variables
    public static Mesh mesh;
    public static Vector3 mouseWorldPosition;
    public static Vector3 lastMousePosition;
    
    // Get Mouse Position in World with Z = 0f
    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0;
        return vec;
    }

    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        return worldCamera.ScreenToWorldPoint(screenPosition);
    }

    public static Mesh drawDot()
    {
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[4];
        Vector2[] uv = new Vector2[4];
        int[] triangles = new int[6];

        vertices[0] = new Vector3(0,0,0);
        vertices[1] = new Vector3(0,1000,0);
        vertices[2] = new Vector3(1000,1000,0);
        vertices[3] = new Vector3(1000,0,0);

        uv[0] = Vector2.zero;
        uv[1] = Vector2.zero;
        uv[2] = Vector2.zero;
        uv[3] = Vector2.zero;

        // make common method for this
        triangles[0] = 0;
        triangles[1] = 3;
        triangles[2] = 1;
        triangles[3] = 1;
        triangles[4] = 3;
        triangles[5] = 2;

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.MarkDynamic();

        return mesh;
    }
    
    public static Mesh MousePressedCodeMonkey()
    {
        // print("mouse pressed");
        mouseWorldPosition = GetMouseWorldPosition();
        mesh = new Mesh();

        Vector3[] vertices = new Vector3[4];
        Vector2[] uv = new Vector2[4];
        int[] triangles = new int[6];

        vertices[0] = mouseWorldPosition;
        vertices[1] = mouseWorldPosition;
        vertices[2] = mouseWorldPosition;
        vertices[3] = mouseWorldPosition;

        uv[0] = Vector2.zero;
        uv[1] = Vector2.zero;
        uv[2] = Vector2.zero;
        uv[3] = Vector2.zero;

        // make common method for this
        triangles[0] = 0;
        triangles[1] = 3;
        triangles[2] = 1;

        triangles[3] = 1;
        triangles[4] = 3;
        triangles[5] = 2;

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.MarkDynamic();

        // GetComponent<MeshFilter>().mesh = mesh;

        lastMousePosition = mouseWorldPosition;
        return mesh;
    }

    public static Mesh MouseDraggedCodeMonkey() {
        mouseWorldPosition = GetMouseWorldPosition(); // need to re-get to draw.
        float minDistance = .1f;

        if (Vector3.Distance(mouseWorldPosition, lastMousePosition) > minDistance)
        {
            Vector3[] vertices = new Vector3[mesh.vertices.Length + 2];
            Vector2[] uv = new Vector2[mesh.uv.Length + 2];
            int[] triangles = new int[mesh.triangles.Length + 6];

            mesh.vertices.CopyTo(vertices, 0);
            mesh.uv.CopyTo(uv, 0);
            mesh.triangles.CopyTo(triangles, 0);

            int vIndex = vertices.Length - 4;
            int vIndex0 = vIndex + 0;
            int vIndex1 = vIndex + 1;
            int vIndex2 = vIndex + 2;
            int vIndex3 = vIndex + 3;

            Vector3 mouseForwardVector = (mouseWorldPosition - lastMousePosition).normalized;
            Vector3 normal2D = new Vector3(0, 0, -1f);
            float lineThickness = 0.2f;
            Vector3 newVertexUp = mouseWorldPosition + Vector3.Cross(mouseForwardVector, normal2D) * lineThickness;
            Vector3 newVertexDown = mouseWorldPosition + Vector3.Cross(mouseForwardVector, normal2D * -1f) * lineThickness;

            // debugVisual1.position = newVertexUp;
            // debugVisual2.position = newVertexDown;

            vertices[vIndex2] = newVertexUp;
            vertices[vIndex3] = newVertexDown;

            uv[vIndex2] = Vector2.zero;
            uv[vIndex3] = Vector2.zero;

            int tIndex = triangles.Length - 6;

            triangles[tIndex + 0] = vIndex0;
            triangles[tIndex + 1] = vIndex2;
            triangles[tIndex + 2] = vIndex1;

            triangles[tIndex + 3] = vIndex1;
            triangles[tIndex + 4] = vIndex2;
            triangles[tIndex + 5] = vIndex3;

            // printVertices(vertices);
            mesh.vertices = vertices;
            mesh.uv = uv;
            mesh.triangles = triangles;
            // logMesh(mesh);

            lastMousePosition = mouseWorldPosition;
        }

        return mesh;
    }

    public static Mesh InitMesh()
    {
        mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
        mesh = new Mesh();
    
        Vector3[] vertices = new Vector3[4];
        Vector2[] uv = UtilsClass.createUV();
        int[] triangles = UtilsClass.createTriangles();

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    
        mesh.MarkDynamic(); // if moved to Awake(), nothing is drawn. hmm maybe this is the solution for me.

        lastMousePosition = mouseWorldPosition;

        return mesh;
    }
    
    public static Vector2[] createUV()
    {
        Vector2[] uv = new Vector2[4];
        uv[0] = Vector2.zero;
        uv[1] = Vector2.zero;
        uv[2] = Vector2.zero;
        uv[3] = Vector2.zero;
        return uv;
    }

    public static int[] createTriangles()
    {
        int[] triangles = new int[6];
        triangles[0] = 0;
        triangles[1] = 3;
        triangles[2] = 1;
    
        triangles[3] = 1;
        triangles[4] = 3;
        triangles[5] = 2;
        return triangles;
    }
    
    public static void logMesh(Mesh mesh)
    {
        // if (mesh.vertices.Length != mesh.uv.Length)
        // {
        //     print("Phils Error: mesh.vertices.Length=" + mesh.vertices.Length + " but mesh.uv.Length=" + mesh.uv.Length);
        //     return;
        // }
        // if (mesh.vertices.Length * 1.5 != mesh.triangles.Length)
        // {
        //     print("Phils Error: mesh.vertices.Length=" + mesh.vertices.Length + " but mesh.triangles.Length=" + mesh.triangles.Length);
        //     return;
        // }
        for(int i = 0; i < mesh.vertices.Length; i++)
        {
            print("mesh vertices/colors[" + i + "]=" + mesh.vertices[i].x + "/"+ mesh.vertices[i].y + mesh.colors32[i].ToString());
        }
        for(int i = 0; i < mesh.triangles.Length; i++)
        {
            print("mesh triangles[" + i + "]=" + mesh.triangles[i]);
        }
    }
}
