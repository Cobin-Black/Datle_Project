namespace DailyDSProject.Services;

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
}