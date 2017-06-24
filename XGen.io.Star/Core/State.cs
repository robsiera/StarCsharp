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
    public class State : Element
    {
        private List<Action> _allowedActions;
        private Behavior _behavior;
        private Action _automaticAction;

        public State() : base()
        {
            this.SetUid();
        }

        public State(string id) : this()
        {
            this.SetId(id);
        }

        public State(Action m) : this()
        {
            this.Add(m);
        }

        public State(Action m, bool automatic) : this()
        {
            this.Add(m, automatic);
        }

        public new Element SetId(string id)
        {
            _id = id;
            return this;
        }

        public bool Add(Action a, bool automatic)
        {
            if (_allowedActions == null)
                _allowedActions = new List<Action>();
            if (automatic)
                _automaticAction = a;
            _allowedActions.Add(a);
            return true;
        }

        public bool Add(Action a)
        {
            if (_allowedActions == null)
                _allowedActions = new List<Action>();
            _allowedActions.Add(a);
            return true;
        }

        public List<Action> GetActions()
        {
            return _allowedActions;
        }

        public Action GetActionForId(string id)
        {
            foreach (Action a in _allowedActions)
                if (a.GetId().Equals(id))
                    return a;
            return null;
        }

        public bool Allows(Action action)
        {
            foreach (Action allowedAction in _allowedActions)
                if (allowedAction.Equals(action))
                    return true;
            return false;
        }

        public void SetBehavior(Behavior b)
        {
            _behavior = b;
        }

        // synchronized
        public State IsCurrentState()
        {
            if (HasTrace())
                AddTrace(this);
            if (_automaticAction != null)
                _behavior.Act(_automaticAction, null);
            return this;
        }

        public override string ToString()
        {
            return GetId();
        }

        public override string ToActivity()
        {
            return $"|State|\n#CCCCCC:{_id};";
        }

        public override string ToState()
        {
            return _id;
        }

        public Type GetActionType()
        {
            if (_allowedActions == null || _allowedActions.Count == 0)
                throw new Util.Exception("NoActionException", "Type::GetActionType");
            return _allowedActions[0].GetActionType();
        }
    }
}