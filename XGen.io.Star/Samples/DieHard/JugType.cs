/*
 * This is free and unencumbered software released into the public domain.
 * 
 * Anyone is free to copy, modify, publish, use, compile, sell, or
 * distribute this software, either in source code form or as a compiled
 * binary, for any purpose, commercial or non-commercial, and by any
 * means.
 * 
 * In jurisdictions that recognize copyright laws, the author or authors
 * of this software dedicate any and all copyright interest in the
 * software to the public domain. We make this dedication for the benefit
 * of the public at large and to the detriment of our heirs and
 * successors. We intend this dedication to be an overt act of
 * relinquishment in perpetuity of all present and future rights to this
 * software under copyright law.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
 * MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
 * IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
 * OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
 * ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
 * OTHER DEALINGS IN THE SOFTWARE.
 * 
 * For more information, please refer to <http://unlicense.org/>
 *//////////////////////////////////////////////////////////////////////////


using XGen.io.Star.Core;

namespace XGen.io.Star.Samples.DieHard
{
	public class JugType : Type 
	{
		public static int _bigSize = 5;
		public static int _smallSize = 3;

	    private int _big;
	    private int _small;
		
		public JugType(int big, int small):base() 
		{
			_big = big;
			_small = small;
			_instance.Add("big", _big);
			_instance.Add("small", _small);
		}

		public JugType(JugType t):base(t)
		{
			_big = t.GetBig();
			_small = t.GetSmall();
		}

		public int GetBig() 
		{
			return _big;
		}
		
		public int GetSmall() 
		{
			return _small;
		}

		public override Type Duplicate()
		{
			return new JugType(this);
		}

		protected override bool Sync() 
		{
			_big = (short)_instance["big"];
			_small = (short)_instance["small"];
			return true;
		}

		protected override bool Persist() 
		{
			return true;
		}

		public override string Display(string s) 
		{
			System.Console.WriteLine(s);
			return s;
		}
	}
}