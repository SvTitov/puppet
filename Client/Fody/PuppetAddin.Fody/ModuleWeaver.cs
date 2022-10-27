using System.Diagnostics;
using Fody;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace PuppetAddin.Fody
{
    public class ModuleWeaver : BaseModuleWeaver
    {
        private const string AttributeName = "PuppetAddin.PuppetAttribute";
        
        public override void Execute()
        {
#if DEBUG
            Debugger.Launch();
#endif
            foreach (var moduleDefinitionType in this.ModuleDefinition.Types)
            {
                if (moduleDefinitionType.CustomAttributes.Any(x => x.Constructor.FullName == AttributeName))
                {
                    var md = new MethodDefinition("Puppet_Connector", MethodAttributes.Public,
                        new TypeDefinition("System.Void", "Void", TypeAttributes.Abstract));
                    var ilProcessor = md.Body.GetILProcessor();

                    ilProcessor.InsertBefore(Instruction.Create(OpCodes.Ldarg_0, new VariableDefinition() ), ); 
                    
                    moduleDefinitionType.Methods.Add(md); 
                }
            }
            
            WriteInfo("--- Fody was applied! Info");
            WriteWarning("--- Fody was applied! Warning");
        }

        public override IEnumerable<string> GetAssembliesForScanning()
        {
            yield return "netstandard";
            yield return "mscorlib";
        }
    }
}