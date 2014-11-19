using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Frameshop.Data
{
    internal class TemporaryFile : IDisposable
    {
        private List<string> relatedFiles = null;

        public string FilePath
        {
            get;
            private set;
        }

        public FileInfo FileInfo
        {
            get { return new FileInfo(FilePath); }
        }

        public TemporaryFile()
            : this(Path.GetTempPath())
        {
        }

        public TemporaryFile(string directory)
        {
            relatedFiles = new List<string>();

            string file = Path.GetRandomFileName();
            file = file.Substring(0, file.Length - 4);
            Create(Path.Combine(directory, file));
        }

        public TemporaryFile(FileInfo copyFrom)
        {
            relatedFiles = new List<string>();

            string file = Path.GetFileName(copyFrom.FullName);
            Copy(Path.Combine(Path.GetTempPath(), file), copyFrom);
        }

        ~TemporaryFile()
        {
            Delete();
        }

        public void Dispose()
        {
            Delete();
            GC.SuppressFinalize(this);
        }

        public TemporaryFile AddRelatedFile(string path)
        {
            relatedFiles.Add(path);

            return this;
        }

        private void Copy(string path, FileInfo copyFrom)
        {
            FilePath = path;
            copyFrom.CopyTo(FilePath, true);
        }

        private void Create(string path)
        {
            FilePath = path;
            using (File.Create(FilePath))
            {
            }
        }

        public void Delete()
        {
            if (FilePath == null) return;
            File.Delete(FilePath);
            FilePath = null;

            foreach (string file in relatedFiles)
            {
                if (File.Exists(file))
                    File.Delete(file);
            }
        }
    }
}
