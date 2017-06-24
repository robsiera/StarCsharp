/*
 * Copyright 2017 Jean-Jacques Dubray jdubray@xgen.io
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *  http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * 
 *////////////////////////////////////////////////////////////////////////////

using System.Collections.Generic;

namespace XGen.Star.Scm.Util
{
	public class Interval : Range 
	{
	    private readonly string _property;
		public decimal Min { get; set; }
		public decimal Max {get; set; }
	    private readonly bool _equals;
		
		public Interval(string property, decimal min, decimal max, bool equals) 
		{
			_property = property;
			Min = min;
			Max = max;
			_equals = equals;
		}
		
		public new bool Eval(IDictionary<string, object> instance) 
		{
			return Compare((decimal)instance[_property]);
		}
		
		private bool Compare(decimal input) 
		{
			if (_equals) 
			{
				if (input >= Min && input <= Max) 
					return true;
			} 
			else 
			{
				if (input >  Min && input <  Max) 
					return true;
			}
			return false;
		}
	}
}
