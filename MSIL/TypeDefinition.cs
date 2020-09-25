using System;
using System.Collections.Generic;
using System.Text;

using Cecil = Mono.Cecil;

namespace MSIL
{
	public class TypeDefinition
	{
		public Cecil.TypeDefinition CecilType { get; private set; }

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

		public TypeDefinition(Cecil.TypeDefinition cecilType, ModuleDefinition parentModule) 
		{
			this.CecilType = cecilType;
			this.ParentModule = parentModule;

			// Console.WriteLine("=======================================");
			// Console.WriteLine("CLASS: " + cecilType.FullName);

			foreach (Cecil.MethodDefinition cecilMethodDefinition in cecilType.Methods)
			{
				MethodDefinition methodDefinition = new MethodDefinition(cecilMethodDefinition, this);
				this.MethodDefinitions.Add(methodDefinition);
				this.MethodDefinitionDict.Add(methodDefinition.CecilMethodDefinition.FullName, methodDefinition);
			}
		}

		public void CilListing() 
		{
			Console.WriteLine("------------------------------");
			Console.WriteLine("TYPE: " + CecilType.FullName);

			foreach (var methodDefinition in this.MethodDefinitions)
				methodDefinition.CilListing();
		}

		public void MC6801_TypeDefinition(StringBuilder sb) 
		{
			sb.AppendLine("* =============================================================================");
			sb.AppendLine($"* TypeDefinition: [{this.CecilType.Name}][{this.CecilType.FullName}][{this.CecilType.GetType()}]");
			sb.AppendLine("* =============================================================================");

			foreach (var methoDefinition in this.MethodDefinitions)
				methoDefinition.MC6809_MethodDefinition(sb);
		}

		public void MC6809_TypeDefinition(StringBuilder sb) 
        {
			sb.AppendLine("* =============================================================================");
			sb.AppendLine($"* TypeDefinition: [{this.CecilType.Name}][{this.CecilType.FullName}][{this.CecilType.GetType()}]");
			sb.AppendLine("* =============================================================================");

			foreach (var methoDefinition in this.MethodDefinitions)
				methoDefinition.MC6809_MethodDefinition(sb);
        }

		public void MC68000_TypeDefinition(StringBuilder sb)
		{
			sb.AppendLine("* =============================================================================");
			sb.AppendLine($"* TypeDefinition: [{this.CecilType.Name}][{this.CecilType.FullName}][{this.CecilType.GetType()}]");
			sb.AppendLine("* =============================================================================");

			foreach (var methoDefinition in this.MethodDefinitions)
				methoDefinition.MC68000_MethodDefinition(sb);
		}

		public void Z80_TypeDefinition(StringBuilder sb)
		{
			sb.AppendLine("* =============================================================================");
			sb.AppendLine($"* TypeDefinition: [{this.CecilType.Name}][{this.CecilType.FullName}][{this.CecilType.GetType()}]");
			sb.AppendLine("* =============================================================================");

			foreach (var methoDefinition in this.MethodDefinitions)
				methoDefinition.Z80_MethodDefinition(sb);
		}
	}
}
