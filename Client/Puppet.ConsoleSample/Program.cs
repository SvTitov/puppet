// See https://aka.ms/new-console-template for more information

using Puppet.ConsoleSample;

HttpClient client = new HttpClient();


var f = new FooClass();
var r = await f.PUPPET_ESTABLISH_CONNECTION().ConfigureAwait(false);
Console.WriteLine(r);
