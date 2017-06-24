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

using XGen.io.Star.Core;

namespace XGen.io.Star.Core.Simulation
{
	public class Sequence<T> : Element where T : Type
	{
		protected List<Step<T>> _sequence;
	    private bool _isRepeating;
	    private readonly bool _strict;
	    private bool _reached;
		
		public Sequence() : base()
		{
			_sequence = new List<Step<T>>();
			_isRepeating = false;
			_strict = false;
		}

		public Sequence(bool repeating, bool strict) : this()
		{
			_isRepeating = repeating;
			_strict = strict;
		}

		public Sequence<T> AddStep(Step<T> s) 
		{
			_sequence.Add(s);
			return this;
		}
		
		public Sequence<T> Reset() 
		{
			_sequence = new List<Step<T>>();
			return this;
		}
		
		public bool MatchWalk(Walk<T> w) 
		{
			List<Step<T>> path = w._Walk;
			var i = _sequence.GetEnumerator();
			var j = path.GetEnumerator();
			_reached = true;
			int lastIndex = 0;
			int index = 0;
			while (i.MoveNext()) 
			{
				Step<T> s0 = i.Current;
				bool cont = false;
				while (j.MoveNext()) 
				{
					Step<T> sp = j.Current;
					index++;
					if (sp.Equals(s0)) 
					{
						if (lastIndex < index) 
						{
							if (_strict) 
							{
								if (index - lastIndex == 1) 
								{
									lastIndex = index;
									cont = true;
								} 
								else 
									cont = false;
							} 
							else 
							{
								lastIndex = index;
								cont = true;
							}
							break;
						}
					}
				}
				if (!cont) 
				{
					_reached = false;
					break;
				}
			}
			return _reached;
		}
	}
}
