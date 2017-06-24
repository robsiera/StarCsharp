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
using System;
using System.Collections.Generic;

namespace XGen.io.Star.Core.Util
{
	public class BoundInterval : Range 
	{
	    private readonly Interval _boundedRange;
	    private readonly string _boundMin;
	    private readonly string _boundMax;
		
		public BoundInterval(Interval range, string min, string max) 
		{
			_boundedRange = range;
			_boundMin = min;
			_boundMax = max;
		}
		
		public bool Eval(IDictionary<string, object> instance) 
		{
			if (_boundMin != null) 
				_boundedRange.Min = decimal.Parse((string)instance[_boundMin]);
			if (_boundMax != null) 
				_boundedRange.Max = decimal.Parse((string)instance[_boundMax]);
			return _boundedRange.Eval(instance);
		}

	}
}
