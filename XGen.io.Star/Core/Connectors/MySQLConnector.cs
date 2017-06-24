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
using System.Collections;
using System.Collections.Generic;
//using MySql.Data.MySqlClient;

namespace XGen.io.Star.Core.Connectors
{
	//public class MySQLConnector : Connector 
	//{
	//	MySqlConnection _MySQLConnection;
	//	String _tableName;
	//	int _idCount;
	//	String _insert;
	//	String _select;
	//	String _instanceId = "INSTANCE_ID";
	//	IList<String> _keys;
	//	static Random random = new Random();
		
		//public Connector connect(IDictionary<String,String> connOptions) 
		//{
		//	System.Console.WriteLine("[MySQLConnector] Initializing connector");
		//	try 
		//	{
		//		_idCount =  (int )(random.Next() * 1147483647 + 1);
		//		System.Console.WriteLine("[MySQL] db:"+connOptions["db"]+"/");
		//		//+connOptions["db")
		//		var connStr = String.Format("server={0};database={2};user={3};password={4}",
		//			"mysql://localhost/", connOptions["db"], connOptions["user"], connOptions["password"]);
		//		var conn = new MySqlConnection(connStr);
		//		conn.Open();
		//		_MySQLConnection = conn;
		//		if (connOptions["tableName"] != null) 
		//		{
		//			_tableName = connOptions["tableName"];
		//		}
		//		if (connOptions["insert"] != null) 
		//		{
		//			_insert = connOptions["insert"];
		//			_keys = new List<String>(_insert.Replace("\\s+","").Split(','));
		//			System.Console.WriteLine("[MySQLConnector] found keys:"+_keys.Count);
		//		} 
		//		else 
		//		{
		//			System.Console.WriteLine("[MySQLConnector] no insert statement:"+connOptions["insert"]);
		//		}
		//		if (connOptions["select"] != null) 
		//		{
		//			_select = connOptions["select"];
		//		}
		//		if (connOptions["instanceId"] != null) 
		//		{
		//			_instanceId = connOptions["instanceId"];
		//		}
		//		if (connOptions["create_table"] != null) 
		//		{
		//			try
		//			{
		//				var comm = _MySQLConnection.CreateCommand();
		//				comm.CommandText = connOptions["create_table"];
		//				var res = comm.ExecuteScalar();
		//			}
		//			catch (MySqlException e)
		//			{
		//				System.Console.WriteLine("[MySQL] exception?create_table "+e.ToString());
		//				throw new DatabaseException();
		//			}
		//		}
		//	} 
		//	catch (MySqlException e) 
		//	{
		//		System.Console.WriteLine("[MySQL] exception:"+e.Message);
		//		System.Console.WriteLine(e.StackTrace);
		//		throw new DatabaseException();
		//	}
		//	return this;
		//}

		//protected String id() 
		//{
		//	return (_idCount++).ToString();
		//}
		
		//public void putItem(Options opt)
		//{
		//	MySqlCommand comm = null;
		//	IDictionary<String,Object> item = (IDictionary<String,Object>)opt.Item;
		//	String insert = "";
		//	String query  = "'"+this.id()+"'";// ID
		//	insert += "ID";
		//	String update = "ID="+query;
		//	if (_keys == null) 
		//	{
		//		System.Console.WriteLine("[MySQLConnector] keys is null");
		//	}
		//	foreach (String key in _keys) {
		//		insert += ", "+key;
		//		query += ", '"+item[key]+"'";
		//		if (!key.Equals(_instanceId)) 
		//		{ 
		//			if (!key.Equals("STEP")) 
		//			{
		//				if (!key.Equals("TICK_COUNT")) 
		//				{
		//					update += ", "+key+"='"+item[key]+"'";
		//				} 
		//				else 
		//				{
		//					update += ", TICK_COUNT = TICK_COUNT + 1";
		//				}
		//			} 
		//			else 
		//			{
		//				update += ", STEP = STEP+1";
		//			}
		//		}
		//	} 
		//	query += ") ON DUPLICATE KEY UPDATE "+update+";";
		//	query  = "INSERT INTO "+_tableName+" ("+insert+") VALUES (" + query;
			
		//	try 
		//	{
		//		comm = _MySQLConnection.CreateCommand();
		//		comm.CommandText = query;
		//		var res = comm.ExecuteScalar();
		//	} 
		//	catch (MySqlException e ) 
		//	{
		//		System.Console.WriteLine("[MySQLConnector] exception: "+e.ToString());
		//		throw new DatabaseException();
		//	} 
		//	System.Console.WriteLine("returning");
		//}

		//public void updateItem(Options opt)
		//{
		//	// TODO Auto-generated method stub
		//}

		//public void deleteItem(Options opt)
		//{
		//	// TODO Auto-generated method stub
		//}

		//public IDictionary<String,Object> getItem(Options opt)
		//{
		//	MySqlCommand comm = null;
		//	IDictionary<String,Object> item = (IDictionary<String,Object>)opt.Item;
		//	//System.Console.WriteLine("[MySQLConnector] getItem:"+item[_instanceId));
		//	String query  = "SELECT * FROM "+_tableName+" WHERE "+_select+" = '"+item[_instanceId]+"'";
		//		// could look for a specific step too
		//		query += ";";
		//	IDictionary<String,Object> res = new Dictionary<String,Object>();
		//	try 
		//	{
		//		comm = _MySQLConnection.CreateCommand();
		//		comm.CommandText = query;
		//		using (var reader = comm.ExecuteReader())
		//		{
		//			while (reader.Read()) 
		//				foreach (String key in _keys) 
		//					res.Add(key, reader.GetString(key));
		//		}
		//	} 
		//	catch (MySqlException e ) 
		//	{
		//		System.Console.WriteLine("[MySQLConnector] getItem exception: "+e.Message);
		//		throw new DatabaseException();
		//	} 
		//	return res;
		//}

		//public IList<IDictionary<String,Object>> scan(Options opt)
		//{
			
		//	List<IDictionary<String,Object>> res = new List<IDictionary<String,Object>>();

		//	MySqlCommand comm = null;
		//	String select = opt.select;
		//	String value = opt.value;
		//	if (select == null) 
		//		select = _select;
		//	if (value == null) 
		//		value = "null";
		//	//System.Console.WriteLine("[MySQLConnector] getItem:"+item[_instanceId));
		//	String query  = "SELECT * FROM "+_tableName+" WHERE "+select+" = '"+value+"'";
		//		// could look for a specific step too
		//		query += ";";
		//	try 
		//	{
		//		comm = _MySQLConnection.CreateCommand();
		//		comm.CommandText = query;
		//		using (var reader = comm.ExecuteReader())
		//		{
		//			while (reader.Read()) 
		//			{
		//				var rec = new Dictionary<String,Object>();
		//				foreach (String key in _keys) 
		//					rec.Add(key, reader.GetString(key));
		//				res.Add(rec);
		//			}
		//		}
		//	} 
		//	catch (MySqlException e ) 
		//	{
		//		System.Console.WriteLine("[MySQLConnector] getItem exception: "+e.Message);
		//		throw new DatabaseException();
		//	} 
		//	return res;
		//}

		//public IList<IDictionary<String,Object>> query(Options opt)
		//{
		//	// TODO Auto-generated method stub
		//	return null;
		//}

		//public void close()
		//{
		//	try 
		//	{
		//		_MySQLConnection.Close();
		//	} 
		//	catch (MySqlException e) 
		//	{
		//		// TODO Auto-generated catch block
		//		System.Console.WriteLine(e.StackTrace);
		//		throw new DatabaseException();
		//	}
			
		//}
    //}
}
