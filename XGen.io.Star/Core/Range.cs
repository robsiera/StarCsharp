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

namespace XGen.io.Star.Core
{
	public abstract class Range : Element 
	{
		protected bool _forbidden;

	    protected Range() 
		{
			_forbidden = false;
		}

	    protected Range(bool f) 
		{
			_forbidden = f;
		}
		
		public bool IsForbidden() 
		{
			return _forbidden;
		}
		
		public bool Eval(IDictionary<string, object> instance) //todo: 
		{
			return false;
		}
	}
}