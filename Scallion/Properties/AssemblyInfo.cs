using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("Scallion")]
[assembly: AssemblyDescription("A wrapper library for MikuMikuDance")]
[assembly: AssemblyCompany("paltee.net")]
[assembly: AssemblyProduct("Scallion")]
[assembly: AssemblyCopyright("Copyright (C) 2016 Paralleltree")]

[assembly: ComVisible(false)]
[assembly: Guid("3934e015-89f5-4214-80df-71b57f62c2cd")]

[assembly: AssemblyVersion("1.0.0.0")]

// Exposes internal components to the testing project.
[assembly: InternalsVisibleTo("Scallion.Tests")]
