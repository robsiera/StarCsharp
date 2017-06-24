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

namespace XGen.Star.Scm.Connectors
{
	//public class H2Connector : Connector 
	//{
	//	static Connection _h2Connection;
	//	static String _tableName;
	//	static int _idCount;
	//	static String _insert;
	//	static String _select;
	//	static String _instanceId = "instanceId";
	//	static List<String> _keys;
		
	////	public override Connector connect(IDictionary<String,String> connOptions) 
	////	{
	////		try 
	////		{
	////			Class.forName("org.h2.Driver");
	////		} 
	////		catch (ClassNotFoundException e) 
	////		{
	////			throw new DatabaseException();
	////		}
			
	////		try 
	////		{
	////			_idCount =  (int )(Math. random() * 1147483647 + 1);
	////			System.Console.WriteLine("[H2] db:"+connOptions["db")+"/");
	////			//+connOptions["db")
	////			// H2 database can be found in absolute directory /data/h2/
	////			Connection h2conn = DriverManager.getConnection("jdbc:h2:/data/h2/"+connOptions["db"), connOptions["user"), connOptions["password"));
	////			_h2Connection = h2conn;
	////			if (connOptions["tableName") != null) 
	////			{
	////				_tableName = connOptions["tableName");
	////			}
	////			if (connOptions["insert") != null) 
	////			{
	////				_insert = connOptions["insert");
	////				_keys = Arrays.asList(_insert.replaceAll("\\s+","").split(","));
	////			}
	////			if (connOptions["select") != null) 
	////			{
	////				_select = connOptions["select");
	////			}
	////			if (connOptions["instanceId") != null) 
	////			{
	////				_instanceId = connOptions["instanceId");
	////			}
	////			if (connOptions["create_table") != null) 
	////			{
	////				try 
	////				{
	////					Statement cts = _h2Connection.createStatement();
	////					String ct = connOptions["create_table");
	////					bool res = cts.execute(ct);
	////				} catch (SQLException e) 
	////				{
	////					System.Console.WriteLine("[h2] exception?create_table "+e.ToString());
	////					throw new DatabaseException();
	////				}				
	////			}
	////		} 
	////		catch (SQLException e) 
	////		{
	////			System.Console.WriteLine("[H2] exception:"+e.getMessage());
	////			throw new DatabaseException();
	////		}
	////		return this;
	////	}

	////	protected String id() 
	////	{
	////		return String.valueOf(_idCount++);
	////	}
		
	////	public override void putItem(Options params) throws DatabaseException 
	////	{
	////		Statement statement = null;
	////		IDictionary<String,Object> item = (IDictionary<String,Object>)params.Item;
	////		//Set<String> keys = item.keySet();
	////		String insert = "";
	////		String query  = "'"+this.id()+"'";// ID
	////		insert += "ID";
	////		for(String key : _keys) 
	////		{
	////			insert += ", "+key;
	////			query += ", '"+item[key)+"'";
	////		} 
	////		query += ");";
	////		query  = "INSERT INTO \""+_tableName+"\" ("+insert+") VALUES (" + query;
	////		System.Console.WriteLine("[H2Connector] insert string(v1): "+ query);
			
	////		try 
	////		{
	////			statement = _h2Connection.createStatement();
	////			bool res = statement.execute(query);
				
	////			System.Console.WriteLine("result: "+String.valueOf(res));
			
	////		} catch (SQLException e ) 
	////		{
	////			System.Console.WriteLine("[H2] exception: "+e.ToString());
	////			throw new DatabaseException();
	////		} 
	////		System.Console.WriteLine("returning");

	////	}

	////	public override void updateItem(Options params)
	////	{
	////		// TODO Auto-generated method stub
	////	}

	////	public override void deleteItem(Options params)
	////	{
	////		// TODO Auto-generated method stub
	////	}

	////	public override IDictionary<String,Object> getItem(Options params)
	////	{
	////		Statement statement = null;
	////		IDictionary<String,Object> item = (IDictionary<String,Object>)params.Item;
	////		String query  = "SELECT * FROM "+_tableName+" WHERE "+_select+" = "+item[_instanceId);
	////			// could look for a specific step too
	////			query += ";";
	////		IDictionary<String,Object> res = new Dictionary<String,Object>();
	////		try 
	////		{
	////			statement = _h2Connection.createStatement();
	////			ResultSet rs = statement.executeQuery(query);
	////			while (rs.next()) 
	////			{
	////				for (String key : _keys) 
	////				{
	////					res.Add(key, rs.getString(key));
	////				}
	////			}
	////		} 
	////		catch (SQLException e ) 
	////		{
	////			throw new DatabaseException();
	////		} 
	////		return res;
	////	}

	////	public override List<IDictionary<String,Object>> query(Options params)
	////	{
	////		IDictionary<String,Object> res = new Dictionary<String,Object>();
	////		// TODO To be implemented
	////		return null;
	////	}

	////	public override List<IDictionary<String,Object>> scan(Options params)
	////	{
	////		// TODO Auto-generated method stub
	////		return null;
	////	}

	////	public override void close()
	////	{
	////		// TODO Auto-generated method stub
	////	}
	//}
}
