using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.IO;


public class PointCloudController : MonoBehaviour
{
    int num;
    Mesh mesh;
    Vector3[] vertices;
    Color32[] colors;
    int[] indices;
    
    public SignalServer sourceServer;
    string path;
    public string fileName = "pc.bin";
    void Start()
    {
        InitMesh();

        sourceServer.OnSignalReceivedByte += SaveFile;
    }

    void InitMesh()
    {
        num = 92160;

        mesh = new Mesh();
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

        vertices = new Vector3[num];
        colors = new Color32[num];
        indices = new int[num];

        for (int i = 0; i < num; i++)
        {
            indices[i] = i;
        }

        mesh.vertices = vertices;
        mesh.colors32 = colors;
        mesh.SetIndices(indices, MeshTopology.Points, 0);

        gameObject.GetComponent<MeshFilter>().mesh = mesh;
    }

    StreamWriter writer;
    FileStream fs;
    BinaryWriter bw;

    void SaveFile(byte[] data){

        path = Application.dataPath + "/../" + fileName;
        //writer = new StreamWriter(path, true);
        fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
        bw = new BinaryWriter(fs);

        bw.Write(data);

        if(bw != null) bw.Close();
        if(fs != null) fs.Close();

        Debug.Log("File write");

        CreatePoints();
    }

    void CreatePoints(){
        path = Application.dataPath + "/../" + fileName;
        FileStream myFile = File.Open(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        BinaryReader myReader = new BinaryReader(myFile);
        
        float amp = 0.001f;
        for (int i = 0; i < num; i++)
        {
            try {
                short myDataX = myReader.ReadInt16();
                vertices[i].x = myDataX * amp;
                short myDataY = myReader.ReadInt16();
                vertices[i].y = myDataY * amp;
                short myDataZ = myReader.ReadInt16();
                vertices[i].z = myDataZ * amp;

                colors[i].b = 255;
                colors[i].g = 255;
                colors[i].r = 255;
                colors[i].a = 255;
            }
            catch(System.Exception e){
                Debug.LogWarning(e.Message.ToString());
                break;
            }
        }

        mesh.vertices = vertices;
        mesh.colors32 = colors;
        mesh.RecalculateBounds();
    }

    private void OnApplicationQuit() {
        if(writer != null) writer.Close();
        if(bw != null) bw.Close();
        if(fs != null) fs.Close();
    }
}
