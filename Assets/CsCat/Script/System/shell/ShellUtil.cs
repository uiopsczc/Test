using System;
using System.Diagnostics;
using UnityEditor;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using CsCat;

namespace CsCat
{
  public class ShellUtil
  {
    private static List<Action> _queue = new List<Action>();
    private List<string> _enviroument_var_list = new List<string>();

    private static string shellApp
    {
      get
      {
#if UNITY_EDITOR_WIN
        string app = "cmd.exe";
#elif UNITY_EDITOR_OSX
			string app = "bash";
#endif
        return app;
      }
    }

    static ShellUtil()
    {
      EditorApplication.update += OnUpdate;
    }

    private static void OnUpdate()
    {
      foreach (var action in _queue)
      {
        try
        {
          action?.Invoke();
        }
        catch (Exception e)
        {
          LogCat.LogError(e);
        }
      }

      _queue.Clear();
    }


    public static ShellRequest ProcessCommand(string cmd, string workDirectory = "",
      List<string> environmentVars = null)
    {
      ShellRequest shellReqest = new ShellRequest();
      ThreadPool.QueueUserWorkItem(delegate(object state)
      {
        Process process = null;
        try
        {
          ProcessStartInfo processStartInfo = new ProcessStartInfo(shellApp);

#if UNITY_EDITOR_OSX
				string splitChar = ":";
				start.Arguments = "-c";
#elif UNITY_EDITOR_WIN
          string splitChar = ";";
          processStartInfo.Arguments = "/c";
#endif

          if (environmentVars != null)
          {
            foreach (string var in environmentVars)
            {
              processStartInfo.EnvironmentVariables["PATH"] += (splitChar + var);
            }
          }

          processStartInfo.Arguments += (" \"" + cmd + " \"");
          processStartInfo.CreateNoWindow = true;
          processStartInfo.ErrorDialog = true;
          processStartInfo.UseShellExecute = false;
          processStartInfo.WorkingDirectory = workDirectory;

          if (processStartInfo.UseShellExecute)
          {
            processStartInfo.RedirectStandardOutput = false;
            processStartInfo.RedirectStandardError = false;
            processStartInfo.RedirectStandardInput = false;
          }
          else
          {
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.RedirectStandardError = true;
            processStartInfo.RedirectStandardInput = true;
            processStartInfo.StandardOutputEncoding = Encoding.UTF8;
            processStartInfo.StandardErrorEncoding = Encoding.UTF8;
          }
          process = Process.Start(processStartInfo);
          process.ErrorDataReceived += delegate(object sender, DataReceivedEventArgs e) { LogCat.LogError(EncodingUtil.GBK2UTF8(e.Data)); };
          process.OutputDataReceived += delegate(object sender, DataReceivedEventArgs e) { LogCat.LogError(EncodingUtil.GBK2UTF8(e.Data)); };
          process.Exited += delegate(object sender, EventArgs e) { LogCat.LogError(e.ToString()); };

          bool is_has_error = false;
          do
          {
            string line = process.StandardOutput.ReadLine();
            if (line == null)
              break;

            line = line.Replace("\\", "/");
            _queue.Add(delegate() { shellReqest.Log(0, line); });
          } while (true);

          while (true)
          {
            string error = process.StandardError.ReadLine();
            if (string.IsNullOrEmpty(error))
              break;

            is_has_error = true;
            _queue.Add(delegate() { shellReqest.Log(LogCatType.Error, error); });
          }

          process.Close();
          if (is_has_error)
            _queue.Add(delegate() { shellReqest.Error(); });
          else
            _queue.Add(delegate() { shellReqest.NotifyDone(); });
        }
        catch (Exception e)
        {
          LogCat.LogError(e);
          process?.Close();
        }
      });
      return shellReqest;
    }




    public void AddEnvironmentVars(params string[] vars)
    {
      foreach (var var in vars)
      {
        if (var.IsNullOrWhiteSpace())
          continue;
        _enviroument_var_list.Add(var);
      }
    }

    public ShellRequest ProcessCMD(string cmd, string work_direcotry)
    {
      return ShellUtil.ProcessCommand(cmd, work_direcotry, _enviroument_var_list);
    }
  }
}