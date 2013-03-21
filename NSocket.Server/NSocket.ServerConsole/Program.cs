﻿using SocketLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace NSocket.ServerConsole
{
    class Program
    {
        static SocketListener listener;
        static void Main(string[] args)
        {
            listener = new SocketLib.SocketListener(3, 1024, IP);
            listener.OnMsgReceived += listener_OnMsgReceived;
            listener.OnSended += listener_OnSended;
            listener.StartListenThread += listener_StartListenThread;
            listener.ClientAccepted += listener_ClientAccepted;
            listener.Init();
            listener.Start(7890);
            //listener.Listen();

            string cmd = string.Empty;
            while ((cmd = Console.ReadLine().Trim()) != "Q")
            {
                switch (cmd.ToUpper())
                {
                    case "STOP":
                        listener.Stop();
                        break;
                    default:
                        break;
                }
            }
        }

        static void listener_ClientAccepted(string uid)
        {
            listener.Send(uid, uid);
        }

        static void listener_OnSended(string uid, string exception)
        {
            Console.WriteLine("Sended: {0} {1}", uid, exception);
        }

        static void listener_OnMsgReceived(string uid, string info)
        {
            Console.WriteLine("Received:{0} {1}", uid, info);
            listener.Send(uid, info);
        }

        static void listener_StartListenThread()
        {
            Console.WriteLine("Start Listenning...");
        }

        static string IP(string ip)
        {
            return ip;
        }
    }
}
