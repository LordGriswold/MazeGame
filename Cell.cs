namespace MazeGame
{
    // Represents a single cell in the maze
    // Initially surrounded on all sides by walls, which are removed during the maze-generation step
    class Cell
    {
        public bool topWall;
        public bool rightWall;
        public bool bottomWall;
        public bool leftWall;
        public string type;
        public int xPos;
        public int yPos;

        public Cell(int x, int y, string t) 
        {
            topWall = true;
            rightWall = true;
            bottomWall = true;
            leftWall = true;
            type = t;
            xPos = x;
            yPos = y;
        }
    }
}