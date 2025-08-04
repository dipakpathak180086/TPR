// Decompiled with JetBrains decompiler
// Type: UpdateApp.Program
// Assembly: UpdateApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F5D84818-6868-412F-9FDC-072D770F24A5
// Assembly location: D:\Projects\New Project\TPR\Code\DesktopApp\Ver10.0.0.1\TPR_App\TPR_App\bin\Debug\UpdateApp.exe

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace UpdateApp
{
    internal class Program
    {
        internal static string mSockIp = "";
        internal static int mSockPort;
        private static string ProcessExeName = "";

        private static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("***Please hold on, while we are downloading new app version. Please do not close this window.***");
                Console.WriteLine("**********Download in progress**********");
                string str1 = Program.ReadCommServerSetting();
                if (str1 == "OK")
                {
                    foreach (Process process in Process.GetProcessesByName(Program.ProcessExeName.Replace(".exe", "")))
                        process.Kill();
                    string[] strArray = new clsNetwork().fnSendReceiveData("GET_NEWEXE_DESKTOP~}").Split('~');
                    string str2 = strArray[0];
                    if (!(str2 == "VALID"))
                    {
                        if (!(str2 == "INVALID"))
                        {
                            if (!(str2 == "ERROR"))
                            {
                                if (str2 == "NO_CONNECTION")
                                    Console.WriteLine("########## Error : Server not connected ##########");
                                else
                                    Console.WriteLine("########## Error : No match from the server ##########");
                            }
                            else
                                Console.WriteLine("########## Error : " + strArray[1] + " ##########");
                        }
                        else
                            Console.WriteLine("########## Error : " + strArray[1] + " ##########");
                    }
                    else
                    {
                        for (int i = 1; i < strArray.Length; i++)
                        {
                            string s = strArray[i].Split('|')[1];
                            string str3 = strArray[i].Split('|')[0];
                            byte[] bytes = Convert.FromBase64String(s);
                            string directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                            if (!Directory.Exists(directoryName + "\\AppVersion"))
                                Directory.CreateDirectory(directoryName + "\\AppVersion");
                            File.WriteAllBytes(directoryName + "\\AppVersion\\" + str3, bytes);
                            File.Copy(directoryName + "\\AppVersion\\" + str3, directoryName + "\\" + str3, true);
                        }
                        
                        Console.WriteLine("**********Download completed successfully, now start the application**********");
                    }
                }
                else
                    Console.WriteLine(str1);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.Message);
            }
            Console.WriteLine("########## You can close the window now ##########");
            Console.ReadLine();
        }

        private static string ReadCommServerSetting()
        {
            StreamReader streamReader = (StreamReader)null;
            try
            {
                streamReader = new StreamReader(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\DBSettings.txt");
                streamReader.ReadLine();
                streamReader.ReadLine();
                streamReader.ReadLine();
                streamReader.ReadLine();
                string str1 = streamReader.ReadLine();
                if (str1 == null || str1 == "")
                    return "Error : Comm Server setting not found please check";
                Program.mSockIp = str1.Split('=')[1].Trim();
                Program.mSockPort = Convert.ToInt32(streamReader.ReadLine().Split('=')[1].Trim());
                string str2 = streamReader.ReadLine();
                if (str2 == null || str2 == "")
                    return "Error : Exe name not found in setting file";
                Program.ProcessExeName = str2.Split('=')[1].Trim();
                return "OK";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (streamReader != null)
                {
                    streamReader.Close();
                    streamReader.Dispose();
                }
            }
        }
    }
}
