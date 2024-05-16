// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "can't be static", Scope = "member", Target = "~M:Tello.IO.Parser.Tests.Startup.ConfigureServices(Microsoft.Extensions.DependencyInjection.IServiceCollection)")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "unit test naming")]
[assembly: SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "unit test naming")]
