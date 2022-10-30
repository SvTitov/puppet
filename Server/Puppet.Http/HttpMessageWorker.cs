using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private int _port;

        private const string BaseUri = "localhost";
        
        #endregion

        #region Properties
        
        public bool IsRunning => _token.Value;

        #endregion

        #region Public
        
        public Task Stop()
        {
            _listener.Stop();
            _listener = null;
            _token.ChangeValue(false);
            
            return Task.CompletedTask;
        }

        public async Task Run(int port)
        {
            _port = port;
            
            _listener = new HttpListener();

            var address = new UriBuilder("http", BaseUri, _port).Uri.AbsoluteUri;

            _listener.Prefixes.Add(address);
            _listener.Start();

            _token.ChangeValue(_listener.IsListening);

            if (_listener.IsListening)
            {
                await Task.Run(ProcessHttp);
                Log("--- Http handler was stared");
            }
            else
            {
                Log("--- Http handler was not started due to inner issue");
            } 
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

                await HandleResponse(context.Response);
            }
        }

        private async Task HandleResponse(HttpListenerResponse contextResponse)
        {
            var command = await _commandProvider?.GetCommand()!;

            var content = JsonSerializer.Serialize(command);

            contextResponse.ContentType = "application/json";

            var sr = new StreamWriter(contextResponse.ContentType);
            await sr.WriteAsync(content);
            
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