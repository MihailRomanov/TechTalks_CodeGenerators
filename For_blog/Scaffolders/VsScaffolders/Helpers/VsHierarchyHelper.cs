using EnvDTE;
using Microsoft.VisualStudio.Shell.Interop;
using System;

namespace VsScaffolders.Helpers
{
    public static class VsHierarchyHelper
    {
        public static IVsHierarchy GetProjectHierarchy(
            IServiceProvider serviceProvider,
            Project project)
        {
            IVsHierarchy ivsHierarchy = null;
            if (serviceProvider != null && project != null)
                ((IVsSolution)serviceProvider.GetService(typeof(IVsSolution)))?.GetProjectOfUniqueName(project.UniqueName, out ivsHierarchy);
            return ivsHierarchy;
        }
    }
}
