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
using System.IO;

namespace XGen.Star.Scm.Simulation
{
	public class Walk<T> : Element where T : Type
	{
	    private readonly List<Step<T>> _walk;
        public List<Step<T>> _Walk => _walk;
	    private readonly int _depth = 0;
	    private Step<T> _currentStep;
	    private readonly T _initialView;
	    private int _level;
		
		public Walk(T t, int depth)
		{
			_depth = depth;
			_level = 0;
			_walk = new List<Step<T>>();
			_currentStep = null;
			_initialView = (T)t.Duplicate();
		}
		
		public Walk(T t) : this(t, 10)
		{
		}
		
		public State Act(Action action, Type view)
		{
			if (view == null) view = _initialView;
			action.eval(view.GetInstance());
			if (view.Present(action.Returns())) 
				return view.CurrentState();

			throw new Util.Exception("InvalidValueException", $"Walk::Act({action.GetId()})");
		}
		
		public Step<T> Step(Action a, State startState , T view)
		{
			State newState = this.Act(a, view);
			return new Step<T>(startState,a,newState,view);
		}
			
		public Walk<T> StartWalking()
		{
			if (_initialView == null) 
				throw new Util.Exception("NotInitializedException","Walk::walk");
			State startState = _initialView.CurrentState();
			bool first = true;
			Step<T> firstStep = null;
			foreach(Action a in startState.GetActions()) 
			{
				T branch = _initialView;
				if (!first) 
				{
					branch = (T)branch.Duplicate();
					Step<T> newStep = Step(a,startState,branch);
					firstStep.AddNextStep(newStep);
					//we need to create a new walk path
					Walk<T> newWalkPath = new Walk<T>(branch, _depth - _level);
					newStep.SetPath(newWalkPath);
				} 
				else 
				{
					firstStep = Step(a,startState,branch);
					this.Add(firstStep);
					first = false;
				}
			}
			if (!HasReachedSteadyState()) 
			{
				this.NextLevel();
				if (this._level < this._depth) 
					this.StartWalking();//Continue Walking the "main path" on _initialView
			}
			return this;
		}
		
		public Walk<T> WalkFromState(State state, int depth)
		{
			//TODO
			return this;
		}
		
		public Walk<T> Add(Step<T> s) 
		{
			_walk.Add(s);
			_currentStep = s;
			return this;
		}
		
		public Walk<T> NextLevel() 
		{
			_level++;
			return this;
		}
		
		public bool HasReachedDepth() {
			return _level>_depth;
		}
		
		public bool HasReachedSteadyState() 
		{
			bool reached = false;
			int l = _walk.Count;
			if (l>=2) {
				Step<T> s0 = _walk[l-2];
				Step<T> s1 = _walk[l-1];
				if (s0.Equals(s1)) {
					reached = true;
				}
			}		
			return reached;
		}
		
		public void Display(string diagramType, StreamWriter p) 
		{
			int swimLanes = 0;
			if (diagramType.ToLower().Equals("activity")) 
			{
				p.WriteLine("@startuml\n\n|Action|\nstart");
				foreach(Step<T> s in _walk) 
				{
					Action a = s.Action;
					State to = s.To;
					//State from = s._from;
					p.WriteLine(a.ToActivity());	
					p.WriteLine(to.ToActivity());
					if (s.NewPath != null) 
					{
						//TODO
					}
				}
				p.WriteLine("stop\n\n@enduml\n\n\n");
			}

		}
	}
}