// See https://aka.ms/new-console-template for more information

using System.Runtime.CompilerServices;
using Puppet.ConsoleSample;

var f = new FooClass();
await f.PUPPET_ESTABLISH_CONNECTION().ConfigureAwait(false);
