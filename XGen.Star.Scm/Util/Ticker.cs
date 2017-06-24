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
using System.Threading;
using XGen.Star.Scm.Connectors;

namespace XGen.Star.Scm.Util
{

	//DB Table
	/*----------------------------------------
	DROP TABLE IF EXISTS TICKERS;
	CREATE TABLE TICKERS (
		ID INT,
		INSTANCE_ID VARCHAR(36) PRIMARY KEY,
		ACTION_CLASS VARCHAR(64),
		TICK_COUNT int,
		TICK_MAX int,
		EXPIRES BIGINT,
		DONE bool
	);
	*///----------------------------------------

	public class Ticker<T> where T:Type
	{
	    private IDictionary<string,T> _tickers;
	    private int _period = 5000;
	    private readonly IConnector _dbConnector;
	    private Thread _thread;
		
		private volatile bool _running = true;
		
		public Ticker() 
		{
			this._tickers = new Dictionary<string,T>();
		}
		
		public Ticker(int period) : this()
		{
			this._period = period;
		}
		
		public Ticker(IConnector c, int period) : this()
		{
			this._dbConnector = c;
			if (period>0) {this._period = period;}
		}
		
		public void Start() 
		{
			_running = true;
			_thread = new Thread (new ThreadStart(Execute));
			_thread.Start();
		}
		
		public void Shutdown() 
		{
			_running = false;
		}
		
		public void Execute() 
		{
			IDictionary<string,T> activeTickers = new Dictionary<string,T>();
			IList<IDictionary<string,object>> updatedRecords;
			
			if (this._dbConnector != null) 
			{
				// scan active tickers
				Options o = new Options();
				o._select = "DONE";
				o._value = "0";
				try 
				{
					updatedRecords = this._dbConnector.Scan(o);
					// update all ticks and
					// stop all expired tickers
					foreach (var record in updatedRecords) 
					{
						int ticks = int.Parse(record["TICK_COUNT"].ToString());
						int tickMax = int.Parse(record["TICK_MAX"].ToString());
						long expires = long.Parse(record["EXPIRES"].ToString());
						ticks++;
						if (ticks>=tickMax) 
							record.Add("DONE", "1");
						else 
						{
							long now = DateTime.UtcNow.Millisecond % 1000;
							if (expires <= now) 
								record.Add("DONE", "1");
							else // we can tick
								activeTickers.Add(record["TICK_COUNT"].ToString(), _tickers[record["TICK_COUNT"].ToString()]);
						}
						record.Add("TICK_COUNT", ticks.ToString());
						this.Persist(record);
					}
				} 
				catch (DatabaseException e) 
				{
					System.Console.WriteLine(e.StackTrace);
				}
			}
			// execute ticks for active tickers
			System.Console.WriteLine("[Ticker] there are "+activeTickers.Count.ToString()+" instances ticking" );

          
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            //TODO Refactor this
      //      foreach (var key in activeTickers.Keys)
      //      {
      //          T c = activeTickers[key];
      //          try
      //          {
      //              if (c != null)
      //              {
      //                  System.Console.WriteLine("[Ticker] class:" + activeTickers[key].ToString());
      //                  T action = (T)Activator.CreateInstance(c);
      //                  MethodInfo m;
      //                  try
      //                  {
      //                      m = action.GetType().GetMethod("tick",new System.Type[] {typeof(String)});
						//	m.Invoke(key);
						//} 
      //                  catch (SecurityException e1)
      //                  {
      //                      System.Console.WriteLine(e1.StackTrace);
      //                  }
      //                  catch (MissingMethodException e1)
      //                  {
      //                      System.Console.WriteLine(e1.StackTrace);
      //                  }
      //                  catch (ArgumentException e)
      //                  {
      //                      System.Console.WriteLine(e.StackTrace);
      //                  }
      //                  catch (TargetInvocationException e)
      //                  {
      //                      System.Console.WriteLine(e.StackTrace);
      //                  }
      //              }
      //          }
      //          catch (InstantiationException e)
      //          {
      //              System.Console.WriteLine(e.StackTrace);
      //          }
      //          catch (IllegalAccessException e)
      //          {
      //              System.Console.WriteLine(e.StackTrace);
      //          }
            //}












            this._tickers = activeTickers;
		}
		
		public bool StartTicker(string instance, T c, string tickMax, string expiration) 
		{
			if (this._tickers[instance] != null) 
			{
				// please stop the ticker first
				// before you start a new one
				System.Console.WriteLine("[Ticker] warning, there is already a ticker running for instance:"+ instance);
				return false;
			}
			
			System.Console.WriteLine("[Ticker] registering ticker for instance:"+ instance);
			// create record in DB
			if (this._dbConnector != null)
			{
			    string className = c.GetType().FullName;
                IDictionary<string,object> tickerInstance = new Dictionary<string,object>();
				tickerInstance.Add("INSTANCE_ID",instance);
				tickerInstance.Add("ACTION_CLASS",className);
				tickerInstance.Add("TICK_COUNT","0");

				if (tickMax != null) 
					tickerInstance.Add("TICK_MAX",tickMax);
				else 
					tickerInstance.Add("TICK_MAX","0");

				System.Console.WriteLine("[Ticker] expiration: " + expiration);
				if (expiration != null) 
					tickerInstance.Add("EXPIRES",expiration);
				else 
					tickerInstance.Add("EXPIRES","0");

				tickerInstance.Add("DONE","0");
				System.Console.WriteLine("[Ticker] persisting:"+ instance);
				this.Persist(tickerInstance);
			}
			// add ticker 
			this._tickers.Add(instance, c);
			return true;
		}

		protected bool Persist(IDictionary<string,object> instance) 
		{
			if (_dbConnector != null) 
			{
				Options current = new Options();
				current._item = instance;
				
				try 
				{
					_dbConnector.PutItem(current);
					return true;
				} catch (DatabaseException e) 
				{
					System.Console.WriteLine("[Ticker] persist exception");
					System.Console.WriteLine(e.StackTrace);
				} 
			}
			return false;
		}

		public bool StopTicker(string instance) 
		{
			if (this._tickers[instance] != null) 
			{
				// update the database
				if (this._dbConnector != null)
				{
				    string className = this._tickers[instance].GetType().FullName;
					IDictionary<string,object> tickerInstance = new Dictionary<string,object>();
					tickerInstance.Add("INSTANCE_ID",instance);
					tickerInstance.Add("ACTION_CLASS",className);
					tickerInstance.Add("TICK_COUNT","0");
					tickerInstance.Add("TICK_MAX","0");
					tickerInstance.Add("EXPIRES","0");
					tickerInstance.Add("DONE","1");
					this.Persist(tickerInstance);
				}
				// remove ticker from list
				this._tickers.Remove(instance);
			}
			return false;
		}

		public void Run() 
		{
			System.Console.WriteLine("[Ticker] run");
			while(_running) 
				this.Execute();
		}
	}
}