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

namespace XGen.Star.Scm
{
    public abstract class Type : Element
    {
        protected IDictionary<string, object> _instance;
        private IDictionary<string, object> _instanceCopy;
        private IDictionary<State, Range> _stateMap;
        private readonly IDictionary<Relation, Type> _relations;

        public Type() : base()
        {
            _instance = new Dictionary<string, object>();
            _relations = new Dictionary<Relation, Type>();
        }

        public Type(Type t) : base()
        {
            if (t._instance != null)
                _instance = new Dictionary<string, object>(t._instance);
            if (t._instanceCopy != null)
                _instanceCopy = new Dictionary<string, object>(t._instanceCopy);
            if (t._stateMap != null)
                this._stateMap = new Dictionary<State, Range>(t._stateMap);
            this._id = t.GetId();
            this._trace = t._trace;
        }

        public virtual Type Duplicate()
        {
            throw new Util.Exception("AbstractTypeDuplicationException", "This method should not be called");
        }

        public void SetInstance(IDictionary<string, object> i)
        {
            _instance = i;
        }

        public IDictionary<string, object> GetInstance()
        {
            return _instance;
        }

        // synchronized
        public bool Present(IDictionary<string, object> request)
        {
            bool ret = true;
            if (_instance == null)
                _instance = request;
            else
            {
                Copy();
                foreach (string key in request.Keys)
                    if (!this.Present(key, request[key]))
                        ret = false;

                if (!ret)
                {
                    Rollback();
                    throw new Util.Exception("RollBackException", "Type::present");
                }
                else
                {
                    if (Sync())
                        Persist();
                }
            }
            return ret;
        }

        protected abstract bool Persist();
        protected abstract bool Sync();

        // synchronizer
        private void Rollback()
        {
            if (_instanceCopy != null)
            {
                _instance = null;
                _instance = new Dictionary<string, object>(_instanceCopy);
            }
            else
                throw new Util.Exception("NoDataException", "Type::rollback");
        }

        //synchronized
        private void Copy()
        {
            _instanceCopy = null;
            if (_instance != null) _instanceCopy = new Dictionary<string, object>(_instance);
        }

        public State CurrentState()
        {
            State currentState = null;
            bool found = false;
            foreach (State s in _stateMap.Keys)
            {
                if (IsInRange(_stateMap[s]))
                {
                    if (s is DesiredState)
                        ((DesiredState)s).SetReached();
                    else
                    {
                        if (!found)
                        {
                            found = true;
                            currentState = s;
                            if (HasTrace())
                                AddTrace(s);
                            if (s is ForbiddenState)
                            {
                                ((ForbiddenState)s).SetReached();
                                throw new Util.Exception("ForbiddenStateException", "Type::currentState");
                            }
                        }
                        else
                        {
                            //notUnique 
                            throw new Util.Exception("DualStateException", "Type::currentState");
                        }
                    }

                }
            }
            return currentState;
        }

        protected bool IsInRange(Range r)
        {
            return r.Eval(_instance);
        }

        public Range AddRange(State s, Range r)
        {
            if (_stateMap == null)
                _stateMap = new Dictionary<State, Range>();
            _stateMap.Add(s, r);
            return r;
        }

        public bool Present(string key, object value)
        {
            if (this.Validate(key, value))
            {
                _instance.Add(key, value);
                return true;
            }
            return false;
        }

        protected bool Validate(string key, object value)
        {
            return true;
        }

        public void Display()
        {
            foreach (string prop in _instance.Keys)
                Display($"{prop}\t = {_instance[prop]}");
        }

        public abstract string Display(string s);

        public bool ViewEquals(Type view)
        {
            bool equals = true;
            foreach (string key in _instance.Keys)
            {
                object o1 = _instance[key];
                object o2 = view._instance[key];
                if (!o1.Equals(o2))
                    equals = false;
            }
            return equals;
        }

        public void Add(Relation r, Type toType)
        {
            _relations.Add(r, toType);
        }

        public IDictionary<Relation, Type> GetRelations()
        {
            return _relations;
        }

        public void SetRanges(IDictionary<State, Range> stateMap)
        {
            foreach (State s in stateMap.Keys)
                this.AddRange(s, stateMap[s]);
        }
    }
}