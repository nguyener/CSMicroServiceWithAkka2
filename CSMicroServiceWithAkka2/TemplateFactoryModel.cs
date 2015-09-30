using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace CSMicroServiceWithAkka2
{

    public class Template
    {
        public int TemplateId { get; set; }
        public string Name { get; set; }

        //public virtual List<TemplateFile> TemplateFiles { get; set; }
    }


    public class TemplateFile
    {
        public int TemplateFileId { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }

        public int TemplateId { get; set; }
        public virtual Template Template { get; set; }

    }
    class TemplateFactory: DbContext
    {
        public DbSet<Template> Templates { get; set; }

        //public DbSet<TemplateFile> TemplateFiles { get; set; }
    }
}
