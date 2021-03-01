using System;

namespace PadraoWebApi.XML
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace = "urn:MXMInvokable")]
    public partial class TUserProcessToken
    {
        public const Int32 EH_BATCH = 1;
        public const Int32 NAO_EH_BATCH = 0;

        private string userField;

        private string pwField;

        private string tokenField;

        private string ambField;

        /// <remarks/>
        public string User
        {
            get
            {
                return this.userField;
            }
            set
            {
                this.userField = value;
            }
        }

        /// <remarks/>
        public string Pw
        {
            get
            {
                return this.pwField;
            }
            set
            {
                this.pwField = value;
            }
        }

        /// <remarks/>
        public string Token
        {
            get
            {
                return this.tokenField;
            }
            set
            {
                this.tokenField = value;
            }
        }

        /// <remarks/>
        public string Amb
        {
            get
            {
                return this.ambField;
            }
            set
            {
                this.ambField = value;
            }
        }

        public string MultiAmbiente { get; set; }
        public string PathTemp { get; set; }

        public bool EnableMonitoring { get; set; }
        public string PathTempLog { get; set; }

        public string SalvarLogTabela { get; set; }
        public Int32 ProcessamentoBatch { get; set; }
		public string DiaDtRPS { get; set; }

        public Int32 IDProcess { get; set; }
    }
}
