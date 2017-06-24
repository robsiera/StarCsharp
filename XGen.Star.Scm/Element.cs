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

namespace XGen.Star.Scm
{
	public abstract class Element 
	{
		protected static bool _warnOnErroneousAction = false;
		protected static bool _warnOnFailedGuard = false;
		protected string _id;
		protected Guid _uid;
	    protected Util.Trace _trace;
		
		public static void SetWarnOnErrroneousAction(bool b) 
		{
			_warnOnErroneousAction = b;
		}

		public static void SetWarnOnFailedGuard(bool b) 
		{
			_warnOnFailedGuard = b;
		}

		public Element() 
		{
			this.SetUid();
		}
		
		public Element SetId(string id) 
		{
			this._id = id;
			return this;
		}
		
		public string GetId() 
		{
			return this._id;
		}
		
		public Element SetUid(Guid uid) 
		{
			this._uid = uid;
			return this;
		}
		
		public Guid GetUid() 
		{
			return this._uid;
		}
		
		public Element SetUid() 
		{
			this.SetUid(Guid.NewGuid());
			return this;
		}
		
		public override bool Equals(object other)
		{
			if (other == null) 
				return false;
			if (other == this) 
				return true;
			if (!(other is Element))
				return false;
			Element otherElement = (Element)other;
			if (this._uid.Equals(otherElement._uid)) 
			    return true;
			return false;
		}

		public override int GetHashCode()
		{
			return _uid.GetHashCode();
		}

		public Element SetTrace(Util.Trace trace) 
		{
			_trace = trace;
			return this;
		}
		
		public Element AddTrace(Element e) 
		{
			if (HasTrace()) _trace.trace(e);
			return this;
		}
		
		public bool HasTrace() 
		{
			return _trace != null;
		}

		public virtual string ToActivity() 
		{
			return $"note left \n{GetId()}\nend note";
		}

		public virtual string ToState() 
		{
			return $"note left \n{GetId()}\nend note";
		}
	}
}