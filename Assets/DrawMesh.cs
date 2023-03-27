using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class DrawMesh : MonoBehaviour {

    //List is easier to manipulate than array.
    private List<Dot> dots = new List<Dot>();

    [SerializeField] private Transform debugVisual1;
    [SerializeField] private Transform debugVisual2;

    private Vector3 mouseWorldPosition;
    private Mesh mesh;
    private Vector3 lastMousePosition;

    private void Awake()
    {
        // mesh = new Mesh();
        // GetComponent<MeshFilter>().mesh = UtilsClass.drawDot();
        // GetComponent<MeshFilter>().mesh.MarkDynamic();
        // UtilsClass.mesh = mesh;
        
        mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
        mesh = new Mesh();
        
        Vector3[] vertices = new Vector3[4];
        Vector2[] uv = UtilsClass.createUV();
        int[] triangles = UtilsClass.createTriangles();
        
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    
        GetComponent<MeshFilter>().mesh = mesh;
        // GetComponent<MeshFilter>().mesh = UtilsClass.InitMesh();
        mesh.MarkDynamic(); // if moved to Awake(), nothing is drawn. hmm maybe this is the solution for me.
        DrawSomethingForChristsSake();
        // lastMousePosition = mouseWorldPosition;
    }

    private void DrawSomethingForChristsSake() {

        Vector3[] vertices = new Vector3[6];
        Vector2[] uv = new Vector2[6];
        int[] triangles = new int[9];
        
        vertices[0] = new Vector3(0,0,0);
        vertices[1] = new Vector3(0,1000,0);
        vertices[2] = new Vector3(1000,1000,0);
        vertices[3] = new Vector3(1000,0,0);
        vertices[4] = new Vector3(1000,2000,0);
        vertices[5] = new Vector3(2000,100,0);
        
        uv[0] = Vector2.zero;
        uv[1] = Vector2.zero;
        uv[2] = Vector2.zero;
        uv[3] = Vector2.zero;
        uv[4] = Vector2.zero;
        uv[5] = Vector2.zero;

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;
        triangles[3] = 3;
        triangles[4] = 4;
        triangles[5] = 5;
        // triangles[6] = 6;
        // triangles[7] = 7;
        // triangles[8] = 8;
        
        // mesh.vertices.CopyTo(vertices, 0);
        // mesh.uv.CopyTo(uv, 0);
        // mesh.triangles.CopyTo(triangles, 0);

        // int vIndex = vertices.Length - 4;
        // int vIndex0 = vIndex + 0;
        // int vIndex1 = vIndex + 1;
        // int vIndex2 = vIndex + 2;
        // int vIndex3 = vIndex + 3;

        // Vector3 mouseForwardVector = (mouseWorldPosition - lastMousePosition).normalized;
        // Vector3 normal2D = new Vector3(0, 0, -1f);
        // float lineThickness = 0.2f;
        // Vector3 newVertexUp = mouseWorldPosition + Vector3.Cross(mouseForwardVector, normal2D) * lineThickness;
        // Vector3 newVertexDown = mouseWorldPosition + Vector3.Cross(mouseForwardVector, normal2D * -1f) * lineThickness;

        // vertices[vIndex2] = newVertexUp;
        // vertices[vIndex3] = newVertexDown;

        // uv[vIndex2] = Vector2.zero;
        // uv[vIndex3] = Vector2.zero;

        // int tIndex = triangles.Length - 6;

        // triangles[tIndex + 0] = vIndex0;
        // triangles[tIndex + 1] = vIndex2;
        // triangles[tIndex + 2] = vIndex1;

        // triangles[tIndex + 3] = vIndex1;
        // triangles[tIndex + 4] = vIndex2;
        // triangles[tIndex + 5] = vIndex3;

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        lastMousePosition = mouseWorldPosition;
    }

    private void Update() {
        // GetComponent<MeshFilter>().mesh = UtilsClass.drawDot();
        if (Input.GetMouseButtonDown(0)) { // Mouse Pressed
            // print("mouse pressed");

            GetComponent<MeshFilter>().mesh = UtilsClass.mesh;
            MousePressedCodeMonkey();
            // mesh = UtilsClass.MousePressedCodeMonkey();
        }     
        
        if (Input.GetMouseButton(0)) { // Mouse held down/dragged
            // print("mouse dragged");

            MouseDraggedCodeMonkey();
            // mesh = UtilsClass.MouseDraggedCodeMonkey();

            // my technique 
            mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            // addDotToList(mouseWorldPosition.x, mouseWorldPosition.y, 100);
        }
        
        // copyDotsToMesh();
    }
    
    public void MousePressedCodeMonkey()
    {
        // print("mouse pressed");

    }
       
    private void MouseDraggedCodeMonkey() {
        mouseWorldPosition = UtilsClass.GetMouseWorldPosition(); // need to re-get to draw.
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
    
            debugVisual1.position = newVertexUp;
            debugVisual2.position = newVertexDown;
    
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

            UtilsClass.mesh = mesh;
            lastMousePosition = mouseWorldPosition;
        }
    }

    private void printVertices(Vector3[] vertices)
    {
        foreach(Vector3 vector in vertices)
        {
            print("v=" + vector);
        }
    }
    
    /**
     * vertice and uv always have same length.
     * triangles always 50% more?  why would triangles ever be anything besides 0,3,1,1,3,2?
     * 
     * List<Dot>
     *   public Vector3[4] vertices
     *   public Vector2[4] uv
     *   public int[6] triangles 
     */
    private void addDotToList(float x, float y, int size)
    {
        Dot dot = new Dot();

        Vector3[] vertices  = new Vector3[4];
        Vector2[] uv        = new Vector2[4];
        int[]     triangles = new int    [6];
        
        vertices[0] = new Vector3(x, y);
        vertices[1] = new Vector3(x, y+size);
        vertices[2] = new Vector3(x+size,y+size);
        vertices[3] = new Vector3(x+size, y);
        
        uv[0] = Vector2.zero;
        uv[1] = Vector2.zero;
        uv[2] = Vector2.zero;
        uv[3] = Vector2.zero;
        
        triangles[0] = 0;
        triangles[1] = 3;
        triangles[2] = 1;
        
        triangles[3] = 1;
        triangles[4] = 3;
        triangles[5] = 2;
        
        dot.vertices = vertices;
        dot.uv = uv;
        dot.triangles = triangles;

        dots.Add(dot);
        // print("addDotToList:dot added: " + dot.ToString());
    }
    
    /**
     * called each frame to copy List<Dot> to Mesh arrays 
     */
    private void copyDotsToMesh ()
    {
        // print("copyDotsToMesh");
        // Base size on number of Dots
        Vector3[] vertices  = new Vector3[dots.Count * 4];
        Vector2[] uv        = new Vector2[dots.Count * 4];
        int[]     triangles = new int    [dots.Count * 6];

        if (vertices.Length * 1.5 != triangles.Length)
        {
            print("copyDotsToMesh(): philError: vertices.Length = " + vertices.Length + " but traing=" + triangles.Length);
            return;
        }
        
        // print("dots.size=" + dots.Count);
        for (int i = 0; i < dots.Count; i++)
        {
            Dot dot = dots[i];
            // a Dot contains 4 vertice 4 uv 6 triangle
            /* example
             * dots[0].vertices = Vector3[01,01,0], Vector3[01,05,0], Vector3[05,05,0], Vector3[05,01,0]
             * dots[0].vertices = Vector3[11,11,0], Vector3[11,15,0], Vector3[15,15,0], Vector3[15,11,0]
             * then mesh.vertices is size 8
             * mesh.vertices[0] =  Vector3[01,01,0] etc
             */
            
            int idx = i * 4; //index Based on i;
            vertices[idx + 0] = new Vector3(dot.vertices[idx + 0].x, dot.vertices[idx + 0].y);
            vertices[idx + 1] = new Vector3(dot.vertices[idx + 1].x, dot.vertices[idx + 1].y);
            vertices[idx + 2] = new Vector3(dot.vertices[idx + 2].x, dot.vertices[idx + 2].y);
            vertices[idx + 3] = new Vector3(dot.vertices[idx + 3].x, dot.vertices[idx + 3].y);

            uv[idx + 0] = Vector2.zero;
            uv[idx + 1] = Vector2.zero;
            uv[idx + 2] = Vector2.zero;
            uv[idx + 3] = Vector2.zero;

            triangles[idx + 0] = 0;
            triangles[idx + 1] = 3;
            triangles[idx + 2] = 1;
            triangles[idx + 3] = 1;
            triangles[idx + 4] = 3;
            triangles[idx + 5] = 2;
        }

        if (dots.Count > 0) {
            mesh.vertices = vertices;
            mesh.uv = uv;
            mesh.triangles = triangles;
            UtilsClass.logMesh(mesh);
            GetComponent<MeshFilter>().mesh = mesh;
            GetComponent<MeshFilter>().mesh = mesh;
        } else {
            // print("dots is empty.");
        }
    }

}