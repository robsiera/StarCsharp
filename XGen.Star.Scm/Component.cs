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
using XGen.Star.Scm.Simulation;

namespace XGen.Star.Scm
{
    public abstract class Component : Element
    {
        protected List<Behavior> _behaviors;
        protected IDictionary<Behavior, Walk<Type>> _walks;

        public void Add(Behavior b)
        {
            if (_behaviors == null)
                _behaviors = new List<Behavior>();
            _behaviors.Add(b);
        }

        public abstract void Start();
        public abstract void Start(IDictionary<string, object> inputs);
        public abstract void Act(Action a);
        public abstract bool DefaultState();
        public abstract string Tick(string key);

        public Component Walk()
        {
            return Walk(10);
        }

        public Component Walk(int depth)
        {
            _walks = new Dictionary<Behavior, Walk<Type>>();
            foreach (Behavior b in _behaviors)
            {
                Action[] actions = b.GetCurrentState()?.GetActions().ToArray();
                if (actions?.Length > 0)
                {
                    Action a = actions[0];
                    if (a.GetActionType() != null)
                    {
                        Walk<Type> walkThe = new Walk<Type>(a.GetActionType(), depth);
                        walkThe.StartWalking();
                        _walks.Add(b, walkThe);
                    }
                }
            }
            return this;
        }

        public IDictionary<Behavior, Walk<Type>> GetWalks()
        {
            return _walks;
        }

        public bool Finalize(IDictionary<string, object> inputs)
        {
            return true;
        }

        public State Rehydrate(string instanceId)
        {
            return null;
        }
    }
}