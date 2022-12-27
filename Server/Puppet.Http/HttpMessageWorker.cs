using System;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Puppet.Http.Commands;
using Puppet.Http.Utils;

namespace Puppet.Http
{
    public class HttpMessageWorker
    {
        #region Fields
        
        private HttpListener _listener;
        private ThreadSafeBoolToken _token = new ();
        private readonly CommandProvider _commandProvider = new ();
        private int _port = 5432;

        private const string BaseUri = "localhost";
        
        #endregion

        #region Properties
        
        public bool IsRunning => _token.Value;

        #endregion

        #region Public
        
        public Task Stop()
        {
            _listener?.Stop();
            _listener = null;
            _token.ChangeValue(false);
           
            Log("--- Http handler was stopped");
            return Task.CompletedTask;
        }

        public async Task Restart()
        {
            Log("--- Restarting Http handler");
            
            await Stop();
            Run(_port);
        }

        public void Run(int port)
        {
            _port = port;
            
            var wasStarted = RunHttpListener(port);

            _token.ChangeValue(wasStarted);

            if (wasStarted)
            {
                Log("--- Http handler was stared");
                Task.Run(ProcessHttp).ConfigureAwait(false);
            }
            else
            {
                Log("--- Http handler was not started due to inner issue");
            } 
        }

        private bool RunHttpListener(int port)
        {
            _listener = new HttpListener();

            var address = BuildUri(port);

            _listener.Prefixes.Add(address);
            _listener.Start();

            return _listener.IsListening;
        }

        private string BuildUri(int port)
        {
            return new UriBuilder("http", BaseUri, port).Uri.AbsoluteUri;
        }

        public void SetCommand(ICommand command)
        {
            switch (command)
            {
               case LogCommand logCommand:
                   Log($"--- Message: {logCommand.Message}");
                   break;
               case Commands.SetCommand:
                   _commandProvider?.SetCommand(command);
                   break;
            } 
        }
        
        public IEnumerable<string> CollectInfo()
        {
            yield return $"Message handler is working: {IsRunning}";
            yield return $"Message handler is running on {_port} port";
        }
        
        #endregion

        #region Private

        private async Task ProcessHttp()
        {
            while (_token.Value)
            {
                var context = await _listener.GetContextAsync();

                await HandleRequest(context.Request);

                var commandFromConsole = await _commandProvider?.GetCommand()!;
                
                await HandleResponse(context.Response, commandFromConsole);
            }
        }

        private async Task HandleResponse(HttpListenerResponse contextResponse, ICommand command)
        {
            var content = JsonSerializer.Serialize(command, command.GetType(), new JsonSerializerOptions { WriteIndented = true });

            contextResponse.ContentType = "application/json";

            var sr = new StreamWriter(contextResponse.OutputStream);
            await sr.WriteAsync(content);
            sr.Close();
            
            contextResponse.StatusCode = 200;
            contextResponse.Close();
        }
        
        private async Task HandleRequest(HttpListenerRequest contextRequest)
        {
            var sr = new StreamReader(contextRequest.InputStream);
            
            var content = await sr.ReadToEndAsync();

            Log($"--- Content: {content}");
        }


        private void Log(string message)
        {
            Console.WriteLine(message);    
        }
        
        #endregion
    }
}