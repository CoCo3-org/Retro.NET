using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSIL
{
	public class MSILStack
	{
		private List<object> List { get; } = new List<object>();

		public int Length { get { return this.List.Count; } }

		public object this[int index] { get { return this.List[index]; } set { this.List[index] = value; } }

		public void Push(object obj)
		{
			this.List.Insert(0, obj);
		}

		public object Pop()
		{
			object obj = this.List[0];
			this.List.RemoveAt(0);
			return obj;
		}

		public void Pop(int n)
		{
			if (n > 0)
			{
				for (int x = 0; x < n; x++)
					this.List.RemoveAt(0);
			}
		}

		public object Peek()
		{
			return this.List[0];
		}
	}
}
