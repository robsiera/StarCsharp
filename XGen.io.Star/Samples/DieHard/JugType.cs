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

using System;

namespace XGen.io.Star.Sample.DieHard
{
	public class JugType : Type 
	{
		public static  Int16 bigSize = 5;
		public static  Int16 smallSize = 3;
		
		Int16 _big;
		Int16 _small;
		
		public JugType(Int16 big, Int16 small):base() 
		{
			_big = big;
			_small = small;
			_instance.put("big", _big);
			_instance.put("small", _small);
		}

		public JugType(JugType t)
		{
			super((Type)t);
			_big = t.getBig();
			_small = t.getSmall();
		}

		public Int16 getBig() 
		{
			return _big;
		}
		
		public Int16 getSmall() 
		{
			return _small;
		}

		public override Type duplicate()
		{
			return new JugType(this);
		}

		protected override bool sync() 
		{
			_big = (Int16)_instance.get("big");
			_small = (Int16)_instance.get("small");
			return true;
		}

		protected override bool persist() 
		{
			return true;
		}

		public override String display(String s) 
		{
			System.Console.WriteLine(s);
			return s;
		}
	}
}