using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

namespace HTTP
{
	public class Client : MonoBehaviour
	{
		public Uri BaseURI { get; protected set; }

		static Client _instance = null;
		static public Client Instance
		{
			get
			{
				if(_instance == null)
				{
					_instance = new GameObject("HTTP_Client", typeof(Client)).GetComponent<Client>();
				}
				return _instance;
			}
		}

		protected virtual void Awake()
		{
			if (_instance != null)
			{
				Destroy(gameObject);
				return;
			}
			DontDestroyOnLoad(gameObject);
		}

		public void Configure(Settings settings)
		{
			Protocol protocol = settings.protocol != null ? settings.protocol.Value : Protocol.HTTP;
			UriBuilder builder = new UriBuilder(protocol.ToString().ToLower(), settings.hostName);
			if (settings.port != null)
				builder.Port = settings.port.Value;
			BaseURI = builder.Uri;
			Log("Configured", settings.ToString() + " - " + BaseURI.ToString());
		}

		public void Send(Request request)
		{
			StartCoroutine(SendInternal(request));
		}

		protected IEnumerator SendInternal(Request request)
		{
			if (BaseURI == null)
			{
				request.Fail();
				Log(new HTTP.Exception(HTTP.Exception.Type.NotConfigured, request.ToString()));
				yield break;
			}
			
			#if !UNITY_EDITOR
			if (! IsConnectedToInternet)
			{
				request.Fail();
				Log(new HTTP.Exception(HTTP.Exception.Type.NoInternetAccess, request.ToString()));
				yield break;
			}
			#endif
			
			UriBuilder builder = new UriBuilder(BaseURI);
			builder.Path = request.Action.ToLower();
			string url = builder.ToString();

			WWW www;
			if (request.Parameters != null)
			{
				WWWForm form = new WWWForm();
				foreach(KeyValuePair<string, string> pair in request.Parameters)
					form.AddField(pair.Key, pair.Value);
				www = new WWW(url, form);
			}
			else
			{
				www = new WWW(url);
			}
			yield return www;
			if (www.error == null)
			{
				Reply reply = new Reply(www.bytes);
				request.Succeed(reply);
				Log("Sent/Received", "Request: " + request.ToString() + "\n" + "Reply: " + reply.ToString()); 
			}
			else
			{
				request.Fail();
				Log(new HTTP.Exception(HTTP.Exception.Type.CouldNotSend, www.error + "\n" + request.ToString()));
			}
		}

		public static void Log(HTTP.Exception exception)
		{
			Log(exception.ToString());
		}
		
		public static void Log(string tag, string data)
		{
			Log(tag + ":\n" + data);
		}
		
		public static void Log(string message)
		{
			Debug.Log("[HTTP] " + message + "\n");
		}

		public static bool IsConnectedToInternet
		{
			get
			{
				#if UNITY_EDITOR || UNITY_STANDALONE
				try
				{
					using (var client = new WebClient())
					{
						using (var stream = client.OpenRead("http://www.google.com"))
						{
							return true;
						}
					}
				}
				catch
				{
					return false;
				}
				#else
				return Application.internetReachability != NetworkReachability.NotReachable;
				#endif
			}
		}
	}
}