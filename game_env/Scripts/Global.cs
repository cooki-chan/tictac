using Godot;
using System;
using System.Numerics;

public class Global : Node
{
    public static bool IsServer = false;
    public static int Health = 300000; //TODO: CHANGE THIS!!!
    public static String IP;
    public static String Port;

    public override void _Notification(int what)
{
    if (what == MainLoop.NotificationWmQuitRequest)
        GetTree().NetworkPeer = null;
        GetTree().Quit(); // default behavior
}
}
