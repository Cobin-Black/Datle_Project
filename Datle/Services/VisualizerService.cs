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
}