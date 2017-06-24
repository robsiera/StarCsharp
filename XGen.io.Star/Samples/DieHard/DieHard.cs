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

using System.Collections.Generic;
using XGen.Star.Scm;
using XGen.Star.Scm.Util;

namespace XGen.io.Star.Samples.DieHard
{
    public class DieHard : Component
    {
        private InitializeAction _initialize;
        private readonly JugType _jugs;
        private readonly JugBehavior _behavior;
        private readonly FillBigAction _fillBig;
        private readonly FillSmallAction _fillSmall;
        private readonly EmptyBigAction _emptyBig;
        private readonly EmptySmallAction _emptySmall;
        private readonly Big2SmallAction _big2Small;
        private readonly Small2BigAction _small2Big;

        private readonly State _emptyBigEmptySmall;
        private readonly State _fullBigFullSmall;
        private readonly State _fullBigEmptySmall;
        private readonly State _emptyBigFullSmall;
        private readonly State _partialBigEmptySmall;
        private readonly State _partialBigFullSmall;
        private readonly State _fullBigPartialSmall;
        private readonly State _emptyBigPartialSmall;
        private readonly State _partialBigPartialSmall;

        private readonly ForbiddenState _forbidden;
        private readonly DesiredState _desired;
        private readonly Trace trace;

        public DieHard(JugBehavior behavior)
        {
            this.Add(behavior);
        }

        public DieHard(int big, int small)
        {

            trace = new Trace("fact_trace");

            _jugs = new JugType(big, small);
            EmptyBigEmptySmallRange emptyBigEmptySmallRange = new EmptyBigEmptySmallRange();
            FullBigFullSmallRange fullBigFullSmallRange = new FullBigFullSmallRange();
            FullBigEmptySmallRange fullBigEmptySmallRange = new FullBigEmptySmallRange();
            EmptyBigFullSmallRange emptyBigFullSmallRange = new EmptyBigFullSmallRange();
            PartialBigEmptySmallRange partialBigEmptySmallRange = new PartialBigEmptySmallRange();
            PartialBigFullSmallRange partialBigFullSmallRange = new PartialBigFullSmallRange();
            FullBigPartialSmallRange fullBigPartialSmallRange = new FullBigPartialSmallRange();
            EmptyBigPartialSmallRange emptyBigPartialSmallRange = new EmptyBigPartialSmallRange();
            PartialBigPartialSmallRange partialBigPartialSmallRange = new PartialBigPartialSmallRange();

            _fillBig = new FillBigAction(_jugs);
            _fillSmall = new FillSmallAction(_jugs);
            _emptyBig = new EmptyBigAction(_jugs);
            _emptySmall = new EmptySmallAction(_jugs);
            _big2Small = new Big2SmallAction(_jugs);
            _small2Big = new Small2BigAction(_jugs);

            _fillBig.SetTrace(trace);
            _fillSmall.SetTrace(trace);
            _emptyBig.SetTrace(trace);
            _emptySmall.SetTrace(trace);
            _big2Small.SetTrace(trace);
            _small2Big.SetTrace(trace);

            _fillBig.SetId("fillBig");
            _fillSmall.SetId("fillSmall");
            _emptyBig.SetId("emptyBig");
            _emptySmall.SetId("emptySmall");
            _big2Small.SetId("big2Small");
            _small2Big.SetId("small2Big");

            _emptyBigEmptySmall = new State(_fillBig);
            _fullBigFullSmall = new State(_emptyBig);
            _fullBigEmptySmall = new State(_big2Small);
            _emptyBigFullSmall = new State(_small2Big);
            _partialBigEmptySmall = new State(_big2Small);
            _partialBigFullSmall = new State(_emptySmall);
            _fullBigPartialSmall = new State(_big2Small);
            _emptyBigPartialSmall = new State(_small2Big);
            _partialBigPartialSmall = new State(_big2Small);

            _forbidden = new ForbiddenState();
            _desired = new DesiredState();
            _forbidden.SetId("f1");
            _desired.SetId("d1");
            _emptyBigEmptySmall.SetTrace(trace).SetId("emptyBigEmptySmall");
            _emptyBigEmptySmall.Add(_fillSmall);
            _fullBigFullSmall.SetTrace(trace).SetId("fullBigFullSmall");
            _fullBigEmptySmall.SetTrace(trace).SetId("fullBigEmptySmall");
            _fullBigEmptySmall.Add(_fillSmall);
            _emptyBigFullSmall.SetTrace(trace).SetId("emptyBigFullSmall");
            _emptyBigFullSmall.Add(_emptySmall);
            _partialBigEmptySmall.SetTrace(trace).SetId("partialBigEmptySmall");
            _partialBigEmptySmall.Add(_emptyBig);
            _partialBigFullSmall.SetTrace(trace).SetId("partialBigFullSmall");
            _partialBigFullSmall.Add(_emptyBig);
            _fullBigPartialSmall.SetTrace(trace).SetId("fullBigPartialSmall");
            _emptyBigPartialSmall.SetTrace(trace).SetId("emptyBigPartialSmall");
            _emptyBigPartialSmall.Add(_fillBig);
            _partialBigPartialSmall.SetTrace(trace).SetId("partialBigPartialSmall");
            _forbidden.SetTrace(trace);
            _desired.SetTrace(trace);

            _jugs.AddRange(_emptyBigEmptySmall, emptyBigEmptySmallRange);
            _jugs.AddRange(_fullBigFullSmall, fullBigFullSmallRange);
            _jugs.AddRange(_fullBigEmptySmall, fullBigEmptySmallRange);
            _jugs.AddRange(_emptyBigFullSmall, emptyBigFullSmallRange);
            _jugs.AddRange(_partialBigEmptySmall, partialBigEmptySmallRange);
            _jugs.AddRange(_partialBigFullSmall, partialBigFullSmallRange);
            _jugs.AddRange(_fullBigPartialSmall, fullBigPartialSmallRange);
            _jugs.AddRange(_emptyBigPartialSmall, emptyBigPartialSmallRange);
            _jugs.AddRange(_partialBigPartialSmall, partialBigPartialSmallRange);
            DesiredRange desiredRange = new DesiredRange();
            _jugs.AddRange(_desired, desiredRange);

            _behavior = new JugBehavior(_jugs);
            _behavior.Add(_emptyBigEmptySmall);
            _behavior.Add(_fullBigFullSmall);
            _behavior.Add(_partialBigEmptySmall);
            _behavior.Add(_partialBigFullSmall);
            _behavior.Add(_fullBigPartialSmall);
            _behavior.Add(_emptyBigPartialSmall);
            _behavior.Add(_partialBigPartialSmall);
        }

        public void Display()
        {
            _jugs.Display();
            trace.Dump();
            trace.Clear();
        }

        public override void Start()
        {
            _behavior.Act(_fillBig, null);
        }

        public override void Start(IDictionary<string, object> inputs)
        {
            _behavior.Act(_initialize, inputs);
            Start();
        }

        public override void Act(Action a)
        {
            _behavior.Act(a, null);
        }

        public override bool DefaultState()
        {
            return (_jugs.CurrentState() == _desired);
        }

        public static void MainDieHard(string[] args)
        {
            try
            {
                DieHard comp = new DieHard(0, 0);

                //automatic transition
                comp.Start();
                //while (!comp.defaultState()) {
                comp.walk();
                //}
                //system triggered transition
                //while(!comp.defaultState()) {
                //	  comp.act(multiply);
                //}
                comp.Display();

                IDictionary<string, object> inputs = new Dictionary<string, object>();
                inputs.Add("big", 1);
                inputs.Add("small", 5);
            }
            catch (Exception e)
            {
                // TODO Auto-generated catch block
                System.Console.WriteLine(e.StackTrace);
            }
        }

        public Component walk()
        {
            this.Act(_big2Small);
            this.Act(_emptySmall);
            this.Act(_big2Small);
            this.Act(_fillBig);
            this.Act(_big2Small);
            return this;
        }

        public override string Tick(string key)
        {
            // TODO Auto-generated method stub
            return null;
        }
    }
}