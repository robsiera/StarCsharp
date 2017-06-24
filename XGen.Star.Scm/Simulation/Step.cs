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

namespace XGen.Star.Scm.Simulation
{
	public class Step<T> : Element where T : Type
	{
		public State _from;
		internal Action Action { get; set; }
		internal State To { get; set; }
        public Type _targetView;
		protected List<Step<T>> _nextSteps;
		internal Walk<T> NewPath {get; set; }
		
		public Step(State from, Action action, State to, T results)
		{
			_from = from;
			Action = action;
			To = to;
			_targetView = results.Duplicate();
			NewPath = null;
		}
		
		public bool Equals(Step<T> p) 
		{
			return To.Equals(p.To) && this._targetView.ViewEquals(p._targetView);
		}
		

		public void Snap()
		{
			_targetView = Action.GetActionType().Duplicate();
		}
		
		public Step<T> AddNextStep(Step<T> s) 
		{
			if (_nextSteps == null) 
				_nextSteps = new List<Step<T>>();		
			_nextSteps.Add(s);
			return this;
		}
		
		public Step<T> SetPath(Walk<T> w) 
		{
			NewPath = w;
			return this;
		}

        protected bool Persist()
        {
            throw new NotImplementedException();
        }

        protected bool Sync()
        {
            throw new NotImplementedException();
        }

        public string Display(string s)
        {
            throw new NotImplementedException();
        }
    }
}
