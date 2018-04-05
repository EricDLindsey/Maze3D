using System;
using System.Collections.Generic;

class Cell
{
	public Cell(int x, int y)
	{
		X = x;
		Y = y;
		open = new HashSet<Dir>();
	}
		
	public int X { get; protected set; }
	public int Y { get; protected set; }

	private HashSet<Dir> open;

	public void SetOpening(Dir d)
	{
		open.Add(d);
	}

	public bool IsOpen(Dir d)
	{
		return open.Contains(d);
	}

	public static implicit operator Cell(string s)
	{
		string[] s2 = s.Split(',');

		int x = Int32.Parse(s2[0].Trim());
		int y = Int32.Parse(s2[1].Trim());
		return new Cell(x, y);
	}

	public override bool Equals(object obj)
	{
		if(obj == null)
			return false;
		Cell b = obj as Cell;
		if(b == null)
			return false;
		if(X == b.X && Y == b.Y)
			return true;
		return false;
	}

	public override int GetHashCode()
	{
		int hash = 17;
		hash = hash * 23 + X.GetHashCode();
		hash = hash * 23 + Y.GetHashCode();
		return hash;
	}
}