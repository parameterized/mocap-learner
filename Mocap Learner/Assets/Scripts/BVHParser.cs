using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;

public class BVHParser
{
    private StreamReader sr;
    private int lineNumber;
    private string line;
    private string[] tokens;

    public BVHData Parse(string filePath)
    {
        using (sr = File.OpenText(filePath))
        {
            lineNumber = 0;

            ReadTokens(sr);
            Assert(tokens[0] == "HIERARCHY", "Expected HIERARCHY");

            List<BVHElement> skeletons = ReadChildren(sr);

            Assert(tokens[0] == "MOTION", "Expected MOTION");

            ReadTokens(sr);
            Assert(tokens[0] == "Frames:", "Expected \"Frames:\"");
            Assert(tokens.Length == 2, "Invalid Frames");
            int numFrames = int.Parse(tokens[1], CultureInfo.InvariantCulture);

            ReadTokens(sr);
            Assert(tokens.Length >= 2 && tokens[0] == "Frame" && tokens[1] == "Time:", "Expected \"Frame Time:\"");
            Assert(tokens.Length == 3, "Invalid Frame Time");
            float frameTime = float.Parse(tokens[2], CultureInfo.InvariantCulture);

            List<List<float>> frames = new List<List<float>>();
            for (int i = 0; i < numFrames; i++)
            {
                frames.Add(new List<float>());
                ReadTokens(sr);
                foreach (string token in tokens)
                {
                    frames[i].Add(float.Parse(token, CultureInfo.InvariantCulture));
                }
            }

            return new BVHData(skeletons, frames, frameTime);
        }
    }

    private void ReadTokens(StreamReader sr)
    {
        line = sr.ReadLine().Trim();
        lineNumber += 1;
        tokens = line.Split(' ');
    }

    private List<BVHElement> ReadChildren(StreamReader sr)
    {
        List<BVHElement> children = new List<BVHElement>();
        while (true)
        {
            BVHElement bvhe = new BVHElement();
            ReadTokens(sr);
            if (tokens[0] == "ROOT")
            {
                bvhe.Type = "ROOT";
                bvhe.Name = tokens[1];
            }
            else if (tokens[0] == "JOINT")
            {
                bvhe.Type = "JOINT";
                bvhe.Name = tokens[1];
            }
            else if (tokens.Length == 2 && tokens[0] == "End" && tokens[1] == "Site")
            {
                bvhe.Type = "End Site";
                bvhe.Name = "End Site";
            }
            else
            {
                return children;
            }

            ReadTokens(sr);
            Assert(tokens[0] == "{", "Expected {");

            ReadTokens(sr);
            Assert(tokens[0] == "OFFSET", "Expected OFFSET");
            Assert(tokens.Length == 4, "Invalid offset");
            bvhe.Offset = new Vector3(
                float.Parse(tokens[1], CultureInfo.InvariantCulture),
                float.Parse(tokens[2], CultureInfo.InvariantCulture),
                float.Parse(tokens[3], CultureInfo.InvariantCulture));

            if (bvhe.Type == "End Site")
            {
                ReadTokens(sr);
            }
            else
            {
                ReadTokens(sr);
                Assert(tokens[0] == "CHANNELS", "Expected CHANNELS");
                Assert(tokens.Length >= 2, "Channels not specified");
                int numChannels = int.Parse(tokens[1], CultureInfo.InvariantCulture);
                Assert(tokens.Length == 2 + numChannels, "Invalid channels");
                bvhe.Channels = tokens.Skip(2).Take(numChannels).ToArray();

                bvhe.Children.AddRange(ReadChildren(sr));
            }

            Assert(tokens[0] == "}", "Expected }");
            children.Add(bvhe);
        }
    }

    private void Assert(bool condition, string message)
    {
        Debug.Assert(condition, message + " (line " + lineNumber + ")");
    }
}

public class BVHElement
{
    public string Type { get; set; }
    public string Name { get; set; }
    public Vector3 Offset { get; set; }
    public string[] Channels { get; set; }
    public List<BVHElement> Children { get; set; }

    public BVHElement()
    {
        Type = string.Empty;
        Name = string.Empty;
        Offset = Vector3.zero;
        Channels = new string[0];
        Children = new List<BVHElement>();
    }

    public BVHElement(string type, string name, Vector3 offset, string[] channels, List<BVHElement> children)
    {
        Type = type;
        Name = name;
        Offset = offset;
        Channels = channels;
        Children = children;
    }
}

public class BVHData
{
    public List<BVHElement> Skeletons { get; set; }
    public List<List<float>> Frames { get; set; }
    public float FrameTime { get; set; }

    public BVHData(List<BVHElement> skeletons, List<List<float>> frames, float frameTime)
    {
        Skeletons = skeletons;
        Frames = frames;
        FrameTime = frameTime;
    }
}
