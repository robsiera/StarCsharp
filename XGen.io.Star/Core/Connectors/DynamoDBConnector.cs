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

namespace XGen.io.Star.Core.Connectors
{
    public class DynamoDbConnector : IConnector {
        public void PutItem(Options opt)
        {
        }

        public void UpdateItem(Options opt)
        {
        }

        public void DeleteItem(Options opt)
        {
        }

        public IDictionary<string,object> GetItem(Options opt)
        {
            return null;
        }

        public IEnumerable<IDictionary<string,object>> Query(Options opt)
        {
            return null;
        }

        public IEnumerable<IDictionary<string,object>> Scan(Options opt)
        {
            return null;
        }

        public void Close()
        {
        }

        public IConnector Connect(IDictionary<string,string> connOptions)
        {
            // TODO Auto-generated method stub
            return null;
        }

        IList<IDictionary<string, object>> IConnector.Query(Options opt)
        {
            throw new NotImplementedException();
        }

        IList<IDictionary<string, object>> IConnector.Scan(Options opt)
        {
            throw new NotImplementedException();
        }
    }
}
