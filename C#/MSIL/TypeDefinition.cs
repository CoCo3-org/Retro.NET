//
// Copyright (c) 2023 James Ross
//
// Released under the GNU AFFERO GENERAL PUBLIC LICENSE Version 3
// Please see README.md, LICENSE, agpl-3.0.txt in root folder
//

using System.Text;

using Cecil = Mono.Cecil;

namespace MSIL
{
	public class TypeDefinition
	{
		public Cecil.TypeDefinition CecilType { get; private set; }

		public string TypeName { get; set; }

		public ModuleDefinition ParentModule { get; private set; }

		// BaseType
		// Interfaces
		// NestedTypes
		// Methods
		// Fields
		// Events
		// Properties

		public List<MethodDefinition> MethodDefinitions { get; } = new List<MethodDefinition>();
		public Dictionary<string, MethodDefinition> MethodDefinitionDict { get; } = new Dictionary<string, MethodDefinition>();

		public TypeDefinition(ModuleDefinition parentModule, Cecil.TypeDefinition cecilType = null) 
		{
			this.CecilType = cecilType;
			this.ParentModule = parentModule;
		}

		public void Initialize() 
		{
			foreach (Cecil.MethodDefinition cecilMethodDefinition in this.CecilType.Methods)
			{
				MethodDefinition methodDefinition = new MethodDefinition(cecilMethodDefinition.Name, this, cecilMethodDefinition);
				this.MethodDefinitions.Add(methodDefinition);
				this.MethodDefinitionDict.Add(methodDefinition.CecilMethodDefinition.FullName, methodDefinition);
			
				methodDefinition.Initialize();
			}
		}

		public void CilListing() 
		{
			if (this.CecilType.FullName == "<Module>")
				// we don't know what this is yet so skip it for now!
				return;

			Console.WriteLine("------------------------------");
			Console.WriteLine("TYPE: " + CecilType.FullName);

			foreach (var methodDefinition in this.MethodDefinitions)
				methodDefinition.CilListing();
		}

		public void MC680x_TypeDefinition(StringBuilder sb) 
		{
			if (this.CecilType.FullName == "<Module>")
				return;

			sb.AppendLine("; =============================================================================");
			sb.AppendLine($"; TypeDefinition: [{this.CecilType.Name}][{this.CecilType.FullName}][{this.CecilType.GetType()}]");
			sb.AppendLine("; =============================================================================");

			foreach (var methoDefinition in this.MethodDefinitions)
				methoDefinition.MC680x_MethodDefinition(sb);
		}

		public void MC6x09_TypeDefinition(StringBuilder sb) 
        {
			if (this.CecilType.FullName == "<Module>")
				return;

			sb.AppendLine("* =============================================================================");
			sb.AppendLine($"* TypeDefinition: [{this.CecilType.Name}][{this.CecilType.FullName}][{this.CecilType.GetType()}]");
			sb.AppendLine("* =============================================================================");

			foreach (var methoDefinition in this.MethodDefinitions)
				methoDefinition.MC6x09_MethodDefinition(sb);
        }

		public void Z80_TypeDefinition(StringBuilder sb) 
		{
			sb.AppendLine("* =============================================================================");
			sb.AppendLine($"* TypeDefinition: [{this.CecilType.Name}][{this.CecilType.FullName}][{this.CecilType.GetType()}]");
			sb.AppendLine("* =============================================================================");

			foreach (var methoDefinition in this.MethodDefinitions)
				methoDefinition.Z80_MethodDefinition(sb);
		}

		public void M6502_TypeDefinition(StringBuilder sb) 
		{
			sb.AppendLine("* =============================================================================");
			sb.AppendLine($"* TypeDefinition: [{this.CecilType.Name}][{this.CecilType.FullName}][{this.CecilType.GetType()}]");
			sb.AppendLine("* =============================================================================");

			foreach (var methoDefinition in this.MethodDefinitions)
				methoDefinition.MOS6502_MethodDefinition(sb);
		}

		public void MC68000_TypeDefinition(StringBuilder sb) 
		{
			sb.AppendLine("* =============================================================================");
			sb.AppendLine($"* TypeDefinition: [{this.CecilType.Name}][{this.CecilType.FullName}][{this.CecilType.GetType()}]");
			sb.AppendLine("* =============================================================================");

			foreach (var methoDefinition in this.MethodDefinitions)
				methoDefinition.MC68000_MethodDefinition(sb);
		}

        public void i86_TypeDefinition(StringBuilder sb) 
        {
            sb.AppendLine("; =============================================================================");
            sb.AppendLine($"; TypeDefinition: [{this.CecilType.Name}][{this.CecilType.FullName}][{this.CecilType.GetType()}]");
            sb.AppendLine("; =============================================================================");

            foreach (var methoDefinition in this.MethodDefinitions)
                methoDefinition.i86_MethodDefinition(sb);
        }

        public void ix86_TypeDefinition(StringBuilder sb) 
        {
            sb.AppendLine("; =============================================================================");
            sb.AppendLine($"; TypeDefinition: [{this.CecilType.Name}][{this.CecilType.FullName}][{this.CecilType.GetType()}]");
            sb.AppendLine("; =============================================================================");

            foreach (var methoDefinition in this.MethodDefinitions)
                methoDefinition.ix86_MethodDefinition(sb);
        }
    }
}
