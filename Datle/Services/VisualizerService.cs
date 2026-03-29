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
        string svg = "";

        for (int i = 0; i < numbers.Length; i++)
        {
            // We multiply the value by 15 to make the bars tall enough to see
            int height = numbers[i] * 15; 
        
            // If the index is 'sorted' (less than currentOuterIndex), make it green.
            // If it's the one we just swapped, make it red. Otherwise, blue.
            string color = (i < highlightIdx) ? "#28a745" : (i == highlightIdx ? "#ff4444" : "#0078d4");
        
            svg += $@"
            <rect x='{i * 45}' y='{120 - height}' width='40' height='{height}' fill='{color}' rx='4' />
            <text x='{i * 45 + 20}' y='140' fill='white' text-anchor='middle' font-size='12'>{numbers[i]}</text>";
        }

        return svg;
    }
}