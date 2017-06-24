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
    public abstract class Behavior : Element
    {
        protected List<State> _states;
        protected State _currentState;
        protected List<State> _endStates;
        protected Component _component;

        public Behavior() : base()
        {
            // This is not possible at this point this the 
            // behavior is not completely built

            //TODO validate that it's no problem to comment this line
            // this.setCurrentState(t.currentState());
        }

        public void Add(State s)
        {
            if (_states == null)
                _states = new List<State>();
            s.SetBehavior(this);
            _states.Add(s);
        }

        public void AddEndState(State s)
        {
            if (_endStates == null)
                _endStates = new List<State>();
            this.Add(s);
            _endStates.Add(s);
        }


        public void SetCurrentState(State s)
        {
            _currentState = s;
        }

        public State GetCurrentState()
        {
            return _currentState;
        }

        public void SetComponent(Component c)
        {
            this._component = c;
        }

        public void Act(Action a, IDictionary<string, object> input)
        {
            if (_currentState != null)
            {
                if (_currentState.Allows(a))
                {
                    Type t = a.GetActionType();
                    if (input == null)
                        input = t.GetInstance();
                    a.eval(input);
                    a.Present();
                    this.SetCurrentState(t.CurrentState());
                    _currentState.IsCurrentState();
                    this.Finalize(_currentState, t);
                }
                else
                {
                    if (_warnOnErroneousAction)
                    {
                        if (HasTrace())
                            _trace.AddTrace(new Property("ActionNotAllowedException()", "Behavior::act(" + a.GetId() + ")"));
                        System.Console.WriteLine("============= " + a.GetId() + " is not allowed =================");
                    }
                    else
                        throw new Exception("ActionNotAllowedException() Behavior::act(" + a.GetId() + ")");
                }
            }
        }

        public Behavior NextAction(string requestedAction, State s, IDictionary<string, object> inputs)
        {
            if (s != null)
            {
                if (HasTrace())
                    _trace.AddTrace(s);
                // Check for automatic actions
                // there shouldn't be any
                this.SetCurrentState(s);
                s.IsCurrentState();
                Action action = s.GetActionForId(requestedAction);
                if (action != null)
                    this.Act(action, inputs);
            }
            return this;
        }

        public bool Finalize(State currentState, Type type)
        {
            if (_endStates != null)
            {
                if (_endStates.Contains(currentState))
                {
                    System.Console.WriteLine("Finalizing >>>>>>>>>> ");
                    this._component.Finalize(type.GetInstance());
                    return true;
                }
            }
            return false;
        }
    }
}