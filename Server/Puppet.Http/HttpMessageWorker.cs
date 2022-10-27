using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Puppet.Http.Utils;

namespace Puppet.Http
{
    public class HttpMessageWorker
    {
        private HttpListener _listener;
        private bool _isWorking = true;
        private ThreadSafeBoolToken _token = new ();
        
        public async Task Run(int port)
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add($"http://localhost:{port}/");
            _listener.Start();
            _token.ChangeValue(true);
            
            Debug.WriteLine("--- Server was started!");
            
            await Task.Run(ProcessHttp);
        }

        public Task Stop()
        {
            _listener.Stop();
            _token.ChangeValue(false);
            
            return Task.CompletedTask;
        }


        public void SetCommand(string command, string[] args)
        {
            
        }

        private async Task ProcessHttp()
        {
            while (_token.Value)
            {
                var context = await _listener.GetContextAsync();

                await HandleRequest(context.Request);

                await HandleResponse(context.Response);
            }
        }

        private Task HandleResponse(HttpListenerResponse contextResponse)
        {
            contextResponse.StatusCode = 200;
            contextResponse.Close();
            
            return Task.CompletedTask;
        }

        private async Task HandleRequest(HttpListenerRequest contextRequest)
        {
            var sr = new StreamReader(contextRequest.InputStream);
            
            var content = await sr.ReadToEndAsync();

            Debug.WriteLine($"--- Content: {content}");
        }
    }
}