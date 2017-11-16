using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[AddComponentMenu("Projection Mapping/Callibration Grid")]
public class CalibrationGrid: MonoBehaviour
{
    /*------------------------
      *Line Drawing 
      *------------------------*/
    [Header("Grid Dimensions")]
    public int gridSize = 100;
    public int squareSize = 10;
    [Header("Lines Settings")]
    public Material lineMaterial;
    public Color lineColor = Color.white;

    // Will be called after all regular rendering is done
    public void OnRenderObject()
    {
            if (!lineMaterial || lineMaterial == null)
            {
                lineMaterial = new Material(Shader.Find("Standard"));
                lineColor = Color.white;
                lineMaterial.SetColor("_Color", lineColor);
                lineMaterial.SetColor("_EmissionColor", lineColor);
                //lineMaterial.SetColor()
                //GetComponent<Renderer>().material = lineMaterial;
            }
            // Apply the line material
            lineMaterial.SetPass(0);

            GL.PushMatrix();
            GL.MultMatrix(transform.localToWorldMatrix);
            // Set transformation matrix for drawing to
            // match our transform
            GL.MultMatrix(transform.localToWorldMatrix);
            // Vertex colors 
            GL.Color(lineColor);
            // Draw lines
            GL.Begin(GL.LINES);
            //Lines on X Axis
            for (int i = 0; i < gridSize + 1; ++i)
            {
                // One vertex at transform position
                GL.Vertex3(i * squareSize - gridSize * squareSize * 0.5f, -0.5f, -gridSize * squareSize * 0.5f);
                // Another vertex at edge of circle
                GL.Vertex3(i * squareSize - gridSize * squareSize * 0.5f, -0.5f, gridSize * squareSize * 0.5f);

                //Z Axis
                // One vertex at transform position
                GL.Vertex3(-gridSize * squareSize * 0.5f, -0.5f, i * squareSize - gridSize * squareSize * 0.5f);
                // Another vertex at edge of circle
                GL.Vertex3(gridSize * squareSize * 0.5f, -0.5f, i * squareSize - gridSize * squareSize * 0.5f);
            }
            GL.End();
            GL.PopMatrix();
        
    }
}