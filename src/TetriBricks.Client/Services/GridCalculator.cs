namespace TetriBricks.Client.Services;

public record GridDimensions(int Rows, int Columns, int TileSize);

public static class GridCalculator
{
    private const int ChromeHeight = 110;
    private const int MinGridDimension = 6;
    private const int MaxRows = 20;
    private const int MaxColumns = 24;

    public static int CalculateTileSize(int viewportWidth, int viewportHeight, bool isTouchDevice, int rows, int columns)
    {
        int minTile = isTouchDevice ? 34 : 28;
        int maxTile = isTouchDevice ? 44 : 46;

        int availableHeight = Math.Max(viewportHeight - ChromeHeight, 100);
        int availableWidth = Math.Max(viewportWidth - 16, 100);

        int maxByWidth = availableWidth / columns;
        int maxByHeight = availableHeight / rows;
        int tileSize = Math.Min(maxByWidth, maxByHeight);

        return Math.Clamp(tileSize, minTile, maxTile);
    }

    public static GridDimensions Calculate(int viewportWidth, int viewportHeight, bool isTouchDevice)
    {
        int minTile = isTouchDevice ? 34 : 28;
        int maxTile = isTouchDevice ? 44 : 46;

        int availableHeight = viewportHeight - ChromeHeight;
        int availableWidth = viewportWidth - 16; // small horizontal margin

        if (availableHeight < 100) availableHeight = 100;
        if (availableWidth < 100) availableWidth = 100;

        // Start with max tile size and work down
        int tileSize = maxTile;
        int rows, columns;

        while (tileSize >= minTile)
        {
            columns = Math.Clamp(availableWidth / tileSize, MinGridDimension, MaxColumns);
            rows = Math.Clamp(availableHeight / tileSize, MinGridDimension, MaxRows);

            // Check that the grid fits
            if (columns * tileSize <= availableWidth && rows * tileSize <= availableHeight)
            {
                return new GridDimensions(rows, columns, tileSize);
            }

            tileSize--;
        }

        // Fallback: use min tile size
        tileSize = minTile;
        columns = Math.Clamp(availableWidth / tileSize, MinGridDimension, MaxColumns);
        rows = Math.Clamp(availableHeight / tileSize, MinGridDimension, MaxRows);

        return new GridDimensions(rows, columns, tileSize);
    }
}
