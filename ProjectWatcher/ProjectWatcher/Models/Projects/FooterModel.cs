using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectWatcher.Models.Projects
{
    public class FooterModel
    {
        private UploadModel upload;

        public String Filter
        {
            get;
            set;
        }

        public String TableDefinition
        {
            get;
            set;
        }

        public String AddProjectTitle
        {
            get;
            set;
        }

        public Boolean IsAdmin
        {
            get;
            set;
        }

        public String ExportTitle
        {
            get;
            set;
        }

        public String UploadTitle
        {
            get;
            set;
        }

        public String UploadResultMessage 
        {
            set
            {
                if (upload == null)
                {
                    upload = new UploadModel();
                }
                upload.SuccessMessage = value; 
            }
        }

        public String SubmitUploadTitle
        {
            set 
            {
                if (upload == null)
                {
                    upload = new UploadModel();
                }
                upload.SubmitTitle = value;
            }
        }

        public UploadModel Upload
        {
            get 
            {
                return upload;
            }
        }

        
    }
}