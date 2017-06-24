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
using System.IO;

namespace XGen.Star.Scm.Util
{

	public class Trace : Element 
	{
	    private IList<Element> _trace;
		protected static Trace _default;
		protected ISet<string> _outputs;

	    private readonly string _priorTrace;
	    private string _updatedTrace;
		
		public Trace() : base()
		{
			_trace = new List<Element>();
			if (_default == null) _default = this;
		}
		
		public static void SetDefaultTrace(Trace t) 
		{
			_default = t;
		}
			
		public Trace(string id) : this()
		{
			this.SetId(id);;
		}
		
		public Trace(string id, string priorTrace) : this(id)
		{
			_priorTrace = priorTrace;
		}
		
		public void trace(Element e) 
		{
			_trace.Add(e);
		}
		
		public static void TraceDefault(Element e) 
		{
			_default.trace(e);
		}
		
		public void Dump() 
		{
			Dump(Console.Out);
		}

        private void Dump(TextWriter writer)
        {
			if (_priorTrace != null) 
				writer.WriteLine(_priorTrace);
			foreach (var e in _trace) 
				writer.WriteLine(e.ToString());
        }
		
		public void Dump(string diagramType, TextWriter p) 
		{
			if (diagramType.ToLower().Equals("activity")) 
			{
				_updatedTrace = "";
				p.WriteLine("@startuml\n\n|Action|\nstart");
				if (_priorTrace != null) 
				{
					p.WriteLine(_priorTrace);
				}
				Element moveForward = null;
				foreach (Element e in _trace) 
				{
					if (moveForward != null) 
					{
						_updatedTrace += e.ToActivity();
						p.WriteLine(e.ToActivity());
						_updatedTrace += moveForward.ToActivity();
						p.WriteLine(moveForward.ToActivity());
						moveForward = null;
					} 
					else 
					{
						if (e is DesiredState || e is ForbiddenState) 
							moveForward = e;
						else 
						{ 
							_updatedTrace += e.ToActivity();
							p.WriteLine(e.ToActivity());
						}
					}
				}
				p.WriteLine("stop\n\n@enduml\n\n\n");
			}
			
			if (diagramType.ToLower().Equals("state")) 
			{
				_outputs = new HashSet<string>();
				p.WriteLine("@startuml\n\n");
				bool started = false;
				State previousState = null;
				var i = _trace.GetEnumerator();
				while (i.MoveNext()) 
				{
					Element e = i.Current;
					if (e is Action) 
					{
						var a = (Action)e;
						i.MoveNext();
						var s = i.Current;
						if (s is State) 
						{
							var st = (State)s;
							if (!started) 
							{
								p.WriteLine("[*]->"+s.GetId()+":"+a.GetId());
								started = true;
							} 
							else 
							{
								if (previousState!=null) 
								{
									var output = previousState.GetId()+"->"+st.ToState()+":"+a.ToState();
									if (!_outputs.Contains(output)) 
									{
										p.WriteLine(output);
										_outputs.Add(output);
									}
								}
							}
							previousState = st;
						}
					} 
					else 
						e.ToState();
				}
                if (previousState!=null)
				    p.WriteLine(previousState.GetId()+"->[*]\n\n@enduml\n");
			}

			if (diagramType.ToLower().Equals("other")) 
			{
				p.WriteLine("@startuml\n\n|Action|\nstart");
				p.WriteLine("stop\n\n@enduml\n\n\n");
			}

		}
		
		public string CurrentTrace() 
		{
			return _priorTrace + _updatedTrace;
		}
		
		public string CurrentActivityDiagram() 
		{
			string diagram  = "@startuml\n\n|Action|\nstart";
			diagram += _priorTrace + _updatedTrace;
			diagram += "stop\n\n@enduml\n\n\n";
			return diagram;
		}

		public void Clear() {
			_trace = new List<Element>();
		}
	}
}