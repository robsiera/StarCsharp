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

namespace XGen.io.Star.Core.Util
{
	public class Exception : System.Exception 
	{
		private static long _serialVersionUid = -7067140721605137477L;
	    private readonly string _exception;
	    private readonly string _source;
	    private object _context;
		
		public Exception() : base()
		{
		}
		
		public Exception(string message) : base(message)
		{
		}
		
		public Exception(string e, string s) : base()
		{
			_exception = e;
			_source = s;
			Property p = new Property("exception::"+_exception,_source);
			Trace.TraceDefault(p);	
		}
		
		public string exception() 
		{
			return _exception;
		}

		public string source() 
		{
			return _source;
		}
		
		public void SetContext(object c) 
		{
			_context = c;
		}
		
		public object Context() 
		{
			return _context;
		}
	}
}