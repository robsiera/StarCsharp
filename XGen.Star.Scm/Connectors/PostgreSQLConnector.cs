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
using Npgsql;

namespace XGen.Star.Scm.Connectors
{
	public class PostgreSqlConnector : IConnector 
	{
	    private static NpgsqlConnection _postgreSqlConnection;
	    private static string _tableName;
	    private static int _idCount;
	    private static string _insert;
	    private static string _select;
	    private static string _instanceId = "instanceId";
	    private static IList<string> _keys;
	    private static readonly Random _rnd = new Random();
		
		public IConnector Connect(IDictionary<string,string> connOptions)
		{
			try 
			{
				_idCount =  (int )(_rnd.Next() * 1147483647 + 1);
				
				System.Console.WriteLine("[PostgreSQL] db:"+connOptions["db"]+"/");
				//+connOptions["db")
				// H2 database can be found in absolute directory /data/h2/
				var connString = $"Server={"h2:/data/h2/"};User Id={connOptions["user"]};Password={connOptions["password"]};Database={connOptions["db"]}";
				var conn = new NpgsqlConnection(connString);
				_postgreSqlConnection = conn;
				conn.Open();
				if (connOptions["tableName"] != null)
				{
					_tableName = connOptions["tableName"];
				}
				if (connOptions["insert"] != null)
				{
					_insert = connOptions["insert"];
					_keys = new List<string>(_insert.Replace("\\s+","").Split(','));
				}
				if (connOptions["select"] != null)
				{
					_select = connOptions["select"];
				}
				if (connOptions["instanceId"] != null)
				{
					_instanceId = connOptions["instanceId"];
				}
				if (connOptions["create_table"] != null)
				{
					try
					{
						var comm = _postgreSqlConnection.CreateCommand();
						comm.CommandText = connOptions["create_table"];
						var res = comm.ExecuteScalar();
					} 
					catch (NpgsqlException e)
					{
						System.Console.WriteLine("[PostgreSQL] exception?create_table "+e.ToString());
						throw new DatabaseException();
					}				
				}
			}
			catch (NpgsqlException e)
			{
				System.Console.WriteLine("[PostgreSQL] exception:"+e.Message);
				throw new DatabaseException();
			}
			return this;
		}

		protected string Id()
		{
			return _idCount++.ToString();
		}
		
		public void PutItem(Options opt)
		{
			NpgsqlCommand comm = null;
			IDictionary<string,object> item = (IDictionary<string,object>)opt._item;
			//Set<String> keys = item.keySet();
			string insert = "";
			string query  = "'"+this.Id()+"'";// ID
			insert += "ID";
			foreach (var key in _keys)
			{
				insert += ", "+key;
				query += ", '"+item[key]+"'";
			} 
			query += ");";
			query  = "INSERT INTO \""+_tableName+"\" ("+insert+") VALUES (" + query;
			System.Console.WriteLine("[PostgreSQLConnector] insert string(v1): "+ query);
			
			try
			{
				comm = _postgreSqlConnection.CreateCommand();
				comm.CommandText = query;
				var res = comm.ExecuteScalar();
				System.Console.WriteLine("result: "+res.ToString());
			}
			catch (NpgsqlException e )
			{
				System.Console.WriteLine("[PostgreSQL] exception: "+e.ToString());
				throw new DatabaseException();
			} 
			System.Console.WriteLine("returning");
		}

		public void UpdateItem(Options opt)
		{
			// TODO Auto-generated method stub
		}

		public void DeleteItem(Options opt)
		{
			// TODO Auto-generated method stub
		}

		public IDictionary<string,object> GetItem(Options opt)
		{
		    var item = (IDictionary<string,object>)opt._item;
			string query  = "SELECT * FROM "+_tableName+" WHERE "+_select+" = "+item[_instanceId];
				// could look for a specific step too
				query += ";";
			var res = new Dictionary<string,object>();
			try
			{
				var comm = _postgreSqlConnection.CreateCommand();
				comm.CommandText = query;
				using(var reader = comm.ExecuteReader())
				{
					while (reader.Read())
						foreach (string key in _keys)
							res.Add(key, reader[key]);
				}
			} 
			catch (NpgsqlException e )
			{
				throw new DatabaseException();
			} 
			return res;
		}

		public void Close()
		{
			_postgreSqlConnection.Close();
		}

        IList<IDictionary<string, object>> IConnector.Query(Options opt)
        {
			var res = new Dictionary<string,object>();
			return null;
        }

        IList<IDictionary<string, object>> IConnector.Scan(Options opt)
        {
			return null;
        }
    }
}
