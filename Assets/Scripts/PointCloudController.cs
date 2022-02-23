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

        sourceServer.OnSignalReceived.AddListener(ParseOnlineData);
    }

    void ParseOnlineData(string data){
        try {
            string[] points = data.Split("|");

            Debug.Log($"data count : {points.Length}");

            List<Vector3> pList = new List<Vector3>();
            foreach (var point in points)
            {
                string[] v3 = point.Split(",");

                float x = 0;
                float.TryParse(v3[0], out x);
                float y = 0;
                float.TryParse(v3[1], out y);
                float z = 0;
                float.TryParse(v3[2], out z);

                pList.Add(new Vector3(x,y,z));
            }

            CreateSinglePoint(pList);
        } catch {}

        //Debug.Log($"Data come {data}");
    }

    void CreateSinglePoint(List<Vector3> v3){
        float amp = 0.001f;
        
        for (int i = 0; i < num; i++)
        {
            try {
                if(i < v3.Count){
                    vertices[i].x = v3[i].x * amp;
                    vertices[i].y = v3[i].y * amp;
                    vertices[i].z = v3[i].z * amp;
                } else {
                    vertices[i].x = 0;
                    vertices[i].y = 0;
                    vertices[i].z = 0;
                }

                colors[i].b = 255;
                colors[i].g = 0;
                colors[i].r = 0;
                colors[i].a = 255;

                // colors[i].b = (byte)Random.Range(0, 255);
                // colors[i].g = (byte)Random.Range(0, 255);
                // colors[i].r = (byte)Random.Range(0, 255);
                // colors[i].a = (byte)Random.Range(0, 255);
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
