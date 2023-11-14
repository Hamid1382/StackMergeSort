using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace StackMergeSort
{
	public static class Program
	{
		public static List<T> Merge<T>(List<T> a, List<T> b) where T : IComparable<T>
		{
			List<T> result = new();
			int i = 0, j = 0;
			while (i < a.Count && j < b.Count)
			{
				if (a[i].CompareTo(b[j]) < 0) result.Add(a[i++]);
				else result.Add(b[j++]);
			}
			while (i < a.Count) result.Add(a[i++]);
			while (j < b.Count) result.Add(b[j++]);
			return result;
		}
		public static List<T> MergeSort<T>(Stack<T> input) where T : IComparable<T>
		{
			Stack<List<T>> result = new();
			result.Push(new List<T>() { input.Pop() });
			result.Push(new List<T>() { input.Pop() });
			while (input.Count > 0)
			{
				var top = result.Pop();
				if (result.Count == 1 || top.Count != result.Peek().Count)
				{
					result.Push(top);
					result.Push(new List<T>() { input.Pop() });
					if (input.Count == 0) break;
					result.Push(new List<T>() { input.Pop() });
				}
				else
				{
					result.Push(Merge(result.Pop(), result.Pop()));
				}
			}
			while (result.Count > 1)
			{
				result.Push(Merge(result.Pop(), result.Pop()));
			}
			return result.Pop();
		}
		static void Main(string[] args)
		{
			Stack<int> input = new();
			Random rand = new();
			for (int i = 0; i < 1024 * 256; i++) input.Push(rand.Next(-1_000_000, 1_000_000));
			var watch = Stopwatch.StartNew();
			//Console.WriteLine("input :" + string.Join(' ', input));
			var result = MergeSort(input);
			watch.Stop();
			Console.WriteLine(watch.Elapsed);
			//Console.WriteLine("output :" + string.Join(' ', result));
		}
	}
}