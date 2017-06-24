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

using Type = XGen.io.Star.Core.Type;

namespace XGen.io.Star.Samples.Factorial
{
	public class FactorialType : Type 
	{
	    private int _f;
	    private int _i;
		
		public FactorialType(int i0) :base()
		{
			_f = 1;
			_i = i0;
			_instance.Add("f", _f);
			_instance.Add("i", _i);
		}
		
		public FactorialType(FactorialType t) :base(t)
		{
			_f = t._f;
			_i = t._i;
			_instance.Add("f", _f);
			_instance.Add("i", _i);
		}

		public override Type Duplicate()
		{
			return new FactorialType(this);
		}
		
		protected override bool Sync() 
		{
			_f = (int)_instance["f"];
			_i = (int)_instance["i"];
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