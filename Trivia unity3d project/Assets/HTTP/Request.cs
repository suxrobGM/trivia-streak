using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HTTP
{
	public class Request
	{
		public string Action;
		public Dictionary<string, string> Parameters;

		public delegate void OnReplyCallback(Reply reply);
		public delegate void OnFailedCallback();

		protected OnReplyCallback onReply;
		protected OnFailedCallback onFailed;

		public Request(string action, Dictionary<string, string> parameters = null)
		{
			this.Action = action;
			this.Parameters = parameters;
		}

		public virtual Request OnReply(OnReplyCallback onReply)
		{
			this.onReply = onReply;
			return this;
		}

		public virtual Request OnFailed(OnFailedCallback onFailed)
		{
			this.onFailed = onFailed;
			return this;
		}

		public void Send()
		{
			Client.Instance.Send(this);
		}

		public void Succeed(Reply reply)
		{
			if (onReply != null)
				onReply(reply);
		}

		public void Fail()
		{
			if (onFailed != null)
				onFailed();
		}

		public override string ToString()
		{
			string output = "[";
			
			if (Action != null)
				output += "Action: " + Action;
			
			if (Parameters != null)
			{
				output += ", Parameters: {";
				foreach(KeyValuePair<string, string> pair in Parameters)
					output += pair.Key + ": " + pair.Value + ", ";
				output += "}";
			}

			output += "]";
			return output;
		}
	}
}