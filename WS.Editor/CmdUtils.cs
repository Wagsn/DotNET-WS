using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WS.Editor
{
    /// <summary>
    /// 自定义CMD窗口，参考：https://blog.csdn.net/u013151336/article/details/51301779/
    /// </summary>
    public class CmdUtils
    {
        Queue<string> CommandQueue { get; set; } = new Queue<string>();

        AutoResetEvent dataReadyEvent { get; } = new AutoResetEvent(false);

        private CmdForm CmdForm { get; set; }

        public void Command(string command)
        {
            lock (CommandQueue)
            {
                CommandQueue.Enqueue(command);
                dataReadyEvent.Set();
            }
        }

        private static Process GetReadyCmdProc()
        {
            var cmdProcess = new Process();//创建进程对象  
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd.exe";//设定需要执行的命令  
            startInfo.Arguments = "";//“/C”表示执行完命令后马上退出  
            startInfo.UseShellExecute = false;//不使用系统外壳程序启动  
            startInfo.RedirectStandardInput = true;//不重定向输入  
            startInfo.RedirectStandardOutput = true; //重定向输出  
            startInfo.CreateNoWindow = true;//不创建窗口  
            cmdProcess.StartInfo = startInfo;
            return cmdProcess;
        }

        /// <summary>
        /// 为日志框追加数据
        /// </summary>
        /// <param name="text"></param>
        private void AppendText(CmdForm cmdoom, string text)
        {
            if (cmdoom.cmdLogTextArea.InvokeRequired)
            {
                while (!cmdoom.cmdLogTextArea.IsHandleCreated)
                {
                    if (cmdoom.cmdLogTextArea.Disposing || cmdoom.cmdLogTextArea.IsDisposed) return;
                }
                UpdateLog set = delegate ()
                {
                    cmdoom.cmdLogTextArea.AppendText(/*Environment.NewLine + */text);
                };
                cmdoom.Invoke(set);
            }
            else
            {
                cmdoom.cmdLogTextArea.AppendText(text);
            }
        }

        /// <summary>
        /// 需要退出循环
        /// </summary>
        /// <param name="cmdoom"></param>
        public void Loop(CmdForm cmdoom)
        {
            while (dataReadyEvent.WaitOne())
            {
                var cmd = "";
                lock (CommandQueue)
                {
                    if(CommandQueue.Count > 0)
                    {
                        cmd = CommandQueue.Dequeue();
                    }
                }
                //Cmd2(cmd);
                try
                {
                    if (cmdoom == null)
                    {
                        Console.WriteLine("CmdForm must be not instantiation!");
                        return;
                    }
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = "cmd.exe";//设定需要执行的命令  
                    startInfo.Arguments = "";//“/C”表示执行完命令后马上退出  
                    startInfo.UseShellExecute = false;//不使用系统外壳程序启动  
                    startInfo.RedirectStandardInput = true;//不重定向输入  
                    startInfo.RedirectStandardOutput = true; //重定向输出  
                    startInfo.CreateNoWindow = true;//不创建窗口  

                    var proc = new Process();
                    proc.StartInfo = startInfo;
                    proc.Start();

                    proc.StandardOutput.ReadLine();
                    proc.StandardOutput.ReadLine();

                    //this.AppendText("Before write line: "+ CmdProc.StandardOutput.ReadLine() + Environment.NewLine);
                    proc.StandardInput.WriteLine(cmd);
                    this.AppendText(cmdoom, proc.StandardOutput.ReadLine() + Environment.NewLine);
                    proc.StandardInput.WriteLine(Environment.NewLine);
                    var write = proc.StandardOutput.ReadLine();
                    this.AppendText(cmdoom, write +Environment.NewLine);

                    //string log = CmdProc.StandardOutput.ReadLine();
                    //this.AppendText(CmdProc.StandardOutput.ReadLine()+ Environment.NewLine);
                    //this.AppendText(log + Environment.NewLine);
                    string pathPre = write.Substring(0, 2).ToUpper();

                    do
                    {
                        // ReadLine 读取输出，因为输入流不能关闭，所以只能自己一行行读取输出
                        string logm = proc.StandardOutput.ReadLine()?.Trim() ?? "";
                        if (logm.IndexOf(pathPre) != -1)
                        {
                            // 在输入命令前的路径
                            break;
                        }
                        this.AppendText(cmdoom, "------------------------ Read New Line" + Environment.NewLine);
                        this.AppendText(cmdoom, logm + Environment.NewLine);

                    } while (true);

                    Console.WriteLine("关闭CMD进程");
                    proc.Close();
                    break;
                }
                catch (Exception e)
                {
                    var oldColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error {e.Message}{Environment.NewLine}{e.StackTrace}");
                    Console.ForegroundColor = oldColor;
                }
            }
            Console.WriteLine("退出循环");
        }

        public void OldLoop(CmdForm cmdoom)
        {
            //Process cmd = null;
            //if (cmd == null)
            //{
            //    cmd = new Process();//创建进程对象  
            //    ProcessStartInfo startInfo = new ProcessStartInfo();
            //    startInfo.FileName = "cmd.exe";//设定需要执行的命令  
            //    startInfo.Arguments = "";//“/C”表示执行完命令后马上退出  
            //    startInfo.UseShellExecute = false;//不使用系统外壳程序启动  
            //    startInfo.RedirectStandardInput = true;//不重定向输入  
            //    startInfo.RedirectStandardOutput = true; //重定向输出  
            //    startInfo.CreateNoWindow = true;//不创建窗口  
            //    cmd.StartInfo = startInfo;
            //    // cmd.Start();
            //}
            //if (cmd.Start())//开始进程  
            //{
            //    cmd.StandardOutput.ReadLine().Trim();
            //    cmd.StandardOutput.ReadLine().Trim();
            //    while (cmdoom.isRun.IndexOf("start") != -1)
            //    {
            //        // 循环查看shell是否有值，有值则执行命令
            //        if (shell.Length > 0)
            //        {
            //            cmd.StandardInput.WriteLine(shell);
            //            cmd.StandardOutput.ReadLine().Trim();

            //            cmd.StandardInput.WriteLine(Environment.NewLine);
            //            if(shell == "exit")
            //            {
            //                break;
            //            }
            //            String log = cmd.StandardOutput.ReadLine().Trim();
            //            String path = log.Substring(0, 2).ToUpper();
            //            updateLog(cmdoom, log);
            //            log = "";
            //            do
            //            {
            //                String logm = cmd.StandardOutput.ReadLine()?.Trim() ?? "";
            //                if (logm.IndexOf(path) != -1)
            //                {
            //                    break;
            //                }
            //                updateLog(cmdoom, logm + Environment.NewLine);
            //                log += logm;

            //            } while (true);

            //            shell = "";
            //        }
            //    }

            //    cmd.Close();

            //    cmd = null;
            //    return;
            //}
            //return;
        }

        private delegate void UpdateLog();
    }
}
