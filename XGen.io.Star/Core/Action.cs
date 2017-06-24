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
using System.Collections;
using System.Collections.Generic;

namespace XGen.io.Star.Core
{
	public abstract class Action : Element 
	{
		protected IDictionary<string,object> _returns;
	    private Type _target;
	    private Guard _guard;

		public IDictionary<string,object> Returns() 
		{
			return _returns;
		}
		
		public object Set(string key, object value) 
		{
			return _returns[key] = value;
		}
		
		public object Get(string key) 
		{
			return _returns[key];
		}
		
		public void SetTargetType(Type t) 
		{
			_target = t;
		}
		
		public Type GetActionType() 
		{
			return _target;
		}
		
		public void SetGuard(Guard g) 
		{
			_guard = g;
		}
		
		public Guard GetGuard() 
		{
			return _guard;
		}
		
		public IDictionary<string,object> eval(IDictionary<string,object> input)
		{
            IDictionary<string, object> returns =new Dictionary<string, object>();
			if (_guard != null) 
			{
				if (_guard.Eval(input)) 
				{
					if (_warnOnFailedGuard) 
						_trace.AddTrace( new Property("GuardFailureException","Action::eval("+this.GetId()+")"));
					else 
						throw new Util.Exception("GuardException","Action::eval");					
				}
			}
			foreach (string key in input.Keys) 
				returns.Add(key, input[key]);
			
			return returns;
		}
		
		public void Present() { 
			if (_target != null)
			{
				if (HasTrace()) 
					AddTrace(this);
				_target.Present(_returns);
			}
		}

		public override string ToString() 
		{
			return GetId();
		}

		public override string ToActivity() 
		{
			return $"|Action|\n:{_id};";
		}

		public override string ToState() 
		{
			return _id;
		}
	}
}