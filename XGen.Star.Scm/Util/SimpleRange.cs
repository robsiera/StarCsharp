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

namespace XGen.Star.Scm.Util
{
	public class SimpleRange : Range 
	{
	    private readonly string _property;
	    private readonly Operator _op;
		public decimal Value { get; set; }
		
		public SimpleRange(string property, Operator op, decimal value) 
		{
			_property = property;
			_op = op;
			Value = value;
		}
		
		public bool Eval(IDictionary<string, object> instance) 
		{
			return Compare((decimal)instance[_property]);
		}
		
		private bool Compare(decimal input) 
		{
			if (_op.Equals(Operator.EQUALS)) 
				return input == Value;
			if (_op.Equals(Operator.NOT_EQUALS)) 
				return input != Value;
			if (_op.Equals(Operator.GREATER_THAN)) 
				return input > Value;
			if (_op.Equals(Operator.LESS_THAN)) 
				return input < Value;
			if (_op.Equals(Operator.GREATER_THAN_OR_EQUAL)) 
				return input >= Value;
			if (_op.Equals(Operator.LESS_THAN_OR_EQUAL)) 
				return input <= Value;
			return false;
		}

	}
}
