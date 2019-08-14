using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HTTP
{
	public enum Protocol
	{
		HTTP,
		HTTPS
	}

	public class Settings
	{
		public string hostName;
		public int? port;
		public Protocol? protocol;

		public Settings(string hostName)
		{
			this.hostName = hostName;
		}

		public Settings Port(int port)
		{
			this.port = port;
			return this;
		}

		public Settings Protocol(Protocol protocol)
		{
			this.protocol = protocol;
			return this;
		}

		public override string ToString()
		{
			string output = "";
			output += "[";
			output += "Hostname: " + hostName;
			if (protocol != null)
				output += ", Protocol: " + protocol.ToString();
			if (port != null)
				output += ", Port: " + port;
			output += "]";
			return output;
		}
	}
}