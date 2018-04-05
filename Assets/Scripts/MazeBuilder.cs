using System.Collections.Generic;
using UnityEngine;

class MazeBuilder : MonoBehaviour
{
	private void Start()
	{
		if(GameData.MazeX > 0)
			Width = GameData.MazeX;
		if(GameData.MazeY > 0)
			Height = GameData.MazeY;

		Maze = new Maze(Width, Height);
		build();
	}

	public Maze Maze { get; protected set; }

	public int Width = 10;
	public int Height = 10;
	public GameObject Wall;
	public GameObject Floor;
		
	private void build()
	{
		gen();
		assemble();
	}

	public override string ToString()
	{
		string s = "";

		for(int i = 0; i < Maze.Width*2+1; i++)
			s += "_";
		s += "\n";

		for(int y = 0; y < Maze.Height; y++)
		{
			for(int x = 0; x < Maze.Width; x++)
			{
				if(!Maze[x, y].IsOpen(Dir.West))
					s += "|";
				else
					s += "_";

				if(Maze[x, y].IsOpen(Dir.South))
					s += " ";
				else
					s += "_";
			}

			s += "|\n";
		}

		return s;
	}

	private void gen()
	{
		Maze.Clear();
		Stack<Cell> stack = new Stack<Cell>();
		Cell curCell = Maze.GetCell(Random.Range(0, GameData.MazeX) + ", " + Random.Range(0, GameData.MazeY));
		stack.Push(curCell);

		while(stack.Count > 0)
		{
			Dir[] openings = Maze.GetUnvisited(curCell);

			if(openings.Length == 0)
			{
				curCell = stack.Pop();
			}
			else
			{
				int choose = Random.Range(0, openings.Length);

				curCell.SetOpening(openings[choose]);
				curCell = Maze.GetCell(curCell, openings[choose]);
				curCell.SetOpening(opposite(openings[choose]));

				stack.Push(curCell);
			}
		}
	}

	private Dir opposite(Dir d)
	{
		Dir rd;

		switch(d)
		{
			case Dir.North:
				rd = Dir.South;
				break;
			case Dir.East:
				rd = Dir.West;
				break;
			case Dir.South:
				rd = Dir.North;
				break;
			case Dir.West:
				rd = Dir.East;
				break;
			default:
				rd = Dir.None;
				break;
		}

		return rd;
	}

	private void assemble()
	{
		foreach(Transform child in transform)
		{
			Destroy(child.gameObject);
		}

		for(int y = 0; y < Height; y++)
		{
			for(int x = 0; x < Width; x++)
			{
				GameObject go = Instantiate(Floor, new Vector3(y * 2, 0, x * 2), Quaternion.identity, transform);
				go.name = "(" + x + ", " + y + ")";

				if(!Maze[x, y].IsOpen(Dir.West))
					Instantiate(Wall, new Vector3(y * 2, 0, x * 2), Quaternion.Euler(0, 90, 0), transform);
				if(!Maze[x, y].IsOpen(Dir.North))
					Instantiate(Wall, new Vector3(y * 2, 0, x * 2), Quaternion.identity, transform);
			}

			Instantiate(Wall, new Vector3(y * 2, 0, Width * 2), Quaternion.Euler(0, 90, 0), transform);
		}

		for(int i = 0; i < Width; i++)
		{
			Instantiate(Wall, new Vector3(Width * 2, 0, i * 2), Quaternion.identity, transform);
		}
	}
}