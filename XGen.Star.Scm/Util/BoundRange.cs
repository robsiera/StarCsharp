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
	public class BoundRange : Range 
	{
	    private readonly SimpleRange _boundedRange;
	    private readonly string _boundProperty;
		
		public BoundRange(SimpleRange range, string property) 
		{
			_boundedRange = range;
			_boundProperty = property;
		}
		
		public bool Eval(IDictionary<string, object> instance) 
		{
			if (_boundProperty != null) 
				_boundedRange.Value = decimal.Parse((string)instance[_boundProperty]);
			return _boundedRange.Eval(instance);
		}
	}
}
