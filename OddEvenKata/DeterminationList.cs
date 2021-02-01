using System;
using System.Collections;
using System.Collections.Generic;

namespace OddEvenKata
{
	public class DeterminationList : IEnumerable<string>
	{
		private int _start;
		private int _end;
		public DeterminationList(int start, int end)
		{
			_start = start;
			_end = end;
		}

		public dynamic this[int number]
		{
			get
			{
				return Execute(number);
			}
		}

		public dynamic Execute(int num)
		{
			if (_start > num && num > _end) throw new ArgumentException("Number out of range!");
			if (num % 2 == 0) return "Even";
			else if (num % 2 != 0 && !num.IsPrime()) return "Odd";
			else return num;
		}

		public IEnumerator<string> GetEnumerator()
		{
			for (int i = _start; i < _end; i++)
			{
				yield return Execute(i);
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}
	}

}