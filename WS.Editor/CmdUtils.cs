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

        /// <summary>
        /// 0：正常退出 -1：失败退出，1：正常循环
        /// </summary>
        private int ExitCode { get; set; } = 1;

        public void Command(string command)
        {
            lock (CommandQueue)
            {
                CommandQueue.Enqueue(command);
                dataReadyEvent.Set();
            }
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
                if (cmdoom.Disposing || cmdoom.IsDisposed) return;
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
            Console.WriteLine("线程循环开始");
            while (dataReadyEvent.WaitOne())
            {
                // 循环退出验证
                if (ExitCode <= 0)
                {
                    Console.WriteLine($"ExitCode: {ExitCode}");
                    break;
                }

                // 读取命令
                var cmd = "";
                lock (CommandQueue)
                {
                    if(CommandQueue.Count > 0)
                    {
                        cmd = CommandQueue.Dequeue();
                    }
                    else
                    {
                        continue;
                    }
                }
                // 开始执行命令
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
                    startInfo.RedirectStandardError = true;  // 重定向错误
                    startInfo.CreateNoWindow = true;//不创建窗口  

                    var proc = new Process();
                    proc.StartInfo = startInfo;
                    Console.WriteLine("开启CMD进程");

                    // 异步委托事件读取错误信息
                    proc.ErrorDataReceived += new DataReceivedEventHandler(delegate (object sender, DataReceivedEventArgs e)
                    {
                        this.AppendText(cmdoom, e.Data+Environment.NewLine);
                        //Console.WriteLine($"在异步委托读取到的错误信息数据：{e.Data}");
                    });
                    proc.OutputDataReceived += new DataReceivedEventHandler(delegate (object sender, DataReceivedEventArgs e)
                    {
                        this.AppendText(cmdoom, e.Data + Environment.NewLine);
                        //Console.WriteLine($"在异步委托读取到的正常信息数据：{e.Data}");
                    });
                    proc.Start();
                    proc.BeginErrorReadLine();
                    proc.BeginOutputReadLine();

                    // 读取版权信息
                    //proc.StandardOutput.ReadLine();
                    //proc.StandardOutput.ReadLine();

                    // 输入命令
                    Console.WriteLine($"开始输入命令：{cmd}");
                    proc.StandardInput.WriteLine(cmd);
                    //this.AppendText(cmdoom, proc.StandardOutput.ReadLine() + Environment.NewLine);
                    //proc.StandardInput.WriteLine(Environment.NewLine);
                    //var write = proc.StandardOutput.ReadLine();
                    //this.AppendText(cmdoom, write +Environment.NewLine);
                    Console.WriteLine("命令输入完毕，回车换行");

                    //string pathPre = write.Substring(0, 2).ToUpper();

                    //// TODO 该循环是不安全的，输入的命令有问题将会造成死循环
                    //do
                    //{
                    //    if (proc.StandardOutput.EndOfStream)
                    //    {
                    //        Console.WriteLine("当前在CMD进程输出流的末尾");
                    //    }
                    //    // ReadLine(该函数需要读取到\r\n才会返回，否则会一值等待) 读取输出，因为输入流不能关闭，所以只能自己一行行读取输出
                    //    string logm = proc.StandardOutput.ReadLine()?.Trim() ?? "";
                    //    Console.WriteLine($"读取到的新行：{logm ?? "null"}");
                    //    if (logm.IndexOf(pathPre) != -1)
                    //    {
                    //        // 此时的logm为在输入命令前的路径
                    //        break;
                    //    }
                    //    this.AppendText(cmdoom, logm + Environment.NewLine);
                    //} while (true);
                    Console.WriteLine("关闭CMD进程");
                    //proc.Close();
                    proc.StandardInput.WriteLine("exit");
                    // 打开新的进程，比如Python，会卡死CMD进程，导致无法退出
                    proc.WaitForExit();
                    proc.Close();
                    //proc.Kill();
                    proc.Dispose();
                }
                catch (Exception e)
                {
                    var oldColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error {e.Message}{Environment.NewLine}{e.StackTrace}");
                    Console.ForegroundColor = oldColor;
                }
            }
            Console.WriteLine("线程循环结束");
        }

        /// <summary>
        /// 关闭CMD线程
        /// 修改退出码，发送空消息到线程
        /// </summary>
        public void CloseCmdThread()
        {
            ExitCode = 0;
            lock (CommandQueue)
            {
                // 发送空消息
                dataReadyEvent.Set();
            }
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
