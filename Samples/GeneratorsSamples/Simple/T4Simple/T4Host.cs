using Microsoft.VisualStudio.TextTemplating;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace T4Simple
{
    class T4Host : ITextTemplatingEngineHost, ITextTemplatingSessionHost
    {
        internal string TemplateFileValue;
        public string TemplateFile
        {
            get { return TemplateFileValue; }
        }

        private string fileExtensionValue = ".txt";
        public string FileExtension
        {
            get { return fileExtensionValue; }
        }

        private Encoding fileEncodingValue = Encoding.UTF8;
        public Encoding FileEncoding
        {
            get { return fileEncodingValue; }
        }

        private CompilerErrorCollection errorsValue;
        public CompilerErrorCollection Errors
        {
            get { return errorsValue; }
        }

        public IList<string> StandardAssemblyReferences
        {
            get
            {
                return new string[]
                {
                    typeof(System.Uri).Assembly.Location
                };
            }
        }
        public IList<string> StandardImports
        {
            get
            {
                return new string[]
                {
                    "System"
                };
            }
        }

        public bool LoadIncludeText(string requestFileName, out string content, out string location)
        {
            content = System.String.Empty;
            location = System.String.Empty;


            if (File.Exists(requestFileName))
            {
                content = File.ReadAllText(requestFileName);
                return true;
            }

            else
            {
                return false;
            }
        }

        public object GetHostOption(string optionName)
        {
            object returnObject;
            switch (optionName)
            {
                case "CacheAssemblies":
                    returnObject = true;
                    break;
                default:
                    returnObject = null;
                    break;
            }
            return returnObject;
        }

        public string ResolveAssemblyReference(string assemblyReference)
        {

            if (File.Exists(assemblyReference))
            {
                return assemblyReference;
            }

            string candidate = Path.Combine(Path.GetDirectoryName(this.TemplateFile), assemblyReference);
            if (File.Exists(candidate))
            {
                return candidate;
            }

            var a = AppDomain.CurrentDomain.GetAssemblies().
                SingleOrDefault(assembly => assembly.GetName().Name == assemblyReference);

            if (a != null)
                return a.Location;

            return "";
        }

        public Type ResolveDirectiveProcessor(string processorName)
        {
            if (string.Compare(processorName, "XYZ", StringComparison.OrdinalIgnoreCase) == 0)
            {
                //return typeof();
            }

            throw new Exception("Directive Processor not found");
        }

        public string ResolvePath(string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException("the file name cannot be null");
            }

            if (File.Exists(fileName))
            {
                return fileName;
            }

            string candidate = Path.Combine(Path.GetDirectoryName(this.TemplateFile), fileName);
            if (File.Exists(candidate))
            {
                return candidate;
            }

            return fileName;
        }

        public string ResolveParameterValue(string directiveId, string processorName, string parameterName)
        {
            if (directiveId == null)
            {
                throw new ArgumentNullException("the directiveId cannot be null");
            }
            if (processorName == null)
            {
                throw new ArgumentNullException("the processorName cannot be null");
            }
            if (parameterName == null)
            {
                throw new ArgumentNullException("the parameterName cannot be null");
            }

            return String.Empty;
        }

        public void SetFileExtension(string extension)
        {
            fileExtensionValue = extension;
        }
        public void SetOutputEncoding(System.Text.Encoding encoding, bool fromOutputDirective)
        {
            fileEncodingValue = encoding;
        }

        public void LogErrors(CompilerErrorCollection errors)
        {
            errorsValue = errors;
        }

        public AppDomain ProvideTemplatingAppDomain(string content)
        {
            return AppDomain.CurrentDomain;// CreateDomain("Generation App Domain");
        }

        ITextTemplatingSession session = new TextTemplatingSession();

        public ITextTemplatingSession CreateSession()
        {
            return session;
        }

        public ITextTemplatingSession Session
        {
            get
            {
                return session;
            }
            set
            {
                session = value;
            }
        }
    }
}
