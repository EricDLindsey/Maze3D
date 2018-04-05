using System.Collections.Generic;

public enum Dir
{
	None,
	North,
	East,
	South,
	West
}

class Maze
{
	public Maze(int w, int h)
	{
		Width = w;
		Height = h;
		grid = new Dictionary<string, Cell>();
	}
		
	private Dictionary<string, Cell> grid;
		
	public int Width { get; protected set; }
	public int Height { get; protected set; }
	public Cell this[int x, int y] { get { return getCell(x, y); } }
		
	public Dir[] GetUnvisited(Cell c)
	{
		List<Dir> list = new List<Dir>();

		if(c.Y > 0 && !grid.ContainsKey(c.X + ", " + (c.Y - 1)))
			list.Add(Dir.North);
		if(c.X > 0 && !grid.ContainsKey((c.X - 1) + ", " + c.Y))
			list.Add(Dir.West);
		if(c.X < Width - 1 && !grid.ContainsKey((c.X + 1) + ", " + c.Y))
			list.Add(Dir.East);
		if(c.Y < Height - 1 && !grid.ContainsKey(c.X + ", " + (c.Y + 1)))
			list.Add(Dir.South);
			
		return list.ToArray();
	}

	public void Clear()
	{
		grid.Clear();
	}

	public Cell GetCell(Cell c, Dir d = Dir.None)
	{
		Cell rCell;

		switch(d)
		{
			case Dir.North:
				rCell = getCell(c.X, c.Y - 1);
				break;
			case Dir.East:
				rCell = getCell(c.X + 1, c.Y);
				break;
			case Dir.South:
				rCell = getCell(c.X, c.Y + 1);
				break;
			case Dir.West:
				rCell = getCell(c.X - 1, c.Y);
				break;
			case Dir.None:
				rCell = getCell(c.X, c.Y);
				break;
			default:
				rCell = null;
				break;
		}

		return rCell;
	}

	private Cell getCell(int x, int y)
	{
		if(x >= 0 && y >= 0 && x < Width && y < Height)
		{
			if(grid.ContainsKey(x + ", " + y))
				return grid[x + ", " + y];

			Cell c = new Cell(x, y);
			grid.Add(x + ", " + y, c);
			return c;
		}

		return null;
	}
}