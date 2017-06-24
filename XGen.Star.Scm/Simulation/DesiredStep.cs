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

namespace XGen.Star.Scm.Simulation
{
	public class DesiredStep<T ,TYpe> : Step<T> where T : Type
	{

		protected bool _reached;

		public DesiredStep(State from, Action action, State to, T results):base(from, action, to, results)
        //throws Exception
        {
			this._reached = false;
		}

		public DesiredStep(string id, State from, Action action, State to, T results):this(from, action, to, results)
		//		throws Exception
        {
			SetId(id);
		}

		public bool SetReached() {
			if (HasTrace()) AddTrace(this);
			_reached = true;
			return _reached;
		}
		
		public bool IsReached() {
			return _reached;
		}

		public bool IsDesired(Step<T> s) {
			if (this._from.Equals(s._from) && this.To.Equals(s.To)) {
				bool strict = this.Action != null;
				if (strict) {
					if (this.Action.Equals(s.Action)) {
						bool strictest = this._targetView != null;
						if (strictest) {
							if (this._targetView.ViewEquals(s._targetView)) return SetReached();
						} else return SetReached();
					}
				} else {
					return SetReached();
				}
			}
			return false;
		}
		
		public override string ToActivity() {
			return "note left \ndesired step ["+GetId()+"]\nend note";
		}

	}
}
