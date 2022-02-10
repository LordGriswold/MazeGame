using System.Collections.Generic;
using System;
namespace MazeGame
{
    class Maze
    {
        int size;
        List<Cell> frontier; // Non-maze cells that are adjacent to maze cells
        Cell[,] maze; // Actual maze

        public Maze(int s) 
        {
            size = s;
            maze = new Cell[size, size];
            frontier = new List<Cell>();

            // Pick a position for a cell to be added to the maze
            System.Random random = new System.Random();
            int randomRow = random.Next(size);
            int randomCol = random.Next(size);
            maze[randomRow, randomCol] = new Cell(randomRow, randomCol, "maze");

            // Add adjacent frontier cells to the maze and update the frontier list
            if (randomCol > 0)  // Check right
            {
                maze[randomRow, randomCol - 1] = new Cell(randomRow, randomCol - 1, "frontier");
                frontier.Add(maze[randomRow, randomCol - 1]);
            }
            if (randomCol < size - 1)  // Check left
            {
                maze[randomRow, randomCol + 1] = new Cell(randomRow, randomCol + 1, "frontier");
                frontier.Add(maze[randomRow, randomCol + 1]);
            }
            if (randomRow > 0) // Check above
            {
                maze[randomRow - 1, randomCol] = new Cell(randomRow - 1, randomCol, "frontier");
                frontier.Add(maze[randomRow - 1, randomCol]);
            }
            if (randomRow < size - 1) // Check below
            {
                maze[randomRow + 1, randomCol] = new Cell(randomRow + 1, randomCol, "frontier");
                frontier.Add(maze[randomRow + 1, randomCol]);
            }

            // While the frontier count is 0...
            
            while (frontier.Count > 0)
            {
                // Randomly pick a cell in the frontier
                int randomIndex = random.Next(frontier.Count);
                Cell fCell = frontier[randomIndex];
                frontier.RemoveAt(randomIndex);
                // Create a list of all maze cells adjacent to the frontier cell
                List<Cell> adjacentMazeCells = new List<Cell>();

                if (fCell.yPos > 0 && string.Equals(maze[fCell.xPos, fCell.yPos - 1]?.type, "maze")) // Check left
                {
                    adjacentMazeCells.Add(maze[fCell.xPos, fCell.yPos - 1]);
                }
                if (fCell.yPos < size - 1 && string.Equals(maze[fCell.xPos, fCell.yPos + 1]?.type, "maze")) // Check right
                {
                    adjacentMazeCells.Add(maze[fCell.xPos, fCell.yPos + 1]);
                }
                if (fCell.xPos > 0 && string.Equals(maze[fCell.xPos - 1, fCell.yPos]?.type, "maze")) // Check above
                {
                    adjacentMazeCells.Add(maze[fCell.xPos - 1, fCell.yPos]);
                }
                if (fCell.xPos < size - 1 && string.Equals(maze[fCell.xPos + 1, fCell.yPos]?.type, "maze")) // Check below
                {
                    adjacentMazeCells.Add(maze[fCell.xPos + 1, fCell.yPos]);
                }

                // Randomly pick an adjacent maze cell
                randomIndex = random.Next(adjacentMazeCells.Count);
                Cell mCell = adjacentMazeCells[randomIndex];

                // Remove the wall connecting the two cells
                if (mCell.xPos < fCell.xPos) // Maze cell is above
                {
                    mCell.bottomWall = false;
                    fCell.topWall = false;
                }
                else if (mCell.xPos > fCell.xPos) // Maze cell is below
                {
                    mCell.topWall = false;
                    fCell.bottomWall = false;
                }
                else if (mCell.yPos < fCell.yPos) // Maze cell is left
                {
                    mCell.rightWall = false;
                    fCell.leftWall = false;
                }
                else if (mCell.yPos > fCell.yPos) // Maze cell is right
                {
                    mCell.leftWall = false;
                    fCell.rightWall = false;
                }

                // Add the frontier cell to the maze
                maze[fCell.xPos, fCell.yPos].type = "maze";

                // Add any new frontier cells to the frontier list

                if (fCell.yPos > 0 && !string.Equals(maze[fCell.xPos, fCell.yPos - 1]?.type, "maze") && !string.Equals(maze[fCell.xPos, fCell.yPos - 1]?.type, "frontier"))  // Check above
                {
                    maze[fCell.xPos, fCell.yPos - 1] = new Cell(fCell.xPos, fCell.yPos - 1, "frontier");
                    frontier.Add(maze[fCell.xPos, fCell.yPos - 1]);
                }
                if (fCell.yPos < size - 1 && !string.Equals(maze[fCell.xPos, fCell.yPos + 1]?.type, "maze") && !string.Equals(maze[fCell.xPos, fCell.yPos + 1]?.type, "frontier"))  // Check below
                {
                    maze[fCell.xPos, fCell.yPos + 1] = new Cell(fCell.xPos, fCell.yPos + 1, "frontier");
                    frontier.Add(maze[fCell.xPos, fCell.yPos + 1]);
                }
                if (fCell.xPos > 0 && !string.Equals(maze[fCell.xPos - 1, fCell.yPos]?.type, "maze") && !string.Equals(maze[fCell.xPos - 1, fCell.yPos]?.type, "frontier")) // Check left
                {
                    maze[fCell.xPos - 1, fCell.yPos] = new Cell(fCell.xPos - 1, fCell.yPos, "frontier");
                    frontier.Add(maze[fCell.xPos - 1, fCell.yPos]);
                }
                if (fCell.xPos < size - 1 && !string.Equals(maze[fCell.xPos + 1, fCell.yPos]?.type, "maze") && !string.Equals(maze[fCell.xPos + 1, fCell.yPos]?.type, "frontier")) // Check right
                {
                    maze[fCell.xPos + 1, fCell.yPos] = new Cell(fCell.xPos + 1, fCell.yPos, "frontier");
                    frontier.Add(maze[fCell.xPos + 1, fCell.yPos]);
                }
            }
            Console.WriteLine("Done!");
            displayMaze();
        }

        public void displayMaze()
        {
            for (int row = 0; row < size; row++) 
            {
                for (int col = 0; col < size; col++)
                {   
                    Console.Write(maze[row, col].topWall ? "xxxx" : "x  x");
                }
                Console.WriteLine();
                for (int col = 0; col < size; col++)
                {   
                    Console.Write(maze[row, col].leftWall ? "x" : " ");
                    Console.Write("  ");
                    Console.Write(maze[row, col].rightWall ? "x" : " ");
                }
                Console.WriteLine();
                for (int col = 0; col < size; col++)
                {   
                    Console.Write(maze[row, col].bottomWall ? "xxxx" : "x  x");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}