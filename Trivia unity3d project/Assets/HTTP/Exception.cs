using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HTTP
{
	public class Exception : System.Exception
	{
		public enum Type
		{
			NotConfigured,
			NoInternetAccess,
			CouldNotSend
		}

		public Type type;

		public Exception(Type type, string message = ""): base(message)
		{
			this.type = type;
		}
		
		public Exception(Type type, string message, System.Exception innerException): base(message, innerException)
		{
			this.type = type;
		}
		
		public override string ToString()
		{
			string output = type.ToString();
			if (Message != "")
				output += "\n" + Message;
			if (InnerException != null)
				output += "\n" + InnerException.ToString();
			return output;
		}
	}
}