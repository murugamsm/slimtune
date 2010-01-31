﻿/*
* Copyright (c) 2007-2009 SlimDX Group
* 
* Permission is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions:
* 
* The above copyright notice and this permission notice shall be included in
* all copies or substantial portions of the Software.
* 
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
* THE SOFTWARE.
*/
using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace SlimTuneUI
{
	static class Program
	{
        static void TimingsTest()
        {
            SqlServerCompactEngine engine = new SqlServerCompactEngine(Path.GetTempFileName(), true);
            
            //start generating random timings
            Random rand = new Random(5);
            for(int i = 0; i < 5000; ++i)
            {
                int id = rand.Next(10);
                long time = rand.Next(1000);
                engine.FunctionTiming(id, time);
            }
            engine.Flush();
        }

		static void SQLiteTest()
		{
			const string dbFile = "test.db";
			if(File.Exists(dbFile))
				File.Delete(dbFile);

			using(SQLiteDatabase db = new SQLiteDatabase(dbFile))
			{
				db.Execute("CREATE TABLE test2 (x INT PRIMARY KEY, y INT, Z INT)");
				db.Execute("INSERT INTO test2 (x, y, z) VALUES (1, 2, 3)");
				db.Execute("INSERT INTO test2 (x, y, z) VALUES (3, 2, 1)");
				db.Execute("INSERT INTO test2 (x, y, z) VALUES (5, 6, 7)");

				using(var cmd = new SQLiteStatement(db, "SELECT * FROM test2 WHERE x > ?1"))
				{
					cmd.BindInt(1, 2);
					while(cmd.Step())
					{
						Console.WriteLine("Value is: {0} | {1} | {2}", cmd.GetInt(0), cmd.GetInt(1), cmd.GetInt(2));
					}
				}
			}
		}

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
		static void Main()
		{
            //TimingsTest();
			//SQLiteTest();
            //return;

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainWindow());
		}
	}
}
