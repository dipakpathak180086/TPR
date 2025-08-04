// Decompiled with JetBrains decompiler
// Type: clsNetwork
// Assembly: UpdateApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F5D84818-6868-412F-9FDC-072D770F24A5
// Assembly location: D:\Projects\New Project\TPR\Code\DesktopApp\Ver10.0.0.1\TPR_App\TPR_App\bin\Debug\UpdateApp.exe

using System;
using System.Net.Sockets;
using System.Text;
using UpdateApp;

internal class clsNetwork
{
  private TcpClient client;
  private bool IS_CONNECTED = false;

  public string fnSendReceiveData(string strData)
  {
    string data = string.Empty;
    try
    {
      this.client = new TcpClient();
      this.client.SendTimeout = 3000;
      this.client.Connect(Program.mSockIp, Convert.ToInt32(Program.mSockPort));
      this.IS_CONNECTED = true;
      this.client.Client.Send(Encoding.ASCII.GetBytes(strData));
      byte[] numArray = new byte[50000000];
      int count = this.client.Client.Receive(numArray);
      data = Encoding.ASCII.GetString(numArray, 0, count);
      this.client.Client.Send(Encoding.ASCII.GetBytes("quit"));
      this.client.Close();
    }
    catch (Exception ex)
    {
      if (this.IS_CONNECTED)
        this.client.Close();
      data = "NO_CONNECTION";
    }
    finally
    {
      this.client = (TcpClient) null;
    }
    return data;
  }
}
