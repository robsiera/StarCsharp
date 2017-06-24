/*
 * This is free and unencumbered software released into the public domain.
 * 
 * Anyone is free to copy, modify, publish, use, compile, sell, or
 * distribute this software, either in source code form or as a compiled
 * binary, for any purpose, commercial or non-commercial, and by any
 * means.
 * 
 * In jurisdictions that recognize copyright laws, the author or authors
 * of this software dedicate any and all copyright interest in the
 * software to the public domain. We make this dedication for the benefit
 * of the public at large and to the detriment of our heirs and
 * successors. We intend this dedication to be an overt act of
 * relinquishment in perpetuity of all present and future rights to this
 * software under copyright law.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
 * MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
 * IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
 * OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
 * ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
 * OTHER DEALINGS IN THE SOFTWARE.
 * 
 * For more information, please refer to <http://unlicense.org/>
 *//////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.IO;
using XGen.io.Star.Core;
using XGen.io.Star.Core.Simulation;
using XGen.io.Star.Core.Util;
using Action = XGen.io.Star.Core.Action;
using Exception = XGen.io.Star.Core.Util.Exception;
using Type = XGen.io.Star.Core.Type;

namespace XGen.io.Star.Samples.Factorial
{
    public class Factorial : Component
    {
        private readonly FactorialType _fact;
        private readonly FactorialBehavior _behavior;
        private readonly MultiplyAction _multiply;
        private readonly InitializeAction _initialize;
        private readonly State _mult;
        private readonly State _def;
        private readonly ForbiddenState _forbidden;
        private readonly DesiredState _desired;
        private readonly Trace trace;

        public Factorial(FactorialBehavior behavior) : base()
        {
            this.Add(behavior);
        }

        public Factorial(int i)
        //throws Exception 
        {
            trace = new Trace("fact_trace");

            _fact = new FactorialType(i);
            EqualsOneRange defaultRange = new EqualsOneRange();
            GreaterThanOneRange multRange = new GreaterThanOneRange();
            LessThanOneRange fRange = new LessThanOneRange();
            EqualsTwoRange desiredRange = new EqualsTwoRange();

            _multiply = new MultiplyAction(_fact);
            _initialize = new InitializeAction(_fact);
            _multiply.SetTrace(trace);
            _initialize.SetTrace(trace);
            _multiply.SetId("multiply");
            _initialize.SetId("initialize");

            _mult = new State(_multiply, true);
            _def = new State(_initialize);
            _mult.SetId("mult");
            _def.SetId("default");
            _forbidden = new ForbiddenState();
            _desired = new DesiredState();
            _forbidden.SetId("f1");
            _desired.SetId("d1");
            _mult.SetTrace(trace);
            _def.SetTrace(trace);
            _forbidden.SetTrace(trace);
            _desired.SetTrace(trace);

            _fact.AddRange(_mult, multRange);
            _fact.AddRange(_def, defaultRange);
            _fact.AddRange(_forbidden, fRange);
            _fact.AddRange(_desired, desiredRange);

            _behavior = new FactorialBehavior(_fact);
            _behavior.Add(_mult);
            _behavior.Add(_def);
            this.Add(_behavior);
            Element.SetWarnOnErrroneousAction(true);

            State s = _fact.CurrentState();

            if (HasTrace())
                _trace.AddTrace(s);
        }

        public override void Start()
        {
            _behavior.Act(_multiply, null);
        }

        public override void Start(IDictionary<string, object> inputs)
        {
            _behavior.Act(_initialize, inputs);
            //start();
        }

        public void Display()
        {
            _fact.Display();
            var sw = new StreamWriter(Console.OpenStandardOutput());
            trace.Dump("activity", sw);
            trace.Dump("state", sw);
            trace.Clear();
        }

        public override void Act(Action a)
        {
            _behavior.Act(a, null);
        }

        public override bool DefaultState()
        {
            return _fact.CurrentState() != _def;
        }

        public static void Main(string[] args)
        {
            try
            {
                Factorial comp = new Factorial(10);
                //automatic transition
                comp.Start();
                //system triggered transition
                //while(!comp.defaultState()) {
                //	  comp.act(multiply);
                //}
                comp.Display();

                IDictionary<string, object> inputs = new Dictionary<string, object>();
                inputs.Add("f", 1);
                inputs.Add("i", 5);

                comp.Start(inputs);
                comp.Display();

                Factorial walkComp = new Factorial(4);
                walkComp.Walk(10);
                walkComp.DisplayWalk();

                //Create an individual walk (relies on Component choosing which Type to walk)
                //Walk<FactorialType> walkThe = new Walk<FactorialType>(walkComp.fact);
                //walkThe.walk();
                Console.ReadKey();
            }
            catch (Exception e)
            {
                // TODO Auto-generated catch block
                System.Console.WriteLine(e.StackTrace);
            }
        }

        protected void DisplayWalk()
        {
            IDictionary<Behavior, Walk<Type>> walks = this.GetWalks();
            var sw = new StreamWriter(Console.OpenStandardOutput());
            if (walks.ContainsKey(_behavior))//TODO Check this
                walks[_behavior].Display("activity", sw);
            Console.SetOut(sw);
        }

        public override string Tick(string key)
        {
            // TODO Auto-generated method stub
            return null;
        }
    }
}