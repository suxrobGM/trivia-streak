using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HTTP
{
	public class Reply
	{
		public byte[] Data;

		public Reply(byte[] data = null)
		{
			this.Data = data;
		}

		public override string ToString()
		{
			string output = "[";

			if (Data != null)
				output += "Data: " + DataAsString;

			output += "]";

			return output;
		}

		public string DataAsString
		{
			get
			{
				return GetString(Data);
			}
		}
		
		public static byte[] GetBytes(string str)
		{
			if (str != null)
				return System.Text.Encoding.UTF8.GetBytes(str);
			else
				return null;
		}
		
		public static string GetString(byte[] bytes)
		{
			if (bytes != null)
				return System.Text.Encoding.UTF8.GetString(bytes);
			else
				return null;
		}
	}
}