namespace Datle.Services;

public class VisualizerService
{
    // A simple method to generate SVG circles for a BST node
    public string GenerateNodeSvg(int x, int y, int value)
    {
        return $@"
            <g>
                <circle cx='{x}' cy='{y}' r='20' fill='#0078d4' />
                <text x='{x}' y='{y + 5}' font-size='12' text-anchor='middle' fill='white'>{value}</text>
            </g>";
    }

    public string GenerateLine(int x1, int y1, int x2, int y2)
    {
        return $"<line x1='{x1}' y1='{y1}' x2='{x2}' y2='{y2}' stroke='black' stroke-width='2' />";
    }

    public string DrawArray(int[] numbers, int highlightIdx)
    {
        if (numbers == null || numbers.Length == 0) return "";

        string svg = "";
        int containerHeight = 150; // Match the height in your Home.razor <svg>
        int barWidth = 40;
        int spacing = 10;

        for (int i = 0; i < numbers.Length; i++)
        {
            // Scale height so a value of 10 looks good but doesn't overflow
            int barHeight = numbers[i] * 10; 
        
            // Calculate Y so the bar sits at the bottom
            // (ContainerHeight - barHeight - some padding for text)
            int yPos = containerHeight - barHeight - 20;

            // Color logic: Sorted (green), Current Min/Pivot (red), Unsorted (blue)
            string color = (i < highlightIdx) ? "#28a745" : (i == highlightIdx ? "#ff4444" : "#0078d4");
        
            svg += $@"
                <g>
                    <rect x='{i * (barWidth + spacing) + 10}' y='{yPos}' 
                      width='{barWidth}' height='{barHeight}' 
                      fill='{color}' rx='4' />
                    <text x='{i * (barWidth + spacing) + 10 + (barWidth / 2)}' 
                      y='{containerHeight - 5}' 
                      fill='black' text-anchor='middle' font-size='12' font-weight='bold'>
                    {numbers[i]}
                </text>
            </g>";
        }

        return svg;
    }

    public string DrawLinkedList(int[] numbers)
    {
        if (numbers == null || numbers.Length == 0) return "";
        string svg = "";
        int nodeWidth = 60;
        int nodeHeight = 35;
        int spacing = 100;

        for (int i = 0; i < numbers.Length; i++)
        {
            int x = 20 + (i * spacing);
            int y = 40;

            // Node Box
            svg += $"<rect x='{x}' y='{y}' width='{nodeWidth}' height='{nodeHeight}' fill='#0078d4' rx='4' />";
            svg += $"<text x='{x + (nodeWidth/2)}' y='{y + 22}' fill='white' text-anchor='middle' font-weight='bold' font-size='14'>{numbers[i]}</text>";

            if (i == 0) svg += $"<text x='{x + (nodeWidth/2)}' y='{y - 10}' font-size='12' text-anchor='middle' font-weight='bold'>Head</text>";

            // Arrow
            if (i < numbers.Length - 1)
            {
                // Calculate arrow line to start from right side of node
                int startX = x + nodeWidth;
                int endX = x + spacing;
                int midY = y + (nodeHeight / 2);

                svg += $"<line x1='{startX}' y1='{midY}' x2='{endX}' y2='{midY}' stroke='#333' stroke-width='2' marker-end='url(#arrowhead)' />";
            }
        }

        return svg;
    }

    public string DrawTree(int[] numbers)
    {
        if (numbers == null || numbers.Length == 0) return "";

        string svg = "";
        int canvasWidth = 400; 
        int rowHeight = 60;
        int radius = 18;

        // Helper to calculate X and Y consistently
        (double x, double y) GetNodeCoords(int index) {
            int level = (int)Math.Floor(Math.Log2(index + 1));
            int posInLevel = index - ((int)Math.Pow(2, level) - 1);
            int totalNodesInLevel = (int)Math.Pow(2, level);
        
            double x = (canvasWidth / (double)(totalNodesInLevel + 1)) * (posInLevel + 1);
            double y = 40 + (level * rowHeight);
            return (x, y);
        }   

        // 1. Draw Lines FIRST (so they appear behind nodes)
        for (int i = 1; i < numbers.Length; i++)
        {
            int pIdx = (i - 1) / 2;
            var child = GetNodeCoords(i);
            var parent = GetNodeCoords(pIdx);

            svg += $"<line x1='{child.x}' y1='{child.y}' x2='{parent.x}' y2='{parent.y}' stroke='#444' stroke-width='2' />";
        }

        // 2. Draw Nodes SECOND
        for (int i = 0; i < numbers.Length; i++)
        {
            var coords = GetNodeCoords(i);

            svg += $@"
            <g>
                <circle cx='{coords.x}' cy='{coords.y}' r='{radius}' fill='#0078d4' stroke='white' stroke-width='2' />
                <text x='{coords.x}' y='{coords.y + 5}' fill='white' font-size='12' text-anchor='middle' font-weight='bold'>
                    {numbers[i]}
                </text>
            </g>";
        }

        return svg;
    }

    public string DrawHashTable(string data)
    {
        if (string.IsNullOrEmpty(data)) return "";
    
        var parts = data.Split(',');
        int tableSize = int.Parse(parts[0]);
        // Optionally, the rest of the parts can be the actual keys stored
        var storedKeys = parts.Skip(1).ToList(); 

        string svg = "";
        int bucketHeight = 35;
        for (int i = 0; i < tableSize; i++)
        {
            int y = i * bucketHeight;
            bool isOccupied = i < storedKeys.Count;
            string color = isOccupied ? "#fff3cd" : "#ffffff";
            // Show the actual key from the DataString if it exists
            string content = isOccupied ? $"Key: {storedKeys[i]}" : "empty";

            svg += $@"
            <g>
                <rect x='10' y='{y}' width='160' height='30' fill='{color}' stroke='#333' rx='2' />
                <text x='20' y='{y + 20}' font-size='12' font-family='sans-serif'>
                    Bucket {i}: {content}
                </text>
            </g>";
        }

        return svg;
    }

    public string DrawGraph(string data)
    {
        if (!int.TryParse(data, out int nodeCount) || nodeCount <= 0) nodeCount = 5;

        string svg = "";
        double centerX = 150, centerY = 75; // Center of the SVG
        double radius = 50; // Distance for leaf nodes
    
        // Store coordinates for edge drawing
        var coords = new List<(double x, double y)>();

        // 1. Calculate Coordinates
        // For a "Star" feel, Node 0 is the center
        coords.Add((centerX, centerY));

        for (int i = 1; i < nodeCount; i++)
        {
            double angle = 2 * Math.PI * (i - 1) / (nodeCount - 1);
            double x = centerX + radius * Math.Cos(angle);
            double y = centerY + radius * Math.Sin(angle);
            coords.Add((x, y));
        }

        // 2. Draw Edges (Lines)
        // Connect every leaf (1 to N) to the center (0)
        for (int i = 1; i < coords.Count; i++)
        {
            svg += $"<line x1='{coords[0].x}' y1='{coords[0].y}' x2='{coords[i].x}' y2='{coords[i].y}' stroke='#444' stroke-width='2' />";
        }

        // 3. Draw Nodes (Circles)
        for (int i = 0; i < coords.Count; i++)
        {
            // Highlight center node 0 in a different color
            string color = (i == 0) ? "#ff4444" : "#6c757d";
            svg += $"<circle cx='{coords[i].x}' cy='{coords[i].y}' r='15' fill='{color}' stroke='white' stroke-width='2' />";
            svg += $"<text x='{coords[i].x}' y='{coords[i].y + 5}' fill='white' text-anchor='middle' font-size='10' font-weight='bold'>{i}</text>";
        }

        return svg;
    }
}